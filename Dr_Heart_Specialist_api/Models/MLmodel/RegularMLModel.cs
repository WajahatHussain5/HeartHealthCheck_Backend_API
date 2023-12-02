using Microsoft.ML;
using System.Linq;

namespace Dr_Heart_Specialist_api.Models.MLmodel.TrainingData
{
    public class RegularMLModel
    {
        private readonly MLContext mlContextR;
        private ITransformer trainedModel = null;
        private static string TrainDataPath = System.Web.HttpContext.Current.Server.MapPath("..\\..\\Models\\MLmodel\\TrainingData\\Regular_Train_Data.csv.txt");


        public RegularMLModel()
        {
            mlContextR = new MLContext();
        }
        public void BuildR()
        {
            // STEP 1: Common data loading configuration  
            var trainingDataView = mlContextR.Data.LoadFromTextFile<Regular_Prediction_Input>(TrainDataPath, hasHeader: true, separatorChar: ';');

            // STEP 2: Concatenate the features and set the training algorithm  
            var pipeline = mlContextR.Transforms.Concatenate("Features", "age", "gender", "chestpain", "bloodpressure", "fbs", "maxheartrate", "angina")
                            .Append(mlContextR.BinaryClassification.Trainers.FastTree(labelColumnName: "diagnosis", featureColumnName: "Features"));
            trainedModel = pipeline.Fit(trainingDataView);
        }
        public HeartPrediction ConsumeR(Regular_Prediction_Input input)
        {
            var predictionEngine = mlContextR.Model.CreatePredictionEngine<Regular_Prediction_Input, HeartPrediction>(trainedModel);
            return predictionEngine.Predict(input);
        }

    }
}