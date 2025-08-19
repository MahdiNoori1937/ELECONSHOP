namespace ELECON.Domain.Interface.ISharedRepository;

public interface ISharedRepository<T>
{

    Task<(string result, int id)> Add(T parameter);
    
    Task<string> Update (T parameter);
    
    Task<string> Delete (int id );
    
    Task<T> Get (int Id);
}