using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using e6.Data;
using e6.Models;
using e6.Models.ViewModels;

namespace e6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhoaHocsController : ControllerBase
    {
        private readonly KhoaHocDbContext _context;

        public KhoaHocsController(KhoaHocDbContext context)
        {
            _context = context;
        }

        // GET: api/KhoaHocs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhoaHoc>>> GetKhoaHocs()
        {
            return await _context.KhoaHocs.ToListAsync();
        }

        [HttpPost("AddNgayHoc")]
        public async Task<IActionResult> AddNgayHoc(int khoaHocId, [FromBody] NgayHocViewModel ngayHocView)
        {
            if (ngayHocView == null)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            var khoaHoc = await _context.KhoaHocs.FindAsync(khoaHocId);
            if (khoaHoc == null)
            {
                return NotFound("Khóa học không tồn tại");
            }

            var soNgayHoc = await _context.NgayHocs.CountAsync(n => n.KhoaHocId == khoaHocId);
            if (soNgayHoc >= 15)
            {
                return BadRequest("Mỗi khóa học chỉ có tối đa 15 ngày học");
            }

            var ngayHoc = new NgayHoc
            {
                KhoaHocId = khoaHocId,
                NoiDung = ngayHocView.NoiDung,
                GhiChu = ngayHocView.GhiChu
            };
            _context.NgayHocs.Add(ngayHoc);
            await _context.SaveChangesAsync();

            return Ok(ngayHoc);
        }



        [HttpGet("DoanhThuThang")]
        public async Task<IActionResult> DoanhThuThang(int thang, int nam)
        {
            var khoaHocs = await _context.KhoaHocs
                .Where(kh => kh.NgayBatDau.Month == thang && kh.NgayBatDau.Year == nam)
                .ToListAsync();

            var doanhThu = 0;

            foreach (var khoaHoc in khoaHocs)
            {
                var soHocVien = await _context.HocViens.CountAsync(hv => hv.KhoaHocId == khoaHoc.KhoaHocId);
                doanhThu += soHocVien * khoaHoc.HocPhi;
            }

            return Ok(new { Thang = thang, Nam = nam, DoanhThu = doanhThu });
        }


        // DELETE: api/KhoaHocs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhoaHoc(int id)
        {
            var khoaHoc = await _context.KhoaHocs.FindAsync(id);
            if (khoaHoc == null)
            {
                return NotFound();
            }
            var hocViens = _context.HocViens.Where(hv => hv.KhoaHocId == id);
            var ngayHocs = _context.NgayHocs.Where(nh => nh.KhoaHocId == id);

            _context.HocViens.RemoveRange(hocViens);
            _context.NgayHocs.RemoveRange(ngayHocs);
            _context.KhoaHocs.Remove(khoaHoc);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KhoaHocExists(int id)
        {
            return _context.KhoaHocs.Any(e => e.KhoaHocId == id);
        }
    }
}
