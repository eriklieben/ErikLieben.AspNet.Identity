// ***********************************************************************
// <copyright file="UserByUserIdSpecification.cs" company="Erik Lieben">
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
    /// Class UserByUserBaseIdSpecification.
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    /// <typeparam name="TType">The type of the t type.</typeparam>
    public class UserByUserIdSpecification<TKey, TType> : Specification<TType>
        where TType : IUserKey<TKey>
    {
        /// <summary>
        /// The identifier
        /// </summary>
        private readonly TKey id;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserByUserBaseIdSpecification{TKey, TType}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public UserByUserIdSpecification(TKey id)
        {
            this.id = id;
        }

        /// <summary>
        /// Gets the predicate.
        /// </summary>
        /// <value>The predicate.</value>
        public override Expression<Func<TType, bool>> Predicate
        {
            get
            {
                return i => i.Id.Equals(this.id);
            }
        }
    }
}
