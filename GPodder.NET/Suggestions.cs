// <copyright file="Suggestions.cs" company="MikeVensel">
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
    /// Handles retrieving podcast suggestions from gPodder.
    /// The methods in this class require authentication.
    /// </summary>
    public class Suggestions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Suggestions"/> class.
        /// </summary>
        public Suggestions()
        {
        }

        /// <summary>
        /// Retrieve podcast suggestions for the user.
        /// </summary>
        /// <param name="count">The number of suggestions to return.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. It will contain a <see cref="IEnumerable{Podcast}"/> if successful.</returns>
        /// <exception cref="HttpRequestException">Thrown if the request is not successful.</exception>
        /// <exception cref="JsonException">Thrown if the response content cannot be serialized into a <see cref="IEnumerable{Podcast}"/>.</exception>
        public async Task<IEnumerable<Podcast>> RetrieveSuggestedPodcasts(int count)
        {
            var response = await Utilities.HttpClient.GetAsync(new Uri(
                $"{GPodderConfig.BaseApiUrl}/suggestions/{count}.json"));

            // todo see if there are any error codes that warrant specific exceptions
            response.EnsureSuccessStatusCode();
            var contentStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<Podcast>>(contentStream);
        }
    }
}
