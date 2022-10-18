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
    public class clsTituloValor
    {
        #region Variables

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        #endregion

        #region Métodos
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lint_NumValor"></param>
        /// <param name="lint_NumCupon"></param>
        /// <param name="lstr_TipoFecha"></param>
        /// <param name="ldt_FchInicio"></param>
        /// <param name="ldt_FchFin"></param>
        /// <returns></returns>
        public DataSet ConsultarTituloValor(int? lint_NumValor, string lstr_Nemotecnico, string lint_NumCupon, string lstr_Garantia, string lstr_IndicadorCupon, string lstr_Tipo, string lstr_TipoNegociacion, string lstr_EstadoValor, DateTime ldt_FchInicio, DateTime ldt_FchFin, string lstr_NroEmisionSerie)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaTituloValor cr_Procedimiento = new clsConsultaTituloValor(lint_NumValor, lstr_Nemotecnico, lint_NumCupon, lstr_Garantia, lstr_IndicadorCupon, lstr_Tipo, lstr_TipoNegociacion, lstr_EstadoValor, ldt_FchInicio, ldt_FchFin, lstr_NroEmisionSerie);
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

        /// </summary>
        /// <param name="lint_NumValor"></param>
        /// <param name="lint_NumCupon"></param>
        /// <param name="lstr_TipoFecha"></param>
        /// <param name="ldt_FchInicio"></param>
        /// <param name="ldt_FchFin"></param>
        /// <returns></returns>
        public DataSet ConsultarTituloValorValores(string lint_NumValor, string lstr_Nemotecnico, string lint_NumCupon, string lstr_Garantia, string lstr_Tipo, string ldt_FchInicio, string ldt_FchFin)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaTituloValorValores cr_Procedimiento = new clsConsultaTituloValorValores(lint_NumValor, lstr_Nemotecnico, lint_NumCupon, lstr_Garantia, lstr_Tipo, ldt_FchInicio, ldt_FchFin);
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

        public string AnularTituloValor(int lint_NumValor, string lstr_Nemotecnico, string lstr_EstadoValor,
            string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            string lstr_ResCreacion = "";
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsAnularTituloValor cr_Procedimiento = new clsAnularTituloValor(lint_NumValor, lstr_Nemotecnico, lstr_EstadoValor,
                     lstr_UsrModifica, ldt_FchModifica);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lint_NumValor"></param>
        /// <param name="lstr_Nemotecnico"></param>
        /// <param name="lstr_Tipo"></param>
        /// <param name="ldt_FchInicio"></param>
        /// <param name="ldt_FchFin"></param>
        /// <returns></returns>
        public DataSet ConsultarTituloValorMant(string lint_NumValor, string lstr_Nemotecnico, string lstr_Tipo, DateTime? ldt_FchInicio, DateTime? ldt_FchFin, string str_ExactaFecha = null)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaTituloValorMant cr_Procedimiento = new clsConsultaTituloValorMant(lint_NumValor, lstr_Nemotecnico, lstr_Tipo, ldt_FchInicio, ldt_FchFin, str_ExactaFecha);
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
        /// <param name="lint_NumValor">-</param>
        /// <param name="lint_NumCupon">-</param>
        /// <param name="lstr_EstadoValor">-</param>
        /// <param name="lstr_Nemotecnico">-</param>
        /// <param name="lstr_Tipo">-</param>
        /// <param name="lstr_TipoNegociacion">-</param>
        /// <param name="lstr_Moneda">-</param>
        /// <param name="ldec_ValorFacial">-</param>
        /// <param name="ldt_FchValor">-</param>
        /// <param name="lstr_PlazoValor">-</param>
        /// <param name="ldt_FchCancelacion">-</param>
        /// <param name="ldt_FchVencimiento">-</param>
        /// <param name="ldt_FchConstitucion">-</param>
        /// <param name="ldec_ValorTransadoBruto">-</param>
        /// <param name="ldec_ValorTransadoNeto">-</param>
        /// <param name="ldec_TasaBruta">-</param>
        /// <param name="ldec_TasaNeta">-</param>
        /// <param name="ldec_Margen">-</param>
        /// <param name="lint_NumEmisionSerie">-</param>
        /// <param name="ldt_FchCreacionT">-</param>
        /// <param name="ldt_FchInicio">-</param>
        /// <param name="lstr_Propietario">-</param>
        /// <param name="lstr_EntidadCustodia">-</param>
        /// <param name="lstr_SistemaNegociacion">-</param>
        /// <param name="lstr_MotivoAnulacion">-</param>
        /// <param name="ldec_RendimientoPorDescuento">-</param>
        /// <param name="ldec_InteresBruto">-</param>
        /// <param name="ldec_InteresBrutoEfectivo">-</param>
        /// <param name="ldec_InteresNeto">-</param>
        /// <param name="ldec_InteresNetoAcumulado">-</param>
        /// <param name="ldec_ImpuestoVencido">-</param>
        /// <param name="ldec_ImpuestoEfectivo">-</param>
        /// <param name="ldec_ImpuestoPagado">-</param>
        /// <param name="ldec_Premio">-</param>
        /// <param name="lstr_ModuloSINPE">-</param>
        /// <param name="lstr_IndicadorGarantia">-</param>
        /// <param name="lstr_IndicadorCupon">-</param>
        /// <param name="lstr_Origen">-</param>
        /// <param name="lstr_Estado">-</param>
        /// <param name="lstr_UsrCreacion">-</param>
        /// <param name="str_CodResultado">-</param>
        /// <param name="str_Mensaje">-</param>
        /// <returns></returns>
        public string CrearTituloValor(int lint_NumValor, int lint_NumCupon, string lstr_EstadoValor, string lstr_Nemotecnico, string lstr_Tipo, string lstr_TipoNegociacion,
            string lstr_Moneda, decimal ldec_ValorFacial, DateTime ldt_FchValor, string lstr_PlazoValor, DateTime ldt_FchCancelacion,
            DateTime ldt_FchVencimiento, DateTime ldt_FchConstitucion, decimal ldec_ValorTransadoBruto, decimal ldec_ValorTransadoNeto,
            decimal ldec_TasaBruta, decimal ldec_TasaNeta, decimal ldec_Margen, string lint_NumEmisionSerie, DateTime ldt_FchCreacionT, DateTime ldt_FchInicio,
            string lstr_Propietario, string lstr_EntidadCustodia, string lstr_SistemaNegociacion, string lstr_MotivoAnulacion, decimal ldec_RendimientoPorDescuento,
            decimal ldec_InteresBruto, decimal ldec_InteresBrutoEfectivo, decimal ldec_InteresNeto, decimal ldec_InteresNetoAcumulado,
            decimal ldec_ImpuestoVencido, decimal ldec_ImpuestoEfectivo, decimal ldec_ImpuestoPagado, decimal ldec_Premio, string lstr_ModuloSINPE,
            string lstr_IndicadorGarantia, string lstr_IndicadorCupon, string lstr_Origen, string lstr_Estado, string lstr_UsrCreacion,
            string lstr_DescripcionNegociacion, string lstr_NumeroIdentificacion, string lstr_TipoIdentificacion, out string str_CodResultado, out string str_Mensaje)
        {
            string lstr_ResCreacion = "";
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;

            decimal MontoUdesColones = 0;
            decimal TipoCambioUdes = 0;

            //Mantenimiento.clsTiposCambio TpoCambio = new Mantenimiento.clsTiposCambio();

            //TipoCambioUdes = Convert.ToDecimal(TpoCambio.ConsultarTiposCambio("UDE", DateTime.Today, "", "N").Tables[0].Rows[0]["Valor"].ToString());

            try
            {
                //línea para calcular valor de UDEs
                //if (lstr_Moneda == "UDE")
                //{
                //    MontoUdesColones = 0;
                //}
                //else
                //{
                    
                //}
                 Mantenimiento.clsDinamico dinamico = new Mantenimiento.clsDinamico();
                 dinamico.ConsultarDinamico("delete from cf.titulosvalores where nrovalor = " + lint_NumValor + " and nemotecnico = '" + lstr_Nemotecnico + "' and nrocupon = " + lint_NumCupon + " and modulosinpe = '" + lstr_ModuloSINPE + "'");
                clsCreaTituloValor cr_Procedimiento = new clsCreaTituloValor(lint_NumValor, lint_NumCupon,
                    lstr_EstadoValor, lstr_Nemotecnico, lstr_Tipo, lstr_TipoNegociacion, lstr_Moneda, ldec_ValorFacial,
                    ldt_FchValor, lstr_PlazoValor, ldt_FchCancelacion, ldt_FchVencimiento, ldt_FchConstitucion,
                    ldec_ValorTransadoBruto, ldec_ValorTransadoNeto, ldec_TasaBruta, ldec_TasaNeta, ldec_Margen,
                    lint_NumEmisionSerie, ldt_FchCreacionT, ldt_FchInicio, lstr_Propietario, lstr_EntidadCustodia,
                    lstr_SistemaNegociacion, lstr_MotivoAnulacion, ldec_RendimientoPorDescuento, ldec_InteresBruto,
                    ldec_InteresBrutoEfectivo, ldec_InteresNeto, ldec_InteresNetoAcumulado, ldec_ImpuestoVencido,
                    ldec_ImpuestoEfectivo, ldec_ImpuestoPagado, ldec_Premio, lstr_ModuloSINPE, lstr_IndicadorGarantia,
                    lstr_IndicadorCupon, lstr_Origen, lstr_Estado, lstr_UsrCreacion,lstr_DescripcionNegociacion,lstr_NumeroIdentificacion,lstr_TipoIdentificacion);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    lstr_ResCreacion = "Código: " + str_CodResultado + ". Mensaje: " + str_Mensaje;
                }
                else
                    throw new Exception(str_CodResultado + " / " + str_Mensaje);
            }
            catch (Exception ex)
            {
                lstr_ResCreacion = "Código: 99. Mensaje: " + ex.Message;
            }
            return lstr_ResCreacion;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lint_NumValor">-</param>
        /// <param name="lint_NumCupon">-</param>
        /// <param name="lstr_EstadoValor">-</param>
        /// <param name="lstr_Nemotecnico">-</param>
        /// <param name="lstr_Tipo">-</param>
        /// <param name="lstr_TipoNegociacion">-</param>
        /// <param name="lstr_Moneda">-</param>
        /// <param name="ldec_ValorFacial">-</param>
        /// <param name="ldt_FchValor">-</param>
        /// <param name="lstr_PlazoValor">-</param>
        /// <param name="ldt_FchCancelacion">-</param>
        /// <param name="ldt_FchVencimiento">-</param>
        /// <param name="ldt_FchConstitucion">-</param>
        /// <param name="ldec_ValorTransadoBruto">-</param>
        /// <param name="ldec_ValorTransadoNeto">-</param>
        /// <param name="ldec_TasaBruta">-</param>
        /// <param name="ldec_TasaNeta">-</param>
        /// <param name="ldec_Margen">-</param>
        /// <param name="lint_NumEmisionSerie">-</param>
        /// <param name="ldt_FchCreacionT">-</param>
        /// <param name="ldt_FchInicio">-</param>
        /// <param name="lstr_Propietario">-</param>
        /// <param name="lstr_EntidadCustodia">-</param>
        /// <param name="lstr_SistemaNegociacion">-</param>
        /// <param name="lstr_MotivoAnulacion">-</param>
        /// <param name="ldec_RendimientoPorDescuento">-</param>
        /// <param name="ldec_InteresBruto">-</param>
        /// <param name="ldec_InteresBrutoEfectivo">-</param>
        /// <param name="ldec_InteresNeto">-</param>
        /// <param name="ldec_InteresNetoAcumulado">-</param>
        /// <param name="ldec_ImpuestoVencido">-</param>
        /// <param name="ldec_ImpuestoEfectivo">-</param>
        /// <param name="ldec_ImpuestoPagado">-</param>
        /// <param name="ldec_Premio">-</param>
        /// <param name="lstr_ModuloSINPE">-</param>
        /// <param name="lstr_IndicadorGarantia">-</param>
        /// <param name="lstr_IndicadorCupon">-</param>
        /// <param name="lstr_Origen">-</param>
        /// <param name="lstr_UsrModifica">-</param>
        /// <param name="ldt_FchModifica">-</param>
        /// <param name="str_CodResultado">-</param>
        /// <param name="str_Mensaje">-</param>
        /// <returns></returns>
        public string ModificarTituloValor(int lint_NumValor, int lint_NumCupon, string lstr_EstadoValor, string lstr_Nemotecnico, string lstr_Tipo, string lstr_TipoNegociacion,
            string lstr_Moneda, decimal ldec_ValorFacial, DateTime ldt_FchValor, string lstr_PlazoValor, DateTime ldt_FchCancelacion,
            DateTime ldt_FchVencimiento, DateTime ldt_FchConstitucion, decimal ldec_ValorTransadoBruto, decimal ldec_ValorTransadoNeto,
            decimal ldec_TasaBruta, decimal ldec_TasaNeta, decimal ldec_Margen, string lint_NumEmisionSerie, DateTime ldt_FchCreacionT, DateTime ldt_FchInicio,
            string lstr_Propietario, string lstr_EntidadCustodia, string lstr_SistemaNegociacion, string lstr_MotivoAnulacion, decimal ldec_RendimientoPorDescuento,
            decimal ldec_InteresBruto, decimal ldec_InteresBrutoEfectivo, decimal ldec_InteresNeto, decimal ldec_InteresNetoAcumulado,
            decimal ldec_ImpuestoVencido, decimal ldec_ImpuestoEfectivo, decimal ldec_ImpuestoPagado, decimal ldec_Premio, string lstr_ModuloSINPE,
            string lstr_IndicadorGarantia, string lstr_IndicadorCupon, string lstr_Origen, string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            string lstr_ResCreacion = "";
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificaTituloValor cr_Procedimiento = new clsModificaTituloValor(lint_NumValor, lint_NumCupon,
                    lstr_EstadoValor, lstr_Nemotecnico, lstr_Tipo, lstr_TipoNegociacion, lstr_Moneda, ldec_ValorFacial,
                    ldt_FchValor, lstr_PlazoValor, ldt_FchCancelacion, ldt_FchVencimiento, ldt_FchConstitucion,
                    ldec_ValorTransadoBruto, ldec_ValorTransadoNeto, ldec_TasaBruta, ldec_TasaNeta, ldec_Margen,
                    lint_NumEmisionSerie, ldt_FchCreacionT, ldt_FchInicio, lstr_Propietario, lstr_EntidadCustodia,
                    lstr_SistemaNegociacion, lstr_MotivoAnulacion, ldec_RendimientoPorDescuento, ldec_InteresBruto,
                    ldec_InteresBrutoEfectivo, ldec_InteresNeto, ldec_InteresNetoAcumulado, ldec_ImpuestoVencido,
                    ldec_ImpuestoEfectivo, ldec_ImpuestoPagado, ldec_Premio, lstr_ModuloSINPE, lstr_IndicadorGarantia,
                    lstr_IndicadorCupon, lstr_Origen, lstr_UsrModifica, ldt_FchModifica);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lint_NumValor"></param>
        /// <param name="lstr_Nemotecnico"></param>
        /// <param name="lstr_TasaVariable"></param>
        /// <param name="ldec_TasaVariableValor"></param>
        /// <param name="ldec_Margen"></param>
        /// <param name="lstr_UsrModifica"></param>
        /// <param name="ldt_FchModifica"></param>
        /// <param name="str_CodResultado"></param>
        /// <param name="str_Mensaje"></param>
        /// <returns></returns>
        public string ModificarTituloValorMant(int lint_NumValor, string lstr_Nemotecnico, string lstr_TasaVariable, 
            decimal ldec_TasaVariableValor, decimal ldec_Margen, string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            string lstr_ResCreacion = "";
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificaTituloValorMant cr_Procedimiento = new clsModificaTituloValorMant(lint_NumValor, 
                    lstr_Nemotecnico, lstr_TasaVariable, ldec_TasaVariableValor, ldec_Margen, 
                    lstr_UsrModifica, ldt_FchModifica);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    lstr_ResCreacion = "Código: " + str_CodResultado + ". Mensaje: " + str_Mensaje;
                }
                else
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lint_NumValor"></param>
        /// <param name="lstr_Nemotecnico"></param>
        /// <param name="lstr_EstadoValor"></param>
        /// <param name="lstr_IndicadorGarantia"></param>
        /// <param name="lstr_UsrModifica"></param>
        /// <param name="ldt_FchModifica"></param>
        /// <param name="str_CodResultado"></param>
        /// <param name="str_Mensaje"></param>
        /// <returns></returns>
        public string EliminarTituloGarantia(int lint_NumValor, string lstr_Nemotecnico, string lstr_Usuario, out string str_CodResultado, out string str_Mensaje)
        {
            string lstr_ResCreacion = "";
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsEliminaTituloGarantia cr_Procedimiento = new clsEliminaTituloGarantia(lint_NumValor, lstr_Nemotecnico, lstr_Usuario);
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

        public string ModificarTituloGarantia(int lint_NumValor, string lstr_Nemotecnico, string lstr_EstadoValor, string lstr_IndicadorGarantia,
            string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            string lstr_ResCreacion = "";
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificaTituloGarantia cr_Procedimiento = new clsModificaTituloGarantia(lint_NumValor, lstr_Nemotecnico, lstr_EstadoValor, lstr_IndicadorGarantia,
                     lstr_UsrModifica, ldt_FchModifica);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_Tipo"></param>
        /// <param name="lstr_EstadoValor"></param>
        /// <param name="ldt_FchValor"></param>
        /// <param name="ldt_FchVencimiento"></param>
        /// <param name="lstr_Propietario"></param>
        /// <param name="lstr_Plazo"></param>
        /// <param name="lstr_Nemotecnico"></param>
        /// <param name="ldec_ValorFacial"></param>
        /// <param name="ldec_ValorTransadoBruto"></param>
        

        //public string GenerarAsientoAjuste()
        //{
        //    //Coleccion de asientos y tipos de asientos requeridos en SAP expuestos por la referencia del servicio
        //    wsAsientos.ZfiAsiento item_asiento = new wsAsientos.ZfiAsiento();
        //    wsAsientos.ZfiAsiento[] tabla_asientos = new wsAsientos.ZfiAsiento[gdat_Asiento.Rows.Count];
        //    //gdat_asientos es el arreglo en el cual guardo todo el asiento generado para contabilizarlo

        //    //variables de proceso
        //    string[] item_resAsientosLog = new string[10];
        //    string logAsiento = string.Empty;
        //    string flagEstadoAsiento = string.Empty;

        //    DateTime ldt_FchContabilizacion = Convert.ToDateTime(txtFecha.Text);
        //    string lstr_Moneda = dbMoneda.SelectedValue;
        //    string lstr_Referencia = txtReferencia.Text;

        //    try
        //    {
        //        for (int i = 0; i < gdat_Asiento.Rows.Count; i++)
        //        {
        //            string lstr_DebeHaber = string.Empty;

        //            if (gdat_Asiento.Rows[i]["ClaveContable"].ToString() == "40")
        //            {
        //                lstr_DebeHaber = "Debe";
        //            }
        //            else
        //            {
        //                lstr_DebeHaber = "Haber";
        //            }

        //            item_asiento = new wsAsientos.ZfiAsiento();

        //            if (i == 0)
        //            {
        //                item_asiento.Blart = "ED";//Clase de documento
        //                item_asiento.Bukrs = "G206";//Sociedad
        //                item_asiento.Werks = lstr_Referencia;
        //                item_asiento.Bldat = ldt_FchContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
        //                item_asiento.Budat = ldt_FchContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización
        //            }
        //            item_asiento.Waers = lstr_Moneda;//Moneda 
        //            item_asiento.Bschl = gdat_Asiento.Rows[i]["ClaveContable"].ToString();//Clave de contabilización
        //            item_asiento.Hkont = gdat_Asiento.Rows[i]["Cuenta"].ToString();//Cuenta de mayor
        //            item_asiento.Wrbtr = Convert.ToDecimal(gdat_Asiento.Rows[i][lstr_DebeHaber].ToString());//Importe
        //            item_asiento.Sgtxt = gdat_Asiento.Rows[i]["TextoInfo"].ToString();//Texto Informativo (50 caracteres)
        //            item_asiento.Kostl = gdat_Asiento.Rows[i]["CentroCosto"].ToString();//Centro de Costo
        //            item_asiento.Prctr = gdat_Asiento.Rows[i]["CentroBeneficio"].ToString();//Centro de Beneficio
        //            item_asiento.Projk = gdat_Asiento.Rows[i]["ElementoPEP"].ToString();//Elemento PEP
        //            item_asiento.Xref2Hd = gdat_Asiento.Rows[i]["PosPre"].ToString();//Posición Presupuestaria
        //            item_asiento.Fistl = gdat_Asiento.Rows[i]["CentroGestor"].ToString();//Centro Gestor
        //            item_asiento.Geber = gdat_Asiento.Rows[i]["Fondo"].ToString();//Fondo
        //            item_asiento.Kblnr = gdat_Asiento.Rows[i]["DocPres"].ToString();//Documento Presupuestario
        //            item_asiento.Kblpos = gdat_Asiento.Rows[i]["PosDocPres"].ToString();//Posición de documento presupuestario

        //            tabla_asientos[i] = item_asiento;
        //        }

        //        //Cargar de Asientos 
        //        string[] concatenado = new string[8];
        //        //envio de asiento mediante servicio web hacia SIGAF
        //        item_resAsientosLog = asientos.EnviarAsientos(tabla_asientos);
        //        for (int j = 0; j < item_resAsientosLog.Length; j++)
        //        {
        //            int x = j + 1;
        //            logAsiento += x + " - " + item_resAsientosLog[j] + " - ";
        //        }
        //        //Registrar en Bitacora de movimientos
        //        ws_SGService.uwsRegistrarAccionBitacora("DE", "", "", "Resultado de Contabilización: " + logAsiento);
        //        return logAsiento;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.ToString();
        //    }
        //}

        #endregion

        #region Constructor

        public clsTituloValor()
        { }

        #endregion
    }
}