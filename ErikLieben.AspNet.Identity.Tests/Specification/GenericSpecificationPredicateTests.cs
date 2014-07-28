namespace ErikLieben.AspNet.Identity.Tests.Specification
{
    using Data.Repository;
    using Xunit;

    public abstract class GenericSpecificationPredicateTests<TSpec, TProp, TKey>
            where TSpec : class, ISpecification<TProp>
    {
        protected abstract TKey GoodValue { get; }

        protected abstract TKey BadValue { get; }

        protected abstract TProp DummyObject { get; }

        [Fact]
        public void Must_return_true_if_predicate_equals_given_value()
        {
            // Arrange
            var sut = typeof(TSpec).GetInstance<TKey>(this.GoodValue) as TSpec;

            // Act
            var result = sut.Predicate.Compile()(this.DummyObject);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Must_return_false_if_predicate_does_not_equals_given_value()
        {
            // Arrange
            var sut = typeof(TSpec).GetInstance<TKey>(this.BadValue) as TSpec;

            // Act
            var result = sut.Predicate.Compile()(this.DummyObject);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Must_return_true_if_satisfied_by_input()
        {
            // Arrange
            var sut = typeof(TSpec).GetInstance<TKey>(this.GoodValue) as TSpec;

            // Act
            var result = sut.IsSatisfiedBy(this.DummyObject);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Must_return_false_if_not_satisfied_by_input()
        {
            // Arrange
            var sut = typeof(TSpec).GetInstance<TKey>(this.BadValue) as TSpec;

            // Act
            var result = sut.IsSatisfiedBy(this.DummyObject);

            // Assert
            Assert.False(result);
        }

    }
}
