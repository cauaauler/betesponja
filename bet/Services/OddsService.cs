using System.Text.Json;
using System.Globalization;
using FutebolSimplesBetsHub.Models;

namespace FutebolSimplesBetsHub.Services
{
    public class OddsService : IOddsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public OddsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _config = configuration;
        }
        
        public async Task<(decimal home, decimal draw, decimal away, DateTime matchTime, string realTeams)> GetRealisticOddsAsync()
        {
            var characters = new[]
            {
                "Bob Esponja", "Patrick", "Lula Molusco", "Sandy", "Sr. Siriguejo", 
                "Plankton", "Karen", "Gary", "Sra. Puff", "Perola", "Larry Lagosta",
                "Tommy", "Harold", "Homem Sereia", "Holandês Voador", "Mexilhãozinho", "Netuno"
            };

            try
            {
                // The Odds API params
                var apiKey = _config["OddsApi:ApiKey"] ?? "";
                var regions = "eu"; // Europe
                var markets = "h2h"; // 1X2
                var dateFormat = "unix";
                var sport = "soccer_epl"; // exemplo: Premier League; pode-se trocar/expandir

                var url = $"https://api.the-odds-api.com/v4/sports/{sport}/odds/?apiKey={apiKey}&regions={regions}&markets={markets}&dateFormat={dateFormat}";
                var res = await _httpClient.GetAsync(url);
                res.EnsureSuccessStatusCode();
                var json = await res.Content.ReadAsStringAsync();
                var events = JsonSerializer.Deserialize<List<OddsApiEvent>>(json);

                if (events != null && events.Any())
                {
                    // Escolher evento futuro
                    var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    var candidate = events.FirstOrDefault(e => new DateTimeOffset(e.CommenceTime).ToUnixTimeSeconds() > now);
                    candidate ??= events.First();

                    // Logar no console as partidas (até 5) para referência
                    Console.WriteLine("Odds API - exemplos de eventos:");
                    foreach (var ev in events.Take(5))
                    {
                        Console.WriteLine($" - {ev.HomeTeam} vs {ev.AwayTeam} @ {ev.CommenceTime:o}");
                    }

                    // Pegar mercado h2h do primeiro bookmaker com dados
                    foreach (var bm in candidate.Bookmakers)
                    {
                        var h2h = bm.Markets.FirstOrDefault(m => m.Key == "h2h");
                        if (h2h == null) continue;

                        decimal? home = null, draw = null, away = null;
                        foreach (var o in h2h.Outcomes)
                        {
                            var name = o.Name.ToLowerInvariant();
                            if (name.Contains("draw")) draw = o.Price;
                            else if (name == candidate.HomeTeam.ToLowerInvariant()) home = o.Price;
                            else if (name == candidate.AwayTeam.ToLowerInvariant()) away = o.Price;
                        }

                        if (home.HasValue && draw.HasValue && away.HasValue)
                        {
                            // Personagens aleatórios distintos
                            var rnd = new Random();
                            var c1 = characters[rnd.Next(characters.Length)];
                            var c2 = characters[rnd.Next(characters.Length)];
                            while (c2 == c1) c2 = characters[rnd.Next(characters.Length)];

                            return (home.Value, draw.Value, away.Value, candidate.CommenceTime, $"{c1} vs {c2}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro The Odds API: {ex.Message}");
            }

            // Fallback
            var random = new Random((int)DateTime.Now.Ticks);
            var p1 = characters[random.Next(characters.Length)];
            var p2 = characters[random.Next(characters.Length)];
            while (p2 == p1) p2 = characters[random.Next(characters.Length)];
            var fallbackTime = DateTime.Now.AddDays(random.Next(0, 7)).Date.AddHours(random.Next(14, 23)).AddMinutes(random.Next(0, 60));
            return (
                GetRandomOddsFromRange(1.5m, 3.5m),
                GetRandomOddsFromRange(2.5m, 4.0m),
                GetRandomOddsFromRange(1.5m, 3.5m),
                fallbackTime,
                $"{p1} vs {p2}"
            );
        }

        public async Task<decimal> GetRandomOddsAsync(decimal min, decimal max)
        {
            return GetRandomOddsFromRange(min, max);
        }

        private static decimal GetRandomOddsFromRange(decimal min, decimal max)
        {
            var random = new Random();
            var value = (decimal)(random.NextDouble() * (double)(max - min) + (double)min);
            return Math.Round(value, 2);
        }

        private static decimal? ParseOdd(string s)
        {
            if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var d)) return Math.Round(d, 2);
            if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out d)) return Math.Round(d, 2);
            return null;
        }
    }
}
