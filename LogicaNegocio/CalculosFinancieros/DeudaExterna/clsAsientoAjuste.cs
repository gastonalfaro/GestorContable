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
    public class clsAsientoAjuste
    {
        #region Variables

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        #endregion

        #region Métodos

        public DataSet ConsultarAsiento(string lstr_UsrCreacion)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaAsientoAjuste cr_Procedimiento = new clsConsultaAsientoAjuste(lstr_UsrCreacion);
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

        public bool CrearAsiento(string lstr_IdAsiento, string lstr_UsrCreacion, string lstr_IdCuenta, string lstr_NombreCuenta, string lstr_ClaveContable, decimal ldec_MontoContable, decimal ldec_MontoDebe, decimal ldec_MontoHaber, string lstr_Moneda, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaAsientoAjuste cr_Procedimiento = new clsCreaAsientoAjuste(lstr_IdAsiento, lstr_UsrCreacion, lstr_IdCuenta, lstr_NombreCuenta, lstr_ClaveContable, ldec_MontoContable, ldec_MontoDebe, ldec_MontoHaber, lstr_Moneda);

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

        public clsAsientoAjuste()
        { }

        #endregion
    }
}