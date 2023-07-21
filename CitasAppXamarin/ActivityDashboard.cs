using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitasAppXamarin
{
    [Activity(Label = "ActivityDashboard")]
    public class ActivityDashboard : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.dashboard);

            FindViewById<Button>(Resource.Id.button_nueva_cita).Click += ActivityDashboard_Click;
            FindViewById<Button>(Resource.Id.button_mis_citas).Click += ActivityDashboard_Click1;
        }

        private void ActivityDashboard_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ActivityCalendario));

            StartActivityForResult(intent, 0);
        }

        private void ActivityDashboard_Click1(object sender, EventArgs e)
        {
            if (Global.Citas_X_Cliente(Global.User_Id).Count == 0)
            {
                var alerta = new AlertDialog.Builder(this);
                alerta.SetTitle("Ups parece que no tienes citas!");
                alerta.SetMessage("\nAun no has agendado ninguna cita, intenta crear una nueva.");
                alerta.Show();
            }
            else
            {
                Intent intent = new Intent(this, typeof(ActivityMisCitas));

                StartActivityForResult(intent, 0);
            }
        }
    }
}