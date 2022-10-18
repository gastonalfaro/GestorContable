using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsConsultarPoliticas : clsProcedimientoAlmacenado
    {
        private string lint_IdPolitica;
        public string Lint_IdPolitica
        {
            get { return lint_IdPolitica; }
            set { lint_IdPolitica = value; }
        }

        private string ldat_FchVigencia;
        public string Ldat_FchVigencia
        {
            get { return ldat_FchVigencia; }
            set { ldat_FchVigencia = value; }
        }

        public clsConsultarPoliticas(string int_IdPolitica, string dat_FchaVigencia)
        {
            lint_IdPolitica = int_IdPolitica;
            ldat_FchVigencia = dat_FchaVigencia;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\ConsultarPoliticas.config", this);
        }
    }
}