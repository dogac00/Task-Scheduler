using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskScheduler
{
    class GridUtils
    {
        public static void OnLoadUpdate()
        {
            FetchRowsFromJson();
            AddDeleteButtons();
            AddStartButtons();
        }

        public static void UpdateGrid()
        {
            FetchRowsFromJson();
        }

        public static void SetGridTimer()
        {
            var timer = TimerUtils.CreateTimer(() => 
            
                    GridActions.InvokeFormIfRequired(), 2000, 2000);

            TimerUtils.AddTimer(timer, "Grid", "Grid Updater", 2000, 2000);
        }

        public static void AddUpdaters()
        {
            List<Task> tasks = MainForm.Form.Repository.FetchAllData();

            foreach (var task in tasks)
                TaskUpdater.UpdateStatusForLoaded(task);
        }

        private static void AddDeleteButtons()
        {
            var deleteButtonColumn = new DataGridViewButtonColumn
            {
                Name = "dataGridViewDeleteButtonColumn",
                HeaderText = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true
            };

            MainForm.Form.TasksDataGrid.Columns.Add(deleteButtonColumn);
        }

        private static void AddStartButtons()
        {
            var startButtonColumn = new DataGridViewButtonColumn
            {
                Name = "dataGridViewStartButtonColumn",
                HeaderText = "Start",
                Text = "Start",
                UseColumnTextForButtonValue = true
            };

            MainForm.Form.TasksDataGrid.Columns.Add(startButtonColumn);
        }

        private static void FetchRowsFromJson()
        {
            List<Task> tasks = MainForm.Form.Repository.FetchAllData();
            List<GridTask> gridTasks = PopulateGridTaskList(tasks);

            var list = new BindingList<GridTask>(gridTasks);
            var source = new BindingSource(list, null);

            try { MainForm.Form.TasksDataGrid.DataSource = source; }
            catch { MessageBox.Show("Something went wrong while fetching JSON."); }
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
    }
}
