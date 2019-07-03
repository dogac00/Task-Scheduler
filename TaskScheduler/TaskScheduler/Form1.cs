using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using FRUtility;

namespace TaskScheduler
{
    public partial class Form1 : Form
    {
        private readonly Validation validation;
        public List<System.Threading.Timer> Timers;

        public Form1()
        {
            Form = this;
            validation = new Validation(Form);
            Timers = new List<System.Threading.Timer>();
            InitializeComponent();
        }

        public static Form1 Form { get; private set; }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!this.IsValid()) return;

            var task = TaskUtils.CreateTask();
            JsonUtils.AddTask(task);

            DoWorkForTimerAndProcess(task);
        }

        private void DoWorkForTimerAndProcess(Task task)
        {
            CheckForStartOnce(task);
            CheckForStartPeriodically(task);
            CheckForStartConsecutively(task);
        }

        private void CheckForStartConsecutively(Task task)
        {
            if (task.Period.Property == StartProperty.Consecutively)
            {
                if (startConsecutivelyNowButton.Checked)
                {
                    TaskUtils.RunTaskConsecutively(task);
                }
            }
        }

        private void CheckForStartPeriodically(Task task)
        {
            if (task.Period.Property == StartProperty.Periodically)
            {
                if (startPeriodicallyNowButton.Checked)
                {
                    TaskUtils.StartTaskForPeriodical(task);
                }
                else if (startPeriodicallySelectDateButton.Checked)
                {
                    TaskUtils.SetTaskStartingTimer(task, 5);
                }
            }
        }

        private void CheckForStartOnce(Task task)
        {
            if (task.Period.Property == StartProperty.Once)
            {
                if (startOnceNowButton.Checked)
                {
                    if (TaskUtils.RunTask(task))
                    {
                        TaskUtils.UpdateStatusEverySeconds(task, 5);
                    }
                }
                else if (startOnceSelectDateButton.Checked)
                {
                    TaskUtils.SetTaskStartingTimer(task, 5);
                }
            }
        }

        

        private bool IsValid()
        {
            if (!validation.IsValidForNames()) return false;
            if (!validation.IsValidForNonClick()) return false;
            if (!validation.IsValidForEmail()) return false;
            if (!validation.IsValidNumericUpDown()) return false;
            if (!validation.IsTaskNameValid()) return false;

            return true;
        }

        public Interval GetInterval()
        {
            if (startPeriodicallyButton.Checked)
            {
                if (startPeriodicallyWeek.Checked)
                    return Interval.Week;
                else if (startPeriodicallyDay.Checked)
                    return Interval.Day;
                else if (startPeriodicallyHour.Checked)
                    return Interval.Hour;
                else
                    return Interval.Min;
            }
            else
            {
                if (startConsecutivelyWeek.Checked)
                    return Interval.Week;
                else if (startConsecutivelyDay.Checked)
                    return Interval.Day;
                else if (startConsecutivelyHour.Checked)
                    return Interval.Hour;
                else
                    return Interval.Min;
            }
        }

        public DateTime GetStartDate(StartProperty prop)
        {
            if (prop == StartProperty.Once)
            {
                if (startOnceNowButton.Checked)
                    return DateTime.Now;

                return startOnceDateTimePicker.Value;
            }
            else if (prop == StartProperty.Periodically)
            {
                if (startPeriodicallyNowButton.Checked)
                    return DateTime.Now;

                return startPeriodicallyDateTimePicker.Value;
            }
            else
            {
                if (startConsecutivelyNowButton.Checked)
                    return DateTime.Now;

                return startConsecutivelyDateTimePicker.Value;
            }
        }

        // Form Click Event Handlers from now on...

        private void StartOnceNowButton_CheckedChanged(object sender, EventArgs e)
        {
            startOnceDateTimePicker.Visible = false;
        }

        private void StartOnceSelectDateButton_CheckedChanged(object sender, EventArgs e)
        {
            startOnceDateTimePicker.Visible = true;
        }

        private void StartPeriodicallySelectDateButton_CheckedChanged(object sender, EventArgs e)
        {
            startPeriodicallyDateTimePicker.Visible = true;
        }

        private void StartPeriodicallyNowButton_CheckedChanged(object sender, EventArgs e)
        {
            startPeriodicallyDateTimePicker.Visible = false;
        }

        private void StartOnceButton_CheckedChanged(object sender, EventArgs e)
        {
            startOncePanel.Visible = true;
            startPeriodicallyPanel.Visible = false;
            startConsecutivelyPanel.Visible = false;
        }

        private void StartPeriodicallyButton_CheckedChanged(object sender, EventArgs e)
        {
            startOncePanel.Visible = false;
            startPeriodicallyPanel.Visible = true;
            startConsecutivelyPanel.Visible = false;
        }

        private void StartConsecutivelyButton_CheckedChanged(object sender, EventArgs e)
        {
            startOncePanel.Visible = false;
            startPeriodicallyPanel.Visible = false;
            startConsecutivelyPanel.Visible = true;
        }

        private void StartConsecutivelyNowButton_CheckedChanged(object sender, EventArgs e)
        {
            startConsecutivelyDateTimePicker.Visible = false;
        }

        private void StartConsecutivelySelectDateButton_CheckedChanged(object sender, EventArgs e)
        {
            startConsecutivelyDateTimePicker.Visible = true;
        }

        private void NotifyButton_CheckedChanged(object sender, EventArgs e)
        {
            if (notifyButton.Checked) runsLongerThanWeek.Checked = true;
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 1)
            {
                GridUtils.UpdateGrid();
            }
        }

        private void UpdateGridButton_Click(object sender, EventArgs e)
        {
            GridUtils.UpdateGrid();
        }

        void TasksDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // if click is on new row or header row
            if (e.RowIndex == tasksDataGrid.NewRowIndex || e.RowIndex < 0)
                return;

            //Check if click is on specific column 
            if (e.ColumnIndex == tasksDataGrid.Columns["dataGridViewDeleteButtonColumn"].Index)
            {
                // Put some logic here, for example to remove row from your binding list.

                if (MessageBox.Show("Are you sure you want to delete this task?", "Message", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {

                    var taskId = e.RowIndex;

                    if (JsonUtils.DeleteTask(taskId))
                    {
                        GridUtils.UpdateGrid();
                    } else
                    {
                        MessageBox.Show("Row cannot be deleted.");
                    }

                }
            }
            else if (e.ColumnIndex == tasksDataGrid.Columns["dataGridViewStartButtonColumn"].Index)
            {
                // Put some logic here, for example to remove row from your binding list.
                var taskId = e.RowIndex;

                MessageBox.Show($"Task id is : {taskId}");
            }
        }

        private void CheckProcess_Click(object sender, EventArgs e)
        {
            try
            {
                var processId = int.Parse(processPath.Text);

                var result = ProcessUtils.IsProcessRunning(processId);

                isRunningLabel.Text = result.ToString();
            }
            catch
            {
                MessageBox.Show("Process path is incorrect.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GridUtils.OnLoadUpdate();
        }
    }

}
