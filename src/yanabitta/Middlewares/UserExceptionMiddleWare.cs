using Microsoft.AspNetCore.Http;
using Services.Exceptions;
using System;
using System.Threading.Tasks;

namespace yanabitta.Middlewares
{
    public class UserExceptionMiddleWare
    {
        private readonly RequestDelegate next;

        public UserExceptionMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }

            catch (UserException ex)
            {
                await ExceptionHandle(context, ex.Code, ex.Message);
            }

            catch (Exception ex)
            {
                await ExceptionHandle(context, 500, ex.Message);
            }
        }

        private async Task ExceptionHandle(HttpContext context, int code, string message)
        {
            context.Response.StatusCode = code;
            await context.Response.WriteAsJsonAsync(new
            {
                Code = code,
                Message = message
            });
        }
    }
}