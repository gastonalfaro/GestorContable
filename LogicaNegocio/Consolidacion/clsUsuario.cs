using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos.Consolidacion;

namespace LogicaNegocio.Consolidacion
{
    public class clsUsuario
    {

        #region variables
        private string lstr_IdUsuario;

        private int lint_IdRol;
        private string lstr_IdSociedadGL;
        #endregion

        #region obtencion y asignacion
        public string Lstr_IdUsuario
        {
            get { return lstr_IdUsuario; }
            set { lstr_IdUsuario = value; }
        }

        public int Lint_IdRol
        {
            get { return lint_IdRol; }
            set { lint_IdRol = value; }
        }

        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }
        
        #endregion

        #region metodos
        public DataSet BuscarUsuario(string str_IdUsuario, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsBuscarUsuario cls_BuscarUsuario = new clsBuscarUsuario(str_IdUsuario, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_BuscarUsuario.Lstr_CodigoResultado;
                str_Mensaje = cls_BuscarUsuario.Lstr_MensajeRespuesta;

                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarUsuario.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarUsuario.Lstr_RespuestaXML)));

                
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }

        public DataSet BuscarUsuariosPorRol(int int_IdRol, string str_IdSociedadGL, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            bool bool_Resultado;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsBuscarUsuariosPorRol cls_BuscarUsuariosPorRol = new clsBuscarUsuariosPorRol(int_IdRol, str_IdSociedadGL, str_Estado, str_UsrCreacion);
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarUsuariosPorRol.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarUsuariosPorRol.Lstr_RespuestaXML)));

                str_CodResultado = cls_BuscarUsuariosPorRol.Lstr_CodigoResultado;
                str_Mensaje = cls_BuscarUsuariosPorRol.Lstr_MensajeRespuesta;

                if ( String.Equals(str_CodResultado, "00") || String.Equals(str_CodResultado, "01"))
                {
                    bool_Resultado = true;
                }
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }

        #endregion

    }
}