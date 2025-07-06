using MyoX.Application.Abstraction.Command;
using MyoX.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyoX.Application.Features.Authentication.Login
{
    public record LoginCommand(LoginDTO request) : ICommand<AuthTokenDTO>;
}
