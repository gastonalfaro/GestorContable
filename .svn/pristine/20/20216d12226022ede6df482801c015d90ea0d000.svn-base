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
    public class clsDevengoMensual
    {
        #region Variables

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        #endregion

        #region Métodos

        public DataSet ConsultarDevengoMensual(string lint_NumValor, string lstr_Nemotecnico)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaDevengoMensual cr_Procedimiento = new clsConsultaDevengoMensual(lint_NumValor, lstr_Nemotecnico);
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

        public DataSet ConsultarDevengoMensualCanje(string lint_NumValor, string lstr_Nemotecnico)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaDevengoMensualCanje cr_Procedimiento = new clsConsultaDevengoMensualCanje(lint_NumValor, lstr_Nemotecnico);
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

        public bool CrearDevengoMensual(int lint_NumValor, string lstr_Nemotecnico, string lstr_Periodo, int lint_IdDevengoIntFK, int lint_DiasPeriodo,
            decimal ldec_InteresTotal, decimal ldec_Cupon, decimal ldec_Descuento, string lstr_UsrCreacion, out string str_CodResultado,
            out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaDevengoMensual cr_Procedimiento = new clsCreaDevengoMensual(lint_NumValor, lstr_Nemotecnico, lstr_Periodo, lint_IdDevengoIntFK, lint_DiasPeriodo,
                   Math.Round(ldec_InteresTotal,12),Math.Round(ldec_Cupon,12), Math.Round(ldec_Descuento,12), lstr_UsrCreacion);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

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

        public bool CrearDevengoMensualCanje(int lint_NumValor, string lstr_Nemotecnico, string lstr_Periodo, int lint_IdDevengoIntFK, int lint_DiasPeriodo,
    decimal ldec_InteresTotal, decimal ldec_Cupon, decimal ldec_Descuento, string lstr_IdentificadorCanje, string lstr_UsrCreacion, out string str_CodResultado,
    out string str_Mensaje, DateTime FchCanje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaDevengoMensualCanje cr_Procedimiento = new clsCreaDevengoMensualCanje(lint_NumValor, lstr_Nemotecnico, lstr_Periodo, lint_IdDevengoIntFK, lint_DiasPeriodo,
                    ldec_InteresTotal, ldec_Cupon, ldec_Descuento, lstr_IdentificadorCanje, lstr_UsrCreacion, FchCanje);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

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

        #endregion

        #region Métodos NroSerie

        public DataSet ConsultarDevengoMensualNroSerie(string lstr_NroEmision)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaDevengoMensualNroSerie cr_Procedimiento = new clsConsultaDevengoMensualNroSerie(lstr_NroEmision);
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

        public bool CrearDevengoMensualNroSerie(string lstr_NroEmision, string lstr_Periodo, int lint_DiasPeriodo,
            decimal ldec_Columna1, decimal ldec_Columna2, decimal ldec_Columna3, decimal ldec_Columna4, string lstr_UsrCreacion, out string str_CodResultado,
            out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaDevengoMensualNroSerie cr_Procedimiento = new clsCreaDevengoMensualNroSerie(lstr_NroEmision, lstr_Periodo, lint_DiasPeriodo,
                    ldec_Columna1, ldec_Columna2, ldec_Columna3, ldec_Columna4, lstr_UsrCreacion);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

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

        #endregion
    }
}