using System.Data;
using Dapper;
using ELECON.Domain.Entities.User;
using ELECON.Domain.Interface.IUserRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Elecon.Infrastructure.Repositories.UserRepository;

public class RoleRepository:IRoleRepository
{
    private readonly IDbConnection _db;

    public RoleRepository(IConfiguration config)
    {
        _db = new SqlConnection(config.GetConnectionString("ELECON_SHOPConnectionStrings"));
    }
    
    public async Task<string> Add(Role parameter, string connectionString)
    {
        DynamicParameters parameters = new ();
        parameters.Add("@CreateDate", parameter.CreateDate, DbType.DateTime);
        parameters.Add("@IsDelete", parameter.IsDelete, DbType.Boolean);
        parameters.Add("@Title", parameter.Title, DbType.String);
        parameters.Add("@Result",null,DbType.String, ParameterDirection.Output);
        
        await _db.ExecuteAsync("dbo.sp_CreateUser", parameters, commandType: CommandType.StoredProcedure);
        
        return parameters.Get<string>("@Result");
    }

    public async Task<string> Update(Role parameter, string connectionString)
    {
        DynamicParameters parameters = new ();
        parameters.Add("@Id", parameter.Id,DbType.Int32);
        parameters.Add("@CreateDate", parameter.CreateDate, DbType.DateTime);
        parameters.Add("@IsDelete", parameter.IsDelete, DbType.Boolean);
        parameters.Add("@Title", parameter.Title, DbType.String);
        parameters.Add("@Result",null,DbType.String, ParameterDirection.Output);
        
        await _db.ExecuteAsync("dbo.sp_CreateUser", parameters, commandType: CommandType.StoredProcedure);
        
        return parameters.Get<string>("@Result");
    }

    public async Task<string> Delete(int id, string connectionString)
    {
        DynamicParameters parameters = new ();
        parameters.Add("@Id", id,DbType.Int32);
        parameters.Add("@Result",null,DbType.String, ParameterDirection.Output);
        
        await _db.ExecuteAsync("dbo.sp_CreateUser", parameters, commandType: CommandType.StoredProcedure);
        
        return parameters.Get<string>("@Result");
    }

    public async Task<Role> Get(int Id, string connectionString)
    {
        DynamicParameters parameters = new();
        parameters.Add("@Id", Id,DbType.Int32);
        IEnumerable<Role> Role = await _db.QueryAsync<Role>("dbo.sp_CreateUser", parameters, commandType: CommandType.StoredProcedure);
        return Role.FirstOrDefault();
    }
}