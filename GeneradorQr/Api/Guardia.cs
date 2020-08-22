using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GeneradorQr.Api
{
    class Guardia
    {
        public async Task FiltrarRutas(HttpContext context, Func<Task> next)
        {
            var request = context.Request;
            var response = context.Response;

            if (request.Path != "/")
            {
                response.StatusCode = 404;
                await response.WriteAsync("NotFound");
            }
            else
            {
                await next.Invoke();
            }
        }
    }
}