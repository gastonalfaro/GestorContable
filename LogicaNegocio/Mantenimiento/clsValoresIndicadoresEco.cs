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
    public class clsValoresIndicadoresEco
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdIndicadorEco;
        public string Lstr_IdIndicadorEco
        {
            get { return lstr_IdIndicadorEco; }
            set { lstr_IdIndicadorEco = value; }
        }

        private DateTime ldt_FchReferencia;
        public DateTime Ldt_FchReferencia
        {
            get { return ldt_FchReferencia; }
            set { ldt_FchReferencia = value; }
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

        public string[] CargarIndicadoresEco(string ldt_FchInicio)
        {
            cr.fi.bccr.indicadoreseconomicos.wsindicadoreseconomicos wsIndicadores = new cr.fi.bccr.indicadoreseconomicos.wsindicadoreseconomicos();
            wsSG.wsSistemaGestor wsSistGest = new wsSG.wsSistemaGestor();
            string lstr_FchInicio = "";
            string[] respuesta = new string[2];
            DataTable ldat_Indicadores = new DataTable();

            //Consultar los indicadores económicos con el respectivo código para el banco central
            DataSet ldat_ISOconBCCR = new DataSet();
            clsConsultarIndicadoresEconomicos cr_Procedimiento = new clsConsultarIndicadoresEconomicos(null, null, null);
            ldat_ISOconBCCR.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
            ldat_ISOconBCCR.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));

            //Inicia ciclo para recorrer los indicadores económicos desde una fecha de inicio a una de final
            try
            {
                foreach (DataRow dr_IsoBccr in ldat_ISOconBCCR.Tables[0].Rows)
                {
                    int index = ldat_ISOconBCCR.Tables[0].Rows.IndexOf(dr_IsoBccr);

                    if (ldt_FchInicio == "")
                    {
                        lstr_FchInicio = Convert.ToDateTime(wsSistGest.uwsConsultarValoresIndicadoresEco(
                            ldat_ISOconBCCR.Tables[0].Rows[index]["IdIndicadorEco"].ToString().Trim(),
                            Convert.ToDateTime("01/01/1900"),
                            "N").Tables[0].Rows[0]["FchReferencia"].ToString()).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        lstr_FchInicio = ldt_FchInicio;
                    }

                    ldat_Indicadores = wsIndicadores.ObtenerIndicadoresEconomicos(
                        ldat_ISOconBCCR.Tables[0].Rows[index]["Transaccion"].ToString(),
                        lstr_FchInicio,
                        DateTime.Today.ToString("dd/MM/yyyy"),
                        "MH_DESARROLLO", "N", "AmbDesarrolloNICSP@hotmail.com", "DCALHSRDSA").Tables[0];

                    foreach (DataRow dr_TipoCambio in ldat_Indicadores.Rows)
                    {
                        clsCrearValorIndicadorEco ltc_indicadorEco = new clsCrearValorIndicadorEco(
                            ldat_ISOconBCCR.Tables[0].Rows[index]["IdIndicadorEco"].ToString().Trim(),
                            Convert.ToDateTime(dr_TipoCambio["DES_FECHA"].ToString()),
                            Convert.ToDecimal(dr_TipoCambio["NUM_VALOR"].ToString().Equals("") ? "0.0" : dr_TipoCambio["NUM_VALOR"].ToString()), "SG");
                    }
                }
                respuesta[0] = "00";
                respuesta[1] = "Indicadores Económicos creados";
            }
            catch (Exception ex)
            {
                respuesta[0] = "99";
                respuesta[1] = ex.ToString();
            }
            return respuesta;
        }

        public string[] ActualizarIndicadoresEco()
        {
            cr.fi.bccr.indicadoreseconomicos.wsindicadoreseconomicos wsIndicadores = new cr.fi.bccr.indicadoreseconomicos.wsindicadoreseconomicos();
            wsSG.wsSistemaGestor wsSistGest = new wsSG.wsSistemaGestor();
            string[] respuesta = new string[2];
            DataTable ldat_Indicadores = new DataTable();
            DataSet ldat_TipoCambio = new DataSet();

            //Consultar las monedas con el respectivo código para el banco central
            DataSet ldat_ISOconBCCR = new DataSet();
            clsConsultarIndicadoresEconomicos cr_Procedimiento = new clsConsultarIndicadoresEconomicos(null, null, null);
            ldat_ISOconBCCR.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
            ldat_ISOconBCCR.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));

            //Inicia ciclo para recorrer los tipos de cambio desde una fecha de inicio a una de final
            try
            {
                foreach (DataRow dr_IsoBccr in ldat_ISOconBCCR.Tables[0].Rows)
                {
                    int index = ldat_ISOconBCCR.Tables[0].Rows.IndexOf(dr_IsoBccr);
                    ldat_Indicadores = wsIndicadores.ObtenerIndicadoresEconomicos(
                        ldat_ISOconBCCR.Tables[0].Rows[index]["Transaccion"].ToString(),
                        DateTime.Today.ToString("dd/MM/yyyy"),
                        DateTime.Today.ToString("dd/MM/yyyy"),
                        "MH_DESARROLLO", "N", "AmbDesarrolloNICSP@hotmail.com", "DCALHSRDSA").Tables[0];

                    clsConsultarValoresIndicadoresEco ltc_ConsultarValoresIndicadoresEco = new clsConsultarValoresIndicadoresEco(
                        ldat_ISOconBCCR.Tables[0].Rows[0]["IdIndicadorEco"].ToString(),
                        DateTime.Today, null);
                    ldat_TipoCambio.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(ltc_ConsultarValoresIndicadoresEco.Lstr_RespuestaSchema)));
                    ldat_TipoCambio.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(ltc_ConsultarValoresIndicadoresEco.Lstr_RespuestaXML)));

                    foreach (DataRow dr_TipoCambio in ldat_Indicadores.Rows)
                    {
                        clsModificarValorIndicadorEco ltc_tipoCambio = new clsModificarValorIndicadorEco(
                            ldat_ISOconBCCR.Tables[0].Rows[index]["IdIndicadorEco"].ToString(),
                            Convert.ToDateTime(dr_TipoCambio["DES_FECHA"].ToString()),
                            Convert.ToDecimal(dr_TipoCambio["NUM_VALOR"].ToString().Equals("") ? "0.0" : dr_TipoCambio["NUM_VALOR"].ToString()), "SG",
                            Convert.ToDateTime(ldat_TipoCambio.Tables[0].Rows[0]["FchModifica"].ToString()));
                    }
                    respuesta[0] = "00";
                    respuesta[1] = "Indicadores Económicos actualizados";
                }
            }
            catch (Exception ex)
            {
                respuesta[0] = "99";
                respuesta[1] = ex.ToString();
            }
            return respuesta;
        }

        public DataSet ConsultarValoresIndicadoresEco(string str_IdIndicadorEco, DateTime dt_FchReferencia, string str_ExactaFecha = "S")
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarValoresIndicadoresEco cr_Procedimiento = new clsConsultarValoresIndicadoresEco(str_IdIndicadorEco, dt_FchReferencia, str_ExactaFecha);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch (Exception ex)
            { Log.Error(ex.Message.ToString()); }
            return lds_TablasConsulta;
        }

        public bool CrearValorIndicadorEco(string str_IdIndicadorEco, DateTime dt_FchReferencia, decimal dec_Valor, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearValorIndicadorEco cls_ProcCrearValorIndicadorEco = new clsCrearValorIndicadorEco(str_IdIndicadorEco, dt_FchReferencia, dec_Valor, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearValorIndicadorEco.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearValorIndicadorEco.Lstr_MensajeRespuesta;

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

        public bool ModificarValorIndicadorEco(string str_IdIndicadorEco, DateTime dt_FchReferencia, decimal dec_Valor, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarValorIndicadorEco cls_ProcModificarValorIndicadorEco = new clsModificarValorIndicadorEco(str_IdIndicadorEco, dt_FchReferencia, dec_Valor, str_UsrModifica, dt_FchModifica);
                str_CodResultado = cls_ProcModificarValorIndicadorEco.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcModificarValorIndicadorEco.Lstr_MensajeRespuesta;

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

        public clsValoresIndicadoresEco()
        { }
    }
}