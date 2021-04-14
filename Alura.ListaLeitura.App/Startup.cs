using Alura.ListaLeitura.App.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(); //significa que minha aplicação esta usando serviço de adição de roteamento do asp net core . 
        }

        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);

            builder.MapRoute("{classe}/{metodo}", RoteamentoPadrao.TratamentoPadrao);

            var rotas = builder.Build();

            app.UseRouter(rotas);
        }        
    }
}