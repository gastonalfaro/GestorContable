using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearOpcionCatalogo : clsProcedimientoAlmacenado
    {
        private Int32 lint_IdCatalogo;
        public Int32 Lint_IdCatalogo
        {
            get { return lint_IdCatalogo; }
            set { lint_IdCatalogo = value; }
        }

        private Int32 lint_IdOpcion;
        public Int32  Lint_IdOpcion
        {
            get { return lint_IdOpcion; }
            set { lint_IdOpcion = value; }
        }

        private string lstr_ValOpcion;
        public string Lstr_ValOpcion
        {
            get { return lstr_ValOpcion; }
            set { lstr_ValOpcion = value; }
        }

        private string lstr_NomOpcion;
        public string Lstr_NomOpcion
        {
            get { return lstr_NomOpcion; }
            set { lstr_NomOpcion = value; }
        }


        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public clsCrearOpcionCatalogo(Int32 int_IdCatalogo, Int32 int_IdOpcion, string str_ValOpcion, string str_NomOpcion, string str_Estado, string str_UsrCreacion)
        {
            lint_IdCatalogo = int_IdCatalogo;
            lint_IdOpcion = int_IdOpcion;
            lstr_ValOpcion = str_ValOpcion;
            lstr_NomOpcion = str_NomOpcion;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearOpcionCatalogo.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}