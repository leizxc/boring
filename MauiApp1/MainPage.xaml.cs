using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification.Core.Models.AndroidOption;

using Plugin.LocalNotification.Core.Models;


#if ANDROID
using Android.Content;
using Android.Provider;
#endif

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSaveReminderClicked(object sender, EventArgs e)
        {
            string title = ReminderTitle.Text;

            // Ensure nullable Date/Time values are handled
            DateTime date = ReminderDate.Date ?? DateTime.Today;
            TimeSpan time = ReminderTime.Time ?? TimeSpan.Zero;
            DateTime reminderDateTime = date + time;

            ReminderOutput.Text = $"Reminder: {title}\nAt: {reminderDateTime}";

            var request = new NotificationRequest
            {
                NotificationId = new Random().Next(1000, 9999),
                Title = "Reminder",
                Description = title,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = reminderDateTime
                },
                Android = new AndroidOptions
                {
                    ChannelId = "reminder_channel",
                    VibrationPattern = new long[] { 0, 500 }
                }
            };

            LocalNotificationCenter.Current.Show(request);
        }
    }
}