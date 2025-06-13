using System.Text.Json;

namespace Rag.Sample.Services
{
    public class GptService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public GptService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<string> GetAnswerAsync(string prompt)
        {
            var body = new
            {
                model = "gpt-4",
                messages = new[]
                {
                new { role = "system", content = "Bir teknik asistan olarak davran." },
                new { role = "user", content = prompt }
            }
            };

            var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", body);
            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            return json.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
        }
    }

}
