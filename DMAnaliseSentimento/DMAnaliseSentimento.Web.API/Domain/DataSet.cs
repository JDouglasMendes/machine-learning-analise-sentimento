using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DMAnaliseSentimento.Web.API.Domain
{
    public class DataSet
    {
        [Key]
        public int Id { get; set; }
        public byte Predicado { get; set; }
        public string  Texto { get; set; }        
    }
}
