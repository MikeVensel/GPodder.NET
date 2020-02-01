// <copyright file="Subscriptions.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET
{
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;

    /// <summary>
    /// Handles retrieving user subscription data from gPodder.
    /// Ensure that <see cref="Authentication.Login(string, string)"/> has been run
    /// before attempting to use the methods in this class.
    /// </summary>
    public class Subscriptions
    {
        /// <summary>
        /// Returns all subscriptions for the user.
        /// This can be used to present the user a list of podcasts when the application starts for the first time.
        /// </summary>
        /// <param name="username">Username for the gPodder account.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. </returns>
        public async Task GetAllSubscriptions(string username)
        {
            var response = await Utilities.HttpClient.GetAsync(
                new Uri($"{GPodderConfig.BaseApiUrl}/subscriptions/{username}.json"));
            var contentString = await response.Content.ReadAsStringAsync();
        }

        private async Task<T> HandleResponseAsync<T>(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            var contentStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(contentStream);
        }
    }
}