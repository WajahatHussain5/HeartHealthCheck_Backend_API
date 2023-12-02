namespace Dr_Heart_Specialist_api.Models.DataModel
{
    public class RegularPredictionData
    {
        public float Age { get; set; }

        public float Gender { get; set; }

        public float Chestpain { get; set; }

        public float SysBloodpressure { get; set; }
        public float DiasBloodpressure { get; set; }
        public float Fbs { get; set; }

        public float Maxheartrate { get; set; }

        public float Angina { get; set; }

        public bool Diagnosis { get; set; }

        public string Username { get; set; }

    }
}