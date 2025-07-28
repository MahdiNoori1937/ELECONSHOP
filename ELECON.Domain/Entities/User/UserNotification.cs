using ELECON.Domain.Entities.BaseEntities;

namespace ELECON.Domain.Entities.User;

public class UserNotification:BaseEntities<int>
{
    public string Message { get; set; }

    public bool IsRead { get; set; }


    #region Relations

    public int UserId { get; set; }
    public User User { get; set; }

    #endregion
}