using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearEmpresa : clsProcedimientoAlmacenado
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

        private string lstr_CorreoEmpresa;
        public string Lstr_CorreoEmpresa
        {
            get { return lstr_CorreoEmpresa; }
            set { lstr_CorreoEmpresa = value; }
        }

        private string lstr_TelefonoEmpresa;
        public string Lstr_TelefonoEmpresa
        {
            get { return lstr_TelefonoEmpresa; }
            set { lstr_TelefonoEmpresa = value; }
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

  
        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public clsCrearEmpresa(string str_IdPersonaJuridica, string str_Nombre, string str_CorreoEmpresa, string str_TelefonoEmpresa, string str_TipoIdPersonaAutoriza, string str_IdPersonaAutoriza, string str_UsrCreacion)
        {
            lstr_IdPersonaJuridica = str_IdPersonaJuridica;
            lstr_Nombre = str_Nombre;
            lstr_CorreoEmpresa = str_CorreoEmpresa;
            lstr_TelefonoEmpresa = str_TelefonoEmpresa;
            lstr_TipoIdPersonaAutoriza = str_TipoIdPersonaAutoriza;
            lstr_IdPersonaAutoriza = str_IdPersonaAutoriza;
            lstr_UsrCreacion = str_UsrCreacion;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearEmpresa.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}