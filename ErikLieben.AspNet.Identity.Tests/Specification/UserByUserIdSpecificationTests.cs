namespace ErikLieben.AspNet.Identity.Tests.Specification
{
    using ErikLieben.AspNet.Identity.Interfaces;

    using Identity.Specification;

    public class UserByUserIdSpecificationTests
    {
        public class Predicate
            : GenericSpecificationPredicateTests<UserByUserIdSpecification<int, IUserKey<int>>, IUserKey<int>, int>
        {
            protected override int BadValue
            {
                get
                {
                    return 2;
                }
            }

            protected override IUserKey<int> DummyObject
            {
                get
                {
                    return new DummyUserObject { Id = this.GoodValue };
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
}
