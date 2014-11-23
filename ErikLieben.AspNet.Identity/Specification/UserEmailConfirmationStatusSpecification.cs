// ***********************************************************************
// <copyright file="UserEmailConfirmationStatusByIdSpecification.cs" company="Erik Lieben">
//     Copyright (c) Erik Lieben. All rights reserved.
// </copyright>
// ***********************************************************************
namespace ErikLieben.AspNet.Identity.Specification
{
    using Interfaces;

    /// <summary>
    /// Class UserEmailConfirmationStatusSpecification.
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    public sealed class UserEmailConfirmationStatusByIdSpecification<TKey> : UserByUserIdSpecification<TKey, IUserEmailConfirmationStatus<TKey>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserLoginByUserKeySpecification{TKey}"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public UserEmailConfirmationStatusByIdSpecification(TKey id) : base(id)
        {
        }
    }
}