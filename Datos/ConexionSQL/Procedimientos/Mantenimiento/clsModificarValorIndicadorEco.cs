using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsModificarValorIndicadorEco : clsProcedimientoAlmacenado
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

        public clsModificarValorIndicadorEco(string str_IdIndicadorEco, DateTime dt_FchReferencia, decimal dec_Valor, string str_UsrModifica, DateTime dt_FchModifica)
        {
            lstr_IdIndicadorEco = str_IdIndicadorEco;
            ldt_FchReferencia = dt_FchReferencia;
            ldec_Valor = dec_Valor;
            lstr_UsrModifica = str_UsrModifica;
            ldt_FchModifica = dt_FchModifica;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ModificarValorIndicadorEco.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}