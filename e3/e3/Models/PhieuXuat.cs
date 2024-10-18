using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e3.Models
{
    [Table("PhieuXuat")]
    public class PhieuXuat
    {
        [Key]
        public int PhieuXuatId { get; set; }

        [Required]
        public string MaPhieu { get; set; }

        [Required]
        public string TieuDe {  get; set; }
        public DateTime NgayXuat { get; set; } = DateTime.Now;

        [JsonIgnore]
        public ICollection<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; }
    }
}
