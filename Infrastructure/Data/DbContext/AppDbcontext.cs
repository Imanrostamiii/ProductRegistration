using Infrastructure.Entity.Role;
using Infrastructure.Entity.RoleClaim;
using Infrastructure.Entity.User;
using Infrastructure.Entity.UserClaim;
using Infrastructure.Entity.UserLogin;
using Infrastructure.Entity.UserRole;
using Infrastructure.Entity.UserToken;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Data.DbContext;

public class AppDbcontext:IdentityDbContext<loginUser,Role,long,UserClaim,UserRole,UserLogin,RoleClaim,UserToken>
{
    
}