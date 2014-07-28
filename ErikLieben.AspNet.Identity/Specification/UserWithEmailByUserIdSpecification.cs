// ***********************************************************************
// <copyright file="UserWithEmailByUserIdSpecification.cs" company="Erik Lieben">
//     Copyright (c) Erik Lieben. All rights reserved.
// </copyright>
// ***********************************************************************
namespace ErikLieben.AspNet.Identity.Specification
{
    using System;
    using System.Linq.Expressions;
    using Data.Repository;
    using Microsoft.AspNet.Identity;

    /// <summary>
    /// User by userId specification
    /// </summary>
    /// <typeparam name="TKey">The type of the key of the user object.</typeparam>
    /// <typeparam name="TUser">The type of the user object.</typeparam>
    public sealed class UserWithEmailByUserIdSpecification<TKey, TUser> : Specification<TUser>
        where TUser : class, IUser<TKey>
    {
        /// <summary>
        /// The key to search for
        /// </summary>
        private readonly TKey key;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserWithEmailByUserIdSpecification{TKey, TUser}"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public UserWithEmailByUserIdSpecification(TKey key)
        {
            this.key = key;
        }

        /// <summary>
        /// Gets the predicate with the search function.
        /// </summary>
        /// <value>The predicate.</value>
        public override Expression<Func<TUser, bool>> Predicate
        {
            get
            {
                return i => i.Id.Equals(this.key);
            }
        }
    }
}