using System.ComponentModel.DataAnnotations;
using Application.BaseDto;
using Infrastructure.Entity.User;

namespace ProductRegistration.Modele.Dto.UserDto;

public class SignupDto:BaseDto<SignupDto,loginUser,long>
{
    [Required(ErrorMessage = "نام و نام خانوادگی  خود را وارد کنید")]
    [StringLength(60, ErrorMessage = "نمی توانید بیش از 50 کاراکتر استفاده کنید")]
    public virtual string Fullname{ get; set; }
    [Required(ErrorMessage = "نام کاربری خود را وارد کنید")]
    public virtual string? UserName { get; set; }
    [Required(ErrorMessage = "ایمیل خود را وارد کنید")]
    public virtual string? Email { get; set; }
    [Required(ErrorMessage = "رمزعبور خود را وارد کنید")]
    public virtual string Password { get; set; } 

}