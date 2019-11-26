using aoponto.dados;
using aoponto.modelos;
using aoponto.servicos;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace aoponto.api
{
    public static class AppExtensions
    {

        public static IApplicationBuilder UseAutorizacao(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AutorizacaoMiddleware>();
        }

        public static IApplicationBuilder UseTratamentoExcecao(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TratamentoExcecaoMiddleware>();
        }

    }
}
