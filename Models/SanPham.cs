using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingWeb.Models
{
    [Table("sanpham")]
    public class SanPham
    {
        [Key]
        [Column("ma")]
        [StringLength(20)]
        [Display(Name = "Mã")]
        [Required(ErrorMessage = "Vui lòng nhập {0}.")]
        public string Ma { get; set; } = null!;

        [Column("ten")]
        [StringLength(100)]
        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Vui lòng nhập {0}.")]
        public string Ten { get; set; } = null!;

        [Column("gia")]
        [Display(Name = "Giá")]
        [Range(0, 999999999999, ErrorMessage = "{0} phải là số không âm.")]
        public decimal? Gia { get; set; }

        [Column("danhmuc_ma")]
        [StringLength(20)]
        [Display(Name = "Danh mục")]
        public string? DanhMucMa { get; set; }

        [ForeignKey(nameof(DanhMucMa))]
        public virtual DanhMuc? DanhMuc { get; set; }
    }
}