// ***********************************************************************
// <copyright file="IUserEmailConfirmationStatus.cs" company="Erik Lieben">
//     Copyright (c) Erik Lieben. All rights reserved.
// </copyright>
// ***********************************************************************
namespace ErikLieben.AspNet.Identity.Interfaces
{
    /// <summary>
    /// Interface IUserEmailConfirmationStatus
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    public interface IUserEmailConfirmationStatus<out TKey> : IUserWithEmail<TKey>
    {
        /// <summary>
        /// Gets or sets a value indicating whether [email confirmed].
        /// </summary>
        /// <value><c>true</c> if [email confirmed]; otherwise, <c>false</c>.</value>
        bool EmailConfirmed { get; set; }
    }
}
