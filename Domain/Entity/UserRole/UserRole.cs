using Infrastructure.Entity.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entity.UserRole;

public class UserRole:IdentityUserRole<long>,IEntity
{
    
}