namespace ErikLieben.AspNet.Identity.Tests.Specification
{
    using Identity.Specification;
    using Interfaces;

    public class UserWithEmailByUserIdSpecificationTests
        : GenericSpecificationPredicateTests<UserWithEmailByUserIdSpecification<int>, IUserWithEmail<int>, int>
    {
        protected override int BadValue
        {
            get
            {
                return 2;
            }
        }

        protected override IUserWithEmail<int> DummyObject
        {
            get
            {
                return new DummyUserWithEmailLogin { Id = GoodValue };
            }
        }

        protected override int GoodValue
        {
            get
            {
                return 1;
            }
        }
    }
}
