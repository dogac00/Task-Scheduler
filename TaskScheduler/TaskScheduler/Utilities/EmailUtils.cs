using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FRUtility;
using NLog;

namespace TaskScheduler
{
    static class EmailUtils
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static EmailInfo SetEmailInfo()
        {
            if (MainForm.Form.NotifyButton.Checked)
            {
                float every = (float) MainForm.Form.RunsLongerThanEvery.Value;
                var interval = GetSelectedRunsLongerThanInterval();

                EmailInfo emailInfo = new EmailInfo
                {
                    IsToBeNotified = true,
                    EmailAddresses = GetTextBoxEmailsList(),
                    NoLongerThan = TimeSpanUtils.GenerateTimeSpan(every, interval)
                };

                return emailInfo;
            }
            else
            {
                return null;
            }
        }

        public static List<string> GetTextBoxEmailsList()
        {
            return MainForm.Form.EmailAddressTextBox.Text.Split(',').ToList();
        }

        public static string [] GetTextBoxEmailsArray()
        {
            return MainForm.Form.EmailAddressTextBox.Text.Split(',');
        }

        public static string ConvertEmailsToString(Task task)
        {
            var emails = task.EmailInfo.EmailAddresses;
            string emailsString = "";

            foreach (var email in emails)
            {
                emailsString += email + ",";
            }

            return emailsString.Remove(emailsString.Length - 1);
        }

        public static string GetTaskEmails(Task task)
        {
            if (task.EmailInfo == null)
            {
                return "No email.";
            }
            else
            {
                return ConvertEmailsToString(task);
            }
        }

        private static bool SendEmail(string [] emails, string body)
        {
            EmailService emailService = new EmailService();

            try
            {
                if (emailService.SendEmail(emails, body))
                {
                    logger.Info($"Email sent successfully to : {emails}");

                    return true;
                }
                else
                {
                    logger.Error($"Email could not be sent to : {emails}");

                    return false;
                }

            }
            catch (Exception e)
            {
                logger.Error(e.Message);

                return false;
            }
        }

        public static bool RedirectToSendEmail(string taskName, TimeSpan dontRunLongerThan)
        {
            string [] emails = GetTextBoxEmailsArray();

            string mailBody = $"Task named \"{ taskName }\" run longer than you expected.\n" +
                $"Task is running longer than : { dontRunLongerThan.ToString() }";

            return SendEmail(emails, mailBody);
        }

        private static Interval GetSelectedRunsLongerThanInterval()
        {
            if (MainForm.Form.RunsLongerThanWeek.Checked)
                return Interval.Week;
            else if (MainForm.Form.RunsLongerThanDay.Checked)
                return Interval.Day;
            else if (MainForm.Form.RunsLongerThanHour.Checked)
                return Interval.Hour;
            else
                return Interval.Min;
        }
    }
}
