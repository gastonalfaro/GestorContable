using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarEmpresasAutorizados : clsProcedimientoAlmacenado
    {
        private string lstr_IdPersonaJuridica;
        public string Lstr_IdPersonaJuridica
        {
            get { return lstr_IdPersonaJuridica; }
            set { lstr_IdPersonaJuridica = value; }
        }

        private string lstr_TipoIdPersonaAutorizada;
        public string Lstr_TipoIdPersonaAutorizada
        {
            get { return lstr_TipoIdPersonaAutorizada; }
            set { lstr_TipoIdPersonaAutorizada = value; }
        }

        private string lstr_IdPersonaAutorizada;
        public string Lstr_IdPersonaAutorizada
        {
            get { return lstr_IdPersonaAutorizada; }
            set { lstr_IdPersonaAutorizada = value; }
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

        private string lstr_CtaCliente;
        public string Lstr_CtaCliente
        {
            get { return lstr_CtaCliente; }
            set { lstr_CtaCliente = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public clsConsultarEmpresasAutorizados(string str_IdPersonaJuridica, string str_TipoIdPersonaAutorizada, string str_IdPersonaAutorizada, string str_TipoIdPersonaAutoriza, string str_IdPersonaAutoriza, string str_CtaCliente, string str_Estado)
        {
            lstr_IdPersonaJuridica = str_IdPersonaJuridica;
            lstr_TipoIdPersonaAutorizada = str_TipoIdPersonaAutorizada;
            lstr_IdPersonaAutorizada = str_IdPersonaAutorizada;
            lstr_TipoIdPersonaAutoriza = str_TipoIdPersonaAutoriza;
            lstr_IdPersonaAutoriza = str_IdPersonaAutoriza;
            lstr_CtaCliente = str_CtaCliente;
            lstr_Estado = str_Estado;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarEmpresasAutorizados.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}