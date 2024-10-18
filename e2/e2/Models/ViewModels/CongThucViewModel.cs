using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace e2.Models.ViewModels
{
    public class CongThucViewModel
    {
        public int NguyenLieuId { get; set; }
        public int SoLuong { get; set; }
        public string DonViTinh { get; set; }
    }
}
