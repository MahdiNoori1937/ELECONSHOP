namespace ELECON.Domain.Entities.BaseEntities;

public class BaseEntities<T> where T : struct
{
    public T Id { get; set; }
    
    public DateTime CreateDate { get; set; }
    
    public bool IsDelete { get; set; }
}