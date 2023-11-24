using Dr_Heart_Specialist_api.Models.MLmodel;
using System.Web.Http;

namespace Dr_Heart_Specialist_api.Controllers
{
    
    public class AdvancedPredictionController : ApiController
    {
        

        [HttpPost]
        public IHttpActionResult AdvancedPredict(Advanced_Prediction_Input input)
        {
            var model = new MLModel();
            model.Build();
            var result = model.Consume(input);
            if (result.Probability > 0.50)
            {
                result.Prediction = 0;
            }
            else 
            {
                result.Prediction = 1;
            }
            return Ok(result);
        }
    }
}
