// <copyright file="MyGpo.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Models
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Stores the 'mygpo' values from the gPodder client parametrization request.
    /// </summary>
    public class MyGpo
    {
        /// <summary>
        /// Gets or sets the URL to which the gpodder.net API Endpoints should be appended.
        /// </summary>
        [JsonPropertyName("baseurl")]
        public string BaseUrl { get; set; }
    }
}
