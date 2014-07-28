// ***********************************************************************
// <copyright file="UserClaimByUserKeySpecification.cs" company="Erik Lieben">
//     Copyright (c) Erik Lieben. All rights reserved.
// </copyright>
// ***********************************************************************
namespace ErikLieben.AspNet.Identity.Specification
{
    using Interfaces;

    /// <summary>
    /// Class UserClaimByUserKeySpecification. This class cannot be inherited.
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    public sealed class UserClaimByUserKeySpecification<TKey> : UserByUserIdSpecification<TKey, IUserClaim<TKey>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimByUserKeySpecification{TKey}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public UserClaimByUserKeySpecification(TKey id) : base(id)
        {
        }
    }
}
