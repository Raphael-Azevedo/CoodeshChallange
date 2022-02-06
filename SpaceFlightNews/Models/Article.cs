using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SpaceFlightNews.Models
{
    public class Article
    {

        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("newsSite")]
        public string NewsSite { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("publishedAt")]
        public string PublishedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonPropertyName("featured")]
        public bool Featured { get; set; }

        [JsonPropertyName("launches")]
        public IEnumerable<Launch> Launches { get; set; }

        [JsonPropertyName("events")]
        public IEnumerable<Event> Events { get; set; }
    }
}

