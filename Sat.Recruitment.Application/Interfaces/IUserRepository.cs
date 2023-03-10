using Sat.Recruitment.Domain.Entities;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistsUserAsync(User user);
        Task<bool> SaveUserAsync(User user);
    }
}
