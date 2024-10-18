
namespace e2.Models
{
    [Table("LoaiMonAn")]
    public class LoaiMonAn
    {
        [Key]
        public int LoaiMonAnId { get; set; }

        [Required]
        public string TenLoai { get; set; }

        [JsonIgnore]
        public ICollection<MonAn> MonAns { get; set; }
    }
}
