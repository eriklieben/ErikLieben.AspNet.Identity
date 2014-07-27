namespace ErikLieben.AspNet.Identity.Specification
{
    public sealed class UserLoginByUserKeySpecification<TKey> : UserKeyByUserIdSpecification<TKey, IUserLogin<TKey>>
    {
        public UserLoginByUserKeySpecification(TKey id) : base(id)
        {

        }
    }
}
