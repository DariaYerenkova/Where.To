using Moq;
using WhereToDataAccess.Entities;
using WhereToDataAccess.Interfaces;
using WhereToServices;
using static System.Collections.Specialized.BitVector32;
using static WhereToDataAccess.Interfaces.IRepository;

namespace ServicesTests
{
    public class UserServiceTests
    {
        #region Private members

        private readonly UserService userService;
        private readonly IEnumerable<User> fakeUsers;
        private readonly Mock<IUnitOfWork> mockUnitOfWork;
        private readonly Mock<IUserRepository> mockRepository;

        #endregion

        public UserServiceTests()
        {
            mockRepository = new Mock<IUserRepository>();
            mockUnitOfWork = new Mock<IUnitOfWork>();

            fakeUsers = new []
            {
                new User { Id = 1, FirstName="Roman", LastName="Horbov"},
                new User { Id = 2, FirstName="Katya", LastName="Romanova"},
                new User { Id = 3, FirstName="Daria", LastName="Nazarenko"}
            };

            mockUnitOfWork.Setup(m => m.Users).Returns(mockRepository.Object);
            userService = new UserService(mockUnitOfWork.Object);
        }

        [Trait("Category", "User")]
        [Fact]
        public void CreateUser_CreateAndSave()
        {
            var user = new User {Id = 4, FirstName = "first", LastName = "last" };

            // Act
            userService.CreateUser(user);

            // Assert
            mockUnitOfWork.Verify(uow => uow.Users.Create(user), Times.Once);
            mockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
        }

        [Trait("Category", "User")]
        [Fact]
        public void Delete_DeleteAndSave()
        {
            // Arrange
            var userId = 1;

            // Act
            userService.Delete(userId);

            // Assert
            mockUnitOfWork.Verify(uow => uow.Users.Delete(userId), Times.Once);
            mockUnitOfWork.Verify(uow => uow.Save(), Times.Once);
        }

        [Trait("Category", "User")]
        [Fact]
        public void GetUsers_ReturnsUsers()
        {
            // Arrange
            mockUnitOfWork.Setup(uow => uow.Users.GetAll()).Returns(fakeUsers.AsQueryable);

            // Act
            var result = userService.GetUsers();

            // Assert
            Assert.Equal(fakeUsers, result);
        }

        [Trait("Category", "User")]
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetUserById_ReturnsUser(int id)
        {
            // Arrange
            mockRepository.Setup(m => m.Get(It.IsAny<int>())).Returns<int>(id => fakeUsers.Single(s => s.Id == id));
            var expected = fakeUsers.Single(t => t.Id == id).FirstName;

            // Act
            var result = userService.GetUserById(id).FirstName;

            // Assert
            Assert.Equal(expected, result);
        }
    }
}