namespace e2.Models
{
    [Table("CongThuc")]
    public class CongThuc
    {
        [Key]
        public int CongThucId { get; set; }

        [ForeignKey("MonAnId")]
        public int MonAnId { get; set; }

        [ForeignKey("NguyenLieuId")]
        public int NguyenLieuId { get; set; }

        [Required]
        public int SoLuong { get; set; }

        [Required]
        public string DonViTinh { get; set; }

        [JsonIgnore]
        public MonAn MonAn { get; set; }

        [JsonIgnore]
        public NguyenLieu NguyenLieu { get; set; }
    }
}
