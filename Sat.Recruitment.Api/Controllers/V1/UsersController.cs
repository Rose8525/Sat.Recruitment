using System;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Application.Interfaces;
using Sat.Recruitment.Application.Model;
using System.Threading.Tasks;
using Sat.Recruitment.Domain.Enums;

namespace Sat.Recruitment.Api.Controllers.V1
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IValidator<UserDto> _validator;
        private readonly IUserService _userService;

        public UsersController(IValidator<UserDto> validator, IUserService userService, ILogger<UsersController> logger)
        {
            _validator = validator;
            _userService = userService;
            _logger = logger;
        }

        /**
         * The contract wasn't modified in order to not break compatibility with the existing clients (The exercise is just a refactoring).
         * In the future, it's better to create a RestfulApi.
         * The response should have the corresponded http status code to be coherent with failed results.
         *      Response status codes wasn't changed in this refactoring to no change the contract.
         *
         * Added a bit of overengineering in this solution in order to show more features, like: Clean architecture.
         */
        [HttpPost]
        [Route("/create-user")]
        public async Task<Result> CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            var result = new Result { IsSuccess = true, Errors = ""};

            if (!decimal.TryParse(money, out decimal newMoney))
            {
                _logger.LogError("Incorrect money value. The money must be a number.");

                result.IsSuccess = false;
                result.Errors = "The money must be a number.";
            }

            if (!Enum.TryParse<UserType>(userType, out UserType enumUserType))
            {
                _logger.LogError("Invalid UserType.");

                result.IsSuccess = false;
                result.Errors += "The userType must be valid.";
            }

            if (!result.IsSuccess)
            {
                return result;
            }

            var newUser = new UserDto()
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = enumUserType,
                Money = newMoney
            };

            var validationResult = await _validator.ValidateAsync(newUser);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ToString("; ");
                _logger.LogError($"Failed validating user data. Errors: {errors}");

                return new Result
                {
                    IsSuccess = false,
                    Errors = errors
                };
            }

            result.IsSuccess = await _userService.AddUsersAsync(newUser);
            result.Errors = "User Created"; // This is not an Error, so Errors field must be empty. This was not changed to not change the contract.

            _logger.LogInformation("User Created");

            return result;
        }
    }
}
