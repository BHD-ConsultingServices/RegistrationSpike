
namespace Registration.Web.Controllers
{
    using System.Web.Mvc;
    using Models;
    using Properties;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new BaseModel
            {
                BaseApiUrl = Settings.Default.BaseApiUrl
            };

            return View(model);
        }
    }
}