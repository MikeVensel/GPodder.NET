// <copyright file="GenericWebResponseException.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception used to indicate an error http status code not specifically handled.
    /// </summary>
    [Serializable]
    internal class GenericWebResponseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericWebResponseException"/> class.
        /// </summary>
        public GenericWebResponseException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericWebResponseException"/> class.
        /// </summary>
        /// <param name="message">Exception message to return.</param>
        public GenericWebResponseException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericWebResponseException"/> class.
        /// </summary>
        /// <param name="message">Exception message to return.</param>
        /// <param name="innerException">Inner <see cref="Exception"/> to return.</param>
        public GenericWebResponseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericWebResponseException"/> class.
        /// </summary>
        /// <param name="info"><see cref="SerializationInfo"/>.</param>
        /// <param name="context"><see cref="StreamingContext"/>.</param>
        protected GenericWebResponseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}