// ***********************************************************************
// <copyright file="Guard.cs" company="Erik Lieben">
//     Copyright (c) Erik Lieben. All rights reserved.
// </copyright>
// ***********************************************************************
namespace ErikLieben.Common
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Class GuardExtensions.
    /// </summary>
    [DebuggerStepThrough]
    [System.Diagnostics.Contracts.ContractVerification(false)]
    public static class GuardExtensions
    {
        // TODO: Fix this class later, remove code analytics suppress or justify them, split file, etc.

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "args")]
        /// <summary>
        /// If the condition is false, the guard will Say
        /// </summary>
        /// <param name="guard">The guard.</param>
        /// <param name="message">The message to day.</param>
        /// <param name="args">The args to format the string with.</param>
        /// <exception cref="System.Exception"></exception>
        public static void Say(this Guard guard, string message, params string[] args)
        {
            if (guard == null)
            {
                return;
            }

            guard.Handled = true;
            //ExceptionUtil.Throw(message, guard.ExceptionType, args);
            // TODO: Fix this
            throw new Exception(message);
        }
    }

    /// <summary>
    /// Class Guard.
    /// </summary>
    [DebuggerStepThrough]
    public class Guard
    {
        #region [ Construct & Destruct ]
        /// <summary>
        /// Initializes a new instance of the <see cref="Guard" /> class.
        /// </summary>
        public Guard()
        {
            Handled = false;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])")]

#if DEBUG
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="Guard" /> is reclaimed by garbage collection.
        /// </summary>
        /// <exception cref="ErikLieben.Common.GuardException"></exception>
        ~Guard()
        {
            if (!Handled)
            {
                throw new GuardException(
                    string.Format(
                        "The Guard has nothing to say at file {0}, method {1}, line {2}, column {3}",
                        this.FileName,
                        this.Method,
                        this.LineNumber,
                        this.ColumnNumber));
            }
        }
#endif
        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Guard"/> is condition.
        /// </summary>
        /// <value><c>true</c> if condition; otherwise, <c>false</c>.</value>
        public bool Condition { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Guard"/> is handled.
        /// </summary>
        /// <value><c>true</c> if handled; otherwise, <c>false</c>.</value>
        public bool Handled { get; set; }

        /// <summary>
        /// Gets or sets the type of the exception.
        /// </summary>
        /// <value>The type of the exception.</value>
        public Type ExceptionType { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the line number.
        /// </summary>
        /// <value>The line number.</value>
        public int LineNumber { get; set; }

        /// <summary>
        /// Gets or sets the column number.
        /// </summary>
        /// <value>The column number.</value>
        public int ColumnNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "E")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "With")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1715:IdentifiersShouldHaveCorrectPrefix", MessageId = "T")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]

        #endregion

        /// <summary>
        /// A static class to allow you to define with what the guard will guard the castle.
        /// </summary>
        /// <typeparam name="E">Type of the exception to guard with</typeparam>
        public static class With<E> where E : Exception, new()
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
            /// <summary>
            /// Guard, againsts the specified condition.
            /// </summary>
            /// <param name="condition">if set to <c>true</c> [condition].</param>
            /// <returns>The guard with the given condition set, if in DEBUG the stack trace info</returns>
            public static Guard Against(bool condition)
            {
                if (!condition)
                {
                    return null;
                }
                else
                {
#if DEBUG
                    StackTrace trace = new StackTrace(true);
                    StackFrame frame = trace.GetFrame(1);
#endif

#if DEBUG
                    return new Guard
                    {
                        ExceptionType = typeof(E),
                        Handled = false,
                        FileName = frame.GetFileName(),
                        Method = frame.GetMethod().Name,
                        LineNumber = frame.GetFileLineNumber(),
                        ColumnNumber = frame.GetFileColumnNumber()
                    };
#else
                        return new Guard { 
                            ExceptionType = typeof(E), 
                            Handled = false
                        };
#endif
                }
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors")]
    /// <summary>
    /// The exception that occurs when you badly used the guard
    /// </summary>
    public class GuardException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public GuardException(string message)
            : base(message)
        {
        }
    }
}


