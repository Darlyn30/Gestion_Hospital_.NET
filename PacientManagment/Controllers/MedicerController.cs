using Microsoft.AspNetCore.Mvc;
using PacientManagment.Core.Application.Interfaces.Repositories;
using PacientManagment.Core.Application.Interfaces.Services;
using PacientManagment.Core.Application.ViewModels.Medicer;
using PacientManagment.Core.Application.ViewModels.User;

namespace PacientManagment.Controllers
{
    public class MedicerController : Controller
    {
        private readonly IMedicerService _service;
        private readonly IConsulterService _consulterService;
        
        public MedicerController(IMedicerService service, IConsulterService consulterService)
        {
            _service = service;
            _consulterService = consulterService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _service.GetAllViewModel();
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            SaveMedicerViewModel model = new();
            model.Consulters = await _consulterService.GetAllViewModel();
            return View("Create", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveMedicerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Consulters = await _consulterService.GetAllViewModel();
                return View("Create", model);
            }

            SaveMedicerViewModel medicerVm = await _service.Add(model);

            if(medicerVm.Id != 0 && medicerVm != null)
            {
                medicerVm.ImgPath = UploadFile(model.File, medicerVm.Id);
                await _service.Update(medicerVm);
            }

            
            return RedirectToRoute(new { controller = "Medicer", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            SaveMedicerViewModel model = await _service.GetByIdSaveViewModel(id);
            model.Consulters = await _consulterService.GetAllViewModel();
            return View("Create", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveMedicerViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return View("Create", model);
            }
            SaveMedicerViewModel medicerVm = await _service.GetByIdSaveViewModel(model.Id);
            model.ImgPath = UploadFile(model.File, model.Id, true, medicerVm.ImgPath);
            await _service.Update(model);
            return RedirectToRoute(new { controller = "Medicer", action = "Index" });
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

            string basePath = $"/Imgs/Medicers/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }
            return RedirectToRoute(new { controller = "Medicer", action = "Index" });
        }

        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/Medicers/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }

        public async Task<IActionResult> Search(string name)
        {
            return View("Index", await _service.GetByNameAsync(name));
        }
    }
}
