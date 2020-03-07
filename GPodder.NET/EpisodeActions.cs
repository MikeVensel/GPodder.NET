// <copyright file="EpisodeActions.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;
    using GPodder.NET.Models;

    /// <summary>
    /// Handles receiving and sending episode actions to/from
    /// gPodder.
    /// </summary>
    public class EpisodeActions
    {
        private string username;

        /// <summary>
        /// Initializes a new instance of the <see cref="EpisodeActions"/> class.
        /// </summary>
        /// <param name="username">Username for the gPodder account.</param>
        public EpisodeActions(string username)
        {
            this.username = username;
        }

        /// <summary>
        /// Gets episode actions.
        /// </summary>
        /// <param name="podcastUrl">The URL of a Podcast feed; if set, only actions for episodes of the given podcast are returned.</param>
        /// <param name="deviceId">A Device ID; if set, only actions for the given device are returned.</param>
        /// <param name="sinceTimestamp">Only episode actions since the given timestamp are returned.</param>
        /// <param name="aggregated">If true, only the latest actions is returned for each episode.</param>
        /// <returns>A <see cref="Task{EpisodeActionsResponse}"/> representing the result of the asynchronous operation.</returns>
        public async Task<EpisodeActionsResponse> GetEpisodeActions(string podcastUrl = "", string deviceId = "", long sinceTimestamp = 0, bool aggregated = true)
        {
            var urlStringBuilder = new StringBuilder($"{GPodderConfig.BaseApiUrl}/api/2/episodes/{this.username}.json" +
                $"?since={sinceTimestamp}" +
                $"&aggregated={aggregated}");

            if (!string.IsNullOrEmpty(podcastUrl))
            {
                urlStringBuilder.Append($"&podcast={podcastUrl}");
            }

            if (!string.IsNullOrEmpty(deviceId))
            {
                urlStringBuilder.Append($"&device={deviceId}");
            }

            var response = await Utilities.HttpClient.GetAsync(
                new Uri(urlStringBuilder.ToString()));
            response.EnsureSuccessStatusCode();
            var contentStream = await response.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter());
            return await JsonSerializer.DeserializeAsync<EpisodeActionsResponse>(contentStream, options);
        }

        /// <summary>
        /// Upload episode actions.
        /// </summary>
        /// <param name="episodeActionsCollection">A collection of episode actions to be uploaded.</param>
        /// <returns>A <see cref="Task{UpdatedDeviceSubscriptions}"/> representing the asynchronous operation.</returns>
        public async Task<UpdatedDeviceSubscriptions> UploadEpisodeActions(IEnumerable<EpisodeAction> episodeActionsCollection)
        {
            var episodeActionsString = JsonSerializer.Serialize(episodeActionsCollection);
            var stringContent = new StringContent(episodeActionsString);
            var response = await Utilities.HttpClient.PostAsync(
                new Uri($"{GPodderConfig.BaseApiUrl}/api/2/episodes/{this.username}.json"),
                stringContent);
            response.EnsureSuccessStatusCode();
            var contentStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<UpdatedDeviceSubscriptions>(contentStream);
        }
    }
}
