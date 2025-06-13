# 🧠 RAG.NetCoreSample

This project is a simple **Retrieval-Augmented Generation (RAG)** system built with .NET Core. When a user submits a question, relevant content is first retrieved from local documents, then used as context for a large language model (LLM) to generate a meaningful response.

## 🚀 Features
- .NET Core Web API architecture
- Support for OpenAI (GPT-4) or **local Ollama** models
- Simple `in-memory` vector store
- Embedding (text-to-vector conversion) and similarity search
- Easily extensible service-based structure

---

## 🧱 Project Structure

```
📁 RAG.NetCoreSample
├── Controllers/AskController.cs          // Handles user questions and returns answers
├── Services/
│   ├── EmbeddingService.cs               // Embedding logic (OpenAI)
│   ├── GptService.cs                     // GPT answer generation (OpenAI)
│   └── VectorSearchService.cs           // Similarity search
├── Data/InMemoryVectorStore.cs          // Vector storage and retrieval
├── Models/
│   ├── AskRequest.cs
│   └── AskResponse.cs
├── Program.cs                            // Dependency injection and app setup
├── appsettings.json                      // API configuration
└── README.md                             // This file
```

---

## ⚙️ Setup

### 1. Requirements
- [.NET 7+ SDK](https://dotnet.microsoft.com/download)
- [Ollama](https://ollama.com) (optional, for local LLM)
- OpenAI API Key (for embedding and GPT)

### 2. Add OpenAI Key
In your `appsettings.json`:
```json
{
  "OpenAI": {
    "ApiKey": "sk-xxxx"
  }
}
```

### 3. Run the App
```bash
dotnet build
dotnet run
```

---

## 🔍 Example Usage

### POST `/api/ask`
```json
{
  "question": "How is provisioning done for Biotekno products?"
}
```

### Response:
```json
{
  "answer": "To perform provisioning for Biotekno products..."
}
```

---

## 🧪 Add Test Data

At startup, you can load some sample embeddings:

```csharp
var chunks = TextSplitter.SplitIntoChunks(File.ReadAllText("docs.txt"));
foreach (var chunk in chunks)
{
    var emb = await embeddingService.GetEmbeddingAsync(chunk);
    vectorStore.AddChunk(chunk, emb);
}
```

---

## 🔄 Using with Ollama (Local LLM)

1. Install Ollama:
```bash
curl -fsSL https://ollama.com/install.sh | sh
```

2. Pull a model:
```bash
ollama run mistral
```

3. Replace `GptService` with your own `OllamaService`, and call the `/api/generate` endpoint using HTTP POST with your prompt.

---

## 📌 Notes

- `EmbeddingService` currently uses OpenAI. You can integrate local embeddings via `.NET + Python` bridge.
- `InMemoryVectorStore` is for demo use only. In production, consider using FAISS, Qdrant, or similar vector DBs.

---

## 📬 Contribution & License
This project is open-source under the MIT License. Feel free to fork and contribute via pull requests.

---

**Author:** ismail Kaşan / 2025 ✨