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
        string nombre, apellido, correo, contra, reContra, telefono;
        short edad = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.registro);

            FindViewById<TextView>(Resource.Id.textView_Login).Click += ActivityRegistro_Click;

            FindViewById<Button>(Resource.Id.button_registro).Click += ActivityRegistro_Click1;

        }

        private void ActivityRegistro_Click1(object sender, EventArgs e)
        {
            nombre = FindViewById<EditText>(Resource.Id.editText_nombre).Text;
            apellido = FindViewById<EditText>(Resource.Id.editText_apellido).Text;
            correo = FindViewById<EditText>(Resource.Id.editText_email).Text;
            contra = FindViewById<EditText>(Resource.Id.editText_pass).Text;
            reContra = FindViewById<EditText>(Resource.Id.editText_repass).Text;
            telefono = FindViewById<EditText>(Resource.Id.editText_telefono).Text;
            try
            {
                edad = short.Parse(FindViewById<EditText>(Resource.Id.editText_edad).Text);
            }
            catch(Exception)
            {
                FindViewById<EditText>(Resource.Id.editText_edad).Error = "Campo invalido";
            }
            
            if(ValidarDatos())
            {
                Global.Registro(nombre, apellido, correo, telefono, edad, contra, reContra);
                Toast.MakeText(this, "Registro exitoso, inicia sesion para continuar", ToastLength.Long).Show();
                Intent intent = new Intent(this, typeof(ActivityLogin));

                StartActivityForResult(intent, 0);
            }
            else
            {
                Toast.MakeText(this, "Error en los datos introducidos", ToastLength.Long).Show();
            }
        }

        private bool ValidarDatos()
        {
            bool valido = true;

            if (nombre == null || nombre.Length < 3)
            {
                FindViewById<EditText>(Resource.Id.editText_nombre).Error = "Campo invalido";
                valido = false;
            }

            if (apellido == null || apellido.Length < 3)
            {
                FindViewById<EditText>(Resource.Id.editText_apellido).Error = "Campo invalido";
                valido = false;
            }

            if (correo == null || correo.Length < 3)
            {
                FindViewById<EditText>(Resource.Id.editText_email).Error = "Campo invalido";
                valido = false;
            }

            if (edad < 1 || edad > 100)
            {
                FindViewById<EditText>(Resource.Id.editText_edad).Error = "Campo invalido";
                valido = false;
            }

            if (telefono == null || telefono.Length < 3)
            {
                FindViewById<EditText>(Resource.Id.editText_telefono).Error = "Campo invalido";
                valido = false;
            }

            if (contra == null || contra.Length < 3)
            {
                FindViewById<EditText>(Resource.Id.editText_pass).Error = "Campo invalido";
                valido = false;
            }

            if (reContra == null || reContra.Length < 3)
            {
                FindViewById<EditText>(Resource.Id.editText_repass).Error = "Campo invalido";
                valido = false;
            }

            if(reContra != contra)
            {
                FindViewById<EditText>(Resource.Id.editText_repass).Error = "Los campos de contraseña no son iguales";
                valido = false;
            }

            return valido;
        }

        private void ActivityRegistro_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ActivityLogin));

            StartActivityForResult(intent, 0);
        }
    }
}