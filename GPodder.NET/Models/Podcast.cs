// <copyright file="Podcast.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Podcast metadata as provided by gPodder.
    /// </summary>
    public class Podcast
    {
        /// <summary>
        /// Gets or sets the url for the podcast.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the title for the podcast.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description for the podcast.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the number of subscribers for the podcast.
        /// </summary>
        [JsonPropertyName("subscribers")]
        public int Subscribers { get; set; }

        /// <summary>
        /// Gets or sets the logo url for the podcast.
        /// </summary>
        [JsonPropertyName("logo_url")]
        public string LogoUrl { get; set; }

        /// <summary>
        /// Gets or sets the website url for the podcast.
        /// </summary>
        [JsonPropertyName("website")]
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets the gPodder url for the podcast.
        /// </summary>
        [JsonPropertyName("mygpo_link")]
        public string MyGpoLink { get; set; }
    }
}
