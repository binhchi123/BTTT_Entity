namespace e2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonAnsController : ControllerBase
    {
        private readonly CookDbContext _context;

        public MonAnsController(CookDbContext context)
        {
            _context = context;
        }

        // GET: api/MonAns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonAn>>> GetMonAns()
        {
            return await _context.MonAns.ToListAsync();
        }

        //POST: api/MonAns
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateMonAn([FromBody] MonAnViewModel monAnView)
        {
            if (monAnView == null || monAnView.CongThucViewModels == null || !monAnView.CongThucViewModels.Any())
            {
                return BadRequest("Thông tin món ăn hoặc công thức không hợp lệ.");
            }

            if (!_context.LoaiMonAns.Any(lm => lm.LoaiMonAnId == monAnView.LoaiMonAnId))
            {
                return BadRequest($"ID loại món ăn không hợp lệ hoặc không tồn tại.");
            }

            foreach (var ct in monAnView.CongThucViewModels)
            {
                if (!_context.NguyenLieus.Any(nl => nl.NguyenLieuId == ct.NguyenLieuId))
                {
                    return BadRequest($"Nguyên liệu với ID {ct.NguyenLieuId} không tồn tại.");
                }

                if (ct.SoLuong <= 0)
                {
                    ct.SoLuong = 1;
                }
            }

            var monAn = new MonAn
            {
                TenMon      = monAnView.TenMon,
                GhiChu      = monAnView.GhiChu,
                LoaiMonAnId = monAnView.LoaiMonAnId,
                CongThucs   = monAnView.CongThucViewModels.Select(ct => new CongThuc
                {
                    NguyenLieuId = ct.NguyenLieuId,
                    SoLuong      = ct.SoLuong,
                    DonViTinh    = ct.DonViTinh
                }).ToList()
            };

            _context.MonAns.Add(monAn);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Đã xảy ra lỗi khi lưu dữ liệu: {ex.Message}");
            }

            return Ok(monAn);
        }

        // DELETE: api/An/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonAn(int id)
        {
            var monAn = await _context.MonAns.FindAsync(id);
            if (monAn == null)
            {
                return NotFound();
            }

            _context.MonAns.Remove(monAn);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MonAnExists(int id)
        {
            return _context.MonAns.Any(e => e.MonAnId == id);
        }
    }
}
