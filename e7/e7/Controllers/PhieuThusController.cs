using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using e7.Data;
using e7.Models;
using e7.Models.ViewModels;

namespace e7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuThusController : ControllerBase
    {
        private readonly PhieuThuDbContext _context;

        public PhieuThusController(PhieuThuDbContext context)
        {
            _context = context;
        }

        // GET: api/PhieuThus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhieuThu>>> GetPhieuThus()
        {
            return await _context.PhieuThus.ToListAsync();
        }

        // POST: api/PhieuThus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostPhieuThu([FromBody] PhieuThuViewModel phieuThuView)
        {
            if (phieuThuView == null || phieuThuView.ChiTietPhieuThus == null || phieuThuView.ChiTietPhieuThus.Count == 0)
            {
                return BadRequest("Dữ liệu không hợp lệ hoặc danh sách chi tiết phiếu thu trống.");
            }

            if (!DateTime.TryParse(phieuThuView.NgayLap.ToString(), out var ngayLap)) 
            {
                return BadRequest("Ngày lập không hợp lệ định dạng đúng yyyy/MM/dd");
            }

            if (string.IsNullOrWhiteSpace(phieuThuView.NhanVienLap))
            {
                return BadRequest("Nhân viên lập không hợp lệ");
            }

            var phieuThu = new PhieuThu
            {
                NgayLap = phieuThuView.NgayLap,
                NhanVienLap = phieuThuView.NhanVienLap,
                GhiChu = phieuThuView.GhiChu,
                ThanhTien = 0
            };
            _context.PhieuThus.Add(phieuThu);
            await _context.SaveChangesAsync();

            double tongThanhTien = 0;
            foreach (var chiTiet in phieuThuView.ChiTietPhieuThus)
            {
                var nguyenLieu = await _context.NguyenLieus.FindAsync(chiTiet.NguyenLieuId);
                if (nguyenLieu == null)
                {
                    return NotFound("Nguyên liệu không tồn tại.");
                }

                if (chiTiet.SoLuongBan <= 0)
                {
                    return BadRequest("Nhập số lượng bán");
                }

                var chiTietPhieuThu = new ChiTietPhieuThu
                {
                    PhieuThuId = phieuThu.PhieuThuId,
                    NguyenLieuId = chiTiet.NguyenLieuId,
                    SoLuongBan = chiTiet.SoLuongBan
                };

                _context.ChiTietPhieuThus.Add(chiTietPhieuThu);
                nguyenLieu.SoLuongKho -= chiTiet.SoLuongBan;
                if (nguyenLieu.SoLuongKho < 0 || chiTiet.SoLuongBan > nguyenLieu.SoLuongKho)
                {
                    return BadRequest("Số lượng tồn kho không đủ");
                }

                tongThanhTien += chiTiet.SoLuongBan * nguyenLieu.GiaBan;
            }
            phieuThu.ThanhTien = tongThanhTien;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                PhieuThuId = phieuThu.PhieuThuId,
                ThanhTien = phieuThu.ThanhTien
            });
        }

        [HttpPost("ChiTiet/{PhieuThuId}")]
        public async Task<IActionResult> PostChiTietPhieuThu(int PhieuThuId, [FromBody] List<ChiTietPhieuThu> ChiTiet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var phieuThu = await _context.PhieuThus
                                          .Include(p => p.ChiTietPhieuThus)
                                          .FirstOrDefaultAsync(p => p.PhieuThuId == PhieuThuId);
            if (phieuThu == null)
            {
                return NotFound("Phiếu thu không tồn tại.");
            }

            foreach (var ct in ChiTiet)
            {
                if (ct.NguyenLieuId <= 0)
                {
                    return BadRequest("Nguyên liệu không hợp lệ.");
                }

                if(ct.SoLuongBan <= 0)
                {
                    return BadRequest("Nhập số lượng bán");
                }

                var nguyenLieu = await _context.NguyenLieus.FindAsync(ct.NguyenLieuId);
                if (nguyenLieu == null)
                {
                    return BadRequest("Nguyên liệu không tồn tại.");
                }

                phieuThu.ChiTietPhieuThus.Add(ct);

                if (nguyenLieu.SoLuongKho >= ct.SoLuongBan)
                {
                    nguyenLieu.SoLuongKho -= ct.SoLuongBan;
                }
                else
                {
                    return BadRequest("Số lượng kho không đủ.");
                }
            }

            double thanhTien = 0;
            foreach (var ct in phieuThu.ChiTietPhieuThus)
            {
                var nguyenLieu = await _context.NguyenLieus.FindAsync(ct.NguyenLieuId);
                thanhTien += ct.SoLuongBan * nguyenLieu.GiaBan;
            }

            phieuThu.ThanhTien = thanhTien;

            await _context.SaveChangesAsync();
            return Ok(phieuThu);
        }

        // DELETE: api/PhieuThus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhieuThu(int id)
        {
            var phieuThu = await _context.PhieuThus.FindAsync(id);
            if (phieuThu == null)
            {
                return NotFound();
            }

            _context.PhieuThus.Remove(phieuThu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("GetPhieuThuByDateRange")]
        public async Task<IActionResult> GetPhieuThus(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.");
            }
            var phieuThus = await _context.PhieuThus
                                           .Include(p => p.ChiTietPhieuThus)
                                           .Where(p => p.NgayLap >= startDate && p.NgayLap <= endDate)
                                           .ToListAsync();

            if (phieuThus == null || !phieuThus.Any())
            {
                return NotFound("Không tìm thấy phiếu thu nào trong khoảng thời gian đã cho.");
            }

            return Ok(phieuThus);
        }

        private bool PhieuThuExists(int id)
        {
            return _context.PhieuThus.Any(e => e.PhieuThuId == id);
        }
    }
}
