using System.Data;
using Dapper;
using ELECON.Domain.Entities.User;
using ELECON.Domain.Interface.IUserRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Elecon.Infrastructure.Repositories.UserRepository;

public class UserLoginHistoryRepository:IUserLoginHistoryRepository
{
    private readonly IDbConnection _db;

    public UserLoginHistoryRepository(IConfiguration config)
    {
        _db = new SqlConnection(config.GetConnectionString("ELECON_SHOPConnectionStrings"));
    }
    public async Task<string> Add(UserLoginHistory parameter)
    {
        DynamicParameters parameters = new ();
        parameters.Add("@UserId", parameter.UserId, DbType.Int32);
        parameters.Add("@IpAddress", parameter.IpAddress, DbType.String);
        parameters.Add("@IsSuccess", parameter.IsSuccess, DbType.Boolean);
        parameters.Add("@LogTime", parameter.LogTime, DbType.DateTime);
        
        
        parameters.Add("@Result",null,DbType.String, ParameterDirection.Output);
        
        await _db.ExecuteAsync("dbo.sp_CreateUser", parameters, commandType: CommandType.StoredProcedure);
        
        return parameters.Get<string>("@Result");
    }

    public async Task<string> Update(UserLoginHistory parameter)
    {
        DynamicParameters parameters = new ();
        parameters.Add("@Id", parameter.Id, DbType.Int32);
        parameters.Add("@UserId", parameter.UserId, DbType.Int32);
        parameters.Add("@IpAddress", parameter.IpAddress, DbType.String);
        parameters.Add("@IsSuccess", parameter.IsSuccess, DbType.Boolean);
        parameters.Add("@LogTime", parameter.LogTime, DbType.DateTime);
        
        
        parameters.Add("@Result",null,DbType.String, ParameterDirection.Output);
        
        await _db.ExecuteAsync("dbo.sp_CreateUser", parameters, commandType: CommandType.StoredProcedure);
        
        return parameters.Get<string>("@Result");
    }

    public async Task<string> Delete(int id)
    {
        DynamicParameters parameters = new ();
        parameters.Add("@Id", id,DbType.Int32);
        parameters.Add("@Result",null,DbType.String, ParameterDirection.Output);
        
        await _db.ExecuteAsync("dbo.sp_CreateUser", parameters, commandType: CommandType.StoredProcedure);
        
        return parameters.Get<string>("@Result");
    }

    public async Task<UserLoginHistory> Get(int Id)
    {
        DynamicParameters parameters = new();
        parameters.Add("@Id", Id,DbType.Int32);
        IEnumerable<UserLoginHistory> userLoginHistory = await _db.QueryAsync<UserLoginHistory>("dbo.sp_CreateUser", parameters, commandType: CommandType.StoredProcedure);
        return userLoginHistory.FirstOrDefault();
    }
}