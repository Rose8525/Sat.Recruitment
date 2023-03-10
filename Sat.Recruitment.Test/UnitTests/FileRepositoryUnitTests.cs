using System.IO;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Enums;
using Sat.Recruitment.Infraestructure.Persistence.File;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.UnitTests
{
    public class FileRepositoryUnitTests : IClassFixture<UnitTestOneTimeSetup>
    {
        private static readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "UnitTestUsers.txt");
        
        [Fact]
        public async Task FileRepositorySaveUser_OK()
        {
            var user = new User
            {
                Email = "rosamariaceraosorio@gmail.com",
                Name = "Rosa",
                Address = "Scoseria 2854",
                Phone = "093810783",
                UserType = UserType.Premium,
                Money = 212m
            };

            var repository = new FileRepository(FilePath);
            var result = await repository.SaveUserAsync(user);

            Assert.True(result);
        }

        [Fact]
        public async Task FileRepositoryExistsUser_FAIL()
        {
            var user = new User
            {
                Email = "rosamariaceraosorio@gmail.com",
                Name = "Rosa",
                Address = "Scoseria 2854",
                Phone = "093810783",
                UserType = UserType.Premium,
                Money = 212m
            };

            var repository = new FileRepository(FilePath);
            var result = await repository.ExistsUserAsync(user);

            Assert.False(result);
        }

        [Fact]
        public async Task FileRepositoryExistsUser_OK()
        {
            var user = new User
            {
                Email = "agustina@gmail.com",
                Name = "Agustina",
                Address = "Scoseria 2854",
                Phone = "093810783",
                UserType = UserType.Premium,
                Money = 212m
            };

            var repository = new FileRepository(FilePath);
            var result = await repository.ExistsUserAsync(user);

            Assert.True(result);
        }
    }
}
