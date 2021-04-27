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
            //services.AddRouting(); //significa que minha aplicação esta usando serviço de adição de roteamento do asp net core . 
            //o mvc já utiliza o AddRouting dentro dele
            services.AddMvc(); //informa a aplicação qeu quero utilizar o framework mvc
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvcWithDefaultRoute();
        }        
    }
}