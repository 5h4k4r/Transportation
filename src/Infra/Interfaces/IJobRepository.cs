using Core.Models.Requests;

namespace Infra.Interfaces;

public interface IJobRepository
{
    void NewJob(NewJobRequest request);
}