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
    class AdapterHorarios : BaseAdapter
    {

        Activity context;
        List<DetalleDia> Lista;

        public AdapterHorarios(Activity context, List<DetalleDia> Lista)
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
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            }

            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = item.Bloque;
            view.FindViewById<TextView>(Android.Resource.Id.Text1).TextAlignment = TextAlignment.Center;

            return view;
        }
    }

}