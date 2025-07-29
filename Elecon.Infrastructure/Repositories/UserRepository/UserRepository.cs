using System.Data;
using Dapper;
using ELECON.Domain.Entities.User;
using ELECON.Domain.Interface.IUserRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Elecon.Infrastructure.Repositories.UserRepository;

public class UserRepository:IUserRepository
{
    private readonly IDbConnection _db;

    public UserRepository(IConfiguration config)
    {
        _db = new SqlConnection(config.GetConnectionString("ELECON_SHOPConnectionStrings"));
    }

    public async Task<string> Add(User parameter, string connectionString)
    {
        DynamicParameters parameters = new();
        parameters.Add("@RoleId", parameter.RoleId);
        parameters.Add("@Email", parameter.Email,DbType.String);
        parameters.Add("@Password", parameter.Pasword,DbType.String);
        parameters.Add("@FirstName", parameter.FirstName,DbType.String);
        parameters.Add("@LastName", parameter.LastName,DbType.String);
        parameters.Add("@PhoneNumber", parameter.PhoneNumber,DbType.String);
        parameters.Add("@UserStatus", parameter.UserStatus,DbType.String);
        parameters.Add("@UserProfileImage", parameter.UserProfileImage,DbType.String);
        parameters.Add("@Result",null,DbType.String, ParameterDirection.Output);
        
        await _db.ExecuteAsync("dbo.sp_CreateUser", parameters, commandType: CommandType.StoredProcedure);
        
        return parameters.Get<string>("@Result");
    }

    public async Task<string> Update(User parameter, string connectionString)
    {
        DynamicParameters parameters = new();
        parameters.Add("@Id", parameter.Id,DbType.Int32);
        parameters.Add("@RoleId", parameter.RoleId);
        parameters.Add("@Email", parameter.Email,DbType.String);
        parameters.Add("@Password", parameter.Pasword,DbType.String);
        parameters.Add("@FirstName", parameter.FirstName,DbType.String);
        parameters.Add("@LastName", parameter.LastName,DbType.String);
        parameters.Add("@PhoneNumber", parameter.PhoneNumber,DbType.String);
        parameters.Add("@UserStatus", parameter.UserStatus,DbType.String);
        parameters.Add("@UserProfileImage", parameter.UserProfileImage,DbType.String);
        parameters.Add("@IsDelete", parameter.IsDelete,DbType.Boolean);
        parameters.Add("@Result",null,DbType.String, ParameterDirection.Output);
        
        await _db.ExecuteAsync("Update_User", parameters, commandType: CommandType.StoredProcedure);
        
        return parameters.Get<string>("@Result");
    }

    public async Task<string> Delete(int id ,string connectionString)
    {
        DynamicParameters parameters = new();
        parameters.Add("@Id", id,DbType.Int32);
        parameters.Add("@Result",null,DbType.String, ParameterDirection.Output);
        await _db.ExecuteAsync("Delete_User", parameters, commandType: CommandType.StoredProcedure);
        return parameters.Get<string>("@Result");
    }

    public async Task<User?> Get(int id, string connectionString)
    {
        DynamicParameters parameters = new();
        parameters.Add("@Id", id,DbType.Int32);
       IEnumerable<User> user = await _db.QueryAsync<User>("dbo.sp_CreateUser", parameters, commandType: CommandType.StoredProcedure);
       return user.FirstOrDefault();
    }

    #region FindByEmailOrNumberAsync

    public async Task<User> FindByEmailOrNumberAsync(string input,string type)
    {
        DynamicParameters parameters = new();
        parameters.Add("@input", input,DbType.String);
        parameters.Add("@type", type,DbType.String);
        IEnumerable<User> user = await _db.QueryAsync<User>("Get_GetUserByInput", 
            parameters, commandType: CommandType.StoredProcedure);
        return user.FirstOrDefault();
    }

    #endregion
}