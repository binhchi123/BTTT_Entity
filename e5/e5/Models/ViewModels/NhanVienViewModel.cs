using System.ComponentModel.DataAnnotations;

namespace e5.Models.ViewModels
{
    public class NhanVienViewModel
    {
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public int HeSoLuong { get; set; }
        public int DuAnId { get; set; }
        public int SoGioLam { get; set; }
    }
}
