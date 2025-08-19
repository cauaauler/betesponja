using System.Text.Json;
using FutebolSimplesBetsHub.Models;

namespace FutebolSimplesBetsHub.Services
{
    public class OddsService : IOddsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl = "https://v3.football.api-sports.io";

        public OddsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["FootballApi:ApiKey"] ?? "test_08d2254f9c8f4f1a2154748848142b";
            
            // Configurar headers da API
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", _apiKey);
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", "v3.football.api-sports.io");
        }
        

        public async Task<(decimal home, decimal draw, decimal away, DateTime matchTime, string realTeams)> GetRealisticOddsAsync()
        {
            // Lista de personagens do Bob Esponja
            var bobEsponjaCharacters = new[]
            {
                "Bob Esponja", "Patrick", "Lula Molusco", "Sandy", "Sr. Siriguejo", 
                "Plankton", "Karen", "Gary", "Sra. Puff", "Perola", "Larry Lagosta",
                "Tommy", "Harold", "Homem Sereia", "Holandês Voador", "Mexilhãozinho", "Netuno"
            };

            try
            {
                // Buscar partidas com odds da API
                var response = await _httpClient.GetAsync($"{_baseUrl}/odds?date={DateTime.Today:yyyy-MM-dd}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Response: {content.Substring(0, Math.Min(200, content.Length))}...");
                
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<ApiMatch>>>(content);

                if (apiResponse?.Response != null && apiResponse.Response.Any())
                {
                    // Filtrar partidas que têm odds
                    var matchesWithOdds = apiResponse.Response
                        .Where(match => match.Odds != null && 
                                      match.Odds.Home.HasValue && 
                                      match.Odds.Draw.HasValue && 
                                      match.Odds.Away.HasValue)
                        .ToList();

                    if (matchesWithOdds.Any())
                    {
                        // Pegar uma partida aleatória que tem odds
                        var apiRandom = new Random();
                        var randomMatch = matchesWithOdds[apiRandom.Next(matchesWithOdds.Count)];
                        
                        // Extrair horário da partida
                        var matchTime = DateTime.Parse(randomMatch.Fixture.Date.ToString());
                        
                        // Verificar se a partida é futura
                        if (matchTime > DateTime.Now)
                        {
                            // Gerar personagens aleatórios do Bob Esponja
                            var apiHomeCharacter = bobEsponjaCharacters[apiRandom.Next(bobEsponjaCharacters.Length)];
                            var apiAwayCharacter = bobEsponjaCharacters[apiRandom.Next(bobEsponjaCharacters.Length)];
                            
                            // Garantir que não seja o mesmo personagem
                            while (apiAwayCharacter == apiHomeCharacter)
                            {
                                apiAwayCharacter = bobEsponjaCharacters[apiRandom.Next(bobEsponjaCharacters.Length)];
                            }
                            
                            var apiCharacterMatch = $"{apiHomeCharacter} vs {apiAwayCharacter}";
                            
                            // Usar odds reais da API
                            var homeOdds = randomMatch.Odds!.Home!.Value;
                            var drawOdds = randomMatch.Odds!.Draw!.Value;
                            var awayOdds = randomMatch.Odds!.Away!.Value;
                            
                            Console.WriteLine($"Usando odds reais da API: {homeOdds}, {drawOdds}, {awayOdds}");
                            
                            return (homeOdds, drawOdds, awayOdds, matchTime, apiCharacterMatch);
                        }
                        else
                        {
                            Console.WriteLine("Partida da API é passada, usando fallback");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nenhuma partida com odds encontrada na API");
                    }
                }
                else
                {
                    Console.WriteLine("Nenhuma partida encontrada na API");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar odds da API: {ex.Message}");
            }

            // Fallback: usar personagens do Bob Esponja aleatórios com horários futuros
            var random = new Random((int)DateTime.Now.Ticks); // Usar timestamp como seed para variação
            var homeCharacter = bobEsponjaCharacters[random.Next(bobEsponjaCharacters.Length)];
            var awayCharacter = bobEsponjaCharacters[random.Next(bobEsponjaCharacters.Length)];
            
            // Garantir que não seja o mesmo personagem
            while (awayCharacter == homeCharacter)
            {
                awayCharacter = bobEsponjaCharacters[random.Next(bobEsponjaCharacters.Length)];
            }
            
            var characterMatch = $"{homeCharacter} vs {awayCharacter}";
            
            // Gerar horário futuro variado (próximos 7 dias, entre 14h e 23h)
            var daysFromNow = random.Next(0, 7);
            var hours = random.Next(14, 23);
            var minutes = random.Next(0, 60);
            var fallbackTime = DateTime.Now.AddDays(daysFromNow).Date.AddHours(hours).AddMinutes(minutes);
            
            // Adicionar variação adicional para garantir que não seja sempre o mesmo horário
            var additionalMinutes = random.Next(-30, 30);
            fallbackTime = fallbackTime.AddMinutes(additionalMinutes);
            
            Console.WriteLine("Usando fallback com odds aleatórias");
            
            return (
                GetRandomOddsFromRange(1.5m, 3.5m),
                GetRandomOddsFromRange(2.5m, 4.0m),
                GetRandomOddsFromRange(1.5m, 3.5m),
                fallbackTime,
                characterMatch
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
    }
}
