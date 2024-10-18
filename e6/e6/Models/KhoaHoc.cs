using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e6.Models
{
    [Table("KhoaHoc")]
    public class KhoaHoc
    {
        [Key]
        public int KhoaHocId { get; set; }

        [Required, StringLength(10)]
        public string TenKhoaHoc { get; set; }

        [Required]
        public string Mota {  get; set; }

        [Required, MaxLength(10000000)]
        public int HocPhi { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }

        [JsonIgnore]
        public ICollection<HocVien> HocViens { get; set; }

        [JsonIgnore]
        public ICollection<NgayHoc> NgayHoc { get; set;}
    }
}
