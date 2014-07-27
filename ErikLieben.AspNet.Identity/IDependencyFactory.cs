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
        /// <typeparam name="T">Type of object wanted</typeparam>
        /// <param name="args">The constructor arguments to create the object.</param>
        /// <returns>The object it requires.</returns>
        T CreateObject<T>(params object[] args);
    }
}
