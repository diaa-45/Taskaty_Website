using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tsakaty.Models;
using Tsakaty.Repository;
using Tsakaty.ViewModels;

namespace Tsakaty.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskRepoitory taskRepoitory;
        private readonly UserManager<ApplicationUser> userManager;

        public TaskController(ITaskRepoitory taskRepoitory, UserManager<ApplicationUser> userManager)
        {
            this.taskRepoitory = taskRepoitory;
            this.userManager = userManager;
        }

        public IActionResult Test()
        {
            return Content(userManager.GetUserId(User));
        }
        [HttpGet]
        public IActionResult Index()
        {
            var userId = userManager.GetUserId(User);
            var tasks = taskRepoitory.GetAllByUserId(userId);
            return View("Index",tasks);
        }
        [HttpGet]
        public IActionResult AllTasks()
        {
            var tasks = taskRepoitory.GetAll();
            return View("Index",tasks);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public IActionResult SaveCreate(CreateTaskViewModel taskViewModel)
        {
            if(ModelState.IsValid)
            {
                Models.Task task = new Models.Task();
                task.Title = taskViewModel.Title;
                task.Description = taskViewModel.Description;
                task.dateTime = DateTime.Now;
                task.UserId = userManager.GetUserId(User);

                taskRepoitory.Create(task);
                taskRepoitory.Save();
                return RedirectToAction("Index");
            }
            return View("Create", taskViewModel);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            return View("Update", taskRepoitory.GetOne(id));
        }
        [HttpPost]
        public IActionResult SaveUpdate(CreateTaskViewModel model,int id)
        {
            if(ModelState.IsValid)
            {
                Models.Task task = taskRepoitory.GetOne(id);
                if(task != null)
                {
                    task.Title = model.Title;
                    task.Description = model.Description;
                    taskRepoitory.Update(task);
                    taskRepoitory.Save();
                    return RedirectToAction("Index");

                }
                return View(model);
            }
            return View(model);
        }
        
        public IActionResult Delete(int id)
        {
            taskRepoitory.delete(id);
            taskRepoitory.Save();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            return View("Details", taskRepoitory.GetOne(id));
        }
    }
}
