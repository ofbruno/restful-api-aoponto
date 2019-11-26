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
    public static class ServicesExtensions
    {
        public static void ConfigurarMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options => options.UseMySql(config["ConexaoMySql:MySqlConnectionString"]));
        }

        public static void ConfigurarOpcoesJson(this IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                //options.SerializerSettings.ContractResolver = new SkipEmptyCollectionsContractResolver();
            });
        }

        public static void ConfigurarInjecoesDependencias(this IServiceCollection services)
        {
            services.AddScoped<IIdentificacao, Identificacao>();

            InjetarServicos(services);
            InjetarDaos(services);
            InjetarFiltros(services);
        }

        private static void InjetarServicos(IServiceCollection services)
        {
            services.AddScoped<IHomeServico, HomeServico>();
            services.AddScoped<ICidadeServico, CidadeServico>();
            services.AddScoped<IFrigorificoServico, FrigorificoServico>();
            services.AddScoped<INoticiaServico, NoticiaServico>();
            services.AddScoped<INotificacaoServico, NotificacaoServico>();
            services.AddScoped<IOfertaLoteServico, OfertaLoteServico>();
            services.AddScoped<IProdutorServico, ProdutorServico>();
            services.AddScoped<IPushServico, PushServico>();
            services.AddScoped<IRacaServico, RacaServico>();
            services.AddScoped<IUsuarioServico, UsuarioServico>();
            services.AddScoped<ILoginServico, LoginServico>();
        }

        private static void InjetarDaos(IServiceCollection services)
        {
            services.AddScoped<ICidadeDao, CidadeDao>();
            services.AddScoped<IEnderecoDao, EnderecoDao>();
            services.AddScoped<IFrigorificoDao, FrigorificoDao>();
            services.AddScoped<ILoteDao, LoteDao>();
            services.AddScoped<INoticiaDao, NoticiaDao>();
            services.AddScoped<INotificacaoDao, NotificacaoDao>();
            services.AddScoped<IProdutorDao, ProdutorDao>();
            services.AddScoped<IRacaDao, RacaDao>();
            services.AddScoped<IUsuarioDao, UsuarioDao>();
            services.AddScoped<IPushDao, PushDao>();
        }

        private static void InjetarFiltros(IServiceCollection services)
        {
            services.AddScoped<ModelValidationAttribute>();
        }
    }

    public class SkipEmptyCollectionsContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            var isDefaultValueIgnored = ((property.DefaultValueHandling ?? DefaultValueHandling.Ignore) & DefaultValueHandling.Ignore) != 0;

            if (!isDefaultValueIgnored || typeof(string).IsAssignableFrom(property.PropertyType) || !typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                return property;
            }

            Predicate<object> newShouldSerialize = obj =>
            {
                var collection = property.ValueProvider.GetValue(obj) as ICollection;
                return collection != null && collection.Count > 0;
            };

            var oldShouldSerialize = property.ShouldSerialize;

            property.ShouldSerialize = oldShouldSerialize != null
                                       ? o => oldShouldSerialize(o) && newShouldSerialize(o)
                                       : newShouldSerialize;

            return property;
        }
    }
}
