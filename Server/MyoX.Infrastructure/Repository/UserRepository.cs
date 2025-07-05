using Microsoft.EntityFrameworkCore;
using MyoX.Domain.Entities.User;
using MyoX.Domain.Interfaces;
using MyoX.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyoX.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(UserEntity user) =>
            await _context.Users.AddAsync(user);

        public async Task<UserEntity?> GetUserByEmailAsync(string email) =>
            await _context.Users.FirstOrDefaultAsync(o => o.Email == email);
    }
}
