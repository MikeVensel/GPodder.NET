// <copyright file="MyGpoFeedService.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Models
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Stores the 'mygpo-feedservice' values from the gPodder client parametrization request.
    /// </summary>
    public class MyGpoFeedService
    {
        /// <summary>
        /// Gets or sets the base URL of the gpodder.net feed service.
        /// </summary>
        [JsonPropertyName("baseurl")]
        public string BaseUrl { get; set; }
    }
}
