using System.Security.Claims;
using Azure.Core;
using Infrastructure.Entity.User;
using Microsoft.AspNetCore.Identity;

namespace Service_Layer.Service.Jwt;

public interface IJWTRepository
{
    Task<IdentityResult> SignUpAsync(loginUser user);
    Task<string> LoginAsync(loginUser user);

}