using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e7.Models
{
    [Table("ChiTietPhieuThu")]
    public class ChiTietPhieuThu
    {
        [Key]
        public int ChiTietPhieuThuId { get; set; }

        [ForeignKey("NguyenLieuId")]
        public int NguyenLieuId { get; set; }

        [ForeignKey("PhieuThuId")]
        public int PhieuThuId { get; set; }

        [Required]
        public int SoLuongBan {  get; set; }

    }
}
