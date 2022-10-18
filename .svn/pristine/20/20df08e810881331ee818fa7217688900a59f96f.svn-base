using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearGrupoCuenta : clsProcedimientoAlmacenado
    {
        private string lstr_IdGrupoCuenta;
        public string Lstr_IdGrupoCuenta
        {
            get { return lstr_IdGrupoCuenta; }
            set { lstr_IdGrupoCuenta = value; }
        }

        private string lstr_IdPlanCuenta;
        public string Lstr_IdPlanCuenta
        {
            get { return lstr_IdPlanCuenta; }
            set { lstr_IdPlanCuenta = value; }
        }

        private string lstr_CuentaDesde;
        public string Lstr_CuentaDesde
        {
            get { return lstr_CuentaDesde; }
            set { lstr_CuentaDesde = value; }
        }

        private string lstr_CuentaHasta;
        public string Lstr_CuentaHasta
        {
            get { return lstr_CuentaHasta; }
            set { lstr_CuentaHasta = value; }
        }

        private string lstr_NomGrupoCuenta;
        public string Lstr_NomGrupoCuenta
        {
            get { return lstr_NomGrupoCuenta; }
            set { lstr_NomGrupoCuenta = value; }
        }


        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public clsCrearGrupoCuenta(string str_IdGrupoCuenta, string str_IdPlanCuenta, string str_CuentaDesde, string str_CuentaHasta, string str_NomGrupoCuenta, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdGrupoCuenta = str_IdGrupoCuenta;
            lstr_IdPlanCuenta = str_IdPlanCuenta;
            lstr_CuentaDesde = str_CuentaDesde;
            lstr_CuentaHasta = str_CuentaHasta;
            lstr_NomGrupoCuenta = str_NomGrupoCuenta;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearGrupoCuenta.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}