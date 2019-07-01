using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FRUtility;
using Newtonsoft.Json;

namespace TaskScheduler
{
    public partial class Form1 : Form
    {
        EmailUtils emailUtils;

        public Form1()
        {
            Form = this;
            emailUtils = new EmailUtils();
            InitializeComponent();
        }

        public static Form1 Form { get; private set; }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            AddTaskToJsonFile(CreateTask());
        }

        private Task CreateTask()
        {
            Task task;

            return task = new Task
            {
                Name = taskName.Text,
                ExecutablePath = taskExecutablePath.Text,
                Period = SetPeriod(),
                EmailInfo = emailUtils.SetEmailInfo()
            };
        }

        private void AddTaskToJsonFile(Task task)
        {
            var jsonString = JsonConvert.SerializeObject(task);

            var filePath = @"C:\Users\ndaky\source\repos\TaskScheduler\TaskScheduler\tasks.json";

            var jsonData = System.IO.File.ReadAllText(filePath);
            List<Task> tasksList;

            try
            {
                tasksList = JsonConvert.DeserializeObject<List<Task>>(jsonData);
            } catch { tasksList = new List<Task>(); }

            tasksList.Add(task);

            jsonData = JsonConvert.SerializeObject(tasksList);
            System.IO.File.WriteAllText(filePath, jsonData);
        }

        private TaskPeriod SetPeriod()
        {
            TaskPeriod period;

            if (startOnceButton.Checked)
            {
                period = new TaskPeriod
                {
                    Property = StartProperty.Once,
                    StartDate = GetStartDate(StartProperty.Once),
                    TimeBetween = TimeSpan.FromSeconds(0)
                };
            }
            else if (startPeriodicallyButton.Checked)
            {
                period = new TaskPeriod
                {
                    Property = StartProperty.Periodically,
                    StartDate = GetStartDate(StartProperty.Periodically),
                    TimeBetween = GetTimeBetween(StartProperty.Periodically)
                };
            }
            else
            {
                period = new TaskPeriod
                {
                    Property = StartProperty.Consecutively,
                    StartDate = GetStartDate(StartProperty.Consecutively),
                    TimeBetween = GetTimeBetween(StartProperty.Consecutively)
                };
            }

            return period;
        }

        private DateTime GetStartDate(StartProperty prop)
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

        private TimeSpan GetTimeBetween(StartProperty prop)
        {
            int every;
            Interval interval;

            if (prop == StartProperty.Periodically)
            {
                every = int.Parse(startPeriodicallyEvery.SelectedItem.ToString());
                interval = GetInterval();
            }
            else
            {
                every = int.Parse(startConsecutivelyDelay.SelectedItem.ToString());
                interval = GetInterval();
            }

            return TimeSpanUtils.GenerateTimeSpan(every, interval);
        }

        private Interval GetInterval()
        {
            if (startPeriodicallyWeek.Checked || startConsecutivelyWeek.Checked)
                return Interval.Week;
            else if (startPeriodicallyDay.Checked || startConsecutivelyDay.Checked)
                return Interval.Day;
            else if (startPeriodicallyHour.Checked || startConsecutivelyHour.Checked)
                return Interval.Hour;
            else
                return Interval.Min;
        }

        private void StartConsecutivelyWeek_CheckedChanged(object sender, EventArgs e)
        {

        }

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

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

}
