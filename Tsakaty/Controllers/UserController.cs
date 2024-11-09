using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tsakaty.Models;
using Tsakaty.Repository;

namespace Tsakaty.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = userRepository.GetAll();
            var userRoles = new List<(ApplicationUser User, bool IsAdmin)>();

            foreach (var user in users)
            {
                bool isAdmin = await userManager.IsInRoleAsync(user, "Admin");
                userRoles.Add((user, isAdmin));
            }

            return View("Index", userRoles);
        }
        public async Task<IActionResult> MakeAdmin(string id)
        {
           await userRepository.MakeAdmin(id);
            var users = userRepository.GetAll();
            var userRoles = new List<(ApplicationUser User, bool IsAdmin)>();

            foreach (var user in users)
            {
                bool isAdmin = await userManager.IsInRoleAsync(user, "Admin");
                userRoles.Add((user, isAdmin));
            }

            return View("Index", userRoles);
        }
        public async Task<IActionResult> RemoveAdmin(string id)
        {
            await userRepository.RemoveAdmin(id);
            var users = userRepository.GetAll();
            var userRoles = new List<(ApplicationUser User, bool IsAdmin)>();

            foreach (var user in users)
            {
                bool isAdmin = await userManager.IsInRoleAsync(user, "Admin");
                userRoles.Add((user, isAdmin));
            }

            return View("Index", userRoles);
        }
        [HttpGet]
        public IActionResult Details(string id)
        {
            ApplicationUser user= userRepository.GetById(id);

            return View("Details",user);
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            ApplicationUser user = userRepository.GetById(id);
            return View("Edit",user);
        }

        
        public async Task<IActionResult> SaveEdit(ApplicationUser applicationUser)
        {
            if(ModelState.IsValid)
            {
                //ApplicationUser user = userRepository.GetById(id);
                var user =  userRepository.GetById(applicationUser.Id);

                user.UserName = applicationUser.UserName;
                user.Address = applicationUser.Address;
                user.Email = applicationUser.Email;
                user.PhoneNumber = applicationUser.PhoneNumber;
                
                await userRepository.Update(user);
                return View("Details",user);

            }
            return View(applicationUser);

        }
    }
}
