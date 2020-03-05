// <copyright file="EpisodeActionsResponse.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Models
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Class used to handle responses to the <see cref="EpisodeActions.GetEpisodeActions(string, string, string, int, bool)"/> method.
    /// </summary>
    public class EpisodeActionsResponse
    {
        /// <summary>
        /// Gets or sets the list of episode actions from gPodder.
        /// </summary>
        [JsonPropertyName("actions")]
        public IEnumerable<EpisodeAction> ActionsList { get; set; }

        /// <summary>
        /// Gets or sets the timestamp returned by gPodder. This value
        /// should be stored by the client and provided when making requests
        /// for episode actions updates.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public int Timestamp { get; set; }
    }
}
