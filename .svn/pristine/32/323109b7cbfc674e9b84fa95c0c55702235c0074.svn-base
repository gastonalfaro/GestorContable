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
    public class clsBancos
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdBanco;
        public string Lstr_IdBanco
        {
            get { return lstr_IdBanco; }
            set { lstr_IdBanco = value; }
        }

        private string lstr_IdBancoPropio;
        public string Lstr_IdBancoPropio
        {
            get { return lstr_IdBancoPropio; }
            set { lstr_IdBancoPropio = value; }
        }

        private string lstr_NomBanco;
        public string Lstr_NomBanco
        {
            get { return lstr_NomBanco; }
            set { lstr_NomBanco = value; }
        }

        private string lstr_IdPais;
        public string Lstr_IdPais
        {
            get { return lstr_IdPais; }
            set { lstr_IdPais = value; }
        }

        private string lstr_Telefono;
        public string Lstr_Telefono
        {
            get { return lstr_Telefono; }
            set { lstr_Telefono = value; }
        }

        private string lstr_Contacto;
        public string Lstr_Contacto
        {
            get { return lstr_Contacto; }
            set { lstr_Contacto = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarBancos(string str_IdBanco = null, string str_IdBancoPropio = null, string str_IdSociedadFi = null, string str_NomBanco = null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarBancos cr_Procedimiento = new clsConsultarBancos(str_IdBanco, str_IdBancoPropio, str_IdSociedadFi, str_NomBanco);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearBanco(string str_IdBanco, string str_IdBancoPropio, string str_IdSociedadFi, string str_NomBanco, string str_IdPais, string str_Telefono, string str_Contacto, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearBanco cls_ProcCrearBanco = new clsCrearBanco(str_IdBanco, str_IdBancoPropio, str_IdSociedadFi, str_NomBanco, str_IdPais, str_Telefono, str_Contacto, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearBanco.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearBanco.Lstr_MensajeRespuesta;

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

        public bool ModificarBanco(string str_IdBanco, string str_NomBanco, string str_IdPais, string str_Telefono, string str_Contacto, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarBanco cls_ProcModificarBanco = new clsModificarBanco(str_IdBanco, str_NomBanco, str_IdPais, str_Telefono, str_Contacto, str_Estado, str_UsrModifica, dt_FchModifica);
                str_CodResultado = cls_ProcModificarBanco.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcModificarBanco.Lstr_MensajeRespuesta;

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


        public DataSet ConsultarBancosServicios(string str_IdBanco, string str_IdServicio, string str_IdSociedadFi)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarBancosServicios cr_Procedimiento = new clsConsultarBancosServicios(str_IdBanco, str_IdServicio, str_IdSociedadFi);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }


        public bool BorrarBancoServicio(string str_IdBanco, string str_IdServicio, string str_IdSociedadFi, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResBorrado = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsBorrarBancoServicio cls_ProcBorrarBancoServicio = new clsBorrarBancoServicio(str_IdBanco, str_IdServicio, str_IdSociedadFi);
                str_CodResultado = cls_ProcBorrarBancoServicio.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcBorrarBancoServicio.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResBorrado = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResBorrado;
        }

        public bool CrearBancoServicio(string str_IdBanco, string str_IdServicio, string str_IdSociedadFi, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearBancoServicio cls_ProcCrearBancoServicio = new clsCrearBancoServicio(str_IdBanco, str_IdServicio, str_IdSociedadFi, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearBancoServicio.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearBancoServicio.Lstr_MensajeRespuesta;

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

        public DataSet ConsultarBancosCuentas(string str_IdBanco, string str_IdBancoPropio, string str_IdCuentaBancaria, string str_CuentaBancaria, string str_IdCuentaContable, string str_IdSociedadFi)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarBancosCuentas cr_Procedimiento = new clsConsultarBancosCuentas(str_IdBanco, str_IdBancoPropio, str_IdCuentaBancaria, str_CuentaBancaria, str_IdCuentaContable, str_IdSociedadFi);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }


        public bool ModificarBancoCuenta(string str_IdBanco, string str_IdCuentaBancaria, string str_IdCuentaContable, string str_IdSociedadFi, string str_TipoCuenta, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResModificado = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarBancoCuenta cls_ProcModificarBancoCuenta = new clsModificarBancoCuenta(str_IdBanco, str_IdCuentaBancaria, str_IdCuentaContable, str_IdSociedadFi, str_TipoCuenta, str_UsrModifica, dt_FchModifica);
                str_CodResultado = cls_ProcModificarBancoCuenta.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcModificarBancoCuenta.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResModificado = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResModificado;
        }

        public bool CrearBancoCuenta(string str_IdBanco, string str_IdBancoPropio, string str_IdCuentaBancaria, string str_CuentaBancaria, string str_IdCuentaContable, string str_IdSociedadFi, string str_TipoCuenta, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearBancoCuenta cls_ProcCrearBancoCuenta = new clsCrearBancoCuenta(str_IdBanco, str_IdBancoPropio, str_IdCuentaBancaria, str_CuentaBancaria, str_IdCuentaContable, str_IdSociedadFi, str_TipoCuenta, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearBancoCuenta.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearBancoCuenta.Lstr_MensajeRespuesta;

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

        public clsBancos()
        { }
    }
}