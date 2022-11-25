using FluentValidation;
using HMS.Business.ViewModels;

namespace HMS.Business.Validators
{
    public class UserLoginVMValidator : AbstractValidator<UserLoginVM>
    {
        public UserLoginVMValidator()
        {
            RuleFor(p => p.Email).NotNull().NotEmpty().EmailAddress().MaximumLength(120);
            RuleFor(p => p.Password).NotNull().NotEmpty().MinimumLength(6).MaximumLength(120);
        }
    }
}