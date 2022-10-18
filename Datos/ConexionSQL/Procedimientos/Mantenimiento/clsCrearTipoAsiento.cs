using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearTipoAsiento : clsProcedimientoAlmacenado
    {
        private string lstr_Codigo;
        public string Lstr_Codigo
        {
            get { return lstr_Codigo; }
            set { lstr_Codigo = value; }
        }

        private string lstr_CodigoAuxiliar;
        public string Lstr_CodigoAuxiliar
        {
            get { return lstr_CodigoAuxiliar; }
            set { lstr_CodigoAuxiliar = value; }
        }

        private string lstr_CodigoAuxiliar2;
        public string Lstr_CodigoAuxiliar2
        {
            get { return lstr_CodigoAuxiliar2; }
            set { lstr_CodigoAuxiliar2 = value; }
        }

        private string lstr_CodigoAuxiliar3;
        public string Lstr_CodigoAuxiliar3
        {
            get { return lstr_CodigoAuxiliar3; }
            set { lstr_CodigoAuxiliar3 = value; }
        }

        private string lstr_CodigoAuxiliar4;
        public string Lstr_CodigoAuxiliar4
        {
            get { return lstr_CodigoAuxiliar4; }
            set { lstr_CodigoAuxiliar4 = value; }
        }
        private string lstr_CodigoAuxiliar5;
        public string Lstr_CodigoAuxiliar5
        {
            get { return lstr_CodigoAuxiliar5; }
            set { lstr_CodigoAuxiliar5 = value; }
        }
        private string lstr_CodigoAuxiliar6;
        public string Lstr_CodigoAuxiliar6
        {
            get { return lstr_CodigoAuxiliar6; }
            set { lstr_CodigoAuxiliar6 = value; }
        }
        private int? lint_Secuencia;
        public int? Lint_Secuencia
        {
            get { return lint_Secuencia; }
            set { lint_Secuencia = value; }
        }

        private string lstr_IdModulo;
        public string Lstr_IdModulo
        {
            get { return lstr_IdModulo; }
            set { lstr_IdModulo = value; }
        }

        private string lstr_IdOperacion;
        public string Lstr_IdOperacion
        {
            get { return lstr_IdOperacion; }
            set { lstr_IdOperacion = value; }
        }


        private string lstr_IdClaveContable;
        public string Lstr_IdClaveContable
        {
            get { return lstr_IdClaveContable; }
            set { lstr_IdClaveContable = value; }
        }

        private string lstr_IdCuentaContable;
        public string Lstr_IdCuentaContable
        {
            get { return lstr_IdCuentaContable; }
            set { lstr_IdCuentaContable = value; }
        }

        private string lstr_IdCentroCosto;
        public string Lstr_IdCentroCosto
        {
            get { return lstr_IdCentroCosto; }
            set { lstr_IdCentroCosto = value; }
        }

        private string lstr_IdCentroBeneficio;
        public string Lstr_IdCentroBeneficio
        {
            get { return lstr_IdCentroBeneficio; }
            set { lstr_IdCentroBeneficio = value; }
        }

        private string lstr_IdElementoPEP;
        public string Lstr_IdElementoPEP
        {
            get { return lstr_IdElementoPEP; }
            set { lstr_IdElementoPEP = value; }
        }

        private string lstr_IdPosPre;
        public string Lstr_IdPosPre
        {
            get { return lstr_IdPosPre; }
            set { lstr_IdPosPre = value; }
        }

        private string lstr_IdCentroGestor;
        public string Lstr_IdCentroGestor
        {
            get { return lstr_IdCentroGestor; }
            set { lstr_IdCentroGestor = value; }
        }

        private string lstr_IdPrograma;
        public string Lstr_IdPrograma
        {
            get { return lstr_IdPrograma; }
            set { lstr_IdPrograma = value; }
        }

        private string lstr_IdFondo;
        public string Lstr_IdFondo
        {
            get { return lstr_IdFondo; }
            set { lstr_IdFondo = value; }
        }

        private string lstr_DocPresupuestario;
        public string Lstr_DocPresupuestario
        {
            get { return lstr_DocPresupuestario; }
            set { lstr_DocPresupuestario = value; }
        }

        private string lstr_PosDocPresupuestario;
        public string Lstr_PosDocPresupuestario
        {
            get { return lstr_PosDocPresupuestario; }
            set { lstr_PosDocPresupuestario = value; }
        }
        
        private string lstr_FlujoEfectivo;
        public string Lstr_FlujoEfectivo
        {
            get { return lstr_FlujoEfectivo; }
            set { lstr_FlujoEfectivo = value; }
        }

        private string lstr_NICSP24;
        public string Lstr_NICSP24
        {
            get { return lstr_NICSP24; }
            set { lstr_NICSP24 = value; }
        }


        private string lstr_IdClaveContable2;
        public string Lstr_IdClaveContable2
        {
            get { return lstr_IdClaveContable2; }
            set { lstr_IdClaveContable2 = value; }
        }

        private string lstr_IdCuentaContable2;
        public string Lstr_IdCuentaContable2
        {
            get { return lstr_IdCuentaContable2; }
            set { lstr_IdCuentaContable2 = value; }
        }

        private string lstr_IdCentroCosto2;
        public string Lstr_IdCentroCosto2
        {
            get { return lstr_IdCentroCosto2; }
            set { lstr_IdCentroCosto2 = value; }
        }

        private string lstr_IdCentroBeneficio2;
        public string Lstr_IdCentroBeneficio2
        {
            get { return lstr_IdCentroBeneficio2; }
            set { lstr_IdCentroBeneficio2 = value; }
        }

        private string lstr_IdElementoPEP2;
        public string Lstr_IdElementoPEP2
        {
            get { return lstr_IdElementoPEP2; }
            set { lstr_IdElementoPEP2 = value; }
        }

        private string lstr_IdPosPre2;
        public string Lstr_IdPosPre2
        {
            get { return lstr_IdPosPre2; }
            set { lstr_IdPosPre2 = value; }
        }

        private string lstr_IdCentroGestor2;
        public string Lstr_IdCentroGestor2
        {
            get { return lstr_IdCentroGestor2; }
            set { lstr_IdCentroGestor2 = value; }
        }

        private string lstr_IdPrograma2;
        public string Lstr_IdPrograma2
        {
            get { return lstr_IdPrograma2; }
            set { lstr_IdPrograma2 = value; }
        }

        private string lstr_IdFondo2;
        public string Lstr_IdFondo2
        {
            get { return lstr_IdFondo2; }
            set { lstr_IdFondo2 = value; }
        }

        private string lstr_DocPresupuestario2;
        public string Lstr_DocPresupuestario2
        {
            get { return lstr_DocPresupuestario2; }
            set { lstr_DocPresupuestario2 = value; }
        }

        private string lstr_PosDocPresupuestario2;
        public string Lstr_PosDocPresupuestario2
        {
            get { return lstr_PosDocPresupuestario2; }
            set { lstr_PosDocPresupuestario2 = value; }
        }

        private string lstr_FlujoEfectivo2;
        public string Lstr_FlujoEfectivo2
        {
            get { return lstr_FlujoEfectivo2; }
            set { lstr_FlujoEfectivo2 = value; }
        }

        private string lstr_NICSP242;
        public string Lstr_NICSP242
        {
            get { return lstr_NICSP242; }
            set { lstr_NICSP242 = value; }
        }


        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public clsCrearTipoAsiento(string str_IdModulo, string str_IdOperacion, string str_Codigo, string str_CodigoAuxiliar, string str_CodigoAuxiliar2, string str_CodigoAuxiliar3, string str_CodigoAuxiliar4, string str_IdClaveContable, string str_IdCuentaContable, string str_IdCentroCosto, string str_IdCentroBeneficio, string str_IdElementoPEP, string str_IdPosPre, string str_IdCentroGestor, string str_IdPrograma, string str_IdFondo, string str_DocPresupuestario, string str_PosDocPresupuestario, string str_FlujoEfectivo, string str_NICSP24,
            string str_IdClaveContable2, string str_IdCuentaContable2, string str_IdCentroCosto2, string str_IdCentroBeneficio2, string str_IdElementoPEP2, string str_IdPosPre2, string str_IdCentroGestor2, string str_IdPrograma2, string str_IdFondo2, string str_DocPresupuestario2, string str_PosDocPresupuestario2, string str_FlujoEfectivo2, string str_NICSP242, string str_Estado, string str_UsrCreacion,
            string str_CodigoAuxiliar5 = "", string str_CodigoAuxiliar6 = "", int? int_Secuencia = 1)
        {
            lstr_Codigo = str_Codigo;
            lstr_CodigoAuxiliar = str_CodigoAuxiliar;
            lstr_CodigoAuxiliar2 = str_CodigoAuxiliar2;
            lstr_CodigoAuxiliar3 = str_CodigoAuxiliar3;
            lstr_CodigoAuxiliar4 = str_CodigoAuxiliar4;
            lstr_CodigoAuxiliar5 = str_CodigoAuxiliar5;
            lstr_CodigoAuxiliar6 = str_CodigoAuxiliar6;
            lint_Secuencia = int_Secuencia;
            lstr_IdModulo = str_IdModulo;
            lstr_IdOperacion = str_IdOperacion;
            lstr_IdClaveContable = str_IdClaveContable;
            lstr_IdCuentaContable = str_IdCuentaContable;
            lstr_IdCentroCosto = str_IdCentroCosto;
            lstr_IdCentroBeneficio = str_IdCentroBeneficio;
            lstr_IdElementoPEP = str_IdElementoPEP;
            lstr_IdPosPre = str_IdPosPre;
            lstr_IdCentroGestor = str_IdCentroGestor;
            lstr_IdPrograma = str_IdPrograma;
            lstr_IdFondo = str_IdFondo;
            lstr_DocPresupuestario = str_DocPresupuestario;
            lstr_PosDocPresupuestario = str_PosDocPresupuestario;
            lstr_FlujoEfectivo = str_FlujoEfectivo;
            lstr_NICSP24 = str_NICSP24;
            lstr_IdClaveContable2 = str_IdClaveContable2;
            lstr_IdCuentaContable2 = str_IdCuentaContable2;
            lstr_IdCentroCosto2 = str_IdCentroCosto2;
            lstr_IdCentroBeneficio2 = str_IdCentroBeneficio2;
            lstr_IdElementoPEP2 = str_IdElementoPEP2;
            lstr_IdPosPre2 = str_IdPosPre2;
            lstr_IdCentroGestor2 = str_IdCentroGestor2;
            lstr_IdPrograma2 = str_IdPrograma2;
            lstr_IdFondo2 = str_IdFondo2;
            lstr_DocPresupuestario2 = str_DocPresupuestario2;
            lstr_PosDocPresupuestario2 = str_PosDocPresupuestario2;
            lstr_FlujoEfectivo2 = str_FlujoEfectivo2;
            lstr_NICSP242 = str_NICSP242;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearTipoAsiento.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}