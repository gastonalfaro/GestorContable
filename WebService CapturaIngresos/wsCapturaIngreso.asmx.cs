using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using LogicaNegocio.CapturaIngresos;
using LogicaNegocio.Mantenimiento;
using Logica.SubirArchivo;
using LogicaNegocio.Seguridad;
using log4net;
using log4net.Config;
using System.Globalization;

namespace WebService_CapturaIngresos
{
    /// <summary>
    /// Summary description for wsCapturaIngreso
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsCapturaIngreso : System.Web.Services.WebService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(wsCapturaIngreso));
        private static string lstr_formato_fecha = "dd/MM/yyyy";
        private static string lstr_separador_decimal = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
        clsCapturaIngresos ci = new clsCapturaIngresos();
        tMoneda mo = new tMoneda();
        clsNumALetras na = new clsNumALetras();
        string numletras;

        private tSeguridad gcls_Seguridad = new tSeguridad();

        private string[] DatosConexion()
        {
            string[] str_DatosConn = new string[5];
            str_DatosConn[0] = ConfigurationManager.AppSettings["Puerto"];
            str_DatosConn[1] = ConfigurationManager.AppSettings["Host"];
            str_DatosConn[2] = ConfigurationManager.AppSettings["UsuarioSistema"];
            str_DatosConn[3] = ConfigurationManager.AppSettings["CredencialUsuario"];
            str_DatosConn[4] = ConfigurationManager.AppSettings["CredencialContrasena"];
            return str_DatosConn;
        }        

        //para enviar el email
        private tUsuario usr_Envio = new tUsuario();
        //para sacar a quién se debe enviar la notificación
        private tSociedadGL soc_Consulta = new tSociedadGL();
        private clsOficinas Oficinas_Consulta = new clsOficinas();

        [WebMethod]
        public String uwsRegistrarAccionBitacora(string str_IdModulo, string str_IdSesionUsuario, string str_Accion, string str_Detalle)
        {
            string lstr_MensajeSalida = String.Empty;
            string str_ResCreacion = String.Empty;
            try
            {
                tBitacora ltro_Bitacora = new tBitacora();
                str_ResCreacion = ltro_Bitacora.ufnRegistrarAccionBitacora(str_IdModulo, str_IdSesionUsuario, str_Accion, str_Detalle);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
            }
            return str_ResCreacion;
        }

        [WebMethod]
        public DataSet uwsObtenerArchivoCapturaIngresos(string int_IdFormulario, string int_Anno, string str_TipoIdPersona = null, string str_IdPersona = null)
        {
            DataSet lstr_Resultado = new DataSet("ArchivoCapturaIngresos");
            int? IdFormulario = null;
            Int16? Anno = null;

            if (!string.IsNullOrEmpty(int_IdFormulario))
                IdFormulario = Convert.ToInt32(int_IdFormulario);

            if (!string.IsNullOrEmpty(int_Anno))
                Anno = Convert.ToInt16(int_Anno);

            clsArchivoSubir l_archivo = new clsArchivoSubir();
            try
            {

                lstr_Resultado = l_archivo.ufnObtenerArchivoCapturaIngresos(IdFormulario, Anno, str_TipoIdPersona, str_IdPersona);
            }
            catch (Exception ex)
            {
                //lstr_Resultado[0] = Convert.ToChar(ex.Message);

            }
            return lstr_Resultado;

        }
        [WebMethod]
        public string[] uwsEnviarCorreoCI(string str_Asunto, string str_Cuerpo, string str_nombre, byte[] b_data, int int_IdFormulario, Int16 int_Anno, string str_userCreacion)
        {
            string[] lstr_Resultado = new string[2];
            clsArchivoSubir l_archivo = new clsArchivoSubir();

            try
            {

                DataSet ds_Formulario; //= new DataSet();
                DataSet ds_Sociedad;// = new DataSet();
                DataSet ds_Oficinas;// = new DataSet();

                ds_Formulario = this.ConsultarFormulariosCapturaIngresos(int_IdFormulario, int_Anno, string.Empty, string.Empty,
                                                                                string.Empty, string.Empty, string.Empty, string.Empty,
                                                                                string.Empty, string.Empty, string.Empty);


                //this.uwsRegistrarAccionBitacora("CI", str_userCreacion, "Guardar Archivo", "Ingreso Total de tablas " + Convert.ToString(ds_Formulario.Tables.Count));
                if (ds_Formulario.Tables.Count > 0)
                {
                    //this.uwsRegistrarAccionBitacora("CI", str_userCreacion, "Guardar Archivo", "Ingreso.3." + Convert.ToString(ds_Formulario.Tables.Count) + Convert.ToString(ds_Formulario.Tables["Table"].Rows.Count));
                    if (ds_Formulario.Tables["Table"].Rows.Count > 0)
                    {
                        //this.uwsRegistrarAccionBitacora("CI", str_userCreacion, "Guardar Archivo", "Sociedad de formulario " + ds_Formulario.Tables["Table"].Rows[0]["IdSociedadGL"].ToString());
                        ds_Sociedad = soc_Consulta.ConsultarSociedadesGL(ds_Formulario.Tables["Table"].Rows[0]["IdSociedadGL"].ToString(), "", "", "");


                        ds_Oficinas = Oficinas_Consulta.ConsultarOficinas(ds_Formulario.Tables["Table"].Rows[0]["IdOficina"].ToString(),
                            ds_Formulario.Tables["Table"].Rows[0]["IdSociedadGL"].ToString(), "","");


                        if (ds_Oficinas.Tables.Count > 0)
                        {
                            if (ds_Oficinas.Tables["Table"].Rows.Count > 0)
                            {
                                //this.uwsRegistrarAccionBitacora("CI", str_userCreacion, "Guardar Archivo", "Correo de Sociedad " + ds_Sociedad.Tables["Table"].Rows[0]["CorreoNotifica"].ToString());
                                //if (lstr_Resultado[0]=="00")
                                if (!string.IsNullOrEmpty(ds_Oficinas.Tables["Table"].Rows[0]["CorreoNotifica"].ToString()))
                                {
                                    this.uwsRegistrarAccionBitacora("CI", str_userCreacion, "Envio de Correo", "Comprobante de pago de formulario " + Convert.ToString(int_Anno) + "." + Convert.ToString(int_IdFormulario) + " Enviado a " + ds_Oficinas.Tables["Table"].Rows[0]["CorreoNotifica"].ToString());
                                    if (!string.IsNullOrEmpty(str_nombre))
                                    {
                                        clsMailAttachment[] Arr_ma = new clsMailAttachment[1];
                                        clsMailAttachment ma = new clsMailAttachment(b_data, str_nombre);

                                        Arr_ma[0] = ma;
                                        //usr_Envio.EnviarCorreoAttach(DatosConexion(), ds_Sociedad.Tables["Table"].Rows[0]["CorreoNotifica"].ToString(), "Se ha recibido el comprobante de pago del formulario " + Convert.ToString(int_IdFormulario) + " del Año " + Convert.ToString(int_Anno), "Comprobante de Pago cargado al Sistema Gestor", Arr_ma);
                                        usr_Envio.EnviarCorreoAttach(DatosConexion(), ds_Oficinas.Tables["Table"].Rows[0]["CorreoNotifica"].ToString(), str_Cuerpo, str_Asunto, Arr_ma);
                                        usr_Envio.EnviarCorreoAttach(DatosConexion(), ds_Formulario.Tables["Table"].Rows[0]["Correo"].ToString(), str_Cuerpo, str_Asunto, Arr_ma);
                                    }
                                    else
                                    {
                                        usr_Envio.EnviarCorreo(DatosConexion(), ds_Oficinas.Tables["Table"].Rows[0]["CorreoNotifica"].ToString(), str_Cuerpo, str_Asunto);
                                        usr_Envio.EnviarCorreo(DatosConexion(), ds_Formulario.Tables["Table"].Rows[0]["Correo"].ToString(), str_Cuerpo, str_Asunto);
                                    }
                                    Log.Info("Comprobante de pago de formulario " + Convert.ToString(int_Anno) + "." + Convert.ToString(int_IdFormulario) + " Enviado a " + ds_Oficinas.Tables["Table"].Rows[0]["CorreoNotifica"].ToString());
                                }
                                //else
                                //    usr_Envio.EnviarCorreo(ds_Sociedad.Tables["Table"].Rows[0]["CorreoNotifica"].ToString(), "Se ha recibido el comprobante de pago del formulario " + Convert.ToString(int_IdFormulario) + " del Año " + Convert.ToString(int_Anno), "Comprobante de Pago no pudo ser cargado al Sistema Gestor");
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                this.uwsRegistrarAccionBitacora("CI", str_userCreacion, "Guardar Archivo", ex.ToString());
                gcls_Seguridad.SaveError(ex);
            }
            return lstr_Resultado;

        }

        #region Formulario


        [WebMethod]
        public string[] CrearFormulario(int lint_IdFormulario, int lint_Anno, string lstr_TipoIdPersona, string lstr_IdPersona, string lstr_NomPersona, string lstr_TipoIdPersonaTramite, string lstr_IdPersonaTramite, string lstr_NomPersonaTramite,
            string lstr_Correo, string lstr_IdSociedadGL, string lstr_IdOficina, string lstr_IdBanco, string lstr_IdElementoPEP, string lstr_IdReservaPresupuestaria, string lstr_NroExpediente, string lstr_Descripcion, string lstr_CtaCliente,
            string lstr_Direccion, DateTime ldt_FchIngreso, DateTime ldt_FchImpreso, DateTime ldt_FchPago, DateTime ldt_FchContabilizado, DateTime ldt_FchAnulado, string lstr_Estado, string lstr_Observaciones, string lstr_IdMoneda, decimal ldec_Monto, string lstr_ReferenciaDTR, string lstr_Usuario/*, 
            out int int_TmpIdFormulario*/)
        {
            string str_Estado = lstr_Estado ?? "PEN";

            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;

            int lint_NroFormulario = 0;

            try
            {
                clsFormulariosCapturaIngresos lcls_Formulario = new clsFormulariosCapturaIngresos();
                bool_ResCreacion = lcls_Formulario.InsertarFormularioCapturaIngresos(lint_IdFormulario, lint_Anno,
                    lstr_TipoIdPersona, lstr_IdPersona, lstr_NomPersona, lstr_TipoIdPersonaTramite, lstr_IdPersonaTramite, lstr_NomPersonaTramite,
                    lstr_Correo, lstr_IdSociedadGL, lstr_IdOficina, lstr_IdBanco, lstr_IdElementoPEP,
                    lstr_IdReservaPresupuestaria, lstr_NroExpediente, lstr_Descripcion, lstr_CtaCliente,
                    lstr_Direccion, ldt_FchIngreso, ldt_FchImpreso, ldt_FchPago, ldt_FchContabilizado, ldt_FchAnulado, str_Estado, lstr_Observaciones, lstr_IdMoneda, ldec_Monto, lstr_ReferenciaDTR, lstr_Usuario, out str_CodResultado,
                    out str_Mensaje, out lint_NroFormulario);

                arr_ResCreacion = new String[3];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;
                arr_ResCreacion[2] = Convert.ToString(lint_NroFormulario);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[3];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();
                arr_ResCreacion[2] = string.Empty;
            }

            //int_TmpIdFormulario = lint_NroFormulario;
            //return mensaje;
            return arr_ResCreacion;
        }

        [WebMethod]
        public string CambiarEstadoFormulario(int lint_IdFormulario, int lint_Anno, string lstr_EstadoActual, string lstr_EstadoNuevo, string lstr_ReferenciaDTR, string lstr_Usuario
            )
        {
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            Boolean lbln_Resultado = true;
            try
            {

                clsFormulariosCapturaIngresos lcls_Formulario = new clsFormulariosCapturaIngresos();
                lbln_Resultado = lcls_Formulario.CambiarEstadoFormularioCapturaIngresos(lint_IdFormulario, lint_Anno,
                    lstr_EstadoActual, lstr_EstadoNuevo, lstr_ReferenciaDTR, lstr_Usuario, out codSalida,
                    out txtSalida);
                if (lbln_Resultado)
                    mensaje = "00";
                else
                    mensaje = "Código " + codSalida + ": " + txtSalida;
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return mensaje;
        }

        [WebMethod]
        public string[] ModificarFormulario(int lint_IdFormulario, int lint_Anno, string lstr_TipoIdPersona, string lstr_IdPersona, string lstr_NomPersona, string lstr_TipoIdPersonaTramite, string lstr_IdPersonaTramite, string lstr_NomPersonaTramite,
               string lstr_Correo, string lstr_IdSociedadGL, string lstr_IdOficina, string lstr_IdBanco, string lstr_IdElementoPEP, string lstr_IdReservaPresupuestaria, string lstr_NroExpediente, string lstr_Descripcion, string lstr_CtaCliente,
               string lstr_Direccion, DateTime ldt_FchIngreso, DateTime ldt_FchImpreso, DateTime ldt_FchPago, DateTime ldt_FchContabilizado, DateTime ldt_FchAnulado, string lstr_Estado, string lstr_Observaciones, string lstr_IdMoneda, decimal ldec_Monto, string lstr_ReferenciaDTR, string lstr_Usuario/*,
            out int int_TmpIdFormulario*/)
        {
           
            return this.CrearFormulario(lint_IdFormulario, lint_Anno,
               lstr_TipoIdPersona, lstr_IdPersona, lstr_NomPersona, lstr_TipoIdPersonaTramite, lstr_IdPersonaTramite, lstr_NomPersonaTramite,
               lstr_Correo, lstr_IdSociedadGL, lstr_IdOficina, lstr_IdBanco, lstr_IdElementoPEP, lstr_IdReservaPresupuestaria, lstr_NroExpediente, lstr_Descripcion, lstr_CtaCliente,
               lstr_Direccion, ldt_FchIngreso, ldt_FchImpreso, ldt_FchPago, ldt_FchContabilizado, ldt_FchAnulado, lstr_Estado, lstr_Observaciones, lstr_IdMoneda, ldec_Monto, lstr_ReferenciaDTR, lstr_Usuario/*,
            out int_TmpIdFormulario*/);
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

        [WebMethod]
        public DataSet ConsultarFormulariosCapturaIngresos(int lint_IdFormulario, Int16 lint_Anno, string lstr_TipoIdPersona, string lstr_IdPersona,
               string lstr_IdSociedadGL, string lstr_IdElementoPEP, string lstr_IdReservaPresupuestaria, string lstr_NroExpediente, string lstr_Descripcion, string lstr_CtaCliente,
               string lstr_Estado)
        {
            DataSet ldas_Formulario = new DataSet();
            string mensaje = "";

            try
            {
                clsFormulariosCapturaIngresos lcls_Formulario = new clsFormulariosCapturaIngresos();
                ldas_Formulario = lcls_Formulario.ConsultarFormulariosCapturaIngresos(lint_IdFormulario, lint_Anno, lstr_TipoIdPersona, lstr_IdPersona,
               lstr_IdSociedadGL, lstr_IdElementoPEP, lstr_IdReservaPresupuestaria, lstr_NroExpediente, string.Empty, lstr_Descripcion, lstr_CtaCliente,
               lstr_Estado);
            }
            catch (Exception ex)
            {
                mensaje = ex.ToString();
                Log.Error(mensaje);
            }
            return ldas_Formulario;
        }


        #endregion


        #region Pago


        [WebMethod]
        public string[] CrearPago(int lint_IdFormulario, int lint_Anno, int lint_IdPago, DateTime ldt_FchIngreso, DateTime ldt_FchPago,
            string lstr_IdInstitucion, string lstr_IdServicio, string lstr_CtaMayor, string lstr_IdOficina, string lstr_IdPosPre, string lstr_IdReservaPresupuestaria,
            string lstr_NroExpediente, string lstr_IdMoneda, decimal ldec_Monto, string lstr_Periodo, string lstr_Usuario)
        {
            string lstr_Estado = "A";
            String[] arr_ResCreacion;
            arr_ResCreacion = new String[3];
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            int int_TmpIdPago = 0;

            try
            {
                clsPagosPorFormulario lcls_Pago = new clsPagosPorFormulario();
                bool_ResCreacion = lcls_Pago.InsertarPagosFormularioCaptura(lint_IdFormulario, lint_Anno, lint_IdPago, ldt_FchIngreso, ldt_FchPago,
                    lstr_IdInstitucion, lstr_IdServicio, lstr_CtaMayor, lstr_IdOficina, lstr_IdPosPre, lstr_IdReservaPresupuestaria,
                    lstr_NroExpediente, lstr_IdMoneda, ldec_Monto, lstr_Periodo, lstr_Estado, lstr_Usuario, out str_CodResultado, out str_Mensaje, out int_TmpIdPago);

                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;
                arr_ResCreacion[2] = Convert.ToString(int_TmpIdPago);

            }
            catch (Exception ex)
            {
                
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();
                arr_ResCreacion[2] = string.Empty;
            }

            return arr_ResCreacion;

        }

        [WebMethod]

        public string[] ModificarPago(int lint_IdFormulario, int lint_Anno, int lint_IdPago, DateTime ldt_FchIngreso, DateTime ldt_FchPago,
            string lstr_IdInstitucion, string lstr_IdServicio, string lstr_CtaMayor, string lstr_IdOficina, string lstr_IdPosPre, string lstr_IdReservaPresupuestaria,
            string lstr_NroExpediente, string lstr_IdMoneda, decimal ldec_Monto, string lstr_Periodo, string lstr_Usuario)
        {
            return this.CrearPago(lint_IdFormulario, lint_Anno, lint_IdPago, ldt_FchIngreso, ldt_FchPago,
            lstr_IdInstitucion, lstr_IdServicio, lstr_CtaMayor, lstr_IdOficina, lstr_IdPosPre, lstr_IdReservaPresupuestaria,
            lstr_NroExpediente, lstr_IdMoneda, ldec_Monto, lstr_Periodo, lstr_Usuario) ;

        }

        [WebMethod]
        public DataSet ConsultarPago(int lint_IdFormulario, int lint_Anno)
        {
            DataSet ldas_Pago = new DataSet();
            string mensaje = "";

            try
            {
                clsPagosPorFormulario lcls_Pago = new clsPagosPorFormulario();
                ldas_Pago = lcls_Pago.ConsultarPagosFormulario(lint_IdFormulario, lint_Anno,
                                                                      null,
                                                                      null,
                                                                      null,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      string.Empty);
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
                Log.Error(mensaje);
            }
            return ldas_Pago;
        }

        [WebMethod]
        public string DeshabilitarPago(int lint_IdFormulario, int lint_Anno, int lint_IdPago, string lstr_Estado, string lstr_UsrModifica, DateTime ldt_FchModifica)
        {
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";

            try
            {
                clsPagosPorFormulario lcls_Pagos = new clsPagosPorFormulario();
                lcls_Pagos.CambiarEstadoPago(lint_IdFormulario, lint_Anno, lint_IdPago, lstr_Estado, lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);
                mensaje = "Código " + codSalida + ": " + txtSalida;
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }
            return mensaje;
        }

        #endregion


        #region Comprobante


        [WebMethod]
        public string CrearComprobantePago(int lint_IdFormulario,
                                            int lint_AnioFormulario,
                                            string lstr_NumComprobante,
                                            DateTime ldt_FchComprobante,
                                            string lstr_IdBanco,
                                            string lstr_IdMoneda,
                                            decimal ldec_pMonto,
                                            string lstr_Observaciones,
                                            string lstr_Usuario,
                                            DateTime ldt_FchModifica)
        {

            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            try
            {
                clsComprobantesPagoPorFormulario lcls_CompPago = new clsComprobantesPagoPorFormulario();
                lcls_CompPago.InsertarComprobantesPagoPorFormulario(lint_IdFormulario,
                                            lint_AnioFormulario,
                                            lstr_NumComprobante,
                                            ldt_FchComprobante,
                                            lstr_IdBanco,
                                            lstr_IdMoneda,
                                            ldec_pMonto,
                                            lstr_Observaciones,
                                            lstr_Usuario,
                                            ldt_FchModifica, out codSalida, out txtSalida);

                mensaje = "Código " + codSalida + ": " + txtSalida;

            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return mensaje;
        }

      

        [WebMethod]
        public DataSet ConsultarComprobantePago(int lint_IdFormulario,
                                            int lint_AnioFormulario,
                                            string lstr_NumComprobante,
                                            DateTime ldt_FchComprobante,
                                            string lstr_IdBanco,
                                            string lstr_IdMoneda)
        {
            DataSet ldas_CompPago = new DataSet();
            string mensaje = "";

            try
            {
                clsComprobantesPagoPorFormulario lcls_CompPago = new clsComprobantesPagoPorFormulario();
                ldas_CompPago = lcls_CompPago.ConsultarComprobantesPagoPorFormulario (lint_IdFormulario,
                                            lint_AnioFormulario,
                                            lstr_NumComprobante,
                                            ldt_FchComprobante,
                                            lstr_IdBanco,
                                            lstr_IdMoneda );
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
                Log.Error(mensaje);
            }
            return ldas_CompPago;
        }

      

        #endregion Comprobante



        [WebMethod]
        public string uwsConvertirMontoLetras(decimal letras)
        {
            numletras = ci.ufnConvertirMontoLetras(letras);
            return numletras;
        }

        [WebMethod]
        public string uwsConvertirMontoStringLetras(string letras, string str_NomMoneda="", string str_IdMoneda = "")
        {
            if (string.IsNullOrEmpty(str_NomMoneda))
            {
                if (!string.IsNullOrEmpty(str_IdMoneda))
                {
                    try
                    {
                        DataSet lds_Monedas = new DataSet();
                        lds_Monedas = mo.ConsultarMonedas(str_IdMoneda, "");

                        if (lds_Monedas.Tables["Table"].Rows.Count > 0)
                        {
                            str_NomMoneda = lds_Monedas.Tables["Table"].Rows[0]["NomMoneda"].ToString();
                        }
                        na.SeparadorDecimalSalida = str_NomMoneda + " con";
                    }
                    catch
                    {
                        na.SeparadorDecimalSalida = "con";
                    }

                }
                else
                {
                    na.SeparadorDecimalSalida = "con";
                }
            }
            else
            {
                na.SeparadorDecimalSalida = str_NomMoneda + " con";
            }
            na.ConvertirDecimales = true;
            na.MascaraSalidaDecimal = "céntimos";
            na.LetraCapital = true;

/*
            if (lstr_separador_decimal == ",")
            letras = letras.Replace(".", "");
            else
            letras = letras.Replace(",", "");*/

            numletras = na.ToCustomCardinal(letras);
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

        #region Asientos
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lint_Anno"></param>
        /// <param name="lint_Idformulario"></param>
        [WebMethod]
        public string EnviarAsientosCI(string lint_Anno, string lint_Idformulario)
        {
            string vResultado = "00";
            try
            {
                Int16? int_Anno = null;
                int int_Idformulario = -1;
                if (!string.IsNullOrEmpty(lint_Anno))
                    int_Anno = Convert.ToInt16(lint_Anno);
                if (!string.IsNullOrEmpty(lint_Idformulario))
                    int_Idformulario = Convert.ToInt32(lint_Idformulario);
                int res = 0;
                int res1 = 0;
                clsTiposAsiento ta = new clsTiposAsiento();
                try
                {
                    res = ta.EnviarAsientosCI(int_Anno, int_Idformulario); 
                    vResultado = res.ToString();
                }
                catch(Exception ex)
                {
                    vResultado = "-1";
                }
                try { 
                    res1 = ta.EnviarAsientosCICT(int_Anno, int_Idformulario);
                    vResultado = res1.ToString();
                }
                catch (Exception ex)
                {
                    vResultado = "-1";
                }
                return vResultado;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return vResultado;
            }
        }
        #endregion
    }
}
