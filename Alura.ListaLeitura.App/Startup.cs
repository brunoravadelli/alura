using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

            builder.MapRoute("Livros/ParaLer", LivrosParaLer);
            builder.MapRoute("Livros/Lendo", LivrosLendo);
            builder.MapRoute("Livros/Lidos", LivrosLidos);
            builder.MapRoute("Cadastro/NovoLivro/{nome}/{autor}", NovoLivroParaLer);
            builder.MapRoute("Livros/Detalhes/{id:int}", ExibeDetalhes);
            builder.MapRoute("Cadastro/NovoLivro", ExibeFormulario);
            builder.MapRoute("Cadastro/Incluir", ProcessaFormulario);

            var rotas = builder.Build();

            app.UseRouter(rotas);

            //app.Run(Roteamento); //método que será executado quando a requisição chegar 
        }

        private Task ProcessaFormulario(HttpContext context)
        {
            var livro = new Livro()
            {
                Titulo = context.Request.Form["titulo"].First(),
                Autor = context.Request.Form["autor"].First(),
            };

            var repo = new LivroRepositorioCSV();

            repo.Incluir(livro);

            return context.Response.WriteAsync("Formulário de livro inserido com sucesso!");
        }

        private Task ExibeFormulario(HttpContext context)
        {

            var html = @"
            <html>
                <form action='/Cadastro/Incluir'>

                    <label>Título:</label>
                    <input name='titulo'/>
                    <br/>

                    <label>Autor:</label>
                    <input name='autor'/>
                    <br/>

                    <button> Gravar </button>
                </form>
            </html>";

            html = CarregaArquivoHTML("formulario");

            return context.Response.WriteAsync(html);
        }

        private string CarregaArquivoHTML(string nomeArquivo)
        {
            var directory = @"C:\Users\Bruno Ravadelli\Documents\AspNetCore\Inicial\Alura.ListaLeitura\Alura.ListaLeitura.App";

            var nomeCompletoArquivo = $@"{directory}\Html\{nomeArquivo}.html";

            using (var arquivo = File.OpenText(nomeCompletoArquivo))
            {
                return arquivo.ReadToEnd();
            }
        }

        private Task ExibeDetalhes(HttpContext context)
        {
            int id = Convert.ToInt32(context.GetRouteValue("id"));

            var repo = new LivroRepositorioCSV();

            var livro = repo.Todos.First(x => x.Id == id);

            return context.Response.WriteAsync(livro.Detalhes());
        }

        private Task NovoLivroParaLer(HttpContext context)
        {
            var livro = new Livro()
            {
                Titulo = context.GetRouteValue("nome").ToString(),
                Autor = context.GetRouteValue("autor").ToString()
            };

            var repo = new LivroRepositorioCSV();

            repo.Incluir(livro);

            return context.Response.WriteAsync("O livro foi adicionado com sucesso");
        }

        public Task Roteamento(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();

            var caminhosAtendidos = new Dictionary<string, RequestDelegate>
            {
                {"/Livros/ParaLer", LivrosParaLer},
                {"/Livros/Lendo", LivrosLendo},
                {"/Livros/Lidos", LivrosLidos}
            };

            if (caminhosAtendidos.ContainsKey(context.Request.Path))
            {
                var metodo = caminhosAtendidos[context.Request.Path];
                return metodo.Invoke(context);
            }

            context.Response.StatusCode = 404;

            return context.Response.WriteAsync("Caminho inexistente!");
        }

        public Task LivrosParaLer(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();

            var conteudoArquivo = CarregaArquivoHTML("para-ler");

            foreach (var livro in _repo.ParaLer.Livros)
            {
                conteudoArquivo = conteudoArquivo
                    .Replace("#NOVO-ITEM#", $"<li>{livro.Titulo} - {livro.Autor}</li>#NOVO-ITEM#");
            }

            conteudoArquivo = conteudoArquivo.Replace("#NOVO-ITEM#", "");

            return context.Response.WriteAsync(conteudoArquivo);
        }

        public Task LivrosLendo(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();

            var fileContent = CarregaArquivoHTML("lendo");

            foreach (var livro in _repo.Lendo.Livros)
            {
                fileContent = fileContent
                    .Replace("#NOVO-ITEM#", $"<li>{livro.Autor} - {livro.Titulo} #NOVO-ITEM#</li>");
            }

            fileContent = fileContent.Replace("#NOVO-ITEM#", "");

            return context.Response.WriteAsync(fileContent);
        }

        public Task LivrosLidos(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            var fileContent = CarregaArquivoHTML("lidos");

            foreach (var livro in _repo.Lidos.Livros)
            {
                fileContent = fileContent
                    .Replace("#NOVO-ITEM#", $"<li>{livro.Titulo} - {livro.Autor} #NOVO-ITEM#</li>");
            }

            fileContent = fileContent.Replace("#NOVO-ITEM#", "");

            return context.Response.WriteAsync(fileContent);
        }
    }
}