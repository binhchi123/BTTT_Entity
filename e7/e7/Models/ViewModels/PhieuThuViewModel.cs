namespace e7.Models.ViewModels
{
    public class PhieuThuViewModel
    {
        public int PhieuThuId { get; set; }  
        public DateTime NgayLap { get; set; } = DateTime.Now;  
        public string NhanVienLap { get; set; }  
        public string GhiChu { get; set; }      
        public double ThanhTien { get; set; }   

        public List<ChiTietPhieuThuViewModel> ChiTietPhieuThus { get; set; } 
    }
}
