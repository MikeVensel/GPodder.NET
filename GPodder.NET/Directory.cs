// <copyright file="Directory.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using GPodder.NET.Exceptions;
    using GPodder.NET.Models;

    /// <summary>
    /// Handles retrieving podcast directory from gPodder.
    /// These do not require authentication.
    /// </summary>
    public class Directory
    {
        private readonly ConfigurationManager configurationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Directory"/> class.
        /// </summary>
        /// <param name="configurationManager"><see cref="ConfigurationManager"/> for settings required for requests.</param>
        public Directory(ConfigurationManager configurationManager)
        {
            this.configurationManager = configurationManager;
        }

        /// <summary>
        /// Retrieves available tag metadata from gPodder.
        /// </summary>
        /// <param name="count">The number of tags to return in the query.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. It will contain an <see cref="IEnumerable{GPodderTag}"/> if successful.</returns>
        /// <exception cref="GenericWebResponseException">Thrown if the request is not successful.</exception>"
        /// <exception cref="JsonException">Thrown if the response content cannot be serialized into the appropriate object.</exception>"
        public async Task<IEnumerable<Tag>> GetTags(int count = 100)
        {
            var clientConfig = await this.configurationManager.GetConfigTask();
            var tagsResponse = await Utilities.HttpClient.GetAsync($"{clientConfig.MyGpo.BaseUrl}api/2/tags/{count}.json");
            return await this.HandleResponseAsync<IEnumerable<Tag>>(tagsResponse);
        }

        /// <summary>
        /// Retrieve top list of podcasts.
        /// </summary>
        /// <param name="number">The number of podcasts to return.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. It will contain an <see cref="IEnumerable{Podcast}"/> if successful.</returns>
        /// <exception cref="GenericWebResponseException">Thrown if the request is not successful.</exception>"
        /// <exception cref="JsonException">Thrown if the response content cannot be serialized into the appropriate object.</exception>"
        public async Task<IEnumerable<Podcast>> GetTopPodcasts(int number)
        {
            var clientConfig = await this.configurationManager.GetConfigTask();
            var response = await Utilities.HttpClient.GetAsync($"{clientConfig.MyGpo.BaseUrl}toplist/{number}.json");
            return await this.HandleResponseAsync<IEnumerable<Podcast>>(response);
        }

        /// <summary>
        /// Retrieve podcasts with the provided tag.
        /// </summary>
        /// <param name="tag">The tag, for which, to search for podcasts.</param>
        /// <param name="count">The number of podcasts to return in the query.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. It will contain an <see cref="IEnumerable{Podcast}"/> if successful.</returns>
        /// <exception cref="GenericWebResponseException">Thrown if the request is not successful.</exception>"
        /// <exception cref="JsonException">Thrown if the response content cannot be serialized into the appropriate object.</exception>"
        public async Task<IEnumerable<Podcast>> GetPodcastsWithTag(string tag, int count = 100)
        {
            var clientConfig = await this.configurationManager.GetConfigTask();
            var response = await Utilities.HttpClient.GetAsync($"{clientConfig.MyGpo.BaseUrl}api/2/tag/{tag}/{count}.json");
            return await this.HandleResponseAsync<IEnumerable<Podcast>>(response);
        }

        private async Task<T> HandleResponseAsync<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<T>(contentStream);
            }

            // documentation does not show any error status codes to handle
            throw new GenericWebResponseException($"An error status code of {response.StatusCode} was returned from gPodder.");
        }
    }
}
