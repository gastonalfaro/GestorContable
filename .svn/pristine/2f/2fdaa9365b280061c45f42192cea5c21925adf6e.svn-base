using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearValorIndicadorEco : clsProcedimientoAlmacenado
    {
        private string lstr_IdIndicadorEco;
        public string Lstr_IdIndicadorEco
        {
            get { return lstr_IdIndicadorEco; }
            set { lstr_IdIndicadorEco = value; }
        }

        private DateTime ldt_FchReferencia;
        public DateTime Ldt_FchReferencia
        {
            get { return ldt_FchReferencia; }
            set { ldt_FchReferencia = value; }
        }


        private decimal ldec_Valor;
        public decimal Ldec_Valor
        {
            get { return ldec_Valor; }
            set { ldec_Valor = value; }
        }

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public clsCrearValorIndicadorEco(string str_IdIndicadorEco, DateTime dt_FchReferencia, decimal dec_Valor, string str_UsrCreacion)
        {
            lstr_IdIndicadorEco = str_IdIndicadorEco;
            ldt_FchReferencia = dt_FchReferencia;
            ldec_Valor = dec_Valor;
            lstr_UsrCreacion = str_UsrCreacion;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearValorIndicadorEco.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}