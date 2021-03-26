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
    public class CadastroLogica
    {
        public static Task ProcessaFormulario(HttpContext context)
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

        public static Task ExibeFormulario(HttpContext context)
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

            html = HtmlUtils.CarregaArquivoHTML("formulario");

            return context.Response.WriteAsync(html);
        }

        public static Task NovoLivroParaLer(HttpContext context)
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
    }
}
