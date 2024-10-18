using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace e2_client.Models
{
    public class MonAn
    {
        public int MonAnId { get; set; }
        public string TenMon { get; set; }
        public string GhiChu { get; set; }
        public int LoaiMonAnId { get; set; }
    }
}
