using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarEmpresas : clsProcedimientoAlmacenado
    {
        private string lstr_IdPersonaJuridica;
        public string Lstr_IdPersonaJuridica
        {
            get { return lstr_IdPersonaJuridica; }
            set { lstr_IdPersonaJuridica = value; }
        }

        private string lstr_Nombre;
        public string Lstr_Nombre
        {
            get { return lstr_Nombre; }
            set { lstr_Nombre = value; }
        }
        
        private string lstr_TipoIdPersonaAutoriza;
        public string Lstr_TipoIdPersonaAutoriza
        {
            get { return lstr_TipoIdPersonaAutoriza; }
            set { lstr_TipoIdPersonaAutoriza = value; }
        }

        private string lstr_IdPersonaAutoriza;
        public string Lstr_IdPersonaAutoriza
        {
            get { return lstr_IdPersonaAutoriza; }
            set { lstr_IdPersonaAutoriza = value; }
        }


        public clsConsultarEmpresas(string str_IdPersonaJuridica, string str_Nombre, string str_IdPersonaAutorizada, string str_TipoIdPersonaAutoriza, string str_IdPersonaAutoriza)
        {
            lstr_IdPersonaJuridica = str_IdPersonaJuridica;
            lstr_Nombre = str_Nombre;
            lstr_TipoIdPersonaAutoriza = str_TipoIdPersonaAutoriza;
            lstr_IdPersonaAutoriza = str_IdPersonaAutoriza;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarEmpresas.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}