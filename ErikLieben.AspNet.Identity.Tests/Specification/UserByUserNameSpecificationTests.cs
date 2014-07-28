namespace ErikLieben.AspNet.Identity.Tests.Specification
{
    using System;
    using Identity.Specification;
    using Xunit;

    public class UserByUserNameSpecificationTests
    {
        public class Predicate
            : GenericSpecificationPredicateTests<UserByUserNameSpecification<int, DummyUserObject>, DummyUserObject, string>
        {
            protected override string BadValue
            {
                get
                {
                    return "bad name";
                }
            }

            protected override DummyUserObject DummyObject
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
