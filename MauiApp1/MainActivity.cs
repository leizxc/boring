using Android.App;
using Android.OS;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification.Core.Models.AndroidOption;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Create channel with vibration enabled
        LocalNotificationCenter.CreateNotificationChannels(new AndroidNotificationChannelRequest[]
        {
            new AndroidNotificationChannelRequest
            {
                Id = "reminder_channel",
                Name = "Reminders",
                Description = "Reminder notifications",
                Importance = AndroidImportance.High,
                EnableVibration = true,
                VibrationPattern = new long[] { 0, 500, 100, 500 } // wait, vibrate, wait, vibrate
            }
        });

        // Request runtime notification permission on Android 13+
        if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu)
        {
            if (CheckSelfPermission(Android.Manifest.Permission.PostNotifications) != Android.Content.PM.Permission.Granted)
            {
                RequestPermissions(new[] { Android.Manifest.Permission.PostNotifications }, 1001);
            }
        }
    }
}