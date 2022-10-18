using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsConsultarPermisosUsuarios : clsProcedimientoAlmacenado
    {
        private string lstr_IdObjeto;
        public string Lstr_IdObjeto
        {
            get { return lstr_IdObjeto; }
            set { lstr_IdObjeto = value; }
        }

        private string lstr_IdUsuario;
        public string Lstr_IdUsuario
        {
            get { return lstr_IdUsuario; }
            set { lstr_IdUsuario = value; }
        }

        public clsConsultarPermisosUsuarios(string str_IdObjeto, string str_IdUsuario)
        {
            lstr_IdObjeto = str_IdObjeto;
            lstr_IdUsuario = str_IdUsuario;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\ConsultarPermisosUsuarios.config", this);
            //try
            //{
            //    var appSettings = ConfigurationManager.AppSettings;
            //    string str_DireccionConfigs = appSettings["DireccionConfigs"];

            //    EjecucionSP(str_DireccionConfigs + "\\ConsultarModulos.config", this);
            //}
            //catch (Exception ex)
            //{
            //    this.Lstr_MensajeRespuesta = ex.ToString();
            //}
        }
    }
}