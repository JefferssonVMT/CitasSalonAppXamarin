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
    [Activity(Label = "ActivityRegistro")]
    public class ActivityRegistro : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.registro);

            FindViewById<TextView>(Resource.Id.textView_Login).Click += ActivityRegistro_Click;
        }

        private void ActivityRegistro_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ActivityLogin));

            StartActivityForResult(intent, 0);
        }
    }
}