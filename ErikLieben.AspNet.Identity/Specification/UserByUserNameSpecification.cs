// ***********************************************************************
// <copyright file="UserByUserNameSpecification.cs" company="Erik Lieben">
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
    /// User by userName specification.
    /// </summary>
    /// <typeparam name="TKey">The type of the key of the user object.</typeparam>
    /// <typeparam name="TUser">The type of the user object.</typeparam>
    public sealed class UserByUserNameSpecification<TKey, TUser> : Specification<TUser>
        where TUser : class, IUser<TKey>
    {
        /// <summary>
        /// The username
        /// </summary>
        private readonly string userName;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserByUserNameSpecification{TKey, TUser}"/> class.
        /// </summary>
        /// <param name="userName">The username.</param>
        public UserByUserNameSpecification(string userName)
        {
            this.userName = userName;
        }

        /// <summary>
        /// Gets the predicate.
        /// </summary>
        /// <value>The predicate.</value>
        public override Expression<Func<TUser, bool>> Predicate
        {
            get
            {
                return i => i.UserName.Equals(this.userName);
            }
        }
    }
}
