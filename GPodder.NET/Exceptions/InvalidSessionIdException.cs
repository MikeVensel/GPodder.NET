// <copyright file="InvalidSessionIdException.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception used to indicate that an http status code of 400 was returned.
    /// </summary>
    [Serializable]
    public class InvalidSessionIdException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSessionIdException"/> class.
        /// </summary>
        public InvalidSessionIdException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSessionIdException"/> class.
        /// </summary>
        /// <param name="message">Exception message to return.</param>
        public InvalidSessionIdException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSessionIdException"/> class.
        /// </summary>
        /// <param name="message">Exception message to return.</param>
        /// <param name="inner">Inner <see cref="Exception"/> to return.</param>
        public InvalidSessionIdException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSessionIdException"/> class.
        /// </summary>
        /// <param name="info"><see cref="SerializationInfo"/>.</param>
        /// <param name="context"><see cref="StreamingContext"/>.</param>
        protected InvalidSessionIdException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
