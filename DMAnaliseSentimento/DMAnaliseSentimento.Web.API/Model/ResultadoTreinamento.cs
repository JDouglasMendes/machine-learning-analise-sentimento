using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DMAnaliseSentimento.Web.API.Model
{
    public class ResultadoTreinamento
    {
        [Key]
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double AccuracyAverage { get; set; }
        public double AccuraciesStdDeviation { get; set; }
        public double AccuraciesConfidenceInterval95 { get; set; }

        public ResultadoTreinamento()
        {
            Data = DateTime.Now;
        }
    }
}
