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
    public class clsTrasladoMagisterio
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
        //public DataSet ConsultarTituloValor(string lint_NumValor = null, string lint_NumCupon = null, string lstr_TipoFecha = null, string ldt_FchInicio = null, string ldt_FchFin = null)
        //{

        //    DataSet lds_TablasConsulta = new DataSet();
        //    try
        //    {
        //        clsConsultaTituloValor cr_Procedimiento = new clsConsultaTituloValor(lint_NumValor, lint_NumCupon, lstr_TipoFecha, ldt_FchInicio, ldt_FchFin);
        //        if (String.Equals(cr_Procedimiento.Lstr_CodigoResultado, "00"))
        //        {
        //            lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
        //            lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
        //        }
        //    }
        //    catch (Exception ex)
        //    { }
        //    return lds_TablasConsulta;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_EstadoValor">-</param>
        /// <param name="lstr_Nemotecnico">-</param>
        /// <param name="lstr_Tipo">-</param>
        /// <param name="lstr_TipoNegociacion">-</param>
        /// <param name="lint_NumValor">-</param>
        /// <param name="lstr_Moneda">-</param>
        /// <param name="ldec_ValorFacial">-</param>
        /// <param name="ldt_FchValor">-</param>
        /// <param name="lstr_PlazoValor">-</param>
        /// <param name="ldt_FchCancelacion">-</param>
        /// <param name="ldt_FchVencimiento">-</param>
        /// <param name="ldec_ValorTransadoBruto">-</param>
        /// <param name="ldec_ValorTransadoNeto">-</param>
        /// <param name="ldec_TasaBruta">-</param>
        /// <param name="ldec_TasaNeta">-</param>
        /// <param name="ldt_FchCreacionT">-</param>
        /// <param name="lstr_Propietario">-</param>
        /// <param name="lstr_SistemaNegociacion">-</param>
        /// <param name="lstr_MotivoAnulacion">-</param>
        /// <param name="ldec_RendimientoPorDescuento">-</param>
        /// <param name="ldec_Premio">-</param>
        /// <param name="ldec_ImpuestoPagado">-</param>
        /// <param name="lstr_Estado">-</param>
        /// <param name="lstr_UsrCreacion">-</param>
        /// <param name="str_CodResultado">-</param>
        /// <param name="str_Mensaje">-</param>
        /// <returns></returns>
        public string CrearTrasladoMagisterio(string lstr_EstadoValor, string lstr_Nemotecnico, string lstr_Tipo, string lstr_TipoNegociacion,
            int lint_NumValor, string lstr_Moneda, decimal ldec_ValorFacial, DateTime ldt_FchValor, string lstr_PlazoValor, DateTime ldt_FchCancelacion,
            DateTime ldt_FchVencimiento, decimal ldec_ValorTransadoBruto, decimal ldec_ValorTransadoNeto, decimal ldec_TasaBruta,
            decimal ldec_TasaNeta, DateTime ldt_FchCreacionT, string lstr_Propietario, string lstr_SistemaNegociacion, string lstr_MotivoAnulacion,
            decimal ldec_RendimientoPorDescuento, decimal ldec_Premio, decimal ldec_ImpuestoPagado, string lstr_Estado, string lstr_ModuloSINPE, string lstr_UsrCreacion,string lstr_EntidadCustodia,
            out string str_CodResultado, out string str_Mensaje)
        {
            string lstr_ResCreacion = "";
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaTrasladoMagisterio cr_Procedimiento = new clsCreaTrasladoMagisterio(lstr_EstadoValor, lstr_Nemotecnico, lstr_Tipo,
                    lstr_TipoNegociacion, lint_NumValor, lstr_Moneda, ldec_ValorFacial, ldt_FchValor, lstr_PlazoValor,
                    ldt_FchCancelacion, ldt_FchVencimiento, ldec_ValorTransadoBruto, ldec_ValorTransadoNeto, ldec_TasaBruta,
                    ldec_TasaNeta, ldt_FchCreacionT, lstr_Propietario, lstr_SistemaNegociacion, lstr_MotivoAnulacion,
                    ldec_RendimientoPorDescuento, ldec_Premio, ldec_ImpuestoPagado, lstr_Estado, lstr_ModuloSINPE, lstr_UsrCreacion, lstr_EntidadCustodia);

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
        //public string ModificarTituloValor(int lint_NumValor, int lint_NumCupon, string lstr_EstadoValor, string lstr_Nemotecnico, string lstr_Tipo, string lstr_TipoNegociacion,
        //    string lstr_Moneda, decimal ldec_ValorFacial, DateTime ldt_FchValor, string lstr_PlazoValor, DateTime ldt_FchCancelacion,
        //    DateTime ldt_FchVencimiento, DateTime ldt_FchConstitucion, decimal ldec_ValorTransadoBruto, decimal ldec_ValorTransadoNeto,
        //    decimal ldec_TasaBruta, decimal ldec_TasaNeta, decimal ldec_Margen, string lint_NumEmisionSerie, DateTime ldt_FchCreacionT, DateTime ldt_FchInicio,
        //    string lstr_Propietario, string lstr_EntidadCustodia, string lstr_SistemaNegociacion, string lstr_MotivoAnulacion, decimal ldec_RendimientoPorDescuento,
        //    decimal ldec_InteresBruto, decimal ldec_InteresBrutoEfectivo, decimal ldec_InteresNeto, decimal ldec_InteresNetoAcumulado,
        //    decimal ldec_ImpuestoVencido, decimal ldec_ImpuestoEfectivo, decimal ldec_ImpuestoPagado, decimal ldec_Premio, string lstr_ModuloSINPE,
        //    string lstr_IndicadorGarantia, string lstr_IndicadorCupon, string lstr_Origen, string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        //{
        //    string lstr_ResCreacion = "";
        //    str_CodResultado = String.Empty;
        //    str_Mensaje = String.Empty;
        //    try
        //    {
        //        clsModificaTituloValor cr_Procedimiento = new clsModificaTituloValor(lint_NumValor, lint_NumCupon,
        //            lstr_EstadoValor, lstr_Nemotecnico, lstr_Tipo, lstr_TipoNegociacion, lstr_Moneda, ldec_ValorFacial,
        //            ldt_FchValor, lstr_PlazoValor, ldt_FchCancelacion, ldt_FchVencimiento, ldt_FchConstitucion,
        //            ldec_ValorTransadoBruto, ldec_ValorTransadoNeto, ldec_TasaBruta, ldec_TasaNeta, ldec_Margen,
        //            lint_NumEmisionSerie, ldt_FchCreacionT, ldt_FchInicio, lstr_Propietario, lstr_EntidadCustodia,
        //            lstr_SistemaNegociacion, lstr_MotivoAnulacion, ldec_RendimientoPorDescuento, ldec_InteresBruto,
        //            ldec_InteresBrutoEfectivo, ldec_InteresNeto, ldec_InteresNetoAcumulado, ldec_ImpuestoVencido,
        //            ldec_ImpuestoEfectivo, ldec_ImpuestoPagado, ldec_Premio, lstr_ModuloSINPE, lstr_IndicadorGarantia,
        //            lstr_IndicadorCupon, lstr_Origen, lstr_UsrModifica, ldt_FchModifica);
        //        str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
        //        str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

        //        Log.Info(str_Mensaje);
        //        if (String.Equals(str_CodResultado, "00"))
        //        {
        //            lstr_ResCreacion = "Código: " + str_CodResultado + ". Mensaje: " + str_Mensaje;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lstr_ResCreacion = ex.ToString();
        //    }
        //    return lstr_ResCreacion;
        //}

        #endregion

        #region Constructor

        public clsTrasladoMagisterio()
        { }

        #endregion
    }
}