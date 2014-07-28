// ***********************************************************************
// <copyright file="UserEmailConfirmationStatusSpecification.cs" company="Erik Lieben">
//     Copyright (c) Erik Lieben. All rights reserved.
// </copyright>
// ***********************************************************************
namespace ErikLieben.AspNet.Identity.Specification
{
    using System;
    using System.Linq.Expressions;

    using ErikLieben.AspNet.Identity.Interfaces;
    using ErikLieben.Data.Repository;

    using Microsoft.AspNet.Identity;

    /// <summary>
    /// Class UserEmailConfirmationStatusSpecification.
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    /// <typeparam name="TUser">The type of the t user.</typeparam>
    public class UserEmailConfirmationStatusSpecification<TKey, TUser> : Specification<IUserEmailConfirmationStatus<TKey>> 
        where TUser : class, IUser<TKey>
    {
        /// <summary>
        /// The identifier
        /// </summary>
        private readonly TKey id;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserEmailConfirmationStatusSpecification{TKey, TUser}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public UserEmailConfirmationStatusSpecification(TKey id)
        {
            this.id = id;
        }

        /// <summary>
        /// Gets the predicate.
        /// </summary>
        /// <value>The predicate.</value>
        public override Expression<Func<IUserEmailConfirmationStatus<TKey>, bool>> Predicate
        {
            get
            {
                return i => i.Id.Equals(this.id);
            }
        }
    }
}