using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace LogicaNegocio
{
    public class Conexion
    {
                                           
        //static String cadenaConexion = @"Data Source=MH-BDQ-NICSP1\QMSS2017E01;Initial Catalog=GestNICSP; Integrated Security=SSPI;";

        static String cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
        public static string CadenaConexion
        {
            get { return cadenaConexion; }
        }
    }
}