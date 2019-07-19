using NLog;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TaskScheduler
{
    public partial class MainForm : Form
    {
        private static MainForm _form;
        private static volatile object _lock;
        private static Logger logger;

        public IRepository Repository { get; }
        public List<TaskTimer> Timers { get; }

        public MainForm(IRepository repository)
        {
            logger = LogManager.GetCurrentClassLogger();
            _form = this;
            _lock = new object();
            Timers = new List<TaskTimer>();
            Repository = repository;

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            logger.Info("Form loaded.");

            GridUtils.OnLoadUpdate();
            GridUtils.SetGridTimer();
            GridUtils.AddUpdaters();
        }

        public static MainForm Form { get { lock (_lock) { return _form; } } }

        private void AddTaskButton_Click(object sender, EventArgs e)
        {
            if (!Form.IsValid) return;

            var task = Repository.AddTask(TaskUtils.CreateTask());

            FieldChecker.CheckFields(task);
        }

        void TasksDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // If click new row or header
            if (e.RowIndex == tasksDataGrid.NewRowIndex || e.RowIndex < 0)
                return;

            if (e.ColumnIndex == tasksDataGrid.Columns["dataGridViewDeleteButtonColumn"].Index)
            {
                if (MessageBox.Show("Are you sure you want to delete this task?", "Message",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var taskId = e.RowIndex;

                    TimerUtils.DisposeTimers(taskId);
                    Repository.DeleteTaskById(taskId);
                    GridUtils.UpdateGrid();
                }
            }
            else if (e.ColumnIndex == tasksDataGrid.Columns["dataGridViewStartButtonColumn"].Index)
            {
                var taskId = e.RowIndex;

                var task = Repository.GetTaskById(taskId);

                if (task == null)
                {
                    MessageBox.Show("Internal error. Task could not be found.");
                    return;
                }

                if (ProcessUtils.IsProcessRunning(task))
                {
                    MessageBox.Show("Task is already running.");
                    return;
                }

                TimerUtils.DisposeTimers(task);
                TaskStarter.GridStarter(task);
                GridUtils.UpdateGrid();
            }
        }

        private bool IsValid => Validation.IsValid;

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
            if (tabControl.SelectedIndex == 0)
                GridUtils.UpdateGrid();
        }

        public TimeSpan GetDontRunLongerThanValue()
        {
            var every = (float) runsLongerThanEvery.Value;
            var longerThan = IntervalUtils.GetDontRunLongerThanInterval();

            return TimeSpanUtils.GenerateTimeSpan(every, longerThan);
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

        private void ChooseFileButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    taskExecutablePath.Text = dialog.FileName;
                }
            }
        }
    }

}
