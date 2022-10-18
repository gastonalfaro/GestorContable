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
    public class clsOficinas
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdOficina;
        public string Lstr_IdOficina
        {
            get { return lstr_IdOficina; }
            set { lstr_IdOficina = value; }
        }

        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_NomOficina;
        public string Lstr_NomOficina
        {
            get { return lstr_NomOficina; }
            set { lstr_NomOficina = value; }
        }

        private string lstr_IdDireccion;
        public string Lstr_IdDireccion
        {
            get { return lstr_IdDireccion; }
            set { lstr_IdDireccion = value; }
        }

        private string lstr_CorreoNotifica;
        public string Lstr_CorreoNotifica
        {
            get { return lstr_CorreoNotifica; }
            set { lstr_CorreoNotifica = value; }
        }

        private char lstr_UsaExpediente;
        public char Lstr_UsaExpediente
        {
            get { return lstr_UsaExpediente; }
            set { lstr_UsaExpediente = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarOficinas(string str_IdOficina, string str_IdSociedadGL, string str_IdDireccion, string str_NomOficina)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarOficinas cr_Procedimiento = new clsConsultarOficinas(str_IdOficina, str_IdSociedadGL, str_IdDireccion, str_NomOficina);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearOficina(string str_IdOficina, string str_IdSociedadGL, string str_NomOficina, string str_IdDireccion, string str_Estado, string str_UsrCreacion, string str_CorreoNotifica, char str_UsaExpediente, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearOficina cls_ProcCrearOficina = new clsCrearOficina(str_IdOficina, str_IdSociedadGL, str_NomOficina, str_IdDireccion, str_Estado, str_UsrCreacion, str_CorreoNotifica, str_UsaExpediente);
                str_CodResultado = cls_ProcCrearOficina.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearOficina.Lstr_MensajeRespuesta;

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

        public bool ModificarOficina(string str_IdOficina, string str_IdSociedadGL, string str_NomOficina, string str_IdDireccion, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica, string str_CorreoNotifica, char str_UsaExpediente, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarOficina cls_ProcModificarOficina = new clsModificarOficina(str_IdOficina, str_IdSociedadGL, str_NomOficina, str_IdDireccion, str_Estado, str_UsrModifica, dt_FchModifica, str_CorreoNotifica, str_UsaExpediente);
                str_CodResultado = cls_ProcModificarOficina.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcModificarOficina.Lstr_MensajeRespuesta;

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

        public DataSet ConsultarOficinasCeBe(string str_IdOficina, string str_IdSociedadGL, string str_IdModulo, string str_IdCentroBeneficio)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarOficinasCeBe cr_Procedimiento = new clsConsultarOficinasCeBe(str_IdOficina, str_IdSociedadGL, str_IdModulo, str_IdCentroBeneficio);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearOficinaCeBe(string str_IdOficina, string str_IdSociedadGL, string str_IdModulo, string str_IdCentroBeneficio, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearOficinaCeBe cls_ProcCrearOficinaCeBe = new clsCrearOficinaCeBe(str_IdOficina, str_IdSociedadGL, str_IdModulo, str_IdCentroBeneficio, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearOficinaCeBe.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearOficinaCeBe.Lstr_MensajeRespuesta;

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

        public bool BorrarOficinaCeBe(string str_IdOficina, string str_IdSociedadGL, string str_IdModulo, string str_IdCentroBeneficio, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsBorrarOficinaCeBe cls_ProcBorrarOficinaCeBe = new clsBorrarOficinaCeBe(str_IdOficina, str_IdSociedadGL, str_IdModulo, str_IdCentroBeneficio);
                str_CodResultado = cls_ProcBorrarOficinaCeBe.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcBorrarOficinaCeBe.Lstr_MensajeRespuesta;

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

        public clsOficinas()
        { }
    }
}