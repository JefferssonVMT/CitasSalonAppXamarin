using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CitasAppXamarin.com.somee.citasapp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitasAppXamarin
{
    [Activity(Label = "ActivityCalendario")]
    public class ActivityCalendario : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.calendario);

            var calendar = FindViewById<CalendarView>(Resource.Id.calendarView1);
            calendar.DateChange += CalendarOnDateChange;
            calendar.SetOutlineSpotShadowColor(Android.Graphics.Color.Red);
        }

        private void CalendarOnDateChange(object sender, CalendarView.DateChangeEventArgs args)
        {
            // Month + 1 porque por alguna razon empieza a contar los meses(solo los meses) desde 0
            var fecha = new DateTime(args.Year, args.Month + 1, args.DayOfMonth);

            if(Global.GetHorarios_Dia(fecha.Month, fecha.Day).Count > 0)
            {
                Intent intent = new Intent(this, typeof(ActivityHorarios));

                intent.PutExtra("mes", fecha.Month);
                intent.PutExtra("dia", fecha.Day);

                StartActivityForResult(intent, 0);
            }
            else
            {
                var alerta = new AlertDialog.Builder(this);
                alerta.SetTitle("Ups sin disponibilidad!");
                alerta.SetMessage("\nAl parecer no tenemos ninguna hora libre en esta fecha, prueba agendar para otro dia o ponte en contacto con nosotros.");
                alerta.Show();
            }

            
        }

    }
}