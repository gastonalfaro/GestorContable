using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsModificarPrevisionIncobrable : clsProcedimientoAlmacenado
    {
        private Int32 lint_DiasMorosidad;
        public Int32 Lint_DiasMorosidad
        {
            get { return lint_DiasMorosidad; }
            set { lint_DiasMorosidad = value; }
        }

        private Decimal ldec_PorcEstimacion;
        public Decimal Ldec_PorcEstimacion
        {
            get { return ldec_PorcEstimacion; }
            set { ldec_PorcEstimacion = value; }
        }

        private string lstr_Descripcion;
        public string Lstr_Descripcion
        {
            get { return lstr_Descripcion; }
            set { lstr_Descripcion = value; }
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

        public clsModificarPrevisionIncobrable(Int32 int_DiasMorosidad, Decimal dec_PorcEstimacion, string str_Descripcion, string str_UsrModifica, DateTime dt_FchModifica)
        {
            lint_DiasMorosidad = int_DiasMorosidad;
            lstr_Descripcion = str_Descripcion;
            ldec_PorcEstimacion = dec_PorcEstimacion;
            lstr_UsrModifica = str_UsrModifica;
            ldt_FchModifica = dt_FchModifica;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ModificarPrevisionIncobrable.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}