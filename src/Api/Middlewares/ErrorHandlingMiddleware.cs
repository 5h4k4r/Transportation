using System.Net;
using System.Text.Json;
using Core.Interfaces;
using Core.Models.Common;
using Core.Models.Exceptions;

namespace Api.Middlewares;

public class ErrorHandlingMiddleware : IMiddleware
{
    private readonly IExceptionMapper _mapper;

    public ErrorHandlingMiddleware(IExceptionMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            try
            {
                _mapper.MapException(e);
            }
            catch (NotFoundException exception)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync(JsonSerializer.Serialize(BasicResponse.ResourceNotFound));
            }
            catch (DuplicateException exception)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(JsonSerializer.Serialize(BasicResponse.ResourceAlreadyExists));
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(JsonSerializer.Serialize(BasicResponse.Unknown));
            }
        }
    }
}