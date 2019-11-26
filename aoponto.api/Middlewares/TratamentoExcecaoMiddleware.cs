using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aoponto.api
{
    public class TratamentoExcecaoMiddleware
    {
        private readonly RequestDelegate _next;

        public TratamentoExcecaoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                TratarExcecao(httpContext, ex);
            }
        }

        private void TratarExcecao(HttpContext httpContext, Exception ex)
        {
            //TODO: Log
        }
    }
}
