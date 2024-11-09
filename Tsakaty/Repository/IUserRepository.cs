using Tsakaty.Models;

namespace Tsakaty.Repository
{
    public interface IUserRepository
    {
        List<ApplicationUser> GetAll();
        ApplicationUser GetById(string id);
        System.Threading.Tasks.Task Update(ApplicationUser user);
        void Delete(string id);
        System.Threading.Tasks.Task MakeAdmin(string id);
        System.Threading.Tasks.Task RemoveAdmin(string id);

    }
}
