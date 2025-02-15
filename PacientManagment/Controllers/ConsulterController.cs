using Microsoft.AspNetCore.Mvc;

namespace PacientManagment.Controllers
{
    public class ConsulterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
