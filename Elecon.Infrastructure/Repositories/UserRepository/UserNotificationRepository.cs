using System.Data;
using Dapper;
using ELECON.Domain.Entities.User;
using ELECON.Domain.Interface.IUserRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Elecon.Infrastructure.Repositories.UserRepository;

public class UserNotificationRepository:IUserNotificationRepository
{
    private readonly IDbConnection _db;

    public UserNotificationRepository(IConfiguration config)
    {
        _db = new SqlConnection(config.GetConnectionString("ELECON_SHOPConnectionStrings"));
    }
    
    public async Task<(string result, int id)> Add(UserNotification parameter)
    {
        DynamicParameters parameters = new ();
        parameters.Add("@CreateDate", parameter.CreateDate, DbType.DateTime);
        parameters.Add("@IsDelete", parameter.IsDelete, DbType.Boolean);
        parameters.Add("@Title", parameter.Message, DbType.String);
        parameters.Add("@IsRead", parameter.IsRead, DbType.Boolean);
        parameters.Add("@UserId", parameter.UserId, DbType.Int32);
        parameters.Add("@ID",null,DbType.Int32, ParameterDirection.Output);
        
        await _db.ExecuteAsync("Add_UserSecurity", parameters, commandType: CommandType.StoredProcedure);

        string result = parameters.Get<string>("@Result");
        int Id = parameters.Get<int>("@Id");
        return (result, Id);
    }

    public async Task<string> Update(UserNotification parameter)
    {
        DynamicParameters parameters = new ();
        parameters.Add("@Id", parameter.Id, DbType.Int32);
        parameters.Add("@CreateDate", parameter.CreateDate, DbType.DateTime);
        parameters.Add("@IsDelete", parameter.IsDelete, DbType.Boolean);
        parameters.Add("@Title", parameter.Message, DbType.String);
        parameters.Add("@IsRead", parameter.IsRead, DbType.Boolean);
        parameters.Add("@UserId", parameter.UserId, DbType.Int32);
        
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

    public async Task<UserNotification> Get(int Id)
    {
       
        DynamicParameters parameters = new();
        parameters.Add("@Id", Id,DbType.Int32);
        IEnumerable<UserNotification> userNotification = await _db.QueryAsync<UserNotification>("dbo.sp_CreateUser", parameters, commandType: CommandType.StoredProcedure);
        return userNotification.FirstOrDefault();
    }
}