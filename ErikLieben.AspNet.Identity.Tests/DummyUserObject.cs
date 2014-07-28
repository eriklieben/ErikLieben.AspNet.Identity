namespace ErikLieben.AspNet.Identity.Tests
{
    using System;
    using Interfaces;

    public class DummyUserObject : IUserKey<int>
    {
        public int Id { get; set; }

        public string UserName { get; set; }
    }

    public class DummyUserClaimObject : DummyUserObject, IUserClaim<int>
    {
        public string Issuer { get; set; }

        public string OriginalIssuer { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public string ValueType { get; set; }
    }

    public class DummyUserWithEmailLogin : DummyUserObject, IUserWithEmail<int>
    {
        public string Email { get; set; }
    }

    public class DummyUserEmailConfirmationStatus : DummyUserWithEmailLogin, IUserEmailConfirmationStatus<int>
    {
        public bool EmailConfirmed { get; set; }
    }

    public class DummyUserLogin : DummyUserObject, IUserLogin<int>
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }

}
