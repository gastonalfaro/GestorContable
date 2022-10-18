using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos.Mantenimiento;
using System.Data;
using log4net;
using log4net.Config;

namespace LogicaNegocio.Mantenimiento
{
    public class tMoneda
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }

        private string lstr_NomMoneda;
        public string Lstr_NomMoneda
        {
            get { return lstr_NomMoneda; }
            set { lstr_NomMoneda = value; }
        }

        private char lstr_ConversionUSD;
        public char Lstr_ConversionUSD
        {
            get { return lstr_ConversionUSD; }
            set { lstr_ConversionUSD = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarMonedas(string str_IdMoneda = null, string str_NomMoneda = null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarMonedas cr_Procedimiento = new clsConsultarMonedas(str_IdMoneda, str_NomMoneda);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearMoneda(string str_IdMoneda, string str_NomMoneda, string str_Estado, string str_UsrCreacion, char str_ConversionUSD, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearMoneda cls_ProcCrearMoneda = new clsCrearMoneda(str_IdMoneda, str_NomMoneda, str_Estado, str_UsrCreacion, str_ConversionUSD);
                str_CodResultado = cls_ProcCrearMoneda.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearMoneda.Lstr_MensajeRespuesta;

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

        public bool ModificarMoneda(string str_IdMoneda, string str_NomMoneda, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica, char str_ConversionUSD, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarMoneda cls_ProcModificarMoneda = new clsModificarMoneda(str_IdMoneda, str_NomMoneda, str_Estado, str_UsrModifica, dt_FchModifica, str_ConversionUSD);
                str_CodResultado = cls_ProcModificarMoneda.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcModificarMoneda.Lstr_MensajeRespuesta;

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

        public tMoneda()
        { }
    }
}