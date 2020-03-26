using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMAnaliseSentimento.Web.API.Domain;
using DMAnaliseSentimento.Web.API.EntityContext;
using DMAnaliseSentimento.Web.API.Model;
using DMAnaliseSentimento.Web.API.ModelsBuilder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using ModelBuilder = DMAnaliseSentimento.Web.API.ModelsBuilder.ModelBuilder;

namespace DMAnaliseSentimento.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TreinarModeloController : ControllerBase
    {
        private readonly DMAnaliseSentimentoContext _context;
        private readonly IConfiguration _configuration;
        public TreinarModeloController(DMAnaliseSentimentoContext context, 
                                       IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Post()
        {
            new ModelBuilder(_configuration, _context).CreateModel(GetModeloParaAnalise());
            return Ok(true);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {            
            return Ok(await _context.ResultadoTreinamento.ToListAsync());
        }

        private List<ModelImportacaoDataSet> GetModeloParaAnalise()
            => _context.DataSet.ToList()
                .Select(ds => new ModelImportacaoDataSet(ds.Predicado == 1 ? true : false, ds.Texto)).ToList();
        
    }
}