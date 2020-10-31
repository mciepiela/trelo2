using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = trelo2.Models.Task;


namespace trelo2.Services.Interfaces
{
    public interface ITasksServices
    {
        IEnumerable<Task> GetUserTasks(string userId);
        bool CreateTaskForUser(Task taskToCreate, string userId);
        Task DetailOfTask(int id);
    }
}
