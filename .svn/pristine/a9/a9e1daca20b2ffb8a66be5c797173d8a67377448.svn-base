using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsConsultarCalculosFlujoEfectivoDE : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private int? lint_IdTramo;
        private DateTime? ldt_FechaDesde;
        private DateTime? ldt_FechaHasta;
        private decimal _CostoAmortInicio,_Interes,_FNE,_SaldoDevengo,_CostoAmortFinal, _Tir;
        #endregion

        #region Obtención y asignación

        public string Lstr_IdPrestamo
        {
            get { return lstr_IdPrestamo; }
            set { lstr_IdPrestamo = value; }
        }
        public int? Lint_IdTramo
        {
            get { return lint_IdTramo; }
            set { lint_IdTramo = value; }
        }
        public DateTime? Ldt_FechaDesde
        {
            get { return ldt_FechaDesde; }
            set { ldt_FechaDesde = value; }
        }
        public DateTime Ldt_FechaHasta
        {
            get { return ldt_FechaHasta == null ? DateTime.MaxValue : (DateTime)ldt_FechaHasta; }
            set { ldt_FechaHasta = value == null ? DateTime.MaxValue : value; }
        }

        public decimal CostoAmortInicio 
        {
            get { return _CostoAmortInicio; }
            set { _CostoAmortInicio = value; }
        }
        public decimal Interes 
        {
            get { return _Interes; }
            set { _Interes = value; }
        }
        public decimal FNE
        {
            get { return _FNE; }
            set { _FNE = value; }
        }
        public decimal CostoAmortFinal
        {
            get { return _CostoAmortFinal; }
            set { _CostoAmortFinal = value; }
        }
        public decimal SaldoDevengo
        {
            get { return _SaldoDevengo; }
            set { _SaldoDevengo = value; }
        }
        public decimal Tir
        {
            get { return _Tir; }
            set { _Tir = value; }
        }
        #endregion

        public DataSet getCalculosFlujoEfectivoDE(string lstr_IdPrestamo, int? lint_IdTramo, DateTime? ldt_FechaDesde = null, DateTime? ldt_FechaHasta = null)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarCalculosFlujoEfectivoDE cr_Procedimiento = new clsConsultarCalculosFlujoEfectivoDE(lstr_IdPrestamo, lint_IdTramo, ldt_FechaDesde, ldt_FechaHasta);
                if (String.Equals(cr_Procedimiento.Lstr_CodigoResultado, "00"))
                {
                    lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                    lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
                }
            }
            catch (Exception ex)
            { }
            return lds_TablasConsulta;
        }

        #region Constructor

        public clsConsultarCalculosFlujoEfectivoDE(string lstr_IdPrestamo, int? lint_IdTramo, DateTime? ldt_FechaDesde = null, DateTime? ldt_FechaHasta = null)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.ldt_FechaDesde = ldt_FechaDesde;
            this.ldt_FechaHasta = ldt_FechaHasta;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\ConsultarCalculosFlujoEfectivoDE.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        public clsConsultarCalculosFlujoEfectivoDE() { }

        #endregion
    }
}