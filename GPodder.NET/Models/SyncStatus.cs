// <copyright file="SyncStatus.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Models
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Object used to receive sync status from gPodder.
    /// </summary>
    public class SyncStatus
    {
        /// <summary>
        /// Gets or sets devices that are synchronized.
        /// </summary>
        [JsonPropertyName("synchronized")]
        public IEnumerable<string> Synchronized { get; set; }

        /// <summary>
        /// Gets or sets devices that are not synchronized.
        /// </summary>
        [JsonPropertyName("not-synchronized")]
        public IEnumerable<string> NotSynchronized { get; set; }
    }
}
