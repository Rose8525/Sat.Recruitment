using Sat.Recruitment.Application.MoneyCalculator;
using Xunit;

namespace Sat.Recruitment.Test.UnitTests
{
    public class NormalUserMoneyCalculatorUnitTests
    {
        [Fact]
        public void NormalUserMoneyCalculatorWithAmountGreaterThan100_OK()
        {
            var moneyCalculator = new NormalUserMoneyCalculator();
            var result = moneyCalculator.Calculate(150);

            Assert.Equal(168m, result);
        }

        [Fact]
        public void NormalUserMoneyCalculatorWithAmountBetween10And100_OK()
        {
            var moneyCalculator = new NormalUserMoneyCalculator();
            var result = moneyCalculator.Calculate(70);

            Assert.Equal(126m, result);
        }

        [Fact]
        public void NormalUserMoneyCalculatorWithAmountLessThan10_OK()
        {
            var moneyCalculator = new NormalUserMoneyCalculator();
            var result = moneyCalculator.Calculate(5);

            Assert.Equal(5m, result);
        }
    }
}
