namespace GPodder.NET.Models
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Podcast tags available from gPodder.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the searchable tag string for the tag.
        /// </summary>
        [JsonPropertyName("tag")]
        public string SearchTag { get; set; }

        /// <summary>
        /// Gets or sets the number of occurrances of the tag in the directory
        /// from gPodder.
        /// </summary>
        [JsonPropertyName("usage")]
        public int Usage { get; set; }
    }
}
