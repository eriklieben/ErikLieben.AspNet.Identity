using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErikLieben.AspNet.Identity.Tests.Specification
{
    using ErikLieben.AspNet.Identity.Interfaces;
    using ErikLieben.AspNet.Identity.Specification;

    // 

    public class UserEmailConfirmationStatusSpecificationTests
        : GenericSpecificationPredicateTests<UserEmailConfirmationStatusByIdSpecification<int>, IUserEmailConfirmationStatus<int>, int>
    {
        protected override int BadValue
        {
            get
            {
                return 1;
            }
        }

        protected override IUserEmailConfirmationStatus<int> DummyObject
        {
            get
            {
                return new DummyUserEmailConfirmationStatus() { Id = this.GoodValue };
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
