using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Contigentes
{
    public class clsCrearAntiguedadDeSaldos : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private int? lint_IdAntiguedadSaldos;
        public int? Lint_IdAntiguedadSaldos
        {
            get { return lint_IdAntiguedadSaldos; }
            set { lint_IdAntiguedadSaldos = value; }
        }
        private int? lint_IdPrevisionIncobrables;
        public int? Lint_IdPrevisionIncobrables
        {
            get { return lint_IdPrevisionIncobrables; }
            set { lint_IdPrevisionIncobrables = value; }
        }

        private string lstr_IdResolucion;
        public string Lstr_IdResolucion
        {
            get { return lstr_IdResolucion; }
            set { lstr_IdResolucion = value; }
        }
        private string lstr_IdExpediente;
        public string Lstr_IdExpediente
        {
            get { return lstr_IdExpediente; }
            set { lstr_IdExpediente = value; }
        }
        private string lstr_DescripcionVencimiento;
        public string Lstr_DescripcionVencimiento
        {
            get { return lstr_DescripcionVencimiento; }
            set { lstr_DescripcionVencimiento = value; }
        }
        private int? lint_DiasDeCuenta;
        public int? Lint_DiasDeCuenta
        {
            get { return lint_DiasDeCuenta; }
            set { lint_DiasDeCuenta = value; }
        }
        private int? lint_MesesDeCuenta;
        public int? Lint_MesesDeCuenta
        {
            get { return lint_MesesDeCuenta; }
            set { lint_MesesDeCuenta = value; }
        }
        private decimal? ldec_MontoIncobrable;
        public decimal? Ldec_MontoIncobrable
        {
            get { return ldec_MontoIncobrable; }
            set { ldec_MontoIncobrable = value; }
        }
        
        private decimal? ldec_DiferenciaAjustar;
        public decimal? Ldec_DiferenciaAjustar
        {
            get { return ldec_DiferenciaAjustar; }
            set { ldec_DiferenciaAjustar = value; }
        }
        private decimal? ldec_PorcentajeIncobrable;
        public decimal? Ldec_PorcentajeIncobrable
        {
            get { return ldec_PorcentajeIncobrable; }
            set { ldec_PorcentajeIncobrable = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }
        private string lstr_Usuario;
        public string Lstr_Usuario
        {
            get { return lstr_Usuario; }
            set { lstr_Usuario = value; }
        }

        private int? lint_TmpIdAntiguedadSaldos;
        public int? Lint_TmpIdAntiguedadSaldos
        {
            get { return lint_TmpIdAntiguedadSaldos; }
            set { lint_TmpIdAntiguedadSaldos = value; }
        }

        #endregion

        #region Obtención y asignación


        #endregion

        #region Constructor

        public clsCrearAntiguedadDeSaldos(int? lint_IdAntiguedadSaldos, int? lint_IdPrevisionIncobrables, string lstr_IdResolucion, string lstr_IdExpediente, 
            string lstr_DescripcionVencimiento, int? lint_DiasDeCuenta, int? lint_MesesDeCuenta, decimal? ldec_MontoIncobrable, decimal? ldec_DiferenciaAjustar,
            decimal? ldec_PorcentajeIncobrable, string lstr_Estado, string lstr_Usuario)
        {
            this.lint_IdAntiguedadSaldos = lint_IdAntiguedadSaldos;
            this.lint_IdPrevisionIncobrables = lint_IdPrevisionIncobrables;
            this.lstr_IdResolucion = lstr_IdResolucion;
            this.lstr_IdExpediente = lstr_IdExpediente;
            this.lstr_DescripcionVencimiento = lstr_DescripcionVencimiento;
            this.lint_DiasDeCuenta = lint_DiasDeCuenta;
            this.lint_MesesDeCuenta = lint_MesesDeCuenta;
            this.ldec_MontoIncobrable = ldec_MontoIncobrable;
            this.ldec_DiferenciaAjustar = ldec_DiferenciaAjustar;
            this.ldec_PorcentajeIncobrable = ldec_PorcentajeIncobrable;
            this.lstr_Estado = lstr_Estado;
            this.lstr_Usuario = lstr_Usuario;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "Contigentes\\CrearAntiguedadDeSaldos.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}