using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsCreaEmisionSubasta : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_NroEmisionSerie;
        private decimal ldec_CapitalFchSubasta;
        private decimal ldec_ImpDevengarFchSubasta;
        private decimal ldec_CuponCorridoFchSubasta;
        private decimal ldec_ValorEmision;
        private decimal ldec_PorcentajeEmision;
        private decimal ldec_ImpDevengarTranscurrido;
        private decimal ldec_CuponCorridoTranscurrido;
        private decimal ldec_CapitalDeBaja;
        private decimal ldec_IporteDevengarDeBaja;
        private decimal ldec_CuponCorridoDeBaja;
        private decimal ldec_ValorEmisionDeBaja;
        private decimal ldec_EntradaSalidaCaja;
        private decimal ldec_NetoSubastado;
        private decimal ldec_TotalNetoBaja;
        private decimal ldec_Diferencia;
        private decimal ldec_Capital;
        private decimal ldec_InteresDevengado;
        private decimal ldec_ImpRenta;
        private decimal ldec_Descuento;
        private decimal ldec_TotalColocado;
        private string lstr_UsrCreacion;


        #endregion

        #region Obtención y asignación

        public string Lstr_NroEmisionSerie
        {
            get { return lstr_NroEmisionSerie; }
            set { lstr_NroEmisionSerie = value; }
        }
        public decimal Ldec_CapitalFchSubasta
        {
            get { return ldec_CapitalFchSubasta; }
            set { ldec_CapitalFchSubasta = value; }
        }
        public decimal Ldec_ImpDevengarFchSubasta
        {
            get { return ldec_ImpDevengarFchSubasta; }
            set { ldec_ImpDevengarFchSubasta = value; }
        }
        public decimal Ldec_CuponCorridoFchSubasta
        {
            get { return ldec_CuponCorridoFchSubasta; }
            set { ldec_CuponCorridoFchSubasta = value; }
        }
        public decimal Ldec_ValorEmision
        {
            get { return ldec_ValorEmision; }
            set { ldec_ValorEmision = value; }
        }
        public decimal Ldec_PorcentajeEmision
        {
            get { return ldec_PorcentajeEmision; }
            set { ldec_PorcentajeEmision = value; }
        }
        public decimal Ldec_ImpDevengarTranscurrido
        {
            get { return ldec_ImpDevengarTranscurrido; }
            set { ldec_ImpDevengarTranscurrido = value; }
        }
        public decimal Ldec_CuponCorridoTranscurrido
        {
            get { return ldec_CuponCorridoTranscurrido; }
            set { ldec_CuponCorridoTranscurrido = value; }
        }
        public decimal Ldec_CapitalDeBaja
        {
            get { return ldec_CapitalDeBaja; }
            set { ldec_CapitalDeBaja = value; }
        }
        public decimal Ldec_IporteDevengarDeBaja
        {
            get { return ldec_IporteDevengarDeBaja; }
            set { ldec_IporteDevengarDeBaja = value; }
        }
        public decimal Ldec_CuponCorridoDeBaja
        {
            get { return ldec_CuponCorridoDeBaja; }
            set { ldec_CuponCorridoDeBaja = value; }
        }
        public decimal Ldec_ValorEmisionDeBaja
        {
            get { return ldec_ValorEmisionDeBaja; }
            set { ldec_ValorEmisionDeBaja = value; }
        }
        public decimal Ldec_EntradaSalidaCaja
        {
            get { return ldec_EntradaSalidaCaja; }
            set { ldec_EntradaSalidaCaja = value; }
        }
        public decimal Ldec_NetoSubastado
        {
            get { return ldec_NetoSubastado; }
            set { ldec_NetoSubastado = value; }
        }
        public decimal Ldec_TotalNetoBaja
        {
            get { return ldec_TotalNetoBaja; }
            set { ldec_TotalNetoBaja = value; }
        }
        public decimal Ldec_Diferencia
        {
            get { return ldec_Diferencia; }
            set { ldec_Diferencia = value; }
        }
        public decimal Ldec_Capital
        {
            get { return ldec_Capital; }
            set { ldec_Capital = value; }
        }
        public decimal Ldec_InteresDevengado
        {
            get { return ldec_InteresDevengado; }
            set { ldec_InteresDevengado = value; }
        }
        public decimal Ldec_ImpRenta
        {
            get { return ldec_ImpRenta; }
            set { ldec_ImpRenta = value; }
        }
        public decimal Ldec_Descuento
        {
            get { return ldec_Descuento; }
            set { ldec_Descuento = value; }
        }
        public decimal Ldec_TotalColocado
        {
            get { return ldec_TotalColocado; }
            set { ldec_TotalColocado = value; }
        }
        public string Lstr_UsrCreacion {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }
        
        #endregion 

        #region Constructor

        public clsCreaEmisionSubasta(string lstr_NroEmisionSerie, decimal ldec_CapitalFchSubasta,decimal ldec_ImpDevengarFchSubasta,
        decimal ldec_CuponCorridoFchSubasta, decimal ldec_ValorEmision, decimal ldec_PorcentajeEmision, decimal ldec_ImpDevengarTranscurrido,
        decimal ldec_CuponCorridoTranscurrido, decimal ldec_CapitalDeBaja, decimal ldec_IporteDevengarDeBaja, decimal ldec_CuponCorridoDeBaja,
        decimal ldec_ValorEmisionDeBaja, decimal ldec_EntradaSalidaCaja, decimal ldec_NetoSubastado, decimal ldec_TotalNetoBaja,
        decimal ldec_Diferencia, decimal ldec_Capital, decimal ldec_InteresDevengado, decimal ldec_ImpRenta, decimal ldec_Descuento,
        decimal ldec_TotalColocado, string lstr_UsrCreacion)
        {
            this.lstr_NroEmisionSerie = lstr_NroEmisionSerie;
            this.ldec_CapitalFchSubasta = ldec_CapitalFchSubasta;
            this.ldec_ImpDevengarFchSubasta = ldec_ImpDevengarFchSubasta;
            this.ldec_CuponCorridoFchSubasta = ldec_CuponCorridoFchSubasta;
            this.ldec_ValorEmision = ldec_ValorEmision;
            this.ldec_PorcentajeEmision = ldec_PorcentajeEmision;
            this.ldec_ImpDevengarTranscurrido = ldec_ImpDevengarTranscurrido;
            this.ldec_CuponCorridoTranscurrido = ldec_CuponCorridoTranscurrido;
            this.ldec_CapitalDeBaja = ldec_CapitalDeBaja;
            this.ldec_IporteDevengarDeBaja = ldec_IporteDevengarDeBaja;
            this.ldec_CuponCorridoDeBaja = ldec_CuponCorridoDeBaja;
            this.ldec_ValorEmisionDeBaja = ldec_ValorEmisionDeBaja;
            this.ldec_EntradaSalidaCaja = ldec_EntradaSalidaCaja;
            this.ldec_NetoSubastado = ldec_NetoSubastado;
            this.ldec_TotalNetoBaja = ldec_TotalNetoBaja;
            this.ldec_Diferencia = ldec_Diferencia;
            this.ldec_Capital = ldec_Capital;
            this.ldec_InteresDevengado = ldec_InteresDevengado;
            this.ldec_ImpRenta = ldec_ImpRenta;
            this.ldec_Descuento = ldec_Descuento;
            this.ldec_TotalColocado = ldec_TotalColocado;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\CrearEmisionesSubasta.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}