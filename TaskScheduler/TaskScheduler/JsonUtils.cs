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

        public static void AddTask(Task task)
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

        public static void UpdateTask(Task task, bool isRunning)
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
                    }
                }

                jsonData = JsonConvert.SerializeObject(tasksList);

                File.WriteAllText(jsonFilePath, jsonData);
            } catch { }
        }

        private static List<Task> DeserializeTasks(string jsonData)
        {
            try { return JsonConvert.DeserializeObject<List<Task>>(jsonData) ?? new List<Task>(); }
            catch { return new List<Task>(); }
        }
    }
}
