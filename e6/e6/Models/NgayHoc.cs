using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e6.Models
{
    [Table("NgayHoc")]
    public class NgayHoc
    {
        [Key]
        public int NgayHocId { get; set; }

        [ForeignKey("KhoaHocId")]
        public int KhoaHocId { get; set; }

        [Required]
        public string NoiDung {  get; set; }    
        public string GhiChu { get; set; }
        [JsonIgnore]
        public KhoaHoc KhoaHoc { get; set; }
    }
}
