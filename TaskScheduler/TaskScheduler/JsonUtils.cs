using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskScheduler
{
    static class JsonUtils
    {
        private static readonly string jsonFilePath = @"..\..\tasks.json";
        private static volatile ReaderWriterLockSlim locker = new ReaderWriterLockSlim();

        private static string GetAllText()
        {
            locker.EnterReadLock();

            string jsonData = File.ReadAllText(jsonFilePath);

            locker.ExitReadLock();

            return jsonData;
        }

        private static void WriteAllText(string data)
        {
            locker.EnterWriteLock();

            File.WriteAllText(jsonFilePath, data);

            locker.ExitWriteLock();
        }

        public static Task AddTask(Task task)
        {
            FileUtils.CheckIfFileExists(jsonFilePath);

            string jsonData = GetAllText();

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

            WriteAllText(jsonData);

            OrderById();

            return GetTaskByName(task.Name);
        }

        public static bool IsTaskNull(Task task)
        {
            return GetTaskByName(task.Name) == null;
        }

        public static void DeleteTask(Task task)
        {
            DeleteTaskByName(task.Name);
        }

        private static void DeleteTaskByName(string name)
        {
            string jsonData = GetAllText();

            List<Task> tasksList = DeserializeTasks(jsonData);

            for (int i = 0; i < tasksList.Count; ++i)
            {
                if (tasksList[i].Name == name)
                {
                    tasksList.RemoveAt(i);
                    break;
                }
            }

            jsonData = JsonConvert.SerializeObject(tasksList);

            WriteAllText(jsonData);

            OrderById();
        }

        public static void DeleteTaskById(int id)
        {
            string jsonData = GetAllText();

            List<Task> tasksList = DeserializeTasks(jsonData);

            for (int i = 0; i < tasksList.Count; ++i)
            {
                if (tasksList[i].Id == id)
                {
                    tasksList.RemoveAt(i);
                    break;
                }
            }

            jsonData = JsonConvert.SerializeObject(tasksList);

            WriteAllText(jsonData);

            OrderById();
        }

        public static void OrderById()
        {
            string jsonData = GetAllText();

            List<Task> tasksList = DeserializeTasks(jsonData);

            for (int i = 0; i < tasksList.Count; ++i)
            {
                tasksList[i].Id = i;
            }

            jsonData = JsonConvert.SerializeObject(tasksList);

            WriteAllText(jsonData);
        }

        public static List<Task> FetchJsonData()
        {
            FileUtils.CheckIfFileExists(jsonFilePath);

            string jsonData = GetAllText();
            
            List<Task> tasks;

            try { tasks = DeserializeTasks(jsonData); }
            catch { tasks = new List<Task>(); }

            return tasks;
        }

        public static void UpdateTask(Task task)
        {
            FileUtils.CheckIfFileExists(jsonFilePath);

            string jsonData = GetAllText();

            List<Task> tasksList = DeserializeTasks(jsonData);

            for (int i = 0; i < tasksList.Count; ++i)
            {
                if (tasksList[i].Name == task.Name)
                {
                    tasksList[i].ExecutablePath = task.ExecutablePath;
                    tasksList[i].IsRunning = task.IsRunning;
                    tasksList[i].ProcessId = task.ProcessId;
                    tasksList[i].Period = task.Period;
                    tasksList[i].EmailInfo = task.EmailInfo;

                    break;
                }
            }

            jsonData = JsonConvert.SerializeObject(tasksList);

            WriteAllText(jsonData);

            OrderById();
        }

        public static Task GetTask(Task task)
        {
            return GetTaskById(task.Id);
        }

        public static Task GetTaskByName(string taskName)
        {
            FileUtils.CheckIfFileExists(jsonFilePath);

            string jsonData = GetAllText();

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

        public static Task GetTaskById(int id)
        {
            FileUtils.CheckIfFileExists(jsonFilePath);

            string jsonData = GetAllText();

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

        private static List<Task> DeserializeTasks(string jsonData)
        {
            try { return JsonConvert.DeserializeObject<List<Task>>(jsonData) ?? new List<Task>(); }
            catch { return new List<Task>(); }
        }
    }
}
