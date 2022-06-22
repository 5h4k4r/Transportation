using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Helpers;
using Core.Models.Base;
using Core.Models.Requests;
using Infra.Entities;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Infra.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;

    public DocumentRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public Task<List<DocumentDto>> ListDocumentsByModel(string modelType, ulong modelId)
    {
        return _context.Documents
            .Where(x => x.ModelType == modelType && x.ModelId == modelId)
            .ProjectTo<DocumentDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public Task AddDocuments(List<DocumentDto> docs, string modelType, ulong modelId)
    {
        var requestDocs = PrepareDocuments(docs.Select(x => x.Path));

        var docsToUpdate = requestDocs.Where(x => x.Path != null);

        requestDocs.ForEach(doc =>
        {
            doc.CreatedAt = DateTime.UtcNow;
            doc.UpdatedAt = DateTime.UtcNow;
            doc.ModelId = modelId;
            doc.ModelType = modelType;
        });
        return _context.Documents.AddRangeAsync(requestDocs);
    }

    public async Task<List<Document>> UpdateDocuments(List<DocumentDto> documents, string modelType,
        ulong modelId)
    {
        var requestDocs = PrepareDocuments(documents.Select(x => x.Path));

        var docsToUpdate = requestDocs.Where(x => x.Path != null);

        var databaseDocs = await _context.Documents
            .Where(x => x.ModelType == modelType && x.ModelId == modelId)
            .ToListAsync();

        foreach (var documentDto in databaseDocs)
        {
            documentDto.UpdatedAt = DateTime.UtcNow;
            documentDto.Path = docsToUpdate.First(x => x.Type == documentDto.Type).Path;
        }

        _context.Documents.UpdateRange(databaseDocs);

        return databaseDocs;
    }

    private List<Document?> PrepareDocuments(IEnumerable<string> docs)
    {
        var documents = new List<Document>();
        var namingPolicy = new SnakeCaseNamingPolicy();

        for (var index = 0; index < docs.GetType().GetProperties().Length; index++)
        {
            var p = docs.GetType().GetProperties()[index];
            if (
                p.Name is "Certificate" or "CertificateBack" or "NationalCardBack" or "Avatar" or "NationalCard"
            )
                documents.Add(new Document
                {
                    Type = namingPolicy.ConvertName(p.Name),
                    Path = p.GetValue(docs, null)?.ToString()
                });
        }

        return documents;
    }
}