using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace e4.Models.ViewModels
{
    public class HocSinhViewModel
    {
        public int HocSinhId { get; set; }
        public int LopId { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string QueQuan { get; set; }
    }
}
