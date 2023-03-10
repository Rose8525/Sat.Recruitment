using Microsoft.AspNetCore.Http;
using Sat.Recruitment.Application.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Domain.Exceptions;

namespace Sat.Recruitment.Api.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                _logger.LogError($"An exception ocurred. Error: {error.Message}");
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case InvalidUserTypeException _:
                    case DuplicatedUserException _:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = CreateResponse(context, error);

                await response.WriteAsync(result);
            }
        }

        private static string CreateResponse(HttpContext context, Exception error)
        {
            string json;
            var version = context.GetRequestedApiVersion();
            if (version?.MajorVersion == 1)
            {
                var resultV1 = new ResultV1 { IsSuccess = false};
                resultV1.Errors.Add(error.Message);
                json = JsonSerializer.Serialize(resultV1);
            }
            else
            {
                json = JsonSerializer.Serialize(new Result
                {
                    IsSuccess = false,
                    Errors = error.Message
                });
            }

            return json;
        }
    }
}

