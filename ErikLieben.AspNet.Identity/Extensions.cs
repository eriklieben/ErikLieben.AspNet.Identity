// ***********************************************************************
// <copyright file="Extensions.cs" company="Erik Lieben">
//     Copyright (c) Erik Lieben. All rights reserved.
// </copyright>
// ***********************************************************************
namespace ErikLieben.AspNet.Identity
{
    using System.Collections.Generic;

    // TODO: Dummy Implement it..
    /// <summary>
    /// Class Extensions to be moved outside of this library.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Maps all to.
        /// </summary>
        /// <typeparam name="TTarget">The type of the t target.</typeparam>
        /// <typeparam name="TSource">The type of the t source.</typeparam>
        /// <param name="items">The items.</param>
        /// <returns>IEnumerable of mapped items</returns>
        public static IEnumerable<TTarget> MapAllTo<TTarget, TSource>(this IEnumerable<TSource> items)
        {
            return default(IEnumerable<TTarget>);
        }
    }
}
