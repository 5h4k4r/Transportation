using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Models.Base;
using Core.Models.Requests;
using Infra.Entities;
using Infra.Extensions;
using Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using File = Infra.Entities.File;
using Task = System.Threading.Tasks.Task;

namespace Infra.Repositories;

public class FileRepository : IFileRepository
{
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;

    public FileRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task CreateFile(List<string> filePaths)
    {
        var files = new List<File>();
        foreach (var path in filePaths)
        {
            var file = new File
            {
                Path = path,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            files.Add(file);
        }

        await _context.Files.AddRangeAsync(files);
    }

    public async Task<List<FileDto>> ListFiles(ListFilesRequest request)
    {
        var file = await CheckForSearchField(_context.Files.AsQueryable(), request)
            .Select(x => x.Id)
            .ToListAsync();

        return await _context.Files.Where(x => file.Any(f => f == x.Id))
            .ProjectTo<FileDto>(_mapper.ConfigurationProvider)
            .ApplySorting(request)
            .ApplyPagination(request)
            .ToListAsync();
    }

    public async Task<int> CountFile(ListFilesRequest request)
    {
        var file = await CheckForSearchField(_context.Files.AsQueryable(), request)
            .Select(x => x.Id)
            .ToListAsync();

        return await _context.Files.Where(x => file.Any(f => f == x.Id))
            .CountAsync();
    }

    public async Task<ClientFile> CreateTranslateFile(CreateTranslateFileRequest request)
    {
        var translateFile = new ClientFile
        {
            FileId = request.FileId,
            LanguageId = request.LanguageId,
            Platform = request.Platform,
            Version = request.Version,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        var createdFile = await _context.ClientFiles.AddAsync(translateFile);
        return createdFile.Entity;
    }


    private IQueryable<File> CheckForSearchField(IQueryable<File> query, ListFilesRequest model)
    {
        if (model.SearchValue == FilterFileType.All)
            return query.Where(x =>
                x.FileModels.First().ModelType.Contains("App\\Models\\Service") ||
                x.FileModels.First().ModelType.Contains("App\\Models\\ServiceAreaType") || x.ClientFiles.Count != 0 ||
                x.FileModels.Count == 0);
        // .Where(x => x.File.ClientFiles.Count == 0);

        if (model.SearchValue == FilterFileType.Service)
            return query.Where(x => x.FileModels.First().ModelType.Contains("App\\Models\\Service"));
        if (model.SearchValue == FilterFileType.ServiceAreaType)
            return query.Where(x => x.FileModels.First().ModelType.Contains("App\\Models\\ServiceAreaType"));
        throw new ArgumentOutOfRangeException();
    }
}