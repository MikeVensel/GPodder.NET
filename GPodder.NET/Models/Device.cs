// <copyright file="Device.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Models
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Device metadata as provided by gPodder.
    /// </summary>
    public class Device
    {
        /// <summary>
        /// Gets or sets the device ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the caption (friendly name) for the device.
        /// </summary>
        [JsonPropertyName("caption")]
        public string Caption { get; set; }

        // todo see if there is information from gPodder that can be used to make this an enum

        /// <summary>
        /// Gets or sets the device's type.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the number of subscriptions on the device.
        /// </summary>
        [JsonPropertyName("subscriptions")]
        public long Subscrciptions { get; set; }
    }
}
