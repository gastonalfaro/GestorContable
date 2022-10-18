using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Datos.ConexionSQL.Procedimientos;
using Datos.ConexionSQL.Procedimientos.SubirArchivo;

namespace Logica.SubirArchivo
{
    public class clsArchivoSubir
    {
       

        // Obtiene la lista de archivos
        public DataSet ufnObtenerListaArchivo()
        {
            DataSet camposTabla = new DataSet();
            clsArchivo l_listadoArchivos = new clsArchivo();
            try {
                  camposTabla=l_listadoArchivos.ObtenerListaArchivo();
            }catch(Exception e){

                //camposTabla.Tables[0].Rows[0];
            }
            

            return camposTabla;

            
        }

        // Guardamos los archivos en la BD
        public string[] ufnGuardarArchivo(string str_nombre, string str_tipoContenido,
            int int_size, byte[] b_data, long int_IdRevelacion, string str_IdResolucion, int int_IdFormulario, string str_IdExpediente, long lg_IdRevelacionPediente, string str_IdCobroPago, Int16 int_Anno, string str_userCreacion)
        {
            //string lstr_Codigo = string.Empty;
            //string lstr_Resultado = string.Empty;
            string[] lstr_resultInsertar = new string[2];
            clsArchivo l_subirArchivo = new clsArchivo();

            lstr_resultInsertar = l_subirArchivo.GuardarArchivo(str_nombre, str_tipoContenido,
                                              int_size, b_data, int_IdRevelacion,
                                              str_IdResolucion, int_IdFormulario, str_IdExpediente, lg_IdRevelacionPediente, str_IdCobroPago, int_Anno, str_userCreacion);
            return lstr_resultInsertar;

        }
        
        // Guardamos los archivos en la BD
        public string[] ufnGuardarArchivoContingente(string str_nombre, string str_tipoContenido,
            int int_size, byte[] b_data, long int_IdRevelacion, string str_IdResolucion, string str_IdSociedadGL, int int_IdFormulario, string str_IdExpediente, long lg_IdRevelacionPediente, string str_IdCobroPago, Int16 int_Anno, string str_userCreacion)
        {
            string lstr_Codigo = string.Empty;
            string lstr_Resultado = string.Empty;
            string[] lstr_resultInsertar = new string[2];
            clsArchivo l_subirArchivo = new clsArchivo();

            lstr_resultInsertar = l_subirArchivo.GuardarArchivoContingente(str_nombre, str_tipoContenido,
                                              int_size, b_data, int_IdRevelacion,
                                              str_IdResolucion, str_IdSociedadGL, int_IdFormulario, str_IdExpediente, lg_IdRevelacionPediente, str_IdCobroPago, int_Anno, str_userCreacion);
            return lstr_resultInsertar;

        }
        
        // Guardamos los archivos en la BD
        public string[] ufnGuardarArchivos(string str_nombre, string str_tipoContenido,
            int int_size, byte[] b_data, long int_IdRevelacion, string str_IdResolucion, int int_IdFormulario, string str_IdExpediente, long lg_IdRevelacionPediente, long lg_IdArchivoDeuda, string str_IdCobroPago, Int16 int_Anno, string str_userCreacion)
        {
            string lstr_Codigo = string.Empty;
            string lstr_Resultado = string.Empty;
            string[] lstr_resultInsertar = new string[2];
            clsArchivo l_subirArchivo = new clsArchivo();

            lstr_resultInsertar = l_subirArchivo.GuardarArchivos(str_nombre, str_tipoContenido,
                                              int_size, b_data, int_IdRevelacion,
                                              str_IdResolucion, int_IdFormulario, str_IdExpediente, lg_IdRevelacionPediente, lg_IdArchivoDeuda, str_IdCobroPago, int_Anno, str_userCreacion);
            return lstr_resultInsertar;

        }

        public DataSet ufnObtenerArchivoPorIdResolucion(String str_IdExpediente, String str_IdSociedad, int int_IdFormulario)
        {
            DataSet resultado = new DataSet();
            clsArchivo l_obtenerLista = new clsArchivo();
            resultado = l_obtenerLista.ObtenerArchivoPorIdResolucion(str_IdExpediente, str_IdSociedad, int_IdFormulario);
            return resultado;

        }

        public DataSet ufnObtenerArchivoPorId(string str_IdArchivo)
        {
            DataSet resultado = new DataSet();
            try
            {
                clsArchivo l_obtenerLista = new clsArchivo();
                resultado = l_obtenerLista.ObtenerArchivoPorIdArchivo(str_IdArchivo);
            }
            catch (Exception ex)
            { }
            return resultado;

        }

        public DataSet ufnObtenerArchivoPorIdArchivoDeuda(string str_IdArchivoDeuda)
        {
            DataSet resultado = new DataSet();
            try
            {
                clsArchivo l_obtenerLista = new clsArchivo();
                resultado = l_obtenerLista.ObtenerArchivoPorIdArchivoDeuda(str_IdArchivoDeuda);
            }
            catch (Exception ex)
            { }
            return resultado;

        }

        public DataSet ufnObtenerArchivoPorIdRevelacion(string str_IdRevelacion)
        {
            DataSet resultado = new DataSet();
            try
            {
                clsArchivo l_obtenerLista = new clsArchivo();
                resultado = l_obtenerLista.ObtenerArchivoPorIdRevelacion(str_IdRevelacion);
            }
            catch (Exception ex)
            { }
            return resultado;

        }

        public DataSet ufnObtenerArchivoPorIdRevelacionPendiente(string str_IdRevelacionPendiente)
        {
            DataSet resultado = new DataSet();
            try
            {
                clsArchivo l_obtenerLista = new clsArchivo();
                resultado = l_obtenerLista.ObtenerArchivoPorIdRevelacionPendiente(str_IdRevelacionPendiente);
            }
            catch (Exception ex)
            { }
            return resultado;

        }

        public DataSet ufnObtenerArchivoCapturaIngresos(int? int_IdFormulario, Int16? int_Anno, string str_TipoIdPersona = null, string str_IdPersona = null)
        {
            DataSet resultado = new DataSet();
            try
            {
                clsArchivo l_obtenerLista = new clsArchivo();
                resultado = l_obtenerLista.ObtenerArchivoCapturaIngresos(int_IdFormulario, int_Anno, str_TipoIdPersona , str_IdPersona );
            }
            catch (Exception ex)
            { }
            return resultado;

        }
        public string[] ufnEliminarArchivo(string str_IdArchivo, string dat_FchModifica)
        {
            string[] lstr_ResEliminacion = new string[2];
            try
            {
                clsEliminarArchivo lcls_Archivo = new clsEliminarArchivo(str_IdArchivo, dat_FchModifica);
                lstr_ResEliminacion[0] = lcls_Archivo.Lstr_CodigoResultado;
                lstr_ResEliminacion[1] = lcls_Archivo.Lstr_MensajeRespuesta;
            }
            catch
            {
                lstr_ResEliminacion[0] = "99";
                lstr_ResEliminacion[1] = "Error en Logica de Negocios";
            }
            return lstr_ResEliminacion;

        }
    }
}