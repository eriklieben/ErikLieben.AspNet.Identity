// ***********************************************************************
// <copyright file="RepositoryIdentityStore.cs" company="Erik Lieben">
//     Copyright (c) Erik Lieben. All rights reserved.
// </copyright>
// ***********************************************************************
namespace ErikLieben.AspNet.Identity
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Data.Factory;
    using Microsoft.AspNet.Identity;
    using Specification;

    /// <summary>
    /// Class RepositoryIdentityStore.
    /// </summary>
    /// <typeparam name="TUser">The type of the user object.</typeparam>
    /// <typeparam name="TKey">The type of the key of the user object.</typeparam>
    public class RepositoryIdentityStore<TUser, TKey> : IUserStore<TUser, TKey>
        where TUser : class, IUser<TKey>
    {
        /// <summary>
        /// The unit of work factory
        /// </summary>
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        
        /// <summary>
        /// The repository factory
        /// </summary>
        private readonly IRepositoryFactory repositoryFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryIdentityStore{TUser, TKey}"/> class.
        /// </summary>
        /// <param name="unitOfWorkFactory">The unit of work factory.</param>
        /// <param name="repositoryFactory">The repository factory.</param>
        public RepositoryIdentityStore(IUnitOfWorkFactory unitOfWorkFactory, IRepositoryFactory repositoryFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// Create the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task for the creation process of the account.</returns>
        public async Task CreateAsync(TUser user)
        {
            Guard
                .With<ArgumentNullException>
                .Against(user == null)
                .Say("user");

            using (var uow = this.unitOfWorkFactory.CreateAsync<TUser>())
            {
                var repository = this.repositoryFactory.Create<TUser>(uow);
                repository.Add(user);
                await uow.CommitAsync(new CancellationToken());
            }
        }

        /// <summary>
        /// Update the user asynchronous.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <returns>Task for the update process of the user account.</returns>
        public async Task UpdateAsync(TUser user)
        {
            Guard
                .With<ArgumentNullException>
                .Against(user == null)
                .Say("user");

            using (var uow = this.unitOfWorkFactory.CreateAsync<TUser>())
            {
                var repository = this.repositoryFactory.Create<TUser>(uow);
                repository.Update(user);
                await uow.CommitAsync(new CancellationToken());
            }
        }

        /// <summary>
        /// delete as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task for the delete process of the user account.</returns>
        public async Task DeleteAsync(TUser user)
        {
            Guard
                .With<ArgumentNullException>
                .Against(user == null)
                .Say("user");

            using (var uow = this.unitOfWorkFactory.CreateAsync<TUser>())
            {
                var repository = this.repositoryFactory.Create<TUser>(uow);
                repository.Delete(user);
                await uow.CommitAsync(new CancellationToken());
            }
        }

        /// <summary>
        /// Find the user by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The user found by the given userId</returns>
        public async Task<TUser> FindByIdAsync(TKey userId)
        {
            Guard
                .With<ArgumentNullException>
                .Against(userId == null)
                .Say("userId");

            using (var uow = this.unitOfWorkFactory.CreateAsync<TUser>())
            {
                var repository = this.repositoryFactory.Create<TUser>(uow);
                return await Task.FromResult(
                    repository.FindFirstOrDefault(new UserByUserIdSpecification<TKey, TUser>(userId), null));
            }
        }

        /// <summary>
        /// Find the user by name as an asynchronous operation.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>The user found by the given user name</returns>
        public async Task<TUser> FindByNameAsync(string userName)
        {
            Guard
                .With<ArgumentNullException>
                .Against(string.IsNullOrWhiteSpace(userName))
                .Say("username cannot be an empty string or null");

            using (var uow = this.unitOfWorkFactory.CreateAsync<TUser>())
            {
                var repository = this.repositoryFactory.Create<TUser>(uow);
                return await Task.FromResult(
                    repository.FindFirstOrDefault(new UserByUserNameSpecification<TKey, TUser>(userName), null));
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
