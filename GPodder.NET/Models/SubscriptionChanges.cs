// <copyright file="SubscriptionChanges.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Models
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Object used to send device subscription changes to gPodder.
    /// </summary>
    public class SubscriptionChanges
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionChanges"/> class.
        /// </summary>
        public SubscriptionChanges()
        {
            this.Add = new List<string>();
            this.Remove = new List<string>();
        }

        /// <summary>
        /// Gets or sets a list of subscriptions to add to the device.
        /// </summary>
        [JsonPropertyName("add")]
        public IEnumerable<string> Add { get; set; }

        /// <summary>
        /// Gets or sets a list of subscriptions to remove to the device.
        /// </summary>
        [JsonPropertyName("remove")]
        public IEnumerable<string> Remove { get; set; }

        /// <summary>
        /// Gets or sets the timestamp used by gPodder to determine when
        /// the device asked for changes last.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public int Timestamp { get; set; }
    }
}
