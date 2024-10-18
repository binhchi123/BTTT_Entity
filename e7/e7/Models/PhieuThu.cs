using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e7.Models
{
    [Table("PhieuThu")]
    public class PhieuThu
    {
        [Key]
        public int PhieuThuId { get; set; }
        public DateTime NgayLap { get; set; } = DateTime.Now;

        [Required]
        public string NhanVienLap { get; set; }

        public string GhiChu { get; set; }
        public double ThanhTien { get; set; }

        [JsonIgnore]
        public ICollection<ChiTietPhieuThu> ChiTietPhieuThus { get; set; }
    }
}
