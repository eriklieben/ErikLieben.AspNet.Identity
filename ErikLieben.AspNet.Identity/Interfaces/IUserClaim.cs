// ***********************************************************************
// <copyright file="IUserClaim.cs" company="Erik Lieben">
//     Copyright (c) Erik Lieben. All rights reserved.
// </copyright>
// ***********************************************************************
namespace ErikLieben.AspNet.Identity.Interfaces
{
    /// <summary>
    /// Interface IUserClaim
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    public interface IUserClaim<out TKey> : IUserKey<TKey>
    {
        /// <summary>
        /// Gets the issuer.
        /// </summary>
        /// <value>The issuer.</value>
        string Issuer { get; }

        /// <summary>
        /// Gets the original issuer.
        /// </summary>
        /// <value>The original issuer.</value>
        string OriginalIssuer { get; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        string Type { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        string Value { get; }

        /// <summary>
        /// Gets the type of the value.
        /// </summary>
        /// <value>The type of the value.</value>
        string ValueType { get; }
    }
}
