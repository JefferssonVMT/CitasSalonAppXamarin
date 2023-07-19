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
    class Global
    {
        //Instanciar el servicio web
        public static CitasWebService Servicio = new CitasWebService();
        public static int User_Id;

        public static bool IniciarSesion(string Email, string Pass)
        {
            return Servicio.Login(Email, Pass);
        }

        public static bool Registro(string nombre, string apellido, string correo, string telefono, short edad, string contra)
        {
            return Servicio.RegistrarCliente(nombre, apellido, correo, telefono, edad, contra);
        }

        public static int Get_UserID(string email)
        {
            return Servicio.Get_User_Id(email);
        }

        public static List<DetalleDia> GetHorarios_Dia(int mes, int dia)
        {
            return Servicio.Get_Horas_Disponibles_Fecha(mes, dia).ToList();
        }

        public static List<Tipos_Servicio> Get_Tipo_Servivios()
        {
            return Servicio.Get_Tipos_Servicio().ToList();
        }

        public static List<Servicios> Get_Servivios(int idTipoServicio)
        {
            return Servicio.Get_Servicios(idTipoServicio).ToList();
        }

        public static bool Agendar_Cita(int IdCliente, int DetalleFechaBloque_Id, int Servicio_Id, string descripcion, string numero_deposito)
        {
            return Servicio.AgendarCita(descripcion, numero_deposito, Servicio_Id, IdCliente, DetalleFechaBloque_Id);
        }

        public static bool Marcar_Ocupado(int DetalleFechaBloque_Id)
        {
            return Servicio.Marcar_Ocupado(DetalleFechaBloque_Id);
        }
    }
}