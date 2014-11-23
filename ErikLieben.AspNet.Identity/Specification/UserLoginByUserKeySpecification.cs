// ***********************************************************************
// <copyright file="UserLoginByUserKeySpecification.cs" company="Erik Lieben">
//     Copyright (c) Erik Lieben. All rights reserved.
// </copyright>
// ***********************************************************************
namespace ErikLieben.AspNet.Identity.Specification
{
    using Interfaces;

    /// <summary>
    /// Specification for user login by user key.
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    public sealed class UserLoginByUserKeySpecification<TKey> : UserByUserIdSpecification<TKey, IUserLogin<TKey>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserLoginByUserKeySpecification{TKey}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public UserLoginByUserKeySpecification(TKey id) : base(id)
        {
        }
    }
}
