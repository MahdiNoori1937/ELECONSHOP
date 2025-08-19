namespace ELECON.Application.Feature.User.DTOs;

public class UserRegisterUserDto
{
    public string RegisterInput { get; set; }

    public string Password { get; set; }
    
}

public class UserLoginSMTPCodeDto
{
    public string RegisterInput { get; set; }
    
    public string SMTPCode { get; set; }
}
public class UserLoginPasswordDto
{
    public string RegisterInput { get; set; }
    
    public string Password { get; set; }
}