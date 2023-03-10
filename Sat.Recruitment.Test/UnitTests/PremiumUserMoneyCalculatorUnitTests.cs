using Sat.Recruitment.Application.MoneyCalculator;
using Xunit;

namespace Sat.Recruitment.Test.UnitTests
{
    public class PremiumUserMoneyCalculatorUnitTests
    {
        [Fact]
        public void PremiumUserMoneyCalculatorWithAmountGreaterThan100_OK()
        {
            var moneyCalculator = new PremiumUserMoneyCalculator();
            var result = moneyCalculator.Calculate(160);

            Assert.Equal(480m, result);
        }

        [Fact]
        public void PremiumUserMoneyCalculatorWithAmountLessOrEqualThan100_OK()
        {
            var moneyCalculator = new PremiumUserMoneyCalculator();
            var result = moneyCalculator.Calculate(20);

            Assert.Equal(20m, result);
        }
    }
}
