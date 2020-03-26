using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DMAnaliseSentimento.Web.API.Analisadores;
using DMAnaliseSentimento.Web.API.Domain;
using DMAnaliseSentimentoML.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.ML;
using Microsoft.Extensions.DependencyInjection;
namespace DMAnaliseSentimento.Web.API.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class AnaliseComentarioController : Controller
    {
        public IConfiguration Configuration { get; }

        public AnaliseComentarioController(IConfiguration configuration) => Configuration = configuration;

        [HttpPost]
        public IActionResult Post([FromBody] string texto, 
                                [FromServices]IMemoryCache cache)
        {
            var analisador = new AnalisadorML(Configuration);
            var predictionEngine = cache.GetOrCreate("predictionEngine", analisador.GetPrediction);
            
            return Ok(analisador.ConsumeModel(texto, predictionEngine));
        }

    
    }
}