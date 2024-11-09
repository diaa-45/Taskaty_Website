
using Tsakaty.Models;

namespace Tsakaty.Repository
{
    public class TaskRepository : ITaskRepoitory
    {
        private readonly AppDbContext context;

        public TaskRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void Create(Models.Task task)
        {
            context.Tasks.Add(task);
        }

        public void delete(int id)
        {
            Models.Task task = GetOne(id);
            if (task != null)
            {
                context.Tasks.Remove(task);
            }
        }

        public List<Models.Task> GetAll()
        {
            return context.Tasks.ToList();
        }

        public Models.Task GetOne(int id)
        {
            return context.Tasks.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Models.Task task)
        {
            context.Tasks.Update(task);
        }
        public void Save() 
        {
            context.SaveChanges();
        }

        public List<Models.Task> GetAllByUserId(string userId)
        {
            return context.Tasks.Where(t => t.UserId == userId).ToList() ?? new List<Models.Task>();
        }
    }
}
