namespace ErikLieben.AspNet.Identity.Tests.Specification
{
    using System;

    using ErikLieben.AspNet.Identity.Interfaces;

    using Identity.Specification;
    using Xunit;

    public class UserByUserNameSpecificationTests
    {
        public class Predicate
            : GenericSpecificationPredicateTests<UserByUserNameSpecification<int>, IUserKey<int>, string>
        {
            protected override string BadValue
            {
                get
                {
                    return "bad name";
                }
            }

            protected override IUserKey<int> DummyObject
            {
                get
                {
                    return new DummyUserObject { UserName = this.GoodValue };
                }
            }

            protected override string GoodValue
            {
                get
                {
                    return "good name";
                }
            }
        }

    }
}
