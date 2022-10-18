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
    public class clsPago
    {
        #region Variables

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        #endregion

        #region Métodos

        public DataSet ConsultarPago(string lstr_IdPrestamo = null, int? lint_IdTramo = null)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaPago cr_Procedimiento = new clsConsultaPago(lstr_IdPrestamo, lint_IdTramo);
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

        public bool CrearPago(string lstr_IdPrestamo, int? lint_IdTramo, int? lint_IdPago, string lstr_IdMoneda, int? lint_IdAcreedor, string lstr_RefAcreedor,
            decimal? ldec_MontoInteres, decimal? ldec_MontoComisiones, decimal? ldec_MontoPrincipal, DateTime? ldt_FechaValor, DateTime? ldt_FechaOperacion,
            DateTime? ldt_FechaTipoCambio, string lstr_Estado, string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaPago cr_Procedimiento = new clsCreaPago(lstr_IdPrestamo, lint_IdTramo, lint_IdPago,
                lstr_IdMoneda, lint_IdAcreedor, lstr_RefAcreedor, ldec_MontoInteres, ldec_MontoComisiones, ldec_MontoPrincipal,
                ldt_FechaValor, ldt_FechaOperacion, ldt_FechaTipoCambio, lstr_Estado, lstr_UsrCreacion);

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


        public bool ModificarPago(string lstr_IdPrestamo, int? lint_IdTramo, int? lint_IdPago, string lstr_IdMoneda, int? lint_IdAcreedor, string lstr_RefAcreedor,
            decimal? ldec_MontoInteres, decimal? ldec_MontoComisiones, decimal? ldec_MontoPrincipal, DateTime? ldt_FechaValor, DateTime? ldt_FechaOperacion,
            DateTime? ldt_FechaTipoCambio, string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarPago cr_Procedimiento = new clsModificarPago(lstr_IdPrestamo, lint_IdTramo, lint_IdPago, lstr_IdMoneda, lint_IdAcreedor, lstr_RefAcreedor,
            ldec_MontoInteres, ldec_MontoComisiones, ldec_MontoPrincipal, ldt_FechaValor, ldt_FechaOperacion,
            ldt_FechaTipoCambio , lstr_UsrModifica, ldt_FchModifica);

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

        public clsPago()
        { }

        #endregion
    }
}