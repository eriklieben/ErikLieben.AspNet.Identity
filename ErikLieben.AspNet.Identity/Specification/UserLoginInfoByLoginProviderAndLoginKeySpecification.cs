// ***********************************************************************
// <copyright file="UserLoginInfoByLoginProviderAndLoginKeySpecification.cs" company="Erik Lieben">
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
    /// Class UserLoginInfoByLoginProviderAndLoginKeySpecification. This class cannot be inherited.
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    public sealed class UserLoginInfoByLoginProviderAndLoginKeySpecification<TKey> : Specification<IUserLogin<TKey>>
    {
        /// <summary>
        /// The provider key
        /// </summary>
        private readonly string providerKey;

        /// <summary>
        /// The login key
        /// </summary>
        private readonly string loginKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserLoginInfoByLoginProviderAndLoginKeySpecification{TKey}"/> class.
        /// </summary>
        /// <param name="providerKey">The provider key.</param>
        /// <param name="loginKey">The login key.</param>
        public UserLoginInfoByLoginProviderAndLoginKeySpecification(string providerKey, string loginKey)
        {
            this.providerKey = providerKey;
            this.loginKey = loginKey;
        }

        /// <summary>
        /// Gets the predicate.
        /// </summary>
        /// <value>The predicate.</value>
        public override Expression<Func<IUserLogin<TKey>, bool>> Predicate
        {
            get
            {
                return i => i.LoginProvider.Equals(this.loginKey) && i.ProviderKey.Equals(this.providerKey);
            }
        }
    }
}
