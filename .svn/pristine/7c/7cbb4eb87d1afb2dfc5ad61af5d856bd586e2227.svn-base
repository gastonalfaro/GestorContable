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
    public class clsIndicadoresEconomicos
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdIndicadorEco;
        public string Lstr_IdIndicadorEco
        {
            get { return lstr_IdIndicadorEco; }
            set { lstr_IdIndicadorEco = value; }
        }

        private string lstr_Transaccion;
        /// <summary>
        /// 
        /// </summary>
        public string Lstr_Transaccion
        {
            get { return lstr_Transaccion; }
            set { lstr_Transaccion = value; }
        }

        private string lstr_NomIndicadorEco;
        public string Lstr_NomIndicadorEco
        {
            get { return lstr_NomIndicadorEco; }
            set { lstr_NomIndicadorEco = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarIndicadoresEconomicos(string str_IdIndicadorEco, string str_Transaccion, string str_NomIndicadorEco)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarIndicadoresEconomicos cr_Procedimiento = new clsConsultarIndicadoresEconomicos(str_IdIndicadorEco, str_Transaccion, str_NomIndicadorEco);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearIndicadorEconomico(string str_IdIndicadorEco, string str_Transaccion, string str_NomIndicadorEco, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearIndicadorEconomico cls_ProcCrearIndicadorEconomico = new clsCrearIndicadorEconomico(str_IdIndicadorEco, str_Transaccion, str_NomIndicadorEco, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearIndicadorEconomico.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearIndicadorEconomico.Lstr_MensajeRespuesta;

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

        public bool ModificarIndicadorEconomico(string str_IdIndicadorEco, string str_Transaccion, string str_NomIndicadorEco, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarIndicadorEconomico cls_ProcModificarIndicadorEconomico = new clsModificarIndicadorEconomico(str_IdIndicadorEco, str_Transaccion, str_NomIndicadorEco, str_Estado, str_UsrModifica, dt_FchModifica);
                str_CodResultado = cls_ProcModificarIndicadorEconomico.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcModificarIndicadorEconomico.Lstr_MensajeRespuesta;

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

        public clsIndicadoresEconomicos()
        { }
    }
}