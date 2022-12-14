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
using LogicaNegocio.Seguridad;

namespace LogicaNegocio.CapturaIngresos
{
    public class clsFormulariosCapturaIngresos
    {
        private tSeguridad gcls_Seguridad = new tSeguridad();

        #region Parámetros
        public static wrTributacion.OrigenConsulta qry_Origen = new wrTributacion.OrigenConsulta();
        public static wrTributacion.Service2 srv_Tributacion = new wrTributacion.Service2();
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");
        private int lint_IdFormulario;
        private int lint_Anno;
        private string lstr_TipoIdPersona;
        private string lstr_IdPersona;
        private string lstr_NomPersona;
        private string lstr_TipoIdPersonaTramite;
        private string lstr_IdPersonaTramite;
        private string lstr_NomPersonaTramite;
        private string lstr_Correo;
        private string lstr_IdSociedadGL;
        private string lstr_IdOficina;
        private string lstr_IdBanco;
        private string lstr_IdElementoPEP;
        private string lstr_IdReservaPresupuestaria;
        private string lstr_NroExpediente;
        private string lstr_Descripcion;
        private string lstr_CtaCliente;
        private string lstr_Direccion;
        private DateTime ldt_FchIngreso;
        private DateTime ldt_FchImpreso;
        private DateTime ldt_FchPago;
        private DateTime ldt_FchContabilizado;
        private DateTime ldt_FchAnulado;
        private string lstr_Estado;
        private string lstr_Observaciones;
        private string lstr_IdMoneda;
        private decimal ldec_Monto;
        private string lstr_ReferenciaDTR;
        private string lstr_Usuario;
        private int lint_TmpIdFormulario;

        #endregion

        #region Obtención y asignación


        public int Lint_IdFormulario
        {
            get { return lint_IdFormulario; }
            set { lint_IdFormulario = value; }
        }


        public int Lint_Anno
        {
            get { return lint_Anno; }
            set { lint_Anno = value; }
        }

        public string Lstr_TipoIdPersona
        {
            get { return lstr_TipoIdPersona; }
            set { lstr_TipoIdPersona = value; }
        }

        public string Lstr_IdPersona
        {
            get { return lstr_IdPersona; }
            set { lstr_IdPersona = value; }
        }

        public string Lstr_NomPersona
        {
            get { return lstr_NomPersona; }
            set { lstr_NomPersona = value; }
        }

        public string Lstr_TipoIdPersonaTramite
        {
            get { return lstr_TipoIdPersonaTramite; }
            set { lstr_TipoIdPersonaTramite = value; }
        }


        public string Lstr_IdPersonaTramite
        {
            get { return lstr_IdPersonaTramite; }
            set { lstr_IdPersonaTramite = value; }
        }

        public string Lstr_NomPersonaTramite
        {
            get { return lstr_NomPersonaTramite; }
            set { lstr_NomPersonaTramite = value; }
        }

        public string Lstr_Correo
        {
            get { return lstr_Correo; }
            set { lstr_Correo = value; }
        }

        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        public string Lstr_IdOficina
        {
            get { return lstr_IdOficina; }
            set { lstr_IdOficina = value; }
        }

        public string Lstr_IdBanco
        {
            get { return lstr_IdBanco; }
            set { lstr_IdBanco = value; }
        }


        public string Lstr_IdElementoPEP
        {
            get { return lstr_IdElementoPEP; }
            set { lstr_IdElementoPEP = value; }
        }


        public string Lstr_IdReservaPresupuestaria
        {
            get { return lstr_IdReservaPresupuestaria; }
            set { lstr_IdReservaPresupuestaria = value; }
        }

        public string Lstr_NroExpediente
        {
            get { return lstr_NroExpediente; }
            set { lstr_NroExpediente = value; }
        }

        public string Lstr_Descripcion
        {
            get { return lstr_Descripcion; }
            set { lstr_Descripcion = value; }
        }

        public string Lstr_CtaCliente
        {
            get { return lstr_CtaCliente; }
            set { lstr_CtaCliente = value; }
        }


        public string Lstr_Direccion
        {
            get { return lstr_Direccion; }
            set { lstr_Direccion = value; }
        }

        public DateTime Ldt_FchIngreso
        {
            get { return ldt_FchIngreso; }
            set { ldt_FchIngreso = value; }
        }

        public DateTime Ldt_FchImpreso
        {
            get { return ldt_FchImpreso; }
            set { ldt_FchImpreso = value; }
        }

        public DateTime Ldt_FchPago
        {
            get { return ldt_FchPago; }
            set { ldt_FchPago = value; }
        }

        public DateTime Ldt_FchContabilizado
        {
            get { return ldt_FchContabilizado; }
            set { ldt_FchContabilizado = value; }
        }

        public DateTime Ldt_FchAnulado
        {
            get { return ldt_FchAnulado; }
            set { ldt_FchAnulado = value; }
        }

        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public string Lstr_Observaciones
        {
            get { return lstr_Observaciones; }
            set { lstr_Observaciones = value; }
        }

        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }

