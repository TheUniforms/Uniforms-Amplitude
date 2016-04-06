using Android.App;
using Android.Widget;
using Android.OS;

namespace AmplTest.Droid
{
    using Amplitude = Uniforms.Amplitude.Amplitude;

    [Activity(Label = "AmplTest", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            InitAmplitude();

            InitLayout();
        }

        void InitAmplitude()
        {
            Uniforms.Amplitude.Droid.Amplitude.Register(this);

            Amplitude.Instance.Initialize(Config.ApiKey);
        }

        void InitLayout()
        {
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate
                {
                    button.Text = string.Format("{0} clicks!", count++);
                    Amplitude.Instance.LogEvent("Test");
                };            
        }
    }
}
