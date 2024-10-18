using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e4.Models
{
    [Table("Lop")]
    public class Lop
    {
        [Key]
        public int LopId { get; set; }

        [Required, StringLength(10)]
        public string TenLop { get; set; }

        [Required, MaxLength(20)]
        public int SiSo { get; set; }

        [JsonIgnore]
        public ICollection<HocSinh> HocSinhs { get; set; }
    }
}
