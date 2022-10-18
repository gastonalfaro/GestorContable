using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarReservasDetalle : clsProcedimientoAlmacenado
    {
        private string lstr_IdReserva;
        public string Lstr_IdReserva
        {
            get { return lstr_IdReserva; }
            set { lstr_IdReserva = value; }
        }

        private string lstr_Posicion;
        public string Lstr_Posicion
        {
            get { return lstr_Posicion; }
            set { lstr_Posicion = value; }
        }

        private string lstr_Detalle;
        public string Lstr_Detalle
        {
            get { return lstr_Detalle; }
            set { lstr_Detalle = value; }
        }

        private string lstr_IdPosPre;
        public string Lstr_IdPosPre
        {
            get { return lstr_IdPosPre; }
            set { lstr_IdPosPre = value; }
        }

        private string lstr_IdCentroGestor;
        public string Lstr_IdCentroGestor
        {
            get { return lstr_IdCentroGestor; }
            set { lstr_IdCentroGestor = value; }
        }

        private string lstr_IdFondo;
        public string Lstr_IdFondo
        {
            get { return lstr_IdFondo; }
            set { lstr_IdFondo = value; }
        }

        private string lstr_Segmento;
        public string Lstr_Segmento
        {
            get { return lstr_Segmento; }
            set { lstr_Segmento = value; }
        }

        private string lstr_IdPrograma;
        public string Lstr_IdPrograma
        {
            get { return lstr_IdPrograma; }
            set { lstr_IdPrograma = value; }
        }


        private string lstr_IdCuentaContable;
        public string Lstr_IdCuentaContable
        {
            get { return lstr_IdCuentaContable; }
            set { lstr_IdCuentaContable = value; }
        }

        private string lstr_IdCentroCosto;
        public string Lstr_IdCentroCosto
        {
            get { return lstr_IdCentroCosto; }
            set { lstr_IdCentroCosto = value; }
        }

        private string lstr_IdElementoPEP;
        public string Lstr_IdElementoPEP
        {
            get { return lstr_IdElementoPEP; }
            set { lstr_IdElementoPEP = value; }
        }


        public clsConsultarReservasDetalle(string str_IdReserva, string str_Posicion, string str_Detalle, string str_IdPosPre, string str_IdCentroGestor, string str_IdFondo, string str_Segmento, string str_IdPrograma, string str_IdCuentaContable, string str_IdCentroCosto, string str_IdElementoPEP)
        {
            lstr_IdReserva = str_IdReserva;
            lstr_Posicion = str_Posicion;
            lstr_Detalle = str_Detalle;
            lstr_IdPosPre = str_IdPosPre;
            lstr_IdCentroGestor = str_IdCentroGestor;
            lstr_IdFondo = str_IdFondo;
            lstr_Segmento = str_Segmento;
            lstr_IdPrograma = str_IdPrograma;
            lstr_IdCuentaContable = str_IdCuentaContable;
            lstr_IdCentroCosto = str_IdCentroCosto;
            lstr_IdElementoPEP = str_IdElementoPEP;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarReservasDetalle.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}