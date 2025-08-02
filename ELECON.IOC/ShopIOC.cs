using ELECON.Application.Extensions;
using ELECON.Domain.Interface.IUserRepository;
using ELECON.Domain.Interface.SmsSender;
using Elecon.Infrastructure.Repositories.SmsSenderRepositories;
using Elecon.Infrastructure.Repositories.UserRepository;
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






        services.AddScoped<UserSendSms>();
    }
}