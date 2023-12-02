using System.Web.Mvc;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
namespace Dr_Heart_Specialist_api.Controllers
{
    /// <summary>
    /// home controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// home page.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
