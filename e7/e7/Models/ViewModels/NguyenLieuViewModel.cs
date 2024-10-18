using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace e7.Models.ViewModels
{
    public class NguyenLieuViewModel
    {
        public int NguyenLieuId { get; set; }
        public int LoaiNguyenLieuId { get; set; }
        public string TenNguyenLieu { get; set; }
        public double GiaBan { get; set; }
        public string DonViTinh { get; set; }
        public int SoLuongKho { get; set; }
    }
}
