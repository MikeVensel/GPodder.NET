namespace GPodder.NET.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception used to indicate that the device id provided is invalid.
    /// </summary>
    public class InvalidDeviceIdException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidDeviceIdException"/> class.
        /// </summary>
        public InvalidDeviceIdException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidDeviceIdException"/> class.
        /// </summary>
        /// <param name="message">Exception message to return.</param>
        public InvalidDeviceIdException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidDeviceIdException"/> class.
        /// </summary>
        /// <param name="message">Exception message to return.</param>
        /// <param name="innerException">Inner <see cref="Exception"/> to return.</param>
        public InvalidDeviceIdException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidDeviceIdException"/> class.
        /// </summary>
        /// <param name="info"><see cref="SerializationInfo"/>.</param>
        /// <param name="context"><see cref="StreamingContext"/>.</param>
        protected InvalidDeviceIdException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
