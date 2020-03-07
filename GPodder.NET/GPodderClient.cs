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
        private readonly string username;
        private readonly string password;
        private Authentication authentication;
        private Suggestions suggestions;
        private Devices devices;
        private Subscriptions subscriptions;
        private DeviceSync deviceSync;
        private EpisodeActions episodeActions;

        /// <summary>
        /// Initializes a new instance of the <see cref="GPodderClient"/> class.
        /// </summary>
        public GPodderClient()
        {
            this.Directory = new Directory();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GPodderClient"/> class.
        /// </summary>
        /// <param name="username">Username for the gPodder account.</param>
        /// <param name="password">Password for the gPodder account.</param>
        public GPodderClient(string username, string password)
        {
            this.username = username;
            this.password = password;
            this.Directory = new Directory();
        }

        /// <summary>
        /// Gets a value indicating whether or not the instance has credentials.
        /// </summary>
        public bool ContainsCredentials
        {
            get
            {
                return !string.IsNullOrEmpty(this.username) && !string.IsNullOrEmpty(this.password);
            }
        }

        /// <summary>
        /// Gets the <see cref="Authentication"/> class to make authentication requests.
        /// </summary>
        public Authentication Authentication
        {
            get
            {
                if (!this.ContainsCredentials)
                {
                    throw new System.Exception("Authentication requests require credentials.");
                }

                if (this.authentication == null)
                {
                    this.authentication = new Authentication(this.username, this.password);
                }

                return this.authentication;
            }
        }

        /// <summary>
        /// Gets the <see cref="Directory"/> class to make directory requests.
        /// </summary>
        public Directory Directory { get; }

        /// <summary>
        /// Gets the <see cref="Suggestions"/> class to make suggestion requests.
        /// </summary>
        public Suggestions Suggestions
        {
            get
            {
                if (!this.ContainsCredentials)
                {
                    throw new System.Exception("Authentication requests require credentials.");
                }

                if (this.suggestions == null)
                {
                    this.suggestions = new Suggestions();
                }

                return this.suggestions;
            }
        }

        /// <summary>
        /// Gets the <see cref="Devices"/> class to make device requests.
        /// </summary>
        public Devices Devices
        {
            get
            {
                if (!this.ContainsCredentials)
                {
                    throw new System.Exception("Authentication requests require credentials.");
                }

                if (this.devices == null)
                {
                    this.devices = new Devices(this.username);
                }

                return this.devices;
            }
        }

        /// <summary>
        /// Gets the <see cref="Subscriptions"/> class to make subscription requests.
        /// </summary>
        public Subscriptions Subscriptions
        {
            get
            {
                if (!this.ContainsCredentials)
                {
                    throw new System.Exception("Authentication requests require credentials.");
                }

                if (this.subscriptions == null)
                {
                    this.subscriptions = new Subscriptions(this.username);
                }

                return this.subscriptions;
            }
        }

        /// <summary>
        /// Gets the <see cref="DeviceSync"/> class to make device sync requests.
        /// </summary>
        public DeviceSync DeviceSync
        {
            get
            {
                if (!this.ContainsCredentials)
                {
                    throw new System.Exception("Authentication requests require credentials.");
                }

                if (this.deviceSync == null)
                {
                    this.deviceSync = new DeviceSync(this.username);
                }

                return this.deviceSync;
            }
        }

        /// <summary>
        /// Gets the <see cref="EpisodeActions"/> class to make episode action requests.
        /// </summary>
        public EpisodeActions EpisodeActions
        {
            get
            {
                if (!this.ContainsCredentials)
                {
                    throw new System.Exception("Authentication requests require credentials.");
                }

                if (this.episodeActions == null)
                {
                    this.episodeActions = new EpisodeActions(this.username);
                }

                return this.episodeActions;
            }
        }
    }
}
