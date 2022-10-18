﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsModificarTipoCambio : clsProcedimientoAlmacenado
    {
        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }

        private DateTime ldt_FchReferencia;
        public DateTime Ldt_FchReferencia
        {
            get { return ldt_FchReferencia; }
            set { ldt_FchReferencia = value; }
        }


        private string lstr_TipoTransaccion;
        public string Lstr_TipoTransaccion
        {
            get { return lstr_TipoTransaccion; }
            set { lstr_TipoTransaccion = value; }
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


        public clsModificarTipoCambio(string str_IdMoneda, DateTime dt_FchReferencia, string str_TipoTransaccion, decimal dec_Valor, string str_UsrModifica, DateTime dt_FchModifica)
        {
            lstr_IdMoneda = str_IdMoneda;
            ldt_FchReferencia = dt_FchReferencia;
            lstr_TipoTransaccion = str_TipoTransaccion;
            ldec_Valor = dec_Valor;
            lstr_UsrModifica = str_UsrModifica;
            ldt_FchModifica = dt_FchModifica;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ModificarTipoCambio.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}