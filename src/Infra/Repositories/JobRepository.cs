using AutoMapper;
using Core.Interfaces;
using Core.Models.Requests;
using Infra.Entities;
using Infra.Interfaces;

namespace Infra.Repositories;

public class JobRepository : IJobRepository
{
    private readonly TransportationContext _context;
    private readonly IMapper _mapper;

    public JobRepository(TransportationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void NewJob(NewJobRequest request)
    {
        
    }
}