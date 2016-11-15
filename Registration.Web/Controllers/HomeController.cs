using System.Web.Mvc;
using Registration.Contracts;
using Registration.Contracts.Registration;

namespace Registration.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult UnRegister(string identityNumber)
        {
            var response = new Result<Contracts.Registration.Registration>
            {
                Data = new Contracts.Registration.Registration
                {
                    
                },
                ResultCode = ResultCode.Success
            };
            // Unregister over REST

            return this.Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UnSubscribe(string identityNumber)
        {
            var response = new Result<Contracts.Registration.Registration>
            {
                Data = new Contracts.Registration.Registration
                {

                },
                ResultCode = ResultCode.Success
            };
            // Unsubscribe over REST

            return this.Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRegisteredStatus(string identityNumber)
        {
            var response = new Result<Status>
            {
                Data = Status.Registered,
                ResultCode = ResultCode.Success
            };
            // Get status from REST service

            return this.Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Register(RegistrationRequest request)
        {
            var response = new Result<Contracts.Registration.Registration>
            {
                Data = new Contracts.Registration.Registration
                {

                },
                ResultCode = ResultCode.Success
            };
            // Get registration result

            return this.Json(response, JsonRequestBehavior.AllowGet);
        }

    }
}