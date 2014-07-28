// ***********************************************************************
// <copyright file="IUserWithEmail.cs" company="Erik Lieben">
//     Copyright (c) Erik Lieben. All rights reserved.
// </copyright>
// ***********************************************************************
namespace ErikLieben.AspNet.Identity.Interfaces
{
    using Microsoft.AspNet.Identity;

    /// <summary>
    /// Interface IUserWithEmail
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    public interface IUserWithEmail<out TKey> : IUser<TKey>
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        string Email { get; set; }
    }
}
