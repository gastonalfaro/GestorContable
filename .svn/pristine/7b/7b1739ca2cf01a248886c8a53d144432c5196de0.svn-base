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
    public class clsTasaFlotante
    {
        #region Variables

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        #endregion

        #region Métodos

        public DataSet ConsultarTasaFlotante(string lstr_IdPrestamo, int? lint_IdTramo, DateTime? ldt_APartir = null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaTasaFlotante cr_Procedimiento = new clsConsultaTasaFlotante(lstr_IdPrestamo, lint_IdTramo, ldt_APartir);
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

        public bool CrearTasaFlotante(string lstr_IdPrestamo, int lint_IdTramo, DateTime ldt_APartir, decimal ldec_Tasa,
            string lstr_Estado, string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaTasaFlotante cr_Procedimiento = new clsCreaTasaFlotante(lstr_IdPrestamo, lint_IdTramo, ldt_APartir, ldec_Tasa, lstr_Estado, lstr_UsrCreacion);

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

        public bool ModificarTasaFlotante(string lstr_IdPrestamo, int lint_IdTramo, DateTime ldt_APartir, decimal ldec_Tasa,
            string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificaTasaFlotante cr_Procedimiento = new clsModificaTasaFlotante(lstr_IdPrestamo, lint_IdTramo, ldt_APartir, ldec_Tasa, lstr_UsrModifica, ldt_FchModifica);

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

        public bool CambiarTasaFlotante(string lstr_IdPrestamo, int lint_IdTramo, DateTime ldt_APartir, string lstr_Estado, string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCambiaEstadoTasaFlotante cr_Procedimiento = new clsCambiaEstadoTasaFlotante(lstr_IdPrestamo, lint_IdTramo, ldt_APartir, lstr_Estado, lstr_UsrModifica, ldt_FchModifica);

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

        public clsTasaFlotante()
        { }

        #endregion
    }
}