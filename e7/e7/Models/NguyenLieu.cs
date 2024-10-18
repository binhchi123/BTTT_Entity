using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e7.Models
{
    [Table("NguyenLieu")]
    public class NguyenLieu
    {
        [Key]
        public int NguyenLieuId { get; set; }

        [ForeignKey("LoaiNguyenLieuId")]
        public int LoaiNguyenLieuId {  get; set; }

        [Required, StringLength(20)]
        public string TenNguyenLieu { get; set; }

        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Giá bán phải lớn hơn 0")]
        public double GiaBan {  get; set; }

        [Required, StringLength(10), MaxLength(10)]
        public string DonViTinh { get; set; }

        [Required,  Range(1, int.MaxValue, ErrorMessage = "Số lượng kho phải lớn hơn 0")]
        public int SoLuongKho {  get; set; }

        [JsonIgnore]
        public LoaiNguyenLieu LoaiNguyenLieu { get; set; }

        [JsonIgnore]
        public ICollection<ChiTietPhieuThu> ChiTietPhieuThus { get; set; }
    }
}
