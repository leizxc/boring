#if ANDROID
using Android.App;
using Android.Content;
using Android.Provider;
using System;

namespace MauiApp1
{
    internal class AlarmService
    {
        public static void SetAlarm(int hour, int minute, string message)
        {
            try
            {
                var intent = new Intent(AlarmClock.ActionSetAlarm);

                intent.PutExtra(AlarmClock.ExtraHour, hour);
                intent.PutExtra(AlarmClock.ExtraMinutes, minute);
                intent.PutExtra(AlarmClock.ExtraMessage, message);

                intent.SetFlags(ActivityFlags.NewTask);

                var context = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;

                // SAFE CHECK: kung may app na tatanggap ng intent
                if (context != null)
                {
                    var pm = context.PackageManager;
                    if (pm != null && intent.ResolveActivity(pm) != null)
                    {
                        context.StartActivity(intent);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("No alarm app found on device or PackageManager unavailable.");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No current activity available.");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Alarm Error: {ex.Message}");
            }
        }
    }
}
#endif