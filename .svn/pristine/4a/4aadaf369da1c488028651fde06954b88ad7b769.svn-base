using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsConsultarCodigo : clsProcedimientoAlmacenado
    {
        /// <summary>
        /// Cedula del Usuario
        /// </summary>
        private string lstr_CedUsuario;
        public string Lstr_CedUsuario
        {
            get { return lstr_CedUsuario; }
            set { lstr_CedUsuario = value; }
        }

        public clsConsultarCodigo(string str_Cedula)
        {
            lstr_CedUsuario = str_Cedula;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\ConsultarCodigo.config", this);
        }
    }
}