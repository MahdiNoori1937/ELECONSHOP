using System.Data;
using Dapper;
using ELECON.Domain.Entities.User;
using ELECON.Domain.Interface.IUserRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Elecon.Infrastructure.Repositories.UserRepository;

public class UserSecurityRepository:IUserSecurityRepository
{
    private readonly IDbConnection _db;

    public UserSecurityRepository(IConfiguration config)
    {
        _db = new SqlConnection(config.GetConnectionString("ELECON_SHOPConnectionStrings"));
    }
    public async Task<string> Add(UserSecurity parameter, string connectionString)
    {
        DynamicParameters parameters = new ();
        parameters.Add("@UserId", parameter.UserId, DbType.Int32);
        parameters.Add("@LockoutEnd", parameter.LockoutEnd, DbType.DateTime);
        parameters.Add("@LastFailedLogin", parameter.LastFailedLogin, DbType.DateTime);
        parameters.Add("@IsLockedOut", parameter.IsLockedOut, DbType.Boolean);
        parameters.Add("@FailedLoginAttempts", parameter.FailedLoginAttempts, DbType.Int32);
       
        
        
        parameters.Add("@Result",null,DbType.String, ParameterDirection.Output);
        
        await _db.ExecuteAsync("dbo.sp_CreateUser", parameters, commandType: CommandType.StoredProcedure);
        
        return parameters.Get<string>("@Result");
    }

    public async Task<string> Update(UserSecurity parameter, string connectionString)
    {
        DynamicParameters parameters = new ();
        parameters.Add("@id", parameter.Id, DbType.Int32);
        parameters.Add("@UserId", parameter.UserId, DbType.Int32);
        parameters.Add("@LockoutEnd", parameter.LockoutEnd, DbType.DateTime);
        parameters.Add("@LastFailedLogin", parameter.LastFailedLogin, DbType.DateTime);
        parameters.Add("@IsLockedOut", parameter.IsLockedOut, DbType.Boolean);
        parameters.Add("@FailedLoginAttempts", parameter.FailedLoginAttempts, DbType.Int32);
       
        
        
        parameters.Add("@Result",null,DbType.String, ParameterDirection.Output);
        
        await _db.ExecuteAsync("dbo.sp_CreateUser", parameters, commandType: CommandType.StoredProcedure);
        
        return parameters.Get<string>("@Result");
    }

    public async Task<string> Delete(int id, string connectionString)
    {
        DynamicParameters parameters = new ();
        parameters.Add("@id", id,DbType.Int32);
        parameters.Add("@Result",null,DbType.String, ParameterDirection.Output);
        
        await _db.ExecuteAsync("dbo.sp_CreateUser", parameters, commandType: CommandType.StoredProcedure);
        
        return parameters.Get<string>("@Result");
    }

    public async Task<UserSecurity> Get(int Id, string connectionString)
    {
        DynamicParameters parameters = new();
        parameters.Add("@Id", Id,DbType.Int32);
        IEnumerable<UserSecurity> userSecurity = await _db.QueryAsync<UserSecurity>("dbo.sp_CreateUser", parameters, commandType: CommandType.StoredProcedure);
        return userSecurity.FirstOrDefault();
    }
}