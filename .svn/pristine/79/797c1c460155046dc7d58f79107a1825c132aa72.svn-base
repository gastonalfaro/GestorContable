using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarCatalogosGenerales : clsProcedimientoAlmacenado
    {
        private Nullable<int> lint_IdCatalogo;
        public Nullable<int> Lint_IdCatalogo
        {
            get { return lint_IdCatalogo; }
            set { lint_IdCatalogo = value; }
        }

        private string lstr_AbrevCatalogo;
        public string Lstr_AbrevCatalogo
        {
            get { return lstr_AbrevCatalogo; }
            set { lstr_AbrevCatalogo = value; }
        }

        private string lstr_NomCatalogo;
        public string Lstr_NomCatalogo
        {
            get { return lstr_NomCatalogo; }
            set { lstr_NomCatalogo = value; }
        }

        private string lstr_IdModulo;
        public string Lstr_IdModulo
        {
            get { return lstr_IdModulo; }
            set { lstr_IdModulo = value; }
        }

        public clsConsultarCatalogosGenerales(Nullable<int> int_IdCatalogo, string str_AbrevCatalogo, string str_NomCatalogo, string str_IdModulo)
        {
            lint_IdCatalogo = int_IdCatalogo;
            lstr_AbrevCatalogo = str_AbrevCatalogo;
            lstr_NomCatalogo = str_NomCatalogo;
            lstr_IdModulo = str_IdModulo;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarCatalogosGenerales.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}