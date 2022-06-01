using Core.Interfaces;
using Core.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Infra.Mapper;

public class ExceptionMapper : IExceptionMapper
{
    private readonly IUnitOfWork _unitOfWork;

    public ExceptionMapper(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void MapException(Exception e)
    {
        if (e is DbUpdateException)
        {
            // get exception error code and message

            var exceptionNumber = (e.InnerException as MySqlException).Number;

            switch (exceptionNumber)
            {
                case 1062:
                    throw new DuplicateException();
                case 1451:
                    throw new ForeignKeyException();
                case 1048:
                    throw new NotNullException();
                case 1054:
                    throw new NotFoundException();
            }
        }

        throw e;
    }
}