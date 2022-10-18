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
    public class clsTituloReclasificado
    {
        #region Variables

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        #endregion

        #region Métodos
        /// <summary>
        /// Consulta los títulos reclasificados
        /// </summary>
        /// <param name="lint_NumValor">-</param>
        /// <param name="lstr_Nemotecnico">-</param>
        /// <param name="ldt_FchInicio">-</param>
        /// <param name="ldt_FchFin">-</param>
        /// <returns></returns>
        public DataSet ConsultarTituloReclasificado(string lint_NumValor, string lstr_Nemotecnico, DateTime ldt_FchInicio, DateTime ldt_FchFin)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaTituloReclasificado cr_Procedimiento = new clsConsultaTituloReclasificado(lint_NumValor, lstr_Nemotecnico, ldt_FchInicio, ldt_FchFin);
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
        /// Crea un título reclasificado en el sistema
        /// </summary>
        /// <param name="lint_NumValor">-</param>
        /// <param name="lstr_Nemotecnico">-</param>
        /// <param name="lstr_Tipo">-</param>
        /// <param name="lstr_Moneda">-</param>
        /// <param name="ldec_ValorFacial">-</param>
        /// <param name="ldec_ValorTransadoBruto">-</param>
        /// <param name="ldec_ValorTransadoNeto">-</param>
        /// <param name="ldt_FchValor">-</param>
        /// <param name="ldt_FchCancelacion">-</param>
        /// <param name="ldt_FchVencimiento">-</param>
        /// <param name="lstr_SistemaNegociacion">-</param>
        /// <param name="lstr_Estado">-</param>
        /// <param name="lstr_UsrCreacion">-</param>
        /// <param name="str_CodResultado">-</param>
        /// <param name="str_Mensaje">-</param>
        /// <returns></returns>
        public string[] CrearTituloReclasificado(int lint_NumValor, string lstr_Nemotecnico, string lstr_Tipo, string lstr_Moneda, decimal ldec_ValorFacial,
            decimal ldec_ValorTransadoBruto, decimal ldec_ValorTransadoNeto, DateTime ldt_FchValor, DateTime ldt_FchCancelacion, DateTime ldt_FchVencimiento,
            string lstr_SistemaNegociacion, string lstr_Estado, string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            string[] lstr_ResCreacion;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                lstr_ResCreacion = new string[2];
                clsCreaTituloReclasificado cr_Procedimiento = new clsCreaTituloReclasificado(lint_NumValor, lstr_Nemotecnico,
                    lstr_Tipo, lstr_Moneda, ldec_ValorFacial, ldec_ValorTransadoBruto, ldec_ValorTransadoNeto,
                    ldt_FchValor, ldt_FchCancelacion, ldt_FchVencimiento, lstr_SistemaNegociacion,
                    lstr_Estado, lstr_UsrCreacion);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    
                    lstr_ResCreacion[0] = str_CodResultado;
                    lstr_ResCreacion[1] = str_Mensaje;
                }
            }
            catch (Exception ex)
            {
                lstr_ResCreacion = new string[1];
                lstr_ResCreacion[0] = ex.ToString();
            }
            return lstr_ResCreacion;
        }

        #endregion

        #region Constructor

        public clsTituloReclasificado()
        { }

        #endregion
    }
}