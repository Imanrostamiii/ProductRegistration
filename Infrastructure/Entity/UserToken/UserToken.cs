using Infrastructure.Entity.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entity.UserToken;

public class UserToken:IdentityUserToken<long>,IEntity
{
    
}