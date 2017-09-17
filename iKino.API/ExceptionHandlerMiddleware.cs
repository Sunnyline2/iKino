using iKino.API.Extensions;
using iKino.API.Models;
using iKino.API.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace iKino.API
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionHandlerMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception.GetType() == typeof(ServiceException))
            {
                return Handlers.HandleServiceExceptionAsync(context, (ServiceException)exception);
            }

            if (exception.GetType() == typeof(Exception))
            {
                return Handlers.HandleExceptionAsync(context, (ServiceException)exception);
            }
            throw new Exception(string.Empty, exception);
        }

        public static class Handlers
        {
            public static Task HandleExceptionAsync(HttpContext context, ServiceException exception)
            {
                context.SetAsJson(StatusCodes.Status500InternalServerError);
                return context.Response.WriteAsync(JsonConvert.SerializeObject(ResponseBody.Create(exception.Message)));
            }

            public static Task HandleServiceExceptionAsync(HttpContext context, ServiceException exception)
            {
                context.SetAsJson(StatusCodes.Status400BadRequest);
                return context.Response.WriteAsync(JsonConvert.SerializeObject(ResponseBody.Create(exception.Message)));
            }
        }
    }
}

