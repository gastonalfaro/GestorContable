using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using LogicaNegocio.Consolidacion;
using LogicaNegocio.Seguridad;
using LogicaNegocio.Mantenimiento;
using log4net;
using log4net.Config;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Globalization;

namespace WebServicePlantillasConsolidacion
{
    /// <summary>
    /// Summary description for wsPlantillasConsolidacion
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsPlantillasConsolidacion : System.Web.Services.WebService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(wsPlantillasConsolidacion));

        #region Metodos

        clsConexionSAP cxnSAP = new clsConexionSAP();
        LogicaNegocio.wsSAPBC.ZINT_EST_CAB_BALANCE_CONSOL t_cabecera = new LogicaNegocio.wsSAPBC.ZINT_EST_CAB_BALANCE_CONSOL();
        LogicaNegocio.wsSAPBC.ZINT_EST_POS_BALANCE_CONSOL[] t_posicion = new LogicaNegocio.wsSAPBC.ZINT_EST_POS_BALANCE_CONSOL[1];

        [WebMethod]
        public DataSet uwsBalanceComprobacion(LogicaNegocio.wsSAPBC.ZINT_EST_CAB_BALANCE_CONSOL t_cabecera, LogicaNegocio.wsSAPBC.ZINT_EST_POS_BALANCE_CONSOL[] t_posicion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lstr_Respuesta = new DataSet();
            str_CodResultado = "00";
            str_Mensaje = "Proceso Finalizado";
            try
            {
                lstr_Respuesta = cxnSAP.RecibeBalanceComprobacion(t_cabecera, t_posicion);
            }
            catch(Exception e)
            {
                Log.Info(e.ToString() );

                str_CodResultado = "99";
                str_Mensaje = "Error "+e.ToString();
            }
            return lstr_Respuesta;
        }

        #region Archivo Anexo Estado Financiero
        [WebMethod]
        public DataSet BuscarArchivoAnexoEstadoFinanciero(string str_IdEstadoFinancieroArchivoAnexo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsArchivoAnexoEstadoFinanciero cls_ArchivoAnexoEstadoFinanciero = new clsArchivoAnexoEstadoFinanciero();
                lds_Resultado = cls_ArchivoAnexoEstadoFinanciero.BuscarArchivoAnexoEstadoFinanciero(str_IdEstadoFinancieroArchivoAnexo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception e)
            {
                str_CodResultado = "99";
                str_Mensaje = str_Mensaje + e.ToString();
            }


            return lds_Resultado;
        }

        [WebMethod]
        public bool EliminarArchivoAnexoEstadoFinanciero(string str_IdEstadoFinancieroArchivoAnexo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsArchivoAnexoEstadoFinanciero cls_ArchivoAnexoEstadoFinanciero = new clsArchivoAnexoEstadoFinanciero();
                lbl_Resultado = cls_ArchivoAnexoEstadoFinanciero.EliminarArchivoAnexoEstadoFinanciero(str_IdEstadoFinancieroArchivoAnexo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception e)
            {
                str_CodResultado = "99";
                str_Mensaje = str_Mensaje + e.ToString();
            }


            return lbl_Resultado;
        }

        [WebMethod]
        public DataSet InsertarArchivoAnexoEstadoFinanciero(string str_IdEntidad, int int_IdEstadoFinanciero, int int_Periodo, string str_UnidadTiempoPeriodo, string str_NombreArchivo, string str_TipoArchivo,
            int int_TamanoByteArchivo, DateTime dt_FechaArchivo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            DataSet lds_Resultado = new DataSet();
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsArchivoAnexoEstadoFinanciero cls_ArchivoAnexoEstadoFinanciero = new clsArchivoAnexoEstadoFinanciero();
                lds_Resultado = cls_ArchivoAnexoEstadoFinanciero.InsertarArchivoAnexoEstadoFinanciero(str_IdEntidad, int_IdEstadoFinanciero, int_Periodo, str_UnidadTiempoPeriodo, str_NombreArchivo, str_TipoArchivo, 
                    int_TamanoByteArchivo, dt_FechaArchivo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception e)
            {
                str_CodResultado = "99";
                str_Mensaje = str_Mensaje + e.ToString();
            }

            return lds_Resultado;
        }
        
        [WebMethod]
        public DataSet InsertarArchivoAnexoEstadoFinancieroFilestream(byte[] Buffer, string str_IdEntidad, int int_IdEstadoFinanciero, int int_Periodo, string str_UnidadTiempoPeriodo, string str_NombreArchivo, string str_TipoArchivo,
            int int_TamanoByteArchivo, DateTime dt_FechaArchivo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            DataSet lds_Resultado = new DataSet();
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsArchivoAnexoEstadoFinanciero cls_ArchivoAnexoEstadoFinanciero = new clsArchivoAnexoEstadoFinanciero();
                lds_Resultado = cls_ArchivoAnexoEstadoFinanciero.InsertarArchivoAnexoEstadoFinancieroFilestream(Buffer, str_IdEntidad, int_IdEstadoFinanciero, int_Periodo, str_UnidadTiempoPeriodo, str_NombreArchivo, str_TipoArchivo,
                    int_TamanoByteArchivo, dt_FechaArchivo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception e)
            {
                str_CodResultado = "99";
                str_Mensaje = str_Mensaje + e.ToString();
            }


            return lds_Resultado;
        }
        
        [WebMethod]
        public DataSet ConsultarArchivosAnexosEstadosFinancierosCargados(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            DataSet lds_Resultado = new DataSet();
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsArchivoAnexoEstadoFinanciero cls_ArchivoAnexoEstadoFinanciero = new clsArchivoAnexoEstadoFinanciero();
                lds_Resultado = cls_ArchivoAnexoEstadoFinanciero.ConsultarArchivosAnexosEstadosFinancierosCargados(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception e)
            {
                str_CodResultado = "99";
                str_Mensaje = str_Mensaje + e.ToString();
            }


            return lds_Resultado;
        }

        [WebMethod]
        public DataSet BuscarArchivoAnexoEstadoFinancieroFilestream(byte[] bt_Buffer, string str_IdEstadoFinancieroArchivoAnexo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsArchivoAnexoEstadoFinanciero cls_ArchivoAnexoEstadoFinanciero = new clsArchivoAnexoEstadoFinanciero();
                lds_Resultado = cls_ArchivoAnexoEstadoFinanciero.BuscarArchivoAnexoEstadoFinancieroFileStream(bt_Buffer, str_IdEstadoFinancieroArchivoAnexo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception e)
            {
                str_CodResultado = "99";
                str_Mensaje = str_Mensaje + e.ToString();
            }


            return lds_Resultado;
        }

        #endregion  

        #region Archivo Estado Financiero
        [WebMethod]
        public DataSet BuscarArchivoEstadoFinanciero(string str_IdEstadoFinancieroArchivo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsArchivoEstadoFinanciero cls_ArchivoEstadoFinanciero = new clsArchivoEstadoFinanciero();
                lds_Resultado = cls_ArchivoEstadoFinanciero.BuscarArchivoEstadoFinanciero(str_IdEstadoFinancieroArchivo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }

        [WebMethod]
        public bool EliminarArchivoEstadoFinanciero(string str_IdEstadoFinancieroArchivo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsArchivoEstadoFinanciero cls_ArchivoEstadoFinanciero = new clsArchivoEstadoFinanciero();
                lbl_Resultado = cls_ArchivoEstadoFinanciero.EliminarArchivoEstadoFinanciero(str_IdEstadoFinancieroArchivo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception e)
            {
                str_CodResultado = "99";
                str_Mensaje = str_Mensaje + e.ToString();
            }

            return lbl_Resultado;
        }

        [WebMethod]
        public DataSet InsertarArchivoEstadoFinanciero(string str_IdEntidad, int int_IdEstadoFinanciero, int int_Periodo, string str_UnidadTiempoPeriodo, string str_NombreArchivo, string str_TipoArchivo,
            int int_TamanoByteArchivo, DateTime dt_FechaArchivo, string str_Usuario, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            DataSet lds_Resultado = new DataSet();
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsArchivoEstadoFinanciero cls_ArchivoEstadoFinanciero = new clsArchivoEstadoFinanciero();
                lds_Resultado = cls_ArchivoEstadoFinanciero.InsertarArchivoEstadoFinanciero(str_IdEntidad, int_IdEstadoFinanciero, int_Periodo, str_UnidadTiempoPeriodo, str_NombreArchivo, str_TipoArchivo,
                    int_TamanoByteArchivo, dt_FechaArchivo, str_Usuario, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }

        [WebMethod]
        public DataSet InsertarArchivoEstadoFinancieroFileStream(byte[] Buffer, string str_IdEntidad, int int_IdEstadoFinanciero, int int_Periodo, string str_UnidadTiempoPeriodo, string str_NombreArchivo, string str_TipoArchivo,
            int int_TamanoByteArchivo, DateTime dt_FechaArchivo, string str_Usuario, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            DataSet lds_Resultado = new DataSet();
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsArchivoEstadoFinanciero cls_ArchivoEstadoFinanciero = new clsArchivoEstadoFinanciero();
                lds_Resultado = cls_ArchivoEstadoFinanciero.InsertarArchivoEstadoFinancieroFileStream(Buffer, str_IdEntidad, int_IdEstadoFinanciero, int_Periodo, str_UnidadTiempoPeriodo, str_NombreArchivo, str_TipoArchivo,
                    int_TamanoByteArchivo, dt_FechaArchivo, str_Usuario, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }

        [WebMethod]
        public DataSet BuscarArchivoEstadoFinancieroFilestream(string str_IdEstadoFinancieroArchivo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsArchivoEstadoFinanciero cls_ArchivoEstadoFinanciero = new clsArchivoEstadoFinanciero();
                lds_Resultado = cls_ArchivoEstadoFinanciero.BuscarArchivoEstadoFinancieroFilestream(str_IdEstadoFinancieroArchivo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        } 
        #endregion

        #region Archivo Estado Financiero Tamano Byte
        [WebMethod]
        public DataSet BuscarArchivoEstadoFinancieroTamanoByte(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsArchivoEstadoFinancieroTamanoByte cls_ArchivoEstadoFinancieroTamanoByte = new clsArchivoEstadoFinancieroTamanoByte();
                lds_Resultado = cls_ArchivoEstadoFinancieroTamanoByte.BuscarArchivoEstadoFinancieroTamanoByte(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }

        #endregion

        #region Archivo Plantilla Estado Financiero
        [WebMethod]
        public DataSet BuscarArchivoPlantillaEstadoFinanciero(string str_IdEstadoFinancieroArchivoPlantilla, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsArchivoPlantillaEstadoFinanciero cls_ArchivoPlantillaEstadoFinanciero = new clsArchivoPlantillaEstadoFinanciero();
                lds_Resultado = cls_ArchivoPlantillaEstadoFinanciero.BuscarArchivoPlantillaEstadoFinanciero(str_IdEstadoFinancieroArchivoPlantilla, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception e)
            {
                str_CodResultado = "99";
                str_Mensaje = str_Mensaje + e.ToString();
            }


            return lds_Resultado;
        }

        [WebMethod]
        public bool EliminarArchivoPlantillaEstadoFinanciero(string str_IdEstadoFinancieroArchivoPlantilla, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsArchivoPlantillaEstadoFinanciero cls_ArchivoPlantillaEstadoFinanciero = new clsArchivoPlantillaEstadoFinanciero();
                lbl_Resultado = cls_ArchivoPlantillaEstadoFinanciero.EliminarArchivoPlantillaEstadoFinanciero(str_IdEstadoFinancieroArchivoPlantilla, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception e)
            {
                str_CodResultado = "99";
                str_Mensaje = str_Mensaje + e.ToString();
            }


            return lbl_Resultado;
        }

        [WebMethod]
        public DataSet InsertarArchivoPlantillaEstadoFinanciero(int int_IdEstadoFinanciero, string str_NombreArchivo, string str_TipoArchivo,
            DateTime dt_FechaArchivo, string str_Usuario, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            DataSet lds_Resultado = new DataSet();
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsArchivoPlantillaEstadoFinanciero cls_ArchivoPlantillaEstadoFinanciero = new clsArchivoPlantillaEstadoFinanciero();
                lds_Resultado = cls_ArchivoPlantillaEstadoFinanciero.InsertarArchivoPlantillaEstadoFinanciero(int_IdEstadoFinanciero, str_NombreArchivo, str_TipoArchivo,
                    dt_FechaArchivo, str_Usuario, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception e)
            {
                str_CodResultado = "99";
                str_Mensaje = str_Mensaje + e.ToString();
            }


            return lds_Resultado;
        }
         
        [WebMethod]
        public DataSet InsertarArchivoPlantillaEstadoFinancieroFilestream(byte[] Buffer, int int_IdEstadoFinanciero, string str_NombreArchivo, string str_TipoArchivo,
            DateTime dt_FechaArchivo, string str_Usuario, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            DataSet lds_Resultado = new DataSet();
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsArchivoPlantillaEstadoFinanciero cls_ArchivoPlantillaEstadoFinanciero = new clsArchivoPlantillaEstadoFinanciero();
                lds_Resultado = cls_ArchivoPlantillaEstadoFinanciero.InsertarArchivoPlantillaEstadoFinancieroFilestream(Buffer, int_IdEstadoFinanciero, str_NombreArchivo, str_TipoArchivo,
                    dt_FechaArchivo, str_Usuario, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception e) 
            {
                str_CodResultado = "99";
                str_Mensaje = str_Mensaje + e.ToString();
            }

            return lds_Resultado;
        }


        [WebMethod]
        public string InsertarArchivoPlantillaEstadoFinancieroFilestreamS(byte[] Buffer, int int_IdEstadoFinanciero, string str_NombreArchivo, string str_TipoArchivo,
            DateTime dt_FechaArchivo, string str_Usuario, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            DataSet lds_Resultado = new DataSet();
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsArchivoPlantillaEstadoFinanciero cls_ArchivoPlantillaEstadoFinanciero = new clsArchivoPlantillaEstadoFinanciero();
                lds_Resultado = cls_ArchivoPlantillaEstadoFinanciero.InsertarArchivoPlantillaEstadoFinancieroFilestream(Buffer, int_IdEstadoFinanciero, str_NombreArchivo, str_TipoArchivo,
                    dt_FechaArchivo, str_Usuario, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception e)
            {
                str_CodResultado = "99";
                str_Mensaje = e.ToString();  
            }

            return str_CodResultado + " " +str_Mensaje;
        }


        [WebMethod]
        public DataSet ConsultarArchivosPlantillasEstadosFinancierosCargados(out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            DataSet lds_Resultado = new DataSet();
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsArchivoPlantillaEstadoFinanciero cls_ArchivoPlantillaEstadoFinanciero = new clsArchivoPlantillaEstadoFinanciero();
                lds_Resultado = cls_ArchivoPlantillaEstadoFinanciero.ConsultarArchivosPlantillasEstadosFinancierosCargados(str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception e)
            {
                str_CodResultado = "99";
                str_Mensaje = str_Mensaje + e.ToString();
            }


            return lds_Resultado;
        }
         

        #endregion

        #region Bitacora Flujo Errores DTSX
        [WebMethod]
        public DataSet BuscarBitacoraFlujoErroresDTSX(string str_NombreProceso, DateTime dt_FechaDe, DateTime dt_FechaHasta, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        { 
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsBitacoraFlujoErroresDTSX cls_BitacoraFlujoErroresDTSX = new clsBitacoraFlujoErroresDTSX();
                lds_Resultado = cls_BitacoraFlujoErroresDTSX.BuscarBitacoraFlujoErroresDTSX(str_NombreProceso, dt_FechaDe, dt_FechaHasta, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }

        [WebMethod]
        public DataSet ConsultarBitacoraFlujoErroresDTSX(out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsBitacoraFlujoErroresDTSX cls_BitacoraFlujoErroresDTSX = new clsBitacoraFlujoErroresDTSX();
                lds_Resultado = cls_BitacoraFlujoErroresDTSX.ConsultarBitacoraFlujoErroresDTSX(str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }
        #endregion

        #region Catalogo Etapa Estado Financiero
        [WebMethod]
        public DataSet BuscarCatalogoEtapaEstadoFinanciero(int int_IdEtapaEstadoFinanciero, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsCatalogoEtapaEstadoFinanciero cls_CatalogoEtapaEstadoFinanciero = new clsCatalogoEtapaEstadoFinanciero();
                lds_Resultado = cls_CatalogoEtapaEstadoFinanciero.BuscarCatalogoEtapaEstadoFinanciero(int_IdEtapaEstadoFinanciero, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }

        [WebMethod]
        public DataSet ConsultarCatalogoEtapaEstadoFinanciero(out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsCatalogoEtapaEstadoFinanciero cls_CatalogoEtapaEstadoFinanciero = new clsCatalogoEtapaEstadoFinanciero();
                lds_Resultado = cls_CatalogoEtapaEstadoFinanciero.ConsultarCatalogoEtapaEstadoFinanciero(str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }

        [WebMethod]
        public bool EliminarCatalogoEtapaEstadoFinanciero(int int_IdEtapaEstadoFinanciero, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsCatalogoEtapaEstadoFinanciero cls_CatalogoEtapaEstadoFinanciero = new clsCatalogoEtapaEstadoFinanciero();
                lbl_Resultado = cls_CatalogoEtapaEstadoFinanciero.EliminarCatalogoEtapaEstadoFinanciero(int_IdEtapaEstadoFinanciero, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lbl_Resultado;
        }

        [WebMethod]
        public bool InsertarCatalogoEtapaEstadoFinanciero(int int_IdEstadoFinanciero, string str_DescripEtapaEstadoFinanciero, out string str_CodResultado, out string str_Mensaje, string str_Usuario, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsCatalogoEtapaEstadoFinanciero cls_CatalogoEtapaEstadoFinanciero = new clsCatalogoEtapaEstadoFinanciero();
                lbl_Resultado = cls_CatalogoEtapaEstadoFinanciero.InsertarCatalogoEtapaEstadoFinanciero(int_IdEstadoFinanciero, str_DescripEtapaEstadoFinanciero, str_Usuario, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lbl_Resultado;
        }

        [WebMethod]
        public bool ModificarCatalogoEtapaEstadoFinanciero(int int_IdEstadoFinanciero, string str_DescripEtapaEstadoFinanciero, string str_Usuario, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsCatalogoEtapaEstadoFinanciero cls_CatalogoEtapaEstadoFinanciero = new clsCatalogoEtapaEstadoFinanciero();
                lbl_Resultado = cls_CatalogoEtapaEstadoFinanciero.ModificarCatalogoEtapaEstadoFinanciero(int_IdEstadoFinanciero, str_DescripEtapaEstadoFinanciero, str_Usuario, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lbl_Resultado;
        }
        #endregion

        #region Catalogo Unidades Tiempo Periodo
        [WebMethod]
        public DataSet BuscarCatalogoUnidadesTiempoPeriodo(string str_UnidadTiempoPeriodo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsCatalogoUnidadesTiempoPeriodo cls_CatalogoUnidadesTiempoPeriodo = new clsCatalogoUnidadesTiempoPeriodo();
                lds_Resultado = cls_CatalogoUnidadesTiempoPeriodo.BuscarCatalogoUnidadesTiempoPeriodo(str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }

        [WebMethod]
        public DataSet ConsultarCatalogoUnidadesTiempoPeriodo(out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsCatalogoUnidadesTiempoPeriodo cls_CatalogoUnidadesTiempoPeriodo = new clsCatalogoUnidadesTiempoPeriodo();
                lds_Resultado = cls_CatalogoUnidadesTiempoPeriodo.ConsultarCatalogoUnidadesTiempoPeriodo(str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }

        #endregion

        #region DTSX
        [WebMethod]
        public String[] uwsEjecutarDTSX(string str_DTSXPaqueteURL, string str_DTSXPaqueteNombre, string str_DTSXPaqueteVariable, bool str_bEjecutar64Bit, string str_Ruta32Bit, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] larr_Respuesta;
            bool lbool_Resultado = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;

            try
            {
                clsDTSX cls_DTSX = new clsDTSX();
                lbool_Resultado = cls_DTSX.EjecutarDTSX(str_DTSXPaqueteURL, str_DTSXPaqueteNombre, str_DTSXPaqueteVariable, str_bEjecutar64Bit, str_Ruta32Bit, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);

                larr_Respuesta = new String[2];
                larr_Respuesta[0] = lbool_Resultado.ToString();
                larr_Respuesta[1] = str_Mensaje;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                larr_Respuesta = new String[2];
                larr_Respuesta[0] = lbool_Resultado.ToString();
                larr_Respuesta[1] = ex.ToString();
            }

            return larr_Respuesta;
        }

        [WebMethod]
        public String[] uwsEjecutarDTSXBD(string str_DTSXPaqueteURL, string str_DTSXPaqueteNombre, string str_DTSXPaqueteVariable, bool str_bEjecutar64Bit, string str_Ruta32Bit, string str_Estado, string str_UsrCreacion, string str_DTSXFolderName, string str_DTSXProyecto, out string str_CodResultado, out string str_Mensaje)
        {
            String[] larr_Respuesta;
            bool lbool_Resultado = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;

            try
            {
                clsDTSX cls_DTSX = new clsDTSX();
                lbool_Resultado = cls_DTSX.EjecutarDTSXBD(str_DTSXPaqueteURL, str_DTSXPaqueteNombre, str_DTSXPaqueteVariable, str_bEjecutar64Bit, str_Ruta32Bit, str_Estado, str_UsrCreacion, str_DTSXFolderName, str_DTSXProyecto, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);

                larr_Respuesta = new String[2];
                larr_Respuesta[0] = str_CodResultado;
                larr_Respuesta[1] = str_Mensaje;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                larr_Respuesta = new String[2];
                larr_Respuesta[0] = "99";
                larr_Respuesta[1] = ex.ToString();
            }

            return larr_Respuesta;
        }

        [WebMethod]
        public String uwsPrueba()
        {
            string lstr_salida = string.Empty;

            /*  
DECLARE  @pvar NVARCHAR(500)  = 
'/SET \Package.Variables[User::PathExcelFile].Properties[Value];"'+'L:\SistemaGestor\Archivos_SistemaGestor\\'+'"' 
+ ' /SET \Package.Variables[User::NameExcelFile].Properties[Value];"21103T32018_ESTADO_BALANCE_COMPROBACION.xlsx"'
+ ' /SET \Package.Variables[User::UsuarioCarga].Properties[Value];"dmendez"'

SELECT @pvar
EXEC pc.uspEjecutarDTSX 'L:/SistemaGestor/DTSX_SistemaGestor/', 
'CargaEstadoFinancieroBalanceComprobacion.dtsx',
@pvar, 
0,
'', '' 
            */

            try
            {
                SqlConnection objSqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                objSqlCon.Open();
                SqlTransaction objSqlTran = objSqlCon.BeginTransaction();

                SqlCommand objSqlCmd = new SqlCommand("pc.uspEjecutarDTSX", objSqlCon, objSqlTran);
                objSqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter objSqlParamstrDTSXPaqueteURL = new SqlParameter("@pstrDTSXPaqueteURL", SqlDbType.VarChar, 200);
                objSqlParamstrDTSXPaqueteURL.Value = "L:/SistemaGestor/DTSX_SistemaGestor/";

                SqlParameter objSqlParamstrDTSXPaqueteNombre = new SqlParameter("@pstrDTSXPaqueteNombre", SqlDbType.VarChar, 100);
                objSqlParamstrDTSXPaqueteNombre.Value = "CargaEstadoFinancieroBalanceComprobacion.dtsx";// "CargaEstadoFinancieroBalanceComprobacion.dtsx";

                SqlParameter objSqlParamstrDTSXPaqueteVariable = new SqlParameter("@pstrDTSXPaqueteVariable", SqlDbType.VarChar, 500);
                objSqlParamstrDTSXPaqueteVariable.Value = "/SET \\Package.Variables[User::PathExcelFile].Properties[Value];\"L:\\SistemaGestor\\Archivos_SistemaGestor\\\\\" /SET \\Package.Variables[User::NameExcelFile].Properties[Value];\"" + "21103T32018_ESTADO_BALANCE_COMPROBACION.xlsx" + "\" /SET \\Package.Variables[User::UsuarioCarga].Properties[Value];\"dmendez\"";

                SqlParameter objSqlParamEjecutar64Bit = new SqlParameter("@pbEjecutar64Bit", SqlDbType.Bit);
                objSqlParamEjecutar64Bit.Value = 0;

                SqlParameter objSqlParamStatus = new SqlParameter("@pResultado", SqlDbType.VarChar, 2);
                objSqlParamStatus.Direction = ParameterDirection.Output;

                SqlParameter objSqlParamMessage = new SqlParameter("@pMensaje", SqlDbType.VarChar, 500);
                objSqlParamMessage.Direction = ParameterDirection.Output;

                objSqlCmd.Parameters.Add(objSqlParamstrDTSXPaqueteURL);
                objSqlCmd.Parameters.Add(objSqlParamstrDTSXPaqueteNombre);
                objSqlCmd.Parameters.Add(objSqlParamstrDTSXPaqueteVariable);
                objSqlCmd.Parameters.Add(objSqlParamEjecutar64Bit);
                objSqlCmd.Parameters.Add(objSqlParamStatus);
                objSqlCmd.Parameters.Add(objSqlParamMessage);

                DataSet Data = new DataSet();
                DataTable table = new DataTable();
                table.Load(objSqlCmd.ExecuteReader());
                Data.Tables.Add(table);

                //lstr_salida = new String[Data.Tables["Table1"].Rows.Count];

                //for (int i = 0; i <= Data.Tables["Table1"].Rows.Count - 1; i++)
                //{
                //    lstr_salida[i] = Data.Tables["Table1"].Rows[i]["FlowLine"].ToString();
                //}


                objSqlTran.Commit();
            }
            catch (Exception ex)
            {
                lstr_salida = "Excepcion:  " + ex.Message.ToString();
                return lstr_salida;
            }

            //catch (Exception ex)
            //{
            //    #region MensajeError
            //    EventLog.WriteEntry(ConfigurationManager.AppSettings["EventLogSource"].ToString(),
            //        //Obtiene el nombre de la clase.
            //    "NICSP"
            //        //Nombre del método.
            //    + "." + MethodInfo.GetCurrentMethod().Name
            //        //Error especifico.
            //    + ": Excepcion  " + ex.Message.ToString() + ". ",
            //    EventLogEntryType.Error);

            //    lstr_salida = "NICSP Excepcion:  " + ex.Message.ToString() + ". ";
            //    return lstr_salida;
            //    #endregion
            //}

            return lstr_salida;
        }

        [WebMethod]
        public String uwsPrueba2(string str_Estado, string str_UsrCreacion, string str_prueba)
        {
            DataSet lds_Resultado = new DataSet();
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;

            try
            {
                clsDTSX clsDTSX = new clsDTSX();
                lds_Resultado = clsDTSX.EjecutarDTSXPrueba(str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception e){
                return e.ToString();
            }

            return str_Mensaje;
        }

        [WebMethod]
        public String[] uwsEliminarArchivo(string str_DTSXPaqueteURL, string str_DTSXPaqueteVariable)
        {
            String[] larr_Respuesta;
            bool lbool_Resultado = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;

            try
            {
                clsEliminarArchivoExcel cls_EliminarArchivo = new clsEliminarArchivoExcel();
                lbool_Resultado = cls_EliminarArchivo.EliminarArchivoExcel(str_DTSXPaqueteURL, str_DTSXPaqueteVariable,  out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                larr_Respuesta = new String[2];
                larr_Respuesta[0] = str_CodResultado;
                larr_Respuesta[1] = str_Mensaje;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                larr_Respuesta = new String[2];
                larr_Respuesta[0] = "99";
                larr_Respuesta[1] = ex.ToString();
            }

            return larr_Respuesta;
        }

        #endregion

        #region Entidades de un Ambito
        [WebMethod]
        public DataSet uwsBuscarEntidadesDeUnAmbito(string str_IdAmbitoConsolidacion, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsEntidadesDeUnAmbito cls_EntidadesDeUnAmbito = new clsEntidadesDeUnAmbito();
                lds_Resultado = cls_EntidadesDeUnAmbito.BuscarEntidadesDeUnAmbito(str_IdAmbitoConsolidacion, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception e ){
                str_CodResultado = "99";
                str_Mensaje = e.ToString();
            }

            return lds_Resultado;
        }

        #endregion

        #region Estado Financiero
        [WebMethod]
        public DataSet uwsBuscarEstadoFinanciero(byte bt_IdEstadoFinanciero, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet ds_ResCreacion = new DataSet(); ;

            try
            {
                clsEstadoFinanciero ltro_BuscarEstadoFinanciero = new clsEstadoFinanciero();
                ds_ResCreacion = ltro_BuscarEstadoFinanciero.BuscarEstadoFinanciero(bt_IdEstadoFinanciero, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            return ds_ResCreacion;
        }

        [WebMethod]
        public DataSet ConsultarEstadoFinanciero(out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsEstadoFinanciero cls_EstadoFinanciero = new clsEstadoFinanciero();
                lds_Resultado = cls_EstadoFinanciero.ConsultarEstadoFinanciero(str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }

        [WebMethod]
        public bool EliminarEstadoFinanciero(byte bt_IdEstadoFinanciero, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsEstadoFinanciero cls_EstadoFinanciero = new clsEstadoFinanciero();
                lbl_Resultado = cls_EstadoFinanciero.EliminarEstadoFinanciero(bt_IdEstadoFinanciero, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lbl_Resultado;
        }

        [WebMethod]
        public bool InsertarEstadoFinanciero(byte bt_IdEstadoFinanciero, string str_NombreEstadoFinanciero, string str_DescripcionEstadoFinanciero, string str_Usuario, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsEstadoFinanciero cls_EstadoFinanciero = new clsEstadoFinanciero();
                lbl_Resultado = cls_EstadoFinanciero.InsertarEstadoFinanciero(bt_IdEstadoFinanciero, str_NombreEstadoFinanciero, str_DescripcionEstadoFinanciero, str_Usuario, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lbl_Resultado;
        }

        [WebMethod]
        public bool ModificarEstadoFinanciero(byte bt_IdEstadoFinanciero, string str_NombreEstadoFinanciero, string str_DescripcionEstadoFinanciero, string str_Usuario, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsEstadoFinanciero cls_EstadoFinanciero = new clsEstadoFinanciero();
                lbl_Resultado = cls_EstadoFinanciero.ModificarEstadoFinanciero(bt_IdEstadoFinanciero, str_NombreEstadoFinanciero, str_DescripcionEstadoFinanciero, str_Usuario, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lbl_Resultado;
        }
        #endregion

        #region Estado Financiero Balance Comprobacion
        [WebMethod]
        public DataSet BuscarEstadoFinancieroBalanceComprobacion(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsEstadoFinancieroBalanceComprobacion cls_EstadoFinancieroBalanceComprobacion = new clsEstadoFinancieroBalanceComprobacion();
                lds_Resultado = cls_EstadoFinancieroBalanceComprobacion.BuscarEstadoFinancieroBalanceComprobacion(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }

        [WebMethod]
        public bool EliminarEstadoFinancieroBalanceComprobacion(byte bt_IdEstadoFinanciero, string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsEstadoFinancieroBalanceComprobacion cls_EstadoFinancieroBalanceComprobacion = new clsEstadoFinancieroBalanceComprobacion();
                lbl_Resultado = cls_EstadoFinancieroBalanceComprobacion.EliminarEstadoFinancieroBalanceComprobacion(bt_IdEstadoFinanciero, str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lbl_Resultado;
        }

        [WebMethod]
        public DataSet ValidarEstadoFinancieroBalanceComprobacion(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsEstadoFinancieroBalanceComprobacion cls_EstadoFinancieroBalanceComprobacion = new clsEstadoFinancieroBalanceComprobacion();
                lds_Resultado = cls_EstadoFinancieroBalanceComprobacion.ValidarEstadoFinancieroBalanceComprobacion(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }

        #endregion

        #region Estado Financiero Balance Comprobacion Para SIGAF
        [WebMethod]
        public DataSet BuscarEstadoFinancieroBalanceComprobacionParaSIGAF(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsEstadoFinancieroBalanceComprobacionParaSIGAF cls_EstadoFinancieroBalanceComprobacionParaSIGAF = new clsEstadoFinancieroBalanceComprobacionParaSIGAF();
                lds_Resultado = cls_EstadoFinancieroBalanceComprobacionParaSIGAF.BuscarEstadoFinancieroBalanceComprobacionParaSIGAF(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }

        #endregion

        #region Estado Financiero Cambio Patrimonio Neto
        [WebMethod]
        public bool EliminarEstadoFinancieroCambioPatrimonioNeto(byte bt_IdEstadoFinanciero, string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsEstadoFinancieroCambioPatrimonioNeto cls_EstadoFinancieroCambioPatrimonioNeto = new clsEstadoFinancieroCambioPatrimonioNeto();
                lbl_Resultado = cls_EstadoFinancieroCambioPatrimonioNeto.EliminarEstadoFinancieroCambioPatrimonioNeto(bt_IdEstadoFinanciero, str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lbl_Resultado;
        }

        [WebMethod]
        public DataSet ValidarEstadoFinancieroCambioPatrimonioNeto(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsEstadoFinancieroCambioPatrimonioNeto cls_EstadoFinancieroCambioPatrimonioNeto = new clsEstadoFinancieroCambioPatrimonioNeto();
                lds_Resultado = cls_EstadoFinancieroCambioPatrimonioNeto.ValidarEstadoFinancieroCambioPatrimonioNeto(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }
        #endregion

        #region Estado Financiero Deuda Publica
        [WebMethod]
        public DataSet BuscarEstadoFinancieroDeudaPublica(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsEstadoFinancierosDeudaPublica cls_EstadoFinancierosDeudaPublica = new clsEstadoFinancierosDeudaPublica();
                lds_Resultado = cls_EstadoFinancierosDeudaPublica.BuscarEstadosFinancierosDeudaPublica(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }

        [WebMethod]
        public bool EliminarEstadoFinancieroDeudaPublica(byte bt_IdEstadoFinanciero, string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsEstadoFinancierosDeudaPublica cls_EstadoFinancierosDeudaPublica = new clsEstadoFinancierosDeudaPublica();
                lbl_Resultado = cls_EstadoFinancierosDeudaPublica.EliminarEstadoFinancieroDeudaPublica(bt_IdEstadoFinanciero, str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lbl_Resultado;
        }

        [WebMethod]
        public DataSet ValidarEstadoFinancieroDeudaPublica(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsEstadoFinancierosDeudaPublica cls_EstadoFinancierosDeudaPublica = new clsEstadoFinancierosDeudaPublica();
                lds_Resultado = cls_EstadoFinancierosDeudaPublica.ValidarEstadoFinancierosDeudaPublica(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }

        #endregion

        #region Estado Financiero Flujo Efectivo
        [WebMethod]
        public bool EliminarEstadoFinancieroFlujoEfectivo(byte bt_IdEstadoFinanciero, string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsEstadoFinancieroFlujoEfectivo cls_EstadoFinancieroFlujoEfectivo = new clsEstadoFinancieroFlujoEfectivo();
                lbl_Resultado = cls_EstadoFinancieroFlujoEfectivo.EliminarEstadoFinancieroFlujoEfectivo(bt_IdEstadoFinanciero, str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lbl_Resultado;
        }

        [WebMethod]
        public DataSet ValidarEstadoFinancieroFlujoEfectivo(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsEstadoFinancieroFlujoEfectivo cls_EstadoFinancieroFlujoEfectivo = new clsEstadoFinancieroFlujoEfectivo();
                lds_Resultado = cls_EstadoFinancieroFlujoEfectivo.ValidarEstadoFinancieroFlujoEfectivo( str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }
        #endregion

        #region Estado Financieros Cargados
        [WebMethod]
        public DataSet ConsultarEstadoFinancierosCargados(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsEstadoFinancierosCargados cls_EstadoFinancierosCargados = new clsEstadoFinancierosCargados();
                lds_Resultado = cls_EstadoFinancierosCargados.ConsultarEstadoFinancierosCargados(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }

        [WebMethod]
        public DataSet ConsultarCorreosAutorizacionEstadosFinancierosCargados(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsEstadoFinancierosCargados cls_EstadoFinancierosCargados = new clsEstadoFinancierosCargados();
                lds_Resultado = cls_EstadoFinancierosCargados.ConsultarCorreosAutorizacionEstadosFinancierosCargados(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }
        
        #endregion

        #region Etapa Estado Financiero
        [WebMethod]
        public DataSet BuscarEtapaEstadoFinanciero(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, DateTime dt_FechaDeEtapaEstado, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsEtapaEstadoFinanciero cls_EtapaEstadoFinanciero = new clsEtapaEstadoFinanciero();
                lds_Resultado = cls_EtapaEstadoFinanciero.BuscarEtapaEstadoFinanciero( str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, dt_FechaDeEtapaEstado, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }

        [WebMethod]
        public DataSet ConsultarEtapaEstadoFinanciero(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsEtapaEstadoFinanciero cls_EtapaEstadoFinanciero = new clsEtapaEstadoFinanciero();
                lds_Resultado = cls_EtapaEstadoFinanciero.ConsultarEtapaEstadoFinanciero(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }

        [WebMethod]
        public bool EliminarEtapaEstadoFinanciero(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, int int_IdEtapasEstadoFinanciero, string str_NotaRazon, string str_UsuarioEtapaEstado, DateTime dt_FechaDeEtapaEstado, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsEtapaEstadoFinanciero cls_EtapaEstadoFinanciero = new clsEtapaEstadoFinanciero();
                lbl_Resultado = cls_EtapaEstadoFinanciero.EliminarEtapaEstadoFinanciero(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, dt_FechaDeEtapaEstado, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lbl_Resultado;
        }

        [WebMethod]
        public bool InsertarEtapaEstadoFinanciero(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, int int_IdEtapaEstadoFinanciero, string str_NotaRazon, string str_UsuarioEtapaEstado, DateTime dt_FechaDeEtapaEstado, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsEtapaEstadoFinanciero cls_EtapaEstadoFinanciero = new clsEtapaEstadoFinanciero();
                lbl_Resultado = cls_EtapaEstadoFinanciero.InsertarEtapaEstadoFinanciero(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, int_IdEtapaEstadoFinanciero, str_NotaRazon, str_UsuarioEtapaEstado, dt_FechaDeEtapaEstado, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lbl_Resultado;
        }

        [WebMethod]
        public bool ModificarEtapaEstadoFinanciero(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, int int_IdEtapasEstadoFinanciero, string str_NotaRazon, string str_UsuarioEtapaEstado, DateTime dt_FechaDeEtapaEstado, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            bool lbl_Resultado = false;

            try
            {
                clsEtapaEstadoFinanciero cls_EtapaEstadoFinanciero = new clsEtapaEstadoFinanciero();
                 lbl_Resultado = cls_EtapaEstadoFinanciero.ModificarEtapaEstadoFinanciero(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, int_IdEtapasEstadoFinanciero, str_NotaRazon, str_UsuarioEtapaEstado, dt_FechaDeEtapaEstado, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lbl_Resultado;
        }
        #endregion

        #region Unidad Tiempo Periodo Correcto Correo Autorizacion
        [WebMethod]
        public DataSet ValidarUnidadTiempoPeriodoCorrectoCorreoAutorizacion(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsUnidadTiempoPeriodoCorrectoCorreoAutorizacion cls_UnidadTiempoPeriodoCorrectoCorreoAutorizacion = new clsUnidadTiempoPeriodoCorrectoCorreoAutorizacion();
                lds_Resultado = cls_UnidadTiempoPeriodoCorrectoCorreoAutorizacion.ValidaCargaUnidadTiempoPeriodoCorrectoCorreoAutorizacion(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }
        #endregion

        #region Unidades Tiempo Periodo EnCurso Por Fecha
        [WebMethod]
        public DataSet BuscarUnidadesTiempoPeriodoEnCursoPorFecha(DateTime dt_Fecha, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsUnidadesTiempoPeriodoEnCursoPorFecha cls_UnidadesTiempoPeriodoEnCursoPorFecha = new clsUnidadesTiempoPeriodoEnCursoPorFecha();
                lds_Resultado = cls_UnidadesTiempoPeriodoEnCursoPorFecha.BuscarUnidadesTiempoPeriodoEnCursoPorFecha(dt_Fecha, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }
        #endregion

        #region Usuario
        [WebMethod]
        public DataSet BuscarUsuario(string str_IdUsuario, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsUsuario cls_Usuario = new clsUsuario();
                lds_Resultado = cls_Usuario.BuscarUsuario(str_IdUsuario, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }

        [WebMethod]
        public DataSet BuscarUsuariosPorRol(int int_IdRol, string str_IdSociedadGL, out string str_CodResultado, out string str_Mensaje, string str_Estado = null, string str_UsrCreacion = null)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_Resultado = new DataSet();

            try
            {
                clsUsuario cls_Usuario = new clsUsuario();
                lds_Resultado = cls_Usuario.BuscarUsuariosPorRol( int_IdRol, str_IdSociedadGL, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch { }

            return lds_Resultado;
        }

        #endregion

        #region Mandar Correo
        [WebMethod]
        public bool EnviarCorreoPC(int CorreoClientePort, string CorreoClienteHost, string CorreoNetworkCredentialUsuario, string CorreoNetworkCredentialPassWord, string str_CorreoFrom, string str_CorreoTo, string str_CorreoCC, string str_Mensaje, string str_Asunto)
        {
            try
            {
                bool MandoCorreo = false;
                clsEnviarCorreoPC cls_EnviarCorreoPC = new clsEnviarCorreoPC();
                MandoCorreo = cls_EnviarCorreoPC.EnviarCorreoPC(CorreoClientePort, CorreoClienteHost, CorreoNetworkCredentialUsuario, CorreoNetworkCredentialPassWord, str_CorreoFrom, str_CorreoTo, str_CorreoCC, str_Mensaje, str_Asunto);
                //Log.Info(Mensaje Error o de Procesado);

                return MandoCorreo;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }
        #endregion

        #endregion

    }

    
}