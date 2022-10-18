using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos.Mantenimiento;
using System.Data;
using log4net;
using log4net.Config;

namespace LogicaNegocio.Mantenimiento
{
    public class clsTiposCambio
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }

        private DateTime ldt_FchReferencia;
        public DateTime Ldt_FchReferencia
        {
            get { return ldt_FchReferencia; }
            set { ldt_FchReferencia = value; }
        }


        private string lstr_TipoTransaccion;
        public string Lstr_TipoTransaccion
        {
            get { return lstr_TipoTransaccion; }
            set { lstr_TipoTransaccion = value; }
        }

        private decimal ldec_Valor;
        public decimal Ldec_Valor
        {
            get { return ldec_Valor; }
            set { ldec_Valor = value; }
        }

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public string[] CargarTiposCambio(string ldt_FchInicio)
        {
            cr.fi.bccr.indicadoreseconomicos.wsindicadoreseconomicos wsIndicadores = new cr.fi.bccr.indicadoreseconomicos.wsindicadoreseconomicos();
            wsSG.wsSistemaGestor wsSistGest = new wsSG.wsSistemaGestor();
            string lstr_FchInicio = "";            
            string[] respuesta = new string[2];
            DataTable ldat_Indicadores = new DataTable();

            //Consultar las monedas con el respectivo código para el banco central
            DataSet ldat_ISOconBCCR = new DataSet();            
            clsConsultarISOconBCCR cr_Procedimiento = new clsConsultarISOconBCCR();
            ldat_ISOconBCCR.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
            ldat_ISOconBCCR.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));

            //Inicia ciclo para recorrer los tipos de cambio desde una fecha de inicio a una de final
            try
            {
                foreach (DataRow dr_IsoBccr in ldat_ISOconBCCR.Tables[0].Rows)
                {
                    int index = ldat_ISOconBCCR.Tables[0].Rows.IndexOf(dr_IsoBccr);
                    if (ldat_ISOconBCCR.Tables[0].Rows[index]["IdMonedaISO"].ToString() != "USD")
                    {
                        if (ldt_FchInicio == "")
                        {
                            lstr_FchInicio = Convert.ToDateTime(wsSistGest.uwsConsultarTiposCambio(
                                ldat_ISOconBCCR.Tables[0].Rows[index]["IdMonedaISO"].ToString(),
                                Convert.ToDateTime("01/01/1900"),
                                null, "N").Tables[0].Rows[0]["FchReferencia"].ToString()).ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            lstr_FchInicio = ldt_FchInicio;
                        }

                        ldat_Indicadores = wsIndicadores.ObtenerIndicadoresEconomicos(
                            ldat_ISOconBCCR.Tables[0].Rows[index]["TransBCCR"].ToString(),
                            lstr_FchInicio,
                            DateTime.Today.ToString("dd/MM/yyyy"),
                            "MH_DESARROLLO", "N", "AmbDesarrolloNICSP@hotmail.com", "DCALHSRDSA").Tables[0];

                        foreach (DataRow dr_TipoCambio in ldat_Indicadores.Rows)
                        {
                            clsCrearTipoCambio ltc_tipoCambio = new clsCrearTipoCambio(
                                ldat_ISOconBCCR.Tables[0].Rows[index]["IdMonedaISO"].ToString(),
                                Convert.ToDateTime(dr_TipoCambio["DES_FECHA"].ToString()),
                                ldat_ISOconBCCR.Tables[0].Rows[index]["TransBCCR"].ToString(),
                                Convert.ToDecimal(dr_TipoCambio["NUM_VALOR"].ToString().Equals("") ? "0.0" : dr_TipoCambio["NUM_VALOR"].ToString()), "SG");
                        }
                    }
                }
                respuesta[0] = "00";
                respuesta[1] = "Tipos de cambio actualizados";
            }
            catch (Exception ex)
            {
                respuesta[0] = "99";
                respuesta[1] = ex.ToString();
            }
            return respuesta;
        }

        public string[] ActualizarTiposCambio()
        {
            cr.fi.bccr.indicadoreseconomicos.wsindicadoreseconomicos wsIndicadores = new cr.fi.bccr.indicadoreseconomicos.wsindicadoreseconomicos();
            wsSG.wsSistemaGestor wsSistGest = new wsSG.wsSistemaGestor();
            string[] respuesta = new string[2];
            DataTable ldat_Indicadores = new DataTable();
            DataSet ldat_TipoCambio = new DataSet();

            //Consultar las monedas con el respectivo código para el banco central
            DataSet ldat_ISOconBCCR = new DataSet();
            clsConsultarISOconBCCR cr_Procedimiento = new clsConsultarISOconBCCR();
            ldat_ISOconBCCR.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
            ldat_ISOconBCCR.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));

            //Inicia ciclo para recorrer los tipos de cambio desde una fecha de inicio a una de final
            try
            {
                foreach (DataRow dr_IsoBccr in ldat_ISOconBCCR.Tables[0].Rows)
                {
                    int index = ldat_ISOconBCCR.Tables[0].Rows.IndexOf(dr_IsoBccr);
                    ldat_Indicadores = wsIndicadores.ObtenerIndicadoresEconomicos(
                        ldat_ISOconBCCR.Tables[0].Rows[index]["TransBCCR"].ToString(),
                        DateTime.Today.ToString("dd/MM/yyyy"),
                        DateTime.Today.ToString("dd/MM/yyyy"),
                        "MH_DESARROLLO", "N", "AmbDesarrolloNICSP@hotmail.com", "DCALHSRDSA").Tables[0];
                    
                    clsConsultarTiposCambio ltc_ConsultarTpoCambio = new clsConsultarTiposCambio(
                        ldat_ISOconBCCR.Tables[0].Rows[0]["IdMonedaISO"].ToString(),
                        DateTime.Today, null);
                    ldat_TipoCambio.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(ltc_ConsultarTpoCambio.Lstr_RespuestaSchema)));
                    ldat_TipoCambio.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(ltc_ConsultarTpoCambio.Lstr_RespuestaXML)));

                    foreach (DataRow dr_TipoCambio in ldat_Indicadores.Rows)
                    {
                        clsModificarTipoCambio ltc_tipoCambio = new clsModificarTipoCambio(
                            ldat_ISOconBCCR.Tables[0].Rows[index]["IdMonedaISO"].ToString(),
                            Convert.ToDateTime(dr_TipoCambio["DES_FECHA"].ToString()),
                            ldat_ISOconBCCR.Tables[0].Rows[index]["TransBCCR"].ToString(),
                            Convert.ToDecimal(dr_TipoCambio["NUM_VALOR"].ToString().Equals("") ? "0.0" : dr_TipoCambio["NUM_VALOR"].ToString()), "SG",
                            Convert.ToDateTime(ldat_TipoCambio.Tables[0].Rows[0]["FchModifica"].ToString()));
                    }
                    respuesta[0] = "00";
                    respuesta[1] = "Tipos de cambio actualizados";
                }
            }
            catch (Exception ex)
            {
                respuesta[0] = "99";
                respuesta[1] = ex.ToString();
            }
            return respuesta;
        }

        public DataSet ConsultarTiposCambio(string str_IdMoneda, DateTime? dt_FchReferencia, string str_TipoTransaccion, string str_ExactaFecha = "N")
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarTiposCambio cr_Procedimiento = new clsConsultarTiposCambio(str_IdMoneda, dt_FchReferencia, str_TipoTransaccion, str_ExactaFecha);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearTipoCambio(string str_IdMoneda, DateTime dt_FchReferencia, string str_TipoTransaccion, decimal dec_Valor, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearTipoCambio cls_ProcCrearTipoCambio = new clsCrearTipoCambio(str_IdMoneda, dt_FchReferencia, str_TipoTransaccion, dec_Valor, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearTipoCambio.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearTipoCambio.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResCreacion;
        }

        public bool ModificarTipoCambio(string str_IdMoneda, DateTime dt_FchReferencia, string str_TipoTransaccion, decimal dec_Valor, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarTipoCambio cls_ProcModificarTipoCambio = new clsModificarTipoCambio(str_IdMoneda, dt_FchReferencia, str_TipoTransaccion, dec_Valor, str_UsrModifica, dt_FchModifica);
                str_CodResultado = cls_ProcModificarTipoCambio.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcModificarTipoCambio.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResCreacion;
        }

        public clsTiposCambio()
        { }
    }
}