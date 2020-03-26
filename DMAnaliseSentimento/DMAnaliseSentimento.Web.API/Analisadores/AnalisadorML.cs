using DMAnaliseSentimento.Web.API.Domain;
using DMAnaliseSentimentoML.Model;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMAnaliseSentimento.Web.API.Analisadores
{
    public class AnalisadorML
    {
        public IConfiguration Configuration { get; }

        public AnalisadorML(IConfiguration configuration) => Configuration = configuration;
        
        public ResultadoAnalise ConsumeModel(string texto, PredictionEngine<ModelInput, ModelOutput> predEngine)
        {         
            var input = new ModelInput
            {
                Texto = texto
            };

            var result = predEngine.Predict(input);
            return new ResultadoAnalise
            {
                ComentarioDepreciativo = result.Prediction,
                Score = result.Score
            };
        }

        public PredictionEngine<ModelInput, ModelOutput> GetPrediction(ICacheEntry context)
        {
            context.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
            context.SetPriority(CacheItemPriority.High);
            MLContext mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(Configuration.GetSection("PathAnalise:Path").Value + "\\MLModel.zip", out var modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
            return predEngine;
        }
    }
}
