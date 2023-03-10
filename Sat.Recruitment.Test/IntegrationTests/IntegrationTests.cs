using Newtonsoft.Json;
using Sat.Recruitment.Application.Model;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Enums;
using Xunit;

namespace Sat.Recruitment.Test.IntegrationTests
{
    public class IntegrationTests : IClassFixture<TestOneTimeSetup>
    {
        private readonly TestOneTimeSetup _setup;

        public IntegrationTests(TestOneTimeSetup setup)
        {
            _setup = setup;
        }

        [Fact]
        public async Task AddNewUser_OK()
        {
            var response = await _setup.HttpClient.PostAsync("create-user?name=Rosa&email=rosamariaceraosorio@gmail.com&address=Scoseria%202854&phone=093810783&userType=Normal&money=80", null);

            var responseDto = JsonConvert.DeserializeObject<Result>(await response.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseDto);
            Assert.True(responseDto.IsSuccess);
            Assert.Equal("User Created", responseDto.Errors);
        }

        [Fact]
        public async Task TryAddAnExistentUser_FAIL()
        {
            const string url = "create-user?name=John&email=john@gmail.com&address=Blanco%202854&phone=45610743&userType=Premium&money=80";
            await _setup.HttpClient.PostAsync(url, null);

            var response = await _setup.HttpClient.PostAsync(url, null);

            var responseDto = JsonConvert.DeserializeObject<Result>(await response.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotNull(responseDto);
            Assert.False(responseDto.IsSuccess);
            Assert.Equal("User is duplicated", responseDto.Errors);
        }

        [Fact]
        public async Task TryAddAnInvalidUser_FAIL()
        {
            var response = await _setup.HttpClient.PostAsync("create-user?phone=674654&userType=Premium&money=25", null);

            var responseDto = JsonConvert.DeserializeObject<Result>(await response.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseDto);
            Assert.False(responseDto.IsSuccess);
            Assert.Equal("'Name' must not be empty.; 'Name' must not be empty.; 'Email' must not be empty.; " +
                         "'Email' must not be empty.; 'Address' must not be empty.; 'Address' must not be empty.", responseDto.Errors);
        }

        [Fact]
        public async Task TryAddAnInvalidMoneyValueUser_FAIL()
        {
            var response = await _setup.HttpClient.PostAsync("create-user?name=Rosa&email=rosamariaceraosorio@gmail.com&address=Scoseria%202854&phone=093810783&userType=Normal&money=as12", null);

            var responseDto = JsonConvert.DeserializeObject<Result>(await response.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseDto);
            Assert.False(responseDto.IsSuccess);
            Assert.Equal("The money must be a number.", responseDto.Errors);
        }

        [Fact]
        public async Task TryAddUserWithInvalidUserType_FAIL()
        {
            var response = await _setup.HttpClient.PostAsync("create-user?name=Rosa&email=rosamariaceraosorio@gmail.com&address=Scoseria%202854&phone=093810783&userType=None&money=80", null);

            var responseDto = JsonConvert.DeserializeObject<Result>(await response.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseDto);
            Assert.False(responseDto.IsSuccess);
            Assert.Equal("The userType must be valid.", responseDto.Errors);
        }


        #region IntegrationTestV2

        [Fact]
        public async Task V2_AddNewUser_OK()
        {
            var user = new User
            {
                Email = "rosa@gmail.com",
                Name = "Rosa2",
                Address = "Scoseria 285423",
                Phone = "34234",
                UserType = UserType.Premium,
                Money = 212m
            };
            var httpContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _setup.HttpClient.PostAsync("/api/v1/users", httpContent);

            var responseDto = JsonConvert.DeserializeObject<ResultV1>(await response.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseDto);
            Assert.True(responseDto.IsSuccess);
            Assert.Empty(responseDto.Errors);
        }

        [Fact]
        public async Task V2_TryAddAnExistentUser_FAIL()
        {

            var user = new User
            {
                Email = "john@gmail.com",
                Name = "John",
                Address = "Blanco 21",
                Phone = "5646465",
                UserType = UserType.Premium,
                Money = 180m
            };

            const string url = "/api/v1/users";
            var httpContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            await _setup.HttpClient.PostAsync(url, httpContent);

            var response = await _setup.HttpClient.PostAsync(url, httpContent);

            var responseDto = JsonConvert.DeserializeObject<ResultV1>(await response.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotNull(responseDto);
            Assert.False(responseDto.IsSuccess);
            Assert.Equal("User is duplicated", responseDto.Errors[0]);
        }

        [Fact]
        public async Task V2_TryAddAnInvalidUser_FAIL()
        {
            var user = new User
            {
                Email = "",
                Name = "",
                Address = "",
                Phone = "093810783",
                UserType = UserType.Premium,
                Money = 212m
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _setup.HttpClient.PostAsync("/api/v1/users", httpContent);

            var responseDto = JsonConvert.DeserializeObject<ResultV1>(await response.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotNull(responseDto);
            Assert.False(responseDto.IsSuccess);
            Assert.Equal("'Name' must not be empty.", responseDto.Errors[0]);
        }

        [Fact]
        public async Task V2_TryAddAnInvalidMoneyValueUser_FAIL()
        {
            var user = new User
            {
                Email = "rosamariaceraosorio@gmail.com",
                Name = "Rosa",
                Address = "Scoseria 2854",
                Phone = "093810783",
                UserType = UserType.Premium,
                Money = -12
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _setup.HttpClient.PostAsync("/api/v1/users", httpContent);

            var responseDto = JsonConvert.DeserializeObject<ResultV1>(await response.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotNull(responseDto);
            Assert.False(responseDto.IsSuccess);
            Assert.Equal("'Money' must be greater than or equal to '0'.", responseDto.Errors[0]);
        }

        [Fact]
        public async Task V2_TryAddUserWithInvalidUserType_FAIL()
        {
            var user = new User
            {
                Email = "rosamariaceraosorio@gmail.com",
                Name = "Rosa",
                Address = "Scoseria 2854",
                Phone = "093810783",
                UserType = UserType.Invalid,
                Money = 212m
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _setup.HttpClient.PostAsync("/api/v1/users", httpContent);

            var responseDto = JsonConvert.DeserializeObject<ResultV1>(await response.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotNull(responseDto);
            Assert.False(responseDto.IsSuccess);
            Assert.Equal("'User Type' must not be empty.", responseDto.Errors[0]);
        }

        #endregion
    }
}
