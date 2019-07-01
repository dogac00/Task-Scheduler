using System.Windows.Forms;

namespace TaskScheduler
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.RunsLongerThanMin = new System.Windows.Forms.RadioButton();
            this.RunsLongerThanHour = new System.Windows.Forms.RadioButton();
            this.RunsLongerThanDay = new System.Windows.Forms.RadioButton();
            this.RunsLongerThanWeek = new System.Windows.Forms.RadioButton();
            this.RunsLongerThanEvery = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.startConsecutivelyMin = new System.Windows.Forms.RadioButton();
            this.startConsecutivelyHour = new System.Windows.Forms.RadioButton();
            this.startConsecutivelyDay = new System.Windows.Forms.RadioButton();
            this.startConsecutivelyWeek = new System.Windows.Forms.RadioButton();
            this.startConsecutivelyDelay = new System.Windows.Forms.ComboBox();
            this.startConsecutivelyDelayText = new System.Windows.Forms.Label();
            this.startConsecutivelyNowButton = new System.Windows.Forms.RadioButton();
            this.startConsecutivelyDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.startConsecutivelySelectDateButton = new System.Windows.Forms.RadioButton();
            this.startPeriodicallyMin = new System.Windows.Forms.RadioButton();
            this.startPeriodicallyHour = new System.Windows.Forms.RadioButton();
            this.startPeriodicallyDay = new System.Windows.Forms.RadioButton();
            this.startPeriodicallyWeek = new System.Windows.Forms.RadioButton();
            this.startPeriodicallyEvery = new System.Windows.Forms.ComboBox();
            this.startPeriodicallyEveryText = new System.Windows.Forms.Label();
            this.startPeriodicallyTimeText = new System.Windows.Forms.Label();
            this.startPeriodicallyNowButton = new System.Windows.Forms.RadioButton();
            this.startPeriodicallyDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.startPeriodicallySelectDateButton = new System.Windows.Forms.RadioButton();
            this.startOnceNowButton = new System.Windows.Forms.RadioButton();
            this.startOnceDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.startOnceSelectDateButton = new System.Windows.Forms.RadioButton();
            this.startConsecutivelyButton = new System.Windows.Forms.RadioButton();
            this.startPeriodicallyButton = new System.Windows.Forms.RadioButton();
            this.startOnceButton = new System.Windows.Forms.RadioButton();
            this.taskExecutablePath = new System.Windows.Forms.TextBox();
            this.taskName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.NotifyButton = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.EmailAddressTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.startPeriodicallyIntervalPanel = new System.Windows.Forms.Panel();
            this.startConsecutivelyIntervalPanel = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.runsLongerThanPanel = new System.Windows.Forms.Panel();
            this.startOncePanel = new System.Windows.Forms.Panel();
            this.startPeriodicallyPanel = new System.Windows.Forms.Panel();
            this.startConsecutivelyPanel = new System.Windows.Forms.Panel();
            this.TabControl1.SuspendLayout();
            this.TabPage3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.startPeriodicallyIntervalPanel.SuspendLayout();
            this.startConsecutivelyIntervalPanel.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.runsLongerThanPanel.SuspendLayout();
            this.startOncePanel.SuspendLayout();
            this.startPeriodicallyPanel.SuspendLayout();
            this.startConsecutivelyPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.TabControl1.Controls.Add(this.TabPage1);
            this.TabControl1.Controls.Add(this.TabPage2);
            this.TabControl1.Controls.Add(this.TabPage3);
            this.TabControl1.Location = new System.Drawing.Point(12, 12);
            this.TabControl1.Name = "tabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(799, 418);
            this.TabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.TabPage1.Location = new System.Drawing.Point(4, 22);
            this.TabPage1.Name = "tabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(710, 369);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Active Task Status";
            this.TabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.TabPage2.Location = new System.Drawing.Point(4, 22);
            this.TabPage2.Name = "tabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(710, 369);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Tasks";
            this.TabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.TabPage3.Controls.Add(this.startConsecutivelyPanel);
            this.TabPage3.Controls.Add(this.startPeriodicallyPanel);
            this.TabPage3.Controls.Add(this.startOncePanel);
            this.TabPage3.Controls.Add(this.runsLongerThanPanel);
            this.TabPage3.Controls.Add(this.panel6);
            this.TabPage3.Controls.Add(this.EmailAddressTextBox);
            this.TabPage3.Controls.Add(this.label6);
            this.TabPage3.Controls.Add(this.NotifyButton);
            this.TabPage3.Controls.Add(this.RunsLongerThanEvery);
            this.TabPage3.Controls.Add(this.button1);
            this.TabPage3.Controls.Add(this.taskExecutablePath);
            this.TabPage3.Controls.Add(this.taskName);
            this.TabPage3.Controls.Add(this.label2);
            this.TabPage3.Controls.Add(this.label1);
            this.TabPage3.Location = new System.Drawing.Point(4, 22);
            this.TabPage3.Name = "tabPage3";
            this.TabPage3.Size = new System.Drawing.Size(791, 392);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Add Task";
            this.TabPage3.UseVisualStyleBackColor = true;
            // 
            // runsLongerThanMin
            // 
            this.RunsLongerThanMin.AutoSize = true;
            this.RunsLongerThanMin.Location = new System.Drawing.Point(175, 10);
            this.RunsLongerThanMin.Name = "runsLongerThanMin";
            this.RunsLongerThanMin.Size = new System.Drawing.Size(42, 17);
            this.RunsLongerThanMin.TabIndex = 36;
            this.RunsLongerThanMin.Text = "Min";
            this.RunsLongerThanMin.UseVisualStyleBackColor = true;
            // 
            // runsLongerThanHour
            // 
            this.RunsLongerThanHour.AutoSize = true;
            this.RunsLongerThanHour.Location = new System.Drawing.Point(121, 10);
            this.RunsLongerThanHour.Name = "runsLongerThanHour";
            this.RunsLongerThanHour.Size = new System.Drawing.Size(48, 17);
            this.RunsLongerThanHour.TabIndex = 35;
            this.RunsLongerThanHour.Text = "Hour";
            this.RunsLongerThanHour.UseVisualStyleBackColor = true;
            // 
            // runsLongerThanDay
            // 
            this.RunsLongerThanDay.AutoSize = true;
            this.RunsLongerThanDay.Location = new System.Drawing.Point(73, 10);
            this.RunsLongerThanDay.Name = "runsLongerThanDay";
            this.RunsLongerThanDay.Size = new System.Drawing.Size(44, 17);
            this.RunsLongerThanDay.TabIndex = 34;
            this.RunsLongerThanDay.Text = "Day";
            this.RunsLongerThanDay.UseVisualStyleBackColor = true;
            // 
            // runsLongerThanWeek
            // 
            this.RunsLongerThanWeek.AutoSize = true;
            this.RunsLongerThanWeek.Location = new System.Drawing.Point(13, 10);
            this.RunsLongerThanWeek.Name = "runsLongerThanWeek";
            this.RunsLongerThanWeek.Size = new System.Drawing.Size(54, 17);
            this.RunsLongerThanWeek.TabIndex = 33;
            this.RunsLongerThanWeek.Text = "Week";
            this.RunsLongerThanWeek.UseVisualStyleBackColor = true;
            // 
            // runsLongerThanEvery
            // 
            this.RunsLongerThanEvery.FormattingEnabled = true;
            this.RunsLongerThanEvery.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30"});
            this.RunsLongerThanEvery.Location = new System.Drawing.Point(230, 292);
            this.RunsLongerThanEvery.Name = "runsLongerThanEvery";
            this.RunsLongerThanEvery.Size = new System.Drawing.Size(46, 21);
            this.RunsLongerThanEvery.TabIndex = 32;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(248, 328);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 23);
            this.button1.TabIndex = 30;
            this.button1.Text = "Add Task";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // startConsecutivelyMin
            // 
            this.startConsecutivelyMin.AutoSize = true;
            this.startConsecutivelyMin.Location = new System.Drawing.Point(183, 5);
            this.startConsecutivelyMin.Name = "startConsecutivelyMin";
            this.startConsecutivelyMin.Size = new System.Drawing.Size(42, 17);
            this.startConsecutivelyMin.TabIndex = 29;
            this.startConsecutivelyMin.Text = "Min";
            this.startConsecutivelyMin.UseVisualStyleBackColor = true;
            // 
            // startConsecutivelyHour
            // 
            this.startConsecutivelyHour.AutoSize = true;
            this.startConsecutivelyHour.Location = new System.Drawing.Point(129, 5);
            this.startConsecutivelyHour.Name = "startConsecutivelyHour";
            this.startConsecutivelyHour.Size = new System.Drawing.Size(48, 17);
            this.startConsecutivelyHour.TabIndex = 28;
            this.startConsecutivelyHour.Text = "Hour";
            this.startConsecutivelyHour.UseVisualStyleBackColor = true;
            // 
            // startConsecutivelyDay
            // 
            this.startConsecutivelyDay.AutoSize = true;
            this.startConsecutivelyDay.Location = new System.Drawing.Point(75, 5);
            this.startConsecutivelyDay.Name = "startConsecutivelyDay";
            this.startConsecutivelyDay.Size = new System.Drawing.Size(44, 17);
            this.startConsecutivelyDay.TabIndex = 27;
            this.startConsecutivelyDay.Text = "Day";
            this.startConsecutivelyDay.UseVisualStyleBackColor = true;
            // 
            // startConsecutivelyWeek
            // 
            this.startConsecutivelyWeek.AutoSize = true;
            this.startConsecutivelyWeek.Location = new System.Drawing.Point(15, 5);
            this.startConsecutivelyWeek.Name = "startConsecutivelyWeek";
            this.startConsecutivelyWeek.Size = new System.Drawing.Size(54, 17);
            this.startConsecutivelyWeek.TabIndex = 26;
            this.startConsecutivelyWeek.Text = "Week";
            this.startConsecutivelyWeek.UseVisualStyleBackColor = true;
            this.startConsecutivelyWeek.CheckedChanged += new System.EventHandler(this.StartConsecutivelyWeek_CheckedChanged);
            // 
            // startConsecutivelyDelay
            // 
            this.startConsecutivelyDelay.FormattingEnabled = true;
            this.startConsecutivelyDelay.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30"});
            this.startConsecutivelyDelay.Location = new System.Drawing.Point(75, 39);
            this.startConsecutivelyDelay.Name = "startConsecutivelyDelay";
            this.startConsecutivelyDelay.Size = new System.Drawing.Size(46, 21);
            this.startConsecutivelyDelay.TabIndex = 25;
            // 
            // startConsecutivelyDelayText
            // 
            this.startConsecutivelyDelayText.AutoSize = true;
            this.startConsecutivelyDelayText.Location = new System.Drawing.Point(35, 42);
            this.startConsecutivelyDelayText.Name = "startConsecutivelyDelayText";
            this.startConsecutivelyDelayText.Size = new System.Drawing.Size(34, 13);
            this.startConsecutivelyDelayText.TabIndex = 24;
            this.startConsecutivelyDelayText.Text = "Delay";
            // 
            // startConsecutivelyNowButton
            // 
            this.startConsecutivelyNowButton.AutoSize = true;
            this.startConsecutivelyNowButton.Checked = true;
            this.startConsecutivelyNowButton.Location = new System.Drawing.Point(17, 3);
            this.startConsecutivelyNowButton.Name = "startConsecutivelyNowButton";
            this.startConsecutivelyNowButton.Size = new System.Drawing.Size(47, 17);
            this.startConsecutivelyNowButton.TabIndex = 22;
            this.startConsecutivelyNowButton.TabStop = true;
            this.startConsecutivelyNowButton.Text = "Now";
            this.startConsecutivelyNowButton.UseVisualStyleBackColor = true;
            this.startConsecutivelyNowButton.CheckedChanged += new System.EventHandler(this.StartConsecutivelyNowButton_CheckedChanged);
            // 
            // startConsecutivelyDateTimePicker
            // 
            this.startConsecutivelyDateTimePicker.Location = new System.Drawing.Point(211, 5);
            this.startConsecutivelyDateTimePicker.Name = "startConsecutivelyDateTimePicker";
            this.startConsecutivelyDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.startConsecutivelyDateTimePicker.TabIndex = 21;
            this.startConsecutivelyDateTimePicker.Visible = false;
            // 
            // startConsecutivelySelectDateButton
            // 
            this.startConsecutivelySelectDateButton.AutoSize = true;
            this.startConsecutivelySelectDateButton.Location = new System.Drawing.Point(94, 3);
            this.startConsecutivelySelectDateButton.Name = "startConsecutivelySelectDateButton";
            this.startConsecutivelySelectDateButton.Size = new System.Drawing.Size(81, 17);
            this.startConsecutivelySelectDateButton.TabIndex = 20;
            this.startConsecutivelySelectDateButton.Text = "Select Date";
            this.startConsecutivelySelectDateButton.UseVisualStyleBackColor = true;
            this.startConsecutivelySelectDateButton.CheckedChanged += new System.EventHandler(this.StartConsecutivelySelectDateButton_CheckedChanged);
            // 
            // startPeriodicallyMin
            // 
            this.startPeriodicallyMin.AutoSize = true;
            this.startPeriodicallyMin.Location = new System.Drawing.Point(183, 3);
            this.startPeriodicallyMin.Name = "startPeriodicallyMin";
            this.startPeriodicallyMin.Size = new System.Drawing.Size(42, 17);
            this.startPeriodicallyMin.TabIndex = 19;
            this.startPeriodicallyMin.Text = "Min";
            this.startPeriodicallyMin.UseVisualStyleBackColor = true;
            // 
            // startPeriodicallyHour
            // 
            this.startPeriodicallyHour.AutoSize = true;
            this.startPeriodicallyHour.Location = new System.Drawing.Point(129, 3);
            this.startPeriodicallyHour.Name = "startPeriodicallyHour";
            this.startPeriodicallyHour.Size = new System.Drawing.Size(48, 17);
            this.startPeriodicallyHour.TabIndex = 18;
            this.startPeriodicallyHour.Text = "Hour";
            this.startPeriodicallyHour.UseVisualStyleBackColor = true;
            // 
            // startPeriodicallyDay
            // 
            this.startPeriodicallyDay.AutoSize = true;
            this.startPeriodicallyDay.Location = new System.Drawing.Point(75, 3);
            this.startPeriodicallyDay.Name = "startPeriodicallyDay";
            this.startPeriodicallyDay.Size = new System.Drawing.Size(44, 17);
            this.startPeriodicallyDay.TabIndex = 17;
            this.startPeriodicallyDay.Text = "Day";
            this.startPeriodicallyDay.UseVisualStyleBackColor = true;
            // 
            // startPeriodicallyWeek
            // 
            this.startPeriodicallyWeek.AutoSize = true;
            this.startPeriodicallyWeek.Location = new System.Drawing.Point(15, 3);
            this.startPeriodicallyWeek.Name = "startPeriodicallyWeek";
            this.startPeriodicallyWeek.Size = new System.Drawing.Size(54, 17);
            this.startPeriodicallyWeek.TabIndex = 16;
            this.startPeriodicallyWeek.Text = "Week";
            this.startPeriodicallyWeek.UseVisualStyleBackColor = true;
            // 
            // startPeriodicallyEvery
            // 
            this.startPeriodicallyEvery.FormattingEnabled = true;
            this.startPeriodicallyEvery.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30"});
            this.startPeriodicallyEvery.Location = new System.Drawing.Point(169, 45);
            this.startPeriodicallyEvery.Name = "startPeriodicallyEvery";
            this.startPeriodicallyEvery.Size = new System.Drawing.Size(46, 21);
            this.startPeriodicallyEvery.TabIndex = 15;
            // 
            // startPeriodicallyEveryText
            // 
            this.startPeriodicallyEveryText.AutoSize = true;
            this.startPeriodicallyEveryText.Location = new System.Drawing.Point(129, 48);
            this.startPeriodicallyEveryText.Name = "startPeriodicallyEveryText";
            this.startPeriodicallyEveryText.Size = new System.Drawing.Size(34, 13);
            this.startPeriodicallyEveryText.TabIndex = 14;
            this.startPeriodicallyEveryText.Text = "Every";
            // 
            // startPeriodicallyTimeText
            // 
            this.startPeriodicallyTimeText.AutoSize = true;
            this.startPeriodicallyTimeText.Location = new System.Drawing.Point(7, 48);
            this.startPeriodicallyTimeText.Name = "startPeriodicallyTimeText";
            this.startPeriodicallyTimeText.Size = new System.Drawing.Size(116, 13);
            this.startPeriodicallyTimeText.TabIndex = 13;
            this.startPeriodicallyTimeText.Text = "Time Between Periods:";
            // 
            // startPeriodicallyNowButton
            // 
            this.startPeriodicallyNowButton.AutoSize = true;
            this.startPeriodicallyNowButton.Checked = true;
            this.startPeriodicallyNowButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.startPeriodicallyNowButton.Location = new System.Drawing.Point(20, 7);
            this.startPeriodicallyNowButton.Name = "startPeriodicallyNowButton";
            this.startPeriodicallyNowButton.Size = new System.Drawing.Size(47, 17);
            this.startPeriodicallyNowButton.TabIndex = 12;
            this.startPeriodicallyNowButton.TabStop = true;
            this.startPeriodicallyNowButton.Text = "Now";
            this.startPeriodicallyNowButton.UseVisualStyleBackColor = true;
            this.startPeriodicallyNowButton.CheckedChanged += new System.EventHandler(this.StartPeriodicallyNowButton_CheckedChanged);
            // 
            // startPeriodicallyDateTimePicker
            // 
            this.startPeriodicallyDateTimePicker.Location = new System.Drawing.Point(226, 10);
            this.startPeriodicallyDateTimePicker.Name = "startPeriodicallyDateTimePicker";
            this.startPeriodicallyDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.startPeriodicallyDateTimePicker.TabIndex = 11;
            this.startPeriodicallyDateTimePicker.Visible = false;
            // 
            // startPeriodicallySelectDateButton
            // 
            this.startPeriodicallySelectDateButton.AutoSize = true;
            this.startPeriodicallySelectDateButton.Location = new System.Drawing.Point(91, 7);
            this.startPeriodicallySelectDateButton.Name = "startPeriodicallySelectDateButton";
            this.startPeriodicallySelectDateButton.Size = new System.Drawing.Size(81, 17);
            this.startPeriodicallySelectDateButton.TabIndex = 10;
            this.startPeriodicallySelectDateButton.Text = "Select Date";
            this.startPeriodicallySelectDateButton.UseVisualStyleBackColor = true;
            this.startPeriodicallySelectDateButton.CheckedChanged += new System.EventHandler(this.StartPeriodicallySelectDateButton_CheckedChanged);
            // 
            // startOnceNowButton
            // 
            this.startOnceNowButton.AutoSize = true;
            this.startOnceNowButton.Checked = true;
            this.startOnceNowButton.Location = new System.Drawing.Point(27, 4);
            this.startOnceNowButton.Name = "startOnceNowButton";
            this.startOnceNowButton.Size = new System.Drawing.Size(47, 17);
            this.startOnceNowButton.TabIndex = 9;
            this.startOnceNowButton.TabStop = true;
            this.startOnceNowButton.Text = "Now";
            this.startOnceNowButton.UseVisualStyleBackColor = true;
            this.startOnceNowButton.CheckedChanged += new System.EventHandler(this.StartOnceNowButton_CheckedChanged);
            // 
            // startOnceDateTimePicker
            // 
            this.startOnceDateTimePicker.Location = new System.Drawing.Point(209, 14);
            this.startOnceDateTimePicker.Name = "startOnceDateTimePicker";
            this.startOnceDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.startOnceDateTimePicker.TabIndex = 8;
            this.startOnceDateTimePicker.Visible = false;
            // 
            // startOnceSelectDateButton
            // 
            this.startOnceSelectDateButton.AutoSize = true;
            this.startOnceSelectDateButton.Location = new System.Drawing.Point(95, 4);
            this.startOnceSelectDateButton.Name = "startOnceSelectDateButton";
            this.startOnceSelectDateButton.Size = new System.Drawing.Size(81, 17);
            this.startOnceSelectDateButton.TabIndex = 7;
            this.startOnceSelectDateButton.Text = "Select Date";
            this.startOnceSelectDateButton.UseVisualStyleBackColor = true;
            this.startOnceSelectDateButton.CheckedChanged += new System.EventHandler(this.StartOnceSelectDateButton_CheckedChanged);
            // 
            // startConsecutivelyButton
            // 
            this.startConsecutivelyButton.AutoSize = true;
            this.startConsecutivelyButton.Location = new System.Drawing.Point(7, 114);
            this.startConsecutivelyButton.Name = "startConsecutivelyButton";
            this.startConsecutivelyButton.Size = new System.Drawing.Size(116, 17);
            this.startConsecutivelyButton.TabIndex = 6;
            this.startConsecutivelyButton.Text = "Start Consecutively";
            this.startConsecutivelyButton.UseVisualStyleBackColor = true;
            this.startConsecutivelyButton.CheckedChanged += new System.EventHandler(this.StartConsecutivelyButton_CheckedChanged);
            // 
            // startPeriodicallyButton
            // 
            this.startPeriodicallyButton.AutoSize = true;
            this.startPeriodicallyButton.Location = new System.Drawing.Point(19, 50);
            this.startPeriodicallyButton.Name = "startPeriodicallyButton";
            this.startPeriodicallyButton.Size = new System.Drawing.Size(103, 17);
            this.startPeriodicallyButton.TabIndex = 5;
            this.startPeriodicallyButton.Text = "Start Periodically";
            this.startPeriodicallyButton.UseVisualStyleBackColor = true;
            this.startPeriodicallyButton.CheckedChanged += new System.EventHandler(this.StartPeriodicallyButton_CheckedChanged);
            // 
            // startOnceButton
            // 
            this.startOnceButton.AutoSize = true;
            this.startOnceButton.Location = new System.Drawing.Point(28, 4);
            this.startOnceButton.Name = "startOnceButton";
            this.startOnceButton.Size = new System.Drawing.Size(76, 17);
            this.startOnceButton.TabIndex = 4;
            this.startOnceButton.Text = "Start Once";
            this.startOnceButton.UseVisualStyleBackColor = true;
            this.startOnceButton.CheckedChanged += new System.EventHandler(this.StartOnceButton_CheckedChanged);
            // 
            // taskExecutablePath
            // 
            this.taskExecutablePath.Location = new System.Drawing.Point(154, 53);
            this.taskExecutablePath.Name = "taskExecutablePath";
            this.taskExecutablePath.Size = new System.Drawing.Size(203, 20);
            this.taskExecutablePath.TabIndex = 3;
            // 
            // taskName
            // 
            this.taskName.Location = new System.Drawing.Point(154, 19);
            this.taskName.Name = "taskName";
            this.taskName.Size = new System.Drawing.Size(114, 20);
            this.taskName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Task Executable Path:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Task Name:";
            // 
            // notifyButton
            // 
            this.NotifyButton.AutoSize = true;
            this.NotifyButton.Location = new System.Drawing.Point(24, 292);
            this.NotifyButton.Name = "notifyButton";
            this.NotifyButton.Size = new System.Drawing.Size(194, 17);
            this.NotifyButton.TabIndex = 37;
            this.NotifyButton.Text = "Notify by Email if Runs Longer Than";
            this.NotifyButton.UseVisualStyleBackColor = true;
            this.NotifyButton.CheckedChanged += new System.EventHandler(this.RadioButton1_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(513, 299);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 13);
            this.label6.TabIndex = 38;
            this.label6.Text = "To:";
            // 
            // emailAddressTextBox
            // 
            this.EmailAddressTextBox.Location = new System.Drawing.Point(535, 297);
            this.EmailAddressTextBox.Name = "emailAddressTextBox";
            this.EmailAddressTextBox.Size = new System.Drawing.Size(152, 20);
            this.EmailAddressTextBox.TabIndex = 39;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.startOnceNowButton);
            this.panel1.Controls.Add(this.startOnceSelectDateButton);
            this.panel1.Location = new System.Drawing.Point(3, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 29);
            this.panel1.TabIndex = 40;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.startPeriodicallyNowButton);
            this.panel2.Controls.Add(this.startPeriodicallySelectDateButton);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 33);
            this.panel2.TabIndex = 41;
            // 
            // startPeriodicallyIntervalPanel
            // 
            this.startPeriodicallyIntervalPanel.Controls.Add(this.startPeriodicallyWeek);
            this.startPeriodicallyIntervalPanel.Controls.Add(this.startPeriodicallyDay);
            this.startPeriodicallyIntervalPanel.Controls.Add(this.startPeriodicallyHour);
            this.startPeriodicallyIntervalPanel.Controls.Add(this.startPeriodicallyMin);
            this.startPeriodicallyIntervalPanel.Location = new System.Drawing.Point(224, 41);
            this.startPeriodicallyIntervalPanel.Name = "startPeriodicallyIntervalPanel";
            this.startPeriodicallyIntervalPanel.Size = new System.Drawing.Size(235, 25);
            this.startPeriodicallyIntervalPanel.TabIndex = 42;
            // 
            // startConsecutivelyIntervalPanel
            // 
            this.startConsecutivelyIntervalPanel.Controls.Add(this.startConsecutivelyWeek);
            this.startConsecutivelyIntervalPanel.Controls.Add(this.startConsecutivelyDay);
            this.startConsecutivelyIntervalPanel.Controls.Add(this.startConsecutivelyHour);
            this.startConsecutivelyIntervalPanel.Controls.Add(this.startConsecutivelyMin);
            this.startConsecutivelyIntervalPanel.Location = new System.Drawing.Point(127, 35);
            this.startConsecutivelyIntervalPanel.Name = "startConsecutivelyIntervalPanel";
            this.startConsecutivelyIntervalPanel.Size = new System.Drawing.Size(235, 25);
            this.startConsecutivelyIntervalPanel.TabIndex = 43;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.startConsecutivelyNowButton);
            this.panel5.Controls.Add(this.startConsecutivelySelectDateButton);
            this.panel5.Location = new System.Drawing.Point(5, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(200, 27);
            this.panel5.TabIndex = 44;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.startOnceButton);
            this.panel6.Controls.Add(this.startPeriodicallyButton);
            this.panel6.Controls.Add(this.startConsecutivelyButton);
            this.panel6.Location = new System.Drawing.Point(24, 94);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(134, 159);
            this.panel6.TabIndex = 45;
            // 
            // runsLongerThanPanel
            // 
            this.runsLongerThanPanel.Controls.Add(this.RunsLongerThanWeek);
            this.runsLongerThanPanel.Controls.Add(this.RunsLongerThanDay);
            this.runsLongerThanPanel.Controls.Add(this.RunsLongerThanHour);
            this.runsLongerThanPanel.Controls.Add(this.RunsLongerThanMin);
            this.runsLongerThanPanel.Location = new System.Drawing.Point(292, 289);
            this.runsLongerThanPanel.Name = "runsLongerThanPanel";
            this.runsLongerThanPanel.Size = new System.Drawing.Size(225, 30);
            this.runsLongerThanPanel.TabIndex = 46;
            // 
            // startOncePanel
            // 
            this.startOncePanel.Controls.Add(this.panel1);
            this.startOncePanel.Controls.Add(this.startOnceDateTimePicker);
            this.startOncePanel.Location = new System.Drawing.Point(165, 80);
            this.startOncePanel.Name = "startOncePanel";
            this.startOncePanel.Size = new System.Drawing.Size(439, 37);
            this.startOncePanel.TabIndex = 47;
            // 
            // startPeriodicallyPanel
            // 
            this.startPeriodicallyPanel.Controls.Add(this.panel2);
            this.startPeriodicallyPanel.Controls.Add(this.startPeriodicallyDateTimePicker);
            this.startPeriodicallyPanel.Controls.Add(this.startPeriodicallyIntervalPanel);
            this.startPeriodicallyPanel.Controls.Add(this.startPeriodicallyTimeText);
            this.startPeriodicallyPanel.Controls.Add(this.startPeriodicallyEveryText);
            this.startPeriodicallyPanel.Controls.Add(this.startPeriodicallyEvery);
            this.startPeriodicallyPanel.Location = new System.Drawing.Point(165, 120);
            this.startPeriodicallyPanel.Name = "startPeriodicallyPanel";
            this.startPeriodicallyPanel.Size = new System.Drawing.Size(495, 77);
            this.startPeriodicallyPanel.TabIndex = 48;
            // 
            // startConsecutivelyPanel
            // 
            this.startConsecutivelyPanel.Controls.Add(this.panel5);
            this.startConsecutivelyPanel.Controls.Add(this.startConsecutivelyDateTimePicker);
            this.startConsecutivelyPanel.Controls.Add(this.startConsecutivelyIntervalPanel);
            this.startConsecutivelyPanel.Controls.Add(this.startConsecutivelyDelayText);
            this.startConsecutivelyPanel.Controls.Add(this.startConsecutivelyDelay);
            this.startConsecutivelyPanel.Location = new System.Drawing.Point(165, 201);
            this.startConsecutivelyPanel.Name = "startConsecutivelyPanel";
            this.startConsecutivelyPanel.Size = new System.Drawing.Size(495, 70);
            this.startConsecutivelyPanel.TabIndex = 49;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 442);
            this.Controls.Add(this.TabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.TabControl1.ResumeLayout(false);
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.startPeriodicallyIntervalPanel.ResumeLayout(false);
            this.startPeriodicallyIntervalPanel.PerformLayout();
            this.startConsecutivelyIntervalPanel.ResumeLayout(false);
            this.startConsecutivelyIntervalPanel.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.runsLongerThanPanel.ResumeLayout(false);
            this.runsLongerThanPanel.PerformLayout();
            this.startOncePanel.ResumeLayout(false);
            this.startPeriodicallyPanel.ResumeLayout(false);
            this.startPeriodicallyPanel.PerformLayout();
            this.startConsecutivelyPanel.ResumeLayout(false);
            this.startConsecutivelyPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RadioButton startOnceNowButton;
        private System.Windows.Forms.DateTimePicker startOnceDateTimePicker;
        private System.Windows.Forms.RadioButton startOnceSelectDateButton;
        private System.Windows.Forms.RadioButton startConsecutivelyButton;
        private System.Windows.Forms.RadioButton startPeriodicallyButton;
        private System.Windows.Forms.RadioButton startOnceButton;
        private System.Windows.Forms.TextBox taskExecutablePath;
        private System.Windows.Forms.TextBox taskName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox startPeriodicallyEvery;
        private System.Windows.Forms.Label startPeriodicallyEveryText;
        private System.Windows.Forms.Label startPeriodicallyTimeText;
        private System.Windows.Forms.RadioButton startPeriodicallyNowButton;
        private System.Windows.Forms.DateTimePicker startPeriodicallyDateTimePicker;
        private System.Windows.Forms.RadioButton startPeriodicallySelectDateButton;
        private System.Windows.Forms.RadioButton startPeriodicallyMin;
        private System.Windows.Forms.RadioButton startPeriodicallyHour;
        private System.Windows.Forms.RadioButton startPeriodicallyDay;
        private System.Windows.Forms.RadioButton startPeriodicallyWeek;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton startConsecutivelyMin;
        private System.Windows.Forms.RadioButton startConsecutivelyHour;
        private System.Windows.Forms.RadioButton startConsecutivelyDay;
        private System.Windows.Forms.RadioButton startConsecutivelyWeek;
        private System.Windows.Forms.ComboBox startConsecutivelyDelay;
        private System.Windows.Forms.Label startConsecutivelyDelayText;
        private System.Windows.Forms.RadioButton startConsecutivelyNowButton;
        private System.Windows.Forms.DateTimePicker startConsecutivelyDateTimePicker;
        private System.Windows.Forms.RadioButton startConsecutivelySelectDateButton;
        private System.Windows.Forms.RadioButton runsLongerThanMin;
        private System.Windows.Forms.RadioButton runsLongerThanHour;
        private System.Windows.Forms.RadioButton runsLongerThanDay;
        private System.Windows.Forms.RadioButton runsLongerThanWeek;
        private System.Windows.Forms.ComboBox runsLongerThanEvery;
        private System.Windows.Forms.TextBox emailAddressTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton notifyButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel startConsecutivelyIntervalPanel;
        private System.Windows.Forms.Panel startPeriodicallyIntervalPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel runsLongerThanPanel;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel startOncePanel;
        private System.Windows.Forms.Panel startPeriodicallyPanel;
        private System.Windows.Forms.Panel startConsecutivelyPanel;

        public TabControl TabControl1 { get => tabControl1; set => tabControl1 = value; }
        public TabPage TabPage2 { get => tabPage2; set => tabPage2 = value; }
        public TabPage TabPage3 { get => tabPage3; set => tabPage3 = value; }
        public TabPage TabPage1 { get => tabPage1; set => tabPage1 = value; }
        public RadioButton NotifyButton { get => notifyButton; set => notifyButton = value; }
        public ComboBox RunsLongerThanEvery { get => runsLongerThanEvery; set => runsLongerThanEvery = value; }
        public TextBox EmailAddressTextBox { get => emailAddressTextBox; set => emailAddressTextBox = value; }
        public RadioButton RunsLongerThanWeek { get => runsLongerThanWeek; set => runsLongerThanWeek = value; }
        public RadioButton RunsLongerThanDay { get => runsLongerThanDay; set => runsLongerThanDay = value; }
        public RadioButton RunsLongerThanMin { get => runsLongerThanMin; set => runsLongerThanMin = value; }
        public RadioButton RunsLongerThanHour { get => runsLongerThanHour; set => runsLongerThanHour = value; }
    }
}

