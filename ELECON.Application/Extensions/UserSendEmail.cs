using ELECON.Domain.Entities.User;
using ELECON.Domain.Interface.IEmailSender;
using ELECON.Domain.Interface.SmsSender;

namespace ELECON.Application.Extensions;

public class UserSendEmail
{
    private readonly IEmailSender _emailSender;

    public UserSendEmail(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task SendSms(User user, string code)
    {
        string message = "";
        if (user.FirstName != null || user.LastName != null)
        {
            string firstName = user.FirstName;
            string lastName = user.LastName;
            message = firstName + " " + lastName + $"عزیز کد یک بار مصرف شما : {code}";
        }
        else
        {
            message = $"کاربر گرامی عزیز کد یکبار مصرف شما: {code}";
        }

        _emailSender.SendEmail(user.Email,"کد یکبار مصرف",message);
    }
}