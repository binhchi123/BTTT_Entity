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
    public class NguyenLieusController : ControllerBase
    {
        private readonly PhieuThuDbContext _context;

        public NguyenLieusController(PhieuThuDbContext context)
        {
            _context = context;
        }

        // GET: api/NguyenLieux
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NguyenLieu>>> GetNguyenLieus()
        {
            return await _context.NguyenLieus.ToListAsync();
        }

        // POST: api/NguyenLieux
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NguyenLieu>> PostNguyenLieu(NguyenLieuViewModel nguyenLieuView)
        {
            if (nguyenLieuView == null)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            var loaiNguyenLieu = await _context.LoaiNguyenLieus.FindAsync(nguyenLieuView.LoaiNguyenLieuId);
            if (loaiNguyenLieu == null)
            {
                return NotFound("Loại nguyên liệu không tồn tại.");
            }

            if(string.IsNullOrWhiteSpace(nguyenLieuView.TenNguyenLieu))
            {
                return BadRequest("Nhập tên nguyên liệu");
            }

            if (string.IsNullOrWhiteSpace(nguyenLieuView.DonViTinh))
            {
                return BadRequest("Nhập đơn vị tính");
            }

            if (nguyenLieuView.GiaBan <= 0)
            {
                return BadRequest("Giá bán phải lớn hơn 0");
            }

            if (nguyenLieuView.SoLuongKho <= 0)
            {
                return BadRequest("Số lượng kho phải lớn hơn 0");
            }

            var nguyenLieu = new NguyenLieu
            {
                LoaiNguyenLieuId = nguyenLieuView.LoaiNguyenLieuId,
                TenNguyenLieu = nguyenLieuView.TenNguyenLieu,
                GiaBan = nguyenLieuView.GiaBan,
                DonViTinh = nguyenLieuView.DonViTinh,
                SoLuongKho = nguyenLieuView.SoLuongKho
            };

            _context.NguyenLieus.Add(nguyenLieu);
            await _context.SaveChangesAsync();

            return Ok(nguyenLieu);
        }

        private bool NguyenLieuExists(int id)
        {
            return _context.NguyenLieus.Any(e => e.NguyenLieuId == id);
        }
    }
}
