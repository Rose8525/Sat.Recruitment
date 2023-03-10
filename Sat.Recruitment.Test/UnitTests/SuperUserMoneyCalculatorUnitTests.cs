using Sat.Recruitment.Application.MoneyCalculator;
using Xunit;

namespace Sat.Recruitment.Test.UnitTests
{
    public class SuperUserMoneyCalculatorUnitTests
    {
        [Fact]
        public void SuperUserMoneyCalculatorWithAmountGreaterThan100_OK()
        {
            var moneyCalculator = new SuperUserMoneyCalculator();
            var result = moneyCalculator.Calculate(120);

            Assert.Equal(144m, result);
        }

        [Fact]
        public void SuperUserMoneyCalculatorWithAmountLessOrEqualThan100_OK()
        {
            var moneyCalculator = new SuperUserMoneyCalculator();
            var result = moneyCalculator.Calculate(100);

            Assert.Equal(100m, result);
        }
    }
}
