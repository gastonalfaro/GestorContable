using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarBancosCuentas : clsProcedimientoAlmacenado
    {


        private string lstr_IdBanco;
        public string Lstr_IdBanco
        {
            get { return lstr_IdBanco; }
            set { lstr_IdBanco = value; }
        }

        private string lstr_IdBancoPropio;
        public string Lstr_IdBancoPropio
        {
            get { return lstr_IdBancoPropio; }
            set { lstr_IdBancoPropio = value; }
        }

        private string lstr_IdCuentaBancaria;
        public string Lstr_IdCuentaBancaria
        {
            get { return lstr_IdCuentaBancaria; }
            set { lstr_IdCuentaBancaria = value; }
        }

        private string lstr_CuentaBancaria;
        public string Lstr_CuentaBancaria
        {
            get { return lstr_CuentaBancaria; }
            set { lstr_CuentaBancaria = value; }
        }

        private string lstr_IdCuentaContable;
        public string Lstr_IdCuentaContable
        {
            get { return lstr_IdCuentaContable; }
            set { lstr_IdCuentaContable = value; }
        }

        private string lstr_IdSociedadFi;
        public string Lstr_IdSociedadFi
        {
            get { return lstr_IdSociedadFi; }
            set { lstr_IdSociedadFi = value; }
        }

        public clsConsultarBancosCuentas(string str_IdBanco, string str_IdBancoPropio, string str_IdCuentaBancaria, string str_CuentaBancaria, string str_IdCuentaContable, string str_IdSociedadFi)
        {
            lstr_IdBanco = str_IdBanco;
            lstr_IdBancoPropio = str_IdBancoPropio;
            lstr_IdCuentaBancaria = str_IdCuentaBancaria;
            lstr_CuentaBancaria = str_CuentaBancaria;
            lstr_IdCuentaContable = str_IdCuentaContable;
            lstr_IdSociedadFi = str_IdSociedadFi;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarBancosCuentas.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}