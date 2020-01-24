// <copyright file="UnauthorizedException.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception used to indicate that an http status code of 401 was returned.
    /// </summary>
    [Serializable]
    public class UnauthorizedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedException"/> class.
        /// </summary>
        public UnauthorizedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedException"/> class.
        /// </summary>
        /// <param name="message">Exception message to return.</param>
        public UnauthorizedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedException"/> class.
        /// </summary>
        /// <param name="message">Exception message to return.</param>
        /// <param name="innerException">Inner <see cref="Exception"/> to return.</param>
        public UnauthorizedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedException"/> class.
        /// </summary>
        /// <param name="info"><see cref="SerializationInfo"/>.</param>
        /// <param name="context"><see cref="StreamingContext"/>.</param>
        protected UnauthorizedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}