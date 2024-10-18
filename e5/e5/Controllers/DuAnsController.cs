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
    public class DuAnsController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public DuAnsController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: api/DuAns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DuAn>>> GetDuAns()
        {
            return await _context.DuAns.ToListAsync();
        }

        // PUT: api/DuAns/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDuAn(int id, [FromBody] DuAnViewModel duAnView)
        {
            if (id != duAnView.DuAnId)
            {
                return BadRequest("ID không hợp lệ.");
            }

            var existingDuAn = await _context.DuAns.FindAsync(id);
            if (existingDuAn == null)
            {
                return NotFound();
            }

            existingDuAn.TenDuAn = duAnView.TenDuAn;
            existingDuAn.MoTa = duAnView.MoTa;
            existingDuAn.GhiChu = duAnView.GhiChu;

            _context.Entry(existingDuAn).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DuAnExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        private bool DuAnExists(int id)
        {
            return _context.DuAns.Any(e => e.DuAnId == id);
        }
    }
}
