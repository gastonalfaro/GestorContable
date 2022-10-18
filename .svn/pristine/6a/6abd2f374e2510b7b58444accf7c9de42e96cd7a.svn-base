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
    public class clsPaises
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdPais;
        public string Lstr_IdPais
        {
            get { return lstr_IdPais; }
            set { lstr_IdPais = value; }
        }

        private string lstr_Nacionalidad;
        public string Lstr_Nacionalidad
        {
            get { return lstr_Nacionalidad; }
            set { lstr_Nacionalidad = value; }
        }

        private string lstr_NomPais;
        public string Lstr_NomPais
        {
            get { return lstr_NomPais; }
            set { lstr_NomPais = value; }
        }


        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }


        public DataSet ConsultarPaises(string str_IdPais, string str_NomPais)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarPaises cr_Procedimiento = new clsConsultarPaises(str_IdPais, str_NomPais);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearPais(string str_IdPais, string str_NomPais, string str_Nacionalidad, string str_IdMoneda, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearPais cls_ProcCrearPais = new clsCrearPais(str_IdPais, str_NomPais, str_Nacionalidad, str_IdMoneda, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearPais.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearPais.Lstr_MensajeRespuesta;

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

        public bool ModificarPais(string str_IdPais, string str_NomPais, string str_Nacionalidad, string str_IdMoneda, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarPais cls_ProcModificarPais = new clsModificarPais(str_IdPais, str_NomPais, str_Nacionalidad, str_IdMoneda, str_UsrModifica, dt_FchModifica);
                str_CodResultado = cls_ProcModificarPais.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcModificarPais.Lstr_MensajeRespuesta;

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

        public clsPaises()
        { }
    }
}