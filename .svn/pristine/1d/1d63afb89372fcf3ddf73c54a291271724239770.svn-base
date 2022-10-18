using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna;
using System.Data;
using log4net;
using log4net.Config;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsCalculoFlujoEfectivoDE
    {
        #region Variables

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        #endregion

        #region Métodos

        public DataSet ConsultarCalculoFlujoEfectivoDE(string lint_NumValor, string lstr_Nemotecnico)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaCalculoFlujoEfectivoDE cr_Procedimiento = new clsConsultaCalculoFlujoEfectivoDE(lint_NumValor, lstr_Nemotecnico);
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

        public DataSet ConsultarCalculosFlujoEfectivoDEAgrupa(string lst_IdPrestamo, string lst_IdTramo)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarCalculosFlujoEfectivoDEAgrupa cr_Procedimiento = new clsConsultarCalculosFlujoEfectivoDEAgrupa(lst_IdPrestamo, Convert.ToInt32( lst_IdPrestamo));
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

        public bool CrearCalculoFlujoEfectivoDE(int lint_NumValor, string lstr_Nemotecnico, string lstr_Periodo, decimal ldec_TasaInteres,
            decimal ldec_Intereses, decimal ldec_FlujoEfectivo, string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaCalculoFlujoEfectivoDE cr_Procedimiento = new clsCreaCalculoFlujoEfectivoDE(lint_NumValor, lstr_Nemotecnico, lstr_Periodo,
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
            {
                str_CodResultado = "99";
                str_Mensaje = ex.ToString();
            }
            return bool_ResCreacion;
        }

        #endregion

        #region Constructor

        public clsCalculoFlujoEfectivoDE()
        { }

        #endregion
    }
}