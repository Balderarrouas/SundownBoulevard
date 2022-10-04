using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SundownBoulevard.Exceptions
{
    public class ErrorHandlingMiddleware
    {

        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }

            catch (Exception error)
            {
                var response = httpContext.Response;
                response.ContentType = "application/json";
                var responseModel = ApiResponse<string>.Fail(error.Message);
                switch (error)
                {
                    case UnauthorizedAccessException e:
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        break;
                    
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }


    }
}