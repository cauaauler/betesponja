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

    // Fixtures (para pegar horário real)
    public class ApiMatch
    {
        [JsonPropertyName("fixture")]
        public ApiFixture Fixture { get; set; } = new();
        
        [JsonPropertyName("teams")]
        public ApiTeams Teams { get; set; } = new();
    }

    public class ApiFixture
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
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
    
    // Odds (bookmakers/bets/values)
    public class ApiOddsEntry
    {
        [JsonPropertyName("fixture")]
        public ApiOddsFixture Fixture { get; set; } = new();
        
        [JsonPropertyName("bookmakers")]
        public List<ApiBookmaker> Bookmakers { get; set; } = new();
    }

    public class ApiOddsFixture
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }

    public class ApiBookmaker
    {
        [JsonPropertyName("bets")]
        public List<ApiBet> Bets { get; set; } = new();
    }

    public class ApiBet
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        
        [JsonPropertyName("values")]
        public List<ApiOddValue> Values { get; set; } = new();
    }

    public class ApiOddValue
    {
        [JsonPropertyName("value")]
        public string Value { get; set; } = string.Empty; // "Home", "Draw", "Away"
        
        [JsonPropertyName("odd")]
        public string Odd { get; set; } = "0"; // string numérica
    }
}
