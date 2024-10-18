using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace e6.Models.ViewModels
{
    public class HocVienViewModel
    {
        public int HocVienId { get; set; }
        public int KhoaHocId { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string QueQuan { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
    }
}
