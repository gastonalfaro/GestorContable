using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos.CapturaIngresos;
using System.Data;
using log4net;
using log4net.Config;

namespace LogicaNegocio.CapturaIngresos
{
    public class clsComprobantesPagoPorFormulario
    {
        #region Parámetros

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private int lint_IdFormulario;
        private int lint_AnioFormulario;
        private string lstr_NumComprobante;
        private DateTime ldt_FchComprobante;
        private string lstr_IdBanco;
        private string lstr_IdMoneda;
        private decimal ldec_pMonto;
        private string lstr_Observaciones;
        private string lstr_Usuario;
        private DateTime ldt_FchModifica;

        #endregion

        #region Obtención y asignación


        public int Lint_IdFormulario
        {
            get { return lint_IdFormulario; }
            set { lint_IdFormulario = value; }
        }

        public int Lint_AnioFormulario
        {
            get { return lint_AnioFormulario; }
            set { lint_AnioFormulario = value; }
        }

        public string Lstr_NumComprobante
        {
            get { return lstr_NumComprobante; }
            set { lstr_NumComprobante = value; }
        }

        public DateTime Ldt_FchComprobante
        {
            get { return ldt_FchComprobante; }
            set { ldt_FchComprobante = value; }
        }

        public string Lstr_IdBanco
        {
            get { return lstr_IdBanco; }
            set { lstr_IdBanco = value; }
        }


        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }

        public decimal Ldec_pMonto
        {
            get { return ldec_pMonto; }
            set { ldec_pMonto = value; }
        }

        public string Lstr_Observaciones
        {
            get { return lstr_Observaciones; }
            set { lstr_Observaciones = value; }
        }

        public string Lstr_Usuario
        {
            get { return lstr_Usuario; }
            set { lstr_Usuario = value; }
        }

        public DateTime Ldt_FchModifica
        {
            get { return ldt_FchModifica; }
            set { ldt_FchModifica = value; }
        }

        #endregion

        #region Metodos

        

        /// <summary>
        /// Método encargado de insertar los pagos asociados a un formulario
        /// </summary>
        /// <returns></returns>

        public bool InsertarComprobantesPagoPorFormulario(int lint_IdFormulario,
                                            int lint_AnioFormulario,                                            
                                            string lstr_NumComprobante,
                                            DateTime ldt_FchComprobante,
                                            string lstr_IdBanco,
                                            string lstr_IdMoneda,
                                            decimal ldec_pMonto,
                                            string lstr_Observaciones,
                                            string lstr_Usuario,
                                            DateTime ldt_FchModifica,
                                             out string str_CodResultado, out string str_Mensaje)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsInsertarComprobantesPagoPorFormulario cr_Procedimiento = new clsInsertarComprobantesPagoPorFormulario(lint_IdFormulario,
                                            lint_AnioFormulario,                                            
                                            lstr_NumComprobante,
                                            ldt_FchComprobante,
                                            lstr_IdBanco,
                                            lstr_IdMoneda,
                                            ldec_pMonto,
                                            lstr_Observaciones,
                                            lstr_Usuario,
                                            ldt_FchModifica);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                if (String.Equals(str_CodResultado, "00"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ConsultarComprobantesPagoPorFormulario(int lint_IdFormulario,
                                            int lint_AnioFormulario,
                                            string lstr_NumComprobante,
                                            DateTime ldt_FchComprobante,
                                            string lstr_IdBanco,
                                            string lstr_IdMoneda)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarComprobantesPagoPorFormulario cr_Procedimiento = new clsConsultarComprobantesPagoPorFormulario(lint_IdFormulario,
                                            lint_AnioFormulario,
                                            lstr_NumComprobante,
                                            ldt_FchComprobante,
                                            lstr_IdBanco,
                                            lstr_IdMoneda);
                if (String.Equals(cr_Procedimiento.Lstr_CodigoResultado, "00"))
                {
                    lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                    lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
                }
            }
            catch (Exception ex)
            { }
            return lds_TablasConsulta;
        }

     
        #endregion

        #region Constructor
        public clsComprobantesPagoPorFormulario()
        { }
        #endregion
    }
}