using Sat.Recruitment.Application.Interfaces;

namespace Sat.Recruitment.Application.MoneyCalculator
{
    public class SuperUserMoneyCalculator : IMoneyCalculator
    {
        private const decimal SUPERUSER_PERCENTAGE = 0.20m;

        public decimal Calculate(decimal amount)
        {
            var calc = this as IMoneyCalculator;

            if (amount > 100)
            {
                return calc.CalculateMoney(amount, SUPERUSER_PERCENTAGE);
            }

            return amount;
        }
    }
}
