// ***********************************************************************
// <copyright file="RepositoryIdentityStore.cs" company="Erik Lieben">
//     Copyright (c) Erik Lieben. All rights reserved.
// </copyright>
// ***********************************************************************
namespace ErikLieben.AspNet.Identity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
    public class RepositoryIdentityStore<TUser, TKey> : 
        IUserLoginStore<TUser, TKey>

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
        /// The dependency factory
        /// </summary>
        private readonly IDependencyFactory dependencyFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryIdentityStore{TUser, TKey}" /> class.
        /// </summary>
        /// <param name="unitOfWorkFactory">The unit of work factory.</param>
        /// <param name="repositoryFactory">The repository factory.</param>
        /// <param name="dependencyFactory">The dependency factory.</param>
        public RepositoryIdentityStore(
            IUnitOfWorkFactory unitOfWorkFactory, 
            IRepositoryFactory repositoryFactory,
            IDependencyFactory dependencyFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.repositoryFactory = repositoryFactory;
            this.dependencyFactory = dependencyFactory;
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
                .Say("user name cannot be an empty string or null");

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
        /// Adds login information to a user as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="login">The login information.</param>
        /// <returns>Task that adds the login information to the user</returns>
        public async Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            Guard
                .With<ArgumentNullException>
                .Against(user == null)
                .Say("user");

            Guard
                .With<ArgumentNullException>
                .Against(login == null)
                .Say("login");

            using (var uow = this.unitOfWorkFactory.CreateAsync<IUserLogin<TKey>>())
            {
                var repository = this.repositoryFactory.Create<IUserLogin<TKey>>(uow);
                repository.Add(
                    this.dependencyFactory.CreateObject<IUserLogin<TKey>>(
                        user.Id, 
                        login.LoginProvider, 
                        login.ProviderKey));

                await uow.CommitAsync(new CancellationToken());
            }
        }

        /// <summary>
        /// Removes login information from a user as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="login">The login information.</param>
        /// <returns>Task that removes the login information from the user</returns>
        public async Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            Guard
                .With<ArgumentNullException>
                .Against(user == null)
                .Say("user");

            Guard
                .With<ArgumentNullException>
                .Against(login == null)
                .Say("login");

            using (var uow = this.unitOfWorkFactory.CreateAsync<IUserLogin<TKey>>())
            {
                var repository = this.repositoryFactory.Create<IUserLogin<TKey>>(uow);
                repository.Delete(this.dependencyFactory.CreateObject<IUserLogin<TKey>>(
                    user.Id, 
                    login.LoginProvider, 
                    login.ProviderKey));
                await uow.CommitAsync(new CancellationToken());
            }
        }

        /// <summary>
        /// Get the logins for a given user asynchronous operation.
        /// </summary>
        /// <param name="user">The user to get logins for.</param>
        /// <returns>The list of logins for the given user</returns>
        public async Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            Guard
                .With<ArgumentNullException>
                .Against(user == null)
                .Say("user");

            using (var uow = this.unitOfWorkFactory.CreateAsync<IUserLogin<TKey>>())
            {
                var repository = this.repositoryFactory.Create<IUserLogin<TKey>>(uow);
                return await Task.FromResult(
                    repository
                        .Find(new UserLoginByUserKeySpecification<TKey>(user.Id), null)
                        .MapAllTo<UserLoginInfo, IUserLogin<TKey>>()
                        .ToList());
            }
        }

        /// <summary>
        /// Find the user that belongs to the given login asynchronous.
        /// </summary>
        /// <param name="login">The login information of the user.</param>
        /// <returns>The user that the login belongs to</returns>
        public async Task<TUser> FindAsync(UserLoginInfo login)
        {
            Guard
                .With<ArgumentNullException>
                .Against(login == null)
                .Say("login");

            using (var uow = this.unitOfWorkFactory.CreateAsync<IUserLogin<TKey>>())
            {
                var repositoryUserLogin = this.repositoryFactory.Create<IUserLogin<TKey>>(uow);
                var userLogin = await Task.FromResult(
                    repositoryUserLogin.FindFirstOrDefault(
                        new UserLoginInfoByLoginProviderAndLoginKeySpecification<TKey>(
                            login.LoginProvider, 
                            login.ProviderKey), 
                        null));

                if (userLogin == null)
                {
                    return null;
                }

                var repositoryUser = this.repositoryFactory.Create<TUser>(uow);
                return await Task.FromResult(
                    repositoryUser.FindFirstOrDefault(new UserByUserIdSpecification<TKey, TUser>(userLogin.UserId), null));
            }
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
