using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e3.Models
{
    [Table("ChiTietPhieuXuat")]
    public class ChiTietPhieuXuat
    {
        [Key]
        public int ChiTietPhieuXuatId { get; set; }

        [ForeignKey("VatTuId")]
        public int VatTuId { get; set; }

        [ForeignKey("PhieuXuatId")]
        public int PhieuXuatId { get; set; }
        public int SoLuongXuat {  get; set; }

        [JsonIgnore]
        public VatTu VatTu { get; set; }

        [JsonIgnore]
        public PhieuXuat PhieuXuat { get;set; }
    }
}
