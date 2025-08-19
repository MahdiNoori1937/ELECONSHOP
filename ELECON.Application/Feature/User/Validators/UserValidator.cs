using ELECON.Application.Feature.User.Command;
using ELECON.Application.Feature.User.DTOs;
using FluentValidation;

namespace ELECON.Application.Feature.User.Validators;

public class UserRegisterValidator : AbstractValidator<UserRegisterUserDto>
{
    public UserRegisterValidator()
    {
        RuleFor(c=>c.RegisterInput).NotNull()
            .NotEmpty().WithMessage("لطفا شماره موبایل یا ایمیل خود را وارد کنید")
            .MaximumLength(200).WithMessage("لطفا کمتر از 200 کاراکتر وارد کنید");
        
        RuleFor(c=>c.Password).NotNull().NotEmpty().WithMessage("لطفا رمز عبور را وارد کنید")
            .MaximumLength(200).WithMessage("لطفا کمتر از 100 کاراکتر وارد کنید");
        
    }
}
public class UserSendSmtpValidator : AbstractValidator<UserLoginSMTPCodeDto>
{
    public UserSendSmtpValidator()
    {
        RuleFor(c=>c.RegisterInput).NotNull()
            .NotEmpty().WithMessage("شماره موبایل یا ایمیل نباید خالی باشد")
            .MaximumLength(200).WithMessage("لطفا کمتر از 200 کاراکتر وارد کنید");
        
        RuleFor(c=>c.SMTPCode).NotNull()
            .WithMessage("لطفا کد ارسالی را وارد کنید")
            .NotEmpty()
            .WithMessage("لطفا کد ارسالی را وارد کنید")
            .Must(c=> c!=null && c.Length==5).WithMessage("کد ارسالی باید 5 کاراکتر باشد");
        
    }
}

public class UserPasswordLoginValidator : AbstractValidator<UserLoginPasswordDto>
{
    public UserPasswordLoginValidator()
    {
        RuleFor(c=>c.RegisterInput).NotNull()
            .NotEmpty().WithMessage("شماره موبایل یا ایمیل نباید خالی باشد")
            .MaximumLength(200).WithMessage("لطفا کمتر از 200 کاراکتر وارد کنید");
        
        RuleFor(c=>c.RegisterInput).NotNull()
            .WithMessage("لطفا رمز عبور را وارد کنید")
            .NotEmpty()
            .WithMessage("لطفا رمز عبور را وارد کنید");
    }
}