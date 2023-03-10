using System.Threading.Tasks;
using Sat.Recruitment.Application.Model;

namespace Sat.Recruitment.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> AddUsersAsync(UserDto userDto);
    }
}
