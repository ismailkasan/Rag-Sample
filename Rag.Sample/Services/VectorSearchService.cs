namespace Rag.Sample.Services
{
    public class VectorSearchService
    {
        private readonly InMemoryVectorStore _store;

        public VectorSearchService(InMemoryVectorStore store)
        {
            _store = store;
        }

        public List<string> Search(float[] queryEmbedding, int topK)
        {
            return _store.Search(queryEmbedding, topK);
        }
    }
}
