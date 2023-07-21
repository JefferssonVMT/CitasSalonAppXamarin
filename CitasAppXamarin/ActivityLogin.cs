using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using Android.Gms.Common;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;

namespace CitasAppXamarin
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class ActivityLogin : AppCompatActivity
    {
        string Email, Contra;

        static readonly string TAG = "MainActivity";

        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;

        [Obsolete]
#pragma warning disable CS0809 // El miembro obsoleto invalida un miembro no obsoleto
        protected override void OnCreate(Bundle savedInstanceState)
#pragma warning restore CS0809 // El miembro obsoleto invalida un miembro no obsoleto
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.login);

            IsPlayServicesAvailable();

            CreateNotificationChannel();
            Log.Debug(TAG, "InstanceID token: " + FirebaseInstanceId.Instance.Token);

            FindViewById<Button>(Resource.Id.button1).Click += ActivityLogin_Click;
            FindViewById<TextView>(Resource.Id.textView_Registro).Click += ActivityLogin_Click1;
        }

        private void ActivityLogin_Click1(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ActivityRegistro));

            StartActivityForResult(intent, 0);
        }

        private void ActivityLogin_Click(object sender, System.EventArgs e)
        {
            Email = FindViewById<EditText>(Resource.Id.editText1).Text;
            Contra = FindViewById<EditText>(Resource.Id.editText2).Text;

            if (Global.IniciarSesion(Email, Contra))
            {
                Global.User_Id = Global.Servicio.Get_User_Id(Email);

                // Reseteo los campos por seguridad
                // FindViewById<EditText>(Resource.Id.editText1).Text = null;
                FindViewById<EditText>(Resource.Id.editText2).Text = null;

                // Intent intent = new Intent(this, typeof(ActivityPrincipal));
                //StartActivityForResult(intent, 0);
                Toast.MakeText(this, "Iniciaste sesion exitosamente", ToastLength.Long).Show();

                Intent intent = new Intent(this, typeof(ActivityDashboard));

                StartActivityForResult(intent, 0);
            }
            else
            {
                Toast.MakeText(this, "Usuario inexistente o datos erroneos", ToastLength.Long).Show();
            }
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                Finish();
                return false;
            }
            else
            {
                return true;
            }
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channel = new NotificationChannel(CHANNEL_ID, "FCM Notifications", NotificationImportance.Default)
            {
                Description = "Firebase Cloud Messages appear in this channel"
            };

            var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }
}