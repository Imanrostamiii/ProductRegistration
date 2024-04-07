using Infrastructure.Entity.BaseEntity;

namespace Infrastructure.Entity.product;

public class productSelles:BaseEntity<int>
{
    public string NameProduct { get; set; }

    public string Emile { get; set; }
    
    public string Phone { get; set; }
    
    public bool IsAvailable  { get; set; }
    
    public DateTime ProductDate { get; set; }
}