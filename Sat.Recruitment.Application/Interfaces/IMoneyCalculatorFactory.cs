using Sat.Recruitment.Domain.Enums;

namespace Sat.Recruitment.Application.Interfaces
{
    public interface IMoneyCalculatorFactory
    {
        IMoneyCalculator CreateCalculator(UserType userType);
    }
}
