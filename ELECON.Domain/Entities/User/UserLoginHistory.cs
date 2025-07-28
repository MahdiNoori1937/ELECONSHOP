namespace ELECON.Domain.Entities.User;

public class UserLoginHistory
{

    public int Id { get; set; }
    
    public DateTime LogTime { get; set; }

    public string IpAddress { get; set; }

    public bool IsSuccess { get; set; }

    #region Relation

    public int UserId { get; set; }

    public User User { get; set; }

    #endregion
}