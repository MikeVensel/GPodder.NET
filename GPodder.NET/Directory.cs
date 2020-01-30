// <copyright file="Directory.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using GPodder.NET.Models;

    /// <summary>
    /// Handles retrieving podcast directory from gPodder.
    /// These do not require authentication.
    /// </summary>
    public class Directory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Directory"/> class.
        /// </summary>
        public Directory()
        {
        }

        /// <summary>
        /// Retrieves available tag metadata from gPodder.
        /// </summary>
        /// <param name="count">The number of tags to return in the query.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. It will contain an <see cref="IEnumerable{Tag}"/> if successful.</returns>
        /// <exception cref="HttpRequestException">Thrown if the request is not successful.</exception>"
        /// <exception cref="JsonException">Thrown if the response content cannot be serialized into a <see cref="IEnumerable{Tag}"/>.</exception>
        public async Task<IEnumerable<Tag>> GetTags(int count = 100)
        {
            var tagsResponse = await Utilities.HttpClient.GetAsync($"{GPodderConfig.BaseApiUrl}/api/2/tags/{count}.json");
            return await this.HandleResponseAsync<IEnumerable<Tag>>(tagsResponse);
        }

        /// <summary>
        /// Retrieve top list of podcasts.
        /// </summary>
        /// <param name="number">The number of podcasts to return.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. It will contain an <see cref="IEnumerable{Podcast}"/> if successful.</returns>
        /// <exception cref="HttpRequestException">Thrown if the request is not successful.</exception>"
        /// <exception cref="JsonException">Thrown if the response content cannot be serialized into a <see cref="IEnumerable{Podcast}"/>.</exception>
        public async Task<IEnumerable<Podcast>> GetTopPodcasts(int number)
        {
            var response = await Utilities.HttpClient.GetAsync($"{GPodderConfig.BaseApiUrl}/toplist/{number}.json");
            return await this.HandleResponseAsync<IEnumerable<Podcast>>(response);
        }

        /// <summary>
        /// Retrieve podcasts with the provided tag.
        /// </summary>
        /// <param name="tag">The tag, for which, to search for podcasts.</param>
        /// <param name="count">The number of podcasts to return in the query.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. It will contain an <see cref="IEnumerable{Podcast}"/> if successful.</returns>
        /// <exception cref="HttpRequestException">Thrown if the request is not successful.</exception>"
        /// <exception cref="JsonException">Thrown if the response content cannot be serialized into a <see cref="IEnumerable{Podcast}"/>.</exception>
        public async Task<IEnumerable<Podcast>> GetPodcastsWithTag(string tag, int count = 100)
        {
            var response = await Utilities.HttpClient.GetAsync($"{GPodderConfig.BaseApiUrl}/api/2/tag/{tag}/{count}.json");
            return await this.HandleResponseAsync<IEnumerable<Podcast>>(response);
        }

        /// <summary>
        /// Retrieve podcast metadata for a podcast.
        /// </summary>
        /// <param name="podcastUrl">The url where the podcast can be found.</param>
        /// <param name="scaleLogo">Size for the logo returned in the <see cref="Podcast.LogoUrl"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. It will contain a <see cref="Podcast"/> if successful.</returns>
        /// <exception cref="HttpRequestException">Thrown if the request is not successful.</exception>"
        /// <exception cref="JsonException">Thrown if the response content cannot be serialized into a <see cref="Podcast"/>.</exception>
        public async Task<Podcast> GetPodcastData(string podcastUrl, int scaleLogo = 64)
        {
            var response = await Utilities.HttpClient.GetAsync(new Uri(
                $"{GPodderConfig.BaseApiUrl}/api/2/data/podcast.json?" +
                $"url={podcastUrl}" +
                $"&scale_logo={scaleLogo}"));
            return await this.HandleResponseAsync<Podcast>(response);
        }

        /// <summary>
        /// Retrieve metadata for a podcast episode.
        /// </summary>
        /// <param name="podcastUrl">The url where the podcast can be found.</param>
        /// <param name="episodeUrl">The url where the episode can be found.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. It will contain a <see cref="Episode"/> if successful.</returns>
        /// <exception cref="HttpRequestException">Thrown if the request is not successful.</exception>"
        /// <exception cref="JsonException">Thrown if the response content cannot be serialized into a <see cref="Episode"/>.</exception>
        public async Task<Episode> GetPodcastEpisode(string podcastUrl, string episodeUrl)
        {
            var response = await Utilities.HttpClient.GetAsync(new Uri(
                $"{GPodderConfig.BaseApiUrl}/api/2/data/episode.json?" +
                $"podcast={podcastUrl}" +
                $"&url={episodeUrl}"));
            return await this.HandleResponseAsync<Episode>(response);
        }

        /// <summary>
        /// Retrieve a <see cref="IEnumerable{Podcast}"/> of podcasts that match the
        /// search query.
        /// </summary>
        /// <param name="searchQuery">The value for which to search podcasts.</param>
        /// <param name="scaleLogo">Size for the logo returned in the <see cref="Podcast.LogoUrl"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. It will contain a <see cref="IEnumerable{Podcast}"/> if successful.</returns>
        /// <exception cref="HttpRequestException">Thrown if the request is not successful.</exception>"
        /// <exception cref="JsonException">Thrown if the response content cannot be serialized into a <see cref="IEnumerable{Podcast}"/>.</exception>
        public async Task<IEnumerable<Podcast>> SearchForPodcasts(string searchQuery, int scaleLogo = 64)
        {
            var response = await Utilities.HttpClient.GetAsync(new Uri(
                $"{GPodderConfig.BaseApiUrl}/search.json?" +
                $"q={searchQuery}" +
                $"&scale_logo={scaleLogo}"));
            return await this.HandleResponseAsync<IEnumerable<Podcast>>(response);
        }

        private async Task<T> HandleResponseAsync<T>(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            var contentStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(contentStream);
        }
    }
}
