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
        [Display(Name = "Mã")]                                          // thêm
        [Required(ErrorMessage = "Vui lòng nhập {0}.")]                 // thêm
        public string Ma { get; set; } = null!;

        [Column("ten")]
        [StringLength(100)]
        [Display(Name = "Tên")]                                         // thêm
        [Required(ErrorMessage = "Vui lòng nhập {0}.")]                 // thêm
        public string Ten { get; set; } = null!;

        [Column("gia")]
        [Display(Name = "Giá")]                                         // thêm
        [Range(0, 999999999999, ErrorMessage = "{0} phải là số không âm.")] // thêm
        public decimal? Gia { get;