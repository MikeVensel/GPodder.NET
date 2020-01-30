// <copyright file="Devices.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
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

        /// <summary>
        /// Update metadata for the provided device.
        /// </summary>
        /// <param name="username">Username for the gPodder account.</param>
        /// <param name="updatedDevice">A existing <see cref="Device"/> with a valid <see cref="Device.Id"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task UpdateDeviceData(string username, Device updatedDevice)
        {
            var contentString = JsonSerializer.Serialize(updatedDevice);
            var contentByteArray = Encoding.UTF8.GetBytes(contentString);
            var httpContent = new ByteArrayContent(contentByteArray);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await Utilities.HttpClient.PostAsync(
                new Uri($"{GPodderConfig.BaseApiUrl}/api/2/devices/{username}/{updatedDevice.Id}.json"),
                httpContent);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Retrieves updates for the device.
        /// </summary>
        /// <param name="username">Username for the gPodder account.</param>
        /// <param name="deviceId">The device ID for the device.</param>
        /// <param name="lastUpdated">A <see cref="DateTime"/> representing the last time the device was updated.</param>
        /// <param name="includeActions">If set to true, an action property will be attached to the <see cref="DeviceUpdates.EpisodeUpdates"/> representing the latest action reported for the episode.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. It will contain a <see cref="DeviceUpdates"/> if successful.</returns>
        public async Task<DeviceUpdates> GetDeviceUpdates(string username, string deviceId, DateTime lastUpdated, bool includeActions = false)
        {
            // GET /api/2/updates/(username)/(deviceid).json
            var response = await Utilities.HttpClient.GetAsync(
                new Uri($"{GPodderConfig.BaseApiUrl}/api/2/updates/{username}/{deviceId}.json" +
                $"?since={lastUpdated.ToUniversalTime().ToString()}" +
                $"&include_actions={includeActions}"));
            response.EnsureSuccessStatusCode();
            var contentStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<DeviceUpdates>(contentStream);
        }
    }
}
