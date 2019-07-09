using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskScheduler
{
    public interface IRepository
    {
        Task AddTask(Task task);

        bool IsTaskNull(Task task);

        void DeleteTask(Task task);

        void DeleteTaskByName(string name);

        void DeleteTaskById(int id);

        List<Task> FetchAllData();

        void UpdateTask(Task task);

        Task GetTask(Task task);

        Task GetTaskByName(string taskName);

        Task GetTaskById(int id);
    }
}
