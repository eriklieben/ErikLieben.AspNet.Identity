namespace ErikLieben.AspNet.Identity.Tests.Specification
{
    using Identity.Specification;
    using Interfaces;

    public class UserWithEmailByEmailSpecificationTests
        : GenericSpecificationPredicateTests<UserWithEmailByEmailSpecification<int, IUserWithEmail<int>>, IUserWithEmail<int>, string>
    {
        protected override string BadValue
        {
            get
            {
                return "bad@email.com";
            }
        }

        protected override IUserWithEmail<int> DummyObject
        {
            get
            {
                return new DummyUserWithEmailLogin() { Email = this.GoodValue };
            }
        }

        protected override string GoodValue
        {
            get
            {
                return "good@email.com";
            }
        }
    }
}
