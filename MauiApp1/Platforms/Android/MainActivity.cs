using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using System.Text;
using System.Text.Json;

namespace MauiApp1
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.RequestedOrientation = ScreenOrientation.Portrait;
            const int requestNotification = 0;
            string[] notiPermission =
            {
                Manifest.Permission.PostNotifications
            };

            if ((int)Build.VERSION.SdkInt < 33) return;
            if (CheckSelfPermission(Manifest.Permission.PostNotifications) != Permission.Granted)
            {
                RequestPermissions(notiPermission, requestNotification);
            }
        }
        protected override void OnStop()
        {
            base.OnStop();
            SaveDataInFileJson();
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            SaveDataInFileJson();
        }
        public async void SaveDataInFileJson()
        {
            var appStorage = FileSystem.Current.AppDataDirectory;
            var filePath = Path.Combine(appStorage, MainPage.JsonFileName);
            Dictionary<string, string> dict = new Dictionary<string, string>()
            {
                {"LastIdNotification", JsonSerializer.Serialize(MainPage.LastIdNotification) },
                {"Tasks",JsonSerializer.Serialize(MainPage.Tasks) },
                {"Notes", JsonSerializer.Serialize(MainPage.Notes) },
                {"Groups", JsonSerializer.Serialize(MainPage.Groups) },
                {"Reminders", JsonSerializer.Serialize(MainPage.Reminders) }
            };
            foreach (var data in dict)
            {
                await File.WriteAllTextAsync(filePath + data.Key, data.Value);
            }
        }
    }
}
