using Infrastructure.Entity.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entity.RoleClaim;

public class RoleClaim:IdentityRoleClaim<long>,IEntity
{
    
}