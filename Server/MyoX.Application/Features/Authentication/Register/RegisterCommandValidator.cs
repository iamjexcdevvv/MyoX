using FluentValidation;
using MyoX.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyoX.Application.Features.Authentication.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(o => o.request.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(o => o.request.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password should at least consist of 8 characters");

            RuleFor(o => o.request.ConfirmPassword)
                .NotEmpty().WithMessage("ConfirmPassword is required")
                .Equal(o => o.request.Password).WithMessage("ConfirmPassword must match Password");
        }
    }
}
