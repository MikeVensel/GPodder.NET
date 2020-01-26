// <copyright file="GPodderClient.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET
{
    /// <summary>
    /// Client class for making requests to GPodder.Net.
    /// </summary>
    public class GPodderClient
    {
        private readonly ConfigurationManager configurationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="GPodderClient"/> class.
        /// </summary>
        public GPodderClient()
        {
            this.configurationManager = new ConfigurationManager();
            this.Authentication = new Authentication(this.configurationManager);
            this.Directory = new Directory(this.configurationManager);
        }

        /// <summary>
        /// Gets the <see cref="Authentication"/> class to make authentication requests.
        /// </summary>
        public Authentication Authentication { get; }

        /// <summary>
        /// Gets the <see cref="Directory"/> class to make directory requests.
        /// </summary>
        public Directory Directory { get; }
    }
}
