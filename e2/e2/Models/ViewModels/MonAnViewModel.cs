namespace e2.Models.ViewModels
{
    public class MonAnViewModel
    {
        public string TenMon { get; set; }
        public string GhiChu { get; set; }
        public int LoaiMonAnId { get; set; }
        public List<CongThucViewModel> CongThucViewModels { get; set; }
    }
}
