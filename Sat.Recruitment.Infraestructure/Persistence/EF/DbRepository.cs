using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Application.Interfaces;
using Sat.Recruitment.Domain.Entities;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infraestructure.Persistence.EF
{
    public class DbRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public DbRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsUserAsync(User user)
        {
           return await _context.Users.AnyAsync(u => u.Email == user.Email || u.Phone == user.Phone
                                                                           || (u.Name == user.Name && u.Address == user.Address));
        }

        public async Task<bool> SaveUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
