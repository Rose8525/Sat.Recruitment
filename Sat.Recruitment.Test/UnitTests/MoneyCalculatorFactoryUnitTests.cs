using Sat.Recruitment.Application.MoneyCalculator;
using Sat.Recruitment.Domain.Enums;
using Sat.Recruitment.Domain.Exceptions;
using Xunit;

namespace Sat.Recruitment.Test.UnitTests
{
    public class MoneyCalculatorFactoryUnitTests
    {
        [Fact]
        public void InvalidUserTypeThrowsAnException_FAIL()
        {
            var moneyCalculatorFactory = new MoneyCalculatorFactory();

            Assert.Throws<InvalidUserTypeException>(() => moneyCalculatorFactory.CreateCalculator(UserType.Invalid));
        }

        [Fact]
        public void ValidUserTypeReturnCorrespondedObject_OK()
        {
            var moneyCalculatorFactory = new MoneyCalculatorFactory();
            var calculator = moneyCalculatorFactory.CreateCalculator(UserType.SuperUser);

            Assert.NotNull(calculator);
            Assert.Equal(typeof(SuperUserMoneyCalculator), calculator.GetType());
        }
    }
}
