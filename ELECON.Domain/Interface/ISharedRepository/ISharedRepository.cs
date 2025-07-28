namespace ELECON.Domain.Interface.ISharedRepository;

public interface ISharedRepository<T>
{
    
    Task<string> Add (T parameter, string connectionString);
    
    Task<string> Update (T parameter, string connectionString);
    
    Task<string> Delete (int id , string connectionString);
    
    Task<T> Get (int Id, string connectionString);
}