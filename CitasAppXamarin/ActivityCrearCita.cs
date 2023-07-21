using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using CitasAppXamarin.com.somee.citasapp;

namespace CitasAppXamarin
{
    [Activity(Label = "ActivityCrearCita")]
    public class ActivityCrearCita : Activity
    {
        List<Tipos_Servicio> tipoServicios;
        List<Servicios> Servicios;
        int idTipoServicio, idServicio, idDetalleHorario;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.creacion_cita);

            idDetalleHorario = Intent.GetIntExtra("idDetalleHorario", 0);

            // ---------- Tipos servicios ----------
            Spinner spinnerTipos = FindViewById<Spinner>(Resource.Id.spinner_TipoServicios);

            tipoServicios = Global.Get_Tipo_Servivios();
            Tipos_Servicio defaultItem = new Tipos_Servicio() { Id = 0, descripcion = "Seleccione un tipo de servicio" };
            
            List<string> tipos = new List<string>();
            tipoServicios.Insert(0, defaultItem);
            foreach (var item in tipoServicios)
                tipos.Add(item.descripcion);

            spinnerTipos.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapterTipos = new ArrayAdapter<string>(this, Resource.Layout.spinnerItem, tipos);

            adapterTipos.SetDropDownViewResource(Resource.Layout.spinnerItem);
            spinnerTipos.Adapter = adapterTipos;

            //---------- Guardar cita ----------
            FindViewById<Button>(Resource.Id.button_Guardar).Click += ActivityCrearCita_Click;
        }

        private void ActivityCrearCita_Click(object sender, EventArgs e)
        {
            string descr = FindViewById<EditText>(Resource.Id.editText_Descripcion).Text;
            string numDep = FindViewById<EditText>(Resource.Id.editText_Deposito).Text;

            if(Global.Agendar_Cita(Global.User_Id, idDetalleHorario, idServicio, descr, numDep))
            {
                Global.Marcar_Ocupado(idDetalleHorario);
                Intent i = new Intent(this, typeof(ActivityDashboard));
                Toast.MakeText(this, "Tu cita se ha registrado con exito", ToastLength.Long).Show();
                StartActivity(i);
            }
            else
            {
                Toast.MakeText(this, "Algo ha salido mal", ToastLength.Long).Show();
            }
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            idTipoServicio = tipoServicios[e.Position].Id;

            // ---------- Servicios ----------
            Spinner spinnerServs = FindViewById<Spinner>(Resource.Id.spinner_Servicios);

            Servicios = Global.Get_Servivios(idTipoServicio);
            Servicios defltItem = new Servicios() { Id = 0, nombre = "Seleccione un tipo de servicio", TipoServicio_Id = 0 };
            if (idTipoServicio > 0)
            
            {
                defltItem = new Servicios() { Id = 0, nombre = "Seleccione un servicio", TipoServicio_Id = 0 };
            }
                

            List<string> servs = new List<string>();
            Servicios.Insert(0, defltItem);
            foreach (var item in Servicios)
                servs.Add(item.nombre);

            spinnerServs.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected2);
            var adapterServs = new ArrayAdapter<string>(this, Resource.Layout.spinnerItem, servs);

            adapterServs.SetDropDownViewResource(Resource.Layout.spinnerItem);
            spinnerServs.Adapter = adapterServs;
        }

        private void spinner_ItemSelected2(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            idServicio = Servicios[e.Position].Id;
        }

    }
}