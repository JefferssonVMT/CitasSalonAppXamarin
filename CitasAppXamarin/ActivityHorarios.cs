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
using CitasAppXamarin.com.somee.citasapp;

namespace CitasAppXamarin
{
    [Activity(Label = "ActivityHorarios")]
    public class ActivityHorarios : Activity
    {
        ListView lv_Horarios;
        int dia, mes;
        List<DetalleDia> fecha;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.horarios);

            lv_Horarios = FindViewById<ListView>(Resource.Id.listView1);
            mes = Intent.GetIntExtra("mes", 0);
            dia = Intent.GetIntExtra("dia", 0);
            fecha = Global.GetHorarios_Dia(mes, dia);

            //Toast.MakeText(this, $"Dia: {dia}, Mes: {mes}", ToastLength.Long).Show();
            lv_Horarios.Adapter = new AdapterHorarios(this, fecha);
            lv_Horarios.ItemClick += Lv_Horarios_ItemClick;
        }

        private void Lv_Horarios_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent i = new Intent(this, typeof(ActivityCrearCita));

            DetalleDia det = fecha [e.Position];

            //Pasamos como parametro el id de la region al ActivityDetalleRegion
            i.PutExtra("idDetalleHorario", det.Id);

            StartActivity(i);
        }
    }
}