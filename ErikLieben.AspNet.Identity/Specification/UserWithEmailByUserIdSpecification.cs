// ***********************************************************************
// <copyright file="UserWithEmailByUserIdSpecification.cs" company="Erik Lieben">
//     Copyright (c) Erik Lieben. All rights reserved.
// </copyright>
// ***********************************************************************
namespace ErikLieben.AspNet.Identity.Specification
{
    using Interfaces;

    /// <summary>
    /// User by userId specification
    /// </summary>
    /// <typeparam name="TKey">The type of the key of the user object.</typeparam>
    /// <typeparam name="TUser">The type of the user object.</typeparam>
    /// <summary>
    /// Class UserClaimByUserKeySpecification. This class cannot be inherited.
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    public sealed class UserWithEmailByUserIdSpecification<TKey> : UserByUserIdSpecification<TKey, IUserWithEmail<TKey>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaimByUserKeySpecification{TKey}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public UserWithEmailByUserIdSpecification(TKey id) : base(id)
        {
        }
    }
}