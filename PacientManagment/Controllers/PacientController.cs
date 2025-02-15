using Microsoft.AspNetCore.Mvc;

namespace PacientManagment.Controllers
{
    public class PacientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
