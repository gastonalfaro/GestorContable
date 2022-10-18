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
    public class clsReservasDetalle
    {
        private string lstr_IdReserva;
        public string Lstr_IdReserva
        {
            get { return lstr_IdReserva; }
            set { lstr_IdReserva = value; }
        }

        private string lstr_Posicion;
        public string Lstr_Posicion
        {
            get { return lstr_Posicion; }
            set { lstr_Posicion = value; }
        }

        private string lstr_Detalle;
        public string Lstr_Detalle
        {
            get { return lstr_Detalle; }
            set { lstr_Detalle = value; }
        }


        private string lstr_IdPosPre;
        public string Lstr_IdPosPre
        {
            get { return lstr_IdPosPre; }
            set { lstr_IdPosPre = value; }
        }

        private string lstr_IdCentroGestor;
        public string Lstr_IdCentroGestor
        {
            get { return lstr_IdCentroGestor; }
            set { lstr_IdCentroGestor = value; }
        }

        private string lstr_IdFondo;
        public string Lstr_IdFondo
        {
            get { return lstr_IdFondo; }
            set { lstr_IdFondo = value; }
        }

        private string lstr_Segmento;
        public string Lstr_Segmento
        {
            get { return lstr_Segmento; }
            set { lstr_Segmento = value; }
        }

        private string lstr_IdPrograma;
        public string Lstr_IdPrograma
        {
            get { return lstr_IdPrograma; }
            set { lstr_IdPrograma = value; }
        }


        private string lstr_IdCuentaContable;
        public string Lstr_IdCuentaContable
        {
            get { return lstr_IdCuentaContable; }
            set { lstr_IdCuentaContable = value; }
        }

        private string lstr_IdCentroCosto;
        public string Lstr_IdCentroCosto
        {
            get { return lstr_IdCentroCosto; }
            set { lstr_IdCentroCosto = value; }
        }

        private string lstr_IdElementoPEP;
        public string Lstr_IdElementoPEP
        {
            get { return lstr_IdElementoPEP; }
            set { lstr_IdElementoPEP = value; }
        }

        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }

        private decimal ldec_Monto;
        public decimal Ldec_Monto
        {
            get { return ldec_Monto; }
            set { ldec_Monto = value; }
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

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
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

        public DataSet ConsultarReservasDetallado(string str_IdReserva, string str_IdEntidadCP, string str_IdSociedadFi, string str_IdMoneda, 
            string str_NomReserva, string str_IdCentroGestor = null, string str_IdCentroCosto = null, string str_IdCuentaContable = null,
            string str_IdElementoPEP = null, string str_IdPosPre = null, string str_SoloDE = "N", string str_SoloDI = "N", string str_SoloCT = "N", string str_OrderBy = null)
        {
           DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarReservasDetallado cr_Procedimiento = new clsConsultarReservasDetallado(str_IdReserva, str_IdEntidadCP, str_IdSociedadFi, str_IdMoneda, 
                    str_NomReserva, str_IdCentroGestor, str_IdCentroCosto, str_IdCuentaContable, 
                    str_IdElementoPEP,str_IdPosPre, str_SoloDE, str_SoloDI, str_SoloCT, str_OrderBy);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool ModificarReservaDetalle(string str_IdReserva, string str_Posicion, Int32 int_OrdenContingentes, Int32 int_OrdenDeudaInterna, Int32 int_OrdenDeudaExterna, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResModificacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarReservaDetalle cls_ProcModificarReserva = new clsModificarReservaDetalle(str_IdReserva, str_Posicion, int_OrdenContingentes, int_OrdenDeudaInterna, int_OrdenDeudaExterna, str_UsrModifica, dt_FchModifica);
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
            {
                throw; 
            }
            return bool_ResCreacion;
        }
        public clsReservasDetalle()
        { }

    }
}