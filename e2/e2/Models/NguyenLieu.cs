using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e2.Models
{
    [Table("NguyenLieu")]
    public class NguyenLieu
    {
        [Key]
        public int NguyenLieuId { get; set; }

        [Required]
        public string TenNguyenLieu { get; set; }

        [JsonIgnore]
        public ICollection<CongThuc> CongThucs { get; set; }
    }
}
