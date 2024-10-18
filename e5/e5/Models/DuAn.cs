using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e5.Models
{
    [Table("DuAn")]
    public class DuAn
    {
        [Key]
        public int DuAnId { get; set; }

        [Required, StringLength(10)]
        public string TenDuAn { get; set; }
        public string MoTa {  get; set; }
        public string GhiChu { get; set; }

        [JsonIgnore]
        public ICollection<PhanCong> PhanCongs { get; set; }
    }
}
