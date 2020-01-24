// <copyright file="Utilities.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET
{
    using System.Net.Http;

    /// <summary>
    /// Class containing miscellaneous methods for use in the rest of this library.
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// <see cref="HttpClient"/> to be used throughout this library.
        /// </summary>
        public static readonly HttpClient HttpClient = new HttpClient();
    }
}
