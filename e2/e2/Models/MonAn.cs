using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e2.Models
{
    [Table("MonAn")]
    public class MonAn
    {
        [Key]
        public int MonAnId { get; set; }

        [Required]
        public string TenMon { get; set; }
        public string GhiChu { get; set; }

        [ForeignKey("LoaiMonAnId")]
        public int LoaiMonAnId { get; set; }

        [JsonIgnore]
        public LoaiMonAn LoaiMonAn { get; set; }    

        [JsonIgnore]
        public ICollection<CongThuc> CongThucs { get; set; }
    }
}
