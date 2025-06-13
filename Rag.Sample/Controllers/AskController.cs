using Microsoft.AspNetCore.Mvc;

namespace Rag.Sample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AskController : ControllerBase
    {
        private readonly EmbeddingService _embeddingService;
        private readonly VectorSearchService _vectorSearch;
        private readonly GptService _gptService;

        public AskController(EmbeddingService embeddingService, VectorSearchService vectorSearch, GptService gptService)
        {
            _embeddingService = embeddingService;
            _vectorSearch = vectorSearch;
            _gptService = gptService;
        }

        [HttpPost]
        public async Task<IActionResult> Ask([FromBody] AskRequest request)
        {
            var embedding = await _embeddingService.GetEmbeddingAsync(request.Question);
            var relatedChunks = _vectorSearch.Search(embedding, topK: 3);
            var context = string.Join("\n\n", relatedChunks);
            var prompt = $"Kullanıcının sorusunu aşağıdaki bağlamla birlikte cevapla:\n\n{context}\n\nSoru: {request.Question}";
            var answer = await _gptService.GetAnswerAsync(prompt);
            return Ok(new AskResponse { Answer = answer });
        }
    }
}
