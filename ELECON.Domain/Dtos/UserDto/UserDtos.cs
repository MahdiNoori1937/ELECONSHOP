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
    public string Input { get; set; }
    
    public string Password { get; set; }
}

public class UserGetClaimDto
{
    public string Input { get; set; }
}

public class UserDetailDto
{
    public int Id { get; set; }
    
    public DateTime CreateDate { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string Email { get; set; }
    
    public string Pasword { get; set; }
    
    public string UserStatus { get; set; }
    
    public string UserProfileImage { get; set; }

    public int RoleId { get; set; }
    
    public string RoleTitle { get; set; }
}