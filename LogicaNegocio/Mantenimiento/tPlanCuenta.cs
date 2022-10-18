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
    public class tPlanCuenta
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdPlanCuenta;
        public string Lstr_IdPlanCuenta
        {
            get { return lstr_IdPlanCuenta; }
            set { lstr_IdPlanCuenta = value; }
        }

        private string lstr_NomPlanCuenta;
        public string Lstr_NomPlanCuenta
        {
            get { return lstr_NomPlanCuenta; }
            set { lstr_NomPlanCuenta = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarPlanesCuentas(string str_IdPlanCuenta, string str_NomPlanCuenta)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarPlanesCuentas cr_Procedimiento = new clsConsultarPlanesCuentas(str_IdPlanCuenta, str_NomPlanCuenta);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearPlanCuenta(string str_IdPlanCuenta, string str_NomPlanCuenta, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearPlanCuenta cls_ProcCrearPlanCuenta = new clsCrearPlanCuenta(str_IdPlanCuenta, str_NomPlanCuenta, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearPlanCuenta.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearPlanCuenta.Lstr_MensajeRespuesta;

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

    }
}