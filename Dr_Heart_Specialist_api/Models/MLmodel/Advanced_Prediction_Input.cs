using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ML.Data;

namespace Dr_Heart_Specialist_api.Models.MLmodel
{
    public class Advanced_Prediction_Input
    {

            [LoadColumn(0)]
            public float Age { get; set; }
            [LoadColumn(1)]
            public float Gender { get; set; }
            [LoadColumn(2)]
            public float Chestpain { get; set; }
            [LoadColumn(3)]
            public float Bloodpressure { get; set; }
            [LoadColumn(4)]
            public float Cholistrol { get; set; }
            [LoadColumn(5)]
            public float Fbs { get; set; }
            [LoadColumn(6)]
            public float Ecg { get; set; }
            [LoadColumn(7)]
            public float Maxheartrate { get; set; }
            [LoadColumn(8)]
            public float Angina { get; set; }
            [LoadColumn(9)]
            public float Oldpeak { get; set; }
            [LoadColumn(10)]
            public float Slope { get; set; }
            [LoadColumn(11)]
            public float Majorvessel { get; set; }
            [LoadColumn(12)]
            public float Thalscan { get; set; }
            [LoadColumn(13)]
            public bool Diagnosis { get; set; }
        

    }
}
