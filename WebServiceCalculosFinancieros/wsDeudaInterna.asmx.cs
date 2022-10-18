using System;
using System.Globalization;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using LogicaNegocio.CalculosFinancieros.DeudaInterna;
using LogicaNegocio.Seguridad;
using LogicaNegocio.CalculosFinancieros;

namespace WebServiceCalculosFinancieros
{
    /// <summary>
    /// Summary description for wsDeudaInterna
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsDeudaInterna : System.Web.Services.WebService
    {
        #region Variables

        private static string lstr_formato_fecha = "dd/MM/yyyy";
        #endregion

        #region Almacenamiento de cupones y valores

        [WebMethod]
        public string ActualizarTitulosValoresValores(string lstr_FechaInicio, string lstr_FechaFin)
        {
            try
            {
                clsCalculosDeudaInterna lcls_CargaValoresCupones = new clsCalculosDeudaInterna();
                lcls_CargaValoresCupones.ActualizarTitulosValoresValores(lstr_FechaInicio, lstr_FechaFin);
                return "00 - Actualizacion Exitosa";

            }
            catch (Exception ex)
            {
                return "99 - "+ex.ToString();
            }
        }

        [WebMethod]
        public string CargarValoresCuponesSINPE(string lstr_FechaInicio, string lstr_FechaFin, string lint_TipoFecha)
        {
            
            string retorno = "";
            clsCalculosDeudaInterna lcls_CargaValoresCupones = new clsCalculosDeudaInterna();
            try
            {
                lcls_CargaValoresCupones.CargarValoresYCupones(lstr_FechaInicio, lstr_FechaFin, Convert.ToInt32(lint_TipoFecha));
                retorno = "00 - Carga exitosa.";
            }
            catch (Exception ex)
            {
                retorno = "99 - " + ex.ToString();
            }
            return retorno;
        }

        [WebMethod]
        public string CargarValoresCuponesRdeSINPE(string lstr_FechaInicio, string lstr_FechaFin, string lint_TipoFecha)
        {

            string retorno = "";
            clsCalculosDeudaInterna lcls_CargaValoresCupones = new clsCalculosDeudaInterna();
            try
            {
                lcls_CargaValoresCupones.CargarValoresYCuponesRDE(lstr_FechaInicio, lstr_FechaFin, Convert.ToInt32(lint_TipoFecha));
                retorno = "Carga exitosa.";
            }
            catch (Exception ex)
            {
                retorno = ex.ToString();
            }
            return retorno;
        }

        [WebMethod]
        public DataSet ConsultarTitulosValores(string lint_NumValor, string lstr_Nemotecnico, string lint_NumCupon, string lstr_Garantia, string lstr_IndicadorCupon, string lstr_Tipo, string lstr_TipoNegociacion, string lstr_EstadoValor, string ldt_FchInicio, string ldt_FchFin)
        {
            DataSet ldas_TitulosValores = new DataSet();
            int? numValor = null;
            string mensaje = "";

            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(lint_NumValor))
                        numValor = Convert.ToInt32(lint_NumValor);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }

                if (mensaje == "")
                {
                    clsTituloValor lcls_TituloValor = new clsTituloValor();
                    ldas_TitulosValores = lcls_TituloValor.ConsultarTituloValor(numValor, lstr_Nemotecnico, lint_NumCupon, lstr_Garantia, lstr_IndicadorCupon, lstr_Tipo, lstr_TipoNegociacion, lstr_EstadoValor, Convert.ToDateTime(ldt_FchInicio), Convert.ToDateTime(ldt_FchFin), string.Empty);
                }
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return ldas_TitulosValores;
        }

        [WebMethod]
        public DataSet ConsultarTitulosValorValores(string lint_NumValor, string lstr_Nemotecnico, string lint_NumCupon, string lstr_Garantia, string lstr_Tipo, string ldt_FchInicio, string ldt_FchFin)
        {
            DataSet ldas_TitulosValores = new DataSet();
            string mensaje = "";

            try
            {
                clsTituloValor lcls_TituloValor = new clsTituloValor();
                ldas_TitulosValores = lcls_TituloValor.ConsultarTituloValorValores(lint_NumValor, lstr_Nemotecnico, lint_NumCupon, lstr_Garantia, lstr_Tipo, ldt_FchInicio, ldt_FchFin);
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return ldas_TitulosValores;
        }

        //[WebMethod]
        //public DataSet ConsultarTitulosValores(string lstr_TpoFecha, string lstr_FchInicio, string lstr_FchFin)
        //{
        //    DataSet ldas_TitulosValores = new DataSet();
        //    string mensaje = "";

        //    try
        //    {
        //        clsTituloValor lcls_TituloValor = new clsTituloValor();
        //        ldas_TitulosValores = lcls_TituloValor.ConsultarTituloValor(null, null, lstr_TpoFecha, lstr_FchInicio, lstr_FchFin);
        //    }
        //    catch (Exception e)
        //    {
        //        mensaje = e.ToString();
        //    }

        //    return ldas_TitulosValores;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lint_NumValor">-</param>
        /// <param name="lstr_EstadoValor">-</param>
        /// <param name="lstr_NemoTecnico">-</param>
        /// <param name="lstr_Tipo">-</param>
        /// <param name="lstr_TipoNegociacion">-</param>
        /// <param name="lstr_Moneda">-</param>
        /// <param name="ldec_ValorFacial">-</param>
        /// <param name="ldt_FchValor">-</param>
        /// <param name="ldt_FchCancelacion">-</param>
        /// <param name="ldt_FchVencimiento">-</param>
        /// <param name="ldec_ValorTransadoBruto">-</param>
        /// <param name="ldec_ValorTransadoNeto">-</param>
        /// <param name="ldec_TasaBruta">-</param>
        /// <param name="ldec_TasaNeta">-</param>
        /// <param name="ldec_Margen">-</param>
        /// <param name="lint_NumEmisionSerie">-</param>
        /// <param name="lstr_EntidadCustodia">-</param>
        /// <param name="lstr_MotivoAnulacion">-</param>
        /// <param name="ldec_RendimientoPorDescuento">-</param>
        /// <param name="ldec_ImpuestoPagado">-</param>
        /// <param name="ldec_Premio">-</param>
        /// <param name="lstr_Origen">-</param>
        /// <param name="lstr_UsrCreacion">-</param>
        /// <returns></returns>
        [WebMethod]
        public string[] CrearTituloValor(int lint_NumValor, string lstr_EstadoValor, string lstr_NemoTecnico,
            string lstr_Tipo,string lstr_TipoNegociacion, string lstr_Moneda, decimal ldec_ValorFacial, string ldt_FchValor, string lstr_Plazo,
            string ldt_FchCancelacion, string ldt_FchVencimiento, decimal ldec_ValorTransadoBruto, decimal ldec_ValorTransadoNeto,
            decimal ldec_TasaBruta, decimal ldec_TasaNeta, decimal ldec_Margen, string lint_NumEmisionSerie, string lstr_Propietario,
            string lstr_EntidadCustodia, string ldt_FchCreacion, string lstr_SistemaNegociacion, string lstr_MotivoAnulacion,
            decimal ldec_RendimientoPorDescuento, decimal ldec_ImpuestoPagado, decimal ldec_Premio, string lstr_Origen,
            string lstr_UsrCreacion, string lstr_DescripcionNegociacion, string lstr_NumeroIdentificacion, string lstr_TipoIdentificacion)
        {
            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string[] mensaje;
            string periodo = String.Empty;

            int tituloexiste = 0; 

            try
            {
                DateTime FechaDefecto = Convert.ToDateTime("01/01/1900");
                DateTime FchValor = DateTime.ParseExact(ldt_FchValor, lstr_formato_fecha, CultureInfo.InvariantCulture);
                DateTime FchCancelacion = DateTime.ParseExact(ldt_FchCancelacion, lstr_formato_fecha, CultureInfo.InvariantCulture);
                DateTime FchVencimiento = DateTime.ParseExact(ldt_FchVencimiento, lstr_formato_fecha, CultureInfo.InvariantCulture);
                DateTime FchCreacion = DateTime.ParseExact(ldt_FchCreacion, lstr_formato_fecha, CultureInfo.InvariantCulture);

                DataTable ldat_TitulosValores = new DataTable();

                //periodo = Math.Round((FchVencimiento - FchValor).TotalDays).ToString();

                clsTituloValor lclsTituloValor = new clsTituloValor();

                LogicaNegocio.Mantenimiento.clsDinamico dinamico = new LogicaNegocio.Mantenimiento.clsDinamico();

                ldat_TitulosValores = dinamico.ConsultarDinamico("select * from cf.titulosvalores where nrovalor = "+lint_NumValor.ToString()+" and nemotecnico = '"+lstr_NemoTecnico.Trim()+"'").Tables[0]; //lclsTituloValor.ConsultarTituloValor(lint_NumValor.ToString(), lstr_NemoTecnico, "%", "%", "%", "01/01/1900", "01/01/5000").Tables[0];
                tituloexiste = ldat_TitulosValores.Rows.Count;
                
                mensaje = new string[2];

                if (tituloexiste == 0)
                {
                    
                    lclsTituloValor.CrearTituloValor(
                        lint_NumValor, 0, lstr_EstadoValor, lstr_NemoTecnico, lstr_Tipo, lstr_TipoNegociacion,
                        lstr_Moneda, ldec_ValorFacial, FchValor, lstr_Plazo, FchCancelacion,
                        FchVencimiento, FechaDefecto, ldec_ValorTransadoBruto, ldec_ValorTransadoNeto,
                        ldec_TasaBruta, ldec_TasaNeta, ldec_Margen, lint_NumEmisionSerie, FchCreacion, FechaDefecto, lstr_Propietario,
                        lstr_EntidadCustodia, lstr_SistemaNegociacion, lstr_MotivoAnulacion, ldec_RendimientoPorDescuento,
                        0, 0, 0, 0, 0, 0, ldec_ImpuestoPagado, ldec_Premio, lstr_Origen,
                        String.Empty, "V", lstr_Origen, lstr_Estado, lstr_UsrCreacion,lstr_DescripcionNegociacion,lstr_NumeroIdentificacion,lstr_TipoIdentificacion,
                        out codSalida, out txtSalida);

                    mensaje[0] = codSalida;
                    mensaje[1] = txtSalida;
                }
                else
                {
                    string lstr_IndicadorGarantia = ldat_TitulosValores.Rows[0]["IndicadorGarantia"].ToString().Trim().Equals("G") ? "G" : null;
                    string lstr_EstadoValor2 = ldat_TitulosValores.Rows[0]["EstadoValor"].ToString().Trim().Equals("En Garantía") ? "En Garantía" : ldat_TitulosValores.Rows[0]["EstadoValor"].ToString().Trim();

                    DateTime FchModifica = Convert.ToDateTime(ldat_TitulosValores.Rows[0]["FchModifica"].ToString());

                    string consulta = "UPDATE [cf].[TitulosValores] SET [NroValor] = "+lint_NumValor+
                                    ",[NroCupon] = "+0+
                                    ",[EstadoValor] = '" + lstr_EstadoValor + "'" +
                                    ",[NemoTecnico] = '" + lstr_NemoTecnico + "'" +
                                    ",[Tipo] = '" + lstr_Tipo + "'" +
                                    ",[TipoNegociacion] = '" + lstr_TipoNegociacion + "'" +
                                    ",[Moneda] = '" + lstr_Moneda + "'" +
                                    ",[ValorFacial] = "+ldec_ValorFacial+
                                    ",[FchValor] = '" + FchValor.ToString("yyyy-MM-dd") + "'" +
                                    ",[PlazoValor] = '" + lstr_Plazo + "'" +
                                    ",[FchCancelacion] = '" + FchCancelacion.ToString("yyyy-MM-dd") + "'" +
                                    ",[FchVencimiento] = '" + FchVencimiento.ToString("yyyy-MM-dd") + "'" +
                                    ",[FchConstitucion] = '" + FchCreacion.ToString("yyyy-MM-dd") + "'" +
                                    ",[ValorTransadoBruto] = "+ldec_ValorTransadoBruto+
                                    ",[ValorTransadoNeto] = "+ldec_ValorTransadoNeto+
                                    ",[TasaBruta] = "+ldec_TasaBruta+
                                    ",[TasaNeta] = "+ldec_TasaNeta+
                                    ",[Margen] = "+ldec_Margen+
                                    ",[NroEmisionSerie] = '" + lint_NumEmisionSerie + "'" +
                                    ",[FchCreacionT] = '" + FchCreacion.ToString("yyyy-MM-dd") + "'" +
                                    ",[FchInicio] = '" + FchValor.ToString("yyyy-MM-dd") + "'" +
                                    " ,[Propietario] = '" + lstr_Propietario + "'" +
                                    " ,[EntidadCustodia] = '" + lstr_EntidadCustodia + "'" +
                                    " ,[SistemaNegociacion] = '" + lstr_SistemaNegociacion + "'" +
                                    " ,[MotivoAnulacion] = '" + lstr_MotivoAnulacion + "'" +
                                    " ,[RendimientoPorDescuento] = "+ldec_RendimientoPorDescuento+
                                    " ,[InteresBruto] = "+0+
                                    " ,[InteresBrutoEfectivo] = "+0+
                                    " ,[InteresNeto] = "+0+
                                    " ,[InteresNetoAcumulado] = "+0+
                                    " ,[ImpuestoVencido] = "+0+
                                    " ,[ImpuestoEfectivo] = "+0+
                                    " ,[ImpuestoPagado] = "+ldec_ImpuestoPagado+
                                    " ,[Premio] = "+ldec_Premio+
                                    " ,[ModuloSINPE] = '" + lstr_Origen + "'" +
                                    " ,[IndicadorGarantia] = '" + lstr_IndicadorGarantia + "'" +
                                    " ,[IndicadorCupon] = 'V'"+
                                    " ,[Origen] = '" + lstr_Origen + "'" +
                                    " ,[UsrModifica] = '" + lstr_UsrCreacion + "'" +
                                    " ,[FchModifica] = '"+DateTime.Today.ToString("yyyy-MM-dd")+"'"+
                                    " WHERE [NroValor] = "+lint_NumValor+
                                    " AND [NroCupon] = "+0+
                                    " AND [NemoTecnico] = '"+lstr_NemoTecnico+"'";

                    dinamico.ConsultarDinamico(consulta);


                    //lclsTituloValor.ModificarTituloValor(
                    //    lint_NumValor, 0, lstr_EstadoValor2, lstr_NemoTecnico, lstr_Tipo, lstr_TipoNegociacion,
                    //    lstr_Moneda, ldec_ValorFacial, FchValor, lstr_Plazo, FchCancelacion,
                    //    FchVencimiento, FechaDefecto, ldec_ValorTransadoBruto, ldec_ValorTransadoNeto,
                    //    ldec_TasaBruta, ldec_TasaNeta, ldec_Margen, lint_NumEmisionSerie, FechaDefecto, FechaDefecto, String.Empty,
                    //    lstr_EntidadCustodia, lstr_SistemaNegociacion, lstr_MotivoAnulacion, ldec_RendimientoPorDescuento,
                    //    0, 0, 0, 0, 0, 0, ldec_ImpuestoPagado, ldec_Premio, lstr_Origen,
                    //    lstr_IndicadorGarantia, "V", lstr_Origen, lstr_UsrCreacion, FchModifica,
                    //    out codSalida, out txtSalida);

                    //mensaje[0] = codSalida;
                    //mensaje[1] = txtSalida;
                }
            }
            catch (Exception e)
            {
                mensaje = new string[1];
                mensaje[0] = e.ToString();
            }

            return mensaje;
        }

        [WebMethod]
        public string[] CrearTituloReclasificado(int lint_NumValor, string lstr_NemoTecnico, string lstr_Tipo, string lstr_Moneda, decimal ldec_ValorFacial,
            decimal ldec_ValorTransadoBruto, decimal ldec_ValorTransadoNeto, DateTime ldt_FchValor, DateTime ldt_FchCancelacion, DateTime ldt_FchVencimiento,
            string lstr_SistemaNegociacion, string lstr_Estado, string lstr_UsrCreacion)
        {
            string[] mensaje;
            try
            {
                mensaje = new string[2];
                DataTable ldat_TitulosValores = new DataTable();
                clsTituloReclasificado lcls_TituloReclasificado = new clsTituloReclasificado();

                lcls_TituloReclasificado.CrearTituloReclasificado(lint_NumValor, lstr_NemoTecnico,
                    lstr_Tipo, lstr_Moneda, ldec_ValorFacial, ldec_ValorTransadoBruto, ldec_ValorTransadoNeto,
                    ldt_FchValor, ldt_FchCancelacion, ldt_FchVencimiento, lstr_SistemaNegociacion,
                    lstr_Estado, lstr_UsrCreacion, out mensaje[0], out mensaje[1]);
            }
            catch (Exception e)
            {
                mensaje = new string[1]; 
                mensaje[0] = e.ToString();
            }

            return mensaje;
        }


        [WebMethod]
        public DataSet ConsultarTitulosReclasificados(string lint_NumValor, string lstr_Nemotecnico, DateTime ldt_FchInicio, DateTime ldt_FchFin)
        {
            try
            {
                clsTituloReclasificado lcls_TituloReclasificado = new clsTituloReclasificado();
                return lcls_TituloReclasificado.ConsultarTituloReclasificado(lint_NumValor, lstr_Nemotecnico, ldt_FchInicio, ldt_FchFin);
            }
            catch (Exception e)
            {

            }
            return null;
        }

        #endregion

        #region Título en garantía

        /// <summary>
        /// Crea un título en garantía en el sistema gestor
        /// </summary>
        /// <param name="lint_NumValor"></param>
        /// <param name="lstr_NemoTecnico"></param>
        /// <param name="lstr_Moneda"></param>
        /// <param name="ldec_ValorFacial"></param>
        /// <param name="ldt_FchCreacionT"></param>
        /// <param name="lstr_Propietario"></param>
        /// <param name="lstr_IndicadorGarantia"></param>
        /// <returns>Retorna un mensaje con el código y texto de la transacción</returns>
        [WebMethod]
        public string CrearTituloGarantia(int lint_NumValor, string lstr_EstadoValor, string lstr_NemoTecnico, string lstr_Moneda, decimal ldec_ValorFacial,
            string ldt_FchCreacionT, string lstr_Propietario, string lstr_IndicadorGarantia, string lstr_UsrCreacion)
        {
            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            int tituloexiste = 0; 

            try
            {
                DateTime FechaDefecto = Convert.ToDateTime("01/01/1900");
                DateTime FchCreacionT = DateTime.ParseExact(ldt_FchCreacionT, lstr_formato_fecha, CultureInfo.InvariantCulture);

                clsTituloValor lcls_TituloGarantia = new clsTituloValor();

                DataTable ldat_TitulosValores = new DataTable();
                ldat_TitulosValores = lcls_TituloGarantia.ConsultarTituloValor(lint_NumValor, lstr_NemoTecnico, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty,Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/5000"), string.Empty).Tables[0];
                tituloexiste = ldat_TitulosValores.Rows.Count;

                if (tituloexiste == 0)
                {
                    lcls_TituloGarantia.CrearTituloValor(
                    lint_NumValor, 0, lstr_EstadoValor, lstr_NemoTecnico, String.Empty, String.Empty,
                    lstr_Moneda, ldec_ValorFacial, FechaDefecto, String.Empty, FechaDefecto, FechaDefecto, FechaDefecto,
                    0, 0, 0, 0, 0, "0", FchCreacionT, FechaDefecto, lstr_Propietario, String.Empty, String.Empty, String.Empty, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, String.Empty, "G", String.Empty, String.Empty, lstr_Estado, lstr_UsrCreacion, string.Empty, string.Empty, string.Empty,
                    out codSalida, out txtSalida);

                    mensaje = "Código " + codSalida + ": " + txtSalida;
                }
                else
                {
                    DateTime FchModifica = Convert.ToDateTime(ldat_TitulosValores.Rows[0]["FchModifica"].ToString());

                    lcls_TituloGarantia.ModificarTituloGarantia(
                        lint_NumValor, lstr_NemoTecnico, lstr_EstadoValor, "G", lstr_UsrCreacion, FchModifica,
                        out codSalida, out txtSalida);

                    mensaje = "Código " + codSalida + ": " + txtSalida;
                }


                //lcls_TituloGarantia.CrearTituloValor(
                //    lint_NumValor, 0, lstr_EstadoValor, lstr_NemoTecnico, String.Empty, String.Empty,
                //    lstr_Moneda, ldec_ValorFacial, FechaDefecto, String.Empty, FechaDefecto, FechaDefecto, FechaDefecto,
                //    0, 0, 0, 0, 0, "0", FchCreacionT, FechaDefecto, lstr_Propietario, String.Empty, String.Empty, String.Empty, 0,
                //    0, 0, 0, 0, 0, 0, 0, 0, String.Empty, "G", String.Empty, String.Empty, lstr_Estado, lstr_UsrCreacion,
                //    out codSalida, out txtSalida);

                //mensaje = "Código " + codSalida + ": " + txtSalida;
            }
            
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return mensaje;
        }

        [WebMethod]
        public string[] EliminarTituloGarantia(int lint_NumValor, string lstr_NemoTecnico, string lstr_Usuario)
        {
            string codSalida = String.Empty;
            string txtSalida = String.Empty;
            string[] str_Mensaje;

            try
            {
                clsTituloValor lcls_TituloGarantia = new clsTituloValor();
                str_Mensaje = new string[2];

                lcls_TituloGarantia.EliminarTituloGarantia(lint_NumValor, lstr_NemoTecnico, lstr_Usuario, out codSalida, out txtSalida);

                str_Mensaje[0] = codSalida;
                str_Mensaje[1] = txtSalida;
            }

            catch (Exception e)
            {
                str_Mensaje = new string[1];
                str_Mensaje[0] = e.ToString();
            }

            return str_Mensaje;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_NroValor"></param>
        /// <param name="lstr_NemoTecnico"></param>
        /// <param name="str_Tipo"></param>
        /// <param name="str_FchInicio"></param>
        /// <param name="str_FchFin"></param>
        /// <returns></returns>
        [WebMethod]
        public DataSet ConsultarTituloValorMant(string lstr_NroValor, string lstr_NemoTecnico, string str_Tipo, string str_FchInicio, string str_FchFin, string str_ExactaFecha)
        {
            DataSet ldas_FlujoEfectivo = new DataSet();
            string mensaje = "";
            DateTime? FchInicio = null;
            DateTime? FchFin = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(str_FchInicio))
                        FchInicio = DateTime.ParseExact(str_FchInicio, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(str_FchFin))
                        FchFin = DateTime.ParseExact(str_FchFin, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    mensaje = "La fecha debe ser en formato: " + lstr_formato_fecha;
                }

                if (mensaje == "")
                {
                    clsTituloValor lcls_TituloValor = new clsTituloValor();
                    ldas_FlujoEfectivo = lcls_TituloValor.ConsultarTituloValorMant(lstr_NroValor, lstr_NemoTecnico, str_Tipo, FchInicio, FchFin, str_ExactaFecha);
                }
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return ldas_FlujoEfectivo;
        }

        [WebMethod]
        public string ModificarTituloValorMant(int lint_NumValor, string lstr_NemoTecnico, string lstr_TasaVariable,
            decimal ldec_TasaVariableValor, decimal ldec_Margen, string lstr_UsrModifica, DateTime ldt_FchModifica)
        {
            string lstr_Resultado = String.Empty;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;

            try
            {
                clsTituloValor lcls_TituloValor = new clsTituloValor();

                lstr_Resultado = lcls_TituloValor.ModificarTituloValorMant(lint_NumValor, lstr_NemoTecnico, lstr_TasaVariable,
                    ldec_TasaVariableValor, ldec_Margen, lstr_UsrModifica, ldt_FchModifica, out str_CodResultado, out str_Mensaje);

               
            }

            catch (Exception e)
            {
                lstr_Resultado = e.ToString();
            }

            return lstr_Resultado;
        }

        [WebMethod]
        public string[] AnularTituloValor(int lint_NumValor, string lstr_NemoTecnico, string lstr_EstadoValor
            , string lstr_UsrModifica, DateTime ldt_FchModifica)
        {
            string[] lstr_Resultado;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;

            try
            {
                clsTituloValor lcls_TituloValor = new clsTituloValor();
                lstr_Resultado = new string[2];

                lcls_TituloValor.AnularTituloValor(lint_NumValor, lstr_NemoTecnico, lstr_EstadoValor
                    , lstr_UsrModifica, ldt_FchModifica, out str_CodResultado, out str_Mensaje);

                lstr_Resultado[0] = str_CodResultado;
                lstr_Resultado[1] = str_Mensaje;
            }

            catch (Exception e)
            {
                lstr_Resultado = new string[1];
                lstr_Resultado[0] = e.ToString();
            }

            return lstr_Resultado;
        }

        #endregion

        #region Canje

        [WebMethod]
        public string CrearCanje(int lint_NumValor, string lstr_EstadoValor, string lstr_NemoTecnico, string lstr_TipoNegociacion,
            string lstr_Moneda, decimal ldec_ValorFacial, string ldt_FchValor, string ldt_FchCancelacion,
            string ldt_FchVencimiento, decimal ldec_ValorTransadoBruto, decimal ldec_ValorTransadoNeto,
            decimal ldec_TasaBruta, decimal ldec_RendimientoPorDescuento, string lstr_UsrCreacion)
        {
            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";

            try
            {
                DateTime FechaDefecto = Convert.ToDateTime("01/01/1900");
                DateTime FchValor = DateTime.ParseExact(ldt_FchValor, lstr_formato_fecha, CultureInfo.InvariantCulture);
                DateTime FchCancelacion = DateTime.ParseExact(ldt_FchCancelacion, lstr_formato_fecha, CultureInfo.InvariantCulture);
                DateTime FchVencimiento = DateTime.ParseExact(ldt_FchVencimiento, lstr_formato_fecha, CultureInfo.InvariantCulture);

                clsTituloValor lcls_Canje = new clsTituloValor();
                lcls_Canje.CrearTituloValor(
                    lint_NumValor, 0, lstr_EstadoValor, lstr_NemoTecnico, String.Empty, lstr_TipoNegociacion,
                    lstr_Moneda, ldec_ValorFacial, FchValor, String.Empty, FchCancelacion,
                    FchVencimiento, FechaDefecto, ldec_ValorTransadoBruto, ldec_ValorTransadoNeto,
                    ldec_TasaBruta, 0, 0, "0", FechaDefecto, FechaDefecto,
                    String.Empty, String.Empty, String.Empty, String.Empty, ldec_RendimientoPorDescuento,
                    0, 0, 0, 0, 0, 0, 0, 0, String.Empty,
                    String.Empty, String.Empty, String.Empty, lstr_Estado, lstr_UsrCreacion, string.Empty, string.Empty, string.Empty,
                    out codSalida, out txtSalida);

                mensaje = "Código " + codSalida + ": " + txtSalida;
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return mensaje;
        }


        [WebMethod]
        public string CrearTituloCanjeSubasta(string lstr_NroEmisionSerie, int lint_NumValor, string lstr_NemoTecnico, string ldt_FchCanje, string lstr_TituloCompraEmision,
            string lstr_UsrCreacion)
        {
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";

            try
            {
                clsCalculosDeudaInterna lcls_DeudaInterna = new clsCalculosDeudaInterna();
                lcls_DeudaInterna.CrearTituloCanjeSubasta(lstr_NroEmisionSerie, lint_NumValor, lstr_NemoTecnico, ldt_FchCanje, lstr_TituloCompraEmision);


                string direccion = System.Configuration.ConfigurationManager.AppSettings["DireccionConfigs"];
                direccion += "log.txt";
                if (!System.IO.File.Exists(direccion))
                    System.IO.File.Create(direccion).Dispose();
                System.IO.File.AppendAllText(direccion, string.Format("{0}{1}", "WS", Environment.NewLine));

                mensaje = "Código " + codSalida + ": " + txtSalida;
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return mensaje;
        }



        [WebMethod]
        public string CalculaCanjeSubasta(string _CanjeSubasta, string _Fecha)
        {
            clsCalculosDeudaInterna lcls_DeudaInterna = new clsCalculosDeudaInterna();

            lcls_DeudaInterna.CalcularCanjeSubasta(_CanjeSubasta, _Fecha);

            return String.Empty;
        }


        [WebMethod]
        public string CrearAsientoCanje(string FechaCanje)
        
        {
            clsCalculosDeudaInterna lcls_DeudaInterna = new clsCalculosDeudaInterna();

            return lcls_DeudaInterna.CrearAsientoCanje(FechaCanje);

        }

        [WebMethod]
        public string CrearAsientoSubasta(string FechaSubasta)
        {
            clsCalculosDeudaInterna lcls_DeudaInterna = new clsCalculosDeudaInterna();

            return lcls_DeudaInterna.CrearAsientoSubasta(FechaSubasta);

        }

        #endregion

        #region Subasta Inversa

        [WebMethod]
        public string CrearSubastaInversa(int lint_NumValor, string lstr_EstadoValor, string lstr_NemoTecnico, string lstr_TipoNegociacion,
            string lstr_Moneda, decimal ldec_ValorFacial, string ldt_FchValor, string ldt_FchCancelacion,
            string ldt_FchVencimiento, string lstr_Propietario, decimal ldec_ValorTransadoBruto, decimal ldec_ValorTransadoNeto,
            decimal ldec_TasaBruta, decimal ldec_RendimientoPorDescuento, string lstr_UsrCreacion)
        {
            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";

            try
            {
                DateTime FechaDefecto = Convert.ToDateTime("01/01/1900");
                DateTime FchValor = DateTime.ParseExact(ldt_FchValor, lstr_formato_fecha, CultureInfo.InvariantCulture);
                DateTime FchCancelacion = DateTime.ParseExact(ldt_FchCancelacion, lstr_formato_fecha, CultureInfo.InvariantCulture);
                DateTime FchVencimiento = DateTime.ParseExact(ldt_FchVencimiento, lstr_formato_fecha, CultureInfo.InvariantCulture);

                clsTituloValor lcls_SubastaInversa = new clsTituloValor();
                lcls_SubastaInversa.CrearTituloValor(
                    lint_NumValor, 0, lstr_EstadoValor, lstr_NemoTecnico, String.Empty, lstr_TipoNegociacion,
                    lstr_Moneda, ldec_ValorFacial, FchValor, String.Empty, FchCancelacion,
                    FchVencimiento, FechaDefecto, ldec_ValorTransadoBruto, ldec_ValorTransadoNeto,
                    ldec_TasaBruta, 0, 0, "0", FechaDefecto, FechaDefecto, lstr_Propietario,
                    String.Empty, String.Empty, String.Empty, ldec_RendimientoPorDescuento,
                    0, 0, 0, 0, 0, 0, 0, 0, String.Empty,
                    String.Empty, String.Empty, String.Empty, lstr_Estado, lstr_UsrCreacion, string.Empty, string.Empty, string.Empty,
                    out codSalida, out txtSalida);

                mensaje = "Código " + codSalida + ": " + txtSalida;
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return mensaje;
        }

        #endregion

        #region Pagos a CCSS

        [WebMethod]
        public string[] CrearPagoCCSS(string lstr_EstadoValor, string lstr_NemoTecnico, string lstr_Tipo, string lstr_TipoNegociacion,
            int lint_NumValor, string lstr_Moneda, decimal ldec_ValorFacial, string ldt_FchValor, string lstr_PlazoValor, string ldt_FchCancelacion,
            string ldt_FchVencimiento, decimal ldec_ValorTransadoBruto, decimal ldec_ValorTransadoNeto, decimal ldec_TasaBruta,
            decimal ldec_TasaNeta, string lstr_NroEmisionSerie, string ldt_FchCreacionT, string lstr_SistemaNegociacion, string lstr_MotivoAnulacion,
            decimal ldec_RendimientoPorDescuento, decimal ldec_Premio, decimal ldec_ImpuestoPagado, string lstr_UsrCreacion, string lstr_ModuloSINPE)
        {
            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string[] mensaje;
            string periodo = String.Empty;

            try
            {
                mensaje = new string[2];
                DateTime FchValor = DateTime.ParseExact(ldt_FchValor, lstr_formato_fecha, CultureInfo.InvariantCulture);
                DateTime FchCancelacion = DateTime.ParseExact(ldt_FchCancelacion, lstr_formato_fecha, CultureInfo.InvariantCulture);
                DateTime FchVencimiento = DateTime.ParseExact(ldt_FchVencimiento, lstr_formato_fecha, CultureInfo.InvariantCulture);
                DateTime FchCreacionT = DateTime.ParseExact(ldt_FchCreacionT, lstr_formato_fecha, CultureInfo.InvariantCulture);

                periodo = Math.Round((FchVencimiento - FchValor).TotalDays).ToString();

                clsPagoCCSS lcls_PagoCCSS = new clsPagoCCSS();
                lcls_PagoCCSS.CrearPagoCCSS(lstr_EstadoValor, lstr_NemoTecnico, lstr_Tipo,
                    lstr_TipoNegociacion, lint_NumValor, lstr_Moneda, ldec_ValorFacial, FchValor, lstr_PlazoValor,
                    FchCancelacion, FchVencimiento, ldec_ValorTransadoBruto, ldec_ValorTransadoNeto, ldec_TasaBruta,
                    ldec_TasaNeta, lstr_NroEmisionSerie, FchCreacionT, lstr_SistemaNegociacion, lstr_MotivoAnulacion,
                    ldec_RendimientoPorDescuento, ldec_Premio, ldec_ImpuestoPagado, lstr_Estado, lstr_UsrCreacion,lstr_ModuloSINPE,
                    out codSalida, out txtSalida);

                mensaje[0] = codSalida;
                mensaje[1] = txtSalida;
            }

            catch (Exception e)
            {
                mensaje = new string[1];
                mensaje[0] = e.ToString();
            }

            return mensaje;
        }

        #endregion

        #region Traslado de cuota a Magisterio

        [WebMethod]
        public string[] CrearTrasladoMagisterio(string lstr_EstadoValor, string lstr_NemoTecnico, string lstr_Tipo, string lstr_TipoNegociacion,
            int lint_NumValor, string lstr_Moneda, decimal ldec_ValorFacial, string ldt_FchValor, string lstr_PlazoValor, string ldt_FchCancelacion,
            string ldt_FchVencimiento, decimal ldec_ValorTransadoBruto, decimal ldec_ValorTransadoNeto, decimal ldec_TasaBruta,
            decimal ldec_TasaNeta, string ldt_FchCreacionT, string lstr_Propietario, string lstr_SistemaNegociacion, string lstr_MotivoAnulacion,
            decimal ldec_RendimientoPorDescuento, decimal ldec_Premio, decimal ldec_ImpuestoPagado, string lstr_UsrCreacion, string lstr_ModuloSINPE,string lstr_EntidadCustodia)
        {
            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string[] mensaje;
            string periodo = String.Empty;

            try
            {
                mensaje = new string[2];
                DateTime FchValor = DateTime.ParseExact(ldt_FchValor, lstr_formato_fecha, CultureInfo.InvariantCulture);
                DateTime FchCancelacion = DateTime.ParseExact(ldt_FchCancelacion, lstr_formato_fecha, CultureInfo.InvariantCulture);
                DateTime FchVencimiento = DateTime.ParseExact(ldt_FchVencimiento, lstr_formato_fecha, CultureInfo.InvariantCulture);
                DateTime FchCreacionT = DateTime.ParseExact(ldt_FchCreacionT, lstr_formato_fecha, CultureInfo.InvariantCulture);

                periodo = Math.Round((FchVencimiento - FchValor).TotalDays).ToString();

                clsTrasladoMagisterio lcls_TrasladoMagisterio = new clsTrasladoMagisterio();
                lcls_TrasladoMagisterio.CrearTrasladoMagisterio(
                    lstr_EstadoValor, lstr_NemoTecnico, lstr_Tipo,
                    lstr_TipoNegociacion, lint_NumValor, lstr_Moneda, ldec_ValorFacial, FchValor, lstr_PlazoValor,
                    FchCancelacion, FchVencimiento, ldec_ValorTransadoBruto, ldec_ValorTransadoNeto, ldec_TasaBruta,
                    ldec_TasaNeta, FchCreacionT, lstr_Propietario, lstr_SistemaNegociacion, lstr_MotivoAnulacion,
                    ldec_RendimientoPorDescuento, ldec_Premio, ldec_ImpuestoPagado, lstr_Estado,lstr_ModuloSINPE, lstr_UsrCreacion,lstr_EntidadCustodia,
                    out codSalida, out txtSalida);

                mensaje[0] = codSalida;
                mensaje[1] = txtSalida;
            }
            catch (Exception e)
            {
                mensaje = new string[1];
                mensaje[0] = e.ToString();
            }

            return mensaje;
        }

        #endregion

        #region Neteos

        [WebMethod]
        public string CrearNeteo(int lint_NumValor, string lstr_EstadoValor, string lstr_NemoTecnico, string lstr_TipoNegociacion,
            string lstr_Moneda, decimal ldec_ValorFacial, string ldt_FchValor, string ldt_FchCancelacion,
            string ldt_FchVencimiento, string lstr_Propietario, decimal ldec_ValorTransadoBruto, decimal ldec_ValorTransadoNeto,
            decimal ldec_TasaBruta, decimal ldec_RendimientoPorDescuento, string lstr_UsrCreacion)
        {
            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";

            try
            {
                DateTime FechaDefecto = Convert.ToDateTime("01/01/1900");
                DateTime FchValor = DateTime.ParseExact(ldt_FchValor, lstr_formato_fecha, CultureInfo.InvariantCulture);
                DateTime FchCancelacion = DateTime.ParseExact(ldt_FchCancelacion, lstr_formato_fecha, CultureInfo.InvariantCulture);
                DateTime FchVencimiento = DateTime.ParseExact(ldt_FchVencimiento, lstr_formato_fecha, CultureInfo.InvariantCulture);

                clsTituloValor lcls_Neteo = new clsTituloValor();
                lcls_Neteo.CrearTituloValor(
                    lint_NumValor, 0, lstr_EstadoValor, lstr_NemoTecnico, String.Empty, lstr_TipoNegociacion,
                    lstr_Moneda, ldec_ValorFacial, FchValor, String.Empty, FchCancelacion,
                    FchVencimiento, FechaDefecto, ldec_ValorTransadoBruto, ldec_ValorTransadoNeto,
                    ldec_TasaBruta, 0, 0, "0", FechaDefecto, FechaDefecto, lstr_Propietario,
                    String.Empty, String.Empty, String.Empty, ldec_RendimientoPorDescuento,
                    0, 0, 0, 0, 0, 0, 0, 0, String.Empty,
                    String.Empty, String.Empty, String.Empty, lstr_Estado, lstr_UsrCreacion, string.Empty, string.Empty, string.Empty,
                    out codSalida, out txtSalida);

                mensaje = "Código " + codSalida + ": " + txtSalida;
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return mensaje;
        }

        #endregion

        #region Costo de Transaccion

        [WebMethod]
        public string[] EliminarCostoTransaccion(int lint_IdCostoTransaccion, string lstr_Usuario)
        {
            string[] mensaje;
            string txtSalida = "";
            string codSalida = "";
            try
            {
                mensaje = new string[2];
                clsCostoTransaccion lcls_CostoTransaccion = new clsCostoTransaccion();
                lcls_CostoTransaccion.EliminarCostoTransaccion(lint_IdCostoTransaccion, lstr_Usuario, out codSalida, out txtSalida);
                mensaje[0] = codSalida;
                mensaje[1] = txtSalida;
            }
            catch(Exception ex)
            {
                mensaje = new string[1];
                mensaje[0] = ex.ToString();
            }
            return mensaje;
        }

        [WebMethod]
        public string[] CrearCostoTransaccion(string lint_NumValor, string lstr_NemoTecnico, string ldt_Fecha, string lstr_Moneda, decimal ldec_Monto, decimal ldec_MontoColones, decimal ldec_TpoCambio, string lstr_Detalle,
            string lstr_ModuloSINPE, string lstr_UsrCreacion)
        {
            string lstr_Estado = "A";
            string txtSalida = "";
            string codSalida = "";
            string[] mensaje;

            try
            {
                mensaje = new string[2];
                DateTime Fecha = DateTime.ParseExact(ldt_Fecha, lstr_formato_fecha, CultureInfo.InvariantCulture);

                clsCostoTransaccion lcls_CostoTransaccion = new clsCostoTransaccion();
                lcls_CostoTransaccion.CrearCostoTransaccion(lint_NumValor, lstr_NemoTecnico, Fecha, lstr_Moneda, ldec_Monto, ldec_MontoColones, ldec_TpoCambio, lstr_Detalle,
                    lstr_ModuloSINPE, lstr_Estado, lstr_UsrCreacion, out codSalida, out txtSalida);

                mensaje[0] = codSalida;
                mensaje[1] = txtSalida;
            }
            catch (Exception e)
            {
                mensaje = new string[1];
                mensaje[0] = e.ToString();
            }

            return mensaje;
        }

        [WebMethod]
        public string[] ModificarCostoTransaccion(int lint_IdCostoTransaccion, string lint_NumValor, string lstr_NemoTecnico, DateTime ldt_Fecha, string lstr_Moneda, decimal ldec_Monto, decimal ldec_MontoColones, decimal ldec_TpoCambio, string lstr_Detalle,
            string lstr_ModuloSINPE, string lstr_Estado, string lstr_UsrModifica, DateTime ldt_FchModifica)
        {
            string txtSalida = "";
            string codSalida = "";
            string[] mensaje;

            try
            {
                mensaje = new string[2];

                clsCostoTransaccion lcls_CostoTransaccion = new clsCostoTransaccion();
                lcls_CostoTransaccion.ModificarCostoTransaccion(lint_IdCostoTransaccion, lint_NumValor, lstr_NemoTecnico, ldt_Fecha, lstr_Moneda, ldec_Monto, ldec_MontoColones, ldec_TpoCambio, lstr_Detalle,
                    lstr_ModuloSINPE, lstr_Estado, lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);

                mensaje[0] = codSalida;
                mensaje[1] = txtSalida;
            }
            catch (Exception e)
            {
                mensaje = new string[1];
                mensaje[0] = e.ToString();
            }

            return mensaje;
        }

        [WebMethod]
        public DataSet ConsultarCostoTransaccion(string lint_IdCostoTransaccion, string lstr_NroValor, string lstr_NemoTecnico, string ldt_Fecha, string lstr_Estado)
        {
            if (lint_IdCostoTransaccion == "" || lint_IdCostoTransaccion == null) lint_IdCostoTransaccion = null;
            if (lstr_NroValor == "" || lstr_NroValor == null) lstr_NroValor = null;
            if (lstr_NemoTecnico == "" || lstr_NemoTecnico == null) lstr_NemoTecnico = null;
            if (ldt_Fecha == "" || ldt_Fecha == null) ldt_Fecha = null;
            if (lstr_Estado == "" || lstr_Estado == null) lstr_Estado = null;

            DataSet ldas_CostoTransaccion = new DataSet();
            string mensaje = "";

            try
            {
                clsCostoTransaccion lcls_CostoTransaccion = new clsCostoTransaccion();
                ldas_CostoTransaccion = lcls_CostoTransaccion.ConsultarCostoTransaccion(lint_IdCostoTransaccion, lstr_NroValor, lstr_NemoTecnico, ldt_Fecha, lstr_Estado);
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return ldas_CostoTransaccion;
        }

        [WebMethod]
        public string[] ContabilizarCalculosFinancieros(string lstr_NbrTabla, string lint_IdCostoTransaccion, string lstr_NroValor, string lstr_NemoTecnico, string lstr_Estado, string lstr_UsrModifica, DateTime ldt_FchModifica)
        {
            string[] mensaje;
            string txtSalida = "";
            string codSalida = "";
            try
            {
                mensaje = new string[2];
                clsCostoTransaccion lcls_CostoTransaccion = new clsCostoTransaccion();
                lcls_CostoTransaccion.ContabilizarCalculosFinancieros(lstr_NbrTabla, lint_IdCostoTransaccion, lstr_NroValor, lstr_NemoTecnico, lstr_Estado, lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);
                mensaje[0] = codSalida;
                mensaje[1] = txtSalida;
            }
            catch (Exception ex)
            {
                mensaje = new string[1];
                mensaje[0] = ex.ToString();
            }
            return mensaje;
        }

        #endregion

        #region Devengo de Intereses

        //[WebMethod]
        //public List<LogicaNegocio.CalculosFinancieros.PeriodosCupones> CuponesDevengo (string NroValor, string NemoTecnico)
        //{
        //    List<LogicaNegocio.CalculosFinancieros.PeriodosCupones> cupon = new List<LogicaNegocio.CalculosFinancieros.PeriodosCupones>();
        //    clsCalculosDeudaInterna calculos = new clsCalculosDeudaInterna();
        //    cupon = calculos.FechasCupones(NroValor, NemoTecnico);
        //    return cupon;
        //}

        //[WebMethod]
        //public List<LogicaNegocio.CalculosFinancieros.Cupones> CuponesTitulos(string NroValor, string NemoTecnico)
        //{
        //    List<LogicaNegocio.CalculosFinancieros.Cupones> cupon = new List<LogicaNegocio.CalculosFinancieros.Cupones>();
        //    clsCalculosDeudaInterna calculos = new clsCalculosDeudaInterna();
        //    cupon = calculos.CuponesPorTitulo(NroValor, NemoTecnico);
        //    return cupon;
        //}

        [WebMethod]
        public string CalculaDevengoValores(int opcion, int nrovalor, string nemotecnico, string exacto)
        {
            string str_Resultado = "00 - Proceso finalizado";
            try
            {
                clsCalculosDeudaInterna lcls_DeudaInterna = new clsCalculosDeudaInterna();
                switch (opcion)
                {
                    case 1: { lcls_DeudaInterna.DevengoCeroCupon(nrovalor, nemotecnico, exacto); break; }
                    case 2: { lcls_DeudaInterna.DevengoTasaFija(nrovalor, nemotecnico, exacto); break; }
                    case 3: { lcls_DeudaInterna.DevengoTasaVariable(nrovalor, nemotecnico, exacto); break; }
                    default:
                        {
                            lcls_DeudaInterna.DevengoCeroCupon(nrovalor, nemotecnico, exacto);
                            lcls_DeudaInterna.DevengoTasaFija(nrovalor, nemotecnico, exacto);
                            lcls_DeudaInterna.DevengoTasaVariable(nrovalor, nemotecnico, exacto); break;
                        }
                }
            }
            catch (Exception E)
            {
                str_Resultado = "99 - Error: " + E.ToString();
            }
            //lcls_DeudaInterna.DevengoSubastaCanje("Compra Canje", "Venta Canje");
            //lcls_DeudaInterna.DevengoSubastaCanje("Compra Subasta", "Venta Subasta");
            return str_Resultado;
        }

        [WebMethod]
        public DataSet ConsultarDevengoIntereses(string lstr_NroValor, string lstr_NemoTecnico)
        {
            DataSet ldas_DevengoIntereses = new DataSet();
            string mensaje = "";

            try
            {
                clsDevengoInteres lcls_DevengoInteres = new clsDevengoInteres();
                ldas_DevengoIntereses = lcls_DevengoInteres.ConsultarDevengoInteres(lstr_NroValor, lstr_NemoTecnico);
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }            return ldas_DevengoIntereses;
        }

        #endregion

        #region Flujo de Efectivo

        [WebMethod]
        public DataSet ConsultarFlujoEfectivo(string lstr_NroValor, string lstr_NemoTecnico)
        {
            DataSet ldas_FlujoEfectivo = new DataSet();
            string mensaje = "";

            try
            {
                clsCalculoFlujoEfectivo lcls_CalculoFlujoEfectivo = new clsCalculoFlujoEfectivo();
                ldas_FlujoEfectivo = lcls_CalculoFlujoEfectivo.ConsultarCalculoFlujoEfectivo(lstr_NroValor, lstr_NemoTecnico);
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return ldas_FlujoEfectivo;
        }

        #endregion

        #region Monto Reserva
        [WebMethod]
        public string ConsultaMontoReservaSAP(string str_IdReserva, string str_IdPosicion)
        {
            //wsMontoReservaSAP.ZWS_MONTO_RESERVA servicio = new wsMontoReservaSAP.ZWS_MONTO_RESERVA(); cucurucho
            //wsMontoReservaSAP.zws_zint_conf_monto_reserva servicio = new wsMontoReservaSAP.zws_zint_conf_monto_reserva();
            //wsMontoReservaSAP.ZWS_MONTO_RESERVA servicio = new wsMontoReservaSAP.ZWS_MONTO_RESERVA();
            //wsMontoReservaSAP.ZWS_ZINT_CONF_MONTO_RESERVA servicio = new wsMontoReservaSAP.ZWS_ZINT_CONF_MONTO_RESERVA();

            //cucurucho 03-10-2022 cambio Gaston *****************
            //wsMontoReservaSAP.ZWS_MONTO_RESERVA servicio = new wsMontoReservaSAP.ZWS_MONTO_RESERVA();
            wsMontoReservaSAP.ZWS_ZINT_CONF_MONTO_RESERVA servicio = new wsMontoReservaSAP.ZWS_ZINT_CONF_MONTO_RESERVA();
           // wsMontoReservaSAP.ZintConfMontoReserva metodo = new wsMontoReservaSAP.ZintConfMontoReserva();
            wsMontoReservaSAP.ZintConfMontoReserva metodo = new wsMontoReservaSAP.ZintConfMontoReserva();

            wsMontoReservaSAP.ZintConfMontoReservaResponse response = new wsMontoReservaSAP.ZintConfMontoReservaResponse();
            metodo.IReserva = str_IdReserva;
            metodo.IPosicion = str_IdPosicion;
            string lstr_user = System.Configuration.ConfigurationManager.AppSettings["USER_SAP"];//usuario
            string lstr_pass = System.Configuration.ConfigurationManager.AppSettings["PASS_SAP"];//contrasena
            servicio.Credentials = new System.Net.NetworkCredential(lstr_user, lstr_pass);
            response = servicio.ZintConfMontoReserva(metodo);
            return response.ESaldo.ToString().Trim();
        }
        #endregion

        #region Contabiliza datos de Deuda Interna
        [WebMethod]
        public string ContabilizaCCSS(String nemo_tecnico, Decimal nro_valor, Decimal _monto_efectivo_pagado, Decimal _2110201041, Decimal _2110201050, Decimal _2110201070, Decimal _2110201080, Decimal _2110201090, Decimal _2110201100, Decimal _2116421000, Decimal _2116422000, Decimal _2116423000, Decimal _2116424000)
        {
            clsContabilizarCCSS clsContCCSS = new clsContabilizarCCSS();
            return clsContCCSS.ContabilizacionCCSS(nemo_tecnico, nro_valor, _monto_efectivo_pagado, _2110201041, _2110201050, _2110201070, _2110201080, _2110201090, _2110201100, _2116421000, _2116422000, _2116423000, _2116424000);
        }

        [WebMethod]
        public string ContabilizaDevengoPorFecha(string lstr_FchInicio, string lstr_FchFin, string lstr_pNroValor = "", string lstr_pNemotecnico = "", string lstr_TipoConsulta = "")
        {

            string mensaje = "00 - Proceso Finalizado";
            DateTime? FchInicio = null;
            DateTime? FchFin = null;

            try
            {
                try
                {
                    FchInicio = DateTime.ParseExact(lstr_FchInicio, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    FchFin = DateTime.ParseExact(lstr_FchFin, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                }
                clsContabilizarDevengoInt devengo = new clsContabilizarDevengoInt();
                mensaje = devengo.DevengoPorFecha(FchInicio, FchFin, lstr_pNroValor, lstr_pNemotecnico, lstr_TipoConsulta);
            }
            catch (Exception e)
            {
                mensaje = "99 - Error: " + e.ToString();
            }

            return mensaje;
        }

        [WebMethod]
        public string ContabilizaMagisterio(int lint_NroValor, string lstr_Nemotecnico)
        {
            clsContabilizarMagisterio clsContMagist = new clsContabilizarMagisterio();
            return clsContMagist.ContabilizacionMagisterio(lint_NroValor, lstr_Nemotecnico);
        }

        [WebMethod]
        public string ContabilizaPrescripciones(string ldt_FchVencimiento)
        {//RAMSES//RAMSES
            string mensaje = "00 - Prescripciones contabilizadas correctamente";
            DateTime? FchVencimiento = null;
            try
            {
                try
                {
                    FchVencimiento = DateTime.ParseExact(ldt_FchVencimiento, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                }
                clsContabilizarPrescripciones clsContPrescripciones = new clsContabilizarPrescripciones();
                clsContPrescripciones.contabiliza_prescipciones(FchVencimiento);
                clsContPrescripciones.contabiliza_prescipcionesCupones(FchVencimiento);
            }
            catch (Exception e)
            {
                mensaje = "99 - Error: " + e.ToString();
            }

            return mensaje;
        }//FUNCION
        
        [WebMethod]
        public string contabilizaPagoCupones(string lstr_FchInicio, string lstr_FchFin, string lint_NroValor, string lstr_Nemotecnico)
        {
            string mensaje = "00 - Pago de cupones contabilizadas correctamente";
            DateTime? FchInicio = null;
            DateTime? FchFin = null;
            int? NroValor= null;
            try
            {
                try
                {
                    FchInicio = DateTime.ParseExact(lstr_FchInicio, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    FchFin = DateTime.ParseExact(lstr_FchFin, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    NroValor = Convert.ToInt32(lint_NroValor);
                }
                catch (Exception ex)
                {
                }
                clsContabilizarPagoCupones clsContPagoCupones = new clsContabilizarPagoCupones();
                mensaje = clsContPagoCupones.contabilizaPagoCupones(NroValor, lstr_Nemotecnico, FchInicio, FchFin);
            }
            catch (Exception e)
            {
                mensaje = "99 - Error: " + e.ToString();
            }

            return mensaje;
        }

        [WebMethod]
        public string ContabilizarReclasificarPlazos(string ldt_FchInicio, string ldt_FchFin, string lint_NroValor, string lstr_Nemotecnico)
        {
            string mensaje = "";
            DateTime? FchInicio = null;
            DateTime? FchFin = null;
            DateTime? FchEntrada = null;
            int? NroValor = null;
            lstr_formato_fecha = "d/M/yyyy";
            try
            {
                try
                {
                    FchEntrada = DateTime.ParseExact(ldt_FchInicio, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    FchInicio = DateTime.ParseExact(String.Format("{0}/{1}/{2}",1,FchEntrada.Value.Month,FchEntrada.Value.Year), lstr_formato_fecha, CultureInfo.InvariantCulture);
                    FchFin = DateTime.ParseExact(String.Format("{0}/{1}/{2}", DateTime.DaysInMonth(FchEntrada.Value.Year,FchEntrada.Value.Month), FchEntrada.Value.Month, FchEntrada.Value.Year), lstr_formato_fecha, CultureInfo.InvariantCulture);
                    NroValor = Convert.ToInt32(lint_NroValor);
                }
                catch (Exception ex)
                {
                    mensaje = ex.ToString();
                }
                reclasificacionPlazos clsreclasificacionPlazos = new reclasificacionPlazos();
                mensaje = clsreclasificacionPlazos.reclasifica_plazos(FchInicio, FchFin, NroValor, lstr_Nemotecnico);
            }
            catch (Exception e)
            {
                mensaje = "99 - Error: " + e.ToString();
            }

            return mensaje;
        }

        [WebMethod]
        public string ContabilizarDifCambiario(string ldt_FchContabilizacion, string lstr_Nemotecnico="")
        {
            string mensaje = "";
            DateTime? FchContabilizacion = null;
            try
            {
                if (!string.IsNullOrEmpty(ldt_FchContabilizacion))
                {
                    try
                    {
                        FchContabilizacion = DateTime.ParseExact(ldt_FchContabilizacion, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    }
                    catch (Exception ex)
                    {
                        mensaje = ex.ToString();
                    }
                }
                else
                {
                    mensaje = "99 - Error: Debe indicar fecha de fin de mes para diferencial cambiario";
                }
                if (mensaje == "")
                {
                        clsContabilizaDifCambiario clsDifCambiario = new clsContabilizaDifCambiario();
                        mensaje = clsDifCambiario.DifCambiario(FchContabilizacion, lstr_Nemotecnico);
                }
                
            }
            catch (Exception e)
            {
                mensaje = "99 - Error: " + e.ToString();
            }

            return mensaje;
        }

        [WebMethod]
        public String ContabilizarCancelacionAnticipada(String nemotecnico, Decimal num_valor, String fecha_cancelacion, Decimal valor_facial, Decimal transado_bruto, Decimal rendimiento_descuento, Decimal premio, Decimal impuesto_pagado)
        {
            String result = "exito";
            DateTime FchCancelacion;
            try
            {
                FchCancelacion = DateTime.ParseExact(fecha_cancelacion, lstr_formato_fecha, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                return "99 - Error " + ex.ToString();
            }
            clsContabilizarCancelacionAnticipada cls = new clsContabilizarCancelacionAnticipada();
            result = cls.Cancelacion(nemotecnico,num_valor,FchCancelacion,valor_facial, transado_bruto, rendimiento_descuento,premio,impuesto_pagado);
            return result;
        }

        [WebMethod]
        public String ContabilizarCancelacion(string lstr_FchInicio, string lstr_FchFin)
        {
            DateTime? FchInicio = null;
            DateTime? FchFin = null;
            try
            {
                FchInicio = DateTime.ParseExact(lstr_FchInicio, lstr_formato_fecha, CultureInfo.InvariantCulture);
                FchFin = DateTime.ParseExact(lstr_FchFin, lstr_formato_fecha, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                return "99 - Error " + ex.ToString();
            }
            clsContabilizarCancelaciones clsContCancelaciones = new clsContabilizarCancelaciones();
            return clsContCancelaciones.Cancelacion(FchInicio, FchFin);
        }

        [WebMethod]
        public String ContabilizarColocacion(string lstr_FchInicio, string lstr_FchFin, string lint_NroValor = "-1", string lstr_Nemotecnico = "",bool lbool_manual = false)
        {
            DateTime? FchInicio = null;
            DateTime? FchFin = null;
            string str_Mensaje = "";
            Int32? NroValor = -1;
            try
            {
                FchInicio = DateTime.ParseExact(lstr_FchInicio, lstr_formato_fecha, CultureInfo.InvariantCulture);
                FchFin = DateTime.ParseExact(lstr_FchFin, lstr_formato_fecha, CultureInfo.InvariantCulture);
                NroValor = Convert.ToInt32(lint_NroValor);
                clsContabilizarColocaciones clsContColocaciones = new clsContabilizarColocaciones();
                str_Mensaje = clsContColocaciones.Colocacion(FchInicio, FchFin, NroValor, lstr_Nemotecnico,lbool_manual);
            }
            catch (Exception ex)
            {
                return "99 - Error " + ex.ToString();
            }
            return str_Mensaje;
        }

        [WebMethod]
        public String ContabilizarCostoTransaccion()
        {
            try
            {
                clsContabilizarCostoTransaccion clsContCostTrans = new clsContabilizarCostoTransaccion();
                return clsContCostTrans.GeneraCostosTransaccion();

            }
            catch (Exception ex)
            {
                return "Error " + ex.ToString();
            }
        }

        [WebMethod]
        public String AjustaHistoria(string fechaFin) 
        { 
            try{
                clsCalculosDeudaInterna lcls_DeudaInterna = new clsCalculosDeudaInterna();
                lcls_DeudaInterna.AjustaHistoriaEmision(fechaFin);
                return "Finalizado"; 
             }
            catch(Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion
    }
}
