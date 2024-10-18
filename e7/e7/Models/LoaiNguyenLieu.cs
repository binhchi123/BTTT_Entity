using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e7.Models
{
    [Table("LoaiNguyenLieu")]
    public class LoaiNguyenLieu
    {
        [Key]
        public int LoaiNguyenLieuId { get; set; }

        [Required, StringLength(20), MaxLength(20)]
        public string TenLoai {  get; set; }
        public string MoTa { get; set; }

        [JsonIgnore]
        public ICollection<NguyenLieu> NguyenLieus { get; set; }
    }
}
