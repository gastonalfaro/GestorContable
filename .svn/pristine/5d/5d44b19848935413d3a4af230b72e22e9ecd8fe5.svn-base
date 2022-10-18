using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using LogicaNegocio.CapturaIngresos;
using log4net;
using log4net.Config;
using System.Globalization;

namespace WebService_CapturaIngresos
{
    /// <summary>
    /// Summary description for wsCapturaIngresos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsCapturaIngresos : System.Web.Services.WebService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(wsCapturaIngresos));
        clsCapturaIngresos ci = new clsCapturaIngresos();
        string numletras;

        #region Formulario


        [WebMethod]
        public string CrearFormulario(int lint_IdFormulario, int lint_AnnoFormulario, string lstr_TipoIdPersona, string lstr_IdentificacionPersona,
            string lstr_CorreoPersona, string lstr_ElementoPEP, string lstr_ReservaPresupuestaria, string lsrt_NumExpediente,
            string lstr_Descripcion, string lstr_CtaCliente, string lstr_Direccion, string lstr_EdoFormulario, string lstr_Usuario,
            out int int_TmpIdFormulario)
        {
            string lstr_Estado = "REG";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            int lint_NroFormulario = 0;

            try
            {
                clsFormulariosCapturaIngresos lcls_Formulario = new clsFormulariosCapturaIngresos();
                lcls_Formulario.InsertarFormularioCapturaIngresos(lint_IdFormulario, lint_AnnoFormulario,
                    lstr_TipoIdPersona, lstr_IdentificacionPersona, lstr_CorreoPersona, lstr_ElementoPEP,
                    lstr_ReservaPresupuestaria, lsrt_NumExpediente, lstr_Descripcion, lstr_CtaCliente,
                    lstr_Direccion, lstr_Estado, lstr_Usuario, out codSalida,
                    out txtSalida, out lint_NroFormulario);

                mensaje = "Código " + codSalida + ": " + txtSalida;
                
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
                Log.Error(mensaje);
            }

            int_TmpIdFormulario = lint_NroFormulario;
            return mensaje;
        }

        [WebMethod]
        public string ModificarFormulario(int lint_IdFormulario, int lint_AnnoFormulario, string lstr_TipoIdPersona, string lstr_IdentificacionPersona,
            string lstr_CorreoPersona, string lstr_ElementoPEP, string lstr_ReservaPresupuestaria, string lsrt_NumExpediente,
            string lstr_Descripcion, string lstr_CtaCliente, string lstr_Direccion, string lstr_EdoFormulario, string lstr_Usuario,
            out int int_TmpIdFormulario)
        {
            string lstr_Estado = "REG";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            int lint_NroFormulario = 0;

            try
            {
                clsFormulariosCapturaIngresos lcls_Formulario = new clsFormulariosCapturaIngresos();
                lcls_Formulario.InsertarFormularioCapturaIngresos(lint_IdFormulario, lint_AnnoFormulario,
                    lstr_TipoIdPersona, lstr_IdentificacionPersona, lstr_CorreoPersona, lstr_ElementoPEP,
                    lstr_ReservaPresupuestaria, lsrt_NumExpediente, lstr_Descripcion, lstr_CtaCliente,
                    lstr_Direccion, lstr_Estado, lstr_Usuario, out codSalida,
                    out txtSalida, out lint_NroFormulario);

                mensaje = "Código " + codSalida + ": " + txtSalida;

            }
            catch (Exception e)
            {
                mensaje = e.ToString();
                Log.Error(mensaje);
            }

            int_TmpIdFormulario = lint_NroFormulario;
            return mensaje;
        }

        [WebMethod]
        public DataSet ConsultarFormulario(string lstr_IdPersona, string lstr_TipoPersona, string lstr_Estado)
        {
            DataSet ldas_Formulario = new DataSet();
            string mensaje = "";

            try
            {
                clsFormulariosCapturaIngresos lcls_Formulario = new clsFormulariosCapturaIngresos();
                ldas_Formulario = lcls_Formulario.ConsultarFormulario(lstr_IdPersona, lstr_TipoPersona, lstr_Estado);
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
                Log.Error(mensaje);
            }
            return ldas_Formulario;
        }

        #endregion


        #region Pago