        public decimal Ldec_Monto
        {
            get { return ldec_Monto; }
            set { ldec_Monto = value; }
        }

        public string Lstr_ReferenciaDTR
        {
            get { return lstr_ReferenciaDTR; }
            set { lstr_ReferenciaDTR = value; }
        }
                
        public string Lstr_Usuario
        {
            get { return lstr_Usuario; }
            set { lstr_Usuario = value; }
        }

        public int Lint_TmpIdFormulario
        {
            get { return lint_TmpIdFormulario; }
            set { lint_TmpIdFormulario = value; }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Método encargado de Insertar los formularios de Captura de Ingresos
        /// </summary>
        /// <param name="lint_IdFormulario"></param>
        /// <param name="lint_Anno"></param>
        /// <param name="lint_TipoIdPersona"></param>
        /// <param name="lstr_IdPersona"></param>
        /// <param name="lstr_IdPersonaTramite"></param>
        /// <param name="lstr_Correo"></param>
        /// <param name="lint_IdElementoPEP"></param>
        /// <param name="lint_IdReservaPresupuestaria"></param>
        /// <param name="lstr_NroExpediente"></param>
        /// <param name="lstr_Descripcion"></param>
        /// <param name="lstr_Direccion"></param>
        /// <param name="lstr_EdoFormulario"></param>
        /// <param name="lstr_Observaciones"></param>
        /// <param name="lstr_Usuario"></param>
        /// <param name="lstr_FchCreacion"></param>
        /// <param name="lstr_UsrModifica"></param>
        /// <param name="lstr_FchModifica"></param>
        /// <param name="str_CodResultado"></param>
        /// <param name="str_Mensaje"></param>
        /// <param name="int_TmpIdFormulario"></param>
        /// <returns></returns>
        public bool InsertarFormularioCapturaIngresos(int lint_IdFormulario, int lint_Anno, string lstr_TipoIdPersona, string lstr_IdPersona, string lstr_NomPersona, string lstr_TipoIdPersonaTramite, string lstr_IdPersonaTramite, string lstr_NomPersonaTramite,
               string lstr_Correo, string lstr_IdSociedadGL, string lstr_IdOficina, string lstr_IdBanco, string lstr_IdElementoPEP, string lstr_IdReservaPresupuestaria, string lstr_NroExpediente, string lstr_Descripcion, string lstr_CtaCliente,
               string lstr_Direccion, DateTime ldt_FchIngreso,  DateTime ldt_FchImpreso, DateTime ldt_FchPago, DateTime ldt_FchContabilizado, DateTime ldt_FchAnulado, string lstr_Estado, string lstr_Observaciones,
                                                 string lstr_IdMoneda, decimal ldec_Monto, string lstr_ReferenciaDTR, string lstr_Usuario,
                                                    out string str_CodResultado, out string str_Mensaje, out int int_TmpIdFormulario)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            //int_TmpIdFormulario = 0;
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsInsertarFormularioCapturaIngresos cr_Procedimiento = new clsInsertarFormularioCapturaIngresos(lint_IdFormulario,
                                                                                                 lint_Anno,
                                                                                                 lstr_TipoIdPersona,
                                                                                                 lstr_IdPersona,
                                                                                                 lstr_NomPersona,
                                                                                                 lstr_TipoIdPersonaTramite,
                                                                                                 lstr_IdPersonaTramite,
                                                                                                 lstr_NomPersonaTramite,
                                                                                                 lstr_Correo,
                                                                                                 lstr_IdSociedadGL, 
                                                                                                 lstr_IdOficina, 
                                                                                                 lstr_IdBanco,
                                                                                                 lstr_IdElementoPEP,
                                                                                                 lstr_IdReservaPresupuestaria,
                                                                                                 lstr_NroExpediente,
                                                                                                 lstr_Descripcion,
                                                                                                 lstr_CtaCliente,
                                                                                                 lstr_Direccion,
                                                                                                 ldt_FchIngreso,
                                                                                                 ldt_FchImpreso,
                                                                                                 ldt_FchPago,
                                                                                                 ldt_FchContabilizado, 
                                                                                                 ldt_FchAnulado, 
                                                                                                 lstr_Estado,
                                                                                                 lstr_Observaciones,
                                                                                                 lstr_IdMoneda,
                                                                                                 ldec_Monto,
                                                                                                 lstr_ReferenciaDTR,
                                                                                                 lstr_Usuario);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;
                int_TmpIdFormulario = cr_Procedimiento.Lint_TmpIdFormulario;

                if (String.Equals(str_CodResultado, "00"))
                {
                    lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                    lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
                    if (lint_IdFormulario != 0)
                        int_TmpIdFormulario = lint_IdFormulario;
                    else
                        int_TmpIdFormulario = Convert.ToInt32(lds_TablasConsulta.Tables["Table"].Rows[0]["pTmpIdFormulario"]);
                    return true;
                }
                else
                {
                    int_TmpIdFormulario = -1;
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ConsultarPersona(string str_IdPersona, string str_Tipo)
        {
            if (str_Tipo == "F") {
                qry_Origen = wrTributacion.OrigenConsulta.Fisico;
            }
            else if (str_Tipo == "J") {
                qry_Origen = wrTributacion.OrigenConsulta.Juridico;
            }
            else{
                qry_Origen = wrTributacion.OrigenConsulta.DIMEX;
            }
            ;
            DataTable tbl_Persona = srv_Tributacion.ObtenerDatos(qry_Origen, str_IdPersona, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            return tbl_Persona;
        }

        public bool CambiarEstadoFormularioCapturaIngresos(int lint_IdFormulario, int lint_Anno, string lstr_EstadoActual, string lstr_EstadoNuevo, string lstr_ReferenciaDTR, string lstr_Usuario,
                                                    out string str_CodResultado, out string str_Mensaje)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
             if (lstr_EstadoNuevo == lstr_EstadoActual || lstr_EstadoNuevo == "")
            {
                str_CodResultado = "-1";
                str_Mensaje = "Debe indicar un estado nuevo distinto.";
                return false;
            }
            if (lstr_EstadoNuevo == "ANU" && (lstr_EstadoActual != "PEN" && lstr_EstadoActual != "IMP" )) {
                 str_CodResultado = "-1";
                 str_Mensaje   = "No se puede Anular un formulario que no esté Pendiente o Impreso.";
                 return false;
            }
            else if   ( lstr_EstadoNuevo == "IMP"  && (lstr_EstadoActual != "PEN" && lstr_EstadoActual != "IMP" ))
            {
                 str_CodResultado = "-1";
                 str_Mensaje   = "No se puede cambiar a Impreso un formulario que no esté Pendiente o Impreso.";
                 return false;
            }
            else if   ( lstr_EstadoNuevo == "PAG" && (lstr_EstadoActual == "ANU" || lstr_EstadoActual == "CNT"))
            {
                 str_CodResultado = "-1";
                 str_Mensaje   = "No se puede cambiar a Pagado un formulario Anulado o Contabilizado.";
                 return false;
       
            }
            else if (lstr_EstadoNuevo == "CNT" && lstr_EstadoActual != "PAG")
            {
                str_CodResultado = "-1";
                str_Mensaje = "No se puede cambiar a Contabilizado un formulario que no esté Pagado.";
                return false;

            }
            else
            {
                try
                {
                    clsCambiarEstadoFormularioCapturaIngresos cr_Procedimiento = new clsCambiarEstadoFormularioCapturaIngresos(lint_IdFormulario,
                                                                                                     lint_Anno,
                                                                                                     lstr_EstadoActual,
                                                                                                     lstr_EstadoNuevo,
                                                                                                     lstr_ReferenciaDTR,
                                                                                                     lstr_Usuario);

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
        }

        public DataSet ConsultarFormulario(string lstr_IdPersona = null, string lstr_TipoIdPersona = null, string lstr_estado = null)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaFormulario cr_Procedimiento = new clsConsultaFormulario(lstr_IdPersona, lstr_TipoIdPersona, lstr_estado);
                if (String.Equals(cr_Procedimiento.Lstr_CodigoResultado, "00"))
                {
                    lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                    lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
                }
            }
            catch (Exception ex)
            {

                gcls_Seguridad.SaveError(ex);
            }
            return lds_TablasConsulta;
        }

        public DataSet ConsultarFormulariosCapturaIngresos(int lint_IdFormulario, Int16 lint_Anno, string lstr_TipoIdPersona, string lstr_IdPersona,
               string lstr_IdSociedadGL, string lstr_IdElementoPEP, string lstr_IdReservaPresupuestaria, string lstr_NroExpediente, string lstr_ConExpediente, string lstr_Descripcion, string lstr_CtaCliente,
               string lstr_Estado)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarFormulariosCapturaIngresos cr_Procedimiento = new clsConsultarFormulariosCapturaIngresos(lint_IdFormulario, lint_Anno, lstr_TipoIdPersona, lstr_IdPersona,
               lstr_IdSociedadGL, lstr_IdElementoPEP, lstr_IdReservaPresupuestaria, lstr_NroExpediente, lstr_ConExpediente, lstr_Descripcion, lstr_CtaCliente,
               lstr_Estado);
                if (String.Equals(cr_Procedimiento.Lstr_CodigoResultado, "00"))
                {
                    lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                    lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
                }
            }
            catch (Exception ex)
            {
                gcls_Seguridad.SaveError(ex);
            }
            return lds_TablasConsulta;
        }

        #endregion

        #region Constructor
        public clsFormulariosCapturaIngresos()
        { }
        #endregion
    }
}