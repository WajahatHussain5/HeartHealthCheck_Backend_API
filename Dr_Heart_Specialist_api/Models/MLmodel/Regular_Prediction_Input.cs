using Microsoft.ML.Data;

namespace Dr_Heart_Specialist_api.Models
{
    public class Regular_Prediction_Input
    {
        [ColumnName("age"), LoadColumn(0)]
        public float Age { get; set; }


        [ColumnName("gender"), LoadColumn(1)]
        public float Gender { get; set; }


        [ColumnName("chestpain"), LoadColumn(2)]
        public float Chestpain { get; set; }


        [ColumnName("bloodpressure"), LoadColumn(3)]
        public float Bloodpressure { get; set; }


        [ColumnName("fbs"), LoadColumn(4)]
        public float Fbs { get; set; }


        [ColumnName("maxheartrate"), LoadColumn(5)]
        public float Maxheartrate { get; set; }


        [ColumnName("angina"), LoadColumn(6)]
        public float Anigna { get; set; }


        [ColumnName("diagnosis"), LoadColumn(7)]
        public bool Diagnosis { get; set; }

    }

}