using DMAnaliseSentimento.Web.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DMAnaliseSentimento.Web.API.EntityContext
{
    public class DbInitializer
    {
        public static void SeedDataSet(DMAnaliseSentimentoContext context, IConfiguration configuration)
        {
            context.Database.EnsureCreated();
            if (context.DataSet.Any())            
                return;
            
            using var streamReader = new StreamReader(configuration.GetSection("PathAnalise:Path").Value + "\\wikipedia-detox-250-line-data.tsv");
            while (streamReader.Peek() >= 0)
            {
                var linha = streamReader.ReadLine();
                if (!string.IsNullOrEmpty(linha))
                {
                    var vetorPredicadoTexto = linha.Split('\t');

                    if (vetorPredicadoTexto == null || vetorPredicadoTexto.Length != 2)                    
                        continue;

                    if (byte.TryParse(vetorPredicadoTexto[0].ToString(), out byte predicado))
                    {
                        DataSet entity = new DataSet
                        {
                            Predicado = predicado,
                            Texto = vetorPredicadoTexto[1]
                        };
                        context.DataSet.Add(entity);
                    }
                }                
            }

            context.SaveChanges();
        }
    }
}
