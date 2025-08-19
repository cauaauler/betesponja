using System.Text.Json;
using System.Text.Json.Serialization;

namespace FutebolSimplesBetsHub.Models
{
    // Modelos para resposta da API (apenas o que o OddsService usa)
    public class ApiResponse<T>
    {
        [JsonPropertyName("get")]
        public string Get { get; set; } = string.Empty;
        
        [JsonPropertyName("parameters")]
        public JsonElement Parameters { get; set; }
        
        [JsonPropertyName("errors")]
        public JsonElement Errors { get; set; }
        
        [JsonPropertyName("results")]
        public int Results { get; set; }
        
        [JsonPropertyName("response")]
        public T Response { get; set; } = default!;
    }

    public class ApiMatch
    {
        [JsonPropertyName("fixture")]
        public ApiFixture Fixture { get; set; } = new();
        
        [JsonPropertyName("teams")]
        public ApiTeams Teams { get; set; } = new();
        
        [JsonPropertyName("odds")]
        public ApiOdds? Odds { get; set; }
    }

    public class ApiFixture
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
    }

    public class ApiTeams
    {
        [JsonPropertyName("home")]
        public ApiTeam Home { get; set; } = new();
        
        [JsonPropertyName("away")]
        public ApiTeam Away { get; set; } = new();
    }

    public class ApiTeam
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
    
    public class ApiOdds
    {
        [JsonPropertyName("home")]
        public decimal? Home { get; set; }
        
        [JsonPropertyName("draw")]
        public decimal? Draw { get; set; }
        
        [JsonPropertyName("away")]
        public decimal? Away { get; set; }
    }
}
