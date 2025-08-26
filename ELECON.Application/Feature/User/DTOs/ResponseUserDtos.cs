using System.ComponentModel;

namespace ELECON.Application.Feature.User.DTOs;

public enum CheckUserRegisterStatus
{
    InputExists,
    Success,
    Failed
}

public enum UserRegisterStatus
{
    EmailExists,
    Success,
    Failed,
    RoleNotExists,
    PhoneNumberExists,
}
public enum AddUserSecurityStatus
{
    UserNotFound,
    Failed,
    Success
}

public enum UserSMTPLoginStatus
{
    UserNotFound,
    UserLockout,
    UserGotTimeOut,
    SMTPCodeFailed,
    Success
}
public enum UserPasswordLoginStatus
{
    UserNotActive,
    UserBanned,
    UserNotFound,
    UserLockout,
    UserGotTimeOut,
    PasswordError,
    PasswordNotSet,
    Success
}

