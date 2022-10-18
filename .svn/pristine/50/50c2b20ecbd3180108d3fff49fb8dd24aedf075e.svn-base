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
using System.Globalization;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsSaldoDeudaExt
    {

        public clsSaldoDeudaExt()
        {
            CultureInfo culture;
            culture = CultureInfo.CreateSpecificCulture("es-CR");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

        #region Variables

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        #endregion

        #region Métodos

        public DataSet ConsultarSaldosDeudaExt(string lstr_IdPrestamo, int? lint_IdTramo, DateTime? ldt_FechaDesde, DateTime? ldt_FechaHasta)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaSaldosDeudaExt cr_Procedimiento = new clsConsultaSaldosDeudaExt(lstr_IdPrestamo, lint_IdTramo, ldt_FechaDesde, ldt_FechaHasta);
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

        #endregion

        #region Constructor

        //public clsSaldoDeudaExt()
        //{ }

        #endregion
    }
}