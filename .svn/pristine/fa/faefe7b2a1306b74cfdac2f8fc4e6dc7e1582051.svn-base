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
    public class tSociedadGL
    {
        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_Denominacion;
        public string Lstr_Denominacion
        {
            get { return lstr_Denominacion; }
            set { lstr_Denominacion = value; }
        }

        private string lstr_NomSociedad;
        public string Lstr_NomSociedad
        {
            get { return lstr_NomSociedad; }
            set { lstr_NomSociedad = value; }
        }

        private string lstr_IdPais;
        public string Lstr_IdPais
        {
            get { return lstr_IdPais; }
            set { lstr_IdPais = value; }
        }

        private string lstr_Poblacion;
        public string Lstr_Poblacion
        {
            get { return lstr_Poblacion; }
            set { lstr_Poblacion = value; }
        }

        private string lstr_Calle;
        public string Lstr_Calle
        {
            get { return lstr_Calle; }
            set { lstr_Calle = value; }
        }

        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }

        private string lstr_IdIdioma;
        public string Lstr_IdIdioma
        {
            get { return lstr_IdIdioma; }
            set { lstr_IdIdioma = value; }
        }

        private string lstr_CorreoNotifica;
        public string Lstr_CorreoNotifica
        {
            get { return lstr_CorreoNotifica; }
            set { lstr_CorreoNotifica = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
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

        public DataSet ConsultarSociedadesGL(string str_IdSociedadGL, string str_NomSociedad, string str_IdPais, string str_IdMoneda)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarSociedadesGL cr_Procedimiento = new clsConsultarSociedadesGL(str_IdSociedadGL, str_NomSociedad, str_IdPais, str_IdMoneda);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }


        public DataSet ConsultarSociedadesGLSociedadesFi(string str_IdSociedadGL, string str_IdModulo, string str_IdSociedadFi)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarSociedadesGLSociedadesFi cr_Procedimiento = new clsConsultarSociedadesGLSociedadesFi(str_IdSociedadGL, str_IdModulo, str_IdSociedadFi);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }


        public bool CrearSociedadGLSociedadFi(string str_IdSociedadGL, string str_IdModulo, string str_IdSociedadFi, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearSociedadGLSociedadFi cls_ProcCrearSociedadGLSociedadFi = new clsCrearSociedadGLSociedadFi(str_IdSociedadGL, str_IdModulo, str_IdSociedadFi, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearSociedadGLSociedadFi.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearSociedadGLSociedadFi.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResCreacion;
        }

        public bool ModificarSociedadGLSociedadFi(string str_IdSociedadGL, string str_IdModulo, string str_IdSociedadFi, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResModifica = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarSociedadGLSociedadFi cls_ProcModificarSociedadGLSociedadFi = new clsModificarSociedadGLSociedadFi(str_IdSociedadGL, str_IdModulo, str_IdSociedadFi, str_UsrModifica, dt_FchModifica);
                str_CodResultado = cls_ProcModificarSociedadGLSociedadFi.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcModificarSociedadGLSociedadFi.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResModifica = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResModifica;
        }

        public bool CrearSociedadGL(string str_IdSociedadGL, string str_NomSociedad, string str_IdPais, string str_Poblacion, string str_Calle, string str_IdMoneda, string str_IdIdioma, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearSociedadGL cls_ProcCrearSociedadGL = new clsCrearSociedadGL(str_IdSociedadGL, str_NomSociedad, str_IdPais, str_Poblacion, str_Calle, str_IdMoneda, str_IdIdioma, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearSociedadGL.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearSociedadGL.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResCreacion;
        }

        public bool ModificarSociedadGL(string str_IdSociedadGL, string str_Denominacion, string str_NomSociedad, string str_IdPais, string str_Poblacion, string str_Calle, string str_IdMoneda, string str_IdIdioma, string str_CorreoNotifica, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResModificacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarSociedadGL cls_ProcModificarSociedadGL = new clsModificarSociedadGL(str_IdSociedadGL, str_Denominacion, str_NomSociedad, str_IdPais, str_Poblacion, str_Calle, str_IdMoneda, str_IdIdioma, str_CorreoNotifica, str_Estado, str_UsrModifica, dt_FchModifica);
                str_CodResultado = cls_ProcModificarSociedadGL.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcModificarSociedadGL.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResModificacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResModificacion;
        }
        
        public tSociedadGL()
        { 
            
        }
    }
}