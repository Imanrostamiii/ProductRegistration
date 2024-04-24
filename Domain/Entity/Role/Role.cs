using Infrastructure.Entity.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entity.Role;

public class Role:IdentityRole<long>,IEntity
{
    
}