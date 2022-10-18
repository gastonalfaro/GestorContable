using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsModificarCatalogoGeneral : clsProcedimientoAlmacenado
    {
        private Int32 lint_IdCatalogo;
        public Int32 Lint_IdCatalogo
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


        private string lstr_Nombre;
        public string Lstr_Nombre
        {
            get { return lstr_Nombre; }
            set { lstr_Nombre = value; }
        }
        private string lstr_IdModulo;
        public string Lstr_IdModulo
        {
            get { return lstr_IdModulo; }
            set { lstr_IdModulo = value; }
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


        public clsModificarCatalogoGeneral(Int32 int_IdCatalogo, string str_AbrevCatalogo, string str_Nombre, string str_IdModulo, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica)
        {
            lint_IdCatalogo = int_IdCatalogo;
            lstr_AbrevCatalogo = str_AbrevCatalogo;
            lstr_Nombre = str_Nombre;
            lstr_IdModulo = str_IdModulo;
            lstr_Estado = str_Estado;
            lstr_UsrModifica = str_UsrModifica;
            ldt_FchModifica = dt_FchModifica;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ModificarCatalogoGeneral.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}