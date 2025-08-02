namespace ELECON.Domain.Interface.SmsSender;

public interface ISmsSenderService
{
    Task  MeliSmsSender(string text, string to);
}