using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarOpcionesCatalogo : clsProcedimientoAlmacenado
    {
        private Int32? lint_IdCatalogo;
        public Int32? Lint_IdCatalogo
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
        
        private Int32? lint_IdOpcion;
        public Int32? Lint_IdOpcion
        {
            get { return lint_IdOpcion; }
            set { lint_IdOpcion = value; }
        }


        private string lstr_NomOpcion;
        public string Lstr_NomOpcion
        {
            get { return lstr_NomOpcion; }
            set { lstr_NomOpcion = value; }
        }


        public clsConsultarOpcionesCatalogo(Int32? int_IdCatalogo, string str_AbrevCatalogo, Int32? int_IdOpcion, string str_NomOpcion)
        {
            lint_IdCatalogo = int_IdCatalogo;
            lstr_AbrevCatalogo = str_AbrevCatalogo;
            lint_IdOpcion = int_IdOpcion;
            lstr_NomOpcion = str_NomOpcion;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarOpcionesCatalogo.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}