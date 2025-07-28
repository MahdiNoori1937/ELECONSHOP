using System.Security.Cryptography;
using ELECON.Domain.Entities.BaseEntities;

namespace ELECON.Domain.Entities.User;

public class UserAddress:BaseEntities<int>
{
    public string Title { get; set; }

    public string Province { get; set; }
    
    public string City { get; set; }
    
    public string PostalCode { get; set; }
    
    public string FullAddress { get; set; }


    #region Relation

    public int UserId { get; set; }
    public User User { get; set; }

    #endregion
}