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
    public class clsAcreedores
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private Nullable<Int32> lint_NroAcreedor;
        public Nullable<Int32> Lint_NroAcreedor
        {
            get { return lint_NroAcreedor; }
            set { lint_NroAcreedor = value; }
        }

        private string lstr_NomAcreedor;
        public string Lstr_NomAcreedor
        {
            get { return lstr_NomAcreedor; }
            set { lstr_NomAcreedor = value; }
        }

        private string lstr_Abreviatura;
        public string Lstr_Abreviatura
        {
            get { return lstr_Abreviatura; }
            set { lstr_Abreviatura = value; }
        }

        private string lstr_Contacto;
        public string Lstr_Contacto
        {
            get { return lstr_Contacto; }
            set { lstr_Contacto = value; }
        }

        private string lstr_Telefono;
        public string Lstr_Telefono
        {
            get { return lstr_Telefono; }
            set { lstr_Telefono = value; }
        }

        private string lstr_Direccion;
        public string Lstr_Direccion
        {
            get { return lstr_Direccion; }
            set { lstr_Direccion = value; }
        }

        private string lstr_Pais;
        public string Lstr_Pais
        {
            get { return lstr_Pais; }
            set { lstr_Pais = value; }
        }

        private string lstr_TipoAcreedor;
        public string Lstr_TipoAcreedor
        {
            get { return lstr_TipoAcreedor; }
            set { lstr_TipoAcreedor = value; }
        }

        private string lstr_PaisInstitucion;
        public string Lstr_PaisInstitucion
        {
            get { return lstr_PaisInstitucion; }
            set { lstr_PaisInstitucion = value; }
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

        public DataSet ConsultarAcreedores(Nullable<Int32> int_NroAcreedor, string str_NomAcreedor)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarAcreedores cr_Procedimiento = new clsConsultarAcreedores(int_NroAcreedor, str_NomAcreedor);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearAcreedor(Int32 int_NroAcreedor, string str_NomAcreedor, string str_Abreviatura, string str_Contacto, string str_Telefono, string str_Direccion, string str_Pais, string str_TipoAcreedor, string str_PaisInstitucion, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearAcreedor cls_ProcCrearAcreedor = new clsCrearAcreedor(int_NroAcreedor, str_NomAcreedor, str_Abreviatura, str_Contacto, str_Telefono, str_Direccion, str_Pais, str_TipoAcreedor, str_PaisInstitucion, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearAcreedor.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearAcreedor.Lstr_MensajeRespuesta;

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

        public bool ModificarAcreedor(Int32 int_NroAcreedor, string str_NomAcreedor, string str_Abreviatura, string str_Contacto, string str_Telefono, string str_Direccion, string str_Pais, string str_TipoAcreedor, string str_PaisInstitucion, string str_IdCtaContable, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarAcreedor cls_ProcModificarAcreedor = new clsModificarAcreedor(int_NroAcreedor, str_NomAcreedor, str_Abreviatura, str_Contacto, str_Telefono, str_Direccion, str_Pais, str_TipoAcreedor, str_PaisInstitucion, str_IdCtaContable, str_UsrModifica, dt_FchModifica);
                str_CodResultado = cls_ProcModificarAcreedor.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcModificarAcreedor.Lstr_MensajeRespuesta;

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

        public clsAcreedores()
        { }
    }
}