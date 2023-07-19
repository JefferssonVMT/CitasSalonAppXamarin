using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;

namespace CitasAppXamarin
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class ActivityLogin : AppCompatActivity
    {
        string Email, Contra;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.login);

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
                Toast.MakeText(this, "Se inicio sesion correctamente", ToastLength.Long).Show();
                Global.User_Id = Global.Servicio.Get_User_Id(Email);

                // Reseteo los campos por seguridad
                // FindViewById<EditText>(Resource.Id.editText1).Text = null;
                FindViewById<EditText>(Resource.Id.editText2).Text = null;

                // Intent intent = new Intent(this, typeof(ActivityPrincipal));
                //StartActivityForResult(intent, 0);
                Toast.MakeText(this, "Iniciaste sesion exitosamente", ToastLength.Long).Show();

                Intent intent = new Intent(this, typeof(ActivityCalendario));

                StartActivityForResult(intent, 0);
            }
            else
            {
                Toast.MakeText(this, "Usuario inexistente o datos erroneos", ToastLength.Long).Show();
            }
        }
    }
}