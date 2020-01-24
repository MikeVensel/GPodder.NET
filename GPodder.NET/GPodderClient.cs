// <copyright file="GPodderClient.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Client class for making requests to GPodder.Net.
    /// </summary>
    public class GPodderClient
    {
        /// <summary>
        /// Exposes the methods in the <see cref="Authentication"/> class.
        /// </summary>
        public readonly Authentication Authentication;

        private ConfigurationManager configurationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="GPodderClient"/> class.
        /// </summary>
        public GPodderClient()
        {
            this.configurationManager = new ConfigurationManager();
            this.Authentication = new Authentication(this.configurationManager);
        }
    }
}
