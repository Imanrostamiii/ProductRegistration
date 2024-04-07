using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure.Core;
using Infrastructure.Entity.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Service_Layer.Service.Jwt;

public class JWTRepository : IJWTRepository
{
    private readonly SignInManager<loginUser> _signInManager;
    private readonly UserManager<loginUser> _userManager;
    private readonly IConfiguration _configuration;

    public JWTRepository(SignInManager<loginUser> signInManager, IConfiguration configuration,
        UserManager<loginUser> userManager)
    {  
        _configuration = configuration;
        _signInManager = signInManager;
        _userManager = userManager;
      
    }

    #region Singup

    public async Task<IdentityResult> SignUpAsync(loginUser user)
    {
        var visit = new loginUser()
        {
           fullname = user.fullname,
            UserName = user.UserName,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
           
        };
        return await _userManager.CreateAsync(visit, user.PasswordHash);
    }

  

    #endregion

    #region login

    /*
    public async Task<string> LoginAsync(LoginUser User)
    {
        var authclaim = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, User.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        //creat claim defult
        //  var auclaim = _signInManager.ClaimsFactory.CreateAsync(User);

        var secretKey = Encoding.UTF8.GetBytes(_configuration["jwt:SecretKey"]); // longer that 16 character
        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);
        var encryptionkey = Encoding.UTF8.GetBytes(_configuration["jwt:EncryptKey"]); //must be 16 character
        var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey),
            SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

        //var certificate = new X509Certificate2("d:\\aaaa2.cer"/*, "P@ssw0rd"#1#);
        //var encryptingCredentials = new X509EncryptingCredentials(certificate);

        /*var claims = await _getClaimsAsync(user);#1#
        var authsignInKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Key"]));
        /*var Token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            expires: DateTime.Now.AddDays(1),
            //claims: authclaim,
            signingCredentials: new SigningCredentials(authsignInKey, SecurityAlgorithms.HmacSha256Signature)
        );#1#
        //return new JwtSecurityTokenHandler().WriteToken(Token);
        */

    #endregion

    public async Task<string> LoginAsync(loginUser user)
    {
        var secretKey = Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]); // longer that 16 character
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);
           
        var encryptionkey = Encoding.UTF8.GetBytes(_configuration["JWT:EncryptKey"]); //must be 16 character
        var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);
   
        var claims = await _getClaimsAsync(user);
        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = _configuration["JWT:Issuer"],
            Audience = _configuration["JWT:Audience"],
            IssuedAt = DateTime.Now,
            NotBefore = DateTime.Now.AddMinutes(0),
            Expires =DateTime.Now.AddDays(1),
            SigningCredentials = signingCredentials,
            EncryptingCredentials = encryptingCredentials,
            Subject = new ClaimsIdentity(claims)
        };
        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
    public async Task<IEnumerable<Claim>> _getClaimsAsync(loginUser user)
    {
        var result = await _signInManager.ClaimsFactory.CreateAsync(user);
        //add custom claims
        var list1 = new List<Claim>(result.Claims);
        //list1.Add(new Claim("DepartmentId", user.DepartmentId.ToString()));
        return list1;
    }

}