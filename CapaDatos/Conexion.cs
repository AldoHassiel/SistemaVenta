using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace CapaDatos
{
    public class Conexion
    {
        private static string _conexion;
        public static string cadena { 
            get {
                if (HayInternet())
                {
                    _conexion = ConfigurationManager.ConnectionStrings["conexion_remota"].ToString();
                }
                else
                {
                    _conexion = ConfigurationManager.ConnectionStrings["conexion_local"].ToString();
                }
                return _conexion;
            }
        }
        private static bool HayInternet()
        {
            try
            {
                using (var cliente = new System.Net.WebClient())
                using (cliente.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}