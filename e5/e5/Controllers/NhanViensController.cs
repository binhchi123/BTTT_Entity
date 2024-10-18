using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using e5.Data;
using e5.Models;
using e5.Models.ViewModels;

namespace e5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanViensController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public NhanViensController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: api/NhanViens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NhanVien>>> GetNhanViens()
        {
            return await _context.NhanViens.ToListAsync();
        }

        // POST: api/NhanViens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhanCong>> PostNhanVien([FromBody] NhanVienViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var duAn = await _context.DuAns.FindAsync(viewModel.DuAnId);
            if (duAn == null)
            {
                return NotFound("Dự án không tồn tại.");
            }

            if (viewModel.NgaySinh < new DateTime(1970, 1, 1) || viewModel.NgaySinh > new DateTime(2000, 12, 31))
            {
                return BadRequest("Ngày sinh phải từ năm 1970 đến 2000.");
            }

            if (viewModel.HeSoLuong <= 0)
            {
                viewModel.HeSoLuong = 1;
            }

            if (viewModel.SoGioLam <= 0)
            {
                viewModel.SoGioLam = 8;
            }

            var nhanVien = new NhanVien
            {
                HoTen = viewModel.HoTen,
                NgaySinh = viewModel.NgaySinh,
                SoDienThoai = viewModel.SoDienThoai,
                DiaChi = viewModel.DiaChi,
                Email = viewModel.Email,
                HeSoLuong = viewModel.HeSoLuong
            };

            _context.NhanViens.Add(nhanVien);
            await _context.SaveChangesAsync();

            var phanCong = new PhanCong
            {
                NhanVienId = nhanVien.NhanVienId,
                DuAnId = viewModel.DuAnId,
                SoGioLam = viewModel.SoGioLam
            };

            _context.PhanCongs.Add(phanCong);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNhanViens), new { id = nhanVien.NhanVienId }, nhanVien);
        }

        // DELETE: api/NhanViens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNhanVien(int id)
        {
            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            _context.NhanViens.Remove(nhanVien);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("luong")]
        public async Task<IActionResult> TinhLuong()
        {
            var nhanViens = await _context.NhanViens.Include(nv => nv.PhanCongs).ToListAsync();

            if (nhanViens == null || !nhanViens.Any())
            {
                return NotFound("Không có nhân viên nào trong hệ thống");
            }

            var dsLuong = nhanViens.Select(nhanVien => new
            {
                NhanVienId = nhanVien.NhanVienId,
                HoTen = nhanVien.HoTen,
                TongLuong = nhanVien.PhanCongs.Sum(pc => pc.SoGioLam * 15 * nhanVien.HeSoLuong) 
            }).ToList();

            return Ok(dsLuong);
        }

        private bool NhanVienExists(int id)
        {
            return _context.NhanViens.Any(e => e.NhanVienId == id);
        }
    }
}
