using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Alura.ListaLeitura.App.Html
{
    public class HtmlUtils
    {
        public static string CarregaArquivoHTML(string nomeArquivo)
        {
            var directory = @"C:\Users\Bruno Ravadelli\Documents\AspNetCore\Inicial\Alura.ListaLeitura\Alura.ListaLeitura.App";

            var nomeCompletoArquivo = $@"{directory}\Html\{nomeArquivo}.html";

            using (var arquivo = File.OpenText(nomeCompletoArquivo))
            {
                return arquivo.ReadToEnd();
            }
        }
    }
}
