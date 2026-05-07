using System;
using Android.App;
using Android.OS;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification.Core.Models.AndroidOption;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Create channel with vibration enabled
        LocalNotificationCenter.CreateNotificationChannels(new List<AndroidNotificationChannelRequest>
        {
            new AndroidNotificationChannelRequest
            {
                Id = "reminder_channel",
                Name = "Reminders",
                Description = "Reminder notifications",
                Importance = AndroidImportance.High,
                EnableVibration = true,
                VibrationPattern = new long[] { 0, 500, 100, 500 }
            }
        });

        // Request runtime notification permission on Android 13+
        if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu)
        {
            const string postNotificationPermission = "android.permission.POST_NOTIFICATIONS";

            // Guard CheckSelfPermission with a platform-aware check so analyzers know the call is safe.
            if (OperatingSystem.IsAndroidVersionAtLeast(23) &&
                CheckSelfPermission(postNotificationPermission) != Android.Content.PM.Permission.Granted)
            {
                RequestPermissions(new[] { postNotificationPermission }, 1001);
            }
        }
    }
}