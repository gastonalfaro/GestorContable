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
    public class clsInteresPunitivo
    {
        #region Variables

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        #endregion

        #region Métodos

        public DataSet ConsultarInteresPunitivo(string lstr_IdPrestamo, int? lint_IdTramo, DateTime? ldt_FchPagoAPartir = null, DateTime? ldt_FchTasaAPartir = null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaInteresPunitivo cr_Procedimiento = new clsConsultaInteresPunitivo(lstr_IdPrestamo, lint_IdTramo, ldt_FchPagoAPartir, ldt_FchTasaAPartir);
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

        public bool CrearInteresPunitivo(string lstr_IdPrestamo, int? lint_IdTramo, DateTime? ldt_FchPagoAPartir, DateTime? ldt_FchTasaAPartir, string lstr_Tasa,
            decimal? ldec_TasaMargen, string ldec_Anno, string ldec_Mes, string lstr_FactorConversion, DateTime? ldt_FchPagoHasta, string lstr_Periodo, decimal? ldec_PeriodoDias,
            decimal? ldec_Monto, decimal? ldec_DiasGracia, decimal? ldec_TasaPunitiva,
            string lstr_Estado, string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaInteresPunitivo cr_Procedimiento = new clsCreaInteresPunitivo(lstr_IdPrestamo, lint_IdTramo, ldt_FchPagoAPartir, ldt_FchTasaAPartir, lstr_Tasa,
            ldec_TasaMargen, ldec_Anno, ldec_Mes, lstr_FactorConversion, ldt_FchPagoHasta, lstr_Periodo, ldec_PeriodoDias,
            ldec_Monto, ldec_DiasGracia, ldec_TasaPunitiva,
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

        public bool ModificarInteresPunitivo(string lstr_IdPrestamo, int? lint_IdTramo, DateTime? ldt_FchPagoAPartir, DateTime? ldt_FchTasaAPartir, string lstr_Tasa,
            decimal? ldec_TasaMargen, string ldec_Anno, string ldec_Mes, string lstr_FactorConversion, DateTime? ldt_FchPagoHasta, string lstr_Periodo, decimal? ldec_PeriodoDias,
            decimal? ldec_Monto, decimal? ldec_DiasGracia, decimal? ldec_TasaPunitiva,
            string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificaInteresPunitivo cr_Procedimiento = new clsModificaInteresPunitivo(lstr_IdPrestamo, lint_IdTramo, ldt_FchPagoAPartir, ldt_FchTasaAPartir, lstr_Tasa,
            ldec_TasaMargen, ldec_Anno, ldec_Mes, lstr_FactorConversion, ldt_FchPagoHasta, lstr_Periodo, ldec_PeriodoDias,
            ldec_Monto, ldec_DiasGracia, ldec_TasaPunitiva,
            lstr_UsrModifica, ldt_FchModifica);

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

        public bool CambiarInteresPunitivo(string lstr_IdPrestamo, int lint_IdTramo, DateTime ldt_FchAPartir, DateTime ldt_FchTasaAPartir, string lstr_Estado, string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCambiaEstadoInteresPunitivo cr_Procedimiento = new clsCambiaEstadoInteresPunitivo(lstr_IdPrestamo, lint_IdTramo, ldt_FchAPartir, ldt_FchTasaAPartir, lstr_Estado, lstr_UsrModifica, ldt_FchModifica);

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
                ex.ToString();
            }
            return bool_ResCreacion;
        }


        public DataSet ConsultarInteresPunitivoPago(string lstr_IdPrestamo, int? lint_IdTramo, DateTime? ldt_FchAPartir = null, int? lint_Secuencia=null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaInteresPunitivoPago cr_Procedimiento = new clsConsultaInteresPunitivoPago(lstr_IdPrestamo, lint_IdTramo, ldt_FchAPartir, lint_Secuencia);
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

        public bool CrearInteresPunitivoPago(string lstr_IdPrestamo, int lint_IdTramo, DateTime ldt_FchAPartir,
            decimal ldec_Monto, string lstr_IdMoneda, int lint_secuencia,
            string lstr_Estado, string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaInteresPunitivoPago cr_Procedimiento = new clsCreaInteresPunitivoPago(lstr_IdPrestamo, lint_IdTramo, ldt_FchAPartir,
            ldec_Monto, lstr_IdMoneda, lint_secuencia,
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

        public bool ModificarInteresPunitivoPago(string lstr_IdPrestamo, int lint_IdTramo, DateTime ldt_FchAPartir, decimal ldec_Monto, string lstr_IdMoneda,
            int lint_secuencia, string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificaInteresPunitivoPago cr_Procedimiento = new clsModificaInteresPunitivoPago(lstr_IdPrestamo, lint_IdTramo, ldt_FchAPartir, ldec_Monto, lstr_IdMoneda,
            lint_secuencia, lstr_UsrModifica, ldt_FchModifica);

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

        //public bool CambiarInteresPunitivoPago(string lstr_IdPrestamo, int lint_IdTramo, DateTime ldt_FchAPartir, int lint_secuencia, string lstr_Estado, string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        //{
        //    bool bool_ResCreacion = false;
        //    str_CodResultado = String.Empty;
        //    str_Mensaje = String.Empty;
        //    try
        //    {
        //        clsCambiaEstadoInteresPunitivo cr_Procedimiento = new clsCambiaEstadoInteresPunitivo(lstr_IdPrestamo, lint_IdTramo, ldt_FchAPartir, lint_secuencia, lstr_Estado, lstr_UsrModifica, ldt_FchModifica);

        //        str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
        //        str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

        //        Log.Info(str_Mensaje);
        //        if (String.Equals(str_CodResultado, "00"))
        //        {
        //            bool_ResCreacion = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //    return bool_ResCreacion;
        //}

        #endregion

        #region Constructor

        public clsInteresPunitivo()
        { }

        #endregion
    }
}