using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMAnaliseSentimento.Web.API.Domain
{
    public class ResultadoAnalise
    {
        public bool ComentarioDepreciativo { get; set; }
        public float Score { get; set; }
    }
}
