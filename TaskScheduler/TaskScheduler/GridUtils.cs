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
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(3);

            System.Threading.Timer timer = null;

            timer = new System.Threading.Timer((e) =>
            {

                Form1.Form.Invoke(new Action(() => {
                    UpdateGrid();
                }));

            }, null, startTimeSpan, periodTimeSpan);

            Form1.Form.Timers.Add(timer);
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
            List<GridTask> gridTasks = JsonUtils.PopulateGridTaskList(tasks);

            var list = new BindingList<GridTask>(gridTasks);
            var source = new BindingSource(list, null);

            try { Form1.Form.TasksDataGrid.DataSource = source; }
            catch { /* Exception can occur due to cross-threading in UI */ }
        }
    }
}
