using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna;
using System.Data;
using log4net;
using log4net.Config;

namespace LogicaNegocio.CalculosFinancieros.DeudaInterna
{
    public class clsSubastaCanje
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        public DataSet ConsultarNroEmisionCompra(string lstr_NroEmision = null, string lstr_SistemaNegociacion = null)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaNroEmisionCompra cr_Procedimiento = new clsConsultaNroEmisionCompra(lstr_NroEmision, lstr_SistemaNegociacion);
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

        public DataSet ConsultarNroEmisionVenta(string lstr_SistemaNegociacion = null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaNroEmisionVenta cr_Procedimiento = new clsConsultaNroEmisionVenta(lstr_SistemaNegociacion);
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

        public DataSet ConsultarInteresDevengo(string lstr_NroEmision = null)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaInteresDevengo cr_Procedimiento = new clsConsultaInteresDevengo(lstr_NroEmision);
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

        public DataSet ConsultarInteresFlujo(string lstr_NroEmision = null)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaInteresFlujo cr_Procedimiento = new clsConsultaInteresFlujo(lstr_NroEmision);
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

        public DataSet ConsultarCostoAmortizacionFinal(string lstr_NroEmision = null)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaCostoAmortizacionFinal cr_Procedimiento = new clsConsultaCostoAmortizacionFinal(lstr_NroEmision);
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

        public DataSet ConsultarNumerosEmision()
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaNumerosEmision cr_Procedimiento = new clsConsultaNumerosEmision();
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

        public DataSet ConsultarColumnaDevengoMensualNroSerie(string lstr_NroEmision = null)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaColumnaDevengoMensual cr_Procedimiento = new clsConsultaColumnaDevengoMensual(lstr_NroEmision);
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

        public DataSet ConsultarPenultimaFechaDevengo(string lstr_NroEmision = null)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaPenultimaFechaDevengo cr_Procedimiento = new clsConsultaPenultimaFechaDevengo(lstr_NroEmision);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_NroEmisionSerie">-</param>
        /// <param name="ldec_CapitalFchSubasta">-</param>
        /// <param name="ldec_ImpDevengarFchSubasta">-</param>
        /// <param name="ldec_CuponCorridoFchSubasta">-</param>
        /// <param name="ldec_ValorEmision">-</param>
        /// <param name="ldec_PorcentajeEmision">-</param>
        /// <param name="ldec_ImpDevengarTranscurrido">-</param>
        /// <param name="ldec_CuponCorridoTranscurrido">-</param>
        /// <param name="ldec_CapitalDeBaja">-</param>
        /// <param name="ldec_IporteDevengarDeBaja">-</param>
        /// <param name="ldec_CuponCorridoDeBaja">-</param>
        /// <param name="ldec_ValorEmisionDeBaja">-</param>
        /// <param name="ldec_EntradaSalidaCaja">-</param>
        /// <param name="ldec_NetoSubastado">-</param>
        /// <param name="ldec_TotalNetoBaja">-</param>
        /// <param name="ldec_Diferencia">-</param>
        /// <param name="ldec_Capital">-</param>
        /// <param name="ldec_InteresDevengado">-</param>
        /// <param name="ldec_ImpRenta">-</param>
        /// <param name="ldec_Descuento">-</param>
        /// <param name="ldec_TotalColocado">-</param>
        /// <param name="lstr_UsrCreacion">-</param>
        /// <param name="str_CodResultado">-</param>
        /// <param name="str_Mensaje">-</param>
        /// <returns></returns>
        public string CrearEmisionSubasta(string lstr_NroEmisionSerie, decimal ldec_CapitalFchSubasta, decimal ldec_ImpDevengarFchSubasta,
        decimal ldec_CuponCorridoFchSubasta, decimal ldec_ValorEmision, decimal ldec_PorcentajeEmision, decimal ldec_ImpDevengarTranscurrido,
        decimal ldec_CuponCorridoTranscurrido, decimal ldec_CapitalDeBaja, decimal ldec_IporteDevengarDeBaja, decimal ldec_CuponCorridoDeBaja,
        decimal ldec_ValorEmisionDeBaja, decimal ldec_EntradaSalidaCaja, decimal ldec_NetoSubastado, decimal ldec_TotalNetoBaja,
        decimal ldec_Diferencia, decimal ldec_Capital, decimal ldec_InteresDevengado, decimal ldec_ImpRenta, decimal ldec_Descuento,
        decimal ldec_TotalColocado, string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            string lstr_ResCreacion = "";
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaEmisionSubasta cr_Procedimiento = new clsCreaEmisionSubasta(lstr_NroEmisionSerie, ldec_CapitalFchSubasta,
                    ldec_ImpDevengarFchSubasta, ldec_CuponCorridoFchSubasta, ldec_ValorEmision, ldec_PorcentajeEmision,
                    ldec_ImpDevengarTranscurrido, ldec_CuponCorridoTranscurrido, ldec_CapitalDeBaja, ldec_IporteDevengarDeBaja,
                    ldec_CuponCorridoDeBaja, ldec_ValorEmisionDeBaja, ldec_EntradaSalidaCaja, ldec_NetoSubastado, ldec_TotalNetoBaja,
                    ldec_Diferencia, ldec_Capital, ldec_InteresDevengado, ldec_ImpRenta, ldec_Descuento, ldec_TotalColocado,
                    lstr_UsrCreacion);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    lstr_ResCreacion = "Código: " + str_CodResultado + ". Mensaje: " + str_Mensaje;
                }
            }
            catch (Exception ex)
            {
                lstr_ResCreacion = ex.ToString();
            }
            return lstr_ResCreacion;
        } 
    }
}