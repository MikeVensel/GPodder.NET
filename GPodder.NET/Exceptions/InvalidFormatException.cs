// <copyright file="InvalidFormatException.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception used to indicate that the format sent to gPodder is invalid.
    /// </summary>
    public class InvalidFormatException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFormatException"/> class.
        /// </summary>
        public InvalidFormatException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFormatException"/> class.
        /// </summary>
        /// <param name="message">Exception message to return.</param>
        public InvalidFormatException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFormatException"/> class.
        /// </summary>
        /// <param name="message">Exception message to return.</param>
        /// <param name="innerException">Inner <see cref="Exception"/> to return.</param>
        public InvalidFormatException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFormatException"/> class.
        /// </summary>
        /// <param name="info"><see cref="SerializationInfo"/>.</param>
        /// <param name="context"><see cref="StreamingContext"/>.</param>
        protected InvalidFormatException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
