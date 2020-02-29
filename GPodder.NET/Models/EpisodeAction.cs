// <copyright file="EpisodeAction.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Models
{
    using System;
    using System.Text.Json.Serialization;
    using GPodder.NET.Enumerations;

    /// <summary>
    /// Podcast episode action metadata used to sync the state of episodes
    /// with gPodder.
    /// </summary>
    public class EpisodeAction
    {
        /// <summary>
        /// Gets or sets the feed URL to the podcast feed the episode belongs to.
        /// </summary>
        [JsonPropertyName("podcast")]
        public string PodcastUrl { get; set; }

        /// <summary>
        /// Gets or sets the media URL of the episode.
        /// </summary>
        [JsonPropertyName("episode")]
        public string EpisodeUrl { get; set; }

        /// <summary>
        /// Gets or sets the device ID on which the action has taken place.
        /// </summary>
        [JsonPropertyName("device")]
        public string DeviceId { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        [JsonPropertyName("action")]
        public EpisodeActionType Action { get; set; }

        /// <summary>
        /// Gets or sets a UTC timestamp when the action took place, in ISO 8601 format.
        /// This is used for requests to gPodder but the other property should be used on clients.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public string UtcTimeString { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="UtcTimeString"/> with a <see cref="DateTime"/> object.
        /// </summary>
        public DateTime ActionTimestamp
        {
            get => DateTime.Parse(this.UtcTimeString).ToUniversalTime();

            set => this.UtcTimeString = value.ToUniversalTime().ToString();
        }

        /// <summary>
        /// Gets or sets the position (in seconds) at which the client started playback.
        /// Requires position and total to be set and is only valid when the action is <see cref="EpisodeActionType.Play"/>.
        /// </summary>
        [JsonPropertyName("started")]
        public int StartingPosition { get; set; }

        /// <summary>
        /// Gets or sets the position (in seconds) at which the client stopped playback.
        /// This property is only valid when the action is <see cref="EpisodeActionType.Play"/>.
        /// </summary>
        [JsonPropertyName("position")]
        public int PlaybackPosition { get; set; }

        /// <summary>
        /// Gets or sets the total length of the file in seconds.
        /// Requires position and started to be set and is only valid when the action is <see cref="EpisodeActionType.Play"/>.
        /// </summary>
        public int TotalEpisodeLength { get; set; }
    }
}
