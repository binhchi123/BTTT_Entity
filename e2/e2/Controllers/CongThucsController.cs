namespace e2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CongThucsController : ControllerBase
    {
        private readonly CookDbContext _context;

        public CongThucsController(CookDbContext context)
        {
            _context = context;
        }

        // GET: api/CongThucs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CongThuc>>> GetCongThucs()
        {
            //return await _context.CongThucs.ToListAsync();

            var recipes = await _context.CongThucs
                .Include(ct => ct.MonAn)
                .Include(ct => ct.NguyenLieu)
                .ToListAsync();
            var result = recipes.GroupBy(ct => ct.MonAn.TenMon)
                .Select(g =>
                {
                    var ingredients = g.Select(ct => $"{ct.NguyenLieu.TenNguyenLieu} - {ct.SoLuong} - {ct.DonViTinh}");
                    return $"Món ăn {g.Key} {string.Join(" ", ingredients)}";
                });

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<object>>> SearchByIngredient(string ingredient)
        {
            var keywords = ingredient.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var recipes = await _context.CongThucs
                .Include(ct => ct.MonAn)
                .Include(ct => ct.NguyenLieu)
                .Where(ct => keywords.Any(keyword => ct.NguyenLieu.TenNguyenLieu.Contains(keyword)))
                .ToListAsync();

            if (!recipes.Any())
            {
                return NotFound("Không tìm thấy món ăn nào.");
            }

            var result = recipes.GroupBy(ct => ct.MonAn.TenMon)
                .Select(g => new
                {
                    MonAn = g.Key,
                    CongThuc = g.Select(ct => new
                    {
                        TenNguyenLieu = ct.NguyenLieu.TenNguyenLieu,
                        SoLuong = ct.SoLuong,
                        DonViTinh = ct.DonViTinh
                    })
                });

            return Ok(result);
        }

        private bool CongThucExists(int id)
        {
            return _context.CongThucs.Any(e => e.CongThucId == id);
        }
    }
}
