using Android.App;
using Android.Content.PM;
using Android.OS;
using hekoTestSR;

namespace HekoTestSR.Droid
{
    [Activity(Label = "hekoTest-SR.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }
    }
}
