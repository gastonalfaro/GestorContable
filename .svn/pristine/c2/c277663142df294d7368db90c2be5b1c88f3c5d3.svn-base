using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CapturaIngresos
{
    public class clsConsultarPagosPorFormulario : clsProcedimientoAlmacenado
    {

        #region Parámetros

        private int? lint_IdFormulario;
        private int? lint_Anno;
        private int? lint_IdPago;
        private DateTime? ldt_FchPagoDesde;
        private DateTime? ldt_FchPagoHasta;
        private string lstr_IdInstitucion;
        private string lstr_IdServicio;
        //private string lstr_CtaMayor;
        private string lstr_IdOficina;
        //private string lstr_IdPosPre;
        private string lstr_IdMoneda;
        //private decimal ldec_Monto;
        private string lstr_Periodo;
        private string lstr_Estado;
        //private string lstr_TipoIdPersonaPago;
        //private string lstr_IdPersonaPago;
        //private string lstr_CtaCliente;       
        private string lstr_UsrCreacion;
        private DateTime ldt_FchCreacion;
        private string lstr_UsrModifica;
        private DateTime ldt_FchModifica;

        #endregion

        #region Obtención y asignación


        public int? Lint_IdFormulario
        {
            get { return lint_IdFormulario; }
            set { lint_IdFormulario = value; }
        }


        public int? Lint_Anno
        {
            get { return lint_Anno; }
            set { lint_Anno = value; }
        }

        public int? Lint_IdPago
        {
            get { return lint_IdPago; }
            set { lint_IdPago = value; }
        }

        public DateTime? Ldt_FchPagoDesde
        {
            get { return ldt_FchPagoDesde; }
            set { ldt_FchPagoDesde = value; }
        }

        public DateTime? Ldt_FchPagoHasta
        {
            get { return ldt_FchPagoHasta; }
            set { ldt_FchPagoHasta = value; }
        }


        public string Lstr_IdInstitucion
        {
            get { return lstr_IdInstitucion; }
            set { lstr_IdInstitucion = value; }
        }

        public string Lstr_IdServicio
        {
            get { return lstr_IdServicio; }
            set { lstr_IdServicio = value; }
        }

        //public string Lstr_CtaMayor
        //{
        //    get { return lstr_CtaMayor; }
        //    set { lstr_CtaMayor = value; }
        //}

        public string Lstr_IdOficina
        {
            get { return lstr_IdOficina; }
            set { lstr_IdOficina = value; }
        }

        //public string Lstr_IdPosPre
        //{
        //    get { return lstr_IdPosPre; }
        //    set { lstr_IdPosPre = value; }
        //}

        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }

        //public decimal Ldec_Monto
        //{
        //    get { return ldec_Monto; }
        //    set { ldec_Monto = value; }
        //}

        public string Lstr_Periodo
        {
            get { return lstr_Periodo; }
            set { lstr_Periodo = value; }
        }

        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        //public string Lstr_TipoIdPersonaPago
        //{
        //    get { return lstr_TipoIdPersonaPago; }
        //    set { lstr_TipoIdPersonaPago = value; }
        //}

        //public string Lstr_IdPersonaPago
        //{
        //    get { return lstr_IdPersonaPago; }
        //    set { lstr_IdPersonaPago = value; }
        //}

        //public string Lstr_CtaCliente
        //{
        //    get { return lstr_CtaCliente; }
        //    set { lstr_CtaCliente = value; }
        //}

        //public string Lstr_UsrCreacion
        //{
        //    get { return lstr_UsrCreacion; }
        //    set { lstr_UsrCreacion = value; }
        //}

        //public DateTime Ldt_FchCreacion
        //{
        //    get { return ldt_FchCreacion; }
        //    set { ldt_FchCreacion = value; }
        //}

        //public string Lstr_UsrModifica
        //{
        //    get { return lstr_UsrModifica; }
        //    set { lstr_UsrModifica = value; }
        //}

        //public DateTime Ldt_FchModifica
        //{
        //    get { return ldt_FchModifica; }
        //    set { ldt_FchModifica = value; }
        //}
        #endregion

        #region Constructor

        public clsConsultarPagosPorFormulario(int? lint_IdFormulario,
                                                 int? lint_Anno,
                                                 int? lint_IdPago,
                                                 DateTime? ldt_FchPagoDesde,
                                                 DateTime? ldt_FchPagoHasta,
                                                 string lstr_IdInstitucion,
                                                 string lstr_IdServicio,
                                                 //string lstr_CtaMayor,
                                                 string lstr_IdOficina,
                                                 //string lstr_IdPosPre,
                                                 string lstr_IdMoneda,
                                                 //decimal ldec_Monto,
                                                 string lstr_Periodo,
                                                 string lstr_Estado
                                                //string lstr_TipoIdPersonaPago,
                                                //string lstr_IdPersonaPago,
                                                //string lstr_CtaCliente,    
                                                 //string lstr_UsrCreacion,
                                                 //DateTime ldt_FchCreacion,
                                                 //string lstr_UsrModifica,
                                                 //DateTime ldt_FchModifica
            )
        {
            this.lint_IdFormulario = lint_IdFormulario;
            this.lint_Anno = lint_Anno;
            this.lint_IdPago = lint_IdPago;
            this.ldt_FchPagoDesde = ldt_FchPagoDesde;
            this.ldt_FchPagoHasta = ldt_FchPagoHasta;
            this.lstr_IdInstitucion = lstr_IdInstitucion;
            this.lstr_IdServicio = lstr_IdServicio;
            //this.lstr_CtaMayor = lstr_CtaMayor;
            this.lstr_IdOficina = lstr_IdOficina;
            //this.lstr_IdPosPre = lstr_IdPosPre;
            this.lstr_IdMoneda = lstr_IdMoneda;
            //this.ldec_Monto = ldec_Monto;
            this.lstr_Periodo = lstr_Periodo;
            this.lstr_Estado = lstr_Estado;
            //this.lstr_TipoIdPersonaPago = lstr_TipoIdPersonaPago;
            //this.lstr_IdPersonaPago = lstr_IdPersonaPago;
            //this.lstr_CtaCliente = lstr_CtaCliente;
            //this.lstr_UsrCreacion = lstr_UsrCreacion;
            //this.ldt_FchCreacion = ldt_FchCreacion;
            //this.lstr_UsrModifica = lstr_UsrModifica;
            //this.ldt_FchModifica = ldt_FchModifica;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CapturaIngresos\\ConsultarPagosPorFormulario.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }

            //EjecucionSP("C:\\Users\\anajera\\Documents\\Visual Studio 2013\\Projects\\SistemaGestorV3\\Datos\\ConexionSQL\\Configs\\CapturaIngresos\\InsertarPagosFormularioCaptura.config", this);
            //EjecucionSP("C:\\Versiones\\SistemaGestor\\ConexionSQL\\Configs\\Mantenimiento\\EstadoGiroEstimado.config", this);
        }

        #endregion

    }
}