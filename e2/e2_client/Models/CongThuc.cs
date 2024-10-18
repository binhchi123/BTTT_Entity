using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace e2_client.Models
{
    public class CongThuc
    {
        public int CongThucId { get; set; }
        public int MonAnId { get; set; }
        public int NguyenLieuId { get; set; }
        public int SoLuong { get; set; }
        public string DonViTinh { get; set; }
    }
}
