using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e3.Models
{
    [Table("VatTu")]
    public class VatTu
    {
        [Key]
        public int VatTuId { get; set; }

        [Required]
        public string TenVatTu { get; set; }
        public int SoLuongTon { get; set; }

        [JsonIgnore]
        public ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }

        [JsonIgnore]
        public ICollection<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; }
    }
}
