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
using System.Globalization;

namespace CitasAppXamarin
{
    [Activity(Label = "ActivityMisCitas")]
    public class ActivityMisCitas : Activity
    {
        ListView lv_Citas;
        List<CitaInfo> MisCitas;
        int tmpIdHorarioFecha = 0, tmpIdCita = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.mis_citas);

            lv_Citas = FindViewById<ListView>(Resource.Id.listView1);

            MisCitas = Global.Citas_X_Cliente(Global.User_Id);

            lv_Citas.Adapter = new AdapterMisCitas(this, MisCitas);
            lv_Citas.ItemClick += Lv_Citas_ItemClick;
        }

        private void Lv_Citas_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var alerta = new AlertDialog.Builder(this);
            alerta.SetTitle("Cancelar cita");
            alerta.SetMessage($"¿ Deseas cancelar tu cita para {MisCitas[e.Position].servicio} " +
                $"el dia {new DateTime(MisCitas[e.Position].año, MisCitas[e.Position].mes, MisCitas[e.Position].dia).ToString("M", new CultureInfo("es-ES"))} " +
                $"con horario de {MisCitas[e.Position].hora} ?");
            alerta.SetPositiveButton("Si", CancelarCita);
            alerta.SetNegativeButton("No", xd);

            tmpIdHorarioFecha = MisCitas[e.Position].detalleFechaBloque_Id;
            tmpIdCita = MisCitas[e.Position].Id;
            alerta.Show();
        }

        private void CancelarCita(object sender, DialogClickEventArgs e)
        {
            if(Global.Marcar_Disponible(tmpIdHorarioFecha) && Global.CancelarCita(tmpIdCita))
            {
                Toast.MakeText(this, "Cita cancelada", ToastLength.Long).Show();
                tmpIdHorarioFecha = 0;
                
                if(Global.Citas_X_Cliente(Global.User_Id).Count > 0)
                {
                    Intent intent = new Intent(this, typeof(ActivityMisCitas));
                    StartActivityForResult(intent, 0);
                }
                else
                {
                    Intent intent = new Intent(this, typeof(ActivityDashboard));
                    StartActivityForResult(intent, 0);
                }
            }
            else
            {
                tmpIdHorarioFecha = 0;
                Toast.MakeText(this, "Algo salió mal", ToastLength.Long).Show();
            }
        }

        private void xd(object sender, DialogClickEventArgs e)
        {
            // DO NOTHING XD
        }

    }
}