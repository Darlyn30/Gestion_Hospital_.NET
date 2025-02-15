using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PacientManagment.Infrastructure.Persistence.Contexts;
using PacientManagment.Middlewares;
using PacientManagment.Models;

namespace PacientManagment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;
        public HomeController(ValidateUserSession validateUserSession, ApplicationContext context)
        {
            _validateUserSession = validateUserSession;
        }
        public async Task<IActionResult> Index()
        {
            if (!await _validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View();
        }

    }
}
