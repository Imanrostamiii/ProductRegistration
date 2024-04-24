using CoreLayer.Extensions;
using Infrastructure.Entity.BaseEntity;
using Infrastructure.Entity.Role;
using Infrastructure.Entity.RoleClaim;
using Infrastructure.Entity.User;
using Infrastructure.Entity.UserClaim;
using Infrastructure.Entity.UserLogin;
using Infrastructure.Entity.UserRole;
using Infrastructure.Entity.UserToken;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.DbContext;

public class AppDbcontext:IdentityDbContext<loginUser,Role,long,UserClaim,UserRole,UserLogin,RoleClaim,UserToken>
{
    public AppDbcontext(DbContextOptions options):base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        var assembly = typeof(IEntity).Assembly;
        builder.allEntity<IEntity>();
        builder.ApplyConfigurationsFromAssembly(assembly);
    }
}