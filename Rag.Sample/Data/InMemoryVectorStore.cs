namespace Rag.Sample.Data
{
    public class InMemoryVectorStore
    {
        public record Chunk(string Text, float[] Embedding);
        private List<Chunk> _chunks = new();

        public void AddChunk(string text, float[] embedding) => _chunks.Add(new Chunk(text, embedding));

        public List<string> Search(float[] query, int topK)
        {
            return _chunks.OrderBy(c => CosineDistance(query, c.Embedding)).Take(topK).Select(c => c.Text).ToList();
        }

        private float CosineDistance(float[] v1, float[] v2)
        {
            float dot = 0, normA = 0, normB = 0;
            for (int i = 0; i < v1.Length; i++)
            {
                dot += v1[i] * v2[i];
                normA += v1[i] * v1[i];
                normB += v2[i] * v2[i];
            }
            return 1 - dot / ((float)Math.Sqrt(normA) * (float)Math.Sqrt(normB));
        }
    }
}
