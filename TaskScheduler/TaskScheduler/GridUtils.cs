using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskScheduler
{
    static class GridUtils
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
            System.Threading.Timer timer = null;

            timer = new System.Threading.Timer((e) =>
            {

                Form1.Form.InvokeIfRequired(UpdateGrid);

            }, null, 2000, 2000);

            Form1.Form.Timers.Add(timer);
        }

        public static void AddUpdaters()
        {
            List<Task> tasks = JsonUtils.FetchJsonData();

            foreach (var task in tasks)
                TaskUpdater.UpdateStatusForLoaded(task);
        }

        private static void AddDeleteButtons()
        {
            var deleteButtonColumn = new DataGridViewButtonColumn();

            deleteButtonColumn.Name = "dataGridViewDeleteButtonColumn";
            deleteButtonColumn.HeaderText = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true;

            Form1.Form.TasksDataGrid.Columns.Add(deleteButtonColumn);
        }

        private static void AddStartButtons()
        {
            var startButtonColumn = new DataGridViewButtonColumn();

            startButtonColumn.Name = "dataGridViewStartButtonColumn";
            startButtonColumn.HeaderText = "Start";
            startButtonColumn.Text = "Start";
            startButtonColumn.UseColumnTextForButtonValue = true;

            Form1.Form.TasksDataGrid.Columns.Add(startButtonColumn);
        }

        private static void FetchRowsFromJson()
        {
            List<Task> tasks = JsonUtils.FetchJsonData();
            List<GridTask> gridTasks = PopulateGridTaskList(tasks);

            var list = new BindingList<GridTask>(gridTasks);
            var source = new BindingSource(list, null);

            try { Form1.Form.TasksDataGrid.DataSource = source; }
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
