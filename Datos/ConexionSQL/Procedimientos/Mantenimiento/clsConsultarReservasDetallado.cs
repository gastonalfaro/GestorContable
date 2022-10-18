using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarReservasDetallado : clsProcedimientoAlmacenado
    {
        private string lstr_IdReserva;
        public string Lstr_IdReserva
        {
            get { return lstr_IdReserva; }
            set { lstr_IdReserva = value; }
        }

        private string lstr_IdEntidadCP;
        public string Lstr_IdEntidadCP
        {
            get { return lstr_IdEntidadCP; }
            set { lstr_IdEntidadCP = value; }
        }

        private string lstr_IdSociedadFi;
        public string Lstr_IdSociedadFi
        {
            get { return lstr_IdSociedadFi; }
            set { lstr_IdSociedadFi = value; }
        }

        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }

        private string lstr_IdCentroGestor;
        public string Lstr_IdCentroGestor
        {
            get { return lstr_IdCentroGestor; }
            set { lstr_IdCentroGestor = value; }
        }

        private string lstr_IdCentroCosto;
        public string Lstr_IdCentroCosto
        {
            get { return lstr_IdCentroCosto; }
            set { lstr_IdCentroCosto = value; }
        }

        private string lstr_IdCuentaContable;
        public string Lstr_IdCuentaContable
        {
            get { return lstr_IdCuentaContable; }
            set { lstr_IdCuentaContable = value; }
        }

        private string lstr_IdElementoPEP;
        public string Lstr_IdElementoPEP
        {
            get { return lstr_IdElementoPEP; }
            set { lstr_IdElementoPEP = value; }
        }

        private string lstr_IdPosPre;
        public string Lstr_IdPosPre
        {
            get { return lstr_IdPosPre; }
            set { lstr_IdPosPre = value; }
        }

        private string lstr_NomReserva;
        public string Lstr_NomReserva
        {
            get { return lstr_NomReserva; }
            set { lstr_NomReserva = value; }
        }

        private string lstr_SoloDE;
        public string Lstr_SoloDE
        {
            get { return lstr_SoloDE; }
            set { lstr_SoloDE = value; }
        }

        private string lstr_SoloDI;
        public string Lstr_SoloDI
        {
            get { return lstr_SoloDI; }
            set { lstr_SoloDI = value; }
        }

        private string lstr_SoloCT;
        public string Lstr_SoloCT
        {
            get { return lstr_SoloCT; }
            set { lstr_SoloCT = value; }
        }

        private string lstr_OrderBy;
        public string Lstr_OrderBy
        {
            get { return lstr_OrderBy; }
            set { lstr_OrderBy = value; }
        }


        public clsConsultarReservasDetallado(string str_IdReserva, string str_IdEntidadCP, string str_IdSociedadFi, string str_IdMoneda, string str_NomReserva,
            string str_IdCentroGestor = null, string str_IdCentroCosto = null, string str_IdCuentaContable = null, string str_IdElementoPEP = null,
            string str_IdPosPre = null, string str_SoloDE = "N", string str_SoloDI = "N", string str_SoloCT = "N", string str_OrderBy = null)
        {
            lstr_IdReserva = str_IdReserva;
            lstr_IdEntidadCP = str_IdEntidadCP;
            lstr_IdSociedadFi = str_IdSociedadFi;
            lstr_IdMoneda = str_IdMoneda;
            lstr_NomReserva = str_NomReserva;
            lstr_IdCentroGestor = str_IdCentroGestor;
            lstr_IdCentroCosto = str_IdCentroCosto;
            lstr_IdCuentaContable = str_IdCuentaContable;
            lstr_IdElementoPEP = str_IdElementoPEP;
            lstr_IdPosPre = str_IdPosPre;
            lstr_SoloDE = str_SoloDE;
            lstr_SoloDI = str_SoloDI;
            lstr_SoloCT = str_SoloCT;

            lstr_OrderBy = str_OrderBy;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarReservasDetallado.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}