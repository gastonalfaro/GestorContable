using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearCuentaConsolida : clsProcedimientoAlmacenado
    {
        private string lstr_IdCuentaContable;
        public string Lstr_IdCuentaContable
        {
            get { return lstr_IdCuentaContable; }
            set { lstr_IdCuentaContable = value; }
        }

        private string lstr_IdPlanCuenta;
        public string Lstr_IdPlanCuenta
        {
            get { return lstr_IdPlanCuenta; }
            set { lstr_IdPlanCuenta = value; }
        }


        private string lstr_NomCuentaContable;
        public string Lstr_NomCuentaContable
        {
            get { return lstr_NomCuentaContable; }
            set { lstr_NomCuentaContable = value; }
        }


        private string lstr_IndTotales;
        public string Lstr_IndTotales
        {
            get { return lstr_IndTotales; }
            set { lstr_IndTotales = value; }
        }

        private string lstr_IndNaturaleza;
        public string Lstr_IndNaturaleza
        {
            get { return lstr_IndNaturaleza; }
            set { lstr_IndNaturaleza = value; }
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

        public clsCrearCuentaConsolida(string str_IdCuentaContable, string str_IdPlanCuenta, string str_NomCuentaContable, string str_IndTotales, string str_IndNaturaleza, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdCuentaContable = str_IdCuentaContable;
            lstr_IdPlanCuenta = str_IdPlanCuenta;
            lstr_NomCuentaContable = str_NomCuentaContable;
            lstr_IndTotales = str_IndTotales;
            lstr_IndNaturaleza = str_IndNaturaleza;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearCuentaConsolida.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}