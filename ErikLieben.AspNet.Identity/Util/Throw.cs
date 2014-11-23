using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErikLieben.AspNet.Identity.Util
{
    /// <summary>
    /// Fluent interface for throwing exceptions
    /// </summary>
    /// <typeparam name="T">Type of exception to throw</typeparam>
    [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Throw", Justification = "class is used to throw exceptions")]
    internal static class Throw<T> where T : Exception, new()
    {
        /// <summary>
        /// Whens the specified condition.
        /// </summary>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The args to format the string with.</param>
        internal static void When(bool condition, string message, params string[] args)
        {
            if (condition)
            {
                Invoke<T>(message, args);
            }
        }

        /// <summary>
        /// Whens the not.
        /// </summary>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The args to format the string with.</param>
        internal static void WhenNot(bool condition, string message, params string[] args)
        {
            if (!condition)
            {
                Invoke<T>(message, args);
            }
        }

        /// <summary>
        /// Throws the specified message.
        /// </summary>
        /// <typeparam name="EX">The type of the EX.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="args">The args to format the string with.</param>
        //[DebuggerStepThrough]
        private static void Invoke<EX>(string message, string[] args) where EX : Exception, new()
        {
            EX exception;

            // Get the constructor
            Type ex = typeof(EX);
            Contract.Assume(ex != null);
            var constructor = ex.GetConstructor(new System.Type[]
            {
                typeof(string), typeof(string)
            });
            Contract.Assume(constructor != null);

            // Workaround for bad .NET framework implementation, message argument should always be first argument.
            // Invoke it in the correct way
            if (typeof(EX).Equals(typeof(ArgumentNullException)) && args != null && args.Length == 1)
            {
                exception = constructor.Invoke(new object[] { args[0], message }) as EX;
            }
            else
            {
                exception = constructor.Invoke(new object[] { message, args.Length > 0 ? args[0] : string.Empty }) as EX;
            }

            throw exception;
        }
    }
}