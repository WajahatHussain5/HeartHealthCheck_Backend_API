//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dr_Heart_Specialist_api.DataBaseEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class REGULAR_PREDICTION_DATA
    {
        public string Username { get; set; }
        public System.DateTime Date_Time { get; set; }
        public double Maxheartrate { get; set; }
        public double Angina { get; set; }
        public double Diagnosis { get; set; }
        public double SysBloodpressure { get; set; }
        public double Chestpain { get; set; }
        public double DiasBloodpressure { get; set; }
        public double Score { get; set; }
        public double User_Prediction_Results { get; set; }
        public double Avg_User_Prediction_Results { get; set; }
    
        public virtual USER_LOGIN USER_LOGIN { get; set; }
    }
}
