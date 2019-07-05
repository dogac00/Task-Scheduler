using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskScheduler
{
    static class JsonUtils
    {
        private static readonly string jsonFilePath = @"..\..\tasks.json";

        public static Task AddTask(Task task)
        {
            FileUtils.CheckIfFileExists(jsonFilePath);

            var jsonData = File.ReadAllText(jsonFilePath);
            List<Task> tasksList;

            try
            {
                // If it throws an exception,
                // json file is empty.

                tasksList = DeserializeTasks(jsonData);
            }
            catch { tasksList = new List<Task>(); }

            tasksList.Add(task);

            jsonData = JsonConvert.SerializeObject(tasksList);

            File.WriteAllText(jsonFilePath, jsonData);

            OrderById();

            return GetTaskByName(task.Name);
        }

        public static bool IsTaskNull(Task task)
        {
            return GetTaskByName(task.Name) == null;
        }

        public static bool DeleteTask(Task task)
        {
            var taskId = task.Id;

            task = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return DeleteTask(taskId);
        }

        public static bool DeleteTask(int taskId)
        {
            bool isDeleted = false;

            try
            {
                var jsonData = File.ReadAllText(jsonFilePath);

                List<Task> tasksList = DeserializeTasks(jsonData);

                for (int i = 0; i < tasksList.Count; ++i)
                {
                    if (tasksList[i].Id == taskId)
                    {
                        tasksList.RemoveAt(i);
                        isDeleted = true;
                    }
                }

                jsonData = JsonConvert.SerializeObject(tasksList);

                File.WriteAllText(jsonFilePath, jsonData);
            }
            catch (Exception e) { MessageBox.Show(e.Message); }

            OrderById();

            return isDeleted;
        }

        public static void OrderById()
        {
            var jsonData = File.ReadAllText(jsonFilePath);

            List<Task> tasksList = DeserializeTasks(jsonData);

            for (int i = 0; i < tasksList.Count; ++i)
            {
                tasksList[i].Id = i;
            }

            jsonData = JsonConvert.SerializeObject(tasksList);

            File.WriteAllText(jsonFilePath, jsonData);
        }

        public static List<Task> FetchJsonData()
        {
            FileUtils.CheckIfFileExists(jsonFilePath);

            string jsonData = null;

            try
            {
                jsonData = File.ReadAllText(jsonFilePath);
            } catch (Exception e) { MessageBox.Show(e.Message); }
            
            
            List<Task> tasks;

            try { tasks = DeserializeTasks(jsonData); }
            catch { tasks = new List<Task>(); }

            return tasks;
        }

        public static void UpdateTask(Task task)
        {
            UpdateTask(task, task.IsRunning, task.ProcessId);
        }

        public static void UpdateTask(Task task, bool isRunning, int processId)
        {
            FileUtils.CheckIfFileExists(jsonFilePath);

            try
            {
                var jsonData = File.ReadAllText(jsonFilePath);

                List<Task> tasksList = DeserializeTasks(jsonData);

                for (int i = 0; i < tasksList.Count; ++i)
                {
                    if (tasksList[i].Name == task.Name)
                    {
                        tasksList[i].IsRunning = isRunning;
                        tasksList[i].ProcessId = processId;
                    }
                }

                jsonData = JsonConvert.SerializeObject(tasksList);

                File.WriteAllText(jsonFilePath, jsonData);

            } catch (Exception e) { MessageBox.Show(e.Message); }

            OrderById();
        }

        public static void UpdateForNotified(Task task)
        {
            var emailInfo = new EmailInfo();

            emailInfo.EmailAddress = null;
        }

        public static Task GetTask(Task task)
        {
            return GetTaskById(task.Id);
        }

        public static Task GetTaskByName(string taskName)
        {
            FileUtils.CheckIfFileExists(jsonFilePath);

            try
            {
                var jsonData = File.ReadAllText(jsonFilePath);

                List<Task> tasksList = DeserializeTasks(jsonData);

                for (int i = 0; i < tasksList.Count; ++i)
                {
                    if (tasksList[i].Name == taskName)
                    {
                        return tasksList[i];
                    }
                }

                return null;
            }
            catch { return null; }
        }

        public static Task GetTaskById(int id)
        {
            FileUtils.CheckIfFileExists(jsonFilePath);

            var jsonData = File.ReadAllText(jsonFilePath);

            List<Task> tasksList = DeserializeTasks(jsonData);

            for (int i = 0; i < tasksList.Count; ++i)
            {
                if (tasksList[i].Id == id)
                {
                    return tasksList[i];
                }
            }

            return null;
        }

        public static List<GridTask> PopulateGridTaskList(List<Task> tasks)
        {
            List<GridTask> gridTasks = new List<GridTask>();

            foreach (var task in tasks)
            {
                var gridTask = new GridTask
                {
                    Name = task.Name,
                    ExecutablePath = task.ExecutablePath,
                    IsRunning = task.IsRunning,
                    StartProperty = task.Period.Property.ToString(),
                    StartDate = task.Period.StartDate,
                    ProcessId = task.ProcessId,
                    EmailAddress = EmailUtils.GetTaskEmails(task),
                    TimeBetween = TaskUtils.GetTaskTimeBetween(task),
                };

                gridTasks.Add(gridTask);
            }

            return gridTasks;
        }

        private static List<Task> DeserializeTasks(string jsonData)
        {
            try { return JsonConvert.DeserializeObject<List<Task>>(jsonData) ?? new List<Task>(); }
            catch { return new List<Task>(); }
        }
    }
}
