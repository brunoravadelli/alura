using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Alura.ListaLeitura.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var _repo = new LivroRepositorioCSV();

            IWebHost webHost = new WebHostBuilder()
                .UseKestrel()     //User a implementação do Kestrel (qual o servidor que implementa o hsot http).
                .UseStartup<Startup>()
                .Build();

            webHost.Run();

        }

        private static void TestesConsole(LivroRepositorioCSV _repo)
        {
            ImprimeLista(_repo.ParaLer);
            ImprimeLista(_repo.Lendo);
            ImprimeLista(_repo.Lidos);
        }

        static void ImprimeLista(ListaDeLeitura lista)
        {
            Console.WriteLine(lista);
        }
    }
}
