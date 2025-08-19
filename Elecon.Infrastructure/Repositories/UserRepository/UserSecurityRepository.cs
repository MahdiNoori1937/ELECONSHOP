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
    public async Task<(string result, int id)> Add(UserSecurity parameter)
    {
        DynamicParameters parameters = new ();
        parameters.Add("@UserId", parameter.UserId, DbType.Int32);
        parameters.Add("@LockoutEnd", parameter.LockoutEnd, DbType.DateTime);
        parameters.Add("@LastFailedLogin", parameter.LastFailedLogin, DbType.DateTime);
        parameters.Add("@IsLockedOut", parameter.IsLockedOut, DbType.Boolean);
        parameters.Add("@FailedLoginAttempts", parameter.FailedLoginAttempts, DbType.Int32);
        parameters.Add("@Result",null,DbType.String, ParameterDirection.Output,size:50);
        parameters.Add("@Id",null,DbType.Int32, ParameterDirection.Output);
        
        await _db.ExecuteAsync("Add_UserSecurity", parameters, commandType: CommandType.StoredProcedure);

        string result = parameters.Get<string>("@Result");
        int Id = parameters.Get<int>("@Id");
        return (result, Id);
        
    }

    public async Task<string> Update(UserSecurity parameter)
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

    public async Task<string> Delete(int id)
    {
        DynamicParameters parameters = new ();
        parameters.Add("@id", id,DbType.Int32);
        parameters.Add("@Result",null,DbType.String, ParameterDirection.Output);
        
        await _db.ExecuteAsync("dbo.sp_CreateUser", parameters, commandType: CommandType.StoredProcedure);
        
        return parameters.Get<string>("@Result");
    }

    public async Task<UserSecurity> Get(int Id)
    {
        DynamicParameters parameters = new();
        parameters.Add("@USERID", Id,DbType.Int32);
        IEnumerable<UserSecurity> userSecurity = await _db.QueryAsync<UserSecurity>("Get_UserSecurity", parameters, commandType: CommandType.StoredProcedure);
        return userSecurity.FirstOrDefault();
    }

    public async Task<UserSecurity> GetUserSecurityByInput(string input)
    {
        DynamicParameters parameters = new();
        parameters.Add("@input", input,DbType.Int32);
        IEnumerable<UserSecurity> userSecurity = await _db.QueryAsync<UserSecurity>("Get_UserSecurityByInput", parameters, commandType: CommandType.StoredProcedure);
        return userSecurity.FirstOrDefault();
    }
}