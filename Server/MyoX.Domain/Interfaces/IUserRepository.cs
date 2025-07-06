using MyoX.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyoX.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetUserByEmailAsync(string email);
        Task CreateUserAsync(UserEntity user);
        Task<bool> IsUserFoundAsync(string email);
    }
}
