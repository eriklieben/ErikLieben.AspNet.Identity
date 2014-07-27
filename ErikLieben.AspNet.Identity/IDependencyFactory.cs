// ***********************************************************************
// <copyright file="IDependencyFactory.cs" company="Erik Lieben">
//     Copyright (c) Erik Lieben. All rights reserved.
// </copyright>
// ***********************************************************************
namespace ErikLieben.AspNet.Identity
{
    /// <summary>
    /// Interface IDependencyFactory
    /// </summary>
    public interface IDependencyFactory
    {
        /// <summary>
        /// Creates the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args">The arguments.</param>
        /// <returns>T.</returns>
        T CreateObject<T>(params object[] args);
    }
}
