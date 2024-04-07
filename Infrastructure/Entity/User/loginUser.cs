using Infrastructure.Entity.BaseEntity;
using Infrastructure.Entity.Sell;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Entity.User;

public class loginUser:IdentityUser<long>,IEntity {
    
    public string fullname { get; set; }
    
    public List<productSelles> ProductSellesList { get; set; }

    public class PatientConfiguration:IEntityTypeConfiguration<loginUser>
    {
        public void Configure(EntityTypeBuilder<loginUser> builder)
        {
            builder.HasMany(p => p.ProductSellesList)
                .WithOne(p => p.users)
                .HasForeignKey(p => p.userId);
        }
    }
}