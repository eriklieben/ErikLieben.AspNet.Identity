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
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;

    using Common;
    using Data.Factory;
    using Data.Projection;
    using Interfaces;
    using Microsoft.AspNet.Identity;
    using Specification;

    /// <summary>
    /// Class RepositoryIdentityStore.
    /// </summary>
    /// <typeparam name="TUser">The type of the user object.</typeparam>
    /// <typeparam name="TKey">The type of the key of the user object.</typeparam>
    public class RepositoryIdentityStore<TUser, TKey> : 
        IUserLoginStore<TUser, TKey>,
        IUserClaimStore<TUser, TKey>,
        IUserEmailStore<TUser, TKey>

        where TUser : class, IUserKey<TKey>
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
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            using (var uow = this.unitOfWorkFactory.CreateAsync<IUserKey<TKey>>())
            {
                var repository = this.repositoryFactory.Create<IUserKey<TKey>>(uow);
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
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            using (var uow = this.unitOfWorkFactory.CreateAsync<IUserKey<TKey>>())
            {
                var repository = this.repositoryFactory.Create<IUserKey<TKey>>(uow);
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
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            using (var uow = this.unitOfWorkFactory.CreateAsync<IUserKey<TKey>>())
            {
                var repository = this.repositoryFactory.Create<IUserKey<TKey>>(uow);
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
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException("userName");
            }

            using (var uow = this.unitOfWorkFactory.CreateAsync<IUserKey<TKey>>())
            {
                var repository = this.repositoryFactory.Create<IUserKey<TKey>>(uow);
                return await Task.FromResult(
                    repository.FindFirstOrDefault(new UserByUserNameSpecification<TKey>(userName), null) as TUser);
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

                var obj = this.dependencyFactory.CreateObject<IUserLogin<TKey>>(
                    user.Id,
                    login.LoginProvider,
                    login.ProviderKey);

                repository.Add(obj);

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
                        .Project().To<UserLoginInfo>()
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
                    repositoryUser.FindFirstOrDefault(new UserByUserIdSpecification<TKey, TUser>(userLogin.Id), null));
            }
        }

        /// <summary>
        /// Gets the claims for the given user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>List of claims belonging to this user</returns>
        public async Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            Guard
                .With<ArgumentNullException>
                .Against(user == null)
                .Say("user");

            using (var uow = this.unitOfWorkFactory.CreateAsync<IUserClaim<TKey>>())
            {
                var repository = this.repositoryFactory.Create<IUserClaim<TKey>>(uow);
                return await Task.FromResult(
                    repository
                        .Find(new UserClaimByUserKeySpecification<TKey>(user.Id), null)
                        .Project().To<Claim>()
                        .ToList());
            }
        }

        /// <summary>
        /// add a claim to the given user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="claim">The claim.</param>
        /// <returns>Task that performs the asynchronous adding.</returns>
        public async Task AddClaimAsync(TUser user, Claim claim)
        {
            Guard
                .With<ArgumentNullException>
                .Against(user == null)
                .Say("user");

            Guard
                .With<ArgumentNullException>
                .Against(claim == null)
                .Say("claim");

            using (var uow = this.unitOfWorkFactory.CreateAsync<IUserClaim<TKey>>())
            {
                var repository = this.repositoryFactory.Create<IUserClaim<TKey>>(uow);
                repository.Add(this.dependencyFactory.CreateObject<IUserClaim<TKey>>(user.Id));
                await uow.CommitAsync(new CancellationToken());
            }
        }

        /// <summary>
        /// Removes the claim from the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="claim">The claim.</param>
        /// <returns>Task that removes the claim from the user asynchronous.</returns>
        public async Task RemoveClaimAsync(TUser user, Claim claim)
        {
            Guard
                .With<ArgumentNullException>
                .Against(user == null)
                .Say("user");

            Guard
                .With<ArgumentNullException>
                .Against(claim == null)
                .Say("claim");

            using (var uow = this.unitOfWorkFactory.CreateAsync<IUserClaim<TKey>>())
            {
                var repository = this.repositoryFactory.Create<IUserClaim<TKey>>(uow);
                var claimToDelete = repository.FindFirstOrDefault(new UserClaimByUserKeySpecification<TKey>(user.Id), null);
                repository.Delete(claimToDelete);
                await uow.CommitAsync(new CancellationToken());
            }
        }

        /// <summary>
        /// set email as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="email">The email.</param>
        /// <returns>Task that performs the update of the user.</returns>
        /// <exception cref="System.ArgumentException">user isn't of type IUserWithEmail</exception>
        public async Task SetEmailAsync(TUser user, string email)
        {
            Guard
                .With<ArgumentNullException>
                .Against(user == null)
                .Say("user");

            Guard
                .With<ArgumentException>
                .Against(string.IsNullOrWhiteSpace(email))
                .Say("email cannot be null or an empty string");

            // Transform the user to a user with email object
            var userWithEmail = user as IUserWithEmail<TKey>;
            if (userWithEmail == null)
            {
                throw new ArgumentException("user isn't of type IUserWithEmail");
            }
            
            // Set the email
            userWithEmail.Email = email;

            // Store it
            using (var uow = this.unitOfWorkFactory.CreateAsync<IUserWithEmail<TKey>>())
            {
                var repository = this.repositoryFactory.Create<IUserWithEmail<TKey>>(uow);
                repository.Update(userWithEmail);
                await uow.CommitAsync(new CancellationToken());
            }
        }

        /// <summary>
        /// Gets the email of a user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The email of the user</returns>
        /// <exception cref="System.ArgumentException">user isn't of type IUserWithEmail</exception>
        public async Task<string> GetEmailAsync(TUser user)
        {
            Guard
                .With<ArgumentNullException>
                .Against(user == null)
                .Say("user");

            using (var uow = this.unitOfWorkFactory.CreateAsync<IUserWithEmail<TKey>>())
            {
                var repository = this.repositoryFactory.Create<IUserWithEmail<TKey>>(uow);
                var userFromDb = repository.FindFirstOrDefault(new UserWithEmailByUserIdSpecification<TKey>(user.Id), null);
                return await Task.FromResult(userFromDb.Email);
            }
        }

        /// <summary>
        /// Gets the email confirmed asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>true if the email is confirmed, else false</returns>
        /// <exception cref="System.ArgumentException">user isn't of type IUserWithEmail
        /// or
        /// unable to find item in mail confirmation repository for given user</exception>
        /// <exception cref="System.InvalidOperationException">Cannot get the confirmation status of the e-mail because user doesn't have an e-mail.</exception>
        public async Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            Guard
                .With<ArgumentNullException>
                .Against(user == null)
                .Say("user");

            var userWithEmail = user as IUserWithEmail<TKey>;
            if (userWithEmail == null)
            {
                throw new ArgumentException("user isn't of type IUserWithEmail");
            }

            if (string.IsNullOrWhiteSpace(userWithEmail.Email))
            {
                throw new InvalidOperationException("Cannot get the confirmation status of the e-mail because user doesn't have an e-mail.");
            }

            using (var uow = this.unitOfWorkFactory.CreateAsync<IUserEmailConfirmationStatus<TKey>>())
            {
                var repository = this.repositoryFactory.Create<IUserEmailConfirmationStatus<TKey>>(uow);
                var userFromDb = repository.FindFirstOrDefault(new UserEmailConfirmationStatusByIdSpecification<TKey>(user.Id), null);
                if (userFromDb == null)
                {
                    throw new ArgumentException("unable to find item in mail confirmation repository for given user");
                }

                return await Task.FromResult(userFromDb.EmailConfirmed);
            }

        }

        /// <summary>
        /// set email confirmed as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="confirmed">if set to <c>true</c> [confirmed].</param>
        /// <returns>Task containing the storing of the given value for email confirmed.</returns>
        /// <exception cref="System.ArgumentException">unable to find item in mail confirmation repository for given user</exception>
        public async Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            // Get the user from the storage
            using (var uow = this.unitOfWorkFactory.CreateAsync<IUserEmailConfirmationStatus<TKey>>())
            {
                var repository = this.repositoryFactory.Create<IUserEmailConfirmationStatus<TKey>>(uow);
                var userFromDb = repository.FindFirstOrDefault(new UserEmailConfirmationStatusByIdSpecification<TKey>(user.Id), null);
                if (userFromDb == null)
                {
                    throw new ArgumentException("unable to find item in mail confirmation repository for given user");
                }

                // Change the state
                userFromDb.EmailConfirmed = confirmed;

                // Save back
                repository.Update(userFromDb);
                await uow.CommitAsync(new CancellationToken());
            }
        }

        /// <summary>
        /// find by email as an asynchronous operation.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>The user that belongs to the given email address</returns>
        /// <exception cref="System.ArgumentException">unable to find item in mail confirmation repository for given user</exception>
        public async Task<TUser> FindByEmailAsync(string email)
        {
            using (var uow = this.unitOfWorkFactory.CreateAsync<IUserWithEmail<TKey>>())
            {
                var repository = this.repositoryFactory.Create<IUserWithEmail<TKey>>(uow);
                var user = repository.FindFirstOrDefault(new UserWithEmailByEmailSpecification<TKey, IUserWithEmail<TKey>>(email), null) as TUser;
                if (user == null)
                {
                    throw new ArgumentException("unable to find item in mail confirmation repository for given user");
                }

                return await Task.FromResult(user);
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
