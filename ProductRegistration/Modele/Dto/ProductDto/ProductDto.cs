using System.ComponentModel.DataAnnotations;
using Application.BaseDto;
using Infrastructure.Entity.Sell;

namespace ProductRegistration.Modele.Dto;

public class ProductDto:BaseDto<ProductDto, productSelles,int>
{
    [Required(ErrorMessage = "نام محصول را وارد کنبد")]
    [StringLength(60, ErrorMessage = "نام محصول باید کمتر از 50 کاراکتر باشد")]
    public string NameProduct { get; set; }

    public string Emile { get; set; }
    
    [Required(ErrorMessage = "شماره تماس را وارد کنید")]
    public string Phone { get; set; }
    
    [Required(ErrorMessage = "ایمیل را وارد کنید")]
    public bool IsAvailable  { get; set; }
    
    [Required(ErrorMessage = "تاریخ محصول وارد کنید")]
    public DateTime ProductDate { get; set; }



}