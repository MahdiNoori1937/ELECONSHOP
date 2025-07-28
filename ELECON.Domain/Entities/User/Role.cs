using ELECON.Domain.Entities.BaseEntities;

namespace ELECON.Domain.Entities.User;

public class Role:BaseEntities<int>
{
    public string Title { get; set; }

    #region Relationships

    public ICollection<User>? Users { get; set; }

    #endregion
}