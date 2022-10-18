using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Contigentes
{
    public class clsRegistrarPretensionInicial:clsProcedimientoAlmacenado
    {
        private string lstr_IdExpediente = string.Empty;
        private string lstr_MonedaPretension = string.Empty; //nombre de la moneda con la que se pretende cobrar ¢ o $
        private decimal ldec_TipoCambio = 0; //valor del tipo de cambio vigente
        private decimal ldec_MontoPretension = 0;//Corresponde al monto estimado de la demanda en la moneda que se haya declarado. 
        private decimal ldec_MontoPretColones = 0;
        private int lint_EstadoPretension = 0;//estado de la pretension 
        private DateTime? ldt_PosibleFecEntRec = new DateTime(); //Moneda base del tramo
        private decimal ldec_ValorPresente = 0;//Valor presente 
        private string lstr_UsrModificar = string.Empty;
        private decimal ldec_MontoPosibleReembolso = 0;
        private string lstr_ObservacionesPretension = string.Empty;
        private string lstr_Sociedad = string.Empty;
        private string lstr_TipoProceso = String.Empty;

        public string Lstr_Sociedad
        {
            get { return lstr_Sociedad; }
            set { lstr_Sociedad = value; }
        }
        public string Lstr_ObservacionesPretension
        {
            get { return lstr_ObservacionesPretension; }
            set { lstr_ObservacionesPretension = value; }
        }

        public decimal Ldec_MontoPosibleReembolso
        {
            get { return ldec_MontoPosibleReembolso; }
            set { ldec_MontoPosibleReembolso = value; }
        }


        public string Lstr_IdExpediente
        {
            get { return lstr_IdExpediente; }
            set { lstr_IdExpediente = value; }
        }
        
        public string Lstr_MonedaPretension
        {
            get { return lstr_MonedaPretension; }
            set { lstr_MonedaPretension = value; }
        }
        
        public decimal Ldec_TipoCambio
        {
            get { return ldec_TipoCambio; }
            set { ldec_TipoCambio = value; }
        }

        public decimal Ldec_MontoPretension
        {
            get { return ldec_MontoPretension; }
            set { ldec_MontoPretension = value; }
        }

        public decimal Ldec_MontoPretColones
        {
            get { return ldec_MontoPretColones; }
            set { ldec_MontoPretColones = value; }
        }

        public int Lint_EstadoPretension
        {
            get { return lint_EstadoPretension; }
            set { lint_EstadoPretension = value; }

        }
        public String Lstr_TipoProceso
        {
            get { return lstr_TipoProceso; }
            set { lstr_TipoProceso = value; }
        }

        public DateTime? Ldt_PosibleFecEntRec
        {
            get { return ldt_PosibleFecEntRec; }
            set { ldt_PosibleFecEntRec = value; }

        }

        public decimal Ldec_ValorPresente
        {

            get { return ldec_ValorPresente; }
            set { ldec_ValorPresente = value; }
        }

        public string Lstr_UsrModificar
        {
            get { return lstr_UsrModificar; }
            set { lstr_UsrModificar = value; }
        }

        /// <summary>
        /// Resgistrar Pretension Inicial Configs
        /// </summary>
        /// <returns></returns>
        public bool ProcesarSPRegitsrarPretesnionInicial()
        {
            string str_RutaArchivo = string.Empty;
            string str_DireccionConfigs = string.Empty;
            bool resultFlag = false;
            try
            {

                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];
                str_RutaArchivo = str_DireccionConfigs + "\\Contigentes\\Expedientes\\RegistrarPretensionInicial.config";
                EjecucionSP(str_RutaArchivo, this);
                resultFlag = true;
            }
            catch (Exception ex)
            {
                resultFlag = false;
            }

            return resultFlag;
        }
    }
}