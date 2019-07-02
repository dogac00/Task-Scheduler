using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using FRUtility;
using Newtonsoft.Json;

namespace TaskScheduler
{
    public partial class Form1 : Form
    {
        private readonly EmailUtils emailUtils;
        private readonly Validation validation;
        System.Threading.Timer updateTimer;
        System.Threading.Timer checkEverySecondsTimer;

        public Form1()
        {
            Form = this;
            emailUtils = new EmailUtils();
            validation = new Validation(Form);
            InitializeComponent();
        }

        public static Form1 Form { get; private set; }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!this.IsValid()) return;

            var task = CreateTask();
            JsonUtils.AddTask(task);

            DoWorkForTimerAndProcess(task);
        }

        private void DoWorkForTimerAndProcess(Task task)
        {
            CheckForStartOnce(task);
            CheckForStartPeriodically(task);
        }

        private void CheckForStartPeriodically(Task task)
        {
            if (task.Period.Property == StartProperty.Periodically)
            {
                if (startPeriodicallyNowButton.Checked)
                {
                    RunTask(task);
                    UpdateIsRunningEverySeconds(task, 3);
                    RunTaskPeriodicallyNow(task);
                }
                else if (startPeriodicallySelectDateButton.Checked)
                {

                }
            }
        }

        private void RunTaskPeriodicallyNow(Task task)
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = task.Period.TimeBetween;

            System.Threading.Timer timer = null;
            timer = new System.Threading.Timer((e) =>
            {

                if (!task.IsRunning)
                {
                    MessageBox.Show("TimeBetween reached task is running...");
                    RunTask(task);
                }

            }, null, startTimeSpan, periodTimeSpan);
        }

        private void CheckForStartOnce(Task task)
        {
            if (task.Period.Property == StartProperty.Once)
            {
                if (startOnceNowButton.Checked)
                {
                    RunTask(task);
                    UpdateIsRunningEverySeconds(task, 5);
                }
                else if (startOnceSelectDateButton.Checked)
                {
                    RunTaskAfterSeconds(task, 5);
                }
            }
        }

        private void UpdateIsRunningEverySeconds(Task task, int seconds)
        {
            var startTimeSpan = TimeSpan.FromSeconds(3);
            var periodTimeSpan = TimeSpan.FromSeconds(seconds);

            updateTimer = new System.Threading.Timer((e) =>
            {
                if (!IsProcessRunning(task))
                {
                    if (task.IsRunning != false)
                    {
                        task.IsRunning = false;
                        JsonUtils.UpdateTask(task, false);
                    }
                }
                else
                {
                    if (task.IsRunning != true)
                    {
                        task.IsRunning = true;
                        JsonUtils.UpdateTask(task, true);
                    }
                }

            }, null, startTimeSpan, periodTimeSpan);
        }

        private bool IsProcessRunning(Task task)
        {
            Process[] processes = Process.GetProcessesByName(task.ExecutablePath);

            return processes.Length != 0;
        }

        private void RunTaskAfterSeconds(Task task, int seconds)
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(seconds);

            checkEverySecondsTimer = new System.Threading.Timer((e) =>
            {

                if (IsTimeReady(task)) checkEverySecondsTimer.Dispose();

            }, null, startTimeSpan, periodTimeSpan);
        }

        private bool IsTimeReady(Task task)
        {
            var ts = new TimeSpan(DateTime.Now.Ticks - task.Period.StartDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (!task.IsRunning && delta < 120)
            {
                RunTask(task);
                return true;
            }

            return false;
        }
        
        private void RunTask(Task task)
        {
            task.IsRunning = true;
            JsonUtils.UpdateTask(task, true);
            Process.Start(task.ExecutablePath);
        }

        private bool IsValid()
        {
            if (!validation.IsValidForNames()) return false;
            if (!validation.IsValidForNonClick()) return false;
            if (!validation.IsValidForEmail()) return false;

            return true;
        }

        private Task CreateTask()
        {
            Task task = new Task
            {
                Name = taskName.Text,
                ExecutablePath = taskExecutablePath.Text,
                IsRunning = false,
                Period = SetPeriod(),
                EmailInfo = emailUtils.SetEmailInfo()
            };

            return task;
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
            float every;
            Interval interval;

            if (prop == StartProperty.Periodically)
            {
                every = (float) startPeriodicallyEvery.Value;
                interval = GetInterval();
            }
            else
            {
                every = (float) startConsecutivelyDelay.Value;
                interval = GetInterval();
            }

            return TimeSpanUtils.GenerateTimeSpan(every, interval);
        }

        private Interval GetInterval()
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

        private void NotifyButton_CheckedChanged(object sender, EventArgs e)
        {
            if (notifyButton.Checked) runsLongerThanWeek.Checked = true;
        }
    }

}
