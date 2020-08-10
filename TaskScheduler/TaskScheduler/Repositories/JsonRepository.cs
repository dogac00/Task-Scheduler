using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Configuration;
using NLog;

namespace TaskScheduler
{
    class JsonRepository : IRepository
    {
        private readonly string jsonFilePath = ConfigurationManager.AppSettings["jsonPath"];
        private volatile ReaderWriterLockSlim locker = new ReaderWriterLockSlim();
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        private string GetAllText()
        {
            locker.EnterReadLock();

            string jsonData = File.ReadAllText(jsonFilePath);

            locker.ExitReadLock();

            return jsonData;
        }

        private void WriteAllText(string data)
        {
            locker.EnterWriteLock();

            File.WriteAllText(jsonFilePath, data);

            locker.ExitWriteLock();
        }

        public Task AddTask(Task task)
        {
            if (File.Exists(jsonFilePath))
                File.WriteAllText(jsonFilePath, "{}");

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

            logger.Info($"Task [{task.Name}] is added to json.");

            OrderById();

            return GetTaskByName(task.Name);
        }

        public bool IsTaskNull(Task task)
        {
            return GetTaskByName(task.Name) == null;
        }

        public void DeleteTask(Task task)
        {
            DeleteTaskByName(task.Name);
        }

        public void DeleteTaskByName(string name)
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

            logger.Info($"Task named : {name} is deleted by name from json.");

            OrderById();
        }

        public void DeleteTaskById(int id)
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

            logger.Info($"Task with Id : {id} is deleted by id from json.");

            OrderById();
        }

        public void OrderById()
        {
            string jsonData = GetAllText();

            List<Task> tasksList = DeserializeTasks(jsonData);

            for (int i = 0; i < tasksList.Count; ++i)
            {
                tasksList[i].Id = i;
            }

            jsonData = JsonConvert.SerializeObject(tasksList);

            WriteAllText(jsonData);

            logger.Info("Json is reordered by id.");
        }

        public List<Task> FetchAllData()
        {
            if (File.Exists(jsonFilePath))
                File.WriteAllText(jsonFilePath, "{}");

            string jsonData = GetAllText();
            
            List<Task> tasks;

            try { tasks = DeserializeTasks(jsonData); }
            catch { tasks = new List<Task>(); }

            return tasks;
        }

        public void UpdateTask(Task task)
        {
            if (File.Exists(jsonFilePath))
                File.WriteAllText(jsonFilePath, "{}");

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

            logger.Info($"Task {task} is updated in json.");
        }

        public Task GetTask(Task task)
        {
            return GetTaskByName(task.Name);
        }

        public Task GetTaskByName(string taskName)
        {
            if (File.Exists(jsonFilePath))
                File.WriteAllText(jsonFilePath, "{}");

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

        public Task GetTaskById(int id)
        {
            if (File.Exists(jsonFilePath))
                File.WriteAllText(jsonFilePath, "{}");

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

        private List<Task> DeserializeTasks(string jsonData)
        {
            try { return JsonConvert.DeserializeObject<List<Task>>(jsonData) ?? new List<Task>(); }
            catch { return new List<Task>(); }
        }
    }
}
