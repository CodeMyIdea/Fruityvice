using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fruityvice.Models
{
    public class FruitFamilyRequest
    {
        [Required]
        [JsonPropertyName("fruitFamily")]
        public string FruitFamily { get; set; }
    }
}
