using FluentValidation;
using FluentValidation.Results;
using MyoX.Application.Abstraction;
using MyoX.Application.Abstraction.Command;
using MyoX.Application.DTO;
using MyoX.Domain.Common;
using MyoX.Domain.Errors;
using MyoX.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyoX.Application.Features.Authentication.Login
{
    public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, AuthTokenDTO>
    {
        private readonly IValidator<LoginCommand> _validator;
        private readonly IUserRepository _userRepo;
        private readonly ITokenService _tokenService;
        public LoginCommandHandler(IValidator<LoginCommand> validator, IUserRepository userRepo, ITokenService tokenService)
        {
            _validator = validator;
            _userRepo = userRepo;
            _tokenService = tokenService;
        }
        public async Task<Result<AuthTokenDTO>> Handle(LoginCommand command, CancellationToken cancellationToken = default)
        {
            ValidationResult result = await _validator.ValidateAsync(command);

            if (!result.IsValid)
            {
                return Result<AuthTokenDTO>.Failure(new Error("Request", "Invalid request"));
            }

            var user = await _userRepo.GetUserByEmailAsync(command.request.Email);

            if (user is null)
            {
                return Result<AuthTokenDTO>.Failure(UserAuthError.UserNotFound);
            }

            if (!BCrypt.Net.BCrypt.Verify(command.request.Password, user.HashedPassword))
            {
                return Result<AuthTokenDTO>.Failure(UserAuthError.IncorrectPassword);
            }

            string accessToken = _tokenService.GenerateJWT(user);

            return Result<AuthTokenDTO>.Success(new AuthTokenDTO(accessToken));
        }
    }
}
