namespace ProductRegistration.Modele.Dto.UserDto;

public class SignupDto
{
    public virtual string Fullname{ get; set; }
    public virtual string? UserName { get; set; }
    public virtual string? Email { get; set; }
    public virtual string Password { get; set; } 

}