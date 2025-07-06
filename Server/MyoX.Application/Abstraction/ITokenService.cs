using MyoX.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyoX.Application.Abstraction
{
    public interface ITokenService
    {
        string GenerateJWT(UserEntity user);
    }
}
