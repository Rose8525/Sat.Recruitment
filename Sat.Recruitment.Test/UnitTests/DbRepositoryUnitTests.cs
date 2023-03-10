using Moq;
using Moq.EntityFrameworkCore;
using Sat.Recruitment.Domain.Entities;
using Sat.Recruitment.Domain.Enums;
using Sat.Recruitment.Infraestructure.Persistence.EF;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.UnitTests
{
    public class DbRepositoryUnitTests
    {
        [Fact]
        public async Task DbRepositoryExistsUser_OK()
        {
            var list = new List<User>();
            var user = new User
            {
                Email = "rosamariaceraosorio@gmail.com",
                Name = "Rosa",
                Address = "Scoseria 2854",
                Phone = "093810783",
                UserType = UserType.Premium,
                Money = 212m
            };
            list.Add(user);

            var userDbContextMock = new Mock<UserDbContext>();
            userDbContextMock.Setup(x => x.Users).ReturnsDbSet(list);

            var dbRepository = new DbRepository(userDbContextMock.Object);
            var result = await dbRepository.ExistsUserAsync(user);

            Assert.True(result);
        }

        [Fact]
        public async Task DbRepositoryExistsUser_False()
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

            var userDbContextMock = new Mock<UserDbContext>();
            userDbContextMock.Setup(x => x.Users).ReturnsDbSet(new List<User>());

            var dbRepository = new DbRepository(userDbContextMock.Object);
            var result = await dbRepository.ExistsUserAsync(user);

            Assert.False(result);
        }

        [Fact]
        public async Task DbRepositorySaveAsync_OK()
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

            var userDbContextMock = new Mock<UserDbContext>();
            userDbContextMock.Setup(x => x.Users).ReturnsDbSet(new List<User>());
            userDbContextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(() => 1).Verifiable();

            var dbRepository = new DbRepository(userDbContextMock.Object);
            var result = await dbRepository.SaveUserAsync(user);

            Assert.True(result);
            userDbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}
