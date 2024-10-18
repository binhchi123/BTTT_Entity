using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e5.Models
{
    [Table("PhanCong")]
    public class PhanCong
    {
        [Key]
        public int PhanCongId { get; set; }

        [ForeignKey("NhanVienId")]
        public int NhanVienId { get; set; }

        [ForeignKey("DuAnId")]
        public int DuAnId { get; set; }
        public int SoGioLam { get; set; }

    }
}
