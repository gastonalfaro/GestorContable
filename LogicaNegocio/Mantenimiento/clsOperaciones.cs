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
    public class clsOperaciones
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdOperacion;
        public string Lstr_IdOperacion
        {
            get { return lstr_IdOperacion; }
            set { lstr_IdOperacion = value; }
        }

        private string lstr_IdOperacionReversa;
        public string Lstr_IdOperacionReversa
        {
            get { return lstr_IdOperacionReversa; }
            set { lstr_IdOperacionReversa = value; }
        }

        private string lstr_IdModulo;
        public string Lstr_IdModulo
        {
            get { return lstr_IdModulo; }
            set { lstr_IdModulo = value; }
        }

        private string lstr_NomOperacion;
        public string Lstr_NomOperacion
        {
            get { return lstr_NomOperacion; }
            set { lstr_NomOperacion = value; }
        }

        private string lstr_IdClaseDoc;
        public string Lstr_IdClaseDoc
        {
            get { return lstr_IdClaseDoc; }
            set { lstr_IdClaseDoc = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarOperaciones(string str_IdOperacion, string str_IdModulo, string str_NomOperacion)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarOperaciones cr_Procedimiento = new clsConsultarOperaciones(str_IdOperacion, str_IdModulo, str_NomOperacion);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearOperacion(string str_IdOperacion, string str_IdModulo, string str_NomOperacion, string str_IdClaseDoc, string str_Estado, string str_IdOperacionReversa, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearOperacion cls_ProcCrearOperacion = new clsCrearOperacion(str_IdOperacion, str_IdModulo, str_NomOperacion, str_IdClaseDoc, str_Estado, str_IdOperacionReversa, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearOperacion.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearOperacion.Lstr_MensajeRespuesta;

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

        public bool ModificarOperacion(string str_IdOperacion, string str_IdModulo, string str_NomOperacion, string str_IdClaseDoc, string str_Estado, string str_IdOperacionReversa, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarOperacion cls_ProcModificarOperacion = new clsModificarOperacion(str_IdOperacion, str_IdModulo, str_NomOperacion, str_IdClaseDoc, str_Estado, str_IdOperacionReversa, str_UsrModifica, dt_FchModifica);
                str_CodResultado = cls_ProcModificarOperacion.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcModificarOperacion.Lstr_MensajeRespuesta;

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

        public clsOperaciones()
        { }
    }
}