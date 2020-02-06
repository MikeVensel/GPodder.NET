// <copyright file="UpdateSubscriptionConflictException.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception used to indicate that an attempt was made to both add and remove a url
    /// during a device subscription update.
    /// </summary>
    public class UpdateSubscriptionConflictException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSubscriptionConflictException"/> class.
        /// </summary>
        public UpdateSubscriptionConflictException()
            : base("The same feed has been added and removed in the same request.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSubscriptionConflictException"/> class.
        /// </summary>
        /// <param name="message">Exception message to return.</param>
        public UpdateSubscriptionConflictException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSubscriptionConflictException"/> class.
        /// </summary>
        /// <param name="message">Exception message to return.</param>
        /// <param name="innerException">Inner <see cref="Exception"/> to return.</param>
        public UpdateSubscriptionConflictException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSubscriptionConflictException"/> class.
        /// </summary>
        /// <param name="info"><see cref="SerializationInfo"/>.</param>
        /// <param name="context"><see cref="StreamingContext"/>.</param>
        protected UpdateSubscriptionConflictException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
