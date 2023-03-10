using Sat.Recruitment.Application.Interfaces;

namespace Sat.Recruitment.Application.MoneyCalculator
{
    public class PremiumUserMoneyCalculator : IMoneyCalculator
    {
        private const decimal PREMIUMUSER_PERCENTAGE = 2m;

        public decimal Calculate(decimal amount)
        {
            var calc = this as IMoneyCalculator;

            if (amount > 100)
            {
                return calc.CalculateMoney(amount, PREMIUMUSER_PERCENTAGE);
            }

            return amount;
        }
    }
}
