namespace ELECON.Domain.Interface.ISmsSenderRepository;

public interface ISmsSenderService
{
    Task  MeliSmsSender(string text, string to);
}