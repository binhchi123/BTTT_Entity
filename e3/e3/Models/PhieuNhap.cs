using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e3.Models
{
    [Table("PhieuNhap")]
    public class PhieuNhap
    {
        [Key]
        public int PhieuNhapId { get; set; }

        [Required]
        public string MaPhieu { get; set; }

        [Required]
        public string TieuDe { get; set; }
        public DateTime NgayNhap { get; set; } = DateTime.Now;

        [JsonIgnore]
        public ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
    }
}
