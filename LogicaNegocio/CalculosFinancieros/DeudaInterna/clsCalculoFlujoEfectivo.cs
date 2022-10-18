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
    public class clsCalculoFlujoEfectivo
    {
        #region Variables

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        #endregion

        #region Métodos

        public DataSet ConsultarCalculoFlujoEfectivo(string lint_NumValor, string lstr_Nemotecnico)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaCalculoFlujoEfectivo cr_Procedimiento = new clsConsultaCalculoFlujoEfectivo(lint_NumValor, lstr_Nemotecnico);
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

        public bool CrearCalculoFlujoEfectivo(int lint_NumValor, string lstr_Nemotecnico, string lstr_Periodo, decimal ldec_TasaInteres,
            decimal ldec_Intereses, decimal ldec_FlujoEfectivo, string lstr_NroAsiento, string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaCalculoFlujoEfectivo cr_Procedimiento = new clsCreaCalculoFlujoEfectivo(lint_NumValor, lstr_Nemotecnico, lstr_Periodo,
                    ldec_TasaInteres, ldec_Intereses, ldec_FlujoEfectivo, lstr_NroAsiento, lstr_UsrCreacion);

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

        #region metodos NroSerie

        public DataSet ConsultarCalculoFlujoEfectivoNroSerie(string lstr_NroEmision)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaCalculoFlujoEfectivoNroSerie cr_Procedimiento = new clsConsultaCalculoFlujoEfectivoNroSerie(lstr_NroEmision);
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

        public bool CrearCalculoFlujoEfectivoNroSerie(string lstr_NroEmision, string lstr_Periodo, decimal ldec_TasaInteres,
            decimal ldec_Intereses, decimal ldec_FlujoEfectivo, string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaCalculoFlujoEfectivoNroSerie cr_Procedimiento = new clsCreaCalculoFlujoEfectivoNroSerie(lstr_NroEmision, lstr_Periodo,
                    ldec_TasaInteres, ldec_Intereses, ldec_FlujoEfectivo, lstr_UsrCreacion);

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

        #region Constructor

        public clsCalculoFlujoEfectivo()
        { }

        #endregion
    }
}