using ELECON.Domain.Entities.BaseEntities;

namespace ELECON.Domain.Entities.User;

public class UserSecurity
{
    public int Id { get; set; }
    
    public DateTime? LockoutEnd { get; set; }
    
    public DateTime? LastFailedLogin { get; set; }
    
    public bool? IsLockedOut { get; set; }

    public int FailedLoginAttempts { get; set; }

    public string SMTPCode { get; set; }
    public int UserId { get; set; }

    public User User { get; set; }
}