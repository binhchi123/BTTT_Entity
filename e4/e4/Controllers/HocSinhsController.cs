using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using e4.Data;
using e4.Models;
using e4.Models.ViewModels;
using System.Net.NetworkInformation;

namespace e4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocSinhsController : ControllerBase
    {
        private readonly StudentDbContext _context;
        private readonly List<string> _cities = new List<string>
        {
            "Hà Nội", "Hồ Chí Minh", "Đà Nẵng", "Cần Thơ", "Nha Trang", "Hải Phòng", "Đài Loan", 
        };

        private bool City(string queQuan)
        {
            return _cities.Any(city => queQuan.Contains(city, StringComparison.OrdinalIgnoreCase));
        }

        public HocSinhsController(StudentDbContext context)
        {
            _context = context;
        }

        // GET: api/HocSinhs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HocSinh>>> GetHocSinhs()
        {
            return await _context.HocSinhs.ToListAsync();
        }

        // GET: api/HocSinhs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HocSinh>> GetHocSinh(int id)
        {
            var hocSinh = await _context.HocSinhs.FindAsync(id);

            if (hocSinh == null)
            {
                return NotFound();
            }

            return hocSinh;
        }

        // PUT: api/HocSinhs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHocSinh(int id, HocSinhViewModel hocSinhView)
        {
            if (id != hocSinhView.HocSinhId)
            {
                return BadRequest("ID không hợp lệ.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hocSinh = await _context.HocSinhs.FindAsync(id);
            if (hocSinh == null)
            {
                return NotFound("Học sinh không tồn tại.");
            }

            var oldLop = await _context.Lops.Include(l => l.HocSinhs)
                                      .FirstOrDefaultAsync(l => l.LopId == hocSinh.LopId);
            var newLop = await _context.Lops.Include(l => l.HocSinhs)
                                              .FirstOrDefaultAsync(l => l.LopId == hocSinhView.LopId);

            if (newLop == null || newLop.HocSinhs.Count >= 20)
            {
                return BadRequest("Lớp học mới đã đủ sĩ số hoặc không tồn tại.");
            }

            if (hocSinhView.NgaySinh < new DateTime(2001, 1, 1) || hocSinhView.NgaySinh > new DateTime(2013, 12, 31))
            {
                return BadRequest("Ngày sinh phải từ năm 2001 đến 2013.");
            }

            if (!City(hocSinhView.QueQuan))
            {
                return BadRequest("Quê quán phải chứa tên ít nhất một thành phố.");
            }

            // Cập nhật thông tin
            if (oldLop != null) 
            {
                oldLop.SiSo--;
            }

            hocSinh.LopId = hocSinhView.LopId;
            hocSinh.HoTen = hocSinhView.HoTen;
            hocSinh.NgaySinh = hocSinhView.NgaySinh;
            hocSinh.QueQuan = hocSinhView.QueQuan;

            if (newLop != null)
            {
                newLop.SiSo++;
            }

            _context.Entry(hocSinh).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HocSinhExists(id))
                {
                    return NotFound("Học sinh không tồn tại.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/HocSinhs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HocSinh>> PostHocSinh(HocSinhViewModel hocSinhView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (hocSinhView.NgaySinh < new DateTime(2001, 1, 1) || hocSinhView.NgaySinh > new DateTime(2013, 12, 31))
            {
                return BadRequest("Ngày sinh phải từ năm 2001 đến 2013.");
            }

            if (!City(hocSinhView.QueQuan))
            {
                return BadRequest("Quê quán phải chứa tên ít nhất một thành phố.");
            }

            var lop = await _context.Lops.Include(l => l.HocSinhs)
                                           .FirstOrDefaultAsync(l => l.LopId == hocSinhView.LopId);

            if (lop == null || lop.HocSinhs.Count >= 20)
            {
                return BadRequest("Lớp học đã đủ sĩ số hoặc không tồn tại.");
            }

            var hocSinh = new HocSinh
            {
                LopId = hocSinhView.LopId,
                HoTen = hocSinhView.HoTen,
                NgaySinh = hocSinhView.NgaySinh,
                QueQuan = hocSinhView.QueQuan
            };

            _context.HocSinhs.Add(hocSinh);
            lop.SiSo++;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHocSinh), new { id = hocSinh.HocSinhId }, hocSinh);
        }


        // DELETE: api/HocSinhs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHocSinh(int id)
        {
            var hocSinh = await _context.HocSinhs.FindAsync(id);
            if (hocSinh == null)
            {
                return NotFound();
            }
            var lop = await _context.Lops.FindAsync(hocSinh.LopId);
            if(lop != null)
            {
                lop.SiSo--;
            }
            _context.HocSinhs.Remove(hocSinh);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("chuyenlop/{hocSinhId}/{lopMoiId}")]
        public async Task<IActionResult> ChuyenLop(int hocSinhId, int lopMoiId)
        {
            var hocSinh = await _context.HocSinhs.FindAsync(hocSinhId);
            var lopCu = await _context.Lops.Include(l => l.HocSinhs)
                                    .FirstOrDefaultAsync(l => l.LopId == hocSinh.LopId);
            var lopMoi = await _context.Lops.Include(l => l.HocSinhs)
                                            .FirstOrDefaultAsync(l => l.LopId == lopMoiId);

            if (hocSinh == null || lopMoi == null || lopMoi.HocSinhs.Count >= 20)
            {
                return BadRequest("Không thể chuyển lớp. Lớp mới đã đủ sĩ số hoặc không tồn tại.");
            }

            hocSinh.LopId = lopMoiId;
            if(lopMoi != null) 
            {
                lopMoi.SiSo++;
            }

            if (lopCu != null)
            {
                lopCu.SiSo--;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HocSinhExists(int id)
        {
            return _context.HocSinhs.Any(e => e.HocSinhId == id);
        }
    }
}
