using FluentValidation;
using Sat.Recruitment.Application.Model;

namespace Sat.Recruitment.Application.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(user => user.Name).NotNull().NotEmpty();
            RuleFor(user => user.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(user => user.Address).NotNull().NotEmpty();
            RuleFor(user => user.Phone).NotNull().NotEmpty();
            RuleFor(user => user.UserType).NotNull().NotEmpty();
            RuleFor(user => user.Money).NotNull().NotEmpty().GreaterThanOrEqualTo(0);
        }
    }
}
