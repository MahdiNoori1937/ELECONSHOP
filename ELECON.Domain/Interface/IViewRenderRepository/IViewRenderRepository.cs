namespace ELECON.Domain.Interface.IViewRenderRepository
{
    public interface IViewRenderRepository
    {
        string RenderToStringAsync(string viewName, object model);
    }
}
