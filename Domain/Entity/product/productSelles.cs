using System.ComponentModel.DataAnnotations;
using Infrastructure.Entity.BaseEntity;
using Infrastructure.Entity.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Entity.Sell;
[Index(nameof(Emile), IsUnique = true)]
[Index(nameof(ProductDate), IsUnique = true)]
public class productSelles:BaseEntity<int>
{
    [Required(ErrorMessage = "نام محصول وارد کنید.")]
    [StringLength(50, ErrorMessage = "نام محصول نمی‌تواند بیشتر از 50 کاراکتر باشد.")]
    public string NameProduct { get; set; }

    [Required(ErrorMessage = "ایمیل وارد کنید.")]
    [EmailAddress(ErrorMessage = "لطفاً یک ایمیل معتبر وارد کنید.")]
    public string Emile { get; set; }
    [Required(ErrorMessage = "شماره تلفن وارد کنید.")]
   
    public string Phone { get; set; }
    
    public bool IsAvailable  { get; set; }
    [Required(ErrorMessage = "تاریخ محصول وارد کنید")]
    [DataType(DataType.Date, ErrorMessage = "لطفاً یک تاریخ معتبر وارد کنید.")]
    public DateTime ProductDate { get; set; }
    public loginUser? users { get; set; }
    
    public long? userId { get; set; }

    
}