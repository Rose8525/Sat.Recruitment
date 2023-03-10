using Sat.Recruitment.Application.Interfaces;

namespace Sat.Recruitment.Application.MoneyCalculator
{
    public class NormalUserMoneyCalculator : IMoneyCalculator
    {
        private const decimal USERNORMAL_BETWEEN_10_AND_100_PERCENTAGE = 0.8m;
        private const decimal USERNORMAL_HIGHERTHAN100_PERCENTAGE = 0.12m;

        public decimal Calculate(decimal amount)
        {
            var calc = this as IMoneyCalculator;
            if (amount > 100)
            {
                //If new user is normal and has more than USD100
                return calc.CalculateMoney(amount, USERNORMAL_HIGHERTHAN100_PERCENTAGE);
            }

            if (amount > 10)
            {
                return calc.CalculateMoney(amount, USERNORMAL_BETWEEN_10_AND_100_PERCENTAGE);
            }

            return amount;
        }
    }
}
