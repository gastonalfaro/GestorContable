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
    public class clsPropietarios
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdPropietario;
        public string Lstr_IdPropietario
        {
            get { return lstr_IdPropietario; }
            set { lstr_IdPropietario = value; }
        }

        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_IdSociedadFi;
        public string Lstr_IdSociedadFi
        {
            get { return lstr_IdSociedadFi; }
            set { lstr_IdSociedadFi = value; }
        }

        private string lstr_NomPropietario;
        public string Lstr_NomPropietario
        {
            get { return lstr_NomPropietario; }
            set { lstr_NomPropietario = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarPropietarios(string str_IdPropietario, string str_IdSociedadGL, string str_IdSociedadFi, string str_NomPropietario, string str_NomExacto = "N")
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarPropietarios cr_Procedimiento = new clsConsultarPropietarios(str_IdPropietario, str_IdSociedadGL, str_IdSociedadFi, str_NomPropietario, str_NomExacto);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearPropietario(string str_IdPropietario, string str_IdSociedadGL, string str_IdSociedadFi, string str_NomPropietario, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearPropietario cls_ProcCrearPropietario = new clsCrearPropietario(str_IdPropietario, str_IdSociedadGL, str_IdSociedadFi, str_NomPropietario, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearPropietario.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearPropietario.Lstr_MensajeRespuesta;

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

        public bool ModificarPropietario(string str_IdPropietario, string str_IdSociedadGL, string str_IdSociedadFi, string str_NomPropietario, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarPropietario cls_ProcModificarPropietario = new clsModificarPropietario(str_IdPropietario, str_IdSociedadGL, str_IdSociedadFi, str_NomPropietario, str_Estado, str_UsrModifica, dt_FchModifica);
                str_CodResultado = cls_ProcModificarPropietario.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcModificarPropietario.Lstr_MensajeRespuesta;

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

        public clsPropietarios()
        { }
    }
}