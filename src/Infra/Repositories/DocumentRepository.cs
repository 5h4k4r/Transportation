using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Models.Base;
using Infra.Entities;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    public void AddDocuments(List<Document> docs, string modelType, ulong modelId)
    {
        docs.ForEach(doc =>
        {
            doc.CreatedAt = DateTime.UtcNow;
            doc.UpdatedAt = DateTime.UtcNow;
            doc.ModelId = modelId;
            doc.ModelType = modelType;
        });
        _context.Documents.AddRange(docs);
    }
}