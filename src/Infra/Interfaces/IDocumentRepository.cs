using Core.Models.Base;
using Infra.Entities;

namespace Infra.Interfaces;

public interface IDocumentRepository
{
    Task<List<DocumentDto>> ListDocumentsByModel(string modelType, ulong modelId);
    void AddDocuments(List<Document> documents, string modelType, ulong modelId);
}