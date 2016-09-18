using System.Web.Mvc;

namespace MvcPL.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Index()
        {
            return View("Error");
        }

        public ViewResult NotFound()
        {
            Response.StatusCode = 404;
            return View("NotFound");
        }

        public ViewResult BadRequest()
        {
            Response.StatusCode = 400;
            return View("BadRequest");
        }
    }
}