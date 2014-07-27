// ***********************************************************************
// <copyright file="IUserLogin.cs" company="Erik Lieben">
//     Copyright (c) Erik Lieben. All rights reserved.
// </copyright>
// ***********************************************************************
namespace ErikLieben.AspNet.Identity
{
    /// <summary>
    /// Interface IUserLogin
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    public interface IUserLogin<out TKey> : IUserKey<TKey>
    {
        /// <summary>
        /// Gets the login provider.
        /// </summary>
        /// <value>The login provider.</value>
        string LoginProvider { get; }
        
        /// <summary>
        /// Gets the provider key.
        /// </summary>
        /// <value>The provider key.</value>
        string ProviderKey { get; }
    }
}
