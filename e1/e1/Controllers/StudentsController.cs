using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using e1.Data;
using e1.Models;
using e1.Models.ViewModels;

namespace e1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentDbContext _context;

        public StudentsController(StudentDbContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        [HttpGet("latest")]
        public ActionResult<IEnumerable<Student>> GetLatestStudent()
        {
            var latest = _context.Students.OrderByDescending(s => s.CreatedDate).ToList();
            return Ok(latest);
        }

        [HttpPost("yearname")]
        public ActionResult<IEnumerable<Student>> SearchStudentByYearName(StudentSearch studentSearch)
        {
            if (studentSearch == null)
            {
                return BadRequest("Không để trống");
            }    
            var yearname = _context.Students.Where(s => s.Birthday.Year == studentSearch.Year && s.Fullname.Contains(studentSearch.Fullname)).ToList();
            if (!yearname.Any())
            {
                return NotFound("Không tìm thấy sinh viên nào phù hợp");
            }
            return Ok(yearname);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
