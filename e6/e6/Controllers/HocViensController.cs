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
    public class HocViensController : ControllerBase
    {
        private readonly KhoaHocDbContext _context;

        public HocViensController(KhoaHocDbContext context)
        {
            _context = context;
        }

        // GET: api/HocViens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HocVien>>> GetHocViens()
        {
            return await _context.HocViens.ToListAsync();
        }

        // PUT: api/HocViens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHocVien(int id, HocVienViewModel hocVienView)
        {
            var hocVien = await _context.HocViens.FindAsync(id);
            if (hocVien == null)
            {
                return NotFound("Học viên không tồn tại");
            }

            hocVien.HoTen = hocVienView.HoTen;
            hocVien.NgaySinh = hocVienView.NgaySinh;
            hocVien.QueQuan = hocVienView.QueQuan;
            hocVien.DiaChi = hocVienView.DiaChi;
            hocVien.SoDienThoai = hocVienView.SoDienThoai;

            await _context.SaveChangesAsync();
            return Ok(hocVien);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchHocViens(string hoTen, string tenKhoaHoc)
        {
            var hocViens = await (from hv in _context.HocViens
                                  join kh in _context.KhoaHocs on hv.KhoaHocId equals kh.KhoaHocId
                                  where hv.HoTen.Contains(hoTen) && kh.TenKhoaHoc.Contains(tenKhoaHoc)
                                  select new
                                  {
                                      hv.HoTen,
                                      hv.NgaySinh,
                                      hv.QueQuan,
                                      hv.DiaChi,
                                      hv.SoDienThoai,
                                      TenKhoaHoc = kh.TenKhoaHoc
                                  }).ToListAsync();

            if (hocViens.Count == 0)
            {
                return NotFound("Không tìm thấy học viên nào");
            }

            return Ok(hocViens);
        }

        private bool HocVienExists(int id)
        {
            return _context.HocViens.Any(e => e.HocVienId == id);
        }
    }
}
