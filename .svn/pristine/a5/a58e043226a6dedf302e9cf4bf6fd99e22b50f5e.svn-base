using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Contigentes
{
    public class clsRegistrarEntidadPersona : clsProcedimientoAlmacenado
    {

        #region Variables
        string lstr_IdExpediente = string.Empty;
        string lstr_CedDemandado = string.Empty;
        string lstr_NombreDemandado = string.Empty;
        string lstr_CedActor = string.Empty;
        string lstr_NombreActor = string.Empty;
        string lstr_TipoEntidadPersona = string.Empty;
        string lstr_Porcentaje = string.Empty;
        string lstr_UsrCreacion = string.Empty;
        string lstr_UsrModificar = string.Empty;
        private string lstr_FchModifica = string.Empty;

       
        
        #endregion

        #region Asignaciones/Obtencionres
        public string Lstr_IdExpediente
        {
            get { return lstr_IdExpediente; }
            set { lstr_IdExpediente = value; }
        }
        public string Lstr_CedDemandado
        {
            get { return lstr_CedDemandado; }
            set { lstr_CedDemandado = value; }
        }
        public string Lstr_NombreDemandado
        {
            get { return lstr_NombreDemandado; }
            set { lstr_NombreDemandado = value; }
        }
        public string Lstr_CedActor
        {
            get { return lstr_CedActor; }
            set { lstr_CedActor = value; }
        }
        public string Lstr_NombreActor
        {
            get { return lstr_NombreActor; }
            set { lstr_NombreActor = value; }
        }
        public string Lstr_TipoEntidadPersona
        {
            get { return lstr_TipoEntidadPersona; }
            set { lstr_TipoEntidadPersona = value; }
        }
        public string Lstr_Porcentaje
        {
            get { return lstr_Porcentaje; }
            set { lstr_Porcentaje = value; }
        }

        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }
        public string Lstr_UsrModificar
        {
            get { return lstr_UsrModificar; }
            set { lstr_UsrModificar = value; }
        }
        public clsRegistrarEntidadPersona() { }
        public string Lstr_FchModifica
        {
            get { return lstr_FchModifica; }
            set { lstr_FchModifica = value; }
        }
        #endregion

        #region Metodos
         /// <summary>
        /// Resgistrar Entidad/Persona Configs
        /// </summary>
        /// <returns></returns>
        public bool ProcesarSPRegitsrarEntidadPersona (){

            string str_RutaArchivo=string.Empty;
            string str_DireccionConfigs = string.Empty;
            bool resultFlag=false;
            try { 
                  
                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];
                str_RutaArchivo = str_DireccionConfigs+"\\Contigentes\\Expedientes\\RegistrarEntidadPersona.config";
                  EjecucionSP(str_RutaArchivo, this);
                  resultFlag=true;
            }catch(Exception ex){
                resultFlag = false;
            }
           
            return true;
         }
        #endregion
    }
}