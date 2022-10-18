using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsCreaAsientoAjuste : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdAsiento;
        private string lstr_UsrCreacion;
        private string lstr_IdCuenta;
        private string lstr_NombreCuenta;
        private string lstr_ClaveContable;
        private decimal ldec_MontoContable;
        private decimal ldec_MontoDebe;
        private decimal ldec_MontoHaber;
        private string lstr_Moneda;

        #endregion

        #region Obtención y asignación

        public string Lstr_IdAsiento
        {
            get { return lstr_IdAsiento; }
            set { lstr_IdAsiento = value; }
        }
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }
        public string Lstr_IdCuenta
        {
            get { return lstr_IdCuenta; }
            set { lstr_IdCuenta = value; }
        }
        public string Lstr_NombreCuenta
        {
            get { return lstr_NombreCuenta; }
            set { lstr_NombreCuenta = value; }
        }
        public string Lstr_ClaveContable
        {
            get { return lstr_ClaveContable; }
            set { lstr_ClaveContable = value; }
        }
        public decimal Ldec_MontoContable
        {
            get { return ldec_MontoContable; }
            set { ldec_MontoContable = value; }
        }
        public decimal Ldec_MontoDebe
        {
            get { return ldec_MontoDebe; }
            set { ldec_MontoDebe = value; }
        }
        public decimal Ldec_MontoHaber
        {
            get { return ldec_MontoHaber; }
            set { ldec_MontoHaber = value; }
        }
        public string Lstr_Moneda
        {
            get { return lstr_Moneda; }
            set { lstr_Moneda = value; }
        }

        #endregion 

        #region Constructor

        public clsCreaAsientoAjuste(string lstr_IdAsiento, string lstr_UsrCreacion, string lstr_IdCuenta, string lstr_NombreCuenta, string lstr_ClaveContable, decimal ldec_MontoContable, decimal ldec_MontoDebe, decimal ldec_MontoHaber, string lstr_Moneda)
        {
            this.lstr_IdAsiento = lstr_IdAsiento;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            this.lstr_IdCuenta = lstr_IdCuenta;
            this.lstr_NombreCuenta = lstr_NombreCuenta;
            this.lstr_ClaveContable = lstr_ClaveContable;
            this.ldec_MontoContable = ldec_MontoContable;
            this.ldec_MontoDebe = ldec_MontoDebe;
            this.ldec_MontoHaber = ldec_MontoHaber;
            this.lstr_Moneda = lstr_Moneda;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\CrearAsientoAjuste.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}