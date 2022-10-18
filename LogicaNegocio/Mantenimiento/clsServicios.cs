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
    public class clsServicios
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdServicio;
        public string Lstr_IdServicio
        {
            get { return lstr_IdServicio; }
            set { lstr_IdServicio = value; }
        }

        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_IdOficina;
        public string Lstr_IdOficina
        {
            get { return lstr_IdOficina; }
            set { lstr_IdOficina = value; }
        }

        private string lstr_NomServicio;
        public string Lstr_NomServicio
        {
            get { return lstr_NomServicio; }
            set { lstr_NomServicio = value; }
        }

        private Nullable<decimal> ldec_Monto;
        public Nullable<decimal> Ldec_Monto
        {
            get { return ldec_Monto; }
            set { ldec_Monto = value; }
        }

        private string lstr_PermiteReserva;
        public string Lstr_PermiteReserva
        {
            get { return lstr_PermiteReserva; }
            set { lstr_PermiteReserva = value; }
        }

        private string lstr_CtaContableDebeActualDev;
        public string Lstr_CtaContableDebeActualDev
        {
            get { return lstr_CtaContableDebeActualDev; }
            set { lstr_CtaContableDebeActualDev = value; }
        }

        private string lstr_CtaContableHaberActualDev;
        public string Lstr_CtaContableHaberActualDev
        {
            get { return lstr_CtaContableHaberActualDev; }
            set { lstr_CtaContableHaberActualDev = value; }
        }

        private string lstr_IdPosPreActualDev;
        public string Lstr_IdPosPreActualDev
        {
            get { return lstr_IdPosPreActualDev; }
            set { lstr_IdPosPreActualDev = value; }
        }


        private string lstr_CtaContableDebeActualPer;
        public string Lstr_CtaContableDebeActualPer
        {
            get { return lstr_CtaContableDebeActualPer; }
            set { lstr_CtaContableDebeActualPer = value; }
        }

        private string lstr_CtaContableHaberActualPer;
        public string Lstr_CtaContableHaberActualPer
        {
            get { return lstr_CtaContableHaberActualPer; }
            set { lstr_CtaContableHaberActualPer = value; }
        }

        private string lstr_IdPosPreActualPer;
        public string Lstr_IdPosPreActualPer
        {
            get { return lstr_IdPosPreActualPer; }
            set { lstr_IdPosPreActualPer = value; }
        }

        private string lstr_CtaContableDebeVencidoDev;
        public string Lstr_CtaContableDebeVencidoDev
        {
            get { return lstr_CtaContableDebeVencidoDev; }
            set { lstr_CtaContableDebeVencidoDev = value; }
        }


        private string lstr_CtaContableHaberVencidoDev;
        public string Lstr_CtaContableHaberVencidoDev
        {
            get { return lstr_CtaContableHaberVencidoDev; }
            set { lstr_CtaContableHaberVencidoDev = value; }
        }

        private string lstr_IdPosPreVencidoDev;
        public string Lstr_IdPosPreVencidoDev
        {
            get { return lstr_IdPosPreVencidoDev; }
            set { lstr_IdPosPreVencidoDev = value; }
        }

        private string lstr_CtaContableDebeVencidoPer;
        public string Lstr_CtaContableDebeVencidoPer
        {
            get { return lstr_CtaContableDebeVencidoDev; }
            set { lstr_CtaContableDebeVencidoDev = value; }
        }
        private string lstr_CtaContableHaberVencidoPer;
        public string Lstr_CtaContableHaberVencidoPer
        {
            get { return lstr_CtaContableHaberVencidoPer; }
            set { lstr_CtaContableHaberVencidoPer = value; }
        }

        private string lstr_IdPosPreVencidoPer;
        public string Lstr_IdPosPreVencidoPer
        {
            get { return lstr_IdPosPreVencidoPer; }
            set { lstr_IdPosPreVencidoPer = value; }
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

        public DataSet ConsultarServicios(string str_IdServicio, string str_IdSociedadGL, string str_IdOficina, string str_NomServicio, string str_IdCuentaContable, string str_IdPosPre)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarServicios cr_Procedimiento = new clsConsultarServicios(str_IdServicio, str_IdSociedadGL, str_IdOficina, str_NomServicio, str_IdCuentaContable, str_IdPosPre);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearServicio(string str_IdServicio, string str_IdSociedadGL, string str_IdOficina, string str_NomServicio, Nullable<decimal> dec_Monto, string str_PermiteReserva,
            string str_CtaContableDebeActualDev, string str_CtaContableHaberActualDev, string str_IdPosPreActualDev,
            string str_CtaContableDebeActualPer, string str_CtaContableHaberActualPer, string str_IdPosPreActualPer,
            string str_CtaContableDebeVencidoDev, string str_CtaContableHaberVencidoDev, string str_IdPosPreVencidoDev,
            string str_CtaContableDebeVencidoPer, string str_CtaContableHaberVencidoPer, string str_IdPosPreVencidoPer,             
            string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearServicio cls_ProcCrearServicio = new clsCrearServicio(str_IdServicio, str_IdSociedadGL, str_IdOficina, str_NomServicio, dec_Monto, str_PermiteReserva,
                    str_CtaContableDebeActualDev, str_CtaContableHaberActualDev, str_IdPosPreActualDev,
                    str_CtaContableDebeActualPer, str_CtaContableHaberActualPer, str_IdPosPreActualPer,
                    str_CtaContableDebeVencidoDev, str_CtaContableHaberVencidoDev, str_IdPosPreVencidoDev,
                    str_CtaContableDebeVencidoPer, str_CtaContableHaberVencidoPer, str_IdPosPreVencidoPer,                     
                    str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearServicio.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearServicio.Lstr_MensajeRespuesta;

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

        public bool ModificarServicio(string str_IdServicio, string str_IdSociedadGL, string str_IdOficina, string str_NomServicio, decimal? dec_Monto, string str_PermiteReserva,
            string str_CtaContableDebeActualDev, string str_CtaContableHaberActualDev, string str_IdPosPreActualDev,
            string str_CtaContableDebeActualPer, string str_CtaContableHaberActualPer, string str_IdPosPreActualPer,
            string str_CtaContableDebeVencidoDev, string str_CtaContableHaberVencidoDev, string str_IdPosPreVencidoDev,
            string str_CtaContableDebeVencidoPer, string str_CtaContableHaberVencidoPer, string str_IdPosPreVencidoPer, 
            string str_Estado, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarServicio cls_ProcModificarServicio = new clsModificarServicio(str_IdServicio, str_IdSociedadGL, str_IdOficina, str_NomServicio, dec_Monto, str_PermiteReserva,
                    str_CtaContableDebeActualDev, str_CtaContableHaberActualDev, str_IdPosPreActualDev,
                    str_CtaContableDebeActualPer, str_CtaContableHaberActualPer, str_IdPosPreActualPer,
                    str_CtaContableDebeVencidoDev, str_CtaContableHaberVencidoDev, str_IdPosPreVencidoDev,
                    str_CtaContableDebeVencidoPer, str_CtaContableHaberVencidoPer, str_IdPosPreVencidoPer,   
                    str_Estado, str_UsrModifica, dt_FchModifica);
                str_CodResultado = cls_ProcModificarServicio.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcModificarServicio.Lstr_MensajeRespuesta;

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

        public DataSet ConsultarServiciosOficinas(string str_IdServicio, string str_IdSociedadGL, string str_IdOficina)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarServiciosOficinas cr_Procedimiento = new clsConsultarServiciosOficinas(str_IdServicio, str_IdSociedadGL,str_IdOficina);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }


        public bool BorrarServicioOficina(string str_IdServicio, string str_IdSociedadGL, string str_IdOficina, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResBorrado = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsBorrarServicioOficina cls_ProcBorrarServicioOficina = new clsBorrarServicioOficina(str_IdServicio, str_IdSociedadGL, str_IdOficina);
                str_CodResultado = cls_ProcBorrarServicioOficina.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcBorrarServicioOficina.Lstr_MensajeRespuesta;

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

        public bool CrearServicioOficina(string str_IdServicio, string str_IdSociedadGL, string str_IdOficina, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearServicioOficina cls_ProcCrearServicioOficina = new clsCrearServicioOficina(str_IdServicio, str_IdSociedadGL, str_IdOficina, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearServicioOficina.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearServicioOficina.Lstr_MensajeRespuesta;

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


        public clsServicios()
        { }
    }
}