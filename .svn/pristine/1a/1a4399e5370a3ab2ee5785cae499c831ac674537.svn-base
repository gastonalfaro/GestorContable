using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Seguridad;
using Datos.ConexionSQL.Procedimientos.Mantenimiento;
using System.Data;
using log4net;
using log4net.Config;

namespace LogicaNegocio.Mantenimiento
{
    public class tParametro
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdParametro;
        public string Lstr_IdParametro
        {
            get { return lstr_IdParametro; }
            set { lstr_IdParametro = value; }
        }

        private tModulo ltmod_Modulo;
        public tModulo Ltmod_Modulo
        {
            get { return ltmod_Modulo; }
            set { ltmod_Modulo = value; }
        }

        private DateTime ldt_FchVigencia;
        public DateTime Ldt_FchVigencia
        {
            get { return ldt_FchVigencia; }
            set { ldt_FchVigencia = value; }
        }

        private string lstr_TipoParametro;
        public string Lstr_TipoParametro
        {
            get { return lstr_TipoParametro; }
            set { lstr_TipoParametro = value; }
        }

        private string lstr_DesParametro;
        public string Lstr_DesParametro
        {
            get { return lstr_DesParametro; }
            set { lstr_DesParametro = value; }
        }

        private string lstr_Valor;
        public string Lstr_Valor
        {
            get { return lstr_Valor; }
            set { lstr_Valor = value; }
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
        /*private tPermisosParametro ltper_Permisos = new tPermisosParametro();
        public tPermisosParametro Ltper_Permisos
        {
            get { return ltper_Permisos; }
            set { ltper_Permisos = value; }
        }*/

        /// <summary>
        /// Creacion de Parametro
        /// </summary>
        /// <param name="str_IdParametro"></param>
        /// <param name="dt_FchVigencia"></param>
        /// <param name="str_IdModulo"></param>
        /// <param name="str_DesParametro"></param>
        /// <param name="str_TipoParametro"></param>        
        /// <param name="str_Valor"></param>
        /// <param name="str_UsrCreacion"></param>
        /// <returns></returns>
        public bool CrearParametro(string str_IdParametro, DateTime dt_FchVigencia, string str_IdModulo, string str_DesParametro, string str_TipoParametro, string str_Valor, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResultadoCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearParametro cls_ProcCrearParametro = new clsCrearParametro(str_IdParametro, dt_FchVigencia, str_IdModulo, str_DesParametro, str_TipoParametro, str_Valor, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearParametro.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearParametro.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(cls_ProcCrearParametro.Lstr_CodigoResultado, "00"))
                {
                    bool_ResultadoCreacion = true;
                }
            }
            catch (Exception ex)
            { 
            }
            return bool_ResultadoCreacion;
        }

        /// <summary>
        /// Consulta de Parametros
        /// </summary>
        /// <param name="str_IdParametro"></param>
        /// <param name="str_IdModulo"></param>
        /// <param name="dt_FchVigencia"></param>
        /// <param name="str_DesParametro"></param>
        /// <param name="str_TipoParametro"></param>
        /// <returns></returns>
        public DataSet ConsultarParametros(string str_IdParametro, string str_IdModulo, DateTime dt_FchVigencia, string str_DesParametro, string str_TipoParametro)
        {
            DataSet lds_TablasParametros = new DataSet();
            try
            {   
                clsConsultarParametros cls_ProcConsultarParametros = new clsConsultarParametros(str_IdParametro, str_IdModulo, dt_FchVigencia, str_DesParametro, str_TipoParametro);
                if (String.Equals(cls_ProcConsultarParametros.Lstr_CodigoResultado, "00"))
                {
                    lds_TablasParametros.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ProcConsultarParametros.Lstr_RespuestaSchema)));
                    lds_TablasParametros.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ProcConsultarParametros.Lstr_RespuestaXML)));
                }
            }
            catch(Exception ex)
            { }
            return lds_TablasParametros;
        }

        /// <summary>
        /// Actualizacion de Parametros
        /// </summary>
        /// <param name="str_IdParametro"></param>
        /// <param name="str_IdModulo"></param>
        /// <param name="str_TipoParametro"></param>
        /// <param name="str_DesParametro"></param>
        /// <param name="str_UsrModificacion"></param>
        /// <returns></returns>
        public bool ModificarParametro(string str_IdParametro, DateTime dt_FchVigencia, string str_IdModulo, string str_DesParametro, string str_TipoParametro, string str_Valor, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResultadoActualizacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarParametro cla_ModificarParametro = new clsModificarParametro(str_IdParametro, dt_FchVigencia, str_IdModulo, str_DesParametro, str_TipoParametro, str_Valor, str_UsrModifica, dt_FchModifica);
                str_CodResultado = cla_ModificarParametro.Lstr_CodigoResultado;
                str_Mensaje = cla_ModificarParametro.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(cla_ModificarParametro.Lstr_CodigoResultado, "00"))
                {
                    bool_ResultadoActualizacion = true; 
                }
            }
            catch (Exception ex)
            { }
            return bool_ResultadoActualizacion;
        }

        public tParametro()
        { }
    }
}