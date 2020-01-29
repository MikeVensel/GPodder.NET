// <copyright file="Devices.cs" company="MikeVensel">
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
    /// Handles retrieving user device data from gPodder.
    /// Ensure that <see cref="Authentication.Login(string, string)"/> has been run
    /// before attempting to use the methods in this class.
    /// </summary>
    public class Devices
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Devices"/> class.
        /// </summary>
        public Devices()
        {
        }

        /// <summary>
        /// Retrieve a list of the user's devices.
        /// </summary>
        /// <param name="username">Username for the gPodder account.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. It will contain a <see cref="IEnumerable{Device}"/> if successful.</returns>
        /// <exception cref="HttpRequestException">Thrown if the request is not successful.</exception>
        /// <exception cref="JsonException">Thrown if the response content cannot be serialized into a <see cref="IEnumerable{Device}"/>.</exception>
        public async Task<IEnumerable<Device>> ListDevices(string username)
        {
            var response = await Utilities.HttpClient.GetAsync(new Uri(
                $"{GPodderConfig.BaseApiUrl}/api/2/devices/{username}.json"));
            response.EnsureSuccessStatusCode();
            var contentStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<Device>>(contentStream);
        }
    }
}
