using DMAnaliseSentimento.Web.API.EntityContext;
using DMAnaliseSentimento.Web.API.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DMAnaliseSentimento.Web.API.ModelsBuilder
{
    public class ModelBuilder
    {       
        private IConfiguration Configuration { get; }
        private DMAnaliseSentimentoContext Context { get; }
        private MLContext _mlContext;

        public ModelBuilder(IConfiguration configuration, DMAnaliseSentimentoContext context)
        {
            _mlContext = new MLContext(seed: 1);
            Configuration = configuration;
            Context = context;
        }

        public void CreateModel(List<ModelImportacaoDataSet> lista)
        {        
            var schema = SchemaDefinition.Create(typeof(ModelImportacaoDataSet));
            var trainingDataView = _mlContext.Data.LoadFromEnumerable(lista, schema);           
            IEstimator<ITransformer> trainingPipeline = BuildTrainingPipeline(_mlContext);            
            Evaluate(_mlContext, trainingDataView, trainingPipeline);            
            ITransformer mlModel = TrainModel(_mlContext, trainingDataView, trainingPipeline);            
            SaveModel(_mlContext, mlModel, trainingDataView.Schema);
        }

        public IEstimator<ITransformer> BuildTrainingPipeline(MLContext mlContext)
        {
            var dataProcessPipeline = mlContext.Transforms.Text.FeaturizeText("Texto_tf", "Texto")
                                      .Append(mlContext.Transforms.CopyColumns("Features", "Texto_tf"))
                                      .Append(mlContext.Transforms.NormalizeMinMax("Features", "Features"))
                                      .AppendCacheCheckpoint(mlContext);

            var trainer = mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Predicado", featureColumnName: "Features");
            var trainingPipeline = dataProcessPipeline.Append(trainer);
            return trainingPipeline;
        }

        public ITransformer TrainModel(MLContext mlContext, IDataView trainingDataView, IEstimator<ITransformer> trainingPipeline)
        {           
            ITransformer model = trainingPipeline.Fit(trainingDataView);         
            return model;
        }

        private void Evaluate(MLContext mlContext, IDataView trainingDataView, IEstimator<ITransformer> trainingPipeline)
        {                     
            var crossValidationResults = mlContext.BinaryClassification.CrossValidateNonCalibrated(trainingDataView, trainingPipeline, numberOfFolds: 5, labelColumnName: "Predicado");
            PrintBinaryClassificationFoldsAverageMetrics(crossValidationResults);
        }

        private void SaveModel(MLContext mlContext, ITransformer mlModel, DataViewSchema modelInputSchema)
        {                        
            mlContext.Model.Save(mlModel, modelInputSchema, GetAbsolutePath());        
        }

        public string GetAbsolutePath()
        {
            var fullPath = Path.Combine(Configuration.GetSection("PathAnalise:Path").Value, "MLModel.zip");
            DeleteModeloExistente(fullPath);
            return fullPath;
        }

        private static void DeleteModeloExistente(string fullPath)
        {
            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
        
        public void PrintBinaryClassificationFoldsAverageMetrics(IEnumerable<TrainCatalogBase.CrossValidationResult<BinaryClassificationMetrics>> crossValResults)
        {
            var metricsInMultipleFolds = crossValResults.Select(r => r.Metrics);
            
            var AccuracyValues = metricsInMultipleFolds.Select(m => m.Accuracy);
            var AccuracyAverage = AccuracyValues.Average();
            var AccuraciesStdDeviation = CalculateStandardDeviation(AccuracyValues);
            var AccuraciesConfidenceInterval95 = CalculateConfidenceInterval95(AccuracyValues);

            var resultadoTreinamento = new ResultadoTreinamento
            {
                AccuraciesConfidenceInterval95 = AccuraciesConfidenceInterval95,
                AccuracyAverage = AccuracyAverage,
                AccuraciesStdDeviation = AccuraciesStdDeviation
            };

            Context.ResultadoTreinamento.Add(resultadoTreinamento);
            Context.SaveChanges();
        }

        public double CalculateStandardDeviation(IEnumerable<double> values)
        {
            double average = values.Average();
            double sumOfSquaresOfDifferences = values.Select(val => (val - average) * (val - average)).Sum();
            double standardDeviation = Math.Sqrt(sumOfSquaresOfDifferences / (values.Count() - 1));
            return standardDeviation;
        }

        public double CalculateConfidenceInterval95(IEnumerable<double> values)
        {
            double confidenceInterval95 = 1.96 * CalculateStandardDeviation(values) / Math.Sqrt((values.Count() - 1));
            return confidenceInterval95;
        }
    }
}
