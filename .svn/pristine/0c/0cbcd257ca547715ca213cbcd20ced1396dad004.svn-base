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
    public class clsDirecciones
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdDireccion;
        public string Lstr_IdDireccion
        {
            get { return lstr_IdDireccion; }
            set { lstr_IdDireccion = value; }
        }

        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_NomDireccion;
        public string Lstr_NomDireccion
        {
            get { return lstr_NomDireccion; }
            set { lstr_NomDireccion = value; }
        }


        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        private string lstr_UsrModifica;
        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }

        private DateTime ldt_FchModifica;
        public DateTime Ldt_FchModifica
        {
            get { return ldt_FchModifica; }
            set { ldt_FchModifica = value; }
        }

        public DataSet ConsultarDirecciones(string str_IdDireccion, string str_IdSociedadGL, string str_NomDireccion)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarDirecciones cr_Procedimiento = new clsConsultarDirecciones(str_IdDireccion, str_IdSociedadGL, str_NomDireccion);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearDireccion(string str_IdDireccion, string str_IdSociedadGL, string str_NomDireccion, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearDireccion cls_ProcCrearDireccion = new clsCrearDireccion(str_IdDireccion, str_IdSociedadGL, str_NomDireccion, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearDireccion.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearDireccion.Lstr_MensajeRespuesta;

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

        public bool ModificarDireccion(string str_IdDireccion, string str_IdSociedadGL, string str_NomDireccion, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarDireccion cls_ProcModificarDireccion = new clsModificarDireccion(str_IdDireccion, str_IdSociedadGL, str_NomDireccion, str_Estado, str_UsrModifica, dt_FchModifica);
                str_CodResultado = cls_ProcModificarDireccion.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcModificarDireccion.Lstr_MensajeRespuesta;

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

        public clsDirecciones()
        { }
    }
}