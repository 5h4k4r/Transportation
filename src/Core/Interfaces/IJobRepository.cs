using Core.Models.Requests;

namespace Core.Interfaces;

public interface IJobRepository
{
    void NewJob(NewJobRequest request);
}