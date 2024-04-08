using Infrastructure.Entity.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductRegistration.Modele.Dto.UserDto;
using Service_Layer.Service.Jwt;

namespace ProductRegistration.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class UserController:ControllerBase
{
    private readonly IJWTRepository _jwtRepository;
    private readonly UserManager<loginUser> _userManager;
    private readonly SignInManager<loginUser> _signInManager;

    public UserController(IJWTRepository jwtRepository, UserManager<loginUser> userManager,SignInManager<loginUser> signInManager)
    {
        _jwtRepository = jwtRepository;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    [HttpPost]
    public async Task<IActionResult> Signup(SignupDto signUpModel)
    {
        var newuser = new loginUser()
        {
            fullname = signUpModel.Fullname,
            UserName = signUpModel.UserName,
            Email = signUpModel.Email,
            PasswordHash = signUpModel.Password,
        
        };
        /*//var result = await _jwtRepository.SignUpAsync(newuser);
        /*if (result.Succeeded)
        {
            return Ok(result.Succeeded);
        }#1#*/
        await _userManager.CreateAsync(newuser, signUpModel.Password);

        return Ok(newuser);
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(dto.Username);
            if (user == null)
            {
                throw new Exception(message: "نام کاربری یا رمز عبور اشتباه است");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!isPasswordValid)
            {
                throw new Exception(message: "نام کاربری یا رمز عبور اشتباه است");
            }
            
            var Result = await _jwtRepository.LoginAsync(user);

            return Ok(Result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    } 
}