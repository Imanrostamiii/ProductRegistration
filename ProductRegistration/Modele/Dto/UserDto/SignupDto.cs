using Application.BaseDto;
using Infrastructure.Entity.User;

namespace ProductRegistration.Modele.Dto.UserDto;

public class SignupDto:BaseDto<SignupDto,loginUser,int>
{
    public virtual string Fullname{ get; set; }
    public virtual string? UserName { get; set; }
    public virtual string? Email { get; set; }
    public virtual string Password { get; set; } 

}