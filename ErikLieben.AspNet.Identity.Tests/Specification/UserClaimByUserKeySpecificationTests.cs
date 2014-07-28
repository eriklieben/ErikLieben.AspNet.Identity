namespace ErikLieben.AspNet.Identity.Tests.Specification
{
    using Identity.Specification;
    using Interfaces;

    public class UserClaimByUserKeySpecificationTests
        : GenericSpecificationPredicateTests<UserClaimByUserKeySpecification<int>, IUserClaim<int>, int>
    {
        protected override int BadValue
        {
            get
            {
                return 1;
            }
        }

        protected override IUserClaim<int> DummyObject
        {
            get
            {
                return new DummyUserClaimObject { Id = this.GoodValue };
            }
        }

        protected override int GoodValue
        {
            get
            {
                return 2;
            }
        }
    }
}
