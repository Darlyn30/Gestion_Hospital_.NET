using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PacientManagment.Core.Application.Interfaces.Services;
using PacientManagment.Core.Application.ViewModels.User;
using PacientManagment.Infrastructure.Persistence.Contexts;
using PacientManagment.Middlewares;

namespace PacientManagment.Controllers
{
    public class MaintUserController : Controller
    {
        private readonly IUserService _service;
        private readonly SessionConsult _sessionConsult;
        private readonly ApplicationContext _context;


        public MaintUserController(IUserService service, SessionConsult sessionConsult, ApplicationContext context)
        {
            _service = service;
            _sessionConsult = sessionConsult;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            //verify Role Account
            bool rolIsAdmin = await _sessionConsult.isAdmin();

            if (!rolIsAdmin)
            {
                return RedirectToAction("AccessDenied"); // Redirect to view
            }

            var model = await _service.GetAllViewModel();
            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            SaveUserViewModel model = new();
            return View("Create", model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SaveUserViewModel model)
        {
            if (_context.Users.Any(u => u.Username == model.Username))
            {
                ModelState.AddModelError("Username", "The User Name has already exist.");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View("Create", model);
            }

            await _service.Add(model);
            return RedirectToRoute(new { controller = "MaintUser", action = "Index" });
        }
        public async Task<IActionResult> Edit(int id)
        {
            SaveUserViewModel model = await _service.GetByIdSaveViewModel(id);
            return View("Create", model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SaveUserViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return View("Create", model);
            }

            await _service.Update(model);
            return RedirectToRoute(new { controller = "MaintUser", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _service.GetByIdSaveViewModel(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {

            await _service.Delete(id);
            return RedirectToRoute(new { controller = "MaintUser", action = "Index" });
        }

        public async Task<IActionResult> Search(string userName)
        {
            return View("Index", await _service.GetByNameAsync(userName));
        }
    }
}
