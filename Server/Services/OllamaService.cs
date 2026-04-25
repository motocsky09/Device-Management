using System.Text;
using System.Text.Json;

namespace Server.Services
{
    public class OllamaService
    {
        private readonly HttpClient _httpClient;
        private const string OllamaUrl = "http://localhost:11434/api/generate";
        private const string Model = "gemma2:2b";

        public OllamaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GenerateDeviceDescription(string name, string manufacturer, string type, string os, string processor, int ram)
        {
            var prompt = $"Generate a short, professional one-sentence description for a device with these specs: " +
                         $"Name: {name}, Manufacturer: {manufacturer}, Type: {type}, " +
                         $"OS: {os}, Processor: {processor}, RAM: {ram}GB. " +
                         $"Return only the description sentence, nothing else.";

            var requestBody = new
            {
                model = Model,
                prompt = prompt,
                stream = false
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(OllamaUrl, content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<JsonElement>(responseJson);

            return result.GetProperty("response").GetString()?.Trim() ?? "No description generated.";
        }
    }
}