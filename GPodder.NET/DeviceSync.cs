// <copyright file="DeviceSync.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET
{
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;
    using GPodder.NET.Models;

    /// <summary>
    /// Handles gPodder device synchronization.
    /// Ensure that <see cref="Authentication.Login"/> has been run
    /// before attempting to use the methods in this class.
    /// </summary>
    public class DeviceSync
    {
        private string username;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceSync"/> class.
        /// </summary>
        /// <param name="username">Username for the gPodder account.</param>
        public DeviceSync(string username)
        {
            this.username = username;
        }

        /// <summary>
        /// Retrieve the device sync status for all devices.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task<SyncStatus> GetSyncStatus()
        {
            var response = await Utilities.HttpClient.GetAsync(
                new Uri($"{GPodderConfig.BaseApiUrl}/api/2/sync-devices/{this.username}.json"));
            response.EnsureSuccessStatusCode();
            var contentStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<SyncStatus>(contentStream);
        }
    }
}
