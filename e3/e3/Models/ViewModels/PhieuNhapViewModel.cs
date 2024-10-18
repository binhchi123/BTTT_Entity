using System.ComponentModel.DataAnnotations;

namespace e3.Models.ViewModels
{
    public class PhieuNhapViewModel
    {
        public string MaPhieu { get; set; }
        public string TieuDe { get; set; }
        public List<ChiTietPhieuNhapViewModel> ChiTietPhieuNhaps { get; set; }
    }
}
