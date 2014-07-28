namespace ErikLieben.AspNet.Identity.Tests.Specification
{
    using Identity.Specification;

    using Xunit;

    public class UserLoginInfoByLoginProviderAndLoginKeySpecificationTests
    {
        public class Predicate
        {
            [Fact]
            public void Must_return_true_if_predicate_equals_given_value()
            {
                // Arrange
                var sut = new UserLoginInfoByLoginProviderAndLoginKeySpecification<int>("provider", "loginkey");

                // Act
                var result = sut.Predicate.Compile()(new DummyUserLogin { LoginProvider = "provider", ProviderKey = "loginkey" });

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void Must_return_false_if_predicate_does_not_equals_given_value()
            {
                // Arrange
                var sut = new UserLoginInfoByLoginProviderAndLoginKeySpecification<int>("provider2", "loginkey2");

                // Act
                var result = sut.Predicate.Compile()(new DummyUserLogin { LoginProvider = "provider", ProviderKey = "loginkey" });

                // Assert
                Assert.False(result);
            }

            [Fact]
            public void Must_return_true_if_satisfied_by_input()
            {
                // Arrange
                var sut = new UserLoginInfoByLoginProviderAndLoginKeySpecification<int>("provider", "loginkey");

                // Act
                var result = sut.IsSatisfiedBy(new DummyUserLogin { LoginProvider = "provider", ProviderKey = "loginkey" });

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void Must_return_false_if_not_satisfied_by_input()
            {
                // Arrange
                var sut = new UserLoginInfoByLoginProviderAndLoginKeySpecification<int>("provider2", "loginkey2");

                // Act
                var result = sut.IsSatisfiedBy(new DummyUserLogin { LoginProvider = "provider", ProviderKey = "loginkey" });

                // Assert
                Assert.False(result);
            }
        }
    }
}
