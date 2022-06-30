using Core.Models.Base;
using Core.Models.Requests;
using Infra.Entities;
using Task = System.Threading.Tasks.Task;

namespace Infra.Interfaces;

public interface IFileRepository
{
    Task CreateFile(List<string> filePaths);

    Task<ClientFile> CreateTranslateFile(CreateTranslateFileRequest request);

    Task<List<FileDto>> ListFiles(ListFilesRequest request);

    Task<int> CountFile(ListFilesRequest request);
}