using AutoMapper;
using Sat.Recruitment.Application.Interfaces;
using Sat.Recruitment.Application.Utils;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Exceptions;
using System.Threading.Tasks;
using Sat.Recruitment.Application.Model;

namespace Sat.Recruitment.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMoneyCalculatorFactory _moneyCalculatorFactory;

        public UserService(IUserRepository repository, IMapper mapper, IMoneyCalculatorFactory moneyCalculator)
        {
            _repository = repository;
            _mapper = mapper;
            _moneyCalculatorFactory = moneyCalculator;
        }

        public async Task<bool> AddUsersAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var existsUser = await _repository.ExistsUserAsync(user);

            if (existsUser)
            {
                throw new DuplicatedUserException("User is duplicated");
            }

            user.Email = EmailNormalization.NormalizeEmail(user.Email);

            var moneyCalculator = _moneyCalculatorFactory.CreateCalculator(user.UserType);
            user.Money = moneyCalculator.Calculate(user.Money);

            return await _repository.SaveUserAsync(user);
        }

    }
}
