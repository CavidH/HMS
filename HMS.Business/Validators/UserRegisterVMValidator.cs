﻿using FluentValidation;
using HMS.Business.ViewModels;

namespace HMS.Business.Validators;

public class UserRegisterVMValidator : AbstractValidator<UserRegisterVM>
{
    public UserRegisterVMValidator()
    {
        RuleFor(p => p.FirstName).NotNull().NotEmpty().MaximumLength(90);
        RuleFor(p => p.LastName).NotNull().NotEmpty().MaximumLength(90);
        RuleFor(p => p.Email).NotNull().NotEmpty().EmailAddress().MaximumLength(120);
        RuleFor(p => p.UserName).NotNull().NotEmpty().MaximumLength(90);
        RuleFor(p => p.Password).NotNull().NotEmpty().MinimumLength(6/*8*/).MaximumLength(90);
        RuleFor(p => p.ConfirmPassword).NotNull().NotEmpty().MinimumLength(8).MaximumLength(90).Equal(p => p.Password);
    }
}