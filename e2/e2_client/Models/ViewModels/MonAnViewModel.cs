namespace e2_client.Models.ViewModels
{
    public class MonAnViewModel
    {
        public string TenMon { get; set; }
        public string GhiChu { get; set; }
        public int LoaiMonAnId { get; set; }
        public string TenLoai { get; set; }
        public List<CongThuc> CongThucViewModels { get; set; }
    }
}
