using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsModificarOpcionCatalogo : clsProcedimientoAlmacenado
    {
        private Int32 lint_IdCatalogo;
        public Int32 Lint_IdCatalogo
        {
            get { return lint_IdCatalogo; }
            set { lint_IdCatalogo = value; }
        }

        private Int32 lint_IdOpcion;
        public Int32 Lint_IdOpcion
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

        private string lstr_UsrModifica;
        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }

        private DateTime ldt_FchModifica;
        public DateTime Ldt_FchModifica
        {
            get { return ldt_FchModifica; }
            set { ldt_FchModifica = value; }
        }


        public clsModificarOpcionCatalogo(Int32 int_IdCatalogo, Int32 int_IdOpcion, string str_ValOpcion, string str_NomOpcion, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica)
        {
            lint_IdCatalogo = int_IdCatalogo;
            lint_IdOpcion = int_IdOpcion;
            lstr_ValOpcion = str_ValOpcion;
            lstr_NomOpcion = str_NomOpcion;
            lstr_Estado = str_Estado;
            lstr_UsrModifica = str_UsrModifica;
            ldt_FchModifica = dt_FchModifica;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ModificarOpcionCatalogo.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}