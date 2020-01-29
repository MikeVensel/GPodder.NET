﻿// <copyright file="GPodderClient.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET
{
    /// <summary>
    /// Client class for making requests to GPodder.Net.
    /// </summary>
    public class GPodderClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GPodderClient"/> class.
        /// </summary>
        public GPodderClient()
        {
            this.Authentication = new Authentication();
            this.Directory = new Directory();
            this.Suggestions = new Suggestions();
            this.Devices = new Devices();
        }

        /// <summary>
        /// Gets the <see cref="Authentication"/> class to make authentication requests.
        /// </summary>
        public Authentication Authentication { get; }

        /// <summary>
        /// Gets the <see cref="Directory"/> class to make directory requests.
        /// </summary>
        public Directory Directory { get; }

        /// <summary>
        /// Gets the <see cref="Suggestions"/> class to make suggestion requests.
        /// </summary>
        public Suggestions Suggestions { get; }

        /// <summary>
        /// Gets the <see cref="Devices"/> class to make device requests.
        /// </summary>
        public Devices Devices { get; }
    }
}
