using ELECON.Application.Extensions;
using ELECON.Domain.Interface.IEmailSender;
using ELECON.Domain.Interface.ISmsSenderRepository;
using ELECON.Domain.Interface.IUserRepository;
using ELECON.Domain.Interface.IViewRenderRepository;
using Elecon.Infrastructure.Repositories.EmailSender;
using Elecon.Infrastructure.Repositories.SmsSenderRepository;
using Elecon.Infrastructure.Repositories.UserRepository;
using Elecon.Infrastructure.Repositories.ViewRenderRepository;
using Microsoft.Extensions.DependencyInjection;

namespace ELECON.IOC;

public static class ShopIOC
{
    public static void IOC(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserSecurityRepository, UserSecurityRepository>();
        services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();
        services.AddScoped<IUserLoginHistoryRepository, UserLoginHistoryRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ISmsSenderService, SmsSenderService>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IViewRenderRepository, ViewRenderService>();






        services.AddScoped<UserSendSms>();
        services.AddScoped<UserSendEmail>();
    }
}