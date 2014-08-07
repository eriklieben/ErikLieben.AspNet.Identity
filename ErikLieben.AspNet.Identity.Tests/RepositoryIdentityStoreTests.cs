using Xunit;

namespace ErikLieben.AspNet.Identity.Tests
{
    using Data.Factory;
    using Data.Repository;
    using Identity;
    using Interfaces;
    using Moq;
    using System;
    using System.Linq;
    using System.Threading;

    public class RepositoryIdentityStoreTests
    {
        public class CreateAsync
        {
            [Fact]
            public async void Must_create_a_user_on_the_repository()
            {
                // Arrange
                var unitOfWorkMock = new Mock<IUnitOfWorkAsync>();
                var unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
                    unitOfWorkFactory
                        .Setup(m => m.CreateAsync<IUserKey<int>>())
                        .Returns(() => unitOfWorkMock.Object);

                var dummyRepository = new DummyRepository<IUserKey<int>>();
                var repositoryFactory = new Mock<IRepositoryFactory>();
                    repositoryFactory
                        .Setup(m => m.Create<IUserKey<int>>(It.IsAny<IUnitOfWorkAsync>()))
                        .Returns(dummyRepository);

                var userToAdd = new DummyUserObject { Id = 1, UserName = "abc" };

                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    unitOfWorkFactory.Object, 
                    repositoryFactory.Object,
                    new Mock<IDependencyFactory>().Object);

                // Act
                await sut.CreateAsync(userToAdd);

                // Assert
                Assert.Equal(userToAdd, dummyRepository.First());
                unitOfWorkMock
                    .Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()), Times.Exactly(1));
            }

            [Fact]
            public async void Must_throw_an_exception_if_user_object_is_null()
            {
                // Arrange
                var unitOfWorkMock = new Mock<IUnitOfWorkAsync>();
                var unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
                unitOfWorkFactory
                    .Setup(m => m.CreateAsync<IUserKey<int>>())
                    .Returns(() => unitOfWorkMock.Object);

                var dummyRepository = new DummyRepository<IUserKey<int>>();
                var repositoryFactory = new Mock<IRepositoryFactory>();
                repositoryFactory
                    .Setup(m => m.Create<IUserKey<int>>(It.IsAny<IUnitOfWorkAsync>()))
                    .Returns(dummyRepository);

                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    unitOfWorkFactory.Object,
                    repositoryFactory.Object,
                    new Mock<IDependencyFactory>().Object);

                // Act
                var ex = await Record.ExceptionAsync(() => sut.CreateAsync(null));

                // Assert
                Assert.IsType<ArgumentNullException>(ex);
                Assert.Equal("Value cannot be null.\r\nParameter name: user", ex.Message);
            }
        }

        public class DeleteAsync
        {
            [Fact]
            public async void Must_delete_a_user_from_the_repository()
            {
                // Arrange
                var unitOfWorkMock = new Mock<IUnitOfWorkAsync>();
                var unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
                unitOfWorkFactory
                    .Setup(m => m.CreateAsync<IUserKey<int>>())
                    .Returns(() => unitOfWorkMock.Object);

                var dummyRepository = new DummyRepository<IUserKey<int>>();
                var userToDelete = new DummyUserObject { Id = 1, UserName = "abc" };
                dummyRepository.InnerList.Add(userToDelete);

                var repositoryFactory = new Mock<IRepositoryFactory>();
                repositoryFactory
                    .Setup(m => m.Create<IUserKey<int>>(It.IsAny<IUnitOfWorkAsync>()))
                    .Returns(dummyRepository);

                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    unitOfWorkFactory.Object,
                    repositoryFactory.Object,
                    new Mock<IDependencyFactory>().Object);

                // Act
                await sut.DeleteAsync(userToDelete);

                // Assert
                Assert.Equal(0, dummyRepository.Count());
                unitOfWorkMock
                    .Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()), Times.Exactly(1));
            }

            [Fact]
            public async void Must_throw_an_exception_if_user_object_is_null()
            {
                // Arrange
                var unitOfWorkMock = new Mock<IUnitOfWorkAsync>();
                var unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
                unitOfWorkFactory
                    .Setup(m => m.CreateAsync<IUserKey<int>>())
                    .Returns(() => unitOfWorkMock.Object);

                var dummyRepository = new DummyRepository<IUserKey<int>>();
                var repositoryFactory = new Mock<IRepositoryFactory>();
                repositoryFactory
                    .Setup(m => m.Create<IUserKey<int>>(It.IsAny<IUnitOfWorkAsync>()))
                    .Returns(dummyRepository);

                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    unitOfWorkFactory.Object,
                    repositoryFactory.Object,
                    new Mock<IDependencyFactory>().Object);

                // Act
                var ex = await Record.ExceptionAsync(() => sut.CreateAsync(null));

                // Assert
                Assert.IsType<ArgumentNullException>(ex);
                Assert.Equal("Value cannot be null.\r\nParameter name: user", ex.Message);
            }
        }

        public class UpdateAsync
        {
            [Fact]
            public async void Must_update_a_user_from_the_repository()
            {
                // Arrange
                var unitOfWorkMock = new Mock<IUnitOfWorkAsync>();
                var unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
                unitOfWorkFactory
                    .Setup(m => m.CreateAsync<IUserKey<int>>())
                    .Returns(() => unitOfWorkMock.Object);

                var dummyRepository = new DummyRepository<IUserKey<int>>();
                var userToUpdate = new DummyUserObject { Id = 1, UserName = "abc" };
                dummyRepository.InnerList.Add(userToUpdate);

                var repositoryFactory = new Mock<IRepositoryFactory>();
                repositoryFactory
                    .Setup(m => m.Create<IUserKey<int>>(It.IsAny<IUnitOfWorkAsync>()))
                    .Returns(dummyRepository);

                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    unitOfWorkFactory.Object,
                    repositoryFactory.Object,
                    new Mock<IDependencyFactory>().Object);

                

                // Act
                await sut.UpdateAsync(new DummyUserObject { Id = 1, UserName = "def" });

                // Assert
                Assert.Equal("def", dummyRepository.First().UserName);
                unitOfWorkMock
                    .Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()), Times.Exactly(1));
            }

            [Fact]
            public async void Must_throw_an_exception_if_user_object_is_null()
            {
                // Arrange
                var unitOfWorkMock = new Mock<IUnitOfWorkAsync>();
                var unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
                unitOfWorkFactory
                    .Setup(m => m.CreateAsync<IUserKey<int>>())
                    .Returns(() => unitOfWorkMock.Object);

                var dummyRepository = new DummyRepository<IUserKey<int>>();
                var repositoryFactory = new Mock<IRepositoryFactory>();
                repositoryFactory
                    .Setup(m => m.Create<IUserKey<int>>(It.IsAny<IUnitOfWorkAsync>()))
                    .Returns(dummyRepository);

                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    unitOfWorkFactory.Object,
                    repositoryFactory.Object,
                    new Mock<IDependencyFactory>().Object);

                // Act
                var ex = await Record.ExceptionAsync(() => sut.CreateAsync(null));

                // Assert
                Assert.IsType<ArgumentNullException>(ex);
                Assert.Equal("Value cannot be null.\r\nParameter name: user", ex.Message);
            }
        }

        public class FindByIdAsync
        {
            [Fact]
            public async void Must_find_the_user_by_the_id_in_the_repository()
            {
                // Arrange
                var unitOfWorkMock = new Mock<IUnitOfWorkAsync>();
                var unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
                unitOfWorkFactory
                    .Setup(m => m.CreateAsync<IUserKey<int>>())
                    .Returns(() => unitOfWorkMock.Object);

                var dummyRepository = new DummyRepository<DummyUserObject>();
                var userToFind = new DummyUserObject { Id = 1, UserName = "abc" };
                dummyRepository.Add(new DummyUserObject { Id = 2, UserName = "def" });
                dummyRepository.Add(userToFind);
                dummyRepository.Add(new DummyUserObject { Id = 3, UserName = "agi" });

                var repositoryFactory = new Mock<IRepositoryFactory>();
                repositoryFactory
                    .Setup(m => m.Create<DummyUserObject>(It.IsAny<IUnitOfWorkAsync>()))
                    .Returns(dummyRepository);

                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    unitOfWorkFactory.Object,
                    repositoryFactory.Object,
                    new Mock<IDependencyFactory>().Object);

                // Act
                var result = await sut.FindByIdAsync(1);

                // Assert
                Assert.Equal(userToFind, result);
            }
        }

        public class FindByUserNameAsync
        {
            [Fact]
            public async void Must_find_the_user_by_the_username()
            {
                // Arrange
                var unitOfWorkMock = new Mock<IUnitOfWorkAsync>();
                var unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
                unitOfWorkFactory
                    .Setup(m => m.CreateAsync<IUserKey<int>>())
                    .Returns(() => unitOfWorkMock.Object);

                var dummyRepository = new DummyRepository<IUserKey<int>>();
                var userToFind = new DummyUserObject { Id = 1, UserName = "abc" };
                dummyRepository.Add(new DummyUserObject { Id = 2, UserName = "def" });
                dummyRepository.Add(userToFind);
                dummyRepository.Add(new DummyUserObject { Id = 3, UserName = "agi" });

                var repositoryFactory = new Mock<IRepositoryFactory>();
                repositoryFactory
                    .Setup(m => m.Create<IUserKey<int>>(It.IsAny<IUnitOfWorkAsync>()))
                    .Returns(() => dummyRepository);

                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    unitOfWorkFactory.Object,
                    repositoryFactory.Object,
                    new Mock<IDependencyFactory>().Object);

                // Act
                var result = await sut.FindByNameAsync("abc");

                // Assert
                Assert.Equal(userToFind, result);
            }

            [Fact]
            public async void Must_throw_an_exception_if_user_object_is_null()
            {
                // Arrange
                var unitOfWorkMock = new Mock<IUnitOfWorkAsync>();
                var unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
                unitOfWorkFactory
                    .Setup(m => m.CreateAsync<IUserKey<int>>())
                    .Returns(() => unitOfWorkMock.Object);

                var dummyRepository = new DummyRepository<DummyUserObject>();
                var repositoryFactory = new Mock<IRepositoryFactory>();
                repositoryFactory
                    .Setup(m => m.Create<DummyUserObject>(It.IsAny<IUnitOfWorkAsync>()))
                    .Returns(dummyRepository);

                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    unitOfWorkFactory.Object,
                    repositoryFactory.Object,
                    new Mock<IDependencyFactory>().Object);

                // Act
                var ex = await Record.ExceptionAsync(() => sut.FindByNameAsync(null));

                // Assert
                Assert.IsType<ArgumentNullException>(ex);
                Assert.Equal("Value cannot be null.\r\nParameter name: userName", ex.Message);
            }
        }

        public class AddLoginAsync
        {
            [Fact]
            public async void Must_create_a_userlogin_on_the_repository()
            {
                // Arrange
                var unitOfWorkMock = new Mock<IUnitOfWorkAsync>();
                var unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
                unitOfWorkFactory
                    .Setup(m => m.CreateAsync<IUserKey<int>>())
                    .Returns(() => unitOfWorkMock.Object);

                var dummyRepository = new DummyRepository<IUserKey<int>>();
                var repositoryFactory = new Mock<IRepositoryFactory>();
                repositoryFactory
                    .Setup(m => m.Create<IUserKey<int>>(It.IsAny<IUnitOfWorkAsync>()))
                    .Returns(dummyRepository);

                var userToAdd = new DummyUserObject { Id = 1, UserName = "abc" };

                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    unitOfWorkFactory.Object,
                    repositoryFactory.Object,
                    new Mock<IDependencyFactory>().Object);

                // Act
                await sut.CreateAsync(userToAdd);

                // Assert
                Assert.Equal(userToAdd, dummyRepository.First());
                unitOfWorkMock
                    .Verify(m => m.CommitAsync(It.IsAny<CancellationToken>()), Times.Exactly(1));
            }

            [Fact]
            public async void Must_throw_an_exception_if_user_object_is_null()
            {
                // Arrange
                var unitOfWorkMock = new Mock<IUnitOfWorkAsync>();
                var unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
                unitOfWorkFactory
                    .Setup(m => m.CreateAsync<IUserKey<int>>())
                    .Returns(() => unitOfWorkMock.Object);

                var dummyRepository = new DummyRepository<IUserKey<int>>();
                var repositoryFactory = new Mock<IRepositoryFactory>();
                repositoryFactory
                    .Setup(m => m.Create<IUserKey<int>>(It.IsAny<IUnitOfWorkAsync>()))
                    .Returns(dummyRepository);

                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    unitOfWorkFactory.Object,
                    repositoryFactory.Object,
                    new Mock<IDependencyFactory>().Object);

                // Act
                var ex = await Record.ExceptionAsync(() => sut.CreateAsync(null));

                // Assert
                Assert.IsType<ArgumentNullException>(ex);
                Assert.Equal("Value cannot be null.\r\nParameter name: user", ex.Message);
            }

        }

    }
}
