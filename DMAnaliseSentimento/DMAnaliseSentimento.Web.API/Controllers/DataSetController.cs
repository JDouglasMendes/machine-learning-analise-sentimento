using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMAnaliseSentimento.Web.API.Domain;
using DMAnaliseSentimento.Web.API.EntityContext;
using Microsoft.AspNetCore.Mvc;

namespace DMAnaliseSentimento.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataSetController : ControllerBase
    {
        private readonly DMAnaliseSentimentoContext _context;

        public DataSetController(DMAnaliseSentimentoContext context) => _context = context;
        
        [HttpPost]
        public async Task<IActionResult> Post(DataSet dataSet)
        {
            _context.DataSet.Add(dataSet);
            await _context.SaveChangesAsync();
            return Ok(dataSet);
        }

        [HttpGet]
        public IActionResult Get() => Ok(_context.DataSet.ToList());                
    }
}