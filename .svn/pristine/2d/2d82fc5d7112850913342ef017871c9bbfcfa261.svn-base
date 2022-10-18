using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna;
using log4net;
using log4net.Config;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsDevengoInteresDE
    {
        #region Variables

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        #endregion

        #region Métodos

        public DataSet ConsultarDevengoInteresDE(string lint_IdPrestamo, string lstr_IdTramo, string ldt_Anno)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaDevengoInteresDE cr_Procedimiento = new clsConsultaDevengoInteresDE(lint_IdPrestamo, lstr_IdTramo, ldt_Anno);
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

        public bool CrearDevengoInteresDE(int lint_IdPrestamo, string lstr_IdTramo, DateTime ldt_Anno, decimal ldec_CostoAmortizacionInicial,
            decimal ldec_Intereses, decimal ldec_Pago, decimal ldec_CostoAmortizacionFinal, decimal ldec_Devengado,
            string lstr_Estado, string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaDevengoInteresDE cr_Procedimiento = new clsCreaDevengoInteresDE(lint_IdPrestamo, lstr_IdTramo, ldt_Anno,
                    ldec_CostoAmortizacionInicial, ldec_Intereses, ldec_Pago, ldec_CostoAmortizacionFinal, ldec_Devengado,
                    lstr_Estado, lstr_UsrCreacion);

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
    }
}