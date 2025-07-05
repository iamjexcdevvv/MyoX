using FluentValidation;
using FluentValidation.Results;
using MyoX.Application.Abstraction.Command;
using MyoX.Domain.Common;
using MyoX.Domain.Entities.User;
using MyoX.Domain.Errors;
using MyoX.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyoX.Application.Features.Authentication.Register
{
    public sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand>
    {
        private readonly IValidator<RegisterCommand> _validator;
        private readonly IUserRepository _userRepo;
        public RegisterCommandHandler(IUserRepository userRepo, IValidator<RegisterCommand> validator)
        {
            _userRepo = userRepo;
            _validator = validator;
        }
        public async Task<Result> Handle(RegisterCommand command, CancellationToken cancellationToken = default)
        {
            ValidationResult result = await _validator.ValidateAsync(command);

            if (!result.IsValid)
            {
                return Result.Failure(new Error("Request", "Invalid request"));
            }

            var user = _userRepo.GetUserByEmailAsync(command.request.Email);

            if (user is not null)
            {
                return Result.Failure(UserAuthError.EmailAlreadyExists);
            }

            UserEntity newUser = new UserEntity
            {
                Email = command.request.Email,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword(command.request.Password)
            };

            await _userRepo.CreateUserAsync(newUser);

            return Result.Success();
        }
    }
}