        [WebMethod]
        public string CrearPago(int lint_IdFormulario, int lint_Anno, int lint_IdPago, DateTime ldt_FchIngreso, DateTime ldt_FchPago,
            string lstr_IdInstitucion, string lstr_IdServicio, string lstr_CtaMayor, string lstr_IdOficina, string lstr_IdPosPre,
            string lstr_IdMoneda, decimal ldec_Monto, string lstr_Periodo, string lstr_Usuario, out int int_TmpIdPago)
        {
            string lstr_Estado = "REG";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            int lint_NroFormulario = 0;

            try
            {
                clsPagosPorFormulario lcls_Pago = new clsPagosPorFormulario();
                lcls_Pago.InsertarPagosFormularioCaptura(lint_IdFormulario, lint_Anno, lint_IdPago, ldt_FchIngreso, ldt_FchPago,
                    lstr_IdInstitucion, lstr_IdServicio, lstr_CtaMayor, lstr_IdOficina, lstr_IdPosPre,
                    lstr_IdMoneda, ldec_Monto, lstr_Periodo, lstr_Estado, lstr_Usuario, out codSalida, out txtSalida, out int_TmpIdPago);

                mensaje = "Código " + codSalida + ": " + txtSalida;

            }
            catch (Exception e)
            {
                mensaje = e.ToString();
                Log.Error(mensaje);
            }

            int_TmpIdPago = lint_NroFormulario;
            return mensaje;
        }

        [WebMethod]
        public string ModificarPago(int lint_IdFormulario, int lint_Anno, int lint_IdPago, DateTime ldt_FchIngreso, DateTime ldt_FchPago,
            string lstr_IdInstitucion, string lstr_IdServicio, string lstr_CtaMayor, string lstr_IdOficina, string lstr_IdPosPre,
            string lstr_IdMoneda, decimal ldec_Monto, string lstr_Periodo, string lstr_Usuario, out int int_TmpIdPago)
        {
            string lstr_Estado = "REG";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            int lint_NroFormulario = 0;

            try
            {
                clsPagosPorFormulario lcls_Pago = new clsPagosPorFormulario();
                lcls_Pago.InsertarPagosFormularioCaptura(lint_IdFormulario, lint_Anno, lint_IdPago, ldt_FchIngreso, ldt_FchPago,
                    lstr_IdInstitucion, lstr_IdServicio, lstr_CtaMayor, lstr_IdOficina, lstr_IdPosPre,
                    lstr_IdMoneda, ldec_Monto, lstr_Periodo, lstr_Estado, lstr_Usuario, out codSalida, out txtSalida, out int_TmpIdPago);

                mensaje = "Código " + codSalida + ": " + txtSalida;

            }
            catch (Exception e)
            {
                mensaje = e.ToString();
                Log.Error(mensaje);
            }

            int_TmpIdPago = lint_NroFormulario;
            return mensaje;
        }

        [WebMethod]
        public DataSet ConsultarPago(int lint_opcion, string lstr_valor1)
        {
            DataSet ldas_Pago = new DataSet();
            string mensaje = "";

            try
            {
                clsPagosPorFormulario lcls_Pago = new clsPagosPorFormulario();

                switch (lint_opcion)
                {
                    case 1: ldas_Pago = lcls_Pago.ConsultarPagosFormulario(Convert.ToInt32(lstr_valor1)); break;
                    case 2: ldas_Pago = lcls_Pago.ConsultarPagosFormulario(0, Convert.ToInt32(lstr_valor1)); break;
                }
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
                Log.Error(mensaje);
            }
            return ldas_Pago;
        }

        #endregion

<<<<<<< .mine
=======


        clsCapturaIngresos ci = new clsCapturaIngresos();
        string numletras;

>>>>>>> .r422
        [WebMethod]
        public string uwsConvertirMontoLetras(decimal letras)
        {
            numletras = ci.ufnConvertirMontoLetras(letras);
            return numletras;
        }

        [WebMethod]
        public bool uwsCalcularDígitoVerificador(string cuentacliente)
        {
            if (ci.ufnCalcularDígitoVerificador(cuentacliente) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //[WebMethod]
        //public DataSet uwsConsultarFormularios(string lstr_IdPersona, string lstr_estado)
        //{
        //    DataSet ldas_formualrios = new DataSet();
        //    string mensaje = "";

        //    try
        //    {
        //        clsConsultasCapturaIngreso lcls_consultas = new clsConsultasCapturaIngreso();
        //        ldas_formualrios = lcls_consultas.ufnConsultaFormulario(lstr_IdPersona, lstr_estado);
        //    }
        //    catch (Exception e)
        //    {
        //        mensaje = e.ToString();
        //    }

        //    return ldas_formualrios;
        //}


    }
}
