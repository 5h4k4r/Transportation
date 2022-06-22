using Core.Models.Base;
using Infra.Entities;
using Task = System.Threading.Tasks.Task;

namespace Infra.Interfaces;

public interface IDocumentRepository
{
    Task<List<DocumentDto>> ListDocumentsByModel(string modelType, ulong modelId);
    Task AddDocuments(List<Document> documents, string modelType, ulong modelId);
    Task<List<Document>> UpdateDocuments(List<Document> documents, string modelType, ulong modelId);
}