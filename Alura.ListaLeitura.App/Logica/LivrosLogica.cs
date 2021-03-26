using Alura.ListaLeitura.App.Html;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Logica
{
    public class LivrosLogica
    {
        public static Task ExibeDetalhes(HttpContext context)
        {
            int id = Convert.ToInt32(context.GetRouteValue("id"));

            var repo = new LivroRepositorioCSV();

            var livro = repo.Todos.First(x => x.Id == id);

            return context.Response.WriteAsync(livro.Detalhes());
        }

        public static Task LivrosParaLer(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();

            var conteudoArquivo = HtmlUtils.CarregaArquivoHTML("para-ler");

            foreach (var livro in _repo.ParaLer.Livros)
            {
                conteudoArquivo = conteudoArquivo
                    .Replace("#NOVO-ITEM#", $"<li>{livro.Titulo} - {livro.Autor}</li>#NOVO-ITEM#");
            }

            conteudoArquivo = conteudoArquivo.Replace("#NOVO-ITEM#", "");

            return context.Response.WriteAsync(conteudoArquivo);
        }

        public static string CarregaLista(IEnumerable<Livro> livros)
        {
            var conteudoArquivo = HtmlUtils.CarregaArquivoHTML("lista");

            foreach (var livro in livros)
            {
                conteudoArquivo = conteudoArquivo
                    .Replace("#NOVO-ITEM", $"<li>{livro.Titulo} - {livro.Autor}");
            }

            return conteudoArquivo.Replace("#NOVO-ITEM", "");
        }

        public static Task LivrosLendo(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();

            var fileContent = HtmlUtils.CarregaArquivoHTML("lendo");

            foreach (var livro in _repo.Lendo.Livros)
            {
                fileContent = fileContent
                    .Replace("#NOVO-ITEM#", $"<li>{livro.Autor} - {livro.Titulo} #NOVO-ITEM#</li>");
            }

            fileContent = fileContent.Replace("#NOVO-ITEM#", "");

            return context.Response.WriteAsync(fileContent);
        }

        public static Task LivrosLidos(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            var fileContent = HtmlUtils.CarregaArquivoHTML("lidos");

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
