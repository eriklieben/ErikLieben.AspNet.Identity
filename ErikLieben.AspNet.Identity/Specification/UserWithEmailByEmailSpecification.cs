// ***********************************************************************
// <copyright file="UserWithEmailByEmailSpecification.cs" company="Erik Lieben">
//     Copyright (c) Erik Lieben. All rights reserved.
// </copyright>
// ***********************************************************************
namespace ErikLieben.AspNet.Identity.Specification
{
    using System;
    using System.Linq.Expressions;
    using Data.Repository;
    using Interfaces;


    /// <summary>
    /// User by userId specification
    /// </summary>
    /// <typeparam name="TKey">The type of the key of the user object.</typeparam>
    /// <typeparam name="TUser">The type of the user object.</typeparam>
    public sealed class UserWithEmailByEmailSpecification<TKey, TUser> : Specification<TUser>
        where TUser : class, IUserWithEmail<TKey>
    {

        /// <summary>
        /// The email
        /// </summary>
        private readonly string email;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserWithEmailByEmailSpecification{TKey, TUser}"/> class.
        /// </summary>
        /// <param name="email">The email.</param>
        public UserWithEmailByEmailSpecification(string email)
        {
            this.email = email;
        }

        /// <summary>
        /// Gets the predicate with the search function.
        /// </summary>
        /// <value>The predicate.</value>
        public override Expression<Func<TUser, bool>> Predicate
        {
            get
            {
                return i => i.Email.Equals(this.email);
            }
        }
    }
}