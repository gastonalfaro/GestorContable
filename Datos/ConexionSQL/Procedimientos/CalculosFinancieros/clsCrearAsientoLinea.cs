using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros
{
    public class clsCrearAsientoLinea : clsProcedimientoAlmacenado
    {
        #region Parámetros

         private int lint_Consecutivo 	;	
         private int lint_Secuencia 	;	
         private string lstr_BLDAT 		;
         private string lstr_BLART		;
         private string lstr_BUKRS 		;
         private string lstr_BUDAT 		;
         private string lstr_WAERS 		;
         private decimal ldec_KURSF;
         private string lstr_XBLNR 		;
         private string lstr_XREF1_HD 	;	
         private string lstr_XREF2_HD 	;	
         private string lstr_BUZEI 		;
         private string lstr_BKTXT 		;
         private string lstr_BSCHL 		;
         private string lstr_HKONT 		;
         private string lstr_UMSKZ 		;
         private decimal ldec_WRBTR;
         private decimal ldec_DMBE2;
         private string lstr_MWSKZ 		;
         private string lstr_XMWST 		;
         private string lstr_ZFBDT 		;
         private string lstr_ZUONR 		;
         private string lstr_SGTXT 		;
         private string lstr_HBKID 		;
         private string lstr_ZLSCH 		;
         private string lstr_KOSTL 		;
         private string lstr_PRCTR 		;
         private string lstr_AUFNR 		;
         private string lstr_PROJK 		;
         private string lstr_FIPEX 		;
         private string lstr_FISTL 		;
         private string lstr_MEASURE 	;	
         private string lstr_GEBER 		;
         private string lstr_WERKS 		;
         private string lstr_VALUT 		;
         private string lstr_KBLNR 		;
         private string lstr_KBLPOS 	;	
         private string lstr_RCOMP 		;
         private string lstr_XREF2 		;
         private string lstr_XREF3 		;
         private string lstr_FKBER;
        private string lstr_UsrCreacion;
        private int lint_TmpSecuencia;

        #endregion

        #region Obtención y asignación

        public int Lint_Consecutivo { get { return lint_Consecutivo; } set { lint_Consecutivo = value; } }
        public int Lint_Secuencia { get { return lint_Secuencia; } set { lint_Secuencia = value; } }
        public string Lstr_BLDAT { get { return lstr_BLDAT; } set { lstr_BLDAT = value; } }
        public string Lstr_BLART { get { return lstr_BLART; } set { lstr_BLART = value; } }
        public string Lstr_BUKRS { get { return lstr_BUKRS; } set { lstr_BUKRS = value; } }
        public string Lstr_BUDAT { get { return lstr_BUDAT; } set { lstr_BUDAT = value; } }
        public string Lstr_WAERS { get { return lstr_WAERS; } set { lstr_WAERS = value; } }
        public decimal Ldec_KURSF { get { return ldec_KURSF; } set { ldec_KURSF = value; } }
        public string Lstr_XBLNR { get { return lstr_XBLNR; } set { lstr_XBLNR = value; } }
        public string Lstr_XREF1_HD { get { return lstr_XREF1_HD; } set { lstr_XREF1_HD = value; } }
        public string Lstr_XREF2_HD { get { return lstr_XREF2_HD; } set { lstr_XREF2_HD = value; } }
        public string Lstr_BUZEI { get { return lstr_BUZEI; } set { lstr_BUZEI = value; } }
        public string Lstr_BKTXT { get { return lstr_BKTXT; } set { lstr_BKTXT = value; } }
        public string Lstr_BSCHL { get { return lstr_BSCHL; } set { lstr_BSCHL = value; } }
        public string Lstr_HKONT { get { return lstr_HKONT; } set { lstr_HKONT = value; } }
        public string Lstr_UMSKZ { get { return lstr_UMSKZ; } set { lstr_UMSKZ = value; } }
        public decimal Ldec_WRBTR { get { return ldec_WRBTR; } set { ldec_WRBTR = value; } }
        public decimal Ldec_DMBE2 { get { return ldec_DMBE2; } set { ldec_DMBE2 = value; } }
        public string Lstr_MWSKZ { get { return lstr_MWSKZ; } set { lstr_MWSKZ = value; } }
        public string Lstr_XMWST { get { return lstr_XMWST; } set { lstr_XMWST = value; } }
        public string Lstr_ZFBDT { get { return lstr_ZFBDT; } set { lstr_ZFBDT = value; } }
        public string Lstr_ZUONR { get { return lstr_ZUONR; } set { lstr_ZUONR = value; } }
        public string Lstr_SGTXT { get { return lstr_SGTXT; } set { lstr_SGTXT = value; } }
        public string Lstr_HBKID { get { return lstr_HBKID; } set { lstr_HBKID = value; } }
        public string Lstr_ZLSCH { get { return lstr_ZLSCH; } set { lstr_ZLSCH = value; } }
        public string Lstr_KOSTL { get { return lstr_KOSTL; } set { lstr_KOSTL = value; } }
        public string Lstr_PRCTR { get { return lstr_PRCTR; } set { lstr_PRCTR = value; } }
        public string Lstr_AUFNR { get { return lstr_AUFNR; } set { lstr_AUFNR = value; } }
        public string Lstr_PROJK { get { return lstr_PROJK; } set { lstr_PROJK = value; } }
        public string Lstr_FIPEX { get { return lstr_FIPEX; } set { lstr_FIPEX = value; } }
        public string Lstr_FISTL { get { return lstr_FISTL; } set { lstr_FISTL = value; } }
        public string Lstr_MEASURE { get { return lstr_MEASURE; } set { lstr_MEASURE = value; } }
        public string Lstr_GEBER { get { return lstr_GEBER; } set { lstr_GEBER = value; } }
        public string Lstr_WERKS { get { return lstr_WERKS; } set { lstr_WERKS = value; } }
        public string Lstr_VALUT { get { return lstr_VALUT; } set { lstr_VALUT = value; } }
        public string Lstr_KBLNR { get { return lstr_KBLNR; } set { lstr_KBLNR = value; } }
        public string Lstr_KBLPOS { get { return lstr_KBLPOS; } set { lstr_KBLPOS = value; } }
        public string Lstr_RCOMP { get { return lstr_RCOMP; } set { lstr_RCOMP = value; } }
        public string Lstr_XREF2 { get { return lstr_XREF2; } set { lstr_XREF2 = value; } }
        public string Lstr_XREF3 { get { return lstr_XREF3; } set { lstr_XREF3 = value; } }
        public string Lstr_FKBER { get { return lstr_FKBER; } set { lstr_FKBER = value; } }
        public string Lstr_UsrCreacion { get { return lstr_UsrCreacion; } set { lstr_UsrCreacion = value; } }
        public int Lint_TmpSecuencia { get { return lint_TmpSecuencia; } set { lint_TmpSecuencia = value; } }

        #endregion

        #region Constructor

        public clsCrearAsientoLinea(
              int lint_Consecutivo ,		
              int lint_Secuencia 		,
              string lstr_BLDAT 		,
              string lstr_BLART		,
              string lstr_BUKRS 	,	
              string lstr_BUDAT 	,	
              string lstr_WAERS 	,
              decimal ldec_KURSF,	
              string lstr_XBLNR 	,	
              string lstr_XREF1_HD 	,	
              string lstr_XREF2_HD 	,	
              string lstr_BUZEI 	,	
              string lstr_BKTXT 	,	
              string lstr_BSCHL 	,	
              string lstr_HKONT 	,	
              string lstr_UMSKZ 	,
              decimal ldec_WRBTR,
              decimal ldec_DMBE2,	
              string lstr_MWSKZ 	,	
              string lstr_XMWST 	,	
              string lstr_ZFBDT 	,	
              string lstr_ZUONR 	,	
              string lstr_SGTXT 	,	
              string lstr_HBKID 	,	
              string lstr_ZLSCH 	,	
              string lstr_KOSTL 	,	
              string lstr_PRCTR 	,	
              string lstr_AUFNR 	,	
              string lstr_PROJK 	,	
              string lstr_FIPEX 	,	
              string lstr_FISTL 	,	
              string lstr_MEASURE 	,	
              string lstr_GEBER 	,	
              string lstr_WERKS 	,	
              string lstr_VALUT 	,	
              string lstr_KBLNR 	,	
              string lstr_KBLPOS 	,	
              string lstr_RCOMP 	,	
              string lstr_XREF2 	,	
              string lstr_XREF3 	,	
              string lstr_FKBER 	,
              string lstr_UsrCreacion
            )
        {
            this.lint_Consecutivo = lint_Consecutivo;
            this.lint_Secuencia 		= lint_Secuencia;
            	  this.lstr_BLDAT 		= lstr_BLDAT;
            	  this.lstr_BLART		= lstr_BLART;
            	  this.lstr_BUKRS 		= lstr_BUKRS;
            	  this.lstr_BUDAT 		= lstr_BUDAT;
            	  this.lstr_WAERS 		= lstr_WAERS;
            	  this.ldec_KURSF 		= ldec_KURSF;
            	  this.lstr_XBLNR 		= lstr_XBLNR;
            	  this.lstr_XREF1_HD 		= lstr_XREF1_HD;
            	  this.lstr_XREF2_HD 		= lstr_XREF2_HD;
            	  this.lstr_BUZEI 		= lstr_BUZEI;
            	  this.lstr_BKTXT 		= lstr_BKTXT;
            	  this.lstr_BSCHL 		= lstr_BSCHL;
            	  this.lstr_HKONT 		= lstr_HKONT;
            	  this.lstr_UMSKZ 		= lstr_UMSKZ;
            	  this.ldec_WRBTR 		= ldec_WRBTR;
            	  this.ldec_DMBE2 		= ldec_DMBE2;
            	  this.lstr_MWSKZ 		= lstr_MWSKZ;
            	  this.lstr_XMWST 		= lstr_XMWST;
            	  this.lstr_ZFBDT 		= lstr_ZFBDT;
            	  this.lstr_ZUONR 		= lstr_ZUONR;
            	  this.lstr_SGTXT 		= lstr_SGTXT;
            	  this.lstr_HBKID 		= lstr_HBKID;
            	  this.lstr_ZLSCH 		= lstr_ZLSCH;
            	  this.lstr_KOSTL 		= lstr_KOSTL;
            	  this.lstr_PRCTR 		= lstr_PRCTR;
            	  this.lstr_AUFNR 		= lstr_AUFNR;
            	  this.lstr_PROJK 		= lstr_PROJK;
            	  this.lstr_FIPEX 		= lstr_FIPEX;
            	  this.lstr_FISTL 		= lstr_FISTL;
            	  this.lstr_MEASURE 		= lstr_MEASURE;
            	  this.lstr_GEBER 		= lstr_GEBER;
            	  this.lstr_WERKS 		= lstr_WERKS;
            	  this.lstr_VALUT 		= lstr_VALUT;
            	  this.lstr_KBLNR 		= lstr_KBLNR;
            	  this.lstr_KBLPOS 		= lstr_KBLPOS;
            	  this.lstr_RCOMP 		= lstr_RCOMP;
            	  this.lstr_XREF2 		= lstr_XREF2;
            	  this.lstr_XREF3 		= lstr_XREF3;
                  this.lstr_FKBER = lstr_FKBER;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\CrearAsientoLinea.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}