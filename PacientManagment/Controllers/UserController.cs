using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PacientManagment.Core.Application.Interfaces.Services;
using PacientManagment.Core.Application.ViewModels.User;
using PacientManagment.Middlewares;
using PacientManagment.Core.Application.Helpers;
using PacientManagment.Core.Domain.Entities;
using System.Runtime.Intrinsics.Arm;
using Microsoft.EntityFrameworkCore;
using PacientManagment.Infrastructure.Persistence.Contexts;

namespace PacientManagment.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidateUserSession _validateUserSession;
        private readonly SessionConsult _sessionConsult;
        private readonly RemoveSession _removeSession;
        private readonly ApplicationContext _context;

        public UserController(IUserService userService, ValidateUserSession validateUserSession, SessionConsult sessionConsult, RemoveSession removeSession, ApplicationContext context)
        {
            _userService = userService;
            _validateUserSession = validateUserSession;
            _sessionConsult = sessionConsult;
            _removeSession = removeSession;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //clear session table if there is data on table
            await _sessionConsult.deleteAll();
            if (await _validateUserSession.HasUser())
            {

                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (await _validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            UserViewModel userVm = await _userService.Login(vm);

            if (userVm != null)
            {
                // save sesion on  db
                await _sessionConsult.InsertSessionAsync(userVm);

                // save user in session HTTP
                HttpContext.Session.Set<UserViewModel>("user", userVm);
                //save hour in session HTTP
                HttpContext.Session.Set("LastActivity", DateTime.UtcNow);

                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("Password", "Incorrect access data.");
            }

            return View(vm);
        }

        public async Task<IActionResult> LogOut()
        {
            // get user http sesion
            var userVm = HttpContext.Session.Get<UserViewModel>("user");

            if (userVm != null)
            {
                // delete session on db 
                await _removeSession.RemoveSessionAsync(userVm.Username);

                // delete session HTTP
                HttpContext.Session.Remove("user");
            }

            // redirect USER index
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }


        public async Task<IActionResult> Register()
        {
            if (await _validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            // verify if the username is not exist on db
            if (_context.Users.Any(u => u.Username == vm.Username))
            {
                ModelState.AddModelError("UserName", "The User Name has already exist.");
                return View(vm);
            }

            // model is valid?
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (await _validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            // add user
            await _userService.Add(vm);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
    }
}
