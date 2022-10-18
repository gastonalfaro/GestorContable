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
    public class clsReservas
    {
        private string lstr_IdReserva;
        public string Lstr_IdReserva
        {
            get { return lstr_IdReserva; }
            set { lstr_IdReserva = value; }
        }

        private string lstr_IdEntidadCP;
        public string Lstr_IdEntidadCP
        {
            get { return lstr_IdEntidadCP; }
            set { lstr_IdEntidadCP = value; }
        }

        private string lstr_IdSociedadFi;
        public string Lstr_IdSociedadFi
        {
            get { return lstr_IdSociedadFi; }
            set { lstr_IdSociedadFi = value; }
        }

        private string lstr_IdClaseDocPsto;
        public string Lstr_IdClaseDocPsto
        {
            get { return lstr_IdClaseDocPsto; }
            set { lstr_IdClaseDocPsto = value; }
        }


        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }

        private string lstr_NomReserva;
        public string Lstr_NomReserva
        {
            get { return lstr_NomReserva; }
            set { lstr_NomReserva = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        private string lstr_Bloqueado;
        public string Lstr_Bloqueado
        {
            get { return lstr_Bloqueado; }
            set { lstr_Bloqueado = value; }
        }

        private Int32 lint_OrdenContingentes;
        public Int32 Lint_OrdenContingentes
        {
            get { return lint_OrdenContingentes; }
            set { lint_OrdenContingentes = value; }
        }

        private Int32 lint_OrdenDeudaInterna;
        public Int32 Lint_OrdenDeudaInterna
        {
            get { return lint_OrdenDeudaInterna; }
            set { lint_OrdenDeudaInterna = value; }
        }


        private Int32 lint_OrdenDeudaExterna;
        public Int32 Lint_OrdenDeudaExterna
        {
            get { return lint_OrdenDeudaExterna; }
            set { lint_OrdenDeudaExterna = value; }
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

        public DataSet ConsultarReservasDetallado(string str_IdReserva, string str_IdEntidadCP, string str_IdSociedadFi, string str_IdMoneda, string str_NomReserva, string str_OrderBy = null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarReservasDetallado cr_Procedimiento = new clsConsultarReservasDetallado(str_IdReserva, str_IdEntidadCP, str_IdSociedadFi, str_IdMoneda, str_NomReserva, str_OrderBy);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }


        public DataSet ConsultarReservas(string str_IdReserva, string str_IdEntidadCP, string str_IdSociedadFi, string str_IdMoneda, string str_NomReserva, string str_OrderBy = null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarReservas cr_Procedimiento = new clsConsultarReservas(str_IdReserva, str_IdEntidadCP, str_IdSociedadFi, str_IdMoneda, str_NomReserva, str_OrderBy);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearReserva(string str_IdReserva, string str_IdEntidadCP, string str_IdSociedadFi, string str_IdClaseDocPsto, string str_IdMoneda, string str_NomReserva, string str_Estado, string str_Bloqueado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearReserva cls_ProcCrearReserva = new clsCrearReserva(str_IdReserva, str_IdEntidadCP, str_IdSociedadFi, str_IdClaseDocPsto, str_IdMoneda, str_NomReserva, str_Estado, str_Bloqueado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearReserva.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearReserva.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResCreacion;
        }

        public bool ModificarReserva(string str_IdReserva, Int32 int_OrdenContingentes, Int32 int_OrdenDeudaInterna, Int32 int_OrdenDeudaExterna, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResModificacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarReserva cls_ProcModificarReserva = new clsModificarReserva(str_IdReserva, int_OrdenContingentes, int_OrdenDeudaInterna, int_OrdenDeudaExterna, str_UsrModifica, dt_FchModifica);
                str_CodResultado = cls_ProcModificarReserva.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcModificarReserva.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResModificacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResModificacion;
        }

        public DataSet ConsultarReservasDetalle(string str_IdReserva, string str_Posicion, string str_Detalle, string str_IdPosPre, string str_IdCentroGestor, string str_IdFondo, string str_Segmento, string str_IdPrograma, string str_IdCuentaContable, string str_IdCentroCosto, string str_IdElementoPEP)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarReservasDetalle cr_Procedimiento = new clsConsultarReservasDetalle(str_IdReserva, str_Posicion, str_Detalle, str_IdPosPre, str_IdCentroGestor, str_IdFondo, str_Segmento, str_IdPrograma, str_IdCuentaContable, str_IdCentroCosto, str_IdElementoPEP);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearReservaDetalle(string str_IdReserva, string str_Posicion, string str_Detalle, string str_IdPosPre, string str_IdCentroGestor, string str_IdFondo, string str_Segmento, string str_IdPrograma, string str_IdCuentaContable, string str_IdCentroCosto, string str_IdElementoPEP, string str_IdMoneda, decimal dec_Monto, string str_Estado, string str_Bloqueado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearReservaDetalle cls_ProcCrearReservaDetalle = new clsCrearReservaDetalle(str_IdReserva, str_Posicion, str_Detalle, str_IdPosPre, str_IdCentroGestor, str_IdFondo, str_Segmento, str_IdPrograma, str_IdCuentaContable, str_IdCentroCosto, str_IdElementoPEP, str_IdMoneda, dec_Monto, str_Estado, str_Bloqueado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearReservaDetalle.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearReservaDetalle.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResCreacion;
        }
        public clsReservas()
        { }

    }
}