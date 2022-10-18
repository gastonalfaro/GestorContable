using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Seguridad;
using Datos.ConexionSQL.Procedimientos.Mantenimiento;
using System.Data;

namespace LogicaNegocio.Mantenimiento
{
    public class tTipoCambio
    {
        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }


        private DateTime ldt_FchReferencia;
        public DateTime Ldt_FchReferencia
        {
            get { return ldt_FchReferencia; }
            set { ldt_FchReferencia = value; }
        }

        private string lstr_TipoTransaccion;
        public string Lstr_TipoTransaccion
        {
            get { return lstr_TipoTransaccion; }
            set { lstr_TipoTransaccion = value; }
        }

        private decimal ldec_Valor;
        public decimal Ldec_Valor
        {
            get { return ldec_Valor; }
            set { ldec_Valor = value; }
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
        /// <param name="str_IdMoneda"></param>
        /// <param name="dt_FchReferencia"></param>
        /// <param name="str_IdModulo"></param>
        /// <param name="str_DesParametro"></param>
        /// <param name="str_TipoTransaccion"></param>        
        /// <param name="dec_Valor"></param>
        /// <param name="str_UsrCreacion"></param>
        /// <returns></returns>
        public bool CrearTipoCambio(string str_IdMoneda, DateTime dt_FchReferencia, string str_TipoTransaccion, decimal dec_Valor, string str_UsrCreacion)
        {
            bool bool_ResultadoCreacion = false;
            try
            {
                clsCrearTipoCambio cls_ProcCrearTipoCambio = new clsCrearTipoCambio(str_IdMoneda, dt_FchReferencia, str_TipoTransaccion, dec_Valor, str_UsrCreacion);
                if (String.Equals(cls_ProcCrearTipoCambio.Lstr_CodigoResultado, "00"))
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
        /// <param name="str_IdMoneda"></param>
        /// <param name="str_IdModulo"></param>
        /// <param name="dt_FchReferencia"></param>
        /// <param name="str_DesParametro"></param>
        /// <param name="str_TipoTransaccion"></param>
        /// <returns></returns>
        public DataSet ConsultarTiposCambio(string str_IdMoneda, DateTime dt_FchReferencia, string str_TipoTransaccion)
        {
            DataSet lds_TablasParametros = new DataSet();
            try
            {
                clsConsultarTiposCambio cls_ProcConsultarTiposCambio = new clsConsultarTiposCambio(str_IdMoneda, dt_FchReferencia, str_TipoTransaccion);
                if (String.Equals(cls_ProcConsultarTiposCambio.Lstr_CodigoResultado, "00"))
                {
                    lds_TablasParametros.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ProcConsultarTiposCambio.Lstr_RespuestaSchema)));
                    lds_TablasParametros.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ProcConsultarTiposCambio.Lstr_RespuestaXML)));
                }
            }
            catch (Exception ex)
            { }
            return lds_TablasParametros;
        }

        /// <summary>
        /// Actualizacion de Parametros
        /// </summary>
        /// <param name="str_IdMoneda"></param>
        /// <param name="str_IdModulo"></param>
        /// <param name="str_TipoTransaccion"></param>
        /// <param name="str_DesParametro"></param>
        /// <param name="str_UsrModificacion"></param>
        /// <returns></returns>
        public bool ModificarTipoCambio(string str_IdMoneda, DateTime dt_FchReferencia, string str_TipoTransaccion, decimal dec_Valor, string str_UsrModifica, DateTime dt_FchModifica)
        {
            bool bool_ResultadoActualizacion = false;
            try
            {
                clsModificarTipoCambio cla_ModificarTipoCambio = new clsModificarTipoCambio(str_IdMoneda, dt_FchReferencia, str_TipoTransaccion, dec_Valor, str_UsrModifica, dt_FchModifica);
                if (String.Equals(cla_ModificarTipoCambio.Lstr_CodigoResultado, "00"))
                {
                    bool_ResultadoActualizacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResultadoActualizacion;
        }

        public tTipoCambio()
        { }
    }
}