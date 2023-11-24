using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
namespace Dr_Heart_Specialist_api.Models.MLmodel
{
    public class MLModel
    {
        private readonly MLContext mlContext;
        private ITransformer trainedModel = null;
        private static string TrainDataPath = @"D://DESKTOP DATA//IDE Projects//C# WEB API//Dr_Heart_Specialist_api//Models//MLmodel//TrainingData//Advanced_Training_Data.csv";


        public MLModel()
        {
            mlContext = new MLContext();
        }
        public void Build()
        {
            // STEP 1: Common data loading configuration  
            var trainingDataView = mlContext.Data.LoadFromTextFile<Advanced_Prediction_Input>(TrainDataPath, hasHeader: true, separatorChar: ';');

            // STEP 2: Concatenate the features and set the training algorithm  
            var pipeline = mlContext.Transforms.Concatenate("Features", "Age", "Gender", "Chestpain", "Bloodpressure", "Cholistrol", "Fbs", "Ecg", "Maxheartrate", "Angina", "Oldpeak", "Slope", "Majorvessel", "Thalscan")
                            .Append(mlContext.BinaryClassification.Trainers.FastTree(labelColumnName: "Diagnosis", featureColumnName: "Features"));
            trainedModel = pipeline.Fit(trainingDataView);
        }
        public HeartPrediction Consume(Advanced_Prediction_Input input)
        {
            var predictionEngine = mlContext.Model.CreatePredictionEngine<Advanced_Prediction_Input, HeartPrediction>(trainedModel);
            return predictionEngine.Predict(input);
        }
     
    }
}