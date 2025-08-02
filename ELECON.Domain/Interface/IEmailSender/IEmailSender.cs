namespace ELECON.Domain.Interface.IEmailSender
{
    public interface IEmailSender
    {
        bool SendEmail(string to, string subject, string body);
    }
}
