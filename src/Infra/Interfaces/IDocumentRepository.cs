using Core.Models.Base;
using Infra.Entities;
using Task = System.Threading.Tasks.Task;

namespace Infra.Interfaces;

public interface IDocumentRepository
{
    Task<List<DocumentDto>> ListDocumentsByModel(string modelType, ulong modelId);
    Task AddDocuments(List<DocumentDto> documents, string modelType, ulong modelId);
    List<DocumentDto> UpdateDocuments(List<DocumentDto> documents);
    Task<List<DocumentDto>> ListDocumentsByIds(List<ulong> documentIds);
}