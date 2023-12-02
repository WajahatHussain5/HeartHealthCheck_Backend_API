using Dr_Heart_Specialist_api.DataBaseEntity;
using Dr_Heart_Specialist_api.Models;
using Dr_Heart_Specialist_api.Models.DataModel;
using Dr_Heart_Specialist_api.Models.MLmodel;
using Dr_Heart_Specialist_api.Models.MLmodel.TrainingData;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace Dr_Heart_Specialist_api.Controllers
{
    /// <summary>
    /// regular prediction controller.
    /// </summary>
    public class RegularPredictionController : ApiController
    {
        Dr_Heart_Specialist_api_db db = new Dr_Heart_Specialist_api_db();

        /// <summary>
        /// Regular Prediction API.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult RegularPredict(RegularPredictionData data)
        {
            User_Info Registered_Info = db.User_Info.Where(c => c.Username == data.Username).FirstOrDefault<User_Info>();

            if (data.DiasBloodpressure == 0.0 || data.Maxheartrate == 0.0 || data.SysBloodpressure == 0.0 || data.Username == "null")
            {
                var reponse = "ALL VALUES ARE REQUIRED MAKE SURE YPU ENTERED ALL CORRECT VALUES";

                return BadRequest(reponse);
            }
            else
            {
                DateTime date_time_now = new DateTime();

                Regular_Prediction_Input input = new Regular_Prediction_Input();

                date_time_now       = DateTime.Now;
                input.Age           = Convert.ToInt32(Registered_Info.Age);
                input.Gender        = Convert.ToInt32(Registered_Info.Gender);
                input.Chestpain     = data.Chestpain;
                input.Bloodpressure = (data.SysBloodpressure + data.DiasBloodpressure) / 2;
                input.Fbs           = Convert.ToInt32(Registered_Info.Fbs); ;
                input.Maxheartrate  = data.Maxheartrate;
                input.Anigna        = data.Angina;

                var model  = new RegularMLModel();
                var result = new HeartPrediction();

                try
                {
                    model.BuildR();

                    result = model.ConsumeR(input);
                }
                catch (Exception e)
                {
                    return Ok(e.ToString());
                }
                if (result.Probability > 0.50)
                {
                    result.Prediction = 1;
                }
                else
                {
                    result.Prediction = 0;
                }

                //Mapping data to save in Regular Prediction Table
                REGULAR_PREDICTION_DATA tosave = new REGULAR_PREDICTION_DATA();

                tosave.Username = data.Username;
                tosave.Angina = input.Anigna;
                tosave.SysBloodpressure = data.SysBloodpressure;
                tosave.DiasBloodpressure = data.DiasBloodpressure;
                tosave.Chestpain = input.Chestpain;
                tosave.Maxheartrate = input.Maxheartrate;
                tosave.Date_Time = date_time_now;

                //Mapping data to save in User Prediction Result Table
                User_Prediction_Results save_result = new User_Prediction_Results();

                save_result.Username  = data.Username;
                save_result.Date_Time = date_time_now;
                save_result.Diagnosis = Convert.ToInt32(result.Prediction);
                save_result.Score     = result.Probability;

                try
                {
                    if (data.Username != null)
                    {
                        db.User_Prediction_Results.Add(save_result);
                        db.REGULAR_PREDICTION_DATA.Add(tosave);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    return Ok(e);
                }

                //Mapping data to save in User AVG Prediction Table
                Avg_User_Prediction_Results avg_result = new Avg_User_Prediction_Results();

                avg_result.Username              = data.Username;
                avg_result.Last_Checked          = date_time_now;
                avg_result.Avg_Angina            = db.Database.SqlQuery<double>(@"SELECT AVG(Angina) FROM [dbo].[REGULAR_PREDICTION_DATA] WHERE Username=@username;", new SqlParameter("@username", data.Username)).FirstOrDefault();
                avg_result.Avg_Chestpain         = db.Database.SqlQuery<double>(@"SELECT AVG(Chestpain) FROM [dbo].[REGULAR_PREDICTION_DATA] WHERE Username=@username", new SqlParameter("@username", data.Username)).FirstOrDefault();
                avg_result.Avg_SysBloodpressure  = db.Database.SqlQuery<double>(@"SELECT AVG(SysBloodpressure) FROM [dbo].[REGULAR_PREDICTION_DATA] WHERE Username=@username", new SqlParameter("@username", data.Username)).FirstOrDefault();
                avg_result.Avg_DiasBloodpressure = db.Database.SqlQuery<double>(@"SELECT AVG(DiasBloodpressure) FROM [dbo].[REGULAR_PREDICTION_DATA] WHERE Username=@username", new SqlParameter("@username", data.Username)).FirstOrDefault();
                avg_result.Avg_Maxheartrate      = db.Database.SqlQuery<double>(@"SELECT AVG(Maxheartrate) FROM [dbo].[REGULAR_PREDICTION_DATA] WHERE Username=@username", new SqlParameter("@username", data.Username)).FirstOrDefault();
                avg_result.Avg_Score             = db.Database.SqlQuery<double>(@"SELECT AVG(Score) FROM [dbo].[USER_PREDICTION_RESULTS] WHERE Username=@username", new SqlParameter("@username", data.Username)).FirstOrDefault();
                var temp                         = db.Database.SqlQuery<double>(@"SELECT AVG(Diagnosis) FROM [dbo].[USER_PREDICTION_RESULTS] WHERE Username=@username", new SqlParameter("@username", data.Username)).FirstOrDefault();

                if (temp > 0.5) { avg_result.Avg_Diagnosis = 1; } else { avg_result.Avg_Diagnosis = 0; }
                
                //END OF MAPPING

                try
                {
                    if (data.Username != null)
                    {
                        db.Avg_User_Prediction_Results.Add(avg_result);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    return Ok(e);
                }

                return Ok(result);
            }
        }
    }
}
