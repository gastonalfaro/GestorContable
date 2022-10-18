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
    public class clsTramo
    {
        #region Variables

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        #endregion

        #region Métodos

        public DataSet ConsultarTramo(string lstr_IdPrestamo = null, int? lint_IdTramo = null, string lstr_TipoAcuerdo = null, string lstr_TipoFinanciamiento = null,
            string lstr_TerminoCredito = null, string lstr_Reorganizacion = null, string lstr_TermReorganizacion = null, decimal? ldec_Monto = null, string ldec_IdMoneda = null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaTramo cr_Procedimiento = new clsConsultaTramo(lstr_IdPrestamo, lint_IdTramo, lstr_TipoAcuerdo, lstr_TipoFinanciamiento,
                                                                         lstr_TerminoCredito, lstr_Reorganizacion, lstr_TermReorganizacion, ldec_Monto,ldec_IdMoneda);
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

        public bool CrearTramo(string lstr_IdPrestamo, int lint_IdTramo, string lstr_TipoAcuerdo, string lstr_TipoFinanciamiento, string lstr_TipoInstrumento,
            string lstr_TerminoCredito, string lstr_Reorganizacion, string lstr_TerminoReorganizado, decimal ldec_Monto,
            string lstr_IdMoneda, decimal ldec_Tasa, string lstr_Estado, string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                //if (lstr_IdMoneda != "EUR" && lstr_IdMoneda != "CRC" && lstr_IdMoneda != "USD")
                //{
                    //clsCalculosDeudaExterna lcls_TipoCambio = new clsCalculosDeudaExterna();
                    //DateTime ldt_FechaActual = DateTime.UtcNow.Date;
                    //ldec_Monto = lcls_TipoCambio.ConvertirdorDeMoneda(lstr_IdMoneda, ldt_FechaActual, ldec_Monto, out str_Mensaje);
                    //lstr_IdMoneda = "USD";
                //}
                clsCreaTramo cr_Procedimiento = new clsCreaTramo(lstr_IdPrestamo, lint_IdTramo, lstr_TipoAcuerdo, lstr_TipoFinanciamiento, lstr_TipoInstrumento,
                                                                    lstr_TerminoCredito, lstr_Reorganizacion, lstr_TerminoReorganizado, ldec_Monto,
                                                                    lstr_IdMoneda, ldec_Tasa, lstr_Estado, lstr_UsrCreacion);

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

        public bool ModificarTramo(string lstr_IdPrestamo, int lint_IdTramo, string lstr_TipoAcuerdo, string lstr_TipoFinanciamiento, string lstr_TipoInstrumento,
            string lstr_TerminoCredito, string lstr_Reorganizacion, string lstr_TerminoReorganizado, decimal ldec_Monto,
            string lstr_IdMoneda, decimal ldec_Tasa, string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificaTramo cr_Procedimiento = new clsModificaTramo(lstr_IdPrestamo, lint_IdTramo, lstr_TipoAcuerdo, lstr_TipoFinanciamiento, lstr_TipoInstrumento,
                                                                    lstr_TerminoCredito, lstr_Reorganizacion, lstr_TerminoReorganizado, ldec_Monto,
                                                                    lstr_IdMoneda, ldec_Tasa, lstr_UsrModifica, ldt_FchModifica);

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

        public bool CambiarTramo(string lstr_IdPrestamo, int lint_IdTramo, string lstr_Estado, string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCambiaEstadoTramo cr_Procedimiento = new clsCambiaEstadoTramo(lstr_IdPrestamo, lint_IdTramo, lstr_Estado, lstr_UsrModifica, ldt_FchModifica);

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

        public clsTramo()
        { }

        #endregion
    }
}