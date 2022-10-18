using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using log4net.Config;
using System.Configuration;

//Log4Net inicializa en WebApplication

namespace Datos.ConexionSQL.Procedimientos.Contigentes
{
    public class clsRegistrarBitacoraDeMovimientosCuentas : clsProcedimientoAlmacenado
    {
        #region Variables 

        /// log4Net variable 
        private static readonly ILog log = LogManager.GetLogger(typeof(clsRegistrarExpediente));
        private string lstr_IdExpediente = string.Empty;
        private string lstr_IdModulo = string.Empty;
        private string lstr_IdSociedadGL = string.Empty; //
        private string lstr_NroAsiento = string.Empty; //
        private string lstr_DetalleTransaccion = string.Empty;
        private decimal ldec_Monto = 0;
        private decimal ldec_Debe = 0;
        private decimal ldec_Haber = 0;
        private string lstr_TipoCuenta = string.Empty;
        private decimal ldec_SaldoAcumulado = 0;
        //Bitacora
        private string lstr_UsrCreacion=string.Empty;//
        private string lstr_UsrModificar = string.Empty;
       
        #endregion
       
        #region asingacion y obtencion
        public string Lstr_IdExpediente
        {
            get { return lstr_IdExpediente; }
            set { lstr_IdExpediente = value; }
        }
        public string Lstr_IdModulo
        {
            get { return lstr_IdModulo; }
            set { lstr_IdModulo = value; }
        }
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }
        public string Lstr_NroAsiento
        {
            get { return lstr_NroAsiento; }
            set { lstr_NroAsiento = value; }
        }
        public string Lstr_DetalleTransaccion
        {
            get { return lstr_DetalleTransaccion; }
            set { lstr_DetalleTransaccion = value; }
        }
        public decimal Ldec_Monto
        {
            get { return ldec_Monto; }
            set { ldec_Monto = value; }
        }
        public decimal Ldec_Debe
        {
            get { return ldec_Debe; }
            set { ldec_Debe = value; }
        }
        public decimal Ldec_Haber
                {
                    get { return ldec_Haber; }
                    set { ldec_Haber = value; }
                }
        public string Lstr_TipoCuenta
            {
                get { return lstr_TipoCuenta; }
                set { lstr_TipoCuenta = value; }
            }
        public decimal Ldec_SaldoAcumulado
        {
            get { return ldec_SaldoAcumulado; }
            set { ldec_SaldoAcumulado = value; }
        }
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }
        public string Lstr_UsrModificar
        {
            get { return lstr_UsrModificar; }
            set { lstr_UsrModificar = value; }
        }
        

        
        #endregion
        
        #region Metodos

        public clsRegistrarBitacoraDeMovimientosCuentas()
        {
           
        }


        /// <summary>
        /// Registrar Bitacora movimientos Configs
        /// </summary>
        /// <returns></returns>
        public bool ProcesarSPRegistrarBitacoraDeMovimientosCuentas()
        {
            string str_RutaArchivo=string.Empty;
            string str_DireccionConfigs = string.Empty;
            bool resultFlag = false;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];
                str_RutaArchivo = str_DireccionConfigs + "\\Contigentes\\Expedientes\\RegistrarBitacoraMovimientosCuentas.config";
                EjecucionSP(str_RutaArchivo, this);
                resultFlag = true;
            }
            catch (Exception ex)
            {
                resultFlag = false;
            }

            return resultFlag;
        }

        #endregion
        
    }
}