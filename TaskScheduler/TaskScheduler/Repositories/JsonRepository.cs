﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Configuration;

namespace TaskScheduler
{
    class JsonRepository : IRepository
    {
        private readonly string jsonFilePath = ConfigurationManager.AppSettings["jsonPath"];
        private volatile ReaderWriterLockSlim locker = new ReaderWriterLockSlim();

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
        }

        public List<Task> FetchAllData()
        {
            FileUtils.CheckIfFileExists(jsonFilePath);

            string jsonData = GetAllText();
            
            List<Task> tasks;

            try { tasks = DeserializeTasks(jsonData); }
            catch { tasks = new List<Task>(); }

            return tasks;
        }

        public void UpdateTask(Task task)
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
        }

        public Task GetTask(Task task)
        {
            return GetTaskByName(task.Name);
        }

        public Task GetTaskByName(string taskName)
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

        public Task GetTaskById(int id)
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

        private List<Task> DeserializeTasks(string jsonData)
        {
            try { return JsonConvert.DeserializeObject<List<Task>>(jsonData) ?? new List<Task>(); }
            catch { return new List<Task>(); }
        }
    }
}
