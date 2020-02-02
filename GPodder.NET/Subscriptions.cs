// <copyright file="Subscriptions.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using GPodder.NET.Enumerations;
    using GPodder.NET.Exceptions;
    using GPodder.NET.Models;

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
        public async Task<IEnumerable<Podcast>> GetAllSubscriptions(string username)
        {
            var response = await Utilities.HttpClient.GetAsync(
                new Uri($"{GPodderConfig.BaseApiUrl}/subscriptions/{username}.json"));
            return await this.HandleResponseAsync<IEnumerable<Podcast>>(response);
        }

        /// <summary>
        /// Returns device subscriptions for the user.
        /// This can be used to present the user a list of podcasts when the application starts for the first time.
        /// </summary>
        /// <param name="username">Username for the gPodder account.</param>
        /// <param name="deviceId">The device ID for the device.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IEnumerable<Podcast>> GetDeviceSubscriptions(string username, string deviceId)
        {
            var response = await Utilities.HttpClient.GetAsync(
                new Uri($"{GPodderConfig.BaseApiUrl}/subscriptions/{username}/{deviceId}.json"));
            return await this.HandleResponseAsync<IEnumerable<Podcast>>(response);
        }

        /// <summary>
        /// Upload the current subscription list of the given user to the server.
        /// </summary>
        /// <param name="username">Username for the gPodder account.</param>
        /// <param name="deviceId">The device ID for the device.</param>
        /// <param name="format">The <see cref="SubUploadFormat"/> of the provided subscriptions.</param>
        /// <param name="stream">A <see cref="Stream"/> of the subscriptions to upload.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task UploadSubscriptionsOfDevice(string username, string deviceId, SubUploadFormat format, Stream stream)
        {
            var streamContent = new StreamContent(stream);
            var response = await Utilities.HttpClient.PutAsync(
                new Uri($"{GPodderConfig.BaseApiUrl}/subscriptions/{username}/{deviceId}.{format.ToString()}"),
                streamContent);
            var handledResponse = await this.HandleResponseAsync<string>(response);
            if (!string.IsNullOrEmpty(handledResponse))
            {
                throw new Exception(handledResponse);
            }
        }

        private async Task<T> HandleResponseAsync<T>(HttpResponseMessage response)
        {
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new InvalidOperationException("The username provided was invalid.");
                    case HttpStatusCode.NotFound:
                        throw new InvalidDeviceIdException("Invalid device ID");
                    case HttpStatusCode.BadRequest:
                        throw new InvalidFormatException("The gPodder server did not accept the format provided.");
                }
            }

            var contentStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(contentStream);
        }
    }
}