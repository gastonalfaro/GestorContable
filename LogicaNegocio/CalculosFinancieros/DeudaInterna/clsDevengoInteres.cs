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
using System.Globalization;

namespace LogicaNegocio.CalculosFinancieros.DeudaInterna
{
    public class clsDevengoInteres
    {
        #region Constructor
        public clsDevengoInteres()
        {
            CultureInfo culture;
            culture = CultureInfo.CreateSpecificCulture("es-CR");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
        #endregion

        #region Variables

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        #endregion

        #region Métodos

        public DataSet ConsultarDevengoInteres(string lint_NumValor, string lstr_Nemotecnico)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaDevengoInteres cr_Procedimiento = new clsConsultaDevengoInteres(lint_NumValor, lstr_Nemotecnico);
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
        public DataSet ConsultarDevengoInteresCanje(string lint_NumValor, string lstr_Nemotecnico)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaDevengoInteresCanje cr_Procedimiento = new clsConsultaDevengoInteresCanje(lint_NumValor, lstr_Nemotecnico);
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

        public bool CrearDevengoInteres(int lint_NumValor, string lstr_Nemotecnico, DateTime ldt_Anno, int lint_IdFlujoEfectivoFK, decimal ldec_CostoAmortizacionInicial,
            decimal ldec_Intereses, decimal ldec_Pago, decimal ldec_CostoAmortizacionFinal, decimal ldec_DescuentoDevengado, decimal ldec_TIR,
            string lstr_Estado, string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaDevengoInteres cr_Procedimiento = new clsCreaDevengoInteres(lint_NumValor, lstr_Nemotecnico, ldt_Anno, lint_IdFlujoEfectivoFK,
                    ldec_CostoAmortizacionInicial, ldec_Intereses, ldec_Pago, ldec_CostoAmortizacionFinal, ldec_DescuentoDevengado, ldec_TIR,
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
            { }
            return bool_ResCreacion;
        }

        public bool CrearTituloCanjeSubasta(string lstr_NroEmisionSerie, int lint_NumValor, string lstr_Nemotecnico, string FchCanje, string lstr_TituloCompraEmision,
            string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                DateTime ldt_FchCanje = Convert.ToDateTime(FchCanje);
                clsCreaTitulosCanjeSubasta cr_Procedimiento = new clsCreaTitulosCanjeSubasta(lstr_NroEmisionSerie, lint_NumValor, lstr_Nemotecnico, ldt_FchCanje, lstr_TituloCompraEmision,lstr_UsrCreacion);

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

        public bool CrearDevengoInteresCanje(int lint_NumValor, string lstr_Nemotecnico, DateTime ldt_Anno, int lint_IdFlujoEfectivoFK, decimal ldec_CostoAmortizacionInicial,
          decimal ldec_Intereses, decimal ldec_Pago, decimal ldec_CostoAmortizacionFinal, decimal ldec_DescuentoDevengado, decimal ldec_TIR,
          string lstr_Estado,string lstr_IdentificadorCanje, DateTime ldt_fchcanje, string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaDevengoInteresCanje cr_Procedimiento = new clsCreaDevengoInteresCanje(lint_NumValor, lstr_Nemotecnico, ldt_Anno, lint_IdFlujoEfectivoFK,
                    ldec_CostoAmortizacionInicial, ldec_Intereses, ldec_Pago, ldec_CostoAmortizacionFinal, ldec_DescuentoDevengado, ldec_TIR,
                    lstr_Estado, lstr_IdentificadorCanje,ldt_fchcanje, lstr_UsrCreacion);

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

        #region Métodos NroSerie

        public DataSet ConsultarDevengoInteresNroSerie(string lstr_NroEmision)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaDevengoInteresNroSerie cr_Procedimiento = new clsConsultaDevengoInteresNroSerie(lstr_NroEmision);
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

        public bool CrearDevengoInteresNroSerie(string lstr_NroEmision, DateTime ldt_Anno, decimal ldec_CostoAmortizacionInicial,
            decimal ldec_Intereses, decimal ldec_Pago, decimal ldec_CostoAmortizacionFinal, decimal ldec_DescuentoDevengado,
            string lstr_Estado, string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaDevengoInteresNroSerie cr_Procedimiento = new clsCreaDevengoInteresNroSerie(lstr_NroEmision, ldt_Anno,
                    ldec_CostoAmortizacionInicial, ldec_Intereses, ldec_Pago, ldec_CostoAmortizacionFinal, ldec_DescuentoDevengado,
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
            { }
            return bool_ResCreacion;
        }

        #endregion
    }
}