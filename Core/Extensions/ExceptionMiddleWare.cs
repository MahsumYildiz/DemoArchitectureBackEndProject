using FluentValidation.Results;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class ExceptionMiddleWare
    {
        private RequestDelegate _next;

        public ExceptionMiddleWare(RequestDelegate next) 
        {
            _next = next;
        }
        public async Task  InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {

                await HandleExceptionAsync(httpContext, e);

            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType= "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "Internal Server Error";
            IEnumerable<ValidationFailure> errors = null;
            if (e is FluentValidation.ValidationException validationException)
            {
                message = validationException.Message;
                errors = validationException.Errors;

                await httpContext.Response.WriteAsync(new ValidationErrorDetails
                {
                    Errors = errors,
                    Message = message,
                    StatusCode = 400
                }.ToString());

                return;
            }

            await httpContext.Response.WriteAsync(new ErrorHandlerDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = e.Message
            }.ToString());
        }
    }
}

