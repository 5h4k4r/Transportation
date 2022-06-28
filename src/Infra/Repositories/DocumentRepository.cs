using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Helpers;
using Core.Models.Base;
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
    public Task<List<DocumentDto>> ListDocumentsByIds(List<ulong> documentIds)
    {
        return _context.Documents
            .Where(x=>documentIds.Any(s=>x.Id == s))
            .ProjectTo<DocumentDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public Task AddDocuments(List<DocumentDto> docs, string modelType, ulong modelId)
    {
        if (docs == null) throw new ArgumentNullException(nameof(docs));
        
        var documents = docs.Select(document => _mapper.Map<Document>(new DocumentDto
            {
                CreatedAt = DateTime.UtcNow, 
                UpdatedAt = DateTime.UtcNow, 
                ModelId = modelId, 
                ModelType = modelType,
                Path = document.Path,
                Type = document.Type
            }))
            .ToList();

        return _context.Documents.AddRangeAsync(documents);
    }

        
    

    public List<DocumentDto> UpdateDocuments(List<DocumentDto> documentsDto)
    {
        var documents = new List<Document>();
        foreach (var document in documentsDto) documents.Add(_mapper.Map<Document>(document));

        _context.Documents.UpdateRange(documents);

        return documentsDto;
    }

    // public async Task VerifyDocuments(VerifyDocumentsRequest model)
    // {
    //     var query = 
    // }

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