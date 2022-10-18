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
    public class clsCatalogosGenerales
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private Nullable<int> lint_IdCatalogo;
        public Nullable<int> Lint_IdCatalogo
        {
            get { return lint_IdCatalogo; }
            set { lint_IdCatalogo = value; }
        }

        private string lstr_NomCatalogo;
        public string Lstr_NomCatalogo
        {
            get { return lstr_NomCatalogo; }
            set { lstr_NomCatalogo = value; }
        }
        private string lstr_IdModulo;
        public string Lstr_IdModulo
        {
            get { return lstr_IdModulo; }
            set { lstr_IdModulo = value; }
        }
        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarCatalogos(Nullable<int> int_IdCatalogo, string str_AbrevCatalogo, string str_NomCatalogo, string str_IdModulo)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarCatalogosGenerales cr_Procedimiento = new clsConsultarCatalogosGenerales(int_IdCatalogo, str_AbrevCatalogo, str_NomCatalogo, str_IdModulo);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearCatalogo(string str_AbrevCatalogo, string str_NomCatalogo, string str_IdModulo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearCatalogoGeneral cls_ProcCrearCatalogo = new clsCrearCatalogoGeneral(str_AbrevCatalogo, str_NomCatalogo, str_IdModulo, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearCatalogo.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearCatalogo.Lstr_MensajeRespuesta;

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

        public bool ModificarCatalogo(Int32 int_IdCatalogo, string str_AbrevCatalogo, string str_NomCatalogo, string str_IdModulo, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarCatalogoGeneral cls_ProcModificarCatalogo = new clsModificarCatalogoGeneral(int_IdCatalogo, str_AbrevCatalogo, str_NomCatalogo, str_IdModulo, str_Estado, str_UsrModifica, dt_FchModifica);
                str_CodResultado = cls_ProcModificarCatalogo.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcModificarCatalogo.Lstr_MensajeRespuesta;

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

        public clsCatalogosGenerales()
        { }
    }
}