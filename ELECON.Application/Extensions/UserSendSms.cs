using ELECON.Domain.Entities.User;
using ELECON.Domain.Interface.SmsSender;

namespace ELECON.Application.Extensions;

public  class UserSendSms
{
    private  readonly ISmsSenderService _smsSenderService;

    public UserSendSms(ISmsSenderService smsSenderService)
    {
        _smsSenderService = smsSenderService;
    }
    public  async Task SendSms(User user, string code)
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

        await _smsSenderService.MeliSmsSender(message, user.PhoneNumber); 
    }
}