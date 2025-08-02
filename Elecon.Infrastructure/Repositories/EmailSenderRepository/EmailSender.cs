using System.Net.Mail;
using ELECON.Domain.Interface.IEmailSender;
using EleconShop.Domain.Dtos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Elecon.Infrastructure.Repositories.EmailSender
{
    public class EmailSender : IEmailSender
    {
        #region Ctor

        private readonly ILogger<EmailSender> _logger;
        private readonly SiteSmtpService Smtp;

        public EmailSender(ILogger<EmailSender> logger,IOptions<SiteSmtpService> _Smtp)
        {
            _logger = logger;
            Smtp = _Smtp.Value;
        }
        #endregion
        
        public bool SendEmail(string to, string subject, string body)
        {
            try
            {
                //Google App Password
                string appPassword = $"{Smtp.Smtp}";
                //Gmail Address
                string fromEmail = $"{Smtp.Gmail}";

                MailMessage mail = new MailMessage();
                //smptForGmail
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                
                //GmailName and gmailSenderNName
                mail.From = new MailAddress(fromEmail, "Mobikala");
                //Add List Or Single For Send
                mail.To.Add(to);
                //Subject Of Email
                mail.Subject = subject;
                //View Of Body
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;

                SmtpServer.UseDefaultCredentials = false;

                SmtpServer.Credentials = new System.Net.NetworkCredential(fromEmail, appPassword);
                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Email Error\n\tErrorMessage:: {exception.Message}");
                return false;
            }
        }
    }
}
