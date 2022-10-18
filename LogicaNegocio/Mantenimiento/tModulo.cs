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
    public class tModulo
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdModulo;
        public string Lstr_IdModulo
        {
            get { return Lstr_IdModulo; }
            set { Lstr_IdModulo = value; }
        }
        
        private string lstr_NombreModulo;
        public string Lstr_NombreModulo
        {
            get { return lstr_NombreModulo; }
            set { lstr_NombreModulo = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarModulos(string str_IdModulo, string str_NomModulo)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarModulos cr_Procedimiento = new clsConsultarModulos(str_IdModulo, str_NomModulo);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearModulo(string str_IdModulo, string str_NomModulo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearModulo cls_ProcCrearModulo = new clsCrearModulo(str_IdModulo, str_NomModulo, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearModulo.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearModulo.Lstr_MensajeRespuesta;

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

        public bool ModificarModulo(string str_IdModulo, string str_NomModulo, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarModulo cls_ProcModificarModulo = new clsModificarModulo(str_IdModulo, str_NomModulo, str_Estado, str_UsrModifica, dt_FchModifica);
                str_CodResultado = cls_ProcModificarModulo.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcModificarModulo.Lstr_MensajeRespuesta;

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

        public tModulo()
        { }
    }
}