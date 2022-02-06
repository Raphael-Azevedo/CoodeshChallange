using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SpaceFlightNews.Models
{
    public class Launch
    {
      
        [JsonPropertyName("id")]
        [NotMapped]
        public string Id { get; set; }

        [JsonPropertyName("provider")]
        [Required]
        public string Provider { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
