// <copyright file="SubUploadFormat.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Enumerations
{
    /// <summary>
    /// Acceptable formats for uploading device subscriptions.
    /// </summary>
    public enum SubUploadFormat
    {
        /// <summary>
        /// Standard JSON
        /// </summary>
        JSON,

        /// <summary>
        /// Standard OPML
        /// </summary>
        OPML,

        /// <summary>
        /// Plaintext
        /// </summary>
        Plaintext,
    }
}
