using Infrastructure.Entity.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entity.UserLogin;

public class UserLogin:IdentityUserLogin<long>,IEntity
{
    
}