using Xunit;

namespace ErikLieben.AspNet.Identity.Tests
{
    using Data.Factory;
    using Data.Repository;

    using ErikLieben.AspNet.Identity.Specification;
    using ErikLieben.Data.Projection;
    using ErikLieben.Data.Mapping;

    using FakeItEasy;
    using FakeItEasy.ExtensionSyntax.Full;

    using Identity;
    using Interfaces;

    using Microsoft.AspNet.Identity;

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
                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    A.Fake<IUnitOfWorkFactory>(),
                    A.Fake<IRepositoryFactory>(),
                    A.Fake<IDependencyFactory>());

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
                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    A.Fake<IUnitOfWorkFactory>(),
                    A.Fake<IRepositoryFactory>(),
                    A.Fake<IDependencyFactory>());

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
                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    A.Fake<IUnitOfWorkFactory>(),
                    A.Fake<IRepositoryFactory>(),
                    A.Fake<IDependencyFactory>());

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
                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    A.Fake<IUnitOfWorkFactory>(),
                    A.Fake<IRepositoryFactory>(),
                    A.Fake<IDependencyFactory>());

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
                var unitOfWorkFactory = A.Fake<IUnitOfWorkFactory>();
                var unitOfWork = A.Fake<IUnitOfWorkAsync>();
                A.CallTo(() => unitOfWorkFactory.CreateAsync<IUserLogin<int>>()).Returns(unitOfWork);

                var dummyRepository = new DummyRepository<IUserLogin<int>>();

                var repositoryFactory = A.Fake<IRepositoryFactory>();
                A.CallTo(() => repositoryFactory.Create<IUserLogin<int>>(A<IUnitOfWorkAsync>.Ignored)).Returns(dummyRepository);

                var dependencyFactory = A.Fake<IDependencyFactory>();
                A.CallTo(() => dependencyFactory.CreateObject<IUserLogin<int>>(A<Object[]>._))
                    .ReturnsLazily((object[] @params) => new DummyUserLogin
                                                            {
                                                                Id = int.Parse(@params[0].ToString()),
                                                                LoginProvider = @params[1].ToString(),
                                                                ProviderKey = @params[2].ToString()
                                                            });

                var userToAdd = new DummyUserLogin { LoginProvider = "a", ProviderKey = "b", Id = 5 };

                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    unitOfWorkFactory,
                    repositoryFactory,
                    dependencyFactory);

                // Act
                await sut.AddLoginAsync(userToAdd, new UserLoginInfo("a","b"));

                // Assert
                var item = dummyRepository.First();
                Assert.Equal(5, item.Id);
                Assert.Equal("a", item.LoginProvider);
                Assert.Equal("b", item.ProviderKey);

                

                A.CallTo(() => unitOfWork.CommitAsync(A<CancellationToken>._)).MustHaveHappened();
            }

            [Fact]
            public async void Must_throw_an_exception_if_user_object_is_null()
            {
                // Arrange
                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    A.Fake<IUnitOfWorkFactory>(),
                    A.Fake<IRepositoryFactory>(),
                    A.Fake<IDependencyFactory>());

                // Act
                var ex = await Record.ExceptionAsync(() => sut.AddLoginAsync(null, null));

                // Assert
                Assert.IsType<ArgumentNullException>(ex);
                Assert.Equal("Value cannot be null.\r\nParameter name: user", ex.Message);
            }

            [Fact]
            public async void Must_throw_an_exception_if_login_object_is_null()
            {
                // Arrange
                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    A.Fake<IUnitOfWorkFactory>(),
                    A.Fake<IRepositoryFactory>(),
                    A.Fake<IDependencyFactory>());

                // Act
                var ex = await Record.ExceptionAsync(() => sut.AddLoginAsync(new DummyUserObject(), null));

                // Assert
                Assert.IsType<ArgumentNullException>(ex);
                Assert.Equal("Value cannot be null.\r\nParameter name: login", ex.Message);
            }

        }

        public class RemoveLoginAsync
        {
            [Fact]
            public async void Must_remove_a_userlogin_on_the_repository()
            {
                // Arrange
                var unitOfWorkFactory = A.Fake<IUnitOfWorkFactory>();
                var unitOfWork = A.Fake<IUnitOfWorkAsync>();
                A.CallTo(() => unitOfWorkFactory.CreateAsync<IUserLogin<int>>()).Returns(unitOfWork);

                var dummyRepository = new DummyRepository<IUserLogin<int>>();

                var repositoryFactory = A.Fake<IRepositoryFactory>();
                A.CallTo(() => repositoryFactory.Create<IUserLogin<int>>(A<IUnitOfWorkAsync>.Ignored)).Returns(dummyRepository);

                var dependencyFactory = A.Fake<IDependencyFactory>();
                A.CallTo(() => dependencyFactory.CreateObject<IUserLogin<int>>(A<Object[]>._))
                    .ReturnsLazily((object[] @params) => new DummyUserLogin
                {
                    Id = int.Parse(@params[0].ToString()),
                    LoginProvider = @params[1].ToString(),
                    ProviderKey = @params[2].ToString()
                });

                var userToAdd = new DummyUserLogin { LoginProvider = "a", ProviderKey = "b", Id = 5 };
                dummyRepository.Add(userToAdd);

                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    unitOfWorkFactory,
                    repositoryFactory,
                    dependencyFactory);
              
                // Act
                await sut.RemoveLoginAsync(userToAdd, new UserLoginInfo("a", "b"));

                // Assert
                Assert.Equal(0, dummyRepository.InnerList.Count);
                A.CallTo(() => unitOfWork.CommitAsync(A<CancellationToken>._)).MustHaveHappened();
            }

            [Fact]
            public async void Must_throw_an_exception_if_user_object_is_null()
            {
                // Arrange
                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    A.Fake<IUnitOfWorkFactory>(),
                    A.Fake<IRepositoryFactory>(),
                    A.Fake<IDependencyFactory>());

                // Act
                var ex = await Record.ExceptionAsync(() => sut.RemoveLoginAsync(null, null));

                // Assert
                Assert.IsType<ArgumentNullException>(ex);
                Assert.Equal("Value cannot be null.\r\nParameter name: user", ex.Message);
            }

            [Fact]
            public async void Must_throw_an_exception_if_login_object_is_null()
            {
                // Arrange
                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    A.Fake<IUnitOfWorkFactory>(),
                    A.Fake<IRepositoryFactory>(),
                    A.Fake<IDependencyFactory>());

                // Act
                var ex = await Record.ExceptionAsync(() => sut.RemoveLoginAsync(new DummyUserObject(), null));

                // Assert
                Assert.IsType<ArgumentNullException>(ex);
                Assert.Equal("Value cannot be null.\r\nParameter name: login", ex.Message);
            }
        }

        public class GetLoginsAsync
        {
            [Fact]
            public async void Must_get_all_the_userlogin()
            {
                // Arrange
                var unitOfWorkFactory = A.Fake<IUnitOfWorkFactory>();
                var unitOfWork = A.Fake<IUnitOfWorkAsync>();
                A.CallTo(() => unitOfWorkFactory.CreateAsync<IUserLogin<int>>()).Returns(unitOfWork);

                var dummyRepository = new DummyRepository<IUserLogin<int>>();

                var repositoryFactory = A.Fake<IRepositoryFactory>();
                A.CallTo(() => repositoryFactory.Create<IUserLogin<int>>(A<IUnitOfWorkAsync>.Ignored)).Returns(dummyRepository);

                var dependencyFactory = A.Fake<IDependencyFactory>();
                A.CallTo(() => dependencyFactory.CreateObject<IUserLogin<int>>(A<Object[]>._))
                    .ReturnsLazily((object[] @params) => new DummyUserLogin
                {
                    Id = int.Parse(@params[0].ToString()),
                    LoginProvider = @params[1].ToString(),
                    ProviderKey = @params[2].ToString()
                });

                var userToAdd = new DummyUserLogin { LoginProvider = "a", ProviderKey = "b", Id = 5 };
                dummyRepository.Add(new DummyUserLogin { LoginProvider = "c", ProviderKey = "d", Id = 7 });
                dummyRepository.Add(userToAdd);
                dummyRepository.Add(new DummyUserLogin { LoginProvider = "c", ProviderKey = "d", Id = 5 });
                dummyRepository.Add(new DummyUserLogin { LoginProvider = "c", ProviderKey = "d", Id = 6 });

                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    unitOfWorkFactory,
                    repositoryFactory,
                    dependencyFactory);

                // Act
                var result = await sut.GetLoginsAsync(userToAdd);

                // Assert
                Assert.Equal(2, result.Count);
            }

            [Fact]
            public async void Must_throw_an_exception_if_user_object_is_null()
            {
                // Arrange
                var sut = new RepositoryIdentityStore<DummyUserObject, int>(
                    A.Fake<IUnitOfWorkFactory>(),
                    A.Fake<IRepositoryFactory>(),
                    A.Fake<IDependencyFactory>());

                // Act
                var ex = await Record.ExceptionAsync(() => sut.RemoveLoginAsync(null, null));

                // Assert
                Assert.IsType<ArgumentNullException>(ex);
                Assert.Equal("Value cannot be null.\r\nParameter name: user", ex.Message);
            }
        }
    }
}
