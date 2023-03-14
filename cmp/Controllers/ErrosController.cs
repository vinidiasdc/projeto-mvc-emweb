using Microsoft.AspNetCore.Mvc;

namespace EM.Web.Controllers
{
    public class ErrosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CPFInvalido()
        {
            return View();
        }
    }
}
