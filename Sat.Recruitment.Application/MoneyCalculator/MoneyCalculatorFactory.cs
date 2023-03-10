using Sat.Recruitment.Application.Interfaces;
using Sat.Recruitment.Domain.Enums;
using Sat.Recruitment.Domain.Exceptions;

namespace Sat.Recruitment.Application.MoneyCalculator
{
    public class MoneyCalculatorFactory : IMoneyCalculatorFactory
    {
        // Was used a switch to avoid more overengineering but it's recomended in bigger projects to use mediator, or a dictionary...  
        public IMoneyCalculator CreateCalculator(UserType userType)
        {
            return userType switch
            {
                UserType.Normal => new NormalUserMoneyCalculator(),
                UserType.SuperUser => new SuperUserMoneyCalculator(),
                UserType.Premium => new PremiumUserMoneyCalculator(),
                _ => throw new InvalidUserTypeException($"Invalid userType: {userType}")
            };
        }
    }
}
