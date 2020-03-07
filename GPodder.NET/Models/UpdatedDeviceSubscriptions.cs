// <copyright file="UpdatedDeviceSubscriptions.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Stores subscription change metadata returned by gPodder when uploading device
    /// subscription changes.
    /// </summary>
    public class UpdatedDeviceSubscriptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatedDeviceSubscriptions"/> class.
        /// </summary>
        public UpdatedDeviceSubscriptions()
        {
            this.UpdateUrls = new List<Tuple<string, string>>();
        }

        /// <summary>
        /// Gets or sets the timestamp/ID that can be used for requesting changes since
        /// the upload.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        /// <summary>
        /// Gets or sets a list of URLs that have been rewritten from gPodder.
        /// </summary>
        [JsonPropertyName("update_urls")]
        public IEnumerable<Tuple<string, string>> UpdateUrls { get; set; }
    }
}
