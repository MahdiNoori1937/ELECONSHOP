using System.Data;
using Dapper;
using ELECON.Application.Feature.User.DTOs;
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

    public async Task<(string result, int id)> Add(User parameter)
    {
        DynamicParameters parameters = new();
        parameters.Add("@RoleId", parameter.RoleId,DbType.Int32);
        parameters.Add("@Email", parameter.Email, DbType.String, ParameterDirection.Input,size:200);
        parameters.Add("@Password", parameter.Pasword, DbType.String, ParameterDirection.Input,size:200);
        parameters.Add("@FirstName", parameter.FirstName, DbType.String, ParameterDirection.Input,size:50);
        parameters.Add("@LastName", parameter.LastName, DbType.String, ParameterDirection.Input,size:50);
        parameters.Add("@PhoneNumber", parameter.PhoneNumber, DbType.String, ParameterDirection.Input,size:20);
        parameters.Add("@UserStatus", parameter.UserStatus, DbType.String, ParameterDirection.Input,size:50);
        parameters.Add("@UserProfileImage", parameter.UserProfileImage, DbType.String, ParameterDirection.Input,size:200);
        parameters.Add("@Result",null,DbType.String, ParameterDirection.Output,size:50);
        parameters.Add("@ID",null,DbType.Int32, ParameterDirection.Output);
        
        await _db.ExecuteAsync("Add_User",param:parameters,commandType:CommandType.StoredProcedure);

        string result = parameters.Get<string>("@Result");
        int Id = parameters.Get<int>("@ID");
        return (result, Id);
    }

    public async Task<string> Update(User parameter)
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

    public async Task<string> Delete(int id )
    {
        DynamicParameters parameters = new();
        parameters.Add("@Id", id,DbType.Int32);
        parameters.Add("@Result",null,DbType.String, ParameterDirection.Output);
        await _db.ExecuteAsync("Delete_User", parameters, commandType: CommandType.StoredProcedure);
        return parameters.Get<string>("@Result");
    }

    public async Task<User?> Get(int id)
    {
        DynamicParameters parameters = new();
        parameters.Add("@Id", id,DbType.Int32);
       IEnumerable<User> user = await _db.QueryAsync<User>("dbo.sp_CreateUser", parameters, commandType: CommandType.StoredProcedure);
       return user.FirstOrDefault();
    }
    
    #region FindByEmailOrNumberAsync

    public async Task<User> FindByEmailOrNumberAsync(string input)
    {
        DynamicParameters parameters = new();
        parameters.Add("@input", input,DbType.String);
        IEnumerable<User> user = await _db.QueryAsync<User>("Get_GetUserByInput", 
            parameters, commandType: CommandType.StoredProcedure);
        return user.FirstOrDefault();
    }
    
    #endregion

    #region MyRegion

    public async Task<string> ChangeUserStatus(int userId, string status)
    {
        
        DynamicParameters parameters = new();
        parameters.Add("@Id", userId,DbType.Int32);
        parameters.Add("@status",status,DbType.String);
        parameters.Add("@Result",null,DbType.String, ParameterDirection.Output,size:50);
        await _db.ExecuteAsync("User_Update_UserStatus", parameters, commandType: CommandType.StoredProcedure);
        return parameters.Get<string>("@Result");
    }
    
    #endregion

    #region FindUserByEmailAndPassword

    public async  Task<UserDetailDto> GetUserDetailById(int userId)
    {
        DynamicParameters parameters = new();
        parameters.Add("@Id", userId,DbType.Int32);
        return (await _db.QueryAsync<UserDetailDto>("User_GetUserDetail", parameters, commandType: CommandType.StoredProcedure)).FirstOrDefault()?? new UserDetailDto();
    }

    #endregion
}