using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e4.Models
{
    [Table("HocSinh")]
    public class HocSinh
    {
        [Key]
        public int HocSinhId { get; set; }

        [ForeignKey("LopId")]
        [Required]
        public int LopId { get; set; }
        
        [Required, StringLength(20, MinimumLength = 2)]
        public string HoTen { get; set; }

        [Required, Range(typeof(DateTime), "2001-01-01", "2013-12-31")]
        public DateTime NgaySinh { get; set; }

        [Required]
        public string QueQuan { get; set; }

        [JsonIgnore]
        public Lop Lop { get; set; }
    }
}
