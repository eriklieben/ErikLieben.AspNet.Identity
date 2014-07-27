// ***********************************************************************
// <copyright file="IUserKey.cs" company="Erik Lieben">
//     Copyright (c) Erik Lieben. All rights reserved.
// </copyright>
// ***********************************************************************
namespace ErikLieben.AspNet.Identity
{
    /// <summary>
    /// Interface IUserKey
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    public interface IUserKey<out TKey>
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        TKey UserId { get; }
    }
}
