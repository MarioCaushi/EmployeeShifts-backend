using System.Net;
using EmployeeShift_backend.DTOs;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;

namespace EmployeeShift_backend.Errors;

public static class ExceptionsMiddlewareExtensions
{
    public static void ConfigureBuildInExceptionHandlers(this WebApplication app)
    {
        app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var contextRequest = context.Features.Get<IHttpRequestFeature>();

                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync(new ErrorDTO()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Path = contextRequest.Path,
                        }.ToString());
                    }
                });
            }
        );
    }
}