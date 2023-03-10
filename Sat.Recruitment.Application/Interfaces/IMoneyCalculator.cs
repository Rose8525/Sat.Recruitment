namespace Sat.Recruitment.Application.Interfaces
{
    public interface IMoneyCalculator
    {
        decimal Calculate(decimal amount);

        decimal CalculateMoney(decimal amount, decimal percent)
        {
            var percentValue = amount * percent;
            return amount + percentValue;
        }
    }
}
