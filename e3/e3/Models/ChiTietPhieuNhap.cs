using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e3.Models
{
    [Table("ChiTietPhieuNhap")]
    public class ChiTietPhieuNhap
    {
        [Key]
        public int ChiTietPhieuNhapId { get; set; }

        [ForeignKey("VatTuId")]
        public int VatTuId { get; set; }

        [ForeignKey("PhieuNhapId")]
        public int PhieuNhapId { get; set; }
        public int SoLuongNhap {  get; set; }

        [JsonIgnore]
        public VatTu VatTu {  get; set; }

        [JsonIgnore]
        public PhieuNhap PhieuNhap { get; set; }
    }
}
