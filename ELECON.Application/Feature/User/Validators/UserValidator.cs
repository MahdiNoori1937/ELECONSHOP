using ELECON.Application.Feature.User.Command;
using ELECON.Application.Feature.User.DTOs;
using FluentValidation;

namespace ELECON.Application.Feature.User.Validators;

public class UserRegisterValidator : AbstractValidator<RegisterUserDto>
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