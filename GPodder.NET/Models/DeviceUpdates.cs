// <copyright file="DeviceUpdates.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Device update metadata as provided by gPodder.
    /// </summary>
    public class DeviceUpdates
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceUpdates"/> class.
        /// </summary>
        public DeviceUpdates()
        {
            this.NewPodcasts = new List<Podcast>();
            this.RemovedPodcastUris = new List<Uri>();
            this.EpisodeUpdates = new List<Episode>();
        }

        /// <summary>
        /// Gets or sets the podcasts to be added to the device.
        /// </summary>
        [JsonPropertyName("add")]
        public IEnumerable<Podcast> NewPodcasts { get; set; }

        /// <summary>
        /// Gets or sets the collection of podcast <see cref="Uri"/> which should
        /// be removed from the device.
        /// </summary>
        [JsonPropertyName("remove")]
        public IEnumerable<Uri> RemovedPodcastUris { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="Episode"/> which should
        /// have a new status on the device.
        /// </summary>
        [JsonPropertyName("updates")]
        public IEnumerable<Episode> EpisodeUpdates { get; set; }
    }
}