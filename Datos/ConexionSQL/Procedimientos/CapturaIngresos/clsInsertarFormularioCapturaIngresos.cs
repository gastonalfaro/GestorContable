using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CapturaIngresos
{
    public class clsInsertarFormularioCapturaIngresos : clsProcedimientoAlmacenado
    {
        #region Parámetros

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

        #region Constructor

        public clsInsertarFormularioCapturaIngresos(int lint_IdFormulario, int lint_Anno, string lstr_TipoIdPersona, string lstr_IdPersona, string lstr_NomPersona, string lstr_TipoIdPersonaTramite, string lstr_IdPersonaTramite, string lstr_NomPersonaTramite,
               string lstr_Correo, string lstr_IdSociedadGL, string lstr_IdOficina, string lstr_IdBanco, string lstr_IdElementoPEP, string lstr_IdReservaPresupuestaria, string lstr_NroExpediente, string lstr_Descripcion, string lstr_CtaCliente,
               string lstr_Direccion, DateTime ldt_FchIngreso, DateTime ldt_FchImpreso, DateTime ldt_FchPago, DateTime ldt_FchContabilizado, DateTime ldt_FchAnulado, string lstr_Estado, string lstr_Observaciones, string lstr_IdMoneda, decimal ldec_Monto, string lstr_ReferenciaDTR, string lstr_Usuario)
        {
            this.lint_IdFormulario = lint_IdFormulario;
            this.lint_Anno = lint_Anno;
            this.lstr_TipoIdPersona = lstr_TipoIdPersona;
            this.lstr_IdPersona = lstr_IdPersona;
            this.lstr_NomPersona = lstr_NomPersona;
            this.lstr_TipoIdPersonaTramite = lstr_TipoIdPersonaTramite;
            this.lstr_IdPersonaTramite = lstr_IdPersonaTramite;
            this.lstr_NomPersonaTramite = lstr_NomPersonaTramite;
            this.lstr_Correo = lstr_Correo;
            this.lstr_IdSociedadGL = lstr_IdSociedadGL;
            this.lstr_IdOficina = lstr_IdOficina;
            this.lstr_IdBanco = lstr_IdBanco;
            this.lstr_IdElementoPEP = lstr_IdElementoPEP;
            this.lstr_NroExpediente = lstr_NroExpediente;
            this.lstr_IdReservaPresupuestaria = lstr_IdReservaPresupuestaria;
            this.lstr_Descripcion = lstr_Descripcion;
            this.lstr_CtaCliente = lstr_CtaCliente;
            this.lstr_Direccion = lstr_Direccion;
            this.ldt_FchIngreso = ldt_FchIngreso;
            this.ldt_FchImpreso = ldt_FchImpreso;
            this.ldt_FchPago = ldt_FchPago;
            this.ldt_FchContabilizado = ldt_FchContabilizado;
            this.ldt_FchAnulado = ldt_FchAnulado;
            this.lstr_Estado = lstr_Estado;
            this.lstr_Observaciones = lstr_Observaciones;
            this.lstr_IdMoneda = lstr_IdMoneda;
            this.ldec_Monto = ldec_Monto;
            this.lstr_ReferenciaDTR = lstr_ReferenciaDTR;
            this.lstr_Usuario = lstr_Usuario;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CapturaIngresos\\InsertarFormularioCapturaIngresos.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }

        }

        #endregion
    }
}