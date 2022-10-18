using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarPrevisionesIncobrables : clsProcedimientoAlmacenado
    {
        private Nullable<int> lint_DiasMorosidad;
        public Nullable<int> Lint_DiasMorosidad
        {
            get { return lint_DiasMorosidad; }
            set { lint_DiasMorosidad = value; }
        }

        private Nullable<Decimal> ldec_PorcEstimacion;
        public Nullable<Decimal> Ldec_PorcEstimacion
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


        public clsConsultarPrevisionesIncobrables(Nullable<int> int_DiasMorosidad, Nullable<Decimal> dec_PorcEstimacion, string str_Descripcion)
        {
            lint_DiasMorosidad = int_DiasMorosidad;
            lstr_Descripcion = str_Descripcion;
            ldec_PorcEstimacion = dec_PorcEstimacion;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarPrevisionesIncobrables.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}