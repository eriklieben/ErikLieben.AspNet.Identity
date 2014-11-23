namespace ErikLieben.AspNet.Identity.Tests.Specification
{
    using Interfaces;
    using Identity.Specification;

    public class UserLoginByUserKeySpecificationTests
        : GenericSpecificationPredicateTests<UserLoginByUserKeySpecification<int>, IUserLogin<int>, int>
    {
        protected override int BadValue
        {
            get
            {
                return 2;
            }
        }

        protected override IUserLogin<int> DummyObject
        {
            get
            {
                return new DummyUserLogin { Id = this.GoodValue };
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
