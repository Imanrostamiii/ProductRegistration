using Application.BaseDto;
using Infrastructure.Entity.User;

namespace ProductRegistration.Modele.Dto.UserDto;

public class LoginDto:BaseDto<LoginDto,loginUser,int>
{
    public string Username { get; set; }
    
    public string Password { get; set; }
}