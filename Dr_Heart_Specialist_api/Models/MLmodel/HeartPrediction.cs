using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ML.Data;

namespace Dr_Heart_Specialist_api.Models.MLmodel
{
    public class HeartPrediction
    {
        public int Prediction;
        public float Probability;

        public float Score;
    }
}