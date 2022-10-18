using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL.Procedimientos.RevelacionNotas;
using System.Data;

namespace LogicaNegocio.RevelacionNotas
{
    public class tRevelacionNotas
    {
        private string lstr_IdRevelacion;
        public string Lstr_IdRevelacion
        {
            get { return lstr_IdRevelacion; }
            set { lstr_IdRevelacion = value; }
        }

        private string lstr_PeriodoAnual;
        public string Lstr_PeriodoAnual
        {
            get { return lstr_PeriodoAnual; }
            set { lstr_PeriodoAnual = value; }
        }
        private string lstr_PeriodoMensual;
        public string Lstr_PeriodoMensual
        {
            get { return lstr_PeriodoMensual; }
            set { lstr_PeriodoMensual = value; }
        }

        private DateTime lstr_FechaInicial;
        public DateTime Lstr_FechaInicial
        {
            get { return lstr_FechaInicial; }
            set { lstr_FechaInicial = value; }
        }

        private DateTime lstr_FechaFinal;
        public DateTime Lstr_FechaFinal
        {
            get { return lstr_FechaFinal; }
            set { lstr_FechaFinal = value; }
        }

        private string lstr_Institucion;
        public string Lstr_Institucion
        {
            get { return lstr_Institucion; }
            set { lstr_Institucion = value; }
        }
        private string lstr_Entidad;
        public string Lstr_Entidad
        {
            get { return lstr_Entidad; }
            set { lstr_Entidad = value; }
        }

        private string lstr_GrupoCuentas;
        public string Lstr_GrupoCuentas
        {
            get { return lstr_GrupoCuentas; }
            set { lstr_GrupoCuentas = value; }
        }
        private string lstr_Cuentas;
        public string Lstr_Cuentas
        {
            get { return lstr_Cuentas; }
            set { lstr_Cuentas = value; }
        }

        private string lstr_Concepto;
        public string Lstr_Concepto
        {
            get { return lstr_Concepto; }
            set { lstr_Concepto = value; }
        }
        private string lstr_Justificacion;
        public string Lstr_Justificacion
        {
            get { return lstr_Justificacion; }
            set { lstr_Justificacion = value; }
        }

        private string lstr_NumExpediente;
        public string Lstr_NumExpediente
        {
            get { return lstr_NumExpediente; }
            set { lstr_NumExpediente = value; }
        }
        private Boolean lstr_HabilitadaPretencion;
        public Boolean Lstr_HabilitadaPretencion
        {
            get { return lstr_HabilitadaPretencion; }
            set { lstr_HabilitadaPretencion = value; }
        }

        private string lstr_EstadoRevelacion;
        public string Lstr_EstadoRevelacion
        {
            get { return lstr_EstadoRevelacion; }
            set { lstr_EstadoRevelacion = value; }
        }

