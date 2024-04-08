using Application.BaseDto;
using Infrastructure.Entity.Sell;

namespace ProductRegistration.Modele.Dto;

public class ProductSelectDto:BaseDto<ProductSelectDto, productSelles,int>
{
    public string NameProduct { get; set; }

    public string Emile { get; set; }
    
    public string Phone { get; set; }
    
    public bool IsAvailable  { get; set; }
    
    public DateTime ProductDate { get; set; }


}