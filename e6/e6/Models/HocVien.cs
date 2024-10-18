using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e6.Models
{
    [Table("HocVien")]
    public class HocVien
    {
        [Key]
        public int HocVienId { get; set; }

        [ForeignKey("KhoaHocId")]
        public int KhoaHocId { get; set; }

        [Required, StringLength(20), MinLength(2)]
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string QueQuan { get; set; }
        public string DiaChi { get; set; }

        [StringLength(10)]
        public string SoDienThoai { get; set; }

        [JsonIgnore]
        public KhoaHoc KhoaHoc { get; set; }
    }
}