        /// <summary>
        /// Retorna los formularios existentes segun criterio
        /// </summary>
        /// <param name="str_IdRevelacion"></param>
        /// <param name="str_MensajeSalida"></param>
        /// <returns></returns>
        public DataSet ufnConsultarFormulario(string str_IdRevelacion, out string str_MensajeSalida)
        {
            DataSet lds_Formularios = new DataSet();
            try
            {
                clsConsultarFormulario lcf_ConsultaFormulario = new clsConsultarFormulario(str_IdRevelacion);
                if (String.Equals(lcf_ConsultaFormulario.Lstr_CodigoResultado, "00"))
                {
                    lds_Formularios.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(lcf_ConsultaFormulario.Lstr_RespuestaSchema)));
                    lds_Formularios.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(lcf_ConsultaFormulario.Lstr_RespuestaXML)));
                }
                str_MensajeSalida = lcf_ConsultaFormulario.Lstr_MensajeRespuesta;
            }
            catch(Exception ex)
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";
            }
            return lds_Formularios;
        }

        /// <summary>
        /// Realiza una busqueda de los formularios segun las condiciones que se le otorga
        /// </summary>
        /// <param name="str_IdRevelacion">Identificador de revelacion</param>
        /// <param name="str_PeriodoMensual">Periodo menssual de revelacion</param>
        /// <param name="str_GrupoCuentas">Clasificacion de cuenta</param>
        /// <param name="str_Cuentas">Detalle de subcuentas anexas</param>
        /// <param name="str_MensajeSalida">Respuesta que se obtiene de parte del procedimiento almacenado</param>
        /// <returns>Dataset con formularios que satisfacen los criterios de busqueda</returns>
        public DataSet ufnBuscarFormulario(string str_IdRevelacion, string str_PeriodoMensual, string str_GrupoCuentas, 
            string str_Cuentas,  string str_UsrCreacion, string str_Institucion, string str_Annio, out string str_MensajeSalida)
        {
            DataSet lds_Formularios = new DataSet();
            try
            {
                clsBuscarFormulario lcf_ConsultaFormulario = new clsBuscarFormulario(str_IdRevelacion, str_PeriodoMensual, str_GrupoCuentas, str_Cuentas, str_UsrCreacion, str_Institucion, str_Annio);
                if (String.Equals(lcf_ConsultaFormulario.Lstr_CodigoResultado, "00"))
                {
                    lds_Formularios.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(lcf_ConsultaFormulario.Lstr_RespuestaSchema)));
                    lds_Formularios.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(lcf_ConsultaFormulario.Lstr_RespuestaXML)));
                }
                str_MensajeSalida = lcf_ConsultaFormulario.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";
            }
            return lds_Formularios;
        }

        public DataSet ufnConsultarRevelacionContingente(string str_IdRevCont, string str_PeriodoAnual, string str_PeriodoMensual, out string str_MensajeSalida)
        {
            DataSet lds_Formularios = new DataSet();
            try
            {
                clsConsultarRevelacionContingente lcf_ConsultarRevContingente = new clsConsultarRevelacionContingente(str_IdRevCont, str_PeriodoAnual, str_PeriodoMensual);
                if (String.Equals(lcf_ConsultarRevContingente.Lstr_CodigoResultado, "00"))
                {
                    lds_Formularios.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(lcf_ConsultarRevContingente.Lstr_RespuestaSchema)));
                    lds_Formularios.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(lcf_ConsultarRevContingente.Lstr_RespuestaXML)));
                }
                str_MensajeSalida = lcf_ConsultarRevContingente.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";
            }
            return lds_Formularios;
        }

        public DataSet ufnConsultarRevelacionContSoc(string str_IdRevCont, string str_PeriodoAnual, string str_PeriodoMensual,
            string str_IdSociedadGL, string str_TipoProceso, out string str_MensajeSalida)
        {
            DataSet lds_Formularios = new DataSet();
            try
            {
                clsConsultarRevelacionContSoc lcf_ConsultarRevContingente = new clsConsultarRevelacionContSoc(str_IdRevCont, str_PeriodoAnual,
                    str_PeriodoMensual, str_IdSociedadGL, str_TipoProceso);
                if (String.Equals(lcf_ConsultarRevContingente.Lstr_CodigoResultado, "00"))
                {
                    lds_Formularios.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(lcf_ConsultarRevContingente.Lstr_RespuestaSchema)));
                    lds_Formularios.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(lcf_ConsultarRevContingente.Lstr_RespuestaXML)));
                }
                str_MensajeSalida = lcf_ConsultarRevContingente.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";
            }
            return lds_Formularios;
        }


        public DataSet ufnConsultarRevelacionPendiente(string str_IdRevelacionPendiente, string str_PeriodoMensual, string str_GrupoCuentas, 
            string str_Cuentas, string str_UsrCreacion, string str_Institucion, string str_PeriodoAnual,out string str_MensajeSalida)
        {
            DataSet lds_Formularios = new DataSet();
            try
            {
                clsConsultarRevelacionPendiente lcf_ConsultarRevPendiente = new clsConsultarRevelacionPendiente(str_IdRevelacionPendiente, str_PeriodoMensual,
                    str_GrupoCuentas, str_Cuentas, str_UsrCreacion, str_Institucion, str_PeriodoAnual);
                if (String.Equals(lcf_ConsultarRevPendiente.Lstr_CodigoResultado, "00"))
                {
                    lds_Formularios.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(lcf_ConsultarRevPendiente.Lstr_RespuestaSchema)));
                    lds_Formularios.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(lcf_ConsultarRevPendiente.Lstr_RespuestaXML)));
                }
                str_MensajeSalida = lcf_ConsultarRevPendiente.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";
            }
            return lds_Formularios;
        }

        public DataSet ufnBuscarRevelacionContingente(string str_IdRevCont, string str_PeriodoAnual, string str_PeriodoMensual, out string str_MensajeSalida)
        {
            DataSet lds_Formularios = new DataSet();
            try
            {
                clsBuscarRevelacionContingente lcf_ConsultarRevContingente = new clsBuscarRevelacionContingente(str_IdRevCont, str_PeriodoAnual, str_PeriodoMensual);
                if (String.Equals(lcf_ConsultarRevContingente.Lstr_CodigoResultado, "00"))
                {
                    lds_Formularios.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(lcf_ConsultarRevContingente.Lstr_RespuestaSchema)));
                    lds_Formularios.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(lcf_ConsultarRevContingente.Lstr_RespuestaXML)));
                }
                str_MensajeSalida = lcf_ConsultarRevContingente.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";
            }
            return lds_Formularios;
        }

        /// <summary>
        /// Creacion de una nueva revelacion
        /// </summary>
        /// <param name="str_IdRevelacion">Identificador de revelacion</param>
        /// <param name="str_PeriodoAnual">Periodo anual en que se realiza la revelacion</param>
        /// <param name="str_PeriodoMensual">Periodo mensual en que se realiza la revelacion</param>
        /// <param name="str_Institucion">Unidad Primaria de Registro</param>
        /// <param name="str_Entidad">Nombre de la Unidad Primaria de Registro</param>
        /// <param name="str_GrupoCuentas">Grupo de cuentas del Plan de Cuentas Contables </param>
        /// <param name="str_Cuentas">Cuentas del Plan de Cuentas Contables</param>
        /// <param name="str_Concepto">Explicación general del concepto </param>
        /// <param name="str_Justificacion">Explicación específica del registro </param>
        /// <param name="str_NumExpediente"></param>
        /// <param name="str_HabilitadaPretencion"></param>
        /// <param name="str_EstadoRevelacion"></param>
        /// <param name="str_UsrCreacion"></param>
        /// <param name="str_MensajeSalida"></param>
        /// <returns></returns>
        public string[] ufnCrearFormulario(string str_PeriodoAnual, string str_PeriodoMensual,
            string str_Institucion, string str_Entidad, string str_IdOficina, string str_PlanCuentas, string str_ClaseCuenta, string str_Cuentas, string str_Concepto,
            string str_Justificacion, string str_NumExpediente, string str_HabilitadaPretencion, string str_EstadoRevelacion,
            string str_UsrCreacion, out string str_MensajeSalida,    
            string str_RubroCuenta, string str_SubCuenta, string str_SubCuentaAnexa, string str_AuxiliarCuenta)
        {
            string[] str_Resultado = new string[2];
            string lstr_IdRevelacion = String.Empty;
            DataSet lds_Formulario = new DataSet();
            try
            {
                clsCrearFormulario lcf_CreacionFormulario = new clsCrearFormulario(str_PeriodoAnual, str_PeriodoMensual,
                    str_Institucion, str_Entidad, str_IdOficina, str_PlanCuentas, str_ClaseCuenta, str_Cuentas, str_Concepto, str_Justificacion, str_NumExpediente,
                    str_HabilitadaPretencion, str_EstadoRevelacion, str_UsrCreacion, str_RubroCuenta, str_SubCuenta, str_SubCuentaAnexa, str_AuxiliarCuenta);
                str_Resultado[0] = lcf_CreacionFormulario.Lstr_CodigoResultado;
                if (String.Equals(lcf_CreacionFormulario.Lstr_CodigoResultado, "00"))
                {
                    lds_Formulario.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(lcf_CreacionFormulario.Lstr_RespuestaSchema)));
                    lds_Formulario.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(lcf_CreacionFormulario.Lstr_RespuestaXML)));
                    str_Resultado[1] = lds_Formulario.Tables["Table"].Rows[0]["IdRevelacion"].ToString();
                }

                str_MensajeSalida = lcf_CreacionFormulario.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";

            }
            return str_Resultado;
        }

        public string[] ufnCrearRevelacionPendiente(string str_PeriodoAnual, string str_PeriodoMensual, string str_Institucion,
            string str_Entidad, string str_IdOficina, string str_PlanCuentas, string str_ClaseCuenta, string str_Cuentas,
            string str_Concepto, string str_Justificacion, string str_EstadoRevelacion, string str_UsrCreacion,
            out string str_MensajeSalida, string str_RubroCuenta, string str_SubCuenta, string str_SubCuentaAnexa, string str_AuxiliarCuenta)
        {
            string[] str_Resultado = new string[2];
            string lstr_IdRevelacion = String.Empty;
            DataSet lds_Formulario = new DataSet();
            try
            {
                clsCrearRevelacionPendiente lcf_CreacionRevelacionPendiente = new clsCrearRevelacionPendiente(str_PeriodoAnual, 
                           str_PeriodoMensual, str_Institucion,
                    str_Entidad,  str_IdOficina, str_PlanCuentas, str_ClaseCuenta, str_Cuentas,
                    str_Concepto, str_Justificacion, str_EstadoRevelacion, str_UsrCreacion, str_RubroCuenta, str_SubCuenta, str_SubCuentaAnexa,str_AuxiliarCuenta);
                str_Resultado[0] = lcf_CreacionRevelacionPendiente.Lstr_CodigoResultado;
                if (String.Equals(lcf_CreacionRevelacionPendiente.Lstr_CodigoResultado, "00"))
                {
                    lds_Formulario.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(lcf_CreacionRevelacionPendiente.Lstr_RespuestaSchema)));
                    lds_Formulario.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(lcf_CreacionRevelacionPendiente.Lstr_RespuestaXML)));
                    str_Resultado[1] = lds_Formulario.Tables["Table"].Rows[0]["IdRevelacion"].ToString();
                }

                str_MensajeSalida = lcf_CreacionRevelacionPendiente.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";

            }
            return str_Resultado;
        }

        public string[] ufnAutorizarRevelacionPendiente(string str_IdRevelacionPendiente, out string str_MensajeSalida)
        {
            string[] str_Resultado = new string[2];
            DataSet lds_Formulario = new DataSet();
            str_Resultado[0] = "99";
            try
            {
                clsAutorizarRevelacionPendiente lcf_AutorizarRevelacionPendiente = new clsAutorizarRevelacionPendiente(str_IdRevelacionPendiente);
                
                if (String.Equals(lcf_AutorizarRevelacionPendiente.Lstr_CodigoResultado, "00"))
                {
                    lds_Formulario.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(lcf_AutorizarRevelacionPendiente.Lstr_RespuestaSchema)));
                    lds_Formulario.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(lcf_AutorizarRevelacionPendiente.Lstr_RespuestaXML)));
                    str_Resultado[0] = lcf_AutorizarRevelacionPendiente.Lstr_CodigoResultado;
                    str_Resultado[1] = lcf_AutorizarRevelacionPendiente.Lstr_MensajeRespuesta;
                }

                str_MensajeSalida = lcf_AutorizarRevelacionPendiente.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";
                str_Resultado[1] = "No ha sido posible acceder a la base de datos";
            }
            return str_Resultado;
        }


        public string[] ufnEliminarRevelacionPendiente(string str_IdRevelacionPendiente, out string str_MensajeSalida)
        {
            string[] str_Resultado = new string[2];
            DataSet lds_Formulario = new DataSet();
            str_Resultado[0] = "99";
            try
            {
                clsEliminarRevelacionPendiente lcf_EliminarRevelacionPendiente = new clsEliminarRevelacionPendiente(str_IdRevelacionPendiente);

                if (String.Equals(lcf_EliminarRevelacionPendiente.Lstr_CodigoResultado, "00"))
                {
                    lds_Formulario.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(lcf_EliminarRevelacionPendiente.Lstr_RespuestaSchema)));
                    lds_Formulario.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(lcf_EliminarRevelacionPendiente.Lstr_RespuestaXML)));
                    str_Resultado[0] = lcf_EliminarRevelacionPendiente.Lstr_CodigoResultado;
                    str_Resultado[1] = lcf_EliminarRevelacionPendiente.Lstr_MensajeRespuesta;
                }

                str_MensajeSalida = lcf_EliminarRevelacionPendiente.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";
                str_Resultado[1] = "No ha sido posible acceder a la base de datos";
            }
            return str_Resultado;
        }

        public string[] ufnModificarRevelacionPendiente(string str_IdRevelacionPendiente, string str_Institucion,
            string str_Entidad, string str_IdOficina, string str_PlanCuentas, string str_ClaseCuenta, string str_Cuentas,
            string str_Concepto, string str_Justificacion, string str_EstadoRevelacion, 
            string str_FchModifica, string str_UsrModifica, out string str_MensajeSalida,
            string str_RubroCuenta, string str_SubCuenta, string str_SubCuentaAnexa, string str_AuxiliarCuenta)
        {
            string[] str_Resultado = new string[2];
            DataSet lds_Formulario = new DataSet();
            str_Resultado[0] = "99";
            try
            {
                clsModificarRevelacionPendiente lcf_ModificarRevelacionPendiente = new clsModificarRevelacionPendiente(str_IdRevelacionPendiente, 
                     str_Institucion, str_Entidad, str_IdOficina, str_PlanCuentas, str_ClaseCuenta, str_Cuentas,
                 str_Concepto,  str_Justificacion, str_EstadoRevelacion, str_FchModifica,  str_UsrModifica, str_RubroCuenta, str_SubCuenta, str_SubCuentaAnexa, str_AuxiliarCuenta);

                if (String.Equals(lcf_ModificarRevelacionPendiente.Lstr_CodigoResultado, "00"))
                {
                    lds_Formulario.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(lcf_ModificarRevelacionPendiente.Lstr_RespuestaSchema)));
                    lds_Formulario.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(lcf_ModificarRevelacionPendiente.Lstr_RespuestaXML)));
                    str_Resultado[0] = lcf_ModificarRevelacionPendiente.Lstr_CodigoResultado;
                    str_Resultado[1] = lcf_ModificarRevelacionPendiente.Lstr_MensajeRespuesta;
                }

                str_MensajeSalida = lcf_ModificarRevelacionPendiente.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";
                str_Resultado[1] = "No ha sido posible acceder a la base de datos";
            }
            return str_Resultado;
        }


        public Boolean ufnCrearFormularioExp(string str_PeriodoAnual, string str_PeriodoMensual,
            string str_Institucion, string str_PlanCuentas, string str_Concepto,
            string str_Justificacion, string str_NumExpediente, string str_HabilitadaPretencion, string str_EstadoRevelacion,
            string str_UsrCreacion, out string str_MensajeSalida)
        {
            Boolean lboo_ResCreacion = false;
            try
            {
                clsCrearFormulario lcf_CreacionFormulario = new clsCrearFormulario(str_PeriodoAnual, str_PeriodoMensual,
                    str_Institucion, str_PlanCuentas, str_Concepto, str_Justificacion, str_NumExpediente,
                    str_HabilitadaPretencion, str_EstadoRevelacion, str_UsrCreacion);
                if (String.Equals(lcf_CreacionFormulario.Lstr_CodigoResultado, "00"))
                {
                    lboo_ResCreacion = true;
                }
                str_MensajeSalida = lcf_CreacionFormulario.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";

            }
            return lboo_ResCreacion;
        }

        public Boolean ufnModificarFormulario(string str_IdRevelacion, string str_Institucion,
             string str_Entidad, string str_IdOficina, string str_GrupoCuentas, string str_Cuentas, string str_Concepto, string str_Justificacion,
             string str_EstadoRevelacion, string str_FchModifica, string str_UsrModifica, out string str_MensajeSalida,
            string str_RubroCuenta, string str_SubCuenta, string str_SubCuentaAnexa, string str_AuxiliarCuenta)
        {
            Boolean lboo_ResModificacion = false;
            try
            {
                clsModificarFormulario lcls_ModificarFormulario = new clsModificarFormulario(str_IdRevelacion, str_Institucion,
                    str_Entidad, str_IdOficina, str_GrupoCuentas, str_Cuentas, str_Concepto, str_Justificacion, str_EstadoRevelacion, str_FchModifica, str_UsrModifica,
                    str_RubroCuenta, str_SubCuenta, str_SubCuentaAnexa, str_AuxiliarCuenta);
                if (String.Equals(lcls_ModificarFormulario.Lstr_CodigoResultado, "00"))
                {
                    lboo_ResModificacion = true;
                }
                str_MensajeSalida = lcls_ModificarFormulario.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";

            }
            return lboo_ResModificacion;
        }
        public string ufnAutorizarCambiosRevelacion(string str_IdRevelacion, DateTime str_UltimoDiaMod, DateTime str_FchModifica, 
            string str_UsrModifica, out string str_MensajeSalida)
        {
            string lstr_ResModificacion = "99";
            try
            {
                clsAutorizarCambiosRevelacion lcls_AutorizarCambios = new clsAutorizarCambiosRevelacion(str_IdRevelacion,  str_UltimoDiaMod,
                    str_FchModifica, str_UsrModifica);

                     lstr_ResModificacion = lcls_AutorizarCambios.Lstr_CodigoResultado;
                    str_MensajeSalida = lcls_AutorizarCambios.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "Error en capa de logica de negocios";

            }
            return lstr_ResModificacion;
        }

        public string ufnActualizarFormularioExp(string str_NumExpediente, string str_HabilitadaPretencion, 
            string str_EstadoRevelacion, string str_FchModifica, string str_UsrModifica, out string str_MensajeSalida)
        {
            string lstr_ResModificacion = "99";
            try
            {
                clsActualizarFormularioExp lcls_ModificarFormulario = new clsActualizarFormularioExp(str_NumExpediente, str_HabilitadaPretencion,
                    str_EstadoRevelacion, str_FchModifica, str_UsrModifica);

                lstr_ResModificacion = lcls_ModificarFormulario.Lstr_CodigoResultado;
                str_MensajeSalida = lcls_ModificarFormulario.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";

            }
            return lstr_ResModificacion;
        }

        public string[] ufnActualizarObservacionesRevCont(string str_IdRevCont, string str_IdSociedadGL,
            string str_TipoProceso, string str_Observacion, string str_UsrModifica, string str_FchModifica)
        {
            string[] lstr_ResModificacion = new string[2];
            lstr_ResModificacion[0] = "99";
            try
            {
                clsActualizarObservacionesRevCont lcls_ActualizarObservacion = new clsActualizarObservacionesRevCont(str_IdRevCont,
                    str_IdSociedadGL, str_TipoProceso, str_Observacion, str_UsrModifica, str_FchModifica);
                lstr_ResModificacion[0] = lcls_ActualizarObservacion.Lstr_CodigoResultado;
                lstr_ResModificacion[1] = lcls_ActualizarObservacion.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                lstr_ResModificacion[1] = "Error en logica";

            }
            return lstr_ResModificacion;
        }

        public string[] ufnActualizarRevConTotalPasivos(string str_IdRevCont, string str_IdSociedadGL,
            string str_TipoProceso, Decimal dec_MontoPasivos, string str_CantExpPasivos, Decimal dec_MontoActivos, DateTime? str_FchModifica, Nullable<Int32> int_Proceso)
        {
            string[] lstr_ResModificacion = new string[2];
            lstr_ResModificacion[0] = "99";
            try
            {
                clsActualizarRevContTotalPasivos lcls_ActualizarPasivos = new clsActualizarRevContTotalPasivos(str_IdRevCont,
                    str_IdSociedadGL, str_TipoProceso, dec_MontoPasivos, str_CantExpPasivos, dec_MontoActivos, str_FchModifica, int_Proceso);
                lstr_ResModificacion[0] = lcls_ActualizarPasivos.Lstr_CodigoResultado;
                lstr_ResModificacion[1] = lcls_ActualizarPasivos.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                lstr_ResModificacion[1] = "Error en logica "+ex.ToString();

            }
            return lstr_ResModificacion;
        }

        public string[] ufnActualizarRevConTotalActivos(string str_IdRevCont, string str_IdSociedadGL,
            string str_TipoProceso, Decimal dec_MontoActivos, string str_CantExpActivos, Decimal dec_MontoPasivos, DateTime? str_FchModifica, Nullable<Int32> int_Proceso)
        {
            string[] lstr_ResModificacion = new string[2];
            lstr_ResModificacion[0] = "99";
            try
            {
                clsActualizarRevContTotalActivos lcls_ActualizarActivos = new clsActualizarRevContTotalActivos(str_IdRevCont,
                    str_IdSociedadGL, str_TipoProceso, dec_MontoActivos, str_CantExpActivos, dec_MontoPasivos, str_FchModifica, int_Proceso);
                lstr_ResModificacion[0] = lcls_ActualizarActivos.Lstr_CodigoResultado;
                lstr_ResModificacion[1] = lcls_ActualizarActivos.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                lstr_ResModificacion[1] = "Error en logica "+ex.ToString();

            }
            return lstr_ResModificacion;
        }

        public string ufnSubirArchivoRevelacion(string str_Nombre, string str_Tipo, byte[] byte_datos, int int_IdRevelacion, string str_Usuario)
        {
            string lstr_Resultado = String.Empty;
            try
            {
                clsSubirArchivoRevelacion cls_subirArchivo = new clsSubirArchivoRevelacion();
                lstr_Resultado = cls_subirArchivo.SubirArchivo(str_Nombre, str_Tipo, byte_datos, int_IdRevelacion, str_Usuario);
            }
            catch (Exception ex)
            {
                lstr_Resultado = "99";
            }
            return lstr_Resultado;
        }

        
        public tRevelacionNotas()
        { }
    }
}