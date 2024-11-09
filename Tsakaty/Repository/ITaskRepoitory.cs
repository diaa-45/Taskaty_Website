namespace Tsakaty.Repository
{
    public interface ITaskRepoitory
    {
        List<Models.Task> GetAll();
        Models.Task GetOne(int id);
        void Create(Models.Task task);
        void Update(Models.Task task);
        void delete(int id);
        void Save();
        List<Models.Task> GetAllByUserId(string userId);
    }
}
