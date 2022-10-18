using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogicaNegocio.Contingentes
{
    public class clsEntidadesPersonas
    {
        #region parametros

        /// <summary>
        /// Declaración e inicialización de variables de Clase Endtidades Personas
        /// </summary>

        private int lint_IdPersona; //Es el tipo de cambio del día en el que se ingresa el expediente
        private string lstr_IdExpediente; //número de expediente origen
        private string lstr_CedDemandado; //tipo de expediente
        private string lstr_NombreDemandado; //estado de expediente
        private string lstr_CedActor; //fecha de la demanda del expediente
        private string lstr_NombreActor; //tipo de proceso del expediente
        private int lint_TipoEntidadPersona; //monto del tramo

       
        

        #endregion
        
        #region constructores
       /// <summary>
        /// Constructor de la clase Expedientes, permite crear un expediente y almacenarlo en sistema
        /// </summary>
        
        public clsEntidadesPersonas(){}

        /// <summary>
        /// Constructor sobrecargado con información obligatoria de acreedores
        /// </summary>

        public clsEntidadesPersonas(int lint_IdPersona,string lstr_IdExpediente,string lstr_CedDemandado,string lstr_NombreDemandado,string lstr_CedActor,string lstr_NombreActor,int lint_TipoEntidadPersona,decimal ldec_Porcetaje,string lstr_UsrCreacion)
        {
            this.lint_IdPersona = lint_IdPersona;
            this.lstr_IdExpediente = lstr_IdExpediente;
            this.lstr_CedDemandado = lstr_CedDemandado;
            this.lstr_NombreDemandado = lstr_NombreDemandado;
            this.lstr_CedActor = lstr_CedActor;
            this.lstr_NombreActor = lstr_NombreActor;
            this.lint_TipoEntidadPersona = lint_TipoEntidadPersona;
            
        }

   

        #endregion

        #region obtención y asignación

        /// <summary>
        /// Obtención y asignación de datos
        /// </summary>
        /// 
        public int Lint_IdPersona
        {
            get { return lint_IdPersona; }
            set { lint_IdPersona = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Lstr_IdExpediente
        {
            get { return lstr_IdExpediente; }
            set { lstr_IdExpediente = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Lstr_CedDemandado
        {
            get { return lstr_CedDemandado; }
            set { lstr_CedDemandado = value; }
        }
        /// <summary>
        /// NombreDemandado
        /// </summary>
        public string Lstr_NombreDemandado
        {
            get { return lstr_NombreDemandado; }
            set { lstr_NombreDemandado = value; }
        }
        /// <summary>
        /// CedActor
        /// </summary>
        public string Lstr_CedActor
        {
            get { return lstr_CedActor; }
            set { lstr_CedActor = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Lstr_NombreActor
        {
            get { return lstr_NombreActor; }
            set { lstr_NombreActor = value; }
        }
        ///// <summary>
        ///// TipoEntidadPersona
        ///// </summary>
        //public string Lstr_TipoEntidadPersona
        //{
        //    get { return lstr_TipoEntidadPersona; }
        //    set { lstr_TipoEntidadPersona = value; }
        //}
        /// <summary>
        /// UsrCreacion
        /// </summary>
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }
        /// <summary>
        /// UsrModifica
        /// </summary>
        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Ldec_Porcetaje
        {
            get { return ldec_Porcetaje; }
            set { ldec_Porcetaje = value; }
        }
        /// <summary>
        /// TipoEntidadPersona
        /// </summary>
        public int Lint_TipoEntidadPersona
        {
            get { return lint_TipoEntidadPersona; }
            set { lint_TipoEntidadPersona = value; }
        }
        private decimal ldec_Porcetaje;
        private string lstr_UsrCreacion;//
        private string lstr_UsrModifica;//
        private string lstr_TipoEntidadPersona;

        #endregion


       
    }
}