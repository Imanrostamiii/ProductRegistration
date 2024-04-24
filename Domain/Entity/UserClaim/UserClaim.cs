using Infrastructure.Entity.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entity.UserClaim;

public class UserClaim:IdentityUserClaim<long>,IEntity
{
    
}