using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsModificarBancoCuenta : clsProcedimientoAlmacenado
    {


        private string lstr_IdBanco;
        public string Lstr_IdBanco
        {
            get { return lstr_IdBanco; }
            set { lstr_IdBanco = value; }
        }

        private string lstr_IdCuentaBancaria;
        public string Lstr_IdCuentaBancaria
        {
            get { return lstr_IdCuentaBancaria; }
            set { lstr_IdCuentaBancaria = value; }
        }

        private string lstr_IdCuentaContable;
        public string Lstr_IdCuentaContable
        {
            get { return lstr_IdCuentaContable; }
            set { lstr_IdCuentaContable = value; }
        }

        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_TipoCuenta;
        public string Lstr_TipoCuenta
        {
            get { return lstr_TipoCuenta; }
            set { lstr_TipoCuenta = value; }
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

        public clsModificarBancoCuenta(string str_IdBanco, string str_IdCuentaBancaria, string str_IdCuentaContable, string str_IdSociedadGL, string str_TipoCuenta, string str_UsrModifica, DateTime dt_FchModifica)
        {
            lstr_IdBanco = str_IdBanco;
            lstr_IdCuentaBancaria = str_IdCuentaBancaria;
            lstr_IdCuentaContable = str_IdCuentaContable;
            lstr_IdSociedadGL = str_IdSociedadGL;
            lstr_TipoCuenta = str_TipoCuenta;
            lstr_UsrModifica = str_UsrModifica;
            ldt_FchModifica = dt_FchModifica;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ModificarBancoCuenta.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}