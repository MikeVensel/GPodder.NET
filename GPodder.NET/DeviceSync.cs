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
    /// Ensure that <see cref="Authentication.Login(string, string)"/> has been run
    /// before attempting to use the methods in this class.
    /// </summary>
    public class DeviceSync
    {
        /// <summary>
        /// Retrieve the device sync status for all devices.
        /// </summary>
        /// <param name="username">Username for the gPodder account.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task<SyncStatus> GetSyncStatus(string username)
        {
            var response = await Utilities.HttpClient.GetAsync(
                new Uri($"{GPodderConfig.BaseApiUrl}/api/2/sync-devices/{username}.json"));
            response.EnsureSuccessStatusCode();
            var contentStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<SyncStatus>(contentStream);
        }
    }
}
