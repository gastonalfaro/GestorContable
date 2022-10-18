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
    public class clsOpcionesCatalogo
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private Int32? lint_IdCatalogo;
        public Int32? Lint_IdCatalogo
        {
            get { return lint_IdCatalogo; }
            set { lint_IdCatalogo = value; }
        }


        private string lstr_AbrevCatalogo;
        public string Lstr_AbrevCatalogo
        {
            get { return lstr_AbrevCatalogo; }
            set { lstr_AbrevCatalogo = value; }
        }

        private Int32? lint_IdOpcion;
        public Int32? Lint_IdOpcion
        {
            get { return lint_IdOpcion; }
            set { lint_IdOpcion = value; }
        }

        private string lstr_ValOpcion;
        public string Lstr_ValOpcion
        {
            get { return lstr_ValOpcion; }
            set { lstr_ValOpcion = value; }
        }

        private string lstr_NomOpcion;
        public string Lstr_NomOpcion
        {
            get { return lstr_NomOpcion; }
            set { lstr_NomOpcion = value; }
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

        public DataSet ConsultarOpcionesCatalogo(Int32? int_IdCatalogo, string str_AbrevCatalogo, Int32? int_IdOpcion, string str_NomOpcion)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarOpcionesCatalogo cr_Procedimiento = new clsConsultarOpcionesCatalogo(int_IdCatalogo, str_AbrevCatalogo, int_IdOpcion, str_NomOpcion);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearOpcionCatalogo(Int32 int_IdCatalogo, Int32 int_IdOpcion, string str_ValOpcion, string str_NomOpcion, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearOpcionCatalogo cls_ProcCrearOpcion = new clsCrearOpcionCatalogo(int_IdCatalogo, int_IdOpcion, str_ValOpcion, str_NomOpcion, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearOpcion.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearOpcion.Lstr_MensajeRespuesta;

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

        public bool ModificarOpcionCatalogo(Int32 int_IdCatalogo, Int32 int_IdOpcion, string str_ValOpcion, string str_NomOpcion, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarOpcionCatalogo cls_ProcModificarOpcion = new clsModificarOpcionCatalogo(int_IdCatalogo, int_IdOpcion, str_ValOpcion, str_NomOpcion, str_Estado, str_UsrModifica, dt_FchModifica);
                str_CodResultado = cls_ProcModificarOpcion.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcModificarOpcion.Lstr_MensajeRespuesta;

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

        public clsOpcionesCatalogo()
        { }
    }
}