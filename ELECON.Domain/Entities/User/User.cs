using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ELECON.Domain.Entities.BaseEntities;

namespace ELECON.Domain.Entities.User;

public class User:BaseEntities<int>
{
    public string FristName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Pasword { get; set; }
    public string UserStatus { get; set; }
    public string UserProfileImage { get; set; }
    
    #region Relations

    public int RoleId { get; set; }
    public Role Role { get; set; }
    
    
    public ICollection<UserAddress>? UserAddresses { get; set; }
    
    public ICollection<UserLoginHistory>? LoginHistories { get; set; }
    
    public ICollection<UserNotification>? UserNotifications { get; set; }
    

    public UserSecurity UserSecurity { get; set; }
    
    
    #endregion
}

public enum UserStatus
{
    [Display(Name = "Active")]
    Active,
    [Display(Name = "Inactive")]
    Inactive,
    [Display(Name = "Deleted")]
    Deleted,
    [Display(Name = "Banned")]
    Banned
}