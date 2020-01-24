// <copyright file="ClientConfig.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Models
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Stores client information from the gPodder client parametrization request.
    /// </summary>
    public class ClientConfig
    {
        /// <summary>
        /// Gets or sets the <see cref="MyGpo"/> object.
        /// </summary>
        [JsonPropertyName("mygpo")]
        public MyGpo MyGpo { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="MyGpoFeedService"/> object.
        /// </summary>
        [JsonPropertyName("mygpo-feedservice")]
        public MyGpoFeedService MyGpoFeedService { get; set; }

        /// <summary>
        /// Gets or sets the time in seconds for which the values in this file can be considered valid.
        /// </summary>
        [JsonPropertyName("update_timeout")]
        public int UpdateTimeout { get; set; }

        /// <summary>
        /// Gets or sets the time in milliseconds for which the values in this file can be considered valid.
        /// </summary>
        [JsonIgnore]
        public int UpdateTimeoutMilliseconds
        {
            get => this.UpdateTimeout * 1000;

            set => this.UpdateTimeout = value / 1000;
        }
    }
}
