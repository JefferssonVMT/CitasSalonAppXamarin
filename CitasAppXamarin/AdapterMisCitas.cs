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
    class AdapterMisCitas : BaseAdapter
    {
        Activity context;
        List<CitaInfo> Lista;

        public AdapterMisCitas(Activity context, List<CitaInfo> Lista)
        {
            this.context = context;
            this.Lista = Lista;
        }

        public override int Count => Lista.Count;

        public override Java.Lang.Object GetItem(int position)
        {
            throw new NotImplementedException();
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //Definimos el formato en que se van a presentar cada uno de los elementos de la lista
            var item = Lista[position];

            View view = convertView;

            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.citasItem, null);
            }

            string fecha = new DateTime(item.año, item.mes, item.dia).ToString("M", new CultureInfo("es-ES"));
            view.FindViewById<TextView>(Resource.Id.textView_servicio_fechayHora).Text = item.servicio + "\n" + fecha + "\n" + item.hora;

            return view;
        }
    }
}