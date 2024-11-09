using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tsakaty.Models;

namespace Tsakaty.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public List<ApplicationUser> GetAll()
        {
            return userManager.Users.ToList();
        }

        public ApplicationUser GetById(string id)
        {
            return userManager.Users.FirstOrDefault(u => u.Id == id);
        }

        public async System.Threading.Tasks.Task Update(ApplicationUser user)
        {
            await userManager.UpdateAsync(user);
        }
        public void Delete(string id)
        {
            ApplicationUser user = GetById(id);
            if (user != null)
            {
                userManager.DeleteAsync(user);
            }
        }

        public async System.Threading.Tasks.Task MakeAdmin(string id)
        {
            ApplicationUser user = GetById(id);
            if (user != null)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }

        public async System.Threading.Tasks.Task RemoveAdmin(string id)
        {
            ApplicationUser user = GetById(id);
            if (user != null)
            {
                await userManager.RemoveFromRoleAsync(user,"Admin");
            }
        }
    }
}
