using System.Text.Json;

namespace Rag.Sample.Services
{
    public class EmbeddingService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public EmbeddingService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<float[]> GetEmbeddingAsync(string input)
        {
            // OpenAI embedding API call (simplified)
            var request = new { input, model = "text-embedding-3-small" };
            var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/embeddings", request);
            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            return json.GetProperty("data")[0].GetProperty("embedding").EnumerateArray().Select(e => e.GetSingle()).ToArray();
        }
    }
}
