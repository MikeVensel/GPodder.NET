// <copyright file="Episode.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Models
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Podcast episode metadata as provided by gPodder.
    /// </summary>
    public class Episode
    {
        /// <summary>
        /// Gets or sets the title for the episode.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the url for the episode.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the title of the podcast to which the episode belongs.
        /// </summary>
        [JsonPropertyName("podcast_title")]
        public string PodcastTitle { get; set; }

        /// <summary>
        /// Gets or sets the url of the podcast to which the episode belongs.
        /// </summary>
        [JsonPropertyName("podcast_url")]
        public string PodcastUrl { get; set; }

        /// <summary>
        /// Gets or sets the description for the episode.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the website to which the episode belongs.
        /// </summary>
        [JsonPropertyName("website")]
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets the time at which the episode aired.
        /// </summary>
        [JsonPropertyName("released")]
        public string ReleasedString { get; set; }

        /// <summary>
        /// Gets or sets the gPodder url for the episode.
        /// </summary>
        [JsonPropertyName("mygpo_link")]
        public string MyGpoLink { get; set; }

        /// <summary>
        /// Gets and sets the <see cref="ReleasedString"/> as a <see cref="DateTime"/>
        /// </summary>
        public DateTime Released
        {
            get
            {
                return DateTime.Parse(this.ReleasedString);
            }
        }
    }
}
