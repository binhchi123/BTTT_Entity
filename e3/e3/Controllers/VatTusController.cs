using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using e3.Data;
using e3.Models;
using e3.Models.ViewModels;

namespace e3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatTusController : ControllerBase
    {
        private readonly VatTuDbContext _context;

        public VatTusController(VatTuDbContext context)
        {
            _context = context;
        }

        // GET: api/VatTus
        [HttpGet]
        public IActionResult GetVatTus()
        {
            var vatTu = _context.VatTus.Select(v => new
            {
                VatTuId = v.VatTuId,
                TenVatTu = v.TenVatTu,
                SoLuongTon = v.SoLuongTon,
                HetHang = v.SoLuongTon == 0
            }).ToList();
            return Ok(vatTu);
        }

        [HttpGet("hethang")]
        public IActionResult GetVatTuHetHang()
        {
            var dsHetHang = _context.VatTus.Where(v => v.SoLuongTon == 0).ToList();

            if (!dsHetHang.Any())
            {
                return Ok("Tất cả sản phẩm đều còn hàng.");
            }

            return Ok(dsHetHang.Select(v => new
            {
                VatTuId = v.VatTuId,
                TenVatTu = v.TenVatTu
            }));
        }

        [HttpPost("themPhieuNhap")]
        public IActionResult ThemPhieuNhap([FromBody] PhieuNhapViewModel phieuNhapView)
        {
            if (string.IsNullOrWhiteSpace(phieuNhapView.MaPhieu))
            {
                return BadRequest("Mã phiếu không được để trống");
            }

            if (_context.PhieuNhaps.Any(n => n.MaPhieu == phieuNhapView.MaPhieu))
            {
                return BadRequest("Mã phiếu đã tồn tại");
            }

            if (string.IsNullOrWhiteSpace(phieuNhapView.TieuDe))
            {
                return BadRequest("Tiêu đề không được để trống");
            }

            if (_context.PhieuNhaps.Any(n => n.TieuDe == phieuNhapView.TieuDe))
            {
                return BadRequest("Tiêu đề đã tồn tại");
            }

            var phieuNhap = new PhieuNhap
            {
                MaPhieu = phieuNhapView.MaPhieu,
                TieuDe = phieuNhapView.TieuDe,
                NgayNhap = DateTime.Now
            };

            _context.PhieuNhaps.Add(phieuNhap);
            _context.SaveChanges();

            foreach (var chiTiet in phieuNhapView.ChiTietPhieuNhaps)
            {
                var vatTu = _context.VatTus.Find(chiTiet.VatTuId);
                if(vatTu == null)
                {
                    return BadRequest("Vật tư không tồn tại");
                }
                if (chiTiet.SoLuongNhap <=0)
                {
                    chiTiet.SoLuongNhap = 1;
                }
                var chiTietPhieuNhap = new ChiTietPhieuNhap
                {
                    VatTuId = chiTiet.VatTuId,
                    PhieuNhapId = phieuNhap.PhieuNhapId,
                    SoLuongNhap = chiTiet.SoLuongNhap
                };

                // Cập nhật số lượng tồn của sản phẩm
                
                if (vatTu != null)
                {
                    vatTu.SoLuongTon += chiTiet.SoLuongNhap;
                }

                _context.ChiTietPhieuNhaps.Add(chiTietPhieuNhap);
            }

            _context.SaveChanges();

            return Ok("Thêm phiếu nhập thành công.");
        }

        [HttpPost("themPhieuXuat")]
        public IActionResult ThemPhieuXuat([FromBody] PhieuXuatViewModel phieuXuatView)
        {
            if (string.IsNullOrWhiteSpace(phieuXuatView.MaPhieu))
            {
                return BadRequest("Mã phiếu không được để trống");
            }

            if (_context.PhieuXuats.Any(x => x.MaPhieu == phieuXuatView.MaPhieu))
            {
                return BadRequest("Mã phiếu đã tồn tại");
            }

            if (string.IsNullOrWhiteSpace(phieuXuatView.TieuDe))
            {
                return BadRequest("Tiêu đề không được để trống");
            }

            if (_context.PhieuXuats.Any(x => x.TieuDe == phieuXuatView.TieuDe))
            {
                return BadRequest("Tiêu đề đã tồn tại");
            }

            var phieuXuat = new PhieuXuat
            {
                MaPhieu = phieuXuatView.MaPhieu,
                TieuDe = phieuXuatView.TieuDe,
                NgayXuat = DateTime.Now
            };

            _context.PhieuXuats.Add(phieuXuat);
            _context.SaveChanges();

            foreach (var chiTiet in phieuXuatView.ChiTietPhieuXuats)
            {
                var vatTu = _context.VatTus.Find(chiTiet.VatTuId);
                if(vatTu == null)
                {
                    return BadRequest("Vật tư không tồn tại");
                }

                if(vatTu.SoLuongTon <= 0)
                {
                    return BadRequest("Vật tư đã hết hàng không thể xuất");
                }

                if (vatTu.SoLuongTon < chiTiet.SoLuongXuat)
                {
                    return BadRequest("Số lượng không đủ để xuất");
                }

                if (chiTiet.SoLuongXuat <= 0)
                {
                    chiTiet.SoLuongXuat = 1;
                }

                // Cập nhật số lượng tồn của sản phẩm
                vatTu.SoLuongTon -= chiTiet.SoLuongXuat;
                var chiTietPhieuXuat = new ChiTietPhieuXuat
                {
                    VatTuId = chiTiet.VatTuId,
                    PhieuXuatId = phieuXuat.PhieuXuatId,
                    SoLuongXuat = chiTiet.SoLuongXuat
                };

                _context.ChiTietPhieuXuats.Add(chiTietPhieuXuat);
            }

            _context.SaveChanges();

            return Ok("Thêm phiếu xuất thành công.");
        }

        private bool VatTuExists(int id)
        {
            return _context.VatTus.Any(e => e.VatTuId == id);
        }
    }
}
