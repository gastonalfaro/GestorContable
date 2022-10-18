using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsModificarReserva : clsProcedimientoAlmacenado
    {
        private string lstr_IdReserva;
        public string Lstr_IdReserva
        {
            get { return lstr_IdReserva; }
            set { lstr_IdReserva = value; }
        }

        private Int32 lint_OrdenContingentes;
        public Int32 Lint_OrdenContingentes
        {
            get { return lint_OrdenContingentes; }
            set { lint_OrdenContingentes = value; }
        }

        private Int32 lint_OrdenDeudaInterna;
        public Int32 Lint_OrdenDeudaInterna
        {
            get { return lint_OrdenDeudaInterna; }
            set { lint_OrdenDeudaInterna = value; }
        }


        private Int32 lint_OrdenDeudaExterna;
        public Int32 Lint_OrdenDeudaExterna
        {
            get { return lint_OrdenDeudaExterna; }
            set { lint_OrdenDeudaExterna = value; }
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

        public clsModificarReserva(string str_IdReserva, Int32 int_OrdenContingentes, Int32 int_OrdenDeudaInterna, Int32 int_OrdenDeudaExterna, string str_UsrModifica, DateTime dt_FchModifica)
        {
            lstr_IdReserva = str_IdReserva;
            lint_OrdenContingentes = int_OrdenContingentes;
            lint_OrdenDeudaInterna = int_OrdenDeudaInterna;
            lint_OrdenDeudaExterna = int_OrdenDeudaExterna;
            lstr_UsrModifica = str_UsrModifica;
            ldt_FchModifica = dt_FchModifica;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ModificarReserva.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}