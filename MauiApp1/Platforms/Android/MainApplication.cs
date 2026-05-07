using Android.App;
using Android.Runtime;
using Android.Content;
using Android.OS;
using Plugin.LocalNotification;
using System.Runtime.Versioning;

namespace MauiApp1
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        [SupportedOSPlatform("android26.0")]
        public override void OnCreate()
        {
            base.OnCreate();

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channel = new NotificationChannel(
                    "reminder_channel",
                    "Reminders",
                    NotificationImportance.High)
                {
                    Description = "Reminder notifications"
                };

                channel.EnableVibration(true);
                channel.SetVibrationPattern(new long[] { 0, 500, 250, 500 });

                var manager = NotificationManager.FromContext(this);
                manager?.CreateNotificationChannel(channel);
            }
            // ✅ Sa mas lumang Android, automatic fallback sa default notification system
        }
    }
}