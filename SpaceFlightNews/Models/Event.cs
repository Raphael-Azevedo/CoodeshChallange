using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SpaceFlightNews.Models
{
    public class Event
    {
        
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("provider")]
        [Required]
        public string Provider { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }

    }
}

