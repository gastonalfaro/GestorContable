using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CapturaIngresos
{
    public class clsConsultarFormulariosCapturaIngresos : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private int lint_IdFormulario;
        private Int16 lint_Anno;
        private string lstr_TipoIdPersona;
        private string lstr_IdPersona;
        //private string lstr_Correo;
        private string lstr_IdSociedadGL;
        private string lstr_IdElementoPEP;
        private string lstr_IdReservaPresupuestaria;
        private string lstr_NroExpediente;
        private string lstr_ConExpediente;
        private string lstr_Descripcion;
        private string lstr_CtaCliente;
        //private string lstr_Direccion;
        private string lstr_Estado;
        //private string lstr_Usuario;
        //private int lint_TmpIdFormulario;

        #endregion

        #region Obtención y asignación


        public int Lint_IdFormulario
        {
            get { return lint_IdFormulario; }
            set { lint_IdFormulario = value; }
        }


        public Int16 Lint_Anno
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

        //public string Lstr_Correo
        //{
        //    get { return lstr_Correo; }
        //    set { lstr_Correo = value; }
        //}

        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
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

        public string Lstr_ConExpediente
        {
            get { return lstr_ConExpediente; }
            set { lstr_ConExpediente = value; }
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


        //public string Lstr_Direccion
        //{
        //    get { return lstr_Direccion; }
        //    set { lstr_Direccion = value; }
        //}


        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        //public string Lstr_Usuario
        //{
        //    get { return lstr_Usuario; }
        //    set { lstr_Usuario = value; }
        //}

        //public int Lint_TmpIdFormulario
        //{
        //    get { return lint_TmpIdFormulario; }
        //    set { lint_TmpIdFormulario = value; }
        //}
        #endregion

        #region Constructor

        public clsConsultarFormulariosCapturaIngresos(int lint_IdFormulario, Int16 lint_Anno, string lstr_TipoIdPersona, string lstr_IdPersona,
               string lstr_IdSociedadGL, string lstr_IdElementoPEP, string lstr_IdReservaPresupuestaria, string lstr_NroExpediente, string lstr_ConExpediente, string lstr_Descripcion, string lstr_CtaCliente,
               string lstr_Estado)
        {
            this.lint_IdFormulario = lint_IdFormulario;
            this.lint_Anno = lint_Anno;
            this.lstr_TipoIdPersona = lstr_TipoIdPersona;
            this.lstr_IdPersona = lstr_IdPersona;
            //this.lstr_Correo = lstr_Correo;
            this.lstr_IdSociedadGL = lstr_IdSociedadGL;
            this.lstr_IdElementoPEP = lstr_IdElementoPEP;
            this.lstr_NroExpediente = lstr_NroExpediente;
            this.lstr_ConExpediente = lstr_ConExpediente;
            this.lstr_IdReservaPresupuestaria = lstr_IdReservaPresupuestaria;
            this.lstr_Descripcion = lstr_Descripcion;
            this.lstr_CtaCliente = lstr_CtaCliente;
            //this.lstr_Direccion = lstr_Direccion;
            this.lstr_Estado = lstr_Estado;
            //this.lstr_Usuario = lstr_Usuario;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CapturaIngresos\\ConsultarFormulariosCapturasIngresos.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }

        }

        #endregion
    }
}