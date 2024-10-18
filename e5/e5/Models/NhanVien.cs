using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e5.Models
{
    [Table("NhanVien")]
    public class NhanVien
    {
        [Key]
        public int NhanVienId { get; set; }

        [Required, StringLength(20, MinimumLength = 2)]
        public string HoTen { get; set; }

        [Range(typeof(DateTime), "1970-01-01", "2000-12-31")]
        public DateTime NgaySinh { get; set; }

        [Required]
        public string SoDienThoai { get; set; }

        [Required]
        public string DiaChi { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int HeSoLuong { get; set; }

        [JsonIgnore]
        public ICollection<PhanCong> PhanCongs { get; set; }

    }
}
