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
    public class clsGiroEstimado
    {
        #region Variables

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        #endregion

        #region Métodos

        public DataSet ConsultarGiroEstimado(string lstr_IdPrestamo, int? lint_IdTramo, DateTime? ldt_FchEstimada)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaGiroEstimado cr_Procedimiento = new clsConsultaGiroEstimado(lstr_IdPrestamo, lint_IdTramo, ldt_FchEstimada);
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

        public bool CrearGiroEstimado(string lstr_IdPrestamo, int lint_IdTramo, DateTime ldt_FchEstimada, decimal ldec_Monto,
            string lstr_Estado, string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaGiroEstimado cr_Procedimiento = new clsCreaGiroEstimado(lstr_IdPrestamo, lint_IdTramo, ldt_FchEstimada, ldec_Monto, lstr_Estado, lstr_UsrCreacion);

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

        public bool ModificarGiroEstimado(string lstr_IdPrestamo, int lint_IdTramo, DateTime ldt_FchEstimada, decimal ldec_Monto,
            string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificaGiroEstimado cr_Procedimiento = new clsModificaGiroEstimado(lstr_IdPrestamo, lint_IdTramo, ldt_FchEstimada, ldec_Monto, lstr_UsrModifica, ldt_FchModifica);
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

        public bool CambiaEstadoGiro(string lstr_IdPrestamo, int lint_IdTramo, string ldt_FchDesembolso, string ldt_FchEstimada, string lstr_Moneda, int lint_Secuencia, string lstr_Estado, string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCambiaEstadoGiro cr_Procedimiento = new clsCambiaEstadoGiro(lstr_IdPrestamo, lint_IdTramo, ldt_FchDesembolso,ldt_FchEstimada, lstr_Moneda, lint_Secuencia, lstr_Estado, lstr_UsrModifica, ldt_FchModifica);

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

        public bool CambiaEstadoGiroEstimado(string lstr_IdPrestamo, int lint_IdTramo, string ldt_FchEstimada, string lstr_Estado, string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCambiaEstadoGiroEstimado cr_Procedimiento = new clsCambiaEstadoGiroEstimado(lstr_IdPrestamo, lint_IdTramo, ldt_FchEstimada, lstr_Estado, lstr_UsrModifica, ldt_FchModifica);

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

        public clsGiroEstimado()
        { }

        #endregion
    }
}