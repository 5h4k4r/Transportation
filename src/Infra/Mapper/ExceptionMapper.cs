using Core.Interfaces;
using Core.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infra.Mapper;

public class ExceptionMapper : IExceptionMapper
{
    public void MapException(Exception e)
    {
        if (e is DbUpdateException)
            throw new NotFoundException();

        throw e;
    }
}