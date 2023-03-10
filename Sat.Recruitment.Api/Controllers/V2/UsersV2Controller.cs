using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Application.Interfaces;
using Sat.Recruitment.Application.Model;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers.V2
{
    /*
     * To handle api version we could use https://github.com/dotnet/aspnet-api-versioning 
     */
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
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

        [HttpPost]
        public async Task<IActionResult> CreateUserExample([FromBody] UserDto newUser)
        {
            var validationResult = await _validator.ValidateAsync(newUser);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.ToString("; ");
                _logger.LogError($"Failed validating user data. Errors: {errors}");

                return BadRequest(new ResultV1
                {
                    IsSuccess = false,
                    Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                });
            }

            var result = await _userService.AddUsersAsync(newUser);

            _logger.LogInformation("User Created");

            return Ok(new ResultV1 { IsSuccess = result });
        }
    }
}
