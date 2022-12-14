using System;
using System.Globalization;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos.Mantenimiento;
using Datos.ConexionSQL.Procedimientos.CalculosFinancieros;
using System.Data;
using log4net;
using log4net.Config;
using LogicaNegocio.CapturaIngresos;
using LogicaNegocio.Contingentes;
using LogicaNegocio.Seguridad;

namespace LogicaNegocio.Mantenimiento
{
    public class clsTiposAsiento
    {
        //private static wrSigafAsientos.Z_FI_CARGA_CONTABLE ServicioAsiento = new wrSigafAsientos.Z_FI_CARGA_CONTABLE(); cucurucho
        private static wrSigafAsientos.Z_FI_CARGA_CONTABLE ServicioAsiento = new wrSigafAsientos.Z_FI_CARGA_CONTABLE();
        //private static wrSigafAsientos.z_fi_carga_contable ServicioAsiento = new wrSigafAsientos.z_fi_carga_contable();
        private static wrSigafAsientos.ZFiCargaContable SigafMetodo = new wrSigafAsientos.ZFiCargaContable();
        private static wrSigafAsientos.ZFiCargaContableResponse SigafResponse = new wrSigafAsientos.ZFiCargaContableResponse();

        private static wrSigafAsientos.ZfiAsiento SigafLinea = new wrSigafAsientos.ZfiAsiento();
        private static wrSigafAsientos.ZfiAsiento[] SigafTablaAsiento;
        private static wrSigafAsientos.ZfiAsientoLog[] SigafAsientoLog = null;//new wrSigafAsientos.ZfiAsientoLog[100];
       
        //private static wrSigafReserva.ZWS_MONTO_RESERVA SigafReserva = new wrSigafReserva.ZWS_MONTO_RESERVA();

        private static string lstr_separador_decimal = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");
        //private wsAsientos.ServicioContable asientos = new wsAsientos.ServicioContable();
        private string resAsientosLog = string.Empty;

        private tBitacora reg_Bitacora = new tBitacora();

        private tParametro lparametro = new tParametro();
        private clsTiposCambio tiposCambio = new clsTiposCambio();
        private clsCobrosPagos cobrospagos = new clsCobrosPagos();
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


        private string lstr_UsrModifica;
        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }

        private DateTime ldt_FchModifica;
        public DateTime Ldt_FchModifica
        {
            get { return ldt_FchModifica; }
            set { ldt_FchModifica = value; }
        }

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public DataSet ConsultarTiposAsiento(string str_Codigo, string str_IdModulo, string str_IdOperacion, string str_IdCuentaContable, string str_IdPosPre, string str_CodigoAuxiliar, string str_CodigoAuxiliar3, string str_CodigoAuxiliar6, string str_CodigoAuxiliar4, string str_CodigoAuxiliar5 = null, string str_CodigoAuxiliar2 = null, int? int_Secuencia = null, string str_OrderBy = null, string str_Exacto = null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarTiposAsiento cr_Procedimiento = new clsConsultarTiposAsiento(str_Codigo, str_IdModulo, str_IdOperacion, str_IdCuentaContable, str_IdPosPre, str_CodigoAuxiliar, str_CodigoAuxiliar2, str_CodigoAuxiliar3, str_CodigoAuxiliar4, str_CodigoAuxiliar5, str_CodigoAuxiliar6, int_Secuencia, str_OrderBy, str_Exacto);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearTipoAsiento(string str_IdModulo, string str_IdOperacion, string str_Codigo, string str_CodigoAuxiliar, string str_CodigoAuxiliar2, string str_CodigoAuxiliar3, string str_CodigoAuxiliar4, string str_IdClaveContable, string str_IdCuentaContable, string str_IdCentroCosto, string str_IdCentroBeneficio, string str_IdElementoPEP, string str_IdPosPre, string str_IdCentroGestor, string str_IdPrograma, string str_IdFondo, string str_DocPresupuestario, string str_PosDocPresupuestario, string str_FlujoEfectivo, string str_NICSP24,
            string str_IdClaveContable2, string str_IdCuentaContable2, string str_IdCentroCosto2, string str_IdCentroBeneficio2, string str_IdElementoPEP2, string str_IdPosPre2, string str_IdCentroGestor2, string str_IdPrograma2, string str_IdFondo2, string str_DocPresupuestario2, string str_PosDocPresupuestario2, string str_FlujoEfectivo2, string str_NICSP242, string str_Estado, string str_UsrCreacion,
            string str_CodigoAuxiliar5, string str_CodigoAuxiliar6, int? int_Secuencia, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearTipoAsiento cls_ProcCrearTipoAsiento = new clsCrearTipoAsiento(str_IdModulo, str_IdOperacion, str_Codigo, str_CodigoAuxiliar, str_CodigoAuxiliar2, str_CodigoAuxiliar3, str_CodigoAuxiliar4, str_IdClaveContable, str_IdCuentaContable, str_IdCentroCosto, str_IdCentroBeneficio, str_IdElementoPEP, str_IdPosPre, str_IdCentroGestor, str_IdPrograma, str_IdFondo, str_DocPresupuestario, str_PosDocPresupuestario, str_FlujoEfectivo, str_NICSP24,
            str_IdClaveContable2, str_IdCuentaContable2, str_IdCentroCosto2, str_IdCentroBeneficio2, str_IdElementoPEP2, str_IdPosPre2, str_IdCentroGestor2, str_IdPrograma2, str_IdFondo2, str_DocPresupuestario2, str_PosDocPresupuestario2, str_FlujoEfectivo2, str_NICSP242, str_Estado, str_UsrCreacion,
            str_CodigoAuxiliar5, str_CodigoAuxiliar6, int_Secuencia);
                str_CodResultado = cls_ProcCrearTipoAsiento.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearTipoAsiento.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResCreacion;
        }

        public bool ModificarTipoAsiento(string str_IdModulo, string str_IdOperacion, string str_Codigo, string str_CodigoAuxiliar, string str_CodigoAuxiliar2, string str_CodigoAuxiliar3, string str_CodigoAuxiliar4, string str_IdClaveContable, string str_IdCuentaContable, string str_IdCentroCosto, string str_IdCentroBeneficio, string str_IdElementoPEP, string str_IdPosPre, string str_IdCentroGestor, string str_IdPrograma, string str_IdFondo, string str_DocPresupuestario, string str_PosDocPresupuestario, string str_FlujoEfectivo, string str_NICSP24,
            string str_IdClaveContable2, string str_IdCuentaContable2, string str_IdCentroCosto2, string str_IdCentroBeneficio2, string str_IdElementoPEP2, string str_IdPosPre2, string str_IdCentroGestor2, string str_IdPrograma2, string str_IdFondo2, string str_DocPresupuestario2, string str_PosDocPresupuestario2, string str_FlujoEfectivo2, string str_NICSP242, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica,
            string str_CodigoAuxiliar5, string str_CodigoAuxiliar6, int? int_Secuencia, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResModifica = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarTipoAsiento cls_ProcModificarTipoAsiento = new clsModificarTipoAsiento(str_IdModulo, str_IdOperacion, str_Codigo, str_CodigoAuxiliar, str_CodigoAuxiliar2, str_CodigoAuxiliar3, str_CodigoAuxiliar4, str_IdClaveContable, str_IdCuentaContable, str_IdCentroCosto, str_IdCentroBeneficio, str_IdElementoPEP, str_IdPosPre, str_IdCentroGestor, str_IdPrograma, str_IdFondo, str_DocPresupuestario, str_PosDocPresupuestario, str_FlujoEfectivo, str_NICSP24,
            str_IdClaveContable2, str_IdCuentaContable2, str_IdCentroCosto2, str_IdCentroBeneficio2, str_IdElementoPEP2, str_IdPosPre2, str_IdCentroGestor2, str_IdPrograma2, str_IdFondo2, str_DocPresupuestario2, str_PosDocPresupuestario2, str_FlujoEfectivo2, str_NICSP242, str_Estado, str_UsrModifica, dt_FchModifica,
            str_CodigoAuxiliar5, str_CodigoAuxiliar6, int_Secuencia);
                str_CodResultado = cls_ProcModificarTipoAsiento.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcModificarTipoAsiento.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResModifica = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResModifica;
        }

        public bool EliminarTipoAsiento(string str_IdModulo, string str_IdOperacion, string str_Codigo, string str_CodigoAuxiliar, string str_CodigoAuxiliar2,
            string str_CodigoAuxiliar3, string str_CodigoAuxiliar4, string str_CodigoAuxiliar5, string str_CodigoAuxiliar6, int? int_Secuencia,
            out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsEliminarTipoAsiento cls_ProcEliminarCrearTipoAsiento = new clsEliminarTipoAsiento(str_IdModulo,
                    str_IdOperacion, str_Codigo, str_CodigoAuxiliar,
                    str_CodigoAuxiliar2, str_CodigoAuxiliar3, str_CodigoAuxiliar4,
                    str_CodigoAuxiliar5, str_CodigoAuxiliar6, int_Secuencia);

                str_CodResultado = cls_ProcEliminarCrearTipoAsiento.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcEliminarCrearTipoAsiento.Lstr_MensajeRespuesta;

                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResCreacion;
        }


        private Object VerificarNull(Object valor)
        {
            if (valor == null)
            {
                valor = "";
            }
            return valor;
        }

        //public wrSigafAsientos.ZfiAsientoLog[] EnviarAsientoSigaf(wrSigafAsientos.ZfiAsiento[] arr_LineasAsiento)
        public string EnviarAsientoSigaf(wrSigafAsientos.ZfiAsiento[] arr_LineasAsiento, string str_Test = "")
        {
            /*
            SigafTablaAsiento = new wrSigafAsientos.ZfiAsiento[2];
            SigafLinea.Blart = "SA";
            SigafLinea.Bukrs = "G206";
            SigafLinea.Bldat = "01.10.2015";
            SigafLinea.Budat = "01.10.2015";
            SigafLinea.Waers = "CRC";
            SigafLinea.Xblnr = "REF";
            SigafLinea.Bktxt = "Texto_Cabecera";
            SigafLinea.Xref1Hd = "REF_1";
            SigafLinea.Xref2Hd = "REF_2";
            SigafLinea.Bschl = "40";
            SigafLinea.Hkont = "5120302000";
            SigafLinea.Wrbtr = 1505;
            SigafLinea.Zuonr = "Asig_1";
            SigafLinea.Sgtxt = "Tex_1";
            SigafLinea.Fipex = "E-10302";
            SigafLinea.Kostl = "20613200";
            SigafLinea.Fistl = "20613200";
            SigafLinea.Geber = "001";
            SigafLinea.Fkber = "";
            SigafLinea.Xref2 = "";
            SigafTablaAsiento[0] = SigafLinea;
            ///cuenta 2
            SigafLinea = new  wrSigafAsientos.ZfiAsiento();
            SigafLinea.Waers = "";
            SigafLinea.Bschl = "50";
            SigafLinea.Hkont = "1114910120";
            SigafLinea.Wrbtr = 1505;
           // SigafLinea.Hkont = "1114910120";
            SigafLinea.Zuonr = "";
            SigafLinea.Sgtxt = "";
            SigafLinea.Kostl = "";
            SigafLinea.Fipex = "P-AGO";
            SigafLinea.Zuonr = "Asig_2";
            SigafLinea.Sgtxt = "Text_2";
            SigafLinea.Fistl = "";
            SigafLinea.Geber = "";
            SigafLinea.Fkber = "";
            SigafLinea.Xref2 = "xref2";
            SigafTablaAsiento[1] = SigafLinea;
            */

            Type elementType = typeof(wrSigafAsientos.ZfiAsiento);
            DataSet ds = new DataSet();
            DataTable t = new DataTable();
            ds.Tables.Add(t);

            //add a column to table for each public property on T
            foreach (var propInfo in elementType.GetProperties())
            {
                t.Columns.Add(propInfo.Name, propInfo.PropertyType);
            }

            //go through each property on T and add each value to the table
            foreach (wrSigafAsientos.ZfiAsiento item in arr_LineasAsiento)
            {
                if (item != null) { 
                DataRow row = t.NewRow();
                foreach (var propInfo in elementType.GetProperties())
                {
                    row[propInfo.Name] = propInfo.GetValue(item, null);
                }
                t.Rows.Add(row);}
            }

            string[] resultMessages = null;
            string result = null;

            decimal ldec_Total40 = 0;
            decimal ldec_Total50 = 0;
            decimal ldec_Diferencia40y50 = 0;
            //wrSigafAsientos.ZfiAsiento[] table_in;

            //wrSigafAsientos.ZfiAsiento line_in = new wsCC.ZfiAsiento();
            try
            {

                //int tamAnno = arr_LineasAsiento.Count();
                //table_in = new wrSigafAsientos.ZfiAsiento[tamAnno];

                for (int i = 0; i < arr_LineasAsiento.Length; i++)
                //int i = 0;
                //foreach (wrSigafAsientos.ZfiAsiento line_in in arr_LineasAsiento)
                {
                    if (arr_LineasAsiento[i] != null)
                    {
                        //seteo de datos de entrada a item de asiento a enviar
                        //line_in = new wsCC.ZfiAsiento();
                        arr_LineasAsiento[i].Mandt = verificarNull(arr_LineasAsiento[i].Mandt).ToString();//Orden
                        if (verificarNull(arr_LineasAsiento[i].Bldat).ToString().Length > 10)
                            arr_LineasAsiento[i].Bldat = verificarNull(arr_LineasAsiento[i].Bldat).ToString().Substring(0, 10);//Texto cabecera
                        if (verificarNull(arr_LineasAsiento[i].Blart).ToString().Length > 2)
                            arr_LineasAsiento[i].Blart = verificarNull(arr_LineasAsiento[i].Blart).ToString().Substring(0, 2);
                        if (verificarNull(arr_LineasAsiento[i].Bukrs).ToString().Length > 4)
                            arr_LineasAsiento[i].Bukrs = verificarNull(arr_LineasAsiento[i].Bukrs).ToString().Substring(0, 4);//"Fecha de documento"
                        if (verificarNull(arr_LineasAsiento[i].Budat).ToString().Length > 10)
                            arr_LineasAsiento[i].Budat = verificarNull(arr_LineasAsiento[i].Budat).ToString().Substring(0, 10);//"Clave de contabilización"
                        if (verificarNull(arr_LineasAsiento[i].Waers).ToString().Length > 5)
                            arr_LineasAsiento[i].Waers = verificarNull(arr_LineasAsiento[i].Waers).ToString().Substring(0, 5);//"Clase de documento"

                        arr_LineasAsiento[i].Kursf = Convert.ToDecimal(verificarNull(arr_LineasAsiento[i].Kursf));//Sociedad
                        if (verificarNull(arr_LineasAsiento[i].Xblnr).ToString().Length > 16)
                            arr_LineasAsiento[i].Xblnr = verificarNull(arr_LineasAsiento[i].Xblnr).ToString().Substring(0, 16);

                        if (verificarNull(arr_LineasAsiento[i].Xref1Hd).ToString().Length > 20)
                            arr_LineasAsiento[i].Xref1Hd = verificarNull(arr_LineasAsiento[i].Xref1Hd).ToString().Substring(0, 17) + "...";
                        else
                            arr_LineasAsiento[i].Xref1Hd = verificarNull(arr_LineasAsiento[i].Xref1Hd).ToString();//Importe en moneda Fuerte

                        if (verificarNull(arr_LineasAsiento[i].Xref2Hd).ToString().Length > 20)
                            arr_LineasAsiento[i].Xref2Hd = verificarNull(arr_LineasAsiento[i].Xref2Hd).ToString().Substring(0, 17) + "...";//"Posición presupuestaria"
                        else
                            arr_LineasAsiento[i].Xref2Hd = verificarNull(arr_LineasAsiento[i].Xref2Hd).ToString();//"Posición presupuestaria"
                        if (verificarNull(arr_LineasAsiento[i].Buzei).ToString().Length > 3)
                            arr_LineasAsiento[i].Buzei = verificarNull(arr_LineasAsiento[i].Buzei).ToString().Substring(0, 3);//Centro Gestor
                        if (verificarNull(arr_LineasAsiento[i].Bktxt).ToString().Length > 25)
                            arr_LineasAsiento[i].Bktxt = verificarNull(arr_LineasAsiento[i].Bktxt).ToString().Substring(0, 25);//Fondo
                        if (verificarNull(arr_LineasAsiento[i].Bschl).ToString().Length > 2)
                            arr_LineasAsiento[i].Bschl = verificarNull(arr_LineasAsiento[i].Bschl).ToString().Substring(0, 2);//Banco Propio
                        if (verificarNull(arr_LineasAsiento[i].Hkont).ToString().Length > 10)
                            arr_LineasAsiento[i].Hkont = verificarNull(arr_LineasAsiento[i].Hkont).ToString().Substring(0, 10);//Cuenta de mayor 
                        if (verificarNull(arr_LineasAsiento[i].Umskz).ToString().Length > 1)
                            arr_LineasAsiento[i].Umskz = verificarNull(arr_LineasAsiento[i].Umskz).ToString().Substring(0, 1);//"Documento presupuestario"
                        arr_LineasAsiento[i].Wrbtr = Convert.ToDecimal(verificarNull(arr_LineasAsiento[i].Wrbtr));//Posición doc pres
                        arr_LineasAsiento[i].Dmbe2 = Convert.ToDecimal(verificarNull(arr_LineasAsiento[i].Dmbe2));//Centro de costo
                        if (verificarNull(arr_LineasAsiento[i].Mwskz).ToString().Length > 2)
                            arr_LineasAsiento[i].Mwskz = verificarNull(arr_LineasAsiento[i].Mwskz).ToString().Substring(0, 2);//Tipo de Cambio
                        if (verificarNull(arr_LineasAsiento[i].Xmwst).ToString().Length > 1)
                            arr_LineasAsiento[i].Xmwst = verificarNull(arr_LineasAsiento[i].Xmwst).ToString().Substring(0, 1);
                        if (verificarNull(arr_LineasAsiento[i].Zfbdt).ToString().Length > 10)
                            arr_LineasAsiento[i].Zfbdt = verificarNull(arr_LineasAsiento[i].Zfbdt).ToString().Substring(0, 10);//"Programa presupuestario"
                        if (verificarNull(arr_LineasAsiento[i].Zuonr).ToString().Length > 18)
                            arr_LineasAsiento[i].Zuonr = verificarNull(arr_LineasAsiento[i].Zuonr).ToString().Substring(0, 18);//"Indicador impuesto"
                        if (verificarNull(arr_LineasAsiento[i].Sgtxt).ToString().Length > 50)
                            arr_LineasAsiento[i].Sgtxt = verificarNull(arr_LineasAsiento[i].Sgtxt).ToString().Substring(0, 50);//"Centro de beneficio"
                        if (verificarNull(arr_LineasAsiento[i].Hbkid).ToString().Length > 5)
                            arr_LineasAsiento[i].Hbkid = verificarNull(arr_LineasAsiento[i].Hbkid).ToString().Substring(0, 5);//Banco Propio
                        if (verificarNull(arr_LineasAsiento[i].Zlsch).ToString().Length > 1)
                            arr_LineasAsiento[i].Zlsch = verificarNull(arr_LineasAsiento[i].Zlsch).ToString().Substring(0, 1);//
                        if (verificarNull(arr_LineasAsiento[i].Kostl).ToString().Length > 10)
                            arr_LineasAsiento[i].Kostl = verificarNull(arr_LineasAsiento[i].Kostl).ToString().Substring(0, 10);//Texto
                        if (verificarNull(arr_LineasAsiento[i].Prctr).ToString().Length > 10)
                            arr_LineasAsiento[i].Prctr = verificarNull(arr_LineasAsiento[i].Prctr).ToString().Substring(0, 10);//Indicador CME
                        if (verificarNull(arr_LineasAsiento[i].Aufnr).ToString().Length > 12)
                            arr_LineasAsiento[i].Aufnr = verificarNull(arr_LineasAsiento[i].Aufnr).ToString().Substring(0, 12);//Fecha Valor
                        if (verificarNull(arr_LineasAsiento[i].Projk).ToString().Length > 24)
                            arr_LineasAsiento[i].Projk = verificarNull(arr_LineasAsiento[i].Projk).ToString().Substring(0, 24);//Elemento Pep
                        if (verificarNull(arr_LineasAsiento[i].Fipex).ToString().Length > 24)
                            arr_LineasAsiento[i].Fipex = verificarNull(arr_LineasAsiento[i].Fipex).ToString().Substring(0, 24);//Pos Pre
                        if (verificarNull(arr_LineasAsiento[i].Fistl).ToString().Length > 16)
                            arr_LineasAsiento[i].Fistl = verificarNull(arr_LineasAsiento[i].Fistl).ToString().Substring(0, 16);//Centro Gestor
                        if (verificarNull(arr_LineasAsiento[i].Measure).ToString().Length > 24)
                            arr_LineasAsiento[i].Measure = verificarNull(arr_LineasAsiento[i].Measure).ToString().Substring(0, 24);//Programa
                        if (verificarNull(arr_LineasAsiento[i].Geber).ToString().Length > 10)
                            arr_LineasAsiento[i].Geber = verificarNull(arr_LineasAsiento[i].Geber).ToString().Substring(0, 10);//Fondo
                        if (verificarNull(arr_LineasAsiento[i].Werks).ToString().Length > 4)
                            arr_LineasAsiento[i].Werks = verificarNull(arr_LineasAsiento[i].Werks).ToString().Substring(0, 4);//Centro
                        if (verificarNull(arr_LineasAsiento[i].Valut).ToString().Length > 10)
                            arr_LineasAsiento[i].Valut = verificarNull(arr_LineasAsiento[i].Valut).ToString().Substring(0, 10);//Fecha de Valor
                        if (verificarNull(arr_LineasAsiento[i].Kblnr).ToString().Length > 10)
                            arr_LineasAsiento[i].Kblnr = verificarNull(arr_LineasAsiento[i].Kblnr).ToString().Substring(0, 10);//Documento Presupuesto
                        if (verificarNull(arr_LineasAsiento[i].Kblpos).ToString().Length > 3)
                            arr_LineasAsiento[i].Kblpos = verificarNull(arr_LineasAsiento[i].Kblpos).ToString().Substring(0, 3);//Posicion Documento Presupuesto
                        if (verificarNull(arr_LineasAsiento[i].Rcomp).ToString().Length > 6)
                            arr_LineasAsiento[i].Rcomp = verificarNull(arr_LineasAsiento[i].Rcomp).ToString().Substring(0, 6);//Asignación


                        if (verificarNull(arr_LineasAsiento[i].Xref2).ToString().Length > 12)
                            arr_LineasAsiento[i].Xref2 = verificarNull(arr_LineasAsiento[i].Xref2).ToString().Substring(0, 12);//Asignación

                        if (verificarNull(arr_LineasAsiento[i].Xref3).ToString().Length > 20)
                            arr_LineasAsiento[i].Xref3 = verificarNull(arr_LineasAsiento[i].Xref3).ToString().Substring(0, 20);//Asignación
                        if (verificarNull(arr_LineasAsiento[i].Fkber).ToString().Length > 16)
                            arr_LineasAsiento[i].Fkber = verificarNull(arr_LineasAsiento[i].Fkber).ToString().Substring(0, 16);//Area funcional
                        //asientos coleccion
                        //table_in[i] = line_in;

                        if (arr_LineasAsiento[i].Bschl == "40")
                            ldec_Total40 += arr_LineasAsiento[i].Wrbtr;
                        else
                            ldec_Total50 += arr_LineasAsiento[i].Wrbtr;
                    }
                    //i++;
                }


                ldec_Diferencia40y50 = ldec_Total40 - ldec_Total50;

                //si el descuadre es pequeño se intenta cuadrar
                if ((Math.Abs(ldec_Diferencia40y50) > 0 && Math.Abs(ldec_Diferencia40y50) < 1)||  
                (Math.Abs(ldec_Diferencia40y50) > -1 && Math.Abs(ldec_Diferencia40y50) < 0) )
                {

                    Boolean lbl_cuadrado = false;
                    for (int i = 0; i < arr_LineasAsiento.Length; i++)
                    //int i = 0;
                    //foreach (wrSigafAsientos.ZfiAsiento line_in in arr_LineasAsiento)
                    {
                       
                            if (!lbl_cuadrado && arr_LineasAsiento[i] != null)
                            {
                                if (ldec_Diferencia40y50 > 0 && ldec_Diferencia40y50 < 1 && arr_LineasAsiento[i].Bschl == "50")
                                {//es mayor el 40 a los 50, subirle la diferencia al 50
                                    arr_LineasAsiento[i].Wrbtr += ldec_Diferencia40y50;
                                    lbl_cuadrado = true;
                                }
                                else
                                {//es mayor el 40 a los 50, subirle la diferencia al 50
                                    if (ldec_Diferencia40y50 < 0 && ldec_Diferencia40y50 > -1 && arr_LineasAsiento[i].Bschl == "40")
                                    {//es mayor el 50 a los 40, subirle la diferencia al 40

                                        arr_LineasAsiento[i].Wrbtr += Math.Abs(ldec_Diferencia40y50);
                                        lbl_cuadrado = true;
                                    }
                                }
                            }
         
                    }//for int i
                }
                //SigafMetodo.GtAsientos = SigafTablaAsiento;
                SigafMetodo.GtAsientos = arr_LineasAsiento;
                SigafMetodo.ITest = str_Test;

                //string lstr_user = "rodriguezl";
                string lstr_user = System.Configuration.ConfigurationManager.AppSettings["USER_SAP"];//usuario
                //string lstr_pass = "Luirodzu2";
                string lstr_pass = System.Configuration.ConfigurationManager.AppSettings["PASS_SAP"];//contrasena

                ServicioAsiento.Credentials = new System.Net.NetworkCredential(lstr_user, lstr_pass);
                //ServicioAsiento.Credentials = System.Net.CredentialCache.DefaultCredentials; ;
          
                Boolean lbln_enviado = false;
                int lint_intentos = 0;
                while (!lbln_enviado && lint_intentos < 5)
                {
                    try
                    {
                        lint_intentos++;
                        SigafResponse = ServicioAsiento.ZFiCargaContable(SigafMetodo);
                        lbln_enviado = true;
                        SigafAsientoLog = SigafResponse.GtLog;

                        resultMessages = new string[SigafResponse.GtLog.Count() + 1];
                        for (int y = 0; y < SigafResponse.GtLog.Count(); y++)
                        {
                            resultMessages[y] = "[" + SigafResponse.GtLog[y].Type + "] " + SigafResponse.GtLog[y].Message;
                            result += "\n" + y + " [" + SigafResponse.GtLog[y].Type + "] " + SigafResponse.GtLog[y].Message;
                        }
                        lint_intentos = 5;
                    }
                    catch (Exception e)
                    {
                        if (lint_intentos == 5)
                        {

                            resultMessages = new string[1];
                            resultMessages[0] = "[E] " + e.Message;
                            result = "[E] " + e.Message;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resultMessages = new string[1];
                resultMessages[0] = "[E] " + ex.Message;
                result = "[E] " + ex.Message;
            }
            return result;

            //return SigafAsientoLog;

        }

        private Object verificarNull(Object valor)
        {
            if (valor == null)
            {
                valor = "";
            }
            return valor;
        }


        //public wrSigafAsientos.ZfiAsientoLog[] EnviarAsientoSigaf(wrSigafAsientos.ZfiAsiento[] arr_LineasAsiento)
        public string[] EnviarAsientos(wrSigafAsientos.ZfiAsiento[] arr_LineasAsiento, string str_Test = "")
        {
            /*
            SigafTablaAsiento = new wrSigafAsientos.ZfiAsiento[2];
            SigafLinea.Blart = "SA";
            SigafLinea.Bukrs = "G206";
            SigafLinea.Bldat = "01.10.2015";
            SigafLinea.Budat = "01.10.2015";
            SigafLinea.Waers = "CRC";
            SigafLinea.Xblnr = "REF";
            SigafLinea.Bktxt = "Texto_Cabecera";
            SigafLinea.Xref1Hd = "REF_1";
            SigafLinea.Xref2Hd = "REF_2";
            SigafLinea.Bschl = "40";
            SigafLinea.Hkont = "5120302000";
            SigafLinea.Wrbtr = 1505;
            SigafLinea.Zuonr = "Asig_1";
            SigafLinea.Sgtxt = "Tex_1";
            SigafLinea.Fipex = "E-10302";
            SigafLinea.Kostl = "20613200";
            SigafLinea.Fistl = "20613200";
            SigafLinea.Geber = "001";
            SigafLinea.Fkber = "";
            SigafLinea.Xref2 = "";
            SigafTablaAsiento[0] = SigafLinea;
            ///cuenta 2
            SigafLinea = new  wrSigafAsientos.ZfiAsiento();
            SigafLinea.Waers = "";
            SigafLinea.Bschl = "50";
            SigafLinea.Hkont = "1114910120";
            SigafLinea.Wrbtr = 1505;
           // SigafLinea.Hkont = "1114910120";
            SigafLinea.Zuonr = "";
            SigafLinea.Sgtxt = "";
            SigafLinea.Kostl = "";
            SigafLinea.Fipex = "P-AGO";
            SigafLinea.Zuonr = "Asig_2";
            SigafLinea.Sgtxt = "Text_2";
            SigafLinea.Fistl = "";
            SigafLinea.Geber = "";
            SigafLinea.Fkber = "";
            SigafLinea.Xref2 = "xref2";
            SigafTablaAsiento[1] = SigafLinea;
            */
            string[] resultMessages = null;
            string result = null;

            decimal ldec_Total40 = 0;
            decimal ldec_Total50 = 0;
            decimal ldec_Diferencia40y50 = 0;

            try
            {
                //int tamAnno = arr_LineasAsiento.Count();
                //table_in = new wrSigafAsientos.ZfiAsiento[tamAnno];

                for (int i = 0; i < arr_LineasAsiento.Length; i++)
                //int i = 0;
                //foreach (wrSigafAsientos.ZfiAsiento line_in in arr_LineasAsiento)
                {
                    if (arr_LineasAsiento[i] != null)
                    {
                        //seteo de datos de entrada a item de asiento a enviar
                        //line_in = new wsCC.ZfiAsiento();
                        arr_LineasAsiento[i].Mandt = verificarNull(arr_LineasAsiento[i].Mandt).ToString();//Orden
                        if (verificarNull(arr_LineasAsiento[i].Bldat).ToString().Length > 10)
                            arr_LineasAsiento[i].Bldat = verificarNull(arr_LineasAsiento[i].Bldat).ToString().Substring(0, 10);//Texto cabecera
                        if (verificarNull(arr_LineasAsiento[i].Blart).ToString().Length > 2)
                            arr_LineasAsiento[i].Blart = verificarNull(arr_LineasAsiento[i].Blart).ToString().Substring(0, 2);
                        if (verificarNull(arr_LineasAsiento[i].Bukrs).ToString().Length > 4)
                            arr_LineasAsiento[i].Bukrs = verificarNull(arr_LineasAsiento[i].Bukrs).ToString().Substring(0, 4);//"Fecha de documento"
                        if (verificarNull(arr_LineasAsiento[i].Budat).ToString().Length > 10)
                            arr_LineasAsiento[i].Budat = verificarNull(arr_LineasAsiento[i].Budat).ToString().Substring(0, 10);//"Clave de contabilización"
                        if (verificarNull(arr_LineasAsiento[i].Waers).ToString().Length > 5)
                        arr_LineasAsiento[i].Waers = verificarNull(arr_LineasAsiento[i].Waers).ToString().Substring(0, 5);//"Clase de documento"
                        
                        arr_LineasAsiento[i].Kursf = Convert.ToDecimal(verificarNull(arr_LineasAsiento[i].Kursf));//Sociedad
                        if (verificarNull(arr_LineasAsiento[i].Xblnr).ToString().Length > 16)
                        arr_LineasAsiento[i].Xblnr = verificarNull(arr_LineasAsiento[i].Xblnr).ToString().Substring(0, 16);

                        if (verificarNull(arr_LineasAsiento[i].Xref1Hd).ToString().Length > 20)
                            arr_LineasAsiento[i].Xref1Hd = verificarNull(arr_LineasAsiento[i].Xref1Hd).ToString().Substring(0, 17) + "...";
                        else
                            arr_LineasAsiento[i].Xref1Hd = verificarNull(arr_LineasAsiento[i].Xref1Hd).ToString();//Importe en moneda Fuerte

                        if (verificarNull(arr_LineasAsiento[i].Xref2Hd).ToString().Length > 20)
                            arr_LineasAsiento[i].Xref2Hd = verificarNull(arr_LineasAsiento[i].Xref2Hd).ToString().Substring(0, 17) + "...";//"Posición presupuestaria"
                        else
                            arr_LineasAsiento[i].Xref2Hd = verificarNull(arr_LineasAsiento[i].Xref2Hd).ToString();//"Posición presupuestaria"
                        if (verificarNull(arr_LineasAsiento[i].Buzei).ToString().Length > 3)
                        arr_LineasAsiento[i].Buzei = verificarNull(arr_LineasAsiento[i].Buzei).ToString().Substring(0, 3);//Centro Gestor
                        if (verificarNull(arr_LineasAsiento[i].Bktxt).ToString().Length > 25)
                        arr_LineasAsiento[i].Bktxt = verificarNull(arr_LineasAsiento[i].Bktxt).ToString().Substring(0, 25);//Fondo
                        if (verificarNull(arr_LineasAsiento[i].Bschl).ToString().Length > 2)
                        arr_LineasAsiento[i].Bschl = verificarNull(arr_LineasAsiento[i].Bschl).ToString().Substring(0, 2);//Banco Propio
                        if (verificarNull(arr_LineasAsiento[i].Hkont).ToString().Length > 10)
                        arr_LineasAsiento[i].Hkont = verificarNull(arr_LineasAsiento[i].Hkont).ToString().Substring(0, 10);//Cuenta de mayor 
                        if (verificarNull(arr_LineasAsiento[i].Umskz).ToString().Length > 1)
                        arr_LineasAsiento[i].Umskz = verificarNull(arr_LineasAsiento[i].Umskz).ToString().Substring(0, 1);//"Documento presupuestario"
                        arr_LineasAsiento[i].Wrbtr = Convert.ToDecimal(verificarNull(arr_LineasAsiento[i].Wrbtr));//Posición doc pres
                        arr_LineasAsiento[i].Dmbe2 = Convert.ToDecimal(verificarNull(arr_LineasAsiento[i].Dmbe2));//Centro de costo
                        if (verificarNull(arr_LineasAsiento[i].Mwskz).ToString().Length > 2)
                        arr_LineasAsiento[i].Mwskz = verificarNull(arr_LineasAsiento[i].Mwskz).ToString().Substring(0, 2);//Tipo de Cambio
                        if (verificarNull(arr_LineasAsiento[i].Xmwst).ToString().Length > 1)
                        arr_LineasAsiento[i].Xmwst = verificarNull(arr_LineasAsiento[i].Xmwst).ToString().Substring(0, 1);
                        if (verificarNull(arr_LineasAsiento[i].Zfbdt).ToString().Length > 10)
                        arr_LineasAsiento[i].Zfbdt = verificarNull(arr_LineasAsiento[i].Zfbdt).ToString().Substring(0, 10);//"Programa presupuestario"
                        if (verificarNull(arr_LineasAsiento[i].Zuonr).ToString().Length > 18)
                        arr_LineasAsiento[i].Zuonr = verificarNull(arr_LineasAsiento[i].Zuonr).ToString().Substring(0, 18);//"Indicador impuesto"
                        if (verificarNull(arr_LineasAsiento[i].Sgtxt).ToString().Length > 50)
                            arr_LineasAsiento[i].Sgtxt = verificarNull(arr_LineasAsiento[i].Sgtxt).ToString().Substring(0, 50);//"Centro de beneficio"
                        if (verificarNull(arr_LineasAsiento[i].Hbkid).ToString().Length > 5)
                            arr_LineasAsiento[i].Hbkid = verificarNull(arr_LineasAsiento[i].Hbkid).ToString().Substring(0, 5);//Banco Propio
                        if (verificarNull(arr_LineasAsiento[i].Zlsch).ToString().Length > 1)
                            arr_LineasAsiento[i].Zlsch = verificarNull(arr_LineasAsiento[i].Zlsch).ToString().Substring(0, 1);//
                        if (verificarNull(arr_LineasAsiento[i].Kostl).ToString().Length > 10)
                            arr_LineasAsiento[i].Kostl = verificarNull(arr_LineasAsiento[i].Kostl).ToString().Substring(0, 10);//Texto
                        if (verificarNull(arr_LineasAsiento[i].Prctr).ToString().Length > 10)
                            arr_LineasAsiento[i].Prctr = verificarNull(arr_LineasAsiento[i].Prctr).ToString().Substring(0, 10);//Indicador CME
                        if (verificarNull(arr_LineasAsiento[i].Aufnr).ToString().Length > 12)
                            arr_LineasAsiento[i].Aufnr = verificarNull(arr_LineasAsiento[i].Aufnr).ToString().Substring(0, 12);//Fecha Valor
                        if (verificarNull(arr_LineasAsiento[i].Projk).ToString().Length > 24)
                            arr_LineasAsiento[i].Projk = verificarNull(arr_LineasAsiento[i].Projk).ToString().Substring(0, 24);//Elemento Pep
                        if (verificarNull(arr_LineasAsiento[i].Fipex).ToString().Length > 24)
                            arr_LineasAsiento[i].Fipex = verificarNull(arr_LineasAsiento[i].Fipex).ToString().Substring(0, 24);//Pos Pre
                        if (verificarNull(arr_LineasAsiento[i].Fistl).ToString().Length > 16)
                            arr_LineasAsiento[i].Fistl = verificarNull(arr_LineasAsiento[i].Fistl).ToString().Substring(0, 16);//Centro Gestor
                        if (verificarNull(arr_LineasAsiento[i].Measure).ToString().Length > 24)
                            arr_LineasAsiento[i].Measure = verificarNull(arr_LineasAsiento[i].Measure).ToString().Substring(0, 24);//Programa
                        if (verificarNull(arr_LineasAsiento[i].Geber).ToString().Length > 10)
                            arr_LineasAsiento[i].Geber = verificarNull(arr_LineasAsiento[i].Geber).ToString().Substring(0, 10);//Fondo
                        if (verificarNull(arr_LineasAsiento[i].Werks).ToString().Length > 4)
                            arr_LineasAsiento[i].Werks = verificarNull(arr_LineasAsiento[i].Werks).ToString().Substring(0, 4);//Centro
                        if (verificarNull(arr_LineasAsiento[i].Valut).ToString().Length > 10)
                            arr_LineasAsiento[i].Valut = verificarNull(arr_LineasAsiento[i].Valut).ToString().Substring(0, 10);//Fecha de Valor
                        if (verificarNull(arr_LineasAsiento[i].Kblnr).ToString().Length > 10)
                            arr_LineasAsiento[i].Kblnr = verificarNull(arr_LineasAsiento[i].Kblnr).ToString().Substring(0, 10);//Documento Presupuesto
                        if (verificarNull(arr_LineasAsiento[i].Kblpos).ToString().Length > 3)
                            arr_LineasAsiento[i].Kblpos = verificarNull(arr_LineasAsiento[i].Kblpos).ToString().Substring(0, 3);//Posicion Documento Presupuesto
                        if (verificarNull(arr_LineasAsiento[i].Rcomp).ToString().Length > 6)
                            arr_LineasAsiento[i].Rcomp = verificarNull(arr_LineasAsiento[i].Rcomp).ToString().Substring(0, 6);//Asignación


                        if (verificarNull(arr_LineasAsiento[i].Xref2).ToString().Length > 12)
                            arr_LineasAsiento[i].Xref2 = verificarNull(arr_LineasAsiento[i].Xref2).ToString().Substring(0, 12);//Asignación

                        if (verificarNull(arr_LineasAsiento[i].Xref3).ToString().Length > 20)
                            arr_LineasAsiento[i].Xref3 = verificarNull(arr_LineasAsiento[i].Xref3).ToString().Substring(0, 20);//Asignación
                        if (verificarNull(arr_LineasAsiento[i].Fkber).ToString().Length > 16)
                            arr_LineasAsiento[i].Fkber = verificarNull(arr_LineasAsiento[i].Fkber).ToString().Substring(0, 16);//Area funcional
                        //asientos coleccion
                        //table_in[i] = line_in;
                        
                        if (arr_LineasAsiento[i].Bschl == "40")
                            ldec_Total40 += arr_LineasAsiento[i].Wrbtr;
                        else
                            ldec_Total50 += arr_LineasAsiento[i].Wrbtr;
                    }
                    //i++;
                }


                ldec_Diferencia40y50 = ldec_Total40 - ldec_Total50;

                //si el descuadre es pequeño se intenta cuadrar
                if ((Math.Abs(ldec_Diferencia40y50) > 0 && Math.Abs(ldec_Diferencia40y50) < 1) ||
                (Math.Abs(ldec_Diferencia40y50) > -1 && Math.Abs(ldec_Diferencia40y50) < 0))
                {

                    Boolean lbl_cuadrado = false;
                    for (int i = 0; i < arr_LineasAsiento.Length; i++)
                    //int i = 0;
                    //foreach (wrSigafAsientos.ZfiAsiento line_in in arr_LineasAsiento)
                    {

                        if (!lbl_cuadrado && arr_LineasAsiento[i] != null)
                        {
                            if (ldec_Diferencia40y50 > 0 && ldec_Diferencia40y50 < 1 && arr_LineasAsiento[i].Bschl == "50")
                            {//es mayor el 40 a los 50, subirle la diferencia al 50
                                arr_LineasAsiento[i].Wrbtr += ldec_Diferencia40y50;
                                lbl_cuadrado = true;
                            }
                            else
                            {//es mayor el 40 a los 50, subirle la diferencia al 50
                                if (ldec_Diferencia40y50 < 0 && ldec_Diferencia40y50 > -1 && arr_LineasAsiento[i].Bschl == "40")
                                {//es mayor el 50 a los 40, subirle la diferencia al 40

                                    arr_LineasAsiento[i].Wrbtr += Math.Abs(ldec_Diferencia40y50);
                                    lbl_cuadrado = true;
                                }
                            }
                        }

                    }//for int i
                }
                //SigafMetodo.GtAsientos = SigafTablaAsiento;
                SigafMetodo.GtAsientos = arr_LineasAsiento;
                SigafMetodo.ITest = str_Test;

                //string lstr_user = "rodriguezl";
                string lstr_user = System.Configuration.ConfigurationManager.AppSettings["USER_SAP"];//usuario
                //string lstr_pass = "Luirodzu2";
                string lstr_pass = System.Configuration.ConfigurationManager.AppSettings["PASS_SAP"];//contrasena

                ServicioAsiento.Credentials = new System.Net.NetworkCredential(lstr_user, lstr_pass);
                Boolean lbln_enviado = false;
                int lint_intentos = 0;
                while (!lbln_enviado && lint_intentos < 5)
                {
                    try
                    {
                        lint_intentos++;
                        SigafResponse = ServicioAsiento.ZFiCargaContable(SigafMetodo);
                        lbln_enviado = true;

                        SigafAsientoLog = SigafResponse.GtLog;
                        /*
                        resultMessages = new string[SigafResponse.GtLog.Count() + 1];
                        for (int y = 0; y < SigafResponse.GtLog.Count(); y++)
                        {
                            resultMessages[y] = "[" + SigafResponse.GtLog[y].Type + "] " + SigafResponse.GtLog[y].Message;
                            result += "\n" + y + " [" + SigafResponse.GtLog[y].Type + "] " + SigafResponse.GtLog[y].Message;
                        }
                        */
                        #region info de contabilizacion

                        //EDITADO PARA RETORNAR NUMERO DE ASIENTO, CUIDADO!
                        if (SigafResponse.GtLog[0].Type.Equals("S"))
                            resultMessages = new string[SigafResponse.GtLog.Count() + 1];
                        else
                            resultMessages = new string[SigafResponse.GtLog.Count()];

                        for (int y = 0; y < SigafResponse.GtLog.Count(); y++)
                        {
                            if (SigafResponse.GtLog[y].Type.Equals("S"))
                            {
                                resultMessages[y] = SigafResponse.GtLog[y].Belnr;
                                resultMessages[y + 1] = "[" + SigafResponse.GtLog[y].Type + "] " + SigafResponse.GtLog[y].Message;

                                result += "\n" + y + " [" + SigafResponse.GtLog[y].Type + "] " + SigafResponse.GtLog[y].Message;
                            }
                            else
                            {
                                resultMessages[y] = "[" + SigafResponse.GtLog[y].Type + "] " + SigafResponse.GtLog[y].Message;

                                result += "\n" + y + " [" + SigafResponse.GtLog[y].Type + "] " + SigafResponse.GtLog[y].Message;
                            }
                        }

                        #endregion

                    }
                    catch (Exception e)
                    {
                        if (lint_intentos == 5)
                        {

                            resultMessages = new string[1];
                            resultMessages[0] = "[E] " + e.Message;
                            result = "[E] " + e.Message;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resultMessages = new string[1];
                resultMessages[0] = "[E] " + ex.Message;
                result = "[E] " + ex.Message;
            }
            return resultMessages;

            //return SigafAsientoLog;

        }

        /// <summary>
        /// Se encarga de contabilizar los formularios de captura de ingreso, excluyendo los que indican numero de expediente de contingentes.
        /// </summary>
        /// <param name="lint_AnnoActual">Anno de Registro de los formularios que se van a contabilizar</param>
        /// <param name="lint_Idformulario"> Identificador de formulario que se desea contabilizar</param>
        public int EnviarAsientosCI(Int16? lint_AnnoActual = null, int lint_Idformulario = -1)
        {
            ILog Log = LogManager.GetLogger("SGCierreContable");
            LogicaNegocio.wrSigafAsientos.ZfiAsiento SigafLinea = new LogicaNegocio.wrSigafAsientos.ZfiAsiento(); //wrSigafAsientos.ZfiAsiento();
            LogicaNegocio.wrSigafAsientos.ZfiAsiento[] SigafTablaAsientoPago;
            LogicaNegocio.wrSigafAsientos.ZfiAsiento[] SigafTablaAsientoDev1;
            LogicaNegocio.wrSigafAsientos.ZfiAsiento[] SigafTablaAsientoDev2;
            LogicaNegocio.wrSigafAsientos.ZfiAsientoLog[] SigafAsientoLogPago = new LogicaNegocio.wrSigafAsientos.ZfiAsientoLog[100];
            LogicaNegocio.wrSigafAsientos.ZfiAsientoLog[] SigafAsientoLogDev1 = new LogicaNegocio.wrSigafAsientos.ZfiAsientoLog[100];
            LogicaNegocio.wrSigafAsientos.ZfiAsientoLog[] SigafAsientoLogDev2 = new LogicaNegocio.wrSigafAsientos.ZfiAsientoLog[100];
            DateTime ldt_FechaActual = DateTime.Today;//new DateTime();
            //-1 para que tome todos los formularios
            int lint_IdPago = -1;
            Int16 gint_AnnoActual;

            gint_AnnoActual = (lint_AnnoActual == null) ? Convert.ToInt16(ldt_FechaActual.Year) : Convert.ToInt16(lint_AnnoActual);
            DateTime ldt_FchPagoDesde = Convert.ToDateTime("1900-01-01");
            DateTime ldt_FchPagoHasta = Convert.ToDateTime("2099-12-31");
            DataSet lds_Formularios = new DataSet();
            DataSet lds_Pagos = new DataSet();
            DataSet lds_Servicios = new DataSet();
            DataSet lds_TiposAsiento = new DataSet();
            DataSet lds_Operaciones = new DataSet();
            DataSet lds_Oficinas = new DataSet();
            DataSet lds_SociedadesGLSociedadesFi = new DataSet();
            DataSet lds_Expediente = new DataSet();
            DataSet lds_ReservasDetallado = new DataSet();

            clsOpcionesCatalogo oc = new clsOpcionesCatalogo();
            DataSet ds_opciones = new DataSet();

            clsFormulariosCapturaIngresos lformulario = new clsFormulariosCapturaIngresos();
            clsPagosPorFormulario lpago = new clsPagosPorFormulario();
            //clsTiposAsiento lasiento = new clsTiposAsiento();
            clsServicios lservicio = new clsServicios();
            clsOperaciones loperacion = new clsOperaciones();
            clsOficinas loficina = new clsOficinas();
            tSociedadGL lsociedadGL = new tSociedadGL();
            clsExpedientes lexpediente = new clsExpedientes();
            clsResoluciones lresolucion = new clsResoluciones();
            clsReservasDetalle lreservasDet = new clsReservasDetalle();
            //clsBitacoraDeMovimientosCuentasExpedientes reg_Bitacora = new clsBitacoraDeMovimientosCuentasExpedientes();
            tBitacora reg_Bitacora = new tBitacora();
            clsFormulariosCapturaIngresos lcls_Formulario = new clsFormulariosCapturaIngresos();
            string lstr_IdModulo = "IdModulo IN ('CI')";
            string lstr_Mensaje = "";
            string lstr_IdSociedadFI = "";
            string lstr_IdCeBe = "";
            string logAsientoPago = string.Empty;
            string logAsientoDev1 = string.Empty;
            string logAsientoDev2 = string.Empty;
            string[] item_resAsientosLogDev1 = null;// new string[100];
            string[] item_resAsientosLogDev2 = null;//new string[100];
            string[] item_resAsientosLogPago = null;//new string[100];
            string resAsientosLogDev1 = string.Empty;
            string resAsientosLogDev2 = string.Empty;
            string resAsientosLogPago = string.Empty;
            string str_IdTipoProcesoExp = string.Empty;
            string str_MonedaExp = string.Empty;
            decimal dec_TipoCambioOrig = 0;
            decimal dec_TipoCambioCierreExp = 0;
            decimal dec_MontoPrincipalExp = 0;
            decimal dec_MontoInteresesExp = 0;
            decimal dec_MontoInteresesMoraExp = 0;
            decimal dec_MontoCostasExp = 0;
            decimal dec_MontoDanosExp = 0;

            string str_CodError = string.Empty;
            string str_Mensaje = string.Empty;
            Boolean lbln_ErrorAsientoLinea = false;
            Boolean lbln_ErrorInterfaz = false;
            Boolean lbln_ErrorBitacora = false;
            Boolean lbln_ErrorCambioEstado = false;
            Boolean lbln_ExisteExpediente;
            Boolean lbln_TieneExpediente;
            Boolean lbln_PeriodoActual;
            Boolean lbln_AsientoEncontrado;
            try
            {

                //saco todos los formularios en estado Pagado para ser contabilizados.
                lds_Formularios = lformulario.ConsultarFormulariosCapturaIngresos(lint_Idformulario, gint_AnnoActual, string.Empty, string.Empty,
                                                                                string.Empty, string.Empty, string.Empty, string.Empty,
                                                                                "-", string.Empty, string.Empty, "PAG");

                #region Formularios
                //solo itero si el dataset tiene registros
                if (lds_Formularios.Tables.Count > 0 && lds_Formularios.Tables["Table"].Rows.Count > 0)
                {

                    //itero en todos los formularios pagados para sacar los servicios pagados e irlos contabilizando
                    for (int i = 0; lds_Formularios.Tables["Table"].Rows.Count > i; i++)
                    {
                        int lint_lineaVerifica = 0;
                        //obtengo los pagos activos del formulario!
                        //lds_Pagos = lpago.ConsultarPagosPorFormulario(Convert.ToInt32(lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"]),
                        //                                              Convert.ToInt32(lds_Formularios.Tables["Table"].Rows[i]["Anno"]),
                        //                                              lint_IdPago,
                        //                                              ldt_FchPagoDesde,
                        //                                              ldt_FchPagoHasta,
                        //                                              string.Empty,
                        //                                              string.Empty,
                        //                                              string.Empty,
                        //                                              string.Empty,
                        //                                              string.Empty,
                        //                                              "A"
                        //                                              );
                        lstr_IdCeBe = "";
                        if (!string.IsNullOrEmpty(lds_Formularios.Tables["Table"].Rows[i]["IdOficina"].ToString().Trim()))
                        {
                            lds_Oficinas = loficina.ConsultarOficinasCeBe(lds_Formularios.Tables["Table"].Rows[i]["IdOficina"].ToString().Trim(), lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim(), "CI", string.Empty);
                            //solo itero si el dataset tiene registros
                            if (lds_Oficinas.Tables.Count > 0 && lds_Oficinas.Tables["Table"].Rows.Count > 0)
                            {

                                lstr_IdCeBe = lds_Oficinas.Tables["Table"].Rows[0]["IdCentroBeneficio"].ToString().Trim();
                            }

                        }


                        #region Expediente
                        lbln_TieneExpediente = false;

                        lbln_ExisteExpediente = false;
                        if (!string.IsNullOrEmpty(lds_Formularios.Tables["Table"].Rows[i]["NroExpediente"].ToString().Trim()))
                        {
                            lbln_TieneExpediente = true;
                            //lds_Expediente = lexpediente.con (lds_Formularios.Tables["Table"].Rows[i]["NroExpediente"].ToString().Trim(), lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim());
                            lds_Expediente = lresolucion.ConsultarResolucion(null, lds_Formularios.Tables["Table"].Rows[i]["NroExpediente"].ToString().Trim(), lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim(), out str_CodError, out str_Mensaje);
                            if (lds_Expediente.Tables.Count > 0 && lds_Expediente.Tables["Table"].Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(lds_Expediente.Tables["Table"].Rows[0]["IdExpediente"].ToString().Trim()))
                                {
                                    lbln_ExisteExpediente = true;

                                    str_MonedaExp = lds_Expediente.Tables["Table"].Rows[0]["Moneda"].ToString().Trim();
                                    dec_TipoCambioOrig = Convert.ToDecimal(lds_Expediente.Tables["Table"].Rows[0]["TipoCambio1"]);
                                    dec_TipoCambioCierreExp = Convert.ToDecimal(lds_Expediente.Tables["Table"].Rows[0]["TipoCambioCierre"]);
                                    dec_MontoPrincipalExp = Convert.ToDecimal(lds_Expediente.Tables["Table"].Rows[0]["MontoPrincipal"]);
                                    dec_MontoInteresesExp = Convert.ToDecimal(lds_Expediente.Tables["Table"].Rows[0]["MontoIntereses"]);
                                    dec_MontoInteresesMoraExp = Convert.ToDecimal(lds_Expediente.Tables["Table"].Rows[0]["InteresesMoratorios"]);
                                    dec_MontoCostasExp = Convert.ToDecimal(lds_Expediente.Tables["Table"].Rows[0]["Costas"]);
                                    dec_MontoDanosExp = Convert.ToDecimal(lds_Expediente.Tables["Table"].Rows[0]["DanoMoral"]);

                                    ds_opciones = oc.ConsultarOpcionesCatalogo(null, "TiposProcesoExpediente", null, lds_Expediente.Tables["Table"].Rows[0]["TipoProcesoExpediente"].ToString().Trim());
                                    if (ds_opciones.Tables.Count > 0 && ds_opciones.Tables["Table"].Rows.Count > 0)
                                    {
                                        str_IdTipoProcesoExp = ds_opciones.Tables["Table"].Rows[0]["ValOpcion"].ToString().Trim();
                                    }
                                }
                            }
                        }
                        #endregion Expediente
                        //lds_Pagos = lpago.ConsultarPagosFormulario(Convert.ToInt32(lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"]),
                        //                                              Convert.ToInt32(lds_Formularios.Tables["Table"].Rows[i]["Anno"])
                        //                                              );
                        lds_Pagos = lpago.ConsultarPagosFormulario(Convert.ToInt32(lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"]),
                                                                      Convert.ToInt32(lds_Formularios.Tables["Table"].Rows[i]["Anno"]),
                                                                      null,
                                                                      null,
                                                                      null,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      "A"
                                                                      );
                        #region Pagos
                        //solo itero si el dataset tiene registros
                        if (lds_Pagos.Tables.Count > 0 && lds_Pagos.Tables["Table"].Rows.Count > 0)
                        {
                            lbln_ErrorAsientoLinea = false;
                            /////////////////////////////////////
                            SigafTablaAsientoPago = new LogicaNegocio.wrSigafAsientos.ZfiAsiento[lds_Pagos.Tables["Table"].Rows.Count * 4];
                            SigafTablaAsientoDev1 = new LogicaNegocio.wrSigafAsientos.ZfiAsiento[lds_Pagos.Tables["Table"].Rows.Count * 4];
                            SigafTablaAsientoDev2 = new LogicaNegocio.wrSigafAsientos.ZfiAsiento[lds_Pagos.Tables["Table"].Rows.Count * 4];

                            int lint_lineaPago = 0;
                            int lint_lineaDev1 = 0;
                            int lint_lineaDev2 = 0;
                            //int lint_lineaVerifica = 0;
                            Boolean lbln_Pago = false;
                            Boolean lbln_Dev1 = false;
                            Boolean lbln_Dev2 = false;
                            /////////////////////////////////////
                            //itero en todos los formularios pagados para sacar los servicios pagados e irlos contabilizando
                            int y = 0;
                            while (lds_Pagos.Tables["Table"].Rows.Count > y && !lbln_ErrorAsientoLinea)
                            {

                                lbln_PeriodoActual = true;
                                if (Convert.ToInt32(lds_Pagos.Tables["Table"].Rows[y]["Periodo"]) != gint_AnnoActual)
                                {
                                    lbln_PeriodoActual = false;
                                }
                                //Obtengo los valores del servicio del pago para completar los parametros del webservice
                                lds_Servicios = lservicio.ConsultarServicios(lds_Pagos.Tables["Table"].Rows[y]["IdServicio"].ToString(),
                                                                             lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString(),
                                                                             string.Empty,
                                                                             string.Empty,
                                                                             string.Empty,
                                                                             string.Empty
                                                                             );
                                #region Servicios
                                //solo itero si el dataset tiene registros
                                if (lds_Servicios.Tables.Count > 0 && lds_Servicios.Tables["Table"].Rows.Count > 0)
                                {
                                    //lds_Operaciones = loperacion.ConsultarOperaciones("",lstr_IdModulo,string.Empty);
                                    ////solo itero si el dataset tiene registros
                                    //if (lds_Operaciones.Tables.Count > 0) {
                                    #region TiposAsiento

                                    #region AsientoCI
                                    lds_TiposAsiento = this.ConsultarTiposAsiento(lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString(),
                                                                                      lstr_IdModulo,
                                                                                      lds_Servicios.Tables["Table"].Rows[0]["IdServicio"].ToString(),
                                                                                      string.Empty,
                                                                                      string.Empty,
                                                                                      string.Empty,
                                                                                      string.Empty,
                                                                                      string.Empty,
                                                                                      string.Empty);


                                    //solo itero si el dataset tiene registros
                                    if (lds_TiposAsiento.Tables.Count > 0 && lds_TiposAsiento.Tables["Table"].Rows.Count > 0)
                                    {

                                        //En este punto ya tengo los datos del formulario, el detalle del pago y el detalle del servicio, ya puedo consumir el webservice del asiento
                                        //SigafTablaAsientoPago = new LogicaNegocio.wrSigafAsientos.ZfiAsiento[lds_TiposAsiento.Tables["Table"].Rows.Count * 2];
                                        //int lint_lineaPago = 0;
                                        //itero en todos los formularios pagados para sacar los servicios pagados e irlos contabilizando
                                        for (int z = 0; lds_TiposAsiento.Tables["Table"].Rows.Count > z; z++)
                                        {
                                            //inicializa el contador de las líenas
                                            lint_lineaVerifica = 0;
                                            if (string.IsNullOrEmpty(lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar3"].ToString().Trim()))
                                            {
                                                lds_SociedadesGLSociedadesFi = lsociedadGL.ConsultarSociedadesGLSociedadesFi(lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString(), lstr_IdModulo, string.Empty);
                                            }
                                            else
                                            {
                                                lds_SociedadesGLSociedadesFi = lsociedadGL.ConsultarSociedadesGLSociedadesFi(lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar3"].ToString(), lstr_IdModulo, string.Empty);
                                            }


                                            //solo itero si el dataset tiene registros
                                            if (lds_SociedadesGLSociedadesFi.Tables.Count > 0 && lds_SociedadesGLSociedadesFi.Tables["Table"].Rows.Count > 0)
                                            {

                                                lstr_IdSociedadFI = lds_SociedadesGLSociedadesFi.Tables["Table"].Rows[0]["IdSociedadFi"].ToString().Trim();
                                            }//if de sociedad fi encontrado

                                            if ((lbln_PeriodoActual && lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar2"].ToString().Trim() == "ACT") ||
                                                (!(lbln_PeriodoActual) && lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar2"].ToString().Trim() == "ANT") ||
                                                string.IsNullOrEmpty(lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar2"].ToString().Trim()))
                                            {
                                                lbln_Pago = false;
                                                lbln_Dev1 = false;
                                                lbln_Dev2 = false;
                                                //Verifico si la linea del asiento está Activa
                                                if (lds_TiposAsiento.Tables["Table"].Rows[z]["Estado"].ToString().Trim() == "A")
                                                {
                                                    switch (lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar"].ToString().Trim())
                                                    {
                                                        case "PAGADO":
                                                            lbln_Pago = true;
                                                            //lint_lineaVerifica = lint_lineaPago;
                                                            break;
                                                        case "DEVENGO":
                                                            lbln_Dev1 = true;
                                                            //lint_lineaVerifica = lint_lineaDev1;
                                                            break;
                                                        case "-DEVENGO":
                                                            lbln_Dev2 = true;
                                                            //lint_lineaVerifica = lint_lineaDev2;
                                                            break;
                                                        default:
                                                            break;
                                                    }



                                                    SigafLinea = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();

                                                    SigafLinea.Blart = "RI";// lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar4"].ToString().Trim(); //Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[i]["IdClaseDoc"]); //"SA"; //BLART = CLASE DE DOCUMENTO
                                                    SigafLinea.Bukrs = lstr_IdSociedadFI;//Convert.ToString(lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"]).Trim();//"G206";//BUKRS = ID de Sociedad
                                                    if (lint_lineaVerifica == 0 && (lint_lineaPago == 0 || lint_lineaDev1 == 0 || lint_lineaDev2 ==0 ))
                                                    {
                                                        SigafLinea.Bldat = ldt_FechaActual.ToString("dd.MM.yyyy");//Convert.ToString(ldt_FechaActual);//"01.10.2015";//bldat = Fecha de Documento
                                                        SigafLinea.Budat = ldt_FechaActual.ToString("dd.MM.yyyy");//Convert.ToString(ldt_FechaActual);//"01.10.2015";//budat = Fecha de Contabilizacion
                                                        lint_lineaVerifica = 1;
                                                    }
                                                    else
                                                    {
                                                        SigafLinea.Bldat = "";//Convert.ToString(ldt_FechaActual);//"01.10.2015";//bldat = Fecha de Documento
                                                        SigafLinea.Budat = "";//Convert.ToString(ldt_FechaActual);//"01.10.2015";//budat = Fecha de Contabilizacion
                                                    }
                                                    SigafLinea.Waers = lds_Pagos.Tables["Table"].Rows[y]["IdMoneda"].ToString().Trim();//"CRC";//waers = IdMoneda
                                                    SigafLinea.Xblnr = Convert.ToString(lds_Formularios.Tables["Table"].Rows[i]["Anno"]) + "." + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim();//"REF";//Xblnr = Referencia
                                                    SigafLinea.Bktxt = lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(); //"Texto_Cabecera";//Bktxt = "Texto_Cabecera"

                                                    //SigafLinea.Xref1Hd = ; //"REF_1";//Xref1Hd = "REF_1"
                                                    SigafLinea.Xref2Hd = "CI." + Convert.ToString(lds_Formularios.Tables["Table"].Rows[i]["Anno"]) + "." + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(); //"REF_2";//Xref2Hd = "REF_2"
                                                    SigafLinea.Bschl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdClaveContable"]).Trim(); //"40";//Bschl = Clave Contable
                                                    SigafLinea.Hkont = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCuentaContable"]).Trim(); //"5120302000";//Hkont = Cuenta Contable de Mayor
                                                    SigafLinea.Wrbtr = Convert.ToDecimal(lds_Pagos.Tables["Table"].Rows[y]["Monto"]); //1505;//Wrbtr = Monto Importe
                                                    SigafLinea.Zuonr = "";//Zuonr = "Asignacion"
                                                    SigafLinea.Sgtxt = "";//Sgtxt = "Texto"
                                                    if (lbln_Pago && SigafLinea.Bschl.Trim() == "50" && !string.IsNullOrEmpty(Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim()))
                                                    {
                                                        lds_ReservasDetallado = lreservasDet.ConsultarReservasDetallado(Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim(), "", "", "", "");

                                                        //solo itero si el dataset tiene registros
                                                        if (lds_ReservasDetallado.Tables.Count > 0 && lds_ReservasDetallado.Tables["Table"].Rows.Count > 0)
                                                        {

                                                            SigafLinea.Fipex = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdPosPre"]).Trim(); //"E-10302";//Fipex = posicion presupuestaria
                                                            SigafLinea.Kostl = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdCentroCosto"]).Trim(); //"20613200";//Kostl = Centro de Costo
                                                            SigafLinea.Fistl = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdCentroGestor"]).Trim(); //"20613200";//Fistl = Centro Gestor
                                                            SigafLinea.Geber = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdFondo"]).Trim(); //"001";//Geber = Fondo                                                        }//if de Reservas encontrado
                                                        }
                                                    }
                                                    else
                                                    {
                                                        SigafLinea.Fipex = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdPosPre"]).Trim(); //"E-10302";//Fipex = posicion presupuestaria
                                                        SigafLinea.Kostl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroCosto"]).Trim(); //"20613200";//Kostl = Centro de Costo
                                                        SigafLinea.Fistl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroGestor"]).Trim(); //"20613200";//Fistl = Centro Gestor
                                                        SigafLinea.Geber = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdFondo"]).Trim(); //"001";//Geber = Fondo
                                                    }
                                                    SigafLinea.Fkber = "";//Fkber = Area Funcional
                                                    SigafLinea.Xref2 = ""; //Xref2 = Flujo de efectivo

                                                    SigafLinea.Valut = "";//Valut = Fecha de Valor                                                  
                                                    SigafLinea.Umskz = "";//Umskz = Indicador CME
                                                    SigafLinea.Werks = "";//Werks = Centro
                                                    SigafLinea.Xmwst = ""; //Calculo de Impuesto
                                                    SigafLinea.Xref3 = "";//Flujo de Efectivo
                                                    SigafLinea.Zfbdt = "";//Fecha base
                                                    SigafLinea.Zlsch = "";//Via de pago
                                                    SigafLinea.Projk = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdElementoPEP"]).Trim(); ////Projk = Id Elemento PEP
                                                    if (string.IsNullOrEmpty(lstr_IdCeBe) || !SigafLinea.Hkont.StartsWith("4"))
                                                    {
                                                        SigafLinea.Prctr = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroBeneficio"]).Trim(); ////Prctr = centro de beneficio
                                                    }
                                                    else
                                                    {
                                                        SigafLinea.Prctr = lstr_IdCeBe;
                                                    }
                                                    SigafLinea.Mwskz = "";//Mwskz = indicador de impuesto
                                                    SigafLinea.Measure = "";//Measure = programa presupuestario
                                                    SigafLinea.Kursf = 0;//Kursf = tipo de cambio
                                                    SigafLinea.Aufnr = "";//Aufnr = Orden
                                                    SigafLinea.Dmbe2 = 0;//Dmbe2 = importe en moneda fuerte
                                                    //if (Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCuentaContable"]).StartsWith("5") )
                                                    //    SigafLinea.Kblnr = Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim(); //Kblnr = documento presupuestario
                                                    //else
                                                    SigafLinea.Kblnr = ""; //Kblnr = documento presupuestario
                                                    SigafLinea.Kblpos = "";//Kblpos = posicion documento presupuestario
                                                    SigafLinea.Rcomp = "";
                                                    SigafLinea.Buzei = "";
                                                    SigafLinea.Mandt = "";
                                                    SigafLinea.Hbkid = "";//Hbkid = banco propio

                                                    switch (lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar"].ToString().Trim())
                                                    {
                                                        case "PAGADO":
                                                            SigafLinea.Xref1Hd = "PAGADO"; //"REF_1";//Xref1Hd = "REF_1"
                                                            SigafTablaAsientoPago[lint_lineaPago] = SigafLinea;
                                                            lint_lineaPago++;
                                                            break;
                                                        case "DEVENGO":
                                                            SigafLinea.Xref1Hd = "DEVENGO"; //"REF_1";//Xref1Hd = "REF_1"
                                                            SigafTablaAsientoDev1[lint_lineaDev1] = SigafLinea;
                                                            lint_lineaDev1++;
                                                            break;
                                                        case "-DEVENGO":
                                                            SigafLinea.Xref1Hd = "-DEVENGO"; //"REF_1";//Xref1Hd = "REF_1"
                                                            SigafTablaAsientoDev2[lint_lineaDev2] = SigafLinea;
                                                            lint_lineaDev2++;
                                                            break;
                                                        default:
                                                            break;
                                                    }



                                                    if (!string.IsNullOrEmpty(Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdClaveContable2"])))
                                                    {
                                                        ///cuenta 2
                                                        SigafLinea = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();

                                                        SigafLinea.Blart = "RI";//lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar4"].ToString().Trim();  //"SA"; //BLART = CLASE DE DOCUMENTO
                                                        SigafLinea.Bukrs = "";//"G206";//BUKRS = ID de Sociedad
                                                        SigafLinea.Bldat = "";//"01.10.2015";//bldat = Fecha de Documento
                                                        SigafLinea.Budat = "";//"01.10.2015";//budat = Fecha de Contabilizacion
                                                        SigafLinea.Waers = "";//"CRC";//waers = IdMoneda
                                                        SigafLinea.Xblnr = "";//"REF";//Xblnr = Referencia
                                                        SigafLinea.Bktxt = ""; //"Texto_Cabecera";//Bktxt = "Texto_Cabecera"
                                                        SigafLinea.Xref1Hd = ""; //"REF_1";//Xref1Hd = "REF_1"
                                                        SigafLinea.Xref2Hd = ""; //"REF_2";//Xref2Hd = "REF_2"
                                                        SigafLinea.Bschl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdClaveContable2"]).Trim(); //"40";//Bschl = Clave Contable
                                                        SigafLinea.Hkont = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCuentaContable2"]).Trim(); //"5120302000";//Hkont = Cuenta Contable de Mayor
                                                        SigafLinea.Wrbtr = Convert.ToDecimal(lds_Pagos.Tables["Table"].Rows[y]["Monto"]); //1505;//Wrbtr = Monto Importe
                                                        SigafLinea.Zuonr = "";//Zuonr = "Asignacion"
                                                        SigafLinea.Sgtxt = "";//Sgtxt = "Texto"
                                                        if (lbln_Pago && SigafLinea.Bschl.Trim() == "50" && !string.IsNullOrEmpty(Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim()))
                                                        {
                                                            lds_ReservasDetallado = lreservasDet.ConsultarReservasDetallado(Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim(), "", "", "", "");

                                                            //solo itero si el dataset tiene registros
                                                            if (lds_ReservasDetallado.Tables.Count > 0 && lds_ReservasDetallado.Tables["Table"].Rows.Count > 0)
                                                            {

                                                                SigafLinea.Fipex = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdPosPre"]).Trim(); //"E-10302";//Fipex = posicion presupuestaria
                                                                SigafLinea.Kostl = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdCentroCosto"]).Trim(); //"20613200";//Kostl = Centro de Costo
                                                                SigafLinea.Fistl = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdCentroGestor"]).Trim(); //"20613200";//Fistl = Centro Gestor
                                                                SigafLinea.Geber = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdFondo"]).Trim(); //"001";//Geber = Fondo                                                        }//if de Reservas encontrado
                                                            }
                                                        }
                                                        else
                                                        {
                                                            SigafLinea.Fipex = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdPosPre2"]).Trim(); //"E-10302";//Fipex = posicion presupuestaria
                                                            SigafLinea.Kostl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroCosto2"]).Trim(); //"20613200";//Kostl = Centro de Costo
                                                            SigafLinea.Fistl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroGestor2"]).Trim(); //"20613200";//Fistl = Centro Gestor
                                                            SigafLinea.Geber = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdFondo2"]).Trim(); //"001";//Geber = Fondo
                                                        }
                                                        SigafLinea.Fkber = "";//Fkber = Area Funcional
                                                        SigafLinea.Xref2 = ""; //Xref2 = Flujo de efectivo

                                                        SigafLinea.Valut = "";//Valut = Fecha de Valor                                                  
                                                        SigafLinea.Umskz = "";//Umskz = Indicador CME
                                                        SigafLinea.Werks = "";//Werks = Centro
                                                        SigafLinea.Xmwst = ""; //Calculo de Impuesto
                                                        SigafLinea.Xref3 = "";//Flujo de Efectivo
                                                        SigafLinea.Zfbdt = "";//Fecha base
                                                        SigafLinea.Zlsch = "";//Via de pago
                                                        SigafLinea.Projk = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdElementoPEP2"]).Trim(); ////Projk = Id Elemento PEP
                                                        if (string.IsNullOrEmpty(lstr_IdCeBe) || !SigafLinea.Hkont.StartsWith("4"))
                                                        {
                                                            SigafLinea.Prctr = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroBeneficio2"]).Trim(); ////Prctr = centro de beneficio
                                                        }
                                                        else
                                                        {
                                                            SigafLinea.Prctr = lstr_IdCeBe;
                                                        }
                                                        SigafLinea.Mwskz = "";//Mwskz = indicador de impuesto
                                                        SigafLinea.Measure = "";//Measure = programa presupuestario
                                                        SigafLinea.Kursf = 0;//Kursf = tipo de cambio
                                                        SigafLinea.Aufnr = "";//Aufnr = Orden
                                                        SigafLinea.Dmbe2 = 0;//Dmbe2 = importe en moneda fuerte
                                                        //if (Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCuentaContable2"]).StartsWith("5"))
                                                        //    SigafLinea.Kblnr = Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim();//Kblnr = documento presupuestario
                                                        //else
                                                        SigafLinea.Kblnr = "";//Kblnr = documento presupuestario
                                                        SigafLinea.Kblpos = "";//Kblpos = posicion documento presupuestario
                                                        SigafLinea.Rcomp = "";
                                                        SigafLinea.Buzei = "";
                                                        SigafLinea.Mandt = "";
                                                        SigafLinea.Hbkid = "";//Hbkid = banco propio


                                                        switch (lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar"].ToString().Trim())
                                                        {
                                                            case "PAGADO":
                                                                SigafTablaAsientoPago[lint_lineaPago] = SigafLinea;
                                                                lint_lineaPago++;
                                                                break;
                                                            case "DEVENGO":
                                                                SigafTablaAsientoDev1[lint_lineaDev1] = SigafLinea;
                                                                lint_lineaDev1++;
                                                                break;
                                                            case "-DEVENGO":
                                                                SigafTablaAsientoDev2[lint_lineaDev2] = SigafLinea;
                                                                lint_lineaDev2++;
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                    //SigafAsientoLogPago = lasiento.EnviarAsientoSigaf(SigafTablaAsientoPago);
                                                }
                                            }//if de estado Activo de la linea del asiento
                                        }//for de las lineas del asiento
                                    }//if asiento encontrado
                                    else
                                    {
                                        //Tipo de Asiento no Encontrado
                                        lstr_Mensaje = "Tipo de Asiento no Encontrado" + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim() + "." + lds_Servicios.Tables["Table"].Rows[0]["IdServicio"].ToString().Trim();

                                        resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora("CI", lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Envio Asientos", lstr_Mensaje, lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Asiento", lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim());
                                        lbln_ErrorAsientoLinea = true;

                                    }//Tipo de Asiento no encontrado
                                    #endregion AsientoCI

                                    #endregion TiposAsiento
                                }//if servicio encontrado
                                else
                                {
                                    //Servicio no Encontrado
                                    lstr_Mensaje = "Servicio no Encontrado" + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim() + "." + lds_Pagos.Tables["Table"].Rows[y]["IdServicio"].ToString().Trim();

                                    resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora("CI", lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Envio Asientos", lstr_Mensaje, lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Servicio", lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim());
                                        
                                    lbln_ErrorAsientoLinea = true;
                                }//servicio no encontrado
                                #endregion Servicios
                                y++;
                            }//while de pagos
                            /////////////////////////////////////////

                            if (!lbln_ErrorAsientoLinea)
                            {
                                #region InterfazTest

                                try //Interfaz
                                {
                                    lbln_ErrorInterfaz = false;
                                    if (lint_lineaDev1 > 0)
                                    {
                                        logAsientoDev1 = this.EnviarAsientoSigaf(SigafTablaAsientoDev1, "X");
                                        /*logAsientoDev1 = string.Empty;
                                        for (int w = 0; w < item_resAsientosLogDev1.Length; w++)
                                        {
                                            logAsientoDev1 += "\n" + i + "-" + item_resAsientosLogDev1[w];
                                        }*/
                                        //MessageBox.Show("Resultado de contabilización: \n\n"+logAsientoPago);
                                        Log.Info("Resultado de contabilización Devengo+: \n\n" + logAsientoDev1);
                                        if (
                                            logAsientoDev1.Contains("[E]")
                                            )
                                        {
                                            lbln_ErrorAsientoLinea = true;
                                            lbln_ErrorInterfaz = true;
                                        }

                                    }
                                    if (lint_lineaDev2 > 0 && !lbln_ErrorInterfaz)
                                    {
                                        logAsientoDev2 = this.EnviarAsientoSigaf(SigafTablaAsientoDev2, "X");
                                        /*logAsientoDev2 = string.Empty;
                                        for (int w = 0; w < item_resAsientosLogDev2.Length; w++)
                                        {
                                            logAsientoDev2 += "\n" + i + "-" + item_resAsientosLogDev2[w];
                                        }*/
                                        //MessageBox.Show("Resultado de contabilización: \n\n"+logAsientoPago);
                                        Log.Info("Resultado de contabilización Devengo-: \n\n" + logAsientoDev2);
                                        if (
                                            logAsientoDev2.Contains("[E]")
                                            )
                                        {
                                            lbln_ErrorAsientoLinea = true;
                                            lbln_ErrorInterfaz = true;
                                        }

                                    }
                                    //SigafAsientoLogPago = lasiento.EnviarAsientoSigaf(SigafTablaAsientoPago);
                                    if (lint_lineaPago > 0 && !lbln_ErrorInterfaz)
                                    {
                                        logAsientoPago = this.EnviarAsientoSigaf(SigafTablaAsientoPago, "X");
                                        /*logAsientoPago = string.Empty;
                                        for (int w = 0; w < item_resAsientosLogPago.Length; w++)
                                        {
                                            logAsientoPago += "\n" + i + "-" + item_resAsientosLogPago[w];
                                        }*/
                                        //MessageBox.Show("Resultado de contabilización: \n\n"+logAsientoPago);
                                        Log.Info("Resultado de contabilización Pago: \n\n" + logAsientoPago);
                                        if (logAsientoPago.Contains("[E]")
                                            )
                                        {
                                            lbln_ErrorAsientoLinea = true;
                                            lbln_ErrorInterfaz = true;
                                        }

                                    }


                                }
                                catch (Exception ex)
                                {
                                    lstr_Mensaje = "Error al invocar interfaz asientos Sigaf: " + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString() + ex.ToString();
                                    Log.Error(lstr_Mensaje);
                                    //lbln_ErrorAsientoLinea = true;
                                    lbln_ErrorInterfaz = true;
                                }//catch

                                #endregion InterfazTest

                                #region Interfaz
                                if (!lbln_ErrorInterfaz)
                                {
                                    try //Interfaz
                                    {
                                        lbln_ErrorInterfaz = false;
                                        if (lint_lineaDev1 > 0)
                                        {
                                            logAsientoDev1 = this.EnviarAsientoSigaf(SigafTablaAsientoDev1);
                                            /*logAsientoDev1 = string.Empty;
                                            for (int w = 0; w < item_resAsientosLogDev1.Length; w++)
                                            {
                                                logAsientoDev1 += "\n" + i + "-" + item_resAsientosLogDev1[w];
                                            }*/
                                            //MessageBox.Show("Resultado de contabilización: \n\n"+logAsientoPago);
                                            Log.Info("Resultado de contabilización Devengo+: \n\n" + logAsientoDev1);
                                            if (
                                                logAsientoDev1.Contains("[E]")
                                                )
                                            {
                                                lbln_ErrorAsientoLinea = true;
                                                lbln_ErrorInterfaz = true;
                                            }

                                        }
                                        if (lint_lineaDev2 > 0 && !lbln_ErrorInterfaz)
                                        {
                                            logAsientoDev2 = this.EnviarAsientoSigaf(SigafTablaAsientoDev2);
                                            /*logAsientoDev2 = string.Empty;
                                            for (int w = 0; w < item_resAsientosLogDev2.Length; w++)
                                            {
                                                logAsientoDev2 += "\n" + i + "-" + item_resAsientosLogDev2[w];
                                            }*/
                                            //MessageBox.Show("Resultado de contabilización: \n\n"+logAsientoPago);
                                            Log.Info("Resultado de contabilización Devengo-: \n\n" + logAsientoDev2);
                                            if (
                                                logAsientoDev2.Contains("[E]")
                                                )
                                            {
                                                lbln_ErrorAsientoLinea = true;
                                                lbln_ErrorInterfaz = true;
                                            }

                                        }
                                        //SigafAsientoLogPago = lasiento.EnviarAsientoSigaf(SigafTablaAsientoPago);
                                        if (lint_lineaPago > 0 && !lbln_ErrorInterfaz)
                                        {
                                            logAsientoPago = this.EnviarAsientoSigaf(SigafTablaAsientoPago);
                                            /*logAsientoPago = string.Empty;
                                            for (int w = 0; w < item_resAsientosLogPago.Length; w++)
                                            {
                                                logAsientoPago += "\n" + i + "-" + item_resAsientosLogPago[w];
                                            }*/
                                            //MessageBox.Show("Resultado de contabilización: \n\n"+logAsientoPago);
                                            Log.Info("Resultado de contabilización Pago: \n\n" + logAsientoPago);
                                            if (logAsientoPago.Contains("[E]")
                                                )
                                            {
                                                lbln_ErrorAsientoLinea = true;
                                                lbln_ErrorInterfaz = true;
                                            }

                                        }


                                    }
                                    catch (Exception ex)
                                    {
                                        lstr_Mensaje = "Error al invocar interfaz asientos Sigaf: " + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString() + ex.ToString();
                                        Log.Error(lstr_Mensaje);
                                        //lbln_ErrorAsientoLinea = true;
                                        lbln_ErrorInterfaz = true;
                                    }//catch
                                }
                                #endregion Interfaz

                                #region Bitacora
                                lbln_ErrorBitacora = false;
                                //Registro de bitacora de movimientos
                                try
                                {
                                    resAsientosLogPago = reg_Bitacora.ufnRegistrarAccionBitacora("CI", lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Envio Asientos", logAsientoPago, lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Pago", lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim());
                                    /*item_resAsientosLogPago = reg_Bitacora.RegistrarBitacoraDeMovimientosCuentasExpedientes(
                                        lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString(),
                                        "CI",
                                        lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString(),
                                        lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString(), //+ "." + lds_Pagos.Tables["Table"].Rows[y]["IdPago"].ToString(),
                                        "",
                                        0,
                                        0,
                                        0,
                                        0,
                                        logAsientoPago,
                                        ""
                                        );
                                    */
                                }//try registro en bitácora
                                catch (Exception err)
                                {
                                    lstr_Mensaje = "Error al registrar en bitácora Pago: " + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim() + err.Message;
                                    item_resAsientosLogPago[0] = lstr_Mensaje;
                                    lbln_ErrorBitacora = true;
                                    Log.Error(lstr_Mensaje);

                                }//catch

                                //Registro de bitacora de movimientos
                                try
                                {
                                    resAsientosLogDev1 = reg_Bitacora.ufnRegistrarAccionBitacora("CI", lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Envio Asientos", logAsientoDev1, lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Devengo1", lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim());
                                    /*item_resAsientosLogDev1 = reg_Bitacora.RegistrarBitacoraDeMovimientosCuentasExpedientes(
                                        lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString(),
                                        "CI",
                                        lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString(),
                                        lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString(), //+ "." + lds_Pagos.Tables["Table"].Rows[y]["IdPago"].ToString(),
                                        "",
                                        0,
                                        0,
                                        0,
                                        0,
                                        logAsientoDev1,
                                        ""
                                        );*/

                                }//try registro en bitácora
                                catch (Exception err)
                                {
                                    lstr_Mensaje = "Error al registrar en bitácora Devengo+: " + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim() + err.Message;
                                    item_resAsientosLogDev1[0] = lstr_Mensaje;
                                    lbln_ErrorBitacora = true;
                                    Log.Error(lstr_Mensaje);

                                }//catch

                                //Registro de bitacora de movimientos
                                try
                                {
                                    resAsientosLogDev2 = reg_Bitacora.ufnRegistrarAccionBitacora("CI", lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Envio Asientos", logAsientoDev2, lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Devengo2", lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim());
                                    /*item_resAsientosLogDev2 = reg_Bitacora.RegistrarBitacoraDeMovimientosCuentasExpedientes(
                                        lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString(),
                                        "CI",
                                        lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString(),
                                        lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString(), //+ "." + lds_Pagos.Tables["Table"].Rows[y]["IdPago"].ToString(),
                                        "",
                                        0,
                                        0,
                                        0,
                                        0,
                                        logAsientoDev2,
                                        ""
                                        );*/

                                }//try registro en bitácora
                                catch (Exception err)
                                {
                                    lstr_Mensaje = "Error al registrar en bitácora Devengo-: " + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim() + err.Message;
                                    item_resAsientosLogDev2[0] = lstr_Mensaje;
                                    lbln_ErrorBitacora = true;
                                    Log.Error(lstr_Mensaje);

                                }//catch
                                //if (item_resAsientosLogPago[0].Contains("[E]") ||
                                //    item_resAsientosLogDev1[0].Contains("[E]") ||
                                //    item_resAsientosLogDev2[0].Contains("[E]")
                                //    )
                                //{
                                //    lbln_ErrorAsientoLinea = true;
                                //}                                           

                                #endregion Bitacora
                            }// if no Error Asiento Linea


                            //////////////////////////////////////
                            #region CambioEstado
                            if (!lbln_ErrorAsientoLinea && !lbln_ErrorInterfaz)
                            {
                                //Contabilizado bien, hay que cambiar el estado del formulario a Contabilizado
                                string lstr_TxtSalida = "";
                                string lstr_CodSalida = "";
                                string lstr_MensajeEstado = "";
                                Boolean lbln_Resultado = true;
                                try
                                {
                                    lbln_Resultado = lcls_Formulario.CambiarEstadoFormularioCapturaIngresos(
                                        Convert.ToInt32(lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"]),
                                        gint_AnnoActual,
                                        "PAG",
                                        "CNT",
                                        "",
                                        "",
                                        out lstr_CodSalida,
                                        out lstr_TxtSalida
                                        );
                                    if (lbln_Resultado)
                                        lstr_MensajeEstado = "00";
                                    else
                                        lstr_MensajeEstado = "Código " + lstr_CodSalida + ": " + lstr_TxtSalida;

                                    if (lstr_MensajeEstado != "00")
                                    {
                                        lbln_Resultado = false;
                                        Log.Error("Error al cambiar estado a pagado: " + lstr_MensajeEstado + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim());
                                    }
                                }
                                catch (Exception ex)
                                {
                                    lstr_Mensaje = "Error al cambiar estado a pagado: " + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim() + ex.ToString();
                                    Log.Error(lstr_Mensaje);
                                    lbln_ErrorCambioEstado = true;
                                }
                            } //if no Errores en contabilizacion de lineas  ni error de interfaz
                            else
                            {

                                Log.Error(lstr_Mensaje + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim());

                                //Console.WriteLine("Error: " + lstr_Mensaje + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString());
                                //Console.ReadLine().ToString();
                            }
                            #endregion CambioEstado
                        }//if pagos encontrados
                        #endregion Pagos


                    }//for de formularios
                }//if de formularios encontrados
                #endregion Formularios
                return 0;
            }//try 
            catch (Exception ex)
            {
                lstr_Mensaje = ex.ToString(); //+ lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString();
                Log.Error(lstr_Mensaje);
                return -1;
                //Console.WriteLine("Error: " + lstr_Mensaje);
                //Console.ReadLine().ToString();
            }//catch
        }


        /// <summary>
        /// Procedimiento que contabiliza los formularios de captura de ingresos y contingentes.
        /// </summary>
        /// <param name="lint_AnnoActual">Anno Actual para buscar los formularios a contabilizar</param>
        /// <param name="lint_Idformulario">Identificador de formulario que se desea contabilizar</param>
        public int EnviarAsientosCICT(Int16? lint_AnnoActual = null, int lint_Idformulario = -1, string str_CodAuxiliar5 = "Captura")
        {
            ILog Log = LogManager.GetLogger("SGCierreContable");
            LogicaNegocio.wrSigafAsientos.ZfiAsiento SigafLinea = new LogicaNegocio.wrSigafAsientos.ZfiAsiento(); //wrSigafAsientos.ZfiAsiento();
            //wsAsientos.ZfiAsiento SigafLinea = new wsAsientos.ZfiAsiento(); //wrSigafAsientos.ZfiAsiento();
            LogicaNegocio.wrSigafAsientos.ZfiAsiento[] SigafTablaAsientoPago;
            //wsAsientos.ZfiAsiento[] SigafTablaAsientoPago;
            LogicaNegocio.wrSigafAsientos.ZfiAsiento[] SigafTablaAsientoDif;
            LogicaNegocio.wrSigafAsientos.ZfiAsientoLog[] SigafAsientoLogPago = new LogicaNegocio.wrSigafAsientos.ZfiAsientoLog[100];
            //string[] SigafAsientoLogPago = new string[100];
            //wsAsientos.ZfiAsientoLog[] SigafAsientoLogPago = new wsAsientos.ZfiAsientoLog[100];
            DateTime ldt_FechaActual = DateTime.Today;//new DateTime();

            DateTime ldt_FechaPago = DateTime.Today;//new DateTime();
            //-1 para que tome todos los formularios
            int lint_IdPago = -1;
            Int16 gint_AnnoActual;

            gint_AnnoActual = (lint_AnnoActual == null) ? Convert.ToInt16(ldt_FechaActual.Year) : Convert.ToInt16(lint_AnnoActual);
            DateTime ldt_FchPagoDesde = Convert.ToDateTime("1900-01-01");
            DateTime ldt_FchPagoHasta = Convert.ToDateTime("2099-12-31");
            DataSet lds_Formularios = new DataSet();
            DataSet lds_Pagos = new DataSet();
            DataSet lds_Servicios = new DataSet();
            DataSet lds_TiposAsiento = new DataSet();
            DataSet lds_Operaciones = new DataSet();
            DataSet lds_Oficinas = new DataSet();
            DataSet lds_SociedadesGLSociedadesFi = new DataSet();
            DataSet lds_Expediente = new DataSet();
            DataSet lds_ReservasDetallado = new DataSet();

            clsOpcionesCatalogo oc = new clsOpcionesCatalogo();
            DataSet ds_opciones = new DataSet();

            clsFormulariosCapturaIngresos lformulario = new clsFormulariosCapturaIngresos();
            clsPagosPorFormulario lpago = new clsPagosPorFormulario();
            //clsTiposAsiento lasiento = new clsTiposAsiento();
            clsServicios lservicio = new clsServicios();
            clsOperaciones loperacion = new clsOperaciones();
            clsOficinas loficina = new clsOficinas();
            tSociedadGL lsociedadGL = new tSociedadGL();
            clsExpedientes lexpediente = new clsExpedientes();
            clsResoluciones lresolucion = new clsResoluciones();
            clsReservasDetalle lreservasDet = new clsReservasDetalle();
            //clsBitacoraDeMovimientosCuentasExpedientes reg_Bitacora = new clsBitacoraDeMovimientosCuentasExpedientes();
            tBitacora reg_Bitacora = new tBitacora();
            clsFormulariosCapturaIngresos lcls_Formulario = new clsFormulariosCapturaIngresos();
            string lstr_IdModulo = "IdModulo IN ('CI')";
            string lstr_Mensaje = "";
            string lstr_IdSociedadFI = "";
            string lstr_IdCeBe = "";
            string logAsientoPago = string.Empty;
            string[] item_resAsientosLogPago = new string[100];
            string resAsientosLogPago = string.Empty;
            string str_IdTipoProcesoExp = string.Empty;
            string str_MonedaExp = string.Empty;
            string lstr_EstadoResolucion = string.Empty;
            string lstr_IdExpedienteFK = string.Empty;
            string lstr_IdSociedadGL = string.Empty;
            String[] lstr_Resultado = new String[2];
            DateTime dt_FchTipoCambioExp;
            decimal dec_TipoCambioOrig = 0;
            decimal dec_TipoCambioCierreExp = 0;
            decimal dec_TipoCambioExp = 0;
            decimal dec_TipoCambioEUR = 0;
            decimal dec_MontoPrincipalExp = 0;
            decimal dec_MontoInteresesExp = 0;
            decimal dec_MontoInteresesMoraExp = 0;
            decimal dec_MontoCostasExp = 0;
            decimal dec_MontoDanosExp = 0;
            int lint_IdCPResEnFirme = 0;
            int lint_IdCPResLiq = 0;


            int lint_IdRes = 0;
            string lstr_IdResolucion = string.Empty;
            //string lstr_IdExpedienteFK = string.Empty;

            decimal dec_Pago = 0;
            decimal dec_PagoCRC = 0;
            decimal dec_PagoEUR = 0;
            decimal dec_PagoUSD = 0;

            decimal dec_DiferenciaPago = 0;
            decimal dec_DiferenciaPagoCRC = 0;
            decimal dec_DiferenciaPagoUSD = 0;
            decimal dec_DiferenciaPagoEUR = 0;
            decimal dec_DiferenciaTC = 0;

            string str_CodError = string.Empty;
            string str_Mensaje = string.Empty;
            Boolean lbln_ErrorAsientoLinea = false;
            Boolean lbln_ErrorInterfaz = false;
            Boolean lbln_ErrorBitacora = false;
            Boolean lbln_ErrorCambioEstado = false;
            Boolean lbln_ExisteExpediente;
            Boolean lbln_TieneExpediente;
            Boolean lbln_PeriodoActual;
            Boolean lbln_AsientoEncontrado;

            decimal ldec_tipo_cambioUSD = 0;
            decimal ldec_tipo_cambioEUR = 0;

            string str_CodAuxiliar4 = string.Empty;
            string str_CodAuxiliar = string.Empty;
            string str_MonedaQry1 = "CRCN";

            string lstr_TransaccionVentaUSD = "3140";
            string lstr_TransaccionCompraUSD = "3280";



            try
            {

                //saco todos los formularios en estado Pagado para ser contabilizados.
                lds_Formularios = lformulario.ConsultarFormulariosCapturaIngresos(lint_Idformulario, gint_AnnoActual, string.Empty, string.Empty,
                                                                                string.Empty, string.Empty, string.Empty, string.Empty,
                                                                                "+", string.Empty, string.Empty, "PAG");

                #region Formularios
                //solo itero si el dataset tiene registros
                if (lds_Formularios.Tables.Count > 0 && lds_Formularios.Tables["Table"].Rows.Count > 0)
                {

                    //itero en todos los formularios pagados para sacar los servicios pagados e irlos contabilizando
                    for (int i = 0; lds_Formularios.Tables["Table"].Rows.Count > i; i++)
                    {
                        ldt_FechaPago = Convert.ToDateTime(lds_Formularios.Tables["Table"].Rows[i]["FchPago"].ToString().Trim());
                        DataSet ds_tipoCambio = tiposCambio.ConsultarTiposCambio(str_MonedaQry1, ldt_FechaPago, lstr_TransaccionCompraUSD, "N");
                        if (ds_tipoCambio.Tables.Count > 0 && ds_tipoCambio.Tables["Table"].Rows.Count > 0)
                        {
                            // se realiza el cambio a dolares para procesar el asiento
                            ldec_tipo_cambioUSD = Convert.ToDecimal(ds_tipoCambio.Tables["Table"].Rows[0]["Valor"]);

                            //ldec_monto_max_moneda = ldec_monto_max / ldec_tipo_cambioUSD;
                        }//if ds_tipoCambio

                        ds_tipoCambio = tiposCambio.ConsultarTiposCambio("EUR", ldt_FechaPago, string.Empty, "N");
                        if (ds_tipoCambio.Tables.Count > 0 && ds_tipoCambio.Tables["Table"].Rows.Count > 0)
                        {
                            // se realiza el cambio a dolares para procesar el asiento
                            ldec_tipo_cambioEUR = Math.Round(Convert.ToDecimal(ds_tipoCambio.Tables["Table"].Rows[0]["Valor"]), 2); ;


                        }//if ds_tipoCambio



                        int lint_lineaVerifica = 0;
                        //obtengo los pagos activos del formulario!
                        //lds_Pagos = lpago.ConsultarPagosPorFormulario(Convert.ToInt32(lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"]),
                        //                                              Convert.ToInt32(lds_Formularios.Tables["Table"].Rows[i]["Anno"]),
                        //                                              lint_IdPago,
                        //                                              ldt_FchPagoDesde,
                        //                                              ldt_FchPagoHasta,
                        //                                              string.Empty,
                        //                                              string.Empty,
                        //                                              string.Empty,
                        //                                              string.Empty,
                        //                                              string.Empty,
                        //                                              "A"
                        //                                              );
                        lstr_IdCeBe = "";
                        if (!string.IsNullOrEmpty(lds_Formularios.Tables["Table"].Rows[i]["IdOficina"].ToString().Trim()))
                        {
                            lds_Oficinas = loficina.ConsultarOficinasCeBe(lds_Formularios.Tables["Table"].Rows[i]["IdOficina"].ToString().Trim(), lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim(), "CI", string.Empty);
                            //solo itero si el dataset tiene registros
                            if (lds_Oficinas.Tables.Count > 0 && lds_Oficinas.Tables["Table"].Rows.Count > 0)
                            {

                                lstr_IdCeBe = lds_Oficinas.Tables["Table"].Rows[0]["IdCentroBeneficio"].ToString().Trim();
                            }

                        }


                        #region Expediente
                        lbln_TieneExpediente = false;

                        lbln_ExisteExpediente = false;
                        if (!string.IsNullOrEmpty(lds_Formularios.Tables["Table"].Rows[i]["NroExpediente"].ToString().Trim()))
                        {
                            lbln_TieneExpediente = true;
                            //lds_Expediente = lexpediente.con (lds_Formularios.Tables["Table"].Rows[i]["NroExpediente"].ToString().Trim(), lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim());
                            lds_Expediente = lresolucion.ConsultarResolucion(null, lds_Formularios.Tables["Table"].Rows[i]["NroExpediente"].ToString().Trim(), lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim(), out str_CodError, out str_Mensaje);

                            System.Data.DataTable dt_Res = lds_Expediente.Tables[0];
                            foreach (DataRow dr_Res in dt_Res.Rows)
                            { 
                            //if (lds_Expediente.Tables.Count > 0 && lds_Expediente.Tables["Table"].Rows.Count > 0)
                            //{
                                if (!string.IsNullOrEmpty(dr_Res["IdExpediente"].ToString().Trim()))
                                {
                                    lbln_ExisteExpediente = true;

                                    str_MonedaExp = dr_Res["Moneda"].ToString().Trim();
                                    lint_IdRes = Convert.ToInt32(dr_Res["IdRes"].ToString());
                                    lstr_IdResolucion = dr_Res["IdResolucion"].ToString();
                                    lstr_IdExpedienteFK = dr_Res["IdExpedienteFK"].ToString();//Llave que relaciona las resoluciones dictadas, con los expedientes existentes                       
                                    lstr_IdSociedadGL = dr_Res["IdSociedadGL"].ToString();
                                    lstr_EstadoResolucion = dr_Res["EstadoResolucion"].ToString();//Campo que define qué tipo de resolución es, Resolución Provisional 1 ó 2,  Resolución en Firme (aplica tanto para activos contingentes como para pasivos contingentes), o Declaración Sin Lugar.
                                    if (str_MonedaExp != "CRC")
                                    {
                                        try
                                        {
                                            dec_TipoCambioOrig = Convert.ToDecimal(dr_Res["TipoCambio1"]);
                                        }
                                        catch (Exception e)
                                        {
                                            dec_TipoCambioOrig = 0;
                                        }
                                        try
                                        {
                                            dec_TipoCambioCierreExp = Convert.ToDecimal(dr_Res["TipoCambioCierre"]);
                                        }
                                        catch (Exception e)
                                        {
                                            dec_TipoCambioCierreExp = 0;
                                        }

                                    }
                                    dt_FchTipoCambioExp = Convert.ToDateTime(dr_Res["FechResolucion"]);
                                    if (dec_TipoCambioCierreExp != 0)
                                    {
                                        int month = dt_FchTipoCambioExp.Month;
                                        int year = dt_FchTipoCambioExp.Year;

                                        int numberOfDays = DateTime.DaysInMonth(year, month);

                                        dt_FchTipoCambioExp = new DateTime(year, month, numberOfDays);

                                        dec_TipoCambioExp = dec_TipoCambioCierreExp;
                                    }
                                    else
                                    {

                                        dec_TipoCambioExp = dec_TipoCambioOrig;
                                    }
                                    //try
                                    //{
                                    //    dec_MontoPrincipalExp = Convert.ToDecimal(dr_Res["MontoPrincipal"]);
                                    //}
                                    //catch (Exception e)
                                    //{
                                    //    dec_MontoPrincipalExp = 0;
                                    //}
                                    //try
                                    //{
                                    //    dec_MontoInteresesExp = Convert.ToDecimal(dr_Res["MontoIntereses"]);
                                    //}
                                    //catch (Exception e)
                                    //{
                                    //    dec_MontoInteresesExp = 0;
                                    //}
                                    //if (dec_MontoInteresesExp == 0)
                                    //{
                                    //    try
                                    //    {
                                    //        dec_MontoInteresesExp = Convert.ToDecimal(dr_Res["Intereses"]);
                                    //    }
                                    //    catch (Exception e)
                                    //    {
                                    //        dec_MontoInteresesExp = 0;
                                    //    }
                                    //}
                                    //try
                                    //{
                                    //    dec_MontoInteresesMoraExp = Convert.ToDecimal(dr_Res["InteresesMoratorios"]);
                                    //}
                                    //catch (Exception e)
                                    //{
                                    //    dec_MontoPrincipalExp = 0;
                                    //}
                                    //try
                                    //{
                                    //    dec_MontoCostasExp = Convert.ToDecimal(dr_Res["Costas"]);
                                    //}
                                    //catch (Exception e)
                                    //{
                                    //    dec_MontoCostasExp = 0;
                                    //}
                                    //try
                                    //{
                                    //    dec_MontoDanosExp = Convert.ToDecimal(dr_Res["DanoMoral"]);
                                    //}
                                    //catch (Exception e)
                                    //{
                                    //    dec_MontoDanosExp = 0;
                                    //}

                                    if (lstr_EstadoResolucion == "En Firme")
                                    {

                                        lint_IdCPResEnFirme = Convert.ToInt32(dr_Res["IdCobroPagoResolucion"]);
                                        try
                                        {
                                            dec_MontoPrincipalExp = Convert.ToDecimal(dr_Res["MontoPrincipal"]);
                                        }
                                        catch (Exception ex)
                                        {
                                            dec_MontoPrincipalExp = 0;
                                        }
                                    }
                                    else if (lstr_EstadoResolucion == "Liquidacion")
                                    {

                                        lint_IdCPResLiq = Convert.ToInt32(dr_Res["IdCobroPagoResolucion"]);

                                        try
                                        {
                                            dec_MontoInteresesExp = Convert.ToDecimal(dr_Res["MontoIntereses"]);
                                        }
                                        catch (Exception ex)
                                        {
                                            dec_MontoInteresesExp = 0;
                                        }
                                        if (dec_MontoInteresesExp == 0)
                                        {
                                            try
                                            {
                                                dec_MontoInteresesExp = Convert.ToDecimal(dr_Res["Intereses"]);
                                            }
                                            catch (Exception e)
                                            {
                                                dec_MontoInteresesExp = 0;
                                            }
                                        }
                                        try
                                        {
                                            dec_MontoInteresesMoraExp = Convert.ToDecimal(dr_Res["InteresesMoratorios"]);
                                        }
                                        catch (Exception ex)
                                        {
                                            dec_MontoInteresesMoraExp = 0;
                                        }
                                        try
                                        {
                                            dec_MontoCostasExp = Convert.ToDecimal(dr_Res["Costas"]);
                                        }
                                        catch (Exception ex)
                                        {
                                            dec_MontoCostasExp = 0;
                                        }
                                        try
                                        {
                                            dec_MontoDanosExp = Convert.ToDecimal(dr_Res["DanoMoral"]);
                                        }
                                        catch (Exception ex)
                                        {
                                            dec_MontoDanosExp = 0;
                                        }
                                    }

                                    ds_opciones = oc.ConsultarOpcionesCatalogo(null, "TiposProcesoExpediente", null, dr_Res["TipoProcesoExpediente"].ToString().Trim());
                                    if (ds_opciones.Tables.Count > 0 && ds_opciones.Tables["Table"].Rows.Count > 0)
                                    {
                                        str_IdTipoProcesoExp = ds_opciones.Tables["Table"].Rows[0]["ValOpcion"].ToString().Trim();
                                    }
                                }
                            }
                        }
                        #endregion Expediente
                        //lds_Pagos = lpago.ConsultarPagosFormulario(Convert.ToInt32(lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"]),
                        //                                              Convert.ToInt32(lds_Formularios.Tables["Table"].Rows[i]["Anno"])
                        //                                              );
                        lds_Pagos = lpago.ConsultarPagosFormulario(Convert.ToInt32(lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"]),
                                                                      Convert.ToInt32(lds_Formularios.Tables["Table"].Rows[i]["Anno"]),
                                                                      null,
                                                                      null,
                                                                      null,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      "A"
                                                                      );
                        #region Pagos
                        //solo itero si el dataset tiene registros
                        if (lds_Pagos.Tables.Count > 0 && lds_Pagos.Tables["Table"].Rows.Count > 0)
                        {
                            lbln_ErrorAsientoLinea = false;
                            /////////////////////////////////////
                            SigafTablaAsientoDif = new LogicaNegocio.wrSigafAsientos.ZfiAsiento[lds_Pagos.Tables["Table"].Rows.Count * 4];
                            SigafTablaAsientoPago = new LogicaNegocio.wrSigafAsientos.ZfiAsiento[lds_Pagos.Tables["Table"].Rows.Count * 6];
                            //SigafTablaAsientoPago = new wsAsientos.ZfiAsiento[lds_Pagos.Tables["Table"].Rows.Count * 6];
                            int lint_lineaDif = 0;
                            int lint_lineaPago = 0;
                            //int lint_lineaVerifica = 0;
                            Boolean lbln_Pago = false;
                            /////////////////////////////////////
                            //itero en todos los formularios pagados para sacar los servicios pagados e irlos contabilizando
                            int y = 0;
                            while (lds_Pagos.Tables["Table"].Rows.Count > y && !lbln_ErrorAsientoLinea)
                            {

                                lbln_PeriodoActual = true;
                                if (Convert.ToInt32(lds_Pagos.Tables["Table"].Rows[y]["Periodo"]) != gint_AnnoActual)
                                {
                                    lbln_PeriodoActual = false;
                                }
                                //Obtengo los valores del servicio del pago para completar los parametros del webservice
                                lds_Servicios = lservicio.ConsultarServicios(lds_Pagos.Tables["Table"].Rows[y]["IdServicio"].ToString(),
                                                                             lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString(),
                                                                             string.Empty,
                                                                             string.Empty,
                                                                             string.Empty,
                                                                             string.Empty
                                                                             );
                                #region Servicios
                                //solo itero si el dataset tiene registros
                                if (lds_Servicios.Tables.Count > 0 && lds_Servicios.Tables["Table"].Rows.Count > 0)
                                {
                                    //lds_Operaciones = loperacion.ConsultarOperaciones("",lstr_IdModulo,string.Empty);
                                    ////solo itero si el dataset tiene registros
                                    //if (lds_Operaciones.Tables.Count > 0) {
                                    #region TiposAsiento
                                    if (lbln_TieneExpediente)
                                    {
                                        #region AsientoContingentes
                                        if (!lbln_ExisteExpediente)
                                        {
                                            #region NoExisteExpediente
                                            lds_TiposAsiento = this.ConsultarTiposAsiento(lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString(),
                                                                                              lstr_IdModulo,
                                                                                              lds_Servicios.Tables["Table"].Rows[0]["IdServicio"].ToString(),
                                                                                              string.Empty,
                                                                                              string.Empty,
                                                                                              string.Empty,
                                                                                              lds_Formularios.Tables["Table"].Rows[i]["IdMoneda"].ToString(),
                                                                                              string.Empty,
                                                                                              "NO_EXISTE",
                                                                                              str_CodAuxiliar5);

                                            //solo itero si el dataset tiene registros
                                            if (lds_TiposAsiento.Tables.Count > 0 && lds_TiposAsiento.Tables["Table"].Rows.Count > 0)
                                            {

                                                //En este punto ya tengo los datos del formulario, el detalle del pago y el detalle del servicio, ya puedo consumir el webservice del asiento
                                                //SigafTablaAsientoPago = new LogicaNegocio.wrSigafAsientos.ZfiAsiento[lds_TiposAsiento.Tables["Table"].Rows.Count * 2];
                                                //int lint_lineaPago = 0;
                                                //itero en todos los formularios pagados para sacar los servicios pagados e irlos contabilizando
                                                for (int z = 0; lds_TiposAsiento.Tables["Table"].Rows.Count > z; z++)
                                                {

                                                    if (string.IsNullOrEmpty(lds_TiposAsiento.Tables["Table"].Rows[z]["Codigo"].ToString().Trim()))
                                                    {
                                                        lds_SociedadesGLSociedadesFi = lsociedadGL.ConsultarSociedadesGLSociedadesFi(lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString(), lstr_IdModulo, string.Empty);
                                                    }
                                                    else
                                                    {
                                                        lds_SociedadesGLSociedadesFi = lsociedadGL.ConsultarSociedadesGLSociedadesFi(lds_TiposAsiento.Tables["Table"].Rows[z]["Codigo"].ToString(), lstr_IdModulo, string.Empty);
                                                    }


                                                    //solo itero si el dataset tiene registros
                                                    if (lds_SociedadesGLSociedadesFi.Tables.Count > 0 && lds_SociedadesGLSociedadesFi.Tables["Table"].Rows.Count > 0)
                                                    {

                                                        lstr_IdSociedadFI = lds_SociedadesGLSociedadesFi.Tables["Table"].Rows[0]["IdSociedadFi"].ToString().Trim();
                                                    }//if de sociedad fi encontrado


                                                    //Verifico si la linea del asiento está Activa
                                                    if (lds_TiposAsiento.Tables["Table"].Rows[z]["Estado"].ToString().Trim() == "A")
                                                    {

                                                        SigafLinea = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
                                                        //SigafLinea = new wsAsientos.ZfiAsiento();
                                                        SigafLinea.Blart = "RI";// lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar4"].ToString().Trim(); //Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[i]["IdClaseDoc"]); //"SA"; //BLART = CLASE DE DOCUMENTO
                                                        SigafLinea.Bukrs = lstr_IdSociedadFI;//Convert.ToString(lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"]).Trim();//"G206";//BUKRS = ID de Sociedad
                                                        if (lint_lineaVerifica == 0)
                                                        {
                                                            SigafLinea.Bldat = ldt_FechaActual.ToString("dd.MM.yyyy");//Convert.ToString(ldt_FechaActual);//"01.10.2015";//bldat = Fecha de Documento
                                                            SigafLinea.Budat = ldt_FechaActual.ToString("dd.MM.yyyy");//Convert.ToString(ldt_FechaActual);//"01.10.2015";//budat = Fecha de Contabilizacion
                                                            lint_lineaVerifica = 1;
                                                        }
                                                        else
                                                        {
                                                            SigafLinea.Bldat = "";//Convert.ToString(ldt_FechaActual);//"01.10.2015";//bldat = Fecha de Documento
                                                            SigafLinea.Budat = "";//Convert.ToString(ldt_FechaActual);//"01.10.2015";//budat = Fecha de Contabilizacion
                                                        }
                                                        SigafLinea.Waers = lds_Pagos.Tables["Table"].Rows[y]["IdMoneda"].ToString().Trim();//"CRC";//waers = IdMoneda
                                                        SigafLinea.Xblnr = Convert.ToString(lds_Formularios.Tables["Table"].Rows[i]["Anno"]) + "." + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim();//"REF";//Xblnr = Referencia
                                                        SigafLinea.Bktxt = lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(); //"Texto_Cabecera";//Bktxt = "Texto_Cabecera"

                                                        //SigafLinea.Xref1Hd = ; //"REF_1";//Xref1Hd = "REF_1"
                                                        SigafLinea.Xref2Hd = "CI." + Convert.ToString(lds_Formularios.Tables["Table"].Rows[i]["Anno"]) + "." + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(); //"REF_2";//Xref2Hd = "REF_2"
                                                        SigafLinea.Bschl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdClaveContable"]).Trim(); //"40";//Bschl = Clave Contable
                                                        SigafLinea.Hkont = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCuentaContable"]).Trim(); //"5120302000";//Hkont = Cuenta Contable de Mayor
                                                        SigafLinea.Wrbtr = Convert.ToDecimal(lds_Pagos.Tables["Table"].Rows[y]["Monto"]); //1505;//Wrbtr = Monto Importe
                                                        SigafLinea.Zuonr = "";//Zuonr = "Asignacion"
                                                        SigafLinea.Sgtxt = "";//Sgtxt = "Texto"
                                                        if (lbln_Pago && SigafLinea.Bschl.Trim() == "50" && !string.IsNullOrEmpty(Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim()))
                                                        {
                                                            lds_ReservasDetallado = lreservasDet.ConsultarReservasDetallado(Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim(), "", "", "", "");

                                                            //solo itero si el dataset tiene registros
                                                            if (lds_ReservasDetallado.Tables.Count > 0 && lds_ReservasDetallado.Tables["Table"].Rows.Count > 0)
                                                            {

                                                                SigafLinea.Fipex = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdPosPre"]).Trim(); //"E-10302";//Fipex = posicion presupuestaria
                                                                SigafLinea.Kostl = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdCentroCosto"]).Trim(); //"20613200";//Kostl = Centro de Costo
                                                                SigafLinea.Fistl = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdCentroGestor"]).Trim(); //"20613200";//Fistl = Centro Gestor
                                                                SigafLinea.Geber = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdFondo"]).Trim(); //"001";//Geber = Fondo                                                        }//if de Reservas encontrado
                                                            }
                                                        }
                                                        else
                                                        {
                                                            SigafLinea.Fipex = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdPosPre"]).Trim(); //"E-10302";//Fipex = posicion presupuestaria
                                                            SigafLinea.Kostl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroCosto"]).Trim(); //"20613200";//Kostl = Centro de Costo
                                                            SigafLinea.Fistl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroGestor"]).Trim(); //"20613200";//Fistl = Centro Gestor
                                                            SigafLinea.Geber = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdFondo"]).Trim(); //"001";//Geber = Fondo
                                                        }
                                                        SigafLinea.Fkber = "";//Fkber = Area Funcional
                                                        SigafLinea.Xref2 = ""; //Xref2 = Flujo de efectivo

                                                        SigafLinea.Valut = "";//Valut = Fecha de Valor                                                  
                                                        SigafLinea.Umskz = "";//Umskz = Indicador CME
                                                        SigafLinea.Werks = "";//Werks = Centro
                                                        SigafLinea.Xmwst = ""; //Calculo de Impuesto
                                                        SigafLinea.Xref3 = "";//Flujo de Efectivo
                                                        SigafLinea.Zfbdt = "";//Fecha base
                                                        SigafLinea.Zlsch = "";//Via de pago
                                                        SigafLinea.Projk = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdElementoPEP"]).Trim(); ////Projk = Id Elemento PEP
                                                        if (string.IsNullOrEmpty(lstr_IdCeBe) || !SigafLinea.Hkont.StartsWith("4"))
                                                        {
                                                            SigafLinea.Prctr = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroBeneficio"]).Trim(); ////Prctr = centro de beneficio
                                                        }
                                                        else
                                                        {
                                                            SigafLinea.Prctr = lstr_IdCeBe;
                                                        }
                                                        SigafLinea.Mwskz = "";//Mwskz = indicador de impuesto
                                                        SigafLinea.Measure = "";//Measure = programa presupuestario
                                                        SigafLinea.Kursf = 0;//Kursf = tipo de cambio
                                                        SigafLinea.Aufnr = "";//Aufnr = Orden
                                                        SigafLinea.Dmbe2 = 0;//Dmbe2 = importe en moneda fuerte
                                                        //if (Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCuentaContable"]).StartsWith("5") )
                                                        //    SigafLinea.Kblnr = Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim(); //Kblnr = documento presupuestario
                                                        //else
                                                        SigafLinea.Kblnr = ""; //Kblnr = documento presupuestario
                                                        SigafLinea.Kblpos = "";//Kblpos = posicion documento presupuestario
                                                        SigafLinea.Rcomp = "";
                                                        SigafLinea.Buzei = "";
                                                        SigafLinea.Mandt = "";
                                                        SigafLinea.Hbkid = "";//Hbkid = banco propio


                                                        SigafLinea.Xref1Hd = "PAGADO"; //"REF_1";//Xref1Hd = "REF_1"
                                                        SigafTablaAsientoPago[lint_lineaPago] = SigafLinea;
                                                        lint_lineaPago++;


                                                        if (!string.IsNullOrEmpty(Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdClaveContable2"])))
                                                        {
                                                            ///cuenta 2
                                                            SigafLinea = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
                                                            //SigafLinea = new wsAsientos.ZfiAsiento();
                                                            SigafLinea.Blart = "RI";//lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar4"].ToString().Trim();  //"SA"; //BLART = CLASE DE DOCUMENTO
                                                            SigafLinea.Bukrs = "";//"G206";//BUKRS = ID de Sociedad
                                                            SigafLinea.Bldat = "";//"01.10.2015";//bldat = Fecha de Documento
                                                            SigafLinea.Budat = "";//"01.10.2015";//budat = Fecha de Contabilizacion
                                                            SigafLinea.Waers = "";//"CRC";//waers = IdMoneda
                                                            SigafLinea.Xblnr = "";//"REF";//Xblnr = Referencia
                                                            SigafLinea.Bktxt = ""; //"Texto_Cabecera";//Bktxt = "Texto_Cabecera"
                                                            SigafLinea.Xref1Hd = ""; //"REF_1";//Xref1Hd = "REF_1"
                                                            SigafLinea.Xref2Hd = ""; //"REF_2";//Xref2Hd = "REF_2"
                                                            SigafLinea.Bschl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdClaveContable2"]).Trim(); //"40";//Bschl = Clave Contable
                                                            SigafLinea.Hkont = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCuentaContable2"]).Trim(); //"5120302000";//Hkont = Cuenta Contable de Mayor
                                                            SigafLinea.Wrbtr = Convert.ToDecimal(lds_Pagos.Tables["Table"].Rows[y]["Monto"]); //1505;//Wrbtr = Monto Importe
                                                            SigafLinea.Zuonr = "";//Zuonr = "Asignacion"
                                                            SigafLinea.Sgtxt = "";//Sgtxt = "Texto"
                                                            if (lbln_Pago && SigafLinea.Bschl.Trim() == "50" && !string.IsNullOrEmpty(Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim()))
                                                            {
                                                                lds_ReservasDetallado = lreservasDet.ConsultarReservasDetallado(Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim(), "", "", "", "");

                                                                //solo itero si el dataset tiene registros
                                                                if (lds_ReservasDetallado.Tables.Count > 0 && lds_ReservasDetallado.Tables["Table"].Rows.Count > 0)
                                                                {

                                                                    SigafLinea.Fipex = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdPosPre"]).Trim(); //"E-10302";//Fipex = posicion presupuestaria
                                                                    SigafLinea.Kostl = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdCentroCosto"]).Trim(); //"20613200";//Kostl = Centro de Costo
                                                                    SigafLinea.Fistl = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdCentroGestor"]).Trim(); //"20613200";//Fistl = Centro Gestor
                                                                    SigafLinea.Geber = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdFondo"]).Trim(); //"001";//Geber = Fondo                                                        }//if de Reservas encontrado
                                                                }
                                                            }
                                                            else
                                                            {
                                                                SigafLinea.Fipex = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdPosPre2"]).Trim(); //"E-10302";//Fipex = posicion presupuestaria
                                                                SigafLinea.Kostl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroCosto2"]).Trim(); //"20613200";//Kostl = Centro de Costo
                                                                SigafLinea.Fistl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroGestor2"]).Trim(); //"20613200";//Fistl = Centro Gestor
                                                                SigafLinea.Geber = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdFondo2"]).Trim(); //"001";//Geber = Fondo
                                                            }
                                                            SigafLinea.Fkber = "";//Fkber = Area Funcional
                                                            SigafLinea.Xref2 = ""; //Xref2 = Flujo de efectivo

                                                            SigafLinea.Valut = "";//Valut = Fecha de Valor                                                  
                                                            SigafLinea.Umskz = "";//Umskz = Indicador CME
                                                            SigafLinea.Werks = "";//Werks = Centro
                                                            SigafLinea.Xmwst = ""; //Calculo de Impuesto
                                                            SigafLinea.Xref3 = "";//Flujo de Efectivo
                                                            SigafLinea.Zfbdt = "";//Fecha base
                                                            SigafLinea.Zlsch = "";//Via de pago
                                                            SigafLinea.Projk = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdElementoPEP2"]).Trim(); ////Projk = Id Elemento PEP
                                                            if (string.IsNullOrEmpty(lstr_IdCeBe) || !SigafLinea.Hkont.StartsWith("4"))
                                                            {
                                                                SigafLinea.Prctr = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroBeneficio2"]).Trim(); ////Prctr = centro de beneficio
                                                            }
                                                            else
                                                            {
                                                                SigafLinea.Prctr = lstr_IdCeBe;
                                                            }
                                                            SigafLinea.Mwskz = "";//Mwskz = indicador de impuesto
                                                            SigafLinea.Measure = "";//Measure = programa presupuestario
                                                            SigafLinea.Kursf = 0;//Kursf = tipo de cambio
                                                            SigafLinea.Aufnr = "";//Aufnr = Orden
                                                            SigafLinea.Dmbe2 = 0;//Dmbe2 = importe en moneda fuerte
                                                            //if (Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCuentaContable2"]).StartsWith("5"))
                                                            //    SigafLinea.Kblnr = Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim();//Kblnr = documento presupuestario
                                                            //else
                                                            SigafLinea.Kblnr = "";//Kblnr = documento presupuestario
                                                            SigafLinea.Kblpos = "";//Kblpos = posicion documento presupuestario
                                                            SigafLinea.Rcomp = "";
                                                            SigafLinea.Buzei = "";
                                                            SigafLinea.Mandt = "";
                                                            SigafLinea.Hbkid = "";//Hbkid = banco propio



                                                            SigafTablaAsientoPago[lint_lineaPago] = SigafLinea;
                                                            lint_lineaPago++;

                                                        }//if (!string.IsNullOrEmpty(Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdClaveContable2"])))
                                                        //SigafAsientoLogPago = lasiento.EnviarAsientoSigaf(SigafTablaAsientoPago);

                                                    }//if de estado Activo de la linea del asiento
                                                }//for de las lineas del asiento
                                            }//if asiento encontrado
                                            else
                                            {
                                                //Tipo de Asiento no Encontrado
                                                lstr_Mensaje = "Tipo de Asiento no Encontrado" + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim() + "." + lds_Servicios.Tables["Table"].Rows[0]["IdServicio"].ToString().Trim();

                                                resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora("CI", lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Envio Asientos", lstr_Mensaje, lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Asiento CT", lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim());
                                        
                                                lbln_ErrorAsientoLinea = true;
                                            }//Tipo de Asiento no encontrado
                                            #endregion NoExisteExpediente
                                        }//if existe expediente.
                                        else
                                        {
                                            str_CodAuxiliar = string.Empty; //TIPO DE CAMBIO NO SE USA SI EL EXP. ES EN CRC
                                            dec_Pago = Convert.ToDecimal(lds_Pagos.Tables["Table"].Rows[y]["Monto"]);
                                            switch (lds_Formularios.Tables["Table"].Rows[i]["IdMoneda"].ToString().Trim())
                                            {
                                                case "CRC":
                                                    dec_PagoCRC = Convert.ToDecimal(lds_Pagos.Tables["Table"].Rows[y]["Monto"]);
                                                    dec_PagoUSD = dec_PagoCRC / ldec_tipo_cambioUSD;
                                                    if (ldec_tipo_cambioEUR != 0)//MONTO está en USD y se debe pasar a la otra moneda
                                                    {
                                                        dec_PagoEUR = dec_PagoUSD / ldec_tipo_cambioEUR;
                                                    }

                                                    break;
                                                case "USD":
                                                    dec_PagoUSD = Convert.ToDecimal(lds_Pagos.Tables["Table"].Rows[y]["Monto"]);
                                                    dec_PagoCRC = dec_PagoUSD * ldec_tipo_cambioUSD;
                                                    if (ldec_tipo_cambioEUR != 0)//MONTO está en USD y se debe pasar a la otra moneda
                                                    {
                                                        dec_PagoEUR = dec_PagoUSD / ldec_tipo_cambioEUR;
                                                    }

                                                    break;
                                                case "EUR":
                                                    dec_PagoEUR = Convert.ToDecimal(lds_Pagos.Tables["Table"].Rows[y]["Monto"]);
                                                    dec_PagoUSD = dec_PagoEUR * ldec_tipo_cambioEUR;
                                                    dec_PagoCRC = dec_PagoUSD * ldec_tipo_cambioUSD;

                                                    break;
                                                default:
                                                    break;
                                            }
                                            switch (str_MonedaExp.Trim())
                                            {
                                                case "CRC":

                                                    switch (lds_Servicios.Tables["Table"].Rows[0]["IdServicio"].ToString().Trim())
                                                    {
                                                        case "344": //principal
                                                            dec_DiferenciaPagoCRC = dec_PagoCRC - dec_MontoPrincipalExp;
                                                            if (dec_PagoCRC > dec_MontoPrincipalExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";

                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "345"://intereses
                                                            dec_DiferenciaPagoCRC = dec_PagoCRC - dec_MontoInteresesExp;
                                                            if (dec_PagoCRC > dec_MontoInteresesExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "346"://intereses moratorios
                                                            dec_DiferenciaPagoCRC = dec_PagoCRC - dec_MontoInteresesMoraExp;
                                                            if (dec_PagoCRC > dec_MontoInteresesMoraExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "347"://costas
                                                            dec_DiferenciaPagoCRC = dec_PagoCRC - dec_MontoCostasExp;
                                                            if (dec_PagoCRC > dec_MontoCostasExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "348"://danos & perjuicios
                                                            dec_DiferenciaPagoCRC = dec_PagoCRC - dec_MontoDanosExp;
                                                            if (dec_PagoCRC > dec_MontoDanosExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        default:
                                                            break;
                                                    }
                                                    break;
                                                case "USD":
                                                    dec_DiferenciaTC = ldec_tipo_cambioUSD - dec_TipoCambioExp;
                                                    if (ldec_tipo_cambioUSD > dec_TipoCambioExp)
                                                    {
                                                        str_CodAuxiliar = "TC_MAYOR";
                                                    }
                                                    else
                                                    {
                                                        str_CodAuxiliar = "TC_MENOR";
                                                    }
                                                    switch (lds_Servicios.Tables["Table"].Rows[0]["IdServicio"].ToString().Trim())
                                                    {
                                                        case "344": //principal
                                                            dec_DiferenciaPagoUSD = dec_PagoUSD - dec_MontoPrincipalExp;
                                                            if (dec_PagoUSD > dec_MontoPrincipalExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "345"://intereses
                                                            dec_DiferenciaPagoUSD = dec_PagoUSD - dec_MontoInteresesExp;
                                                            if (dec_PagoUSD > dec_MontoInteresesExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "346"://intereses moratorios
                                                            dec_DiferenciaPagoUSD = dec_PagoUSD - dec_MontoInteresesMoraExp;
                                                            if (dec_PagoUSD > dec_MontoInteresesMoraExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "347"://costas
                                                            dec_DiferenciaPagoUSD = dec_PagoUSD - dec_MontoCostasExp;
                                                            if (dec_PagoUSD > dec_MontoCostasExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "348"://danos & perjuicios
                                                            dec_DiferenciaPagoUSD = dec_PagoUSD - dec_MontoDanosExp;
                                                            if (dec_PagoUSD > dec_MontoDanosExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        default:
                                                            break;
                                                    }
                                                    dec_DiferenciaPagoCRC = dec_DiferenciaPagoUSD * ldec_tipo_cambioUSD;
                                                    if (ldec_tipo_cambioEUR != 0)//MONTO está en USD y se debe pasar a la otra moneda
                                                    {
                                                        dec_DiferenciaPagoEUR = dec_DiferenciaPagoUSD / ldec_tipo_cambioEUR;
                                                    }
                                                    break;
                                                case "EUR":
                                                    dec_DiferenciaTC = Math.Round((ldec_tipo_cambioEUR * ldec_tipo_cambioUSD), 2) - dec_TipoCambioExp;
                                                    if (Math.Round((ldec_tipo_cambioEUR * ldec_tipo_cambioUSD), 2) > dec_TipoCambioExp)
                                                    {
                                                        str_CodAuxiliar = "TC_MAYOR";
                                                    }
                                                    else
                                                    {
                                                        str_CodAuxiliar = "TC_MENOR";
                                                    }
                                                    switch (lds_Servicios.Tables["Table"].Rows[0]["IdServicio"].ToString().Trim())
                                                    {
                                                        case "344": //principal
                                                            dec_DiferenciaPagoEUR = dec_PagoEUR - dec_MontoPrincipalExp;
                                                            if (dec_PagoEUR > dec_MontoPrincipalExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "345"://intereses
                                                            dec_DiferenciaPagoEUR = dec_PagoEUR - dec_MontoInteresesExp;
                                                            if (dec_PagoEUR > dec_MontoInteresesExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "346"://intereses moratorios
                                                            dec_DiferenciaPagoEUR = dec_PagoEUR - dec_MontoInteresesMoraExp;
                                                            if (dec_PagoEUR > dec_MontoInteresesMoraExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "347"://costas
                                                            dec_DiferenciaPagoEUR = dec_PagoEUR - dec_MontoCostasExp;
                                                            if (dec_PagoEUR > dec_MontoCostasExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "348"://danos & perjuicios
                                                            dec_DiferenciaPagoEUR = dec_PagoEUR - dec_MontoDanosExp;
                                                            if (dec_PagoEUR > dec_MontoDanosExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        default:
                                                            break;
                                                    }
                                                    dec_DiferenciaPagoUSD = dec_DiferenciaPagoEUR * ldec_tipo_cambioEUR;
                                                    dec_DiferenciaPagoCRC = dec_DiferenciaPagoUSD * ldec_tipo_cambioUSD;
                                                    break;
                                                default:
                                                    break;
                                            }
                                            switch (lds_Formularios.Tables["Table"].Rows[i]["IdMoneda"].ToString().Trim())
                                            {
                                                case "CRC":
                                                    dec_DiferenciaPago = dec_DiferenciaPagoCRC;

                                                    break;
                                                case "USD":
                                                    dec_DiferenciaPago = dec_DiferenciaPagoUSD;

                                                    break;
                                                case "EUR":
                                                    dec_DiferenciaPago = dec_DiferenciaPagoEUR;

                                                    break;
                                                default:
                                                    break;
                                            }

                                            #region SiExisteExpediente
                                            lds_TiposAsiento = this.ConsultarTiposAsiento(lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString(),
                                                                                              lstr_IdModulo,
                                                                                              lds_Servicios.Tables["Table"].Rows[0]["IdServicio"].ToString(),
                                                                                              string.Empty,
                                                                                              string.Empty,
                                                                                              str_CodAuxiliar,
                                                                                              lds_Formularios.Tables["Table"].Rows[i]["IdMoneda"].ToString(),
                                                                                              str_IdTipoProcesoExp,
                                                                                              str_CodAuxiliar4,
                                                                                              str_CodAuxiliar5);

                                            //solo itero si el dataset tiene registros
                                            if (lds_TiposAsiento.Tables.Count > 0 && lds_TiposAsiento.Tables["Table"].Rows.Count > 0)
                                            {

                                                //En este punto ya tengo los datos del formulario, el detalle del pago y el detalle del servicio, ya puedo consumir el webservice del asiento
                                                //SigafTablaAsientoPago = new LogicaNegocio.wrSigafAsientos.ZfiAsiento[lds_TiposAsiento.Tables["Table"].Rows.Count * 2];
                                                //int lint_lineaPago = 0;
                                                //itero en todos los formularios pagados para sacar los servicios pagados e irlos contabilizando
                                                for (int z = 0; lds_TiposAsiento.Tables["Table"].Rows.Count > z; z++)
                                                {

                                                    if (string.IsNullOrEmpty(lds_TiposAsiento.Tables["Table"].Rows[z]["Codigo"].ToString().Trim()))
                                                    {
                                                    lds_SociedadesGLSociedadesFi = lsociedadGL.ConsultarSociedadesGLSociedadesFi(lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString(), lstr_IdModulo, string.Empty);
                                                    }
                                                    else
                                                    {
                                                        lds_SociedadesGLSociedadesFi = lsociedadGL.ConsultarSociedadesGLSociedadesFi(lds_TiposAsiento.Tables["Table"].Rows[z]["Codigo"].ToString(), lstr_IdModulo, string.Empty);
                                                    }


                                                    //solo itero si el dataset tiene registros
                                                    if (lds_SociedadesGLSociedadesFi.Tables.Count > 0 && lds_SociedadesGLSociedadesFi.Tables["Table"].Rows.Count > 0)
                                                    {

                                                        lstr_IdSociedadFI = lds_SociedadesGLSociedadesFi.Tables["Table"].Rows[0]["IdSociedadFi"].ToString().Trim();
                                                    }//if de sociedad fi encontrado


                                                    //Verifico si la linea del asiento está Activa
                                                    if (lds_TiposAsiento.Tables["Table"].Rows[z]["Estado"].ToString().Trim() == "A")
                                                    {

                                                        SigafLinea = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
                                                        //SigafLinea = new wsAsientos.ZfiAsiento();
                                                        SigafLinea.Blart = "RI";// lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar4"].ToString().Trim(); //Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[i]["IdClaseDoc"]); //"SA"; //BLART = CLASE DE DOCUMENTO
                                                        SigafLinea.Bukrs = lstr_IdSociedadFI;//Convert.ToString(lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"]).Trim();//"G206";//BUKRS = ID de Sociedad
                                                        if (lint_lineaPago == 0
                                                        || ((lint_lineaDif == 0 &&
                                                              ((lds_Pagos.Tables["Table"].Rows[y]["IdMoneda"].ToString().Trim() != "CRC") &&(
                                                               ((lds_TiposAsiento.Tables["Table"].Rows[z]["Secuencia"].ToString().Trim()) == "3") // && dec_DiferenciaPago > 0)
                                                            //|| ((lds_TiposAsiento.Tables["Table"].Rows[z]["Secuencia"].ToString().Trim()) == "2" && dec_DiferenciaPago > 0 && dec_DiferenciaTC > 0)
                                                              ))
                                                              )
                                                           )
                                                           )
                                                        {
                                                            SigafLinea.Bldat = ldt_FechaActual.ToString("dd.MM.yyyy");//Convert.ToString(ldt_FechaActual);//"01.10.2015";//bldat = Fecha de Documento
                                                            SigafLinea.Budat = ldt_FechaActual.ToString("dd.MM.yyyy");//Convert.ToString(ldt_FechaActual);//"01.10.2015";//budat = Fecha de Contabilizacion
                                                            lint_lineaVerifica = 1;
                                                        }
                                                        else
                                                        {
                                                            SigafLinea.Bldat = "";//Convert.ToString(ldt_FechaActual);//"01.10.2015";//bldat = Fecha de Documento
                                                            SigafLinea.Budat = "";//Convert.ToString(ldt_FechaActual);//"01.10.2015";//budat = Fecha de Contabilizacion
                                                        }
                                                        SigafLinea.Waers = lds_Pagos.Tables["Table"].Rows[y]["IdMoneda"].ToString().Trim();//"CRC";//waers = IdMoneda
                                                        if (SigafLinea.Waers.Trim() != "CRC")
                                                            SigafLinea.Kursf = (SigafLinea.Waers.Trim() == "USD") ? Math.Round(ldec_tipo_cambioUSD, 5) : Math.Round(ldec_tipo_cambioEUR * ldec_tipo_cambioUSD, 5);//tipo de cambio 
                                                        SigafLinea.Xblnr = Convert.ToString(lds_Formularios.Tables["Table"].Rows[i]["Anno"]) + "." + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim();//"REF";//Xblnr = Referencia
                                                        SigafLinea.Bktxt = lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(); //"Texto_Cabecera";//Bktxt = "Texto_Cabecera"

                                                        //SigafLinea.Xref1Hd = ; //"REF_1";//Xref1Hd = "REF_1"
                                                        SigafLinea.Xref2Hd = "CI." + Convert.ToString(lds_Formularios.Tables["Table"].Rows[i]["Anno"]) + "." + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(); //"REF_2";//Xref2Hd = "REF_2"
                                                        SigafLinea.Bschl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdClaveContable"]).Trim(); //"40";//Bschl = Clave Contable
                                                        SigafLinea.Hkont = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCuentaContable"]).Trim(); //"5120302000";//Hkont = Cuenta Contable de Mayor
                                                        switch ((lds_TiposAsiento.Tables["Table"].Rows[z]["Secuencia"].ToString().Trim()))
                                                        {
                                                            case "1":
                                                                SigafLinea.Wrbtr = dec_Pago;//Convert.ToDecimal(lds_Pagos.Tables["Table"].Rows[y]["Monto"]); //1505;//Wrbtr = Monto Importe
                                                                if (dec_DiferenciaPago > 0)
                                                                    SigafLinea.Wrbtr -= dec_DiferenciaPago;
                                                                //if (dec_DiferenciaTC < 0 && SigafLinea.Waers.Trim() != "CRC")
                                                                //    SigafLinea.Kursf = (SigafLinea.Waers.Trim() == "USD") ? Math.Round(ldec_tipo_cambioUSD, 5) : Math.Round(ldec_tipo_cambioEUR * ldec_tipo_cambioUSD, 5);//tipo de cambio                                                
                                                                break;
                                                            case "2":
                                                                SigafLinea.Wrbtr = Math.Abs(dec_DiferenciaPago);
                                                                //if (SigafLinea.Waers.Trim() != "CRC")
                                                                //    SigafLinea.Kursf = (SigafLinea.Waers.Trim() == "USD") ? Math.Round(ldec_tipo_cambioUSD, 5) : Math.Round(ldec_tipo_cambioEUR * ldec_tipo_cambioUSD, 5);//tipo de cambio 
                                                                break;
                                                            case "3":
                                                                SigafLinea.Waers = "CRC";
                                                                //if (dec_DiferenciaTC < 0)
                                                                //{
                                                                    if (dec_DiferenciaPago > 0)
                                                                        SigafLinea.Wrbtr = (dec_Pago - dec_DiferenciaPago) * Math.Abs(dec_DiferenciaTC);
                                                                    else
                                                                        SigafLinea.Wrbtr = (dec_Pago) * Math.Abs(dec_DiferenciaTC);
                                                                    //SigafLinea.Kursf = 0;
                                                                    SigafLinea.Kursf = 0;//tipo de cambio 
                                                                /*}
                                                                else
                                                                {
                                                                    if (dec_DiferenciaPago > 0)
                                                                        SigafLinea.Wrbtr = (dec_Pago - dec_DiferenciaPago) * Math.Abs(dec_DiferenciaTC) / ldec_tipo_cambioUSD;
                                                                    else
                                                                        SigafLinea.Wrbtr = (dec_Pago) * Math.Abs(dec_DiferenciaTC) / ldec_tipo_cambioUSD;
                                                                    //SigafLinea.Kursf = 0;
                                                                    SigafLinea.Kursf = (SigafLinea.Waers.Trim() == "USD") ? Math.Round(ldec_tipo_cambioUSD, 5) : Math.Round(ldec_tipo_cambioEUR * ldec_tipo_cambioUSD, 5);//tipo de cambio 
                                                                }*/


                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                        SigafLinea.Wrbtr = Math.Round(SigafLinea.Wrbtr, 2);

                                                        SigafLinea.Zuonr = "";//Zuonr = "Asignacion"
                                                        SigafLinea.Sgtxt = "";//Sgtxt = "Texto"
                                                        if (lbln_Pago && SigafLinea.Bschl.Trim() == "50" && !string.IsNullOrEmpty(Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim()))
                                                        {
                                                            lds_ReservasDetallado = lreservasDet.ConsultarReservasDetallado(Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim(), "", "", "", "");

                                                            //solo itero si el dataset tiene registros
                                                            if (lds_ReservasDetallado.Tables.Count > 0 && lds_ReservasDetallado.Tables["Table"].Rows.Count > 0)
                                                            {

                                                                SigafLinea.Fipex = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdPosPre"]).Trim(); //"E-10302";//Fipex = posicion presupuestaria
                                                                SigafLinea.Kostl = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdCentroCosto"]).Trim(); //"20613200";//Kostl = Centro de Costo
                                                                SigafLinea.Fistl = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdCentroGestor"]).Trim(); //"20613200";//Fistl = Centro Gestor
                                                                SigafLinea.Geber = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdFondo"]).Trim(); //"001";//Geber = Fondo                                                        }//if de Reservas encontrado
                                                            }
                                                        }
                                                        else
                                                        {
                                                            SigafLinea.Fipex = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdPosPre"]).Trim(); //"E-10302";//Fipex = posicion presupuestaria
                                                            SigafLinea.Kostl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroCosto"]).Trim(); //"20613200";//Kostl = Centro de Costo
                                                            SigafLinea.Fistl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroGestor"]).Trim(); //"20613200";//Fistl = Centro Gestor
                                                            SigafLinea.Geber = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdFondo"]).Trim(); //"001";//Geber = Fondo
                                                        }
                                                        SigafLinea.Fkber = "";//Fkber = Area Funcional
                                                        SigafLinea.Xref2 = ""; //Xref2 = Flujo de efectivo

                                                        SigafLinea.Valut = "";//Valut = Fecha de Valor                                                  
                                                        SigafLinea.Umskz = "";//Umskz = Indicador CME
                                                        SigafLinea.Werks = "";//Werks = Centro
                                                        SigafLinea.Xmwst = ""; //Calculo de Impuesto
                                                        SigafLinea.Xref3 = "";//Flujo de Efectivo
                                                        SigafLinea.Zfbdt = "";//Fecha base
                                                        SigafLinea.Zlsch = "";//Via de pago
                                                        SigafLinea.Projk = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdElementoPEP"]).Trim(); ////Projk = Id Elemento PEP
                                                        if (string.IsNullOrEmpty(lstr_IdCeBe) || !SigafLinea.Hkont.StartsWith("4"))
                                                        {
                                                            SigafLinea.Prctr = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroBeneficio"]).Trim(); ////Prctr = centro de beneficio
                                                        }
                                                        else
                                                        {
                                                            SigafLinea.Prctr = lstr_IdCeBe;
                                                        }
                                                        SigafLinea.Mwskz = "";//Mwskz = indicador de impuesto
                                                        SigafLinea.Measure = "";//Measure = programa presupuestario
                                                        //SigafLinea.Kursf = 0;//Kursf = tipo de cambio
                                                        SigafLinea.Aufnr = "";//Aufnr = Orden
                                                        SigafLinea.Dmbe2 = 0;//Dmbe2 = importe en moneda fuerte
                                                        //if (Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCuentaContable"]).StartsWith("5") )
                                                        //    SigafLinea.Kblnr = Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim(); //Kblnr = documento presupuestario
                                                        //else
                                                        SigafLinea.Kblnr = ""; //Kblnr = documento presupuestario
                                                        SigafLinea.Kblpos = "";//Kblpos = posicion documento presupuestario
                                                        SigafLinea.Rcomp = "";
                                                        SigafLinea.Buzei = "";
                                                        SigafLinea.Mandt = "";
                                                        SigafLinea.Hbkid = "";//Hbkid = banco propio
                                                        if ((lds_Pagos.Tables["Table"].Rows[y]["IdMoneda"].ToString().Trim() != "CRC") &&(
                                                               ((lds_TiposAsiento.Tables["Table"].Rows[z]["Secuencia"].ToString().Trim()) == "3") // && dec_DiferenciaPago > 0)
                                                            //|| ((lds_TiposAsiento.Tables["Table"].Rows[z]["Secuencia"].ToString().Trim()) == "2" && dec_DiferenciaPago > 0 && dec_DiferenciaTC > 0)
                                                              ))
                                                        {
                                                            SigafLinea.Xref1Hd = "DIFERENCIA"; //"REF_1";//Xref1Hd = "REF_1"

                                                            SigafTablaAsientoDif[lint_lineaDif] = SigafLinea;
                                                            lint_lineaDif++;
                                                        }
                                                        //if ((lds_TiposAsiento.Tables["Table"].Rows[z]["Secuencia"].ToString().Trim()) != "3" || (lds_Pagos.Tables["Table"].Rows[y]["IdMoneda"].ToString().Trim() == "CRC"))
                                                        else
                                                        {
                                                            SigafLinea.Xref1Hd = "PAGADO"; //"REF_1";//Xref1Hd = "REF_1"

                                                            SigafTablaAsientoPago[lint_lineaPago] = SigafLinea;
                                                            lint_lineaPago++;
                                                        }
                                                        //else
                                                        //{
                                                        //    SigafLinea.Xref1Hd = "DIFERENCIA"; //"REF_1";//Xref1Hd = "REF_1"

                                                        //    SigafTablaAsientoDif[lint_lineaDif] = SigafLinea;
                                                        //    lint_lineaDif++;
                                                        //    //lint_lineaPago++;
                                                        //}

                                                        if (!string.IsNullOrEmpty(Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdClaveContable2"])))
                                                        {
                                                            ///cuenta 2
                                                            SigafLinea = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
                                                            //SigafLinea = new wsAsientos.ZfiAsiento();
                                                            SigafLinea.Blart = "RI";//lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar4"].ToString().Trim();  //"SA"; //BLART = CLASE DE DOCUMENTO
                                                            SigafLinea.Bukrs = "";//"G206";//BUKRS = ID de Sociedad
                                                            SigafLinea.Bldat = "";//"01.10.2015";//bldat = Fecha de Documento
                                                            SigafLinea.Budat = "";//"01.10.2015";//budat = Fecha de Contabilizacion
                                                            SigafLinea.Waers = lds_Pagos.Tables["Table"].Rows[y]["IdMoneda"].ToString().Trim();//"CRC";//waers = IdMoneda
                                                            if (SigafLinea.Waers.Trim() != "CRC")
                                                                SigafLinea.Kursf = (SigafLinea.Waers.Trim() == "USD") ? Math.Round(ldec_tipo_cambioUSD, 5) : Math.Round(ldec_tipo_cambioEUR * ldec_tipo_cambioUSD, 5);//tipo de cambio 

                                                            SigafLinea.Xblnr = "";//"REF";//Xblnr = Referencia
                                                            SigafLinea.Bktxt = ""; //"Texto_Cabecera";//Bktxt = "Texto_Cabecera"
                                                            SigafLinea.Xref1Hd = ""; //"REF_1";//Xref1Hd = "REF_1"
                                                            SigafLinea.Xref2Hd = ""; //"REF_2";//Xref2Hd = "REF_2"
                                                            SigafLinea.Bschl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdClaveContable2"]).Trim(); //"40";//Bschl = Clave Contable
                                                            SigafLinea.Hkont = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCuentaContable2"]).Trim(); //"5120302000";//Hkont = Cuenta Contable de Mayor
                                                            switch ((lds_TiposAsiento.Tables["Table"].Rows[z]["Secuencia"].ToString().Trim()))
                                                            {
                                                                case "1":
                                                                    SigafLinea.Wrbtr = dec_Pago;//Convert.ToDecimal(lds_Pagos.Tables["Table"].Rows[y]["Monto"]); //1505;//Wrbtr = Monto Importe
                                                                    if (dec_DiferenciaPago > 0)
                                                                        SigafLinea.Wrbtr -= dec_DiferenciaPago;
                                                                    //if (dec_DiferenciaTC < 0 && SigafLinea.Waers.Trim() != "CRC")
                                                                    //    SigafLinea.Kursf = (SigafLinea.Waers.Trim() == "USD") ? Math.Round(ldec_tipo_cambioUSD, 5) : Math.Round(ldec_tipo_cambioEUR * ldec_tipo_cambioUSD, 5);//tipo de cambio                                                
                                                                    break;
                                                                case "2":
                                                                    SigafLinea.Wrbtr = Math.Abs(dec_DiferenciaPago);
                                                                    //if (SigafLinea.Waers.Trim() != "CRC")
                                                                    //    SigafLinea.Kursf = (SigafLinea.Waers.Trim() == "USD") ? Math.Round(ldec_tipo_cambioUSD, 5)  : Math.Round(ldec_tipo_cambioEUR * ldec_tipo_cambioUSD, 5);//tipo de cambio 
                                                                    break;
                                                                case "3":
                                                                    SigafLinea.Waers = "CRC";
                                                                    //if (dec_DiferenciaTC < 0)
                                                                    //{
                                                                        if (dec_DiferenciaPago > 0)
                                                                            SigafLinea.Wrbtr = (dec_Pago - dec_DiferenciaPago) * Math.Abs(dec_DiferenciaTC);
                                                                        else
                                                                            SigafLinea.Wrbtr = (dec_Pago) * Math.Abs(dec_DiferenciaTC);
                                                                        //SigafLinea.Kursf = 0;
                                                                        SigafLinea.Kursf = 0;//tipo de cambio 
                                                                    /*}
                                                                    else
                                                                    {
                                                                        if (dec_DiferenciaPago > 0)
                                                                            SigafLinea.Wrbtr = (dec_Pago - dec_DiferenciaPago) * Math.Abs(dec_DiferenciaTC) / ldec_tipo_cambioUSD;
                                                                        else
                                                                            SigafLinea.Wrbtr = (dec_Pago) * Math.Abs(dec_DiferenciaTC) / ldec_tipo_cambioUSD;
                                                                        //SigafLinea.Kursf = 0;
                                                                        SigafLinea.Kursf = (SigafLinea.Waers.Trim() == "USD") ? Math.Round(ldec_tipo_cambioUSD, 5) : Math.Round(ldec_tipo_cambioEUR * ldec_tipo_cambioUSD, 5);//tipo de cambio 
                                                                    }*/
                                                                    break;
                                                                default:
                                                                    break;
                                                            }

                                                            SigafLinea.Wrbtr = Math.Round(SigafLinea.Wrbtr, 2);
                                                            SigafLinea.Zuonr = "";//Zuonr = "Asignacion"
                                                            SigafLinea.Sgtxt = "";//Sgtxt = "Texto"
                                                            if (lbln_Pago && SigafLinea.Bschl.Trim() == "50" && !string.IsNullOrEmpty(Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim()))
                                                            {
                                                                lds_ReservasDetallado = lreservasDet.ConsultarReservasDetallado(Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim(), "", "", "", "");

                                                                //solo itero si el dataset tiene registros
                                                                if (lds_ReservasDetallado.Tables.Count > 0 && lds_ReservasDetallado.Tables["Table"].Rows.Count > 0)
                                                                {

                                                                    SigafLinea.Fipex = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdPosPre"]).Trim(); //"E-10302";//Fipex = posicion presupuestaria
                                                                    SigafLinea.Kostl = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdCentroCosto"]).Trim(); //"20613200";//Kostl = Centro de Costo
                                                                    SigafLinea.Fistl = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdCentroGestor"]).Trim(); //"20613200";//Fistl = Centro Gestor
                                                                    SigafLinea.Geber = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdFondo"]).Trim(); //"001";//Geber = Fondo                                                        }//if de Reservas encontrado
                                                                }
                                                            }
                                                            else
                                                            {
                                                                SigafLinea.Fipex = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdPosPre2"]).Trim(); //"E-10302";//Fipex = posicion presupuestaria
                                                                SigafLinea.Kostl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroCosto2"]).Trim(); //"20613200";//Kostl = Centro de Costo
                                                                SigafLinea.Fistl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroGestor2"]).Trim(); //"20613200";//Fistl = Centro Gestor
                                                                SigafLinea.Geber = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdFondo2"]).Trim(); //"001";//Geber = Fondo
                                                            }
                                                            SigafLinea.Fkber = "";//Fkber = Area Funcional
                                                            SigafLinea.Xref2 = ""; //Xref2 = Flujo de efectivo

                                                            SigafLinea.Valut = "";//Valut = Fecha de Valor                                                  
                                                            SigafLinea.Umskz = "";//Umskz = Indicador CME
                                                            SigafLinea.Werks = "";//Werks = Centro
                                                            SigafLinea.Xmwst = ""; //Calculo de Impuesto
                                                            SigafLinea.Xref3 = "";//Flujo de Efectivo
                                                            SigafLinea.Zfbdt = "";//Fecha base
                                                            SigafLinea.Zlsch = "";//Via de pago
                                                            SigafLinea.Projk = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdElementoPEP2"]).Trim(); ////Projk = Id Elemento PEP
                                                            if (string.IsNullOrEmpty(lstr_IdCeBe) || !SigafLinea.Hkont.StartsWith("4"))
                                                            {
                                                                SigafLinea.Prctr = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroBeneficio2"]).Trim(); ////Prctr = centro de beneficio
                                                            }
                                                            else
                                                            {
                                                                SigafLinea.Prctr = lstr_IdCeBe;
                                                            }
                                                            SigafLinea.Mwskz = "";//Mwskz = indicador de impuesto
                                                            SigafLinea.Measure = "";//Measure = programa presupuestario
                                                            //SigafLinea.Kursf = 0;//Kursf = tipo de cambio
                                                            SigafLinea.Aufnr = "";//Aufnr = Orden
                                                            SigafLinea.Dmbe2 = 0;//Dmbe2 = importe en moneda fuerte
                                                            //if (Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCuentaContable2"]).StartsWith("5"))
                                                            //    SigafLinea.Kblnr = Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim();//Kblnr = documento presupuestario
                                                            //else
                                                            SigafLinea.Kblnr = "";//Kblnr = documento presupuestario
                                                            SigafLinea.Kblpos = "";//Kblpos = posicion documento presupuestario
                                                            SigafLinea.Rcomp = "";
                                                            SigafLinea.Buzei = "";
                                                            SigafLinea.Mandt = "";
                                                            SigafLinea.Hbkid = "";//Hbkid = banco propio


                                                            //if ((lds_TiposAsiento.Tables["Table"].Rows[z]["Secuencia"].ToString().Trim()) != "3" || (lds_Pagos.Tables["Table"].Rows[y]["IdMoneda"].ToString().Trim() == "CRC"))
                                                            //{
                                                            if ((lds_Pagos.Tables["Table"].Rows[y]["IdMoneda"].ToString().Trim() != "CRC") &&(
                                                                   ((lds_TiposAsiento.Tables["Table"].Rows[z]["Secuencia"].ToString().Trim()) == "3") // && dec_DiferenciaPago > 0)
                                                                //|| ((lds_TiposAsiento.Tables["Table"].Rows[z]["Secuencia"].ToString().Trim()) == "2" && dec_DiferenciaPago > 0 && dec_DiferenciaTC > 0)
                                                                  ))
                                                            {
                                                                SigafTablaAsientoDif[lint_lineaDif] = SigafLinea;
                                                                lint_lineaDif++;
                                                                //lint_lineaPago++;
                                                            }
                                                            else
                                                            {
                                                                SigafTablaAsientoPago[lint_lineaPago] = SigafLinea;
                                                                lint_lineaPago++;
                                                            }
                                                            //else
                                                            //{
                                                            //    SigafTablaAsientoDif[lint_lineaDif] = SigafLinea;
                                                            //    lint_lineaDif++;
                                                            //    //lint_lineaPago++;
                                                            //}

                                                        }//if (!string.IsNullOrEmpty(Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdClaveContable2"])))
                                                        //SigafAsientoLogPago = lasiento.EnviarAsientoSigaf(SigafTablaAsientoPago);

                                                    }//if de estado Activo de la linea del asiento
                                                }//for de las lineas del asiento
                                            }//if asiento encontrado
                                            else
                                            {
                                                //Tipo de Asiento no Encontrado
                                                lstr_Mensaje = "Tipo de Asiento no Encontrado "
                                                    + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim()
                                                    + "." + lds_Servicios.Tables["Table"].Rows[0]["IdServicio"].ToString().Trim()
                                                    + "." + str_CodAuxiliar
                                                    + "." + lds_Formularios.Tables["Table"].Rows[i]["IdMoneda"].ToString()
                                                    + "." + str_IdTipoProcesoExp
                                                    + "." + str_CodAuxiliar4
                                                    + "." + str_CodAuxiliar5;
                                                resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora("CI", lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Envio Asientos", lstr_Mensaje, lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Asiento CT", lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim());
                                        
                                                lbln_ErrorAsientoLinea = true;
                                            }//Tipo de Asiento no encontrado
                                            #endregion SiExisteExpediente
                                        }//else
                                        #endregion AsientoContingentes
                                    }//if tiene expediente
                                    else
                                    {
                                        lstr_Mensaje = "No tiene Expediente " + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim() + "." + lds_Pagos.Tables["Table"].Rows[y]["IdServicio"].ToString().Trim();
                                        lbln_ErrorAsientoLinea = true;
                                    }
                                    #endregion TiposAsiento
                                }//if servicio encontrado
                                else
                                {
                                    //Servicio no Encontrado
                                    lstr_Mensaje = "Servicio no Encontrado " + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim() + "." + lds_Pagos.Tables["Table"].Rows[y]["IdServicio"].ToString().Trim();

                                    resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora("CI", lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Envio Asientos", lstr_Mensaje, lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Servicio CT", lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim());
                                        
                                    lbln_ErrorAsientoLinea = true;
                                }//servicio no encontrado
                                #endregion Servicios
                                y++;
                            }//while de pagos
                            /////////////////////////////////////////

                            if (!lbln_ErrorAsientoLinea)
                            {
                                #region Interfaz

                                try //Interfaz
                                {
                                    lbln_ErrorInterfaz = false;


                                    if (lint_lineaPago > 0)
                                    {
                                        #region PruebaAsientos
                                        logAsientoPago = string.Empty;
                                        //SigafAsientoLogPago = asientos.EnviarAsientos(SigafTablaAsientoPago);
                                        logAsientoPago = this.EnviarAsientoSigaf(SigafTablaAsientoPago, "X");
                                        /*for (int w = 0; w < item_resAsientosLogPago.Length; w++)
                                        {
                                            logAsientoPago += "\n" + i + "-" + item_resAsientosLogPago[w];
                                        }*/
                                        //MessageBox.Show("Resultado de contabilización: \n\n"+logAsientoPago);
                                        Log.Info("Resultado de contabilización Pago: \n\n" + logAsientoPago);
                                        if (logAsientoPago.Contains("[E]")
                                            )
                                        {
                                            lbln_ErrorAsientoLinea = true;
                                            lbln_ErrorInterfaz = true;
                                        }



                                        if (lint_lineaDif > 0 && !lbln_ErrorInterfaz)
                                        {
                                            //logAsientoPago = string.Empty;
                                            //SigafAsientoLogPago = asientos.EnviarAsientos(SigafTablaAsientoPago);
                                            logAsientoPago += this.EnviarAsientoSigaf(SigafTablaAsientoDif, "X");
                                            /*for (int w = 0; w < item_resAsientosLogPago.Length; w++)
                                            {
                                                logAsientoPago += "\n" + i + "-" + item_resAsientosLogPago[w];
                                            }*/
                                            //MessageBox.Show("Resultado de contabilización: \n\n"+logAsientoPago);
                                            Log.Info("Resultado de contabilización Pago: \n\n" + logAsientoPago);
                                            if (logAsientoPago.Contains("[E]")
                                                )
                                            {
                                                lbln_ErrorAsientoLinea = true;
                                                lbln_ErrorInterfaz = true;
                                            }

                                        }
                                        #endregion PruebaAsientos
                                        if (!lbln_ErrorInterfaz)
                                        {
                                            logAsientoPago = string.Empty;
                                            //SigafAsientoLogPago = asientos.EnviarAsientos(SigafTablaAsientoPago);
                                            logAsientoPago = this.EnviarAsientoSigaf(SigafTablaAsientoPago);
                                            /*for (int w = 0; w < item_resAsientosLogPago.Length; w++)
                                            {
                                                logAsientoPago += "\n" + i + "-" + item_resAsientosLogPago[w];
                                            }*/
                                            //MessageBox.Show("Resultado de contabilización: \n\n"+logAsientoPago);
                                            Log.Info("Resultado de contabilización Pago: \n\n" + logAsientoPago);
                                            if (logAsientoPago.Contains("[E]")
                                                )
                                            {
                                                lbln_ErrorAsientoLinea = true;
                                                lbln_ErrorInterfaz = true;
                                            }

                                            if (lint_lineaDif > 0 && !lbln_ErrorInterfaz)
                                            {
                                                //logAsientoPago = string.Empty;
                                                //SigafAsientoLogPago = asientos.EnviarAsientos(SigafTablaAsientoPago);
                                                logAsientoPago += this.EnviarAsientoSigaf(SigafTablaAsientoDif);
                                                /*for (int w = 0; w < item_resAsientosLogPago.Length; w++)
                                                {
                                                    logAsientoPago += "\n" + i + "-" + item_resAsientosLogPago[w];
                                                }*/
                                                //MessageBox.Show("Resultado de contabilización: \n\n"+logAsientoPago);
                                                Log.Info("Resultado de contabilización Pago: \n\n" + logAsientoPago);
                                                if (logAsientoPago.Contains("[E]")
                                                    )
                                                {
                                                    lbln_ErrorAsientoLinea = true;
                                                    lbln_ErrorInterfaz = true;
                                                }

                                            }
                                        }

                                    }

                                }
                                catch (Exception ex)
                                {
                                    lstr_Mensaje = "Error al invocar interfaz asientos Sigaf: " + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString() + ex.ToString();
                                    Log.Error(lstr_Mensaje);
                                    //lbln_ErrorAsientoLinea = true;
                                    lbln_ErrorInterfaz = true;
                                }//catch

                                #endregion Interfaz

                                #region Bitacora
                                lbln_ErrorBitacora = false;
                                //Registro de bitacora de movimientos
                                try
                                {
                                    if (!string.IsNullOrEmpty(logAsientoPago))
                                        lstr_Mensaje = logAsientoPago;
                                    resAsientosLogPago = reg_Bitacora.ufnRegistrarAccionBitacora("CI", lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Envio Asientos", lstr_Mensaje, lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Pago CT", lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim());

                                }//try registro en bitácora
                                catch (Exception err)
                                {
                                    lstr_Mensaje = "Error al registrar en bitácora Pago: " + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim() + err.Message;
                                    item_resAsientosLogPago[0] = lstr_Mensaje;
                                    lbln_ErrorBitacora = true;
                                    Log.Error(lstr_Mensaje);
                                    resAsientosLogPago = reg_Bitacora.ufnRegistrarAccionBitacora("CI", lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Envio Asientos", lstr_Mensaje, lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Pago CT", lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim());

                                }//catch
                                #endregion Bitacora
                            }// if no Error Asiento Linea


                            //////////////////////////////////////
                            #region CambioEstado
                            if (!lbln_ErrorAsientoLinea && !lbln_ErrorInterfaz)
                            {
                                //Contabilizado bien, hay que cambiar el estado del formulario a Contabilizado
                                string lstr_TxtSalida = "";
                                string lstr_CodSalida = "";
                                string lstr_MensajeEstado = "";
                                Boolean lbln_Resultado = true;
                                decimal? dec_MontoPrincipal = 0;
                                decimal? dec_MontoIntereses = 0;
                                decimal? dec_MontoInteresesMora = 0;
                                decimal? dec_MontoCostas = 0;
                                decimal? dec_MontoDanos = 0;
                                try
                                {
                                    DataTable dt_Pagos = lds_Pagos.Tables[0];
                                    foreach (DataRow dr_Pago in dt_Pagos.Rows)
                                    {
                                        switch(dr_Pago["IdServicio"].ToString().Trim() )
                                        { 
                                            case "344":
                                        
                                            dec_MontoPrincipal += Convert.ToDecimal( dr_Pago["Monto"]);
                                            break;
                                            case "345":
                                        
                                            dec_MontoIntereses += Convert.ToDecimal(dr_Pago["Monto"]);
                                            break;
                                            case "346":
                                        
                                            dec_MontoInteresesMora += Convert.ToDecimal(dr_Pago["Monto"]);
                                            break;
                                            case "347":
                                        
                                            dec_MontoCostas += Convert.ToDecimal(dr_Pago["Monto"]);

                                            break;
                                            case "348":
                                        
                                            dec_MontoDanos += Convert.ToDecimal(dr_Pago["Monto"]);
                                            break;
                                        }
                                    }


                                    if (lint_IdCPResLiq != 0)
                                    {
                                        lstr_Resultado = cobrospagos.ModificarCobrosPagosArchivo(
                                                  lint_IdRes,
                                                  lint_IdCPResLiq,
                                                  lstr_IdResolucion ,//Identificador único de la resolución dictada en los tribunales de justicia
                                                  lstr_IdExpedienteFK,
                                                  lstr_IdSociedadGL,
                                                  "Liquidacion",
                                                  str_MonedaExp,//dr_Res["Moneda"].ToString(),//La moneda en la cual se recibe el cobro. Campo obligatorio
                                                  dec_TipoCambioOrig,//dr_Res["TipoCambio"].ToString(),//El tipo de cambio al momento de incluirlo en el sistema.
                                                  0,//Es el monto principal a cobrar/pagar
                                                  0,//Monto principal a cobrar/pagar en colones
                                                  dec_MontoIntereses,
                                                  dec_MontoIntereses,
                                                  dec_MontoInteresesMora,
                                                  dec_MontoInteresesMora,
                                                  dec_MontoCostas,
                                                  dec_MontoCostas,
                                                  dec_MontoDanos,
                                                  dec_MontoDanos,
                                                  "1",
                                                  "Captura");

                                        reg_Bitacora.ufnRegistrarAccionBitacora("CT", "1", "Registro de Pago Liquidación", "Exp: " + lstr_IdExpedienteFK + " en " + lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim()
                                                + " Principal " + dec_MontoPrincipal.ToString()
                                                + " Intereses " + dec_MontoIntereses.ToString()
                                                + " Moratorios " + dec_MontoInteresesMora.ToString()
                                                + " Costas " + dec_MontoCostas.ToString()
                                                + " Daños " + dec_MontoDanos.ToString()
                                                );

                                    }
                                    if (lint_IdCPResEnFirme != 0)
                                    {
                                        lstr_Resultado = cobrospagos.ModificarCobrosPagosArchivo(
                                                  lint_IdRes,
                                                  lint_IdCPResEnFirme,
                                                  lstr_IdResolucion,//Identificador único de la resolución dictada en los tribunales de justicia
                                                  lstr_IdExpedienteFK,
                                                  lstr_IdSociedadGL,
                                                  "En Firme",
                                                  str_MonedaExp,//dr_Res["Moneda"].ToString(),//La moneda en la cual se recibe el cobro. Campo obligatorio
                                                  dec_TipoCambioOrig,//dr_Res["TipoCambio"].ToString(),//El tipo de cambio al momento de incluirlo en el sistema.
                                                  dec_MontoPrincipal,//Es el monto principal a cobrar/pagar
                                                  dec_MontoPrincipal,//Monto principal a cobrar/pagar en colones
                                                  0,//dec_MontoIntereses,
                                                  0,//dec_MontoIntereses,
                                                  0,//dec_MontoInteresesMora,
                                                  0,//dec_MontoInteresesMora,
                                                  0,//dec_MontoCostas,
                                                  0,//dec_MontoCostas,
                                                  0,//dec_MontoDanos,
                                                  0,//dec_MontoDanos,
                                                  "1",
                                                  "Captura");
                                        reg_Bitacora.ufnRegistrarAccionBitacora("CT", "1", "Registro de Pago En Firme", "Exp: " + lstr_IdExpedienteFK + " en " + lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim()
                                                + " Principal " + dec_MontoPrincipal.ToString()
                                                + " Intereses " + dec_MontoIntereses.ToString()
                                                + " Moratorios " + dec_MontoInteresesMora.ToString()
                                                + " Costas " + dec_MontoCostas.ToString()
                                                + " Daños " + dec_MontoDanos.ToString()
                                                );

                                    }

                                    lbln_Resultado = lcls_Formulario.CambiarEstadoFormularioCapturaIngresos(
                                        Convert.ToInt32(lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"]),
                                        gint_AnnoActual,
                                        "PAG",
                                        "CNT",
                                        "",
                                        "",
                                        out lstr_CodSalida,
                                        out lstr_TxtSalida
                                        );
                                    if (lbln_Resultado)
                                        lstr_MensajeEstado = "00";
                                    else
                                        lstr_MensajeEstado = "Código " + lstr_CodSalida + ": " + lstr_TxtSalida;

                                    if (lstr_MensajeEstado != "00")
                                    {
                                        lbln_Resultado = false;
                                        lstr_Mensaje = "Error al cambiar estado a pagado: " + lstr_MensajeEstado + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim();
                                        Log.Error(lstr_Mensaje);
                                        resAsientosLogPago = reg_Bitacora.ufnRegistrarAccionBitacora("CI", lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Envio Asientos", lstr_Mensaje, lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Pago", lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim());
                                    }
                                }
                                catch (Exception ex)
                                {
                                    lstr_Mensaje = "Error al cambiar estado a pagado: " + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim() + ex.ToString();
                                    Log.Error(lstr_Mensaje);

                                    resAsientosLogPago = reg_Bitacora.ufnRegistrarAccionBitacora("CI", lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Envio Asientos", lstr_Mensaje, lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Pago", lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim());
                                    lbln_ErrorCambioEstado = true;
                                }
                            } //if no Errores en contabilizacion de lineas  ni error de interfaz
                            else
                            {

                                Log.Error(lstr_Mensaje + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim());
                                resAsientosLogPago = reg_Bitacora.ufnRegistrarAccionBitacora("CI", lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Envio Asientos", lstr_Mensaje, lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Pago", lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim());
                                //Console.WriteLine("Error: " + lstr_Mensaje + lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString());
                                //Console.ReadLine().ToString();
                            }
                            #endregion CambioEstado
                        }//if pagos encontrados
                        #endregion Pagos


                    }//for de formularios
                }//if de formularios encontrados
                #endregion Formularios
                return 0;
            }//try 
            catch (Exception ex)
            {
                lstr_Mensaje = ex.ToString(); //+ lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString();
                Log.Error(lstr_Mensaje);
                //resAsientosLogPago = reg_Bitacora.ufnRegistrarAccionBitacora("CI", lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Envio Asientos", lstr_Mensaje, lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Pago", lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim());
                return -1;
                //Console.WriteLine("Error: " + lstr_Mensaje);
                //Console.ReadLine().ToString();
            }//catch
        }

        /// <summary>
        /// Procedimiento que contabiliza los formularios de captura de ingresos y contingentes.
        /// </summary>
        /// <param name="lint_AnnoActual">Anno Actual para buscar los formularios a contabilizar</param>
        /// <param name="lint_Idformulario">Identificador de formulario que se desea contabilizar</param>
        public string[] EnviarAsientosCICTJudicial(
         string lstr_IdExpediente,//Llave que relaciona las resoluciones dictadas, con los expedientes existentes
         string lstr_IdSociedadGL,
         string lstr_EstadoResolucion,//Campo que define qué tipo de resolución es, Resolución Provisional 1 ó 2,  Resolución en Firme (aplica tanto para activos contingentes como para pasivos contingentes), o Declaración Sin Lugar.
         string lstr_Moneda,//La moneda en la cual se recibe el cobro. Campo obligatorio
         decimal? ldec_TipoCambio,//El tipo de cambio al momento de incluirlo en el sistema.
         decimal? ldec_MontoPrincipal,//Es el monto principal a cobrar/pagar
         decimal? ldec_MontoPrincipalColones,//Monto principal a cobrar/pagar en colones
         decimal? ldec_Intereses,
         decimal? ldec_InteresesColones,
         decimal? ldec_InteresesMoratorios,
         decimal? ldec_InteresesMoratoriosColones,
         decimal? ldec_Costas,
         decimal? ldec_CostasColones,
         decimal? ldec_DanoMoral,
         decimal? ldec_DanoMoralColones, 
         string str_CodAuxiliar5 = "Judicial")
        {
            string[] resultado = new string[2];
            resultado[0] = "00";
            resultado[1] = "Proceso finalizado";
            DataTable dt_Pagos = new DataTable();
            DataRow dr_Pago;

            dt_Pagos.Columns.Add("IdServicio", typeof(string));
            dt_Pagos.Columns.Add("Monto", typeof(string));
            dt_Pagos.Columns.Add("IdMoneda", typeof(string));

            if (ldec_MontoPrincipal > 0)
            {
                dr_Pago = dt_Pagos.NewRow();
                dr_Pago["IdServicio"] = "344";
                dr_Pago["Monto"] = ldec_MontoPrincipal;
                dr_Pago["IdMoneda"] = lstr_Moneda;
                dt_Pagos.Rows.Add(dr_Pago);
            }
            if (ldec_Intereses > 0)
            {
                dr_Pago = dt_Pagos.NewRow();
                dr_Pago["IdServicio"] = "345";
                dr_Pago["Monto"] = ldec_Intereses;
                dr_Pago["IdMoneda"] = lstr_Moneda;
                dt_Pagos.Rows.Add(dr_Pago);
            }
            if (ldec_InteresesMoratorios > 0)
            {
                dr_Pago = dt_Pagos.NewRow();
                dr_Pago["IdServicio"] = "346";
                dr_Pago["Monto"] = ldec_InteresesMoratorios;
                dr_Pago["IdMoneda"] = lstr_Moneda;
                dt_Pagos.Rows.Add(dr_Pago);
            }
            if (ldec_Costas > 0)
            {
                dr_Pago = dt_Pagos.NewRow();
                dr_Pago["IdServicio"] = "347";
                dr_Pago["Monto"] = ldec_Costas;
                dr_Pago["IdMoneda"] = lstr_Moneda;
                dt_Pagos.Rows.Add(dr_Pago);
            }

            if (ldec_DanoMoral > 0)
            {
                dr_Pago = dt_Pagos.NewRow();
                dr_Pago["IdServicio"] = "348";
                dr_Pago["Monto"] = ldec_DanoMoral;
                dr_Pago["IdMoneda"] = lstr_Moneda;
                dt_Pagos.Rows.Add(dr_Pago);
            }
            ILog Log = LogManager.GetLogger("SGCierreContable");
            LogicaNegocio.wrSigafAsientos.ZfiAsiento SigafLinea = new LogicaNegocio.wrSigafAsientos.ZfiAsiento(); //wrSigafAsientos.ZfiAsiento();
            //wsAsientos.ZfiAsiento SigafLinea = new wsAsientos.ZfiAsiento(); //wrSigafAsientos.ZfiAsiento();
            LogicaNegocio.wrSigafAsientos.ZfiAsiento[] SigafTablaAsientoPago;
            //wsAsientos.ZfiAsiento[] SigafTablaAsientoPago;
            LogicaNegocio.wrSigafAsientos.ZfiAsiento[] SigafTablaAsientoDif;
            LogicaNegocio.wrSigafAsientos.ZfiAsientoLog[] SigafAsientoLogPago = new LogicaNegocio.wrSigafAsientos.ZfiAsientoLog[100];
            //string[] SigafAsientoLogPago = new string[100];
            //wsAsientos.ZfiAsientoLog[] SigafAsientoLogPago = new wsAsientos.ZfiAsientoLog[100];
            DateTime ldt_FechaActual = DateTime.Today;//new DateTime();

            DateTime ldt_FechaPago = DateTime.Today;//new DateTime();
            //-1 para que tome todos los formularios
           
            DateTime ldt_FchPagoDesde = Convert.ToDateTime("1900-01-01");
            DateTime ldt_FchPagoHasta = Convert.ToDateTime("2099-12-31");
            DataSet lds_Formularios = new DataSet();
            DataSet lds_Pagos = new DataSet();
            DataSet lds_Servicios = new DataSet();
            DataSet lds_TiposAsiento = new DataSet();
            DataSet lds_Operaciones = new DataSet();
            DataSet lds_Oficinas = new DataSet();
            DataSet lds_SociedadesGLSociedadesFi = new DataSet();
            DataSet lds_Expediente = new DataSet();
            DataSet lds_ReservasDetallado = new DataSet();

            clsOpcionesCatalogo oc = new clsOpcionesCatalogo();
            DataSet ds_opciones = new DataSet();

            clsFormulariosCapturaIngresos lformulario = new clsFormulariosCapturaIngresos();
            clsPagosPorFormulario lpago = new clsPagosPorFormulario();
            //clsTiposAsiento lasiento = new clsTiposAsiento();
            clsServicios lservicio = new clsServicios();
            clsOperaciones loperacion = new clsOperaciones();
            clsOficinas loficina = new clsOficinas();
            tSociedadGL lsociedadGL = new tSociedadGL();
            clsExpedientes lexpediente = new clsExpedientes();
            clsResoluciones lresolucion = new clsResoluciones();
            clsReservasDetalle lreservasDet = new clsReservasDetalle();
            //clsBitacoraDeMovimientosCuentasExpedientes reg_Bitacora = new clsBitacoraDeMovimientosCuentasExpedientes();
            tBitacora reg_Bitacora = new tBitacora();
            clsFormulariosCapturaIngresos lcls_Formulario = new clsFormulariosCapturaIngresos();
            string lstr_IdModulo = "IdModulo IN ('CI')";
            string lstr_Mensaje = "";
            string lstr_IdSociedadFI = "";
            string lstr_IdCeBe = "";
            string logAsientoPago = string.Empty;
            string[] item_resAsientosLogPago = new string[100];
            string resAsientosLogPago = string.Empty;
            string str_IdTipoProcesoExp = string.Empty;
            string str_MonedaExp = string.Empty;
            DateTime dt_FchTipoCambioExp;
            decimal dec_TipoCambioOrig = 0;
            decimal dec_TipoCambioCierreExp = 0;
            decimal dec_TipoCambioExp = 0;
            decimal dec_TipoCambioEUR = 0;
            decimal dec_MontoPrincipalExp = 0;
            decimal dec_MontoInteresesExp = 0;
            decimal dec_MontoInteresesMoraExp = 0;
            decimal dec_MontoCostasExp = 0;
            decimal dec_MontoDanosExp = 0;
            int lint_IdCPResEnFirme = 0;
            int lint_IdCPResLiq = 0;

            int lint_IdRes = 0;
            string lstr_IdResolucion = string.Empty;
            string lstr_IdExpedienteFK = string.Empty;
            decimal dec_Pago = 0;
            decimal dec_PagoCRC = 0;
            decimal dec_PagoEUR = 0;
            decimal dec_PagoUSD = 0;

            decimal dec_DiferenciaPago = 0;
            decimal dec_DiferenciaPagoCRC = 0;
            decimal dec_DiferenciaPagoUSD = 0;
            decimal dec_DiferenciaPagoEUR = 0;
            decimal dec_DiferenciaTC = 0;

            string str_CodError = string.Empty;
            string str_Mensaje = string.Empty;
            Boolean lbln_ErrorAsientoLinea = false;
            Boolean lbln_ErrorInterfaz = false;
            Boolean lbln_ErrorBitacora = false;
            Boolean lbln_ErrorCambioEstado = false;
            Boolean lbln_ExisteExpediente;
            Boolean lbln_TieneExpediente;
            Boolean lbln_PeriodoActual;
            Boolean lbln_AsientoEncontrado;

            decimal ldec_tipo_cambioUSD = 0;
            decimal ldec_tipo_cambioEUR = 0;

            string str_CodAuxiliar4 = string.Empty;
            string str_CodAuxiliar = string.Empty;
            string str_MonedaQry1 = "CRCN";

            string lstr_TransaccionVentaUSD = "3140";
            string lstr_TransaccionCompraUSD = "3280";


            try
            {
               
                #region Formularios
                try
                {

                    //ldt_FechaPago = Convert.ToDateTime(lds_Formularios.Tables["Table"].Rows[i]["FchPago"].ToString().Trim());
                    DataSet ds_tipoCambio = tiposCambio.ConsultarTiposCambio(str_MonedaQry1, ldt_FechaPago, lstr_TransaccionCompraUSD, "N");
                    if (ds_tipoCambio.Tables.Count > 0 && ds_tipoCambio.Tables["Table"].Rows.Count > 0)
                    {
                        // se realiza el cambio a dolares para procesar el asiento
                        ldec_tipo_cambioUSD = Convert.ToDecimal(ds_tipoCambio.Tables["Table"].Rows[0]["Valor"]);

                        //ldec_monto_max_moneda = ldec_monto_max / ldec_tipo_cambioUSD;
                    }//if ds_tipoCambio

                    ds_tipoCambio = tiposCambio.ConsultarTiposCambio("EUR", ldt_FechaPago, string.Empty, "N");
                    if (ds_tipoCambio.Tables.Count > 0 && ds_tipoCambio.Tables["Table"].Rows.Count > 0)
                    {
                        // se realiza el cambio a dolares para procesar el asiento
                        ldec_tipo_cambioEUR = Math.Round(Convert.ToDecimal(ds_tipoCambio.Tables["Table"].Rows[0]["Valor"]), 2);


                    }//if ds_tipoCambio

                }
                catch(Exception e)
                {
                    lstr_Mensaje = "Error al obtener TC " + e.ToString();
                    resultado[0] = "99";
                    resultado[1] = lstr_Mensaje;
                    lbln_ErrorAsientoLinea = true;

                }

                        int lint_lineaVerifica = 0;
                        lds_Pagos.Tables.Add(dt_Pagos);
                        //obtengo los pagos activos del formulario!
                        //lds_Pagos = lpago.ConsultarPagosPorFormulario(Convert.ToInt32(lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"]),
                        //                                              Convert.ToInt32(lds_Formularios.Tables["Table"].Rows[i]["Anno"]),
                        //                                              lint_IdPago,
                        //                                              ldt_FchPagoDesde,
                        //                                              ldt_FchPagoHasta,
                        //                                              string.Empty,
                        //                                              string.Empty,
                        //                                              string.Empty,
                        //                                              string.Empty,
                        //                                              string.Empty,
                        //                                              "A"
                        //                                              );
                        lstr_IdCeBe = "";
                        


                        #region Expediente
                        lbln_TieneExpediente = false;

                        lbln_ExisteExpediente = false;

                        try
                        {

                            //lds_Expediente = lexpediente.con (lds_Formularios.Tables["Table"].Rows[i]["NroExpediente"].ToString().Trim(), lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim());
                            lds_Expediente = lresolucion.ConsultarResolucion(null, lstr_IdExpediente, lstr_IdSociedadGL, out str_CodError, out str_Mensaje);
                            System.Data.DataTable dt_Res = lds_Expediente.Tables[0];
                            foreach (DataRow dr_Res in dt_Res.Rows)
                            {
                                //if (lds_Expediente.Tables.Count > 0 && lds_Expediente.Tables["Table"].Rows.Count > 0)
                                //{
                                lbln_TieneExpediente = true;
                                lstr_EstadoResolucion = dr_Res["EstadoResolucion"].ToString();//Campo que define qué tipo de resolución es, Resolución Provisional 1 ó 2,  Resolución en Firme (aplica tanto para activos contingentes como para pasivos contingentes), o Declaración Sin Lugar.
                                if (!string.IsNullOrEmpty(dr_Res["IdExpediente"].ToString().Trim()))
                                {
                                    lbln_ExisteExpediente = true;

                                    str_MonedaExp = dr_Res["Moneda"].ToString().Trim();

                                    lint_IdRes = Convert.ToInt32(dr_Res["IdRes"].ToString());
                                    lstr_IdResolucion = dr_Res["IdResolucion"].ToString();
                                    lstr_IdExpedienteFK = dr_Res["IdExpedienteFK"].ToString();//Llave que relaciona las resoluciones dictadas, con los expedientes existentes                       
                                    lstr_IdSociedadGL = dr_Res["IdSociedadGL"].ToString();
                                    lstr_EstadoResolucion = dr_Res["EstadoResolucion"].ToString();//Campo que define qué tipo de resolución es, Resolución Provisional 1 ó 2,  Resolución en Firme (aplica tanto para activos contingentes como para pasivos contingentes), o Declaración Sin Lugar.
                                    if (str_MonedaExp != "CRC")
                                    {
                                        try
                                        {
                                            dec_TipoCambioOrig = Convert.ToDecimal(dr_Res["TipoCambio1"]);
                                        }
                                        catch (Exception e)
                                        {
                                            dec_TipoCambioOrig = 0;
                                        }
                                        try
                                        {
                                            dec_TipoCambioCierreExp = Convert.ToDecimal(dr_Res["TipoCambioCierre"]);
                                        }
                                        catch (Exception e)
                                        {
                                            dec_TipoCambioCierreExp = 0;
                                        }

                                    }
                                    dt_FchTipoCambioExp = Convert.ToDateTime(dr_Res["FechResolucion"]);
                                    if (dec_TipoCambioCierreExp != 0)
                                    {
                                        int month = dt_FchTipoCambioExp.Month;
                                        int year = dt_FchTipoCambioExp.Year;

                                        int numberOfDays = DateTime.DaysInMonth(year, month);

                                        dt_FchTipoCambioExp = new DateTime(year, month, numberOfDays);

                                        dec_TipoCambioExp = dec_TipoCambioCierreExp;
                                    }
                                    else
                                    {

                                        dec_TipoCambioExp = dec_TipoCambioOrig;
                                    }
                                    //try
                                    //{
                                    //    dec_MontoPrincipalExp = Convert.ToDecimal(lds_Expediente.Tables["Table"].Rows[0]["MontoPrincipal"]);
                                    //}
                                    //catch (Exception e)
                                    //{
                                    //    dec_MontoPrincipalExp = 0;
                                    //}
                                    //try
                                    //{
                                    //    dec_MontoInteresesExp = Convert.ToDecimal(lds_Expediente.Tables["Table"].Rows[0]["MontoIntereses"]);
                                    //}
                                    //catch (Exception e)
                                    //{
                                    //    dec_MontoInteresesExp = 0;
                                    //}
                                    //if (dec_MontoInteresesExp == 0)
                                    //{
                                    //    try
                                    //    {
                                    //        dec_MontoInteresesExp = Convert.ToDecimal(lds_Expediente.Tables["Table"].Rows[0]["Intereses"]);
                                    //    }
                                    //    catch (Exception e)
                                    //    {
                                    //        dec_MontoInteresesExp = 0;
                                    //    }
                                    //}
                                    //try
                                    //{
                                    //    dec_MontoInteresesMoraExp = Convert.ToDecimal(lds_Expediente.Tables["Table"].Rows[0]["InteresesMoratorios"]);
                                    //}
                                    //catch (Exception e)
                                    //{
                                    //    dec_MontoPrincipalExp = 0;
                                    //}
                                    //try
                                    //{
                                    //    dec_MontoCostasExp = Convert.ToDecimal(lds_Expediente.Tables["Table"].Rows[0]["Costas"]);
                                    //}
                                    //catch (Exception e)
                                    //{
                                    //    dec_MontoCostasExp = 0;
                                    //}
                                    //try
                                    //{
                                    //    dec_MontoDanosExp = Convert.ToDecimal(lds_Expediente.Tables["Table"].Rows[0]["DanoMoral"]);
                                    //}
                                    //catch (Exception e)
                                    //{
                                    //    dec_MontoDanosExp = 0;
                                    //}

                                    if (lstr_EstadoResolucion == "En Firme")
                                    {

                                        lint_IdCPResEnFirme = Convert.ToInt32(dr_Res["IdCobroPagoResolucion"]);
                                        try
                                        {
                                            dec_MontoPrincipalExp = Convert.ToDecimal(dr_Res["MontoPrincipal"]);
                                        }
                                        catch (Exception ex)
                                        {
                                            dec_MontoPrincipalExp = 0;
                                        }
                                    }
                                    else if (lstr_EstadoResolucion == "Liquidacion")
                                    {

                                        lint_IdCPResLiq = Convert.ToInt32(dr_Res["IdCobroPagoResolucion"]);

                                        try
                                        {
                                            dec_MontoInteresesExp = Convert.ToDecimal(dr_Res["MontoIntereses"]);
                                        }
                                        catch (Exception ex)
                                        {
                                            dec_MontoInteresesExp = 0;
                                        }
                                        if (dec_MontoInteresesExp == 0)
                                        {
                                            try
                                            {
                                                dec_MontoInteresesExp = Convert.ToDecimal(dr_Res["Intereses"]);
                                            }
                                            catch (Exception e)
                                            {
                                                dec_MontoInteresesExp = 0;
                                            }
                                        }
                                        try
                                        {
                                            dec_MontoInteresesMoraExp = Convert.ToDecimal(dr_Res["InteresesMoratorios"]);
                                        }
                                        catch (Exception ex)
                                        {
                                            dec_MontoInteresesMoraExp = 0;
                                        }
                                        try
                                        {
                                            dec_MontoCostasExp = Convert.ToDecimal(dr_Res["Costas"]);
                                        }
                                        catch (Exception ex)
                                        {
                                            dec_MontoCostasExp = 0;
                                        }
                                        try
                                        {
                                            dec_MontoDanosExp = Convert.ToDecimal(dr_Res["DanoMoral"]);
                                        }
                                        catch (Exception ex)
                                        {
                                            dec_MontoDanosExp = 0;
                                        }
                                    }
                                    ds_opciones = oc.ConsultarOpcionesCatalogo(null, "TiposProcesoExpediente", null, dr_Res["TipoProcesoExpediente"].ToString().Trim());
                                    if (ds_opciones.Tables.Count > 0 && ds_opciones.Tables["Table"].Rows.Count > 0)
                                    {
                                        str_IdTipoProcesoExp = ds_opciones.Tables["Table"].Rows[0]["ValOpcion"].ToString().Trim();
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {

                            lstr_Mensaje = "Error al obtener resolucion " + lstr_IdExpediente+" "+ lstr_IdSociedadGL;
                            resultado[0] = "99";
                            resultado[1] = lstr_Mensaje;
                            lbln_ErrorAsientoLinea = true;
                        }
                        #endregion expediente
                            #region Pagos
                        try
                        {
                            lbln_ErrorAsientoLinea = false;
                            /////////////////////////////////////
                            SigafTablaAsientoDif = new LogicaNegocio.wrSigafAsientos.ZfiAsiento[lds_Pagos.Tables["Table1"].Rows.Count * 4];
                            SigafTablaAsientoPago = new LogicaNegocio.wrSigafAsientos.ZfiAsiento[lds_Pagos.Tables["Table1"].Rows.Count * 6];
                            //SigafTablaAsientoPago = new wsAsientos.ZfiAsiento[lds_Pagos.Tables["Table1"].Rows.Count * 6];
                            int lint_lineaDif = 0;
                            int lint_lineaPago = 0;
                            //int lint_lineaVerifica = 0;
                            Boolean lbln_Pago = false;
                            /////////////////////////////////////
                            //itero en todos los formularios pagados para sacar los servicios pagados e irlos contabilizando
                            int y = 0;
                            while (dt_Pagos.Rows.Count > y && !lbln_ErrorAsientoLinea)
                            {


                                //Obtengo los valores del servicio del pago para completar los parametros del webservice
                                lds_Servicios = lservicio.ConsultarServicios(dt_Pagos.Rows[y]["IdServicio"].ToString(),
                                                                             lstr_IdSociedadGL,
                                                                             string.Empty,
                                                                             string.Empty,
                                                                             string.Empty,
                                                                             string.Empty
                                                                             );
                                #region Servicios
                                //solo itero si el dataset tiene registros
                                if (lds_Servicios.Tables.Count > 0 && lds_Servicios.Tables["Table"].Rows.Count > 0)
                                {
                                    //lds_Operaciones = loperacion.ConsultarOperaciones("",lstr_IdModulo,string.Empty);
                                    ////solo itero si el dataset tiene registros
                                    //if (lds_Operaciones.Tables.Count > 0) {
                                    #region TiposAsiento
                                    if (lbln_TieneExpediente)
                                    {
                                        #region AsientoContingentes
                                        if (!lbln_ExisteExpediente)
                                        {
                                            #region NoExisteExpediente
                                            //(string str_CodigoAuxiliar3, string str_CodigoAuxiliar6, string str_CodigoAuxiliar4, string str_CodigoAuxiliar5 = null, string str_CodigoAuxiliar2 = null, int? int_Secuencia = null, string str_OrderBy = null, string str_Exacto = null)
                                            lds_TiposAsiento = this.ConsultarTiposAsiento(lstr_IdSociedadGL,
                                                                                              lstr_IdModulo,
                                                                                              lds_Servicios.Tables["Table"].Rows[0]["IdServicio"].ToString(),
                                                                                              string.Empty,
                                                                                              string.Empty,
                                                                                              string.Empty, //str_CodigoAuxiliar
                                                                                              string.Empty, //str_CodigoAuxiliar3
                                                                                              string.Empty,//str_CodigoAuxiliar6
                                                                                              "NO_EXISTE", //str_CodigoAuxiliar4
                                                                                              str_CodAuxiliar5, //str_CodigoAuxiliar5
                                                                                              dt_Pagos.Rows[y]["IdMoneda"].ToString());//str_CodigoAuxiliar2

                                            //solo itero si el dataset tiene registros
                                            if (lds_TiposAsiento.Tables.Count > 0 && lds_TiposAsiento.Tables["Table"].Rows.Count > 0)
                                            {

                                                //En este punto ya tengo los datos del formulario, el detalle del pago y el detalle del servicio, ya puedo consumir el webservice del asiento
                                                //SigafTablaAsientoPago = new LogicaNegocio.wrSigafAsientos.ZfiAsiento[lds_TiposAsiento.Tables["Table"].Rows.Count * 2];
                                                //int lint_lineaPago = 0;
                                                //itero en todos los formularios pagados para sacar los servicios pagados e irlos contabilizando
                                                for (int z = 0; lds_TiposAsiento.Tables["Table"].Rows.Count > z; z++)
                                                {

                                                    if (string.IsNullOrEmpty(lds_TiposAsiento.Tables["Table"].Rows[z]["Codigo"].ToString().Trim()))
                                                    {
                                                        lds_SociedadesGLSociedadesFi = lsociedadGL.ConsultarSociedadesGLSociedadesFi(lstr_IdSociedadGL, lstr_IdModulo, string.Empty);
                                                    }
                                                    else
                                                    {
                                                        lds_SociedadesGLSociedadesFi = lsociedadGL.ConsultarSociedadesGLSociedadesFi(lds_TiposAsiento.Tables["Table"].Rows[z]["Codigo"].ToString(), lstr_IdModulo, string.Empty);
                                                    }


                                                    //solo itero si el dataset tiene registros
                                                    if (lds_SociedadesGLSociedadesFi.Tables.Count > 0 && lds_SociedadesGLSociedadesFi.Tables["Table"].Rows.Count > 0)
                                                    {

                                                        lstr_IdSociedadFI = lds_SociedadesGLSociedadesFi.Tables["Table"].Rows[0]["IdSociedadFi"].ToString().Trim();
                                                    }//if de sociedad fi encontrado


                                                    //Verifico si la linea del asiento está Activa
                                                    if (lds_TiposAsiento.Tables["Table"].Rows[z]["Estado"].ToString().Trim() == "A")
                                                    {

                                                        SigafLinea = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
                                                        //SigafLinea = new wsAsientos.ZfiAsiento();
                                                        SigafLinea.Blart = "RI";// lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar4"].ToString().Trim(); //Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[i]["IdClaseDoc"]); //"SA"; //BLART = CLASE DE DOCUMENTO
                                                        SigafLinea.Bukrs = lstr_IdSociedadFI;//Convert.ToString(lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"]).Trim();//"G206";//BUKRS = ID de Sociedad
                                                        if (lint_lineaVerifica == 0)
                                                        {
                                                            SigafLinea.Bldat = ldt_FechaActual.ToString("dd.MM.yyyy");//Convert.ToString(ldt_FechaActual);//"01.10.2015";//bldat = Fecha de Documento
                                                            SigafLinea.Budat = ldt_FechaActual.ToString("dd.MM.yyyy");//Convert.ToString(ldt_FechaActual);//"01.10.2015";//budat = Fecha de Contabilizacion
                                                            lint_lineaVerifica = 1;
                                                        }
                                                        else
                                                        {
                                                            SigafLinea.Bldat = "";//Convert.ToString(ldt_FechaActual);//"01.10.2015";//bldat = Fecha de Documento
                                                            SigafLinea.Budat = "";//Convert.ToString(ldt_FechaActual);//"01.10.2015";//budat = Fecha de Contabilizacion
                                                        }
                                                        SigafLinea.Waers = lds_Pagos.Tables["Table"].Rows[y]["IdMoneda"].ToString().Trim();//"CRC";//waers = IdMoneda
                                                        SigafLinea.Xblnr = lstr_IdExpediente;//"REF";//Xblnr = Referencia
                                                        SigafLinea.Bktxt = lstr_IdExpediente; //"Texto_Cabecera";//Bktxt = "Texto_Cabecera"

                                                        //SigafLinea.Xref1Hd = ; //"REF_1";//Xref1Hd = "REF_1"
                                                        SigafLinea.Xref2Hd = "CI." + lstr_IdExpediente; //"REF_2";//Xref2Hd = "REF_2"
                                                        SigafLinea.Bschl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdClaveContable"]).Trim(); //"40";//Bschl = Clave Contable
                                                        SigafLinea.Hkont = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCuentaContable"]).Trim(); //"5120302000";//Hkont = Cuenta Contable de Mayor
                                                        SigafLinea.Wrbtr = Convert.ToDecimal(lds_Pagos.Tables["Table"].Rows[y]["Monto"]); //1505;//Wrbtr = Monto Importe
                                                        SigafLinea.Zuonr = "";//Zuonr = "Asignacion"
                                                        SigafLinea.Sgtxt = "";//Sgtxt = "Texto"
                                                        if (lbln_Pago && SigafLinea.Bschl.Trim() == "50" && !string.IsNullOrEmpty(Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim()))
                                                        {
                                                            lds_ReservasDetallado = lreservasDet.ConsultarReservasDetallado(Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim(), "", "", "", "");

                                                            //solo itero si el dataset tiene registros
                                                            if (lds_ReservasDetallado.Tables.Count > 0 && lds_ReservasDetallado.Tables["Table"].Rows.Count > 0)
                                                            {

                                                                SigafLinea.Fipex = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdPosPre"]).Trim(); //"E-10302";//Fipex = posicion presupuestaria
                                                                SigafLinea.Kostl = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdCentroCosto"]).Trim(); //"20613200";//Kostl = Centro de Costo
                                                                SigafLinea.Fistl = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdCentroGestor"]).Trim(); //"20613200";//Fistl = Centro Gestor
                                                                SigafLinea.Geber = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdFondo"]).Trim(); //"001";//Geber = Fondo                                                        }//if de Reservas encontrado
                                                            }
                                                        }
                                                        else
                                                        {
                                                            SigafLinea.Fipex = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdPosPre"]).Trim(); //"E-10302";//Fipex = posicion presupuestaria
                                                            SigafLinea.Kostl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroCosto"]).Trim(); //"20613200";//Kostl = Centro de Costo
                                                            SigafLinea.Fistl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroGestor"]).Trim(); //"20613200";//Fistl = Centro Gestor
                                                            SigafLinea.Geber = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdFondo"]).Trim(); //"001";//Geber = Fondo
                                                        }
                                                        SigafLinea.Fkber = "";//Fkber = Area Funcional
                                                        SigafLinea.Xref2 = ""; //Xref2 = Flujo de efectivo

                                                        SigafLinea.Valut = "";//Valut = Fecha de Valor                                                  
                                                        SigafLinea.Umskz = "";//Umskz = Indicador CME
                                                        SigafLinea.Werks = "";//Werks = Centro
                                                        SigafLinea.Xmwst = ""; //Calculo de Impuesto
                                                        SigafLinea.Xref3 = "";//Flujo de Efectivo
                                                        SigafLinea.Zfbdt = "";//Fecha base
                                                        SigafLinea.Zlsch = "";//Via de pago
                                                        SigafLinea.Projk = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdElementoPEP"]).Trim(); ////Projk = Id Elemento PEP
                                                        if (string.IsNullOrEmpty(lstr_IdCeBe) || !SigafLinea.Hkont.StartsWith("4"))
                                                        {
                                                            SigafLinea.Prctr = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroBeneficio"]).Trim(); ////Prctr = centro de beneficio
                                                        }
                                                        else
                                                        {
                                                            SigafLinea.Prctr = lstr_IdCeBe;
                                                        }
                                                        SigafLinea.Mwskz = "";//Mwskz = indicador de impuesto
                                                        SigafLinea.Measure = "";//Measure = programa presupuestario
                                                        SigafLinea.Kursf = 0;//Kursf = tipo de cambio
                                                        SigafLinea.Aufnr = "";//Aufnr = Orden
                                                        SigafLinea.Dmbe2 = 0;//Dmbe2 = importe en moneda fuerte
                                                        //if (Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCuentaContable"]).StartsWith("5") )
                                                        //    SigafLinea.Kblnr = Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim(); //Kblnr = documento presupuestario
                                                        //else
                                                        SigafLinea.Kblnr = ""; //Kblnr = documento presupuestario
                                                        SigafLinea.Kblpos = "";//Kblpos = posicion documento presupuestario
                                                        SigafLinea.Rcomp = "";
                                                        SigafLinea.Buzei = "";
                                                        SigafLinea.Mandt = "";
                                                        SigafLinea.Hbkid = "";//Hbkid = banco propio


                                                        SigafLinea.Xref1Hd = "PAGADO"; //"REF_1";//Xref1Hd = "REF_1"
                                                        SigafTablaAsientoPago[lint_lineaPago] = SigafLinea;
                                                        lint_lineaPago++;


                                                        if (!string.IsNullOrEmpty(Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdClaveContable2"])))
                                                        {
                                                            ///cuenta 2
                                                            SigafLinea = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
                                                            //SigafLinea = new wsAsientos.ZfiAsiento();
                                                            SigafLinea.Blart = "RI";//lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar4"].ToString().Trim();  //"SA"; //BLART = CLASE DE DOCUMENTO
                                                            SigafLinea.Bukrs = "";//"G206";//BUKRS = ID de Sociedad
                                                            SigafLinea.Bldat = "";//"01.10.2015";//bldat = Fecha de Documento
                                                            SigafLinea.Budat = "";//"01.10.2015";//budat = Fecha de Contabilizacion
                                                            SigafLinea.Waers = "";//"CRC";//waers = IdMoneda
                                                            SigafLinea.Xblnr = "";//"REF";//Xblnr = Referencia
                                                            SigafLinea.Bktxt = ""; //"Texto_Cabecera";//Bktxt = "Texto_Cabecera"
                                                            SigafLinea.Xref1Hd = ""; //"REF_1";//Xref1Hd = "REF_1"
                                                            SigafLinea.Xref2Hd = ""; //"REF_2";//Xref2Hd = "REF_2"
                                                            SigafLinea.Bschl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdClaveContable2"]).Trim(); //"40";//Bschl = Clave Contable
                                                            SigafLinea.Hkont = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCuentaContable2"]).Trim(); //"5120302000";//Hkont = Cuenta Contable de Mayor
                                                            SigafLinea.Wrbtr = Convert.ToDecimal(lds_Pagos.Tables["Table"].Rows[y]["Monto"]); //1505;//Wrbtr = Monto Importe
                                                            SigafLinea.Zuonr = "";//Zuonr = "Asignacion"
                                                            SigafLinea.Sgtxt = "";//Sgtxt = "Texto"
                                                            if (lbln_Pago && SigafLinea.Bschl.Trim() == "50" && !string.IsNullOrEmpty(Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim()))
                                                            {
                                                                lds_ReservasDetallado = lreservasDet.ConsultarReservasDetallado(Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim(), "", "", "", "");

                                                                //solo itero si el dataset tiene registros
                                                                if (lds_ReservasDetallado.Tables.Count > 0 && lds_ReservasDetallado.Tables["Table"].Rows.Count > 0)
                                                                {

                                                                    SigafLinea.Fipex = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdPosPre"]).Trim(); //"E-10302";//Fipex = posicion presupuestaria
                                                                    SigafLinea.Kostl = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdCentroCosto"]).Trim(); //"20613200";//Kostl = Centro de Costo
                                                                    SigafLinea.Fistl = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdCentroGestor"]).Trim(); //"20613200";//Fistl = Centro Gestor
                                                                    SigafLinea.Geber = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdFondo"]).Trim(); //"001";//Geber = Fondo                                                        }//if de Reservas encontrado
                                                                }
                                                            }
                                                            else
                                                            {
                                                                SigafLinea.Fipex = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdPosPre2"]).Trim(); //"E-10302";//Fipex = posicion presupuestaria
                                                                SigafLinea.Kostl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroCosto2"]).Trim(); //"20613200";//Kostl = Centro de Costo
                                                                SigafLinea.Fistl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroGestor2"]).Trim(); //"20613200";//Fistl = Centro Gestor
                                                                SigafLinea.Geber = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdFondo2"]).Trim(); //"001";//Geber = Fondo
                                                            }
                                                            SigafLinea.Fkber = "";//Fkber = Area Funcional
                                                            SigafLinea.Xref2 = ""; //Xref2 = Flujo de efectivo

                                                            SigafLinea.Valut = "";//Valut = Fecha de Valor                                                  
                                                            SigafLinea.Umskz = "";//Umskz = Indicador CME
                                                            SigafLinea.Werks = "";//Werks = Centro
                                                            SigafLinea.Xmwst = ""; //Calculo de Impuesto
                                                            SigafLinea.Xref3 = "";//Flujo de Efectivo
                                                            SigafLinea.Zfbdt = "";//Fecha base
                                                            SigafLinea.Zlsch = "";//Via de pago
                                                            SigafLinea.Projk = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdElementoPEP2"]).Trim(); ////Projk = Id Elemento PEP
                                                            if (string.IsNullOrEmpty(lstr_IdCeBe) || !SigafLinea.Hkont.StartsWith("4"))
                                                            {
                                                                SigafLinea.Prctr = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroBeneficio2"]).Trim(); ////Prctr = centro de beneficio
                                                            }
                                                            else
                                                            {
                                                                SigafLinea.Prctr = lstr_IdCeBe;
                                                            }
                                                            SigafLinea.Mwskz = "";//Mwskz = indicador de impuesto
                                                            SigafLinea.Measure = "";//Measure = programa presupuestario
                                                            SigafLinea.Kursf = 0;//Kursf = tipo de cambio
                                                            SigafLinea.Aufnr = "";//Aufnr = Orden
                                                            SigafLinea.Dmbe2 = 0;//Dmbe2 = importe en moneda fuerte
                                                            //if (Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCuentaContable2"]).StartsWith("5"))
                                                            //    SigafLinea.Kblnr = Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim();//Kblnr = documento presupuestario
                                                            //else
                                                            SigafLinea.Kblnr = "";//Kblnr = documento presupuestario
                                                            SigafLinea.Kblpos = "";//Kblpos = posicion documento presupuestario
                                                            SigafLinea.Rcomp = "";
                                                            SigafLinea.Buzei = "";
                                                            SigafLinea.Mandt = "";
                                                            SigafLinea.Hbkid = "";//Hbkid = banco propio



                                                            SigafTablaAsientoPago[lint_lineaPago] = SigafLinea;
                                                            lint_lineaPago++;

                                                        }//if (!string.IsNullOrEmpty(Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdClaveContable2"])))
                                                        //SigafAsientoLogPago = lasiento.EnviarAsientoSigaf(SigafTablaAsientoPago);

                                                    }//if de estado Activo de la linea del asiento
                                                }//for de las lineas del asiento
                                            }//if asiento encontrado
                                            else
                                            {
                                                //Tipo de Asiento no Encontrado
                                                lstr_Mensaje = "Tipo de Asiento no Encontrado " + lstr_IdExpediente + "." + lds_Servicios.Tables["Table"].Rows[0]["IdServicio"].ToString().Trim();

                                                resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora("CT", "1", "Envio Asientos", lstr_Mensaje, "1", "Asiento", lstr_IdSociedadGL);
                                                resultado[0] = "99";
                                                resultado[1] = lstr_Mensaje;
                                                lbln_ErrorAsientoLinea = true;
                                            }//Tipo de Asiento no encontrado
                                            #endregion NoExisteExpediente
                                        }//if existe expediente.
                                        else
                                        {
                                            str_CodAuxiliar = string.Empty; //TIPO DE CAMBIO NO SE USA SI EL EXP. ES EN CRC
                                            dec_Pago = Convert.ToDecimal(lds_Pagos.Tables["Table1"].Rows[y]["Monto"]);
                                            switch (dt_Pagos.Rows[y]["IdMoneda"].ToString().Trim())
                                            {
                                                case "CRC":
                                                    dec_PagoCRC = Convert.ToDecimal(lds_Pagos.Tables["Table1"].Rows[y]["Monto"]);
                                                    dec_PagoUSD = dec_PagoCRC / ldec_tipo_cambioUSD;
                                                    if (ldec_tipo_cambioEUR != 0)//MONTO está en USD y se debe pasar a la otra moneda
                                                    {
                                                        dec_PagoEUR = dec_PagoUSD / ldec_tipo_cambioEUR;
                                                    }

                                                    break;
                                                case "USD":
                                                    dec_PagoUSD = Convert.ToDecimal(lds_Pagos.Tables["Table1"].Rows[y]["Monto"]);
                                                    dec_PagoCRC = dec_PagoUSD * ldec_tipo_cambioUSD;
                                                    if (ldec_tipo_cambioEUR != 0)//MONTO está en USD y se debe pasar a la otra moneda
                                                    {
                                                        dec_PagoEUR = dec_PagoUSD / ldec_tipo_cambioEUR;
                                                    }

                                                    break;
                                                case "EUR":
                                                    dec_PagoEUR = Convert.ToDecimal(lds_Pagos.Tables["Table1"].Rows[y]["Monto"]);
                                                    dec_PagoUSD = dec_PagoEUR * ldec_tipo_cambioEUR;
                                                    dec_PagoCRC = dec_PagoUSD * ldec_tipo_cambioUSD;

                                                    break;
                                                default:
                                                    break;
                                            }
                                            switch (str_MonedaExp.Trim())
                                            {
                                                case "CRC":

                                                    switch (lds_Servicios.Tables["Table"].Rows[0]["IdServicio"].ToString().Trim())
                                                    {
                                                        case "344": //principal
                                                            dec_DiferenciaPagoCRC = dec_PagoCRC - dec_MontoPrincipalExp;
                                                            if (dec_PagoCRC > dec_MontoPrincipalExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";

                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "345"://intereses
                                                            dec_DiferenciaPagoCRC = dec_PagoCRC - dec_MontoInteresesExp;
                                                            if (dec_PagoCRC > dec_MontoInteresesExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "346"://intereses moratorios
                                                            dec_DiferenciaPagoCRC = dec_PagoCRC - dec_MontoInteresesMoraExp;
                                                            if (dec_PagoCRC > dec_MontoInteresesMoraExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "347"://costas
                                                            dec_DiferenciaPagoCRC = dec_PagoCRC - dec_MontoCostasExp;
                                                            if (dec_PagoCRC > dec_MontoCostasExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "348"://danos & perjuicios
                                                            dec_DiferenciaPagoCRC = dec_PagoCRC - dec_MontoDanosExp;
                                                            if (dec_PagoCRC > dec_MontoDanosExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        default:
                                                            break;
                                                    }
                                                    break;
                                                case "USD":
                                                    dec_DiferenciaTC = ldec_tipo_cambioUSD - dec_TipoCambioExp;
                                                    if (ldec_tipo_cambioUSD > dec_TipoCambioExp)
                                                    {
                                                        str_CodAuxiliar = "TC_MAYOR";
                                                    }
                                                    else
                                                    {
                                                        str_CodAuxiliar = "TC_MENOR";
                                                    }
                                                    switch (lds_Servicios.Tables["Table"].Rows[0]["IdServicio"].ToString().Trim())
                                                    {
                                                        case "344": //principal
                                                            dec_DiferenciaPagoUSD = dec_PagoUSD - dec_MontoPrincipalExp;
                                                            if (dec_PagoUSD > dec_MontoPrincipalExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "345"://intereses
                                                            dec_DiferenciaPagoUSD = dec_PagoUSD - dec_MontoInteresesExp;
                                                            if (dec_PagoUSD > dec_MontoInteresesExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "346"://intereses moratorios
                                                            dec_DiferenciaPagoUSD = dec_PagoUSD - dec_MontoInteresesMoraExp;
                                                            if (dec_PagoUSD > dec_MontoInteresesMoraExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "347"://costas
                                                            dec_DiferenciaPagoUSD = dec_PagoUSD - dec_MontoCostasExp;
                                                            if (dec_PagoUSD > dec_MontoCostasExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "348"://danos & perjuicios
                                                            dec_DiferenciaPagoUSD = dec_PagoUSD - dec_MontoDanosExp;
                                                            if (dec_PagoUSD > dec_MontoDanosExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        default:
                                                            break;
                                                    }
                                                    dec_DiferenciaPagoCRC = dec_DiferenciaPagoUSD * ldec_tipo_cambioUSD;
                                                    if (ldec_tipo_cambioEUR != 0)//MONTO está en USD y se debe pasar a la otra moneda
                                                    {
                                                        dec_DiferenciaPagoEUR = dec_DiferenciaPagoUSD / ldec_tipo_cambioEUR;
                                                    }
                                                    break;
                                                case "EUR":
                                                    dec_DiferenciaTC = Math.Round((ldec_tipo_cambioEUR * ldec_tipo_cambioUSD), 2) - dec_TipoCambioExp;
                                                    if (Math.Round((ldec_tipo_cambioEUR * ldec_tipo_cambioUSD), 2) > dec_TipoCambioExp)
                                                    {
                                                        str_CodAuxiliar = "TC_MAYOR";
                                                    }
                                                    else
                                                    {
                                                        str_CodAuxiliar = "TC_MENOR";
                                                    }
                                                    switch (lds_Servicios.Tables["Table"].Rows[0]["IdServicio"].ToString().Trim())
                                                    {
                                                        case "344": //principal
                                                            dec_DiferenciaPagoEUR = dec_PagoEUR - dec_MontoPrincipalExp;
                                                            if (dec_PagoEUR > dec_MontoPrincipalExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "345"://intereses
                                                            dec_DiferenciaPagoEUR = dec_PagoEUR - dec_MontoInteresesExp;
                                                            if (dec_PagoEUR > dec_MontoInteresesExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "346"://intereses moratorios
                                                            dec_DiferenciaPagoEUR = dec_PagoEUR - dec_MontoInteresesMoraExp;
                                                            if (dec_PagoEUR > dec_MontoInteresesMoraExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "347"://costas
                                                            dec_DiferenciaPagoEUR = dec_PagoEUR - dec_MontoCostasExp;
                                                            if (dec_PagoEUR > dec_MontoCostasExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        case "348"://danos & perjuicios
                                                            dec_DiferenciaPagoEUR = dec_PagoEUR - dec_MontoDanosExp;
                                                            if (dec_PagoEUR > dec_MontoDanosExp)
                                                            {
                                                                str_CodAuxiliar4 = "EXISTE_MAYOR";
                                                            }
                                                            else
                                                            {

                                                                str_CodAuxiliar4 = "EXISTE_MENORIGUAL";
                                                            }
                                                            break;
                                                        default:
                                                            break;
                                                    }
                                                    dec_DiferenciaPagoUSD = dec_DiferenciaPagoEUR * ldec_tipo_cambioEUR;
                                                    dec_DiferenciaPagoCRC = dec_DiferenciaPagoUSD * ldec_tipo_cambioUSD;
                                                    break;
                                                default:
                                                    break;
                                            }
                                            switch (dt_Pagos.Rows[y]["IdMoneda"].ToString().Trim())
                                            {
                                                case "CRC":
                                                    dec_DiferenciaPago = dec_DiferenciaPagoCRC;

                                                    break;
                                                case "USD":
                                                    dec_DiferenciaPago = dec_DiferenciaPagoUSD;

                                                    break;
                                                case "EUR":
                                                    dec_DiferenciaPago = dec_DiferenciaPagoEUR;

                                                    break;
                                                default:
                                                    break;
                                            }

                                            #region SiExisteExpediente
                                            //ConsultarTiposAsiento(string str_CodigoAuxiliar3, string str_CodigoAuxiliar6, string str_CodigoAuxiliar4, string str_CodigoAuxiliar5 = null, string str_CodigoAuxiliar2 = null, int? int_Secuencia = null, string str_OrderBy = null, string str_Exacto = null)
                                            lds_TiposAsiento = this.ConsultarTiposAsiento(lstr_IdSociedadGL,
                                                                                              lstr_IdModulo,
                                                                                              dt_Pagos.Rows[y]["IdServicio"].ToString(),
                                                                                              string.Empty,
                                                                                              string.Empty,
                                                                                              str_CodAuxiliar, //CodigoAuxiliar
                                                                                              str_IdTipoProcesoExp, //CodigoAuxiliar3
                                                                                              string.Empty, //CodigoAuxiliar6
                                                                                              str_CodAuxiliar4, //CodigoAuxiliar4
                                                                                              str_CodAuxiliar5,//CodigoAuxiliar5
                                                                                              dt_Pagos.Rows[y]["IdMoneda"].ToString()); //CodigoAuxiliar2

                                            //solo itero si el dataset tiene registros
                                            if (lds_TiposAsiento.Tables.Count > 0 && lds_TiposAsiento.Tables["Table"].Rows.Count > 0)
                                            {

                                                //En este punto ya tengo los datos del formulario, el detalle del pago y el detalle del servicio, ya puedo consumir el webservice del asiento
                                                //SigafTablaAsientoPago = new LogicaNegocio.wrSigafAsientos.ZfiAsiento[lds_TiposAsiento.Tables["Table"].Rows.Count * 2];
                                                //int lint_lineaPago = 0;
                                                //itero en todos los formularios pagados para sacar los servicios pagados e irlos contabilizando
                                                for (int z = 0; lds_TiposAsiento.Tables["Table"].Rows.Count > z; z++)
                                                {

                                                    if (string.IsNullOrEmpty(lds_TiposAsiento.Tables["Table"].Rows[z]["Codigo"].ToString().Trim()))
                                                    {
                                                        lds_SociedadesGLSociedadesFi = lsociedadGL.ConsultarSociedadesGLSociedadesFi(lstr_IdSociedadGL, lstr_IdModulo, string.Empty);
                                                    }
                                                    else
                                                    {
                                                        lds_SociedadesGLSociedadesFi = lsociedadGL.ConsultarSociedadesGLSociedadesFi(lds_TiposAsiento.Tables["Table"].Rows[z]["Codigo"].ToString(), lstr_IdModulo, string.Empty);
                                                    }


                                                    //solo itero si el dataset tiene registros
                                                    if (lds_SociedadesGLSociedadesFi.Tables.Count > 0 && lds_SociedadesGLSociedadesFi.Tables["Table"].Rows.Count > 0)
                                                    {

                                                        lstr_IdSociedadFI = lds_SociedadesGLSociedadesFi.Tables["Table"].Rows[0]["IdSociedadFi"].ToString().Trim();
                                                    }//if de sociedad fi encontrado


                                                    //Verifico si la linea del asiento está Activa
                                                    if (lds_TiposAsiento.Tables["Table"].Rows[z]["Estado"].ToString().Trim() == "A")
                                                    {

                                                        SigafLinea = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
                                                        //SigafLinea = new wsAsientos.ZfiAsiento();
                                                        SigafLinea.Blart = "RI";// lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar4"].ToString().Trim(); //Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[i]["IdClaseDoc"]); //"SA"; //BLART = CLASE DE DOCUMENTO
                                                        SigafLinea.Bukrs = lstr_IdSociedadFI;//Convert.ToString(lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"]).Trim();//"G206";//BUKRS = ID de Sociedad
                                                        if (lint_lineaPago == 0
                                                        || ((lint_lineaDif == 0 &&
                                                              ((lds_Pagos.Tables["Table1"].Rows[y]["IdMoneda"].ToString().Trim() != "CRC") && (
                                                               ((lds_TiposAsiento.Tables["Table"].Rows[z]["Secuencia"].ToString().Trim()) == "3") // && dec_DiferenciaPago > 0)
                                                            //|| ((lds_TiposAsiento.Tables["Table"].Rows[z]["Secuencia"].ToString().Trim()) == "2" && dec_DiferenciaPago > 0 && dec_DiferenciaTC > 0)
                                                              ))
                                                              )
                                                           )
                                                           )
                                                        {
                                                            SigafLinea.Bldat = ldt_FechaActual.ToString("dd.MM.yyyy");//Convert.ToString(ldt_FechaActual);//"01.10.2015";//bldat = Fecha de Documento
                                                            SigafLinea.Budat = ldt_FechaActual.ToString("dd.MM.yyyy");//Convert.ToString(ldt_FechaActual);//"01.10.2015";//budat = Fecha de Contabilizacion
                                                            lint_lineaVerifica = 1;
                                                        }
                                                        else
                                                        {
                                                            SigafLinea.Bldat = "";//Convert.ToString(ldt_FechaActual);//"01.10.2015";//bldat = Fecha de Documento
                                                            SigafLinea.Budat = "";//Convert.ToString(ldt_FechaActual);//"01.10.2015";//budat = Fecha de Contabilizacion
                                                        }
                                                        SigafLinea.Waers = lds_Pagos.Tables["Table1"].Rows[y]["IdMoneda"].ToString().Trim();//"CRC";//waers = IdMoneda
                                                        if (SigafLinea.Waers.Trim() != "CRC")
                                                            SigafLinea.Kursf = (SigafLinea.Waers.Trim() == "USD") ? Math.Round(ldec_tipo_cambioUSD, 5) : Math.Round(ldec_tipo_cambioEUR * ldec_tipo_cambioUSD, 5);//tipo de cambio 
                                                        SigafLinea.Xblnr = lstr_IdExpediente;//"REF";//Xblnr = Referencia
                                                        SigafLinea.Bktxt = lstr_IdExpediente; //"Texto_Cabecera";//Bktxt = "Texto_Cabecera"

                                                        //SigafLinea.Xref1Hd = ; //"REF_1";//Xref1Hd = "REF_1"
                                                        SigafLinea.Xref2Hd = "CI." + lstr_IdExpediente; //"REF_2";//Xref2Hd = "REF_2"
                                                        SigafLinea.Bschl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdClaveContable"]).Trim(); //"40";//Bschl = Clave Contable
                                                        SigafLinea.Hkont = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCuentaContable"]).Trim(); //"5120302000";//Hkont = Cuenta Contable de Mayor
                                                        switch ((lds_TiposAsiento.Tables["Table"].Rows[z]["Secuencia"].ToString().Trim()))
                                                        {
                                                            case "1":
                                                                SigafLinea.Wrbtr = dec_Pago;//Convert.ToDecimal(lds_Pagos.Tables["Table"].Rows[y]["Monto"]); //1505;//Wrbtr = Monto Importe
                                                                if (dec_DiferenciaPago > 0)
                                                                    SigafLinea.Wrbtr -= dec_DiferenciaPago;
                                                                //if (dec_DiferenciaTC < 0 && SigafLinea.Waers.Trim() != "CRC")
                                                                //    SigafLinea.Kursf = (SigafLinea.Waers.Trim() == "USD") ? Math.Round(ldec_tipo_cambioUSD, 5) : Math.Round(ldec_tipo_cambioEUR * ldec_tipo_cambioUSD, 5);//tipo de cambio                                                
                                                                break;
                                                            case "2":
                                                                SigafLinea.Wrbtr = Math.Abs(dec_DiferenciaPago);
                                                                //if (SigafLinea.Waers.Trim() != "CRC")
                                                                //    SigafLinea.Kursf = (SigafLinea.Waers.Trim() == "USD") ? Math.Round(ldec_tipo_cambioUSD, 5) : Math.Round(ldec_tipo_cambioEUR * ldec_tipo_cambioUSD, 5);//tipo de cambio 
                                                                break;
                                                            case "3":
                                                                SigafLinea.Waers = "CRC";
                                                                //if (dec_DiferenciaTC < 0)
                                                                //{
                                                                    if (dec_DiferenciaPago > 0)
                                                                        SigafLinea.Wrbtr = (dec_Pago - dec_DiferenciaPago) * Math.Abs(dec_DiferenciaTC);
                                                                    else
                                                                        SigafLinea.Wrbtr = (dec_Pago) * Math.Abs(dec_DiferenciaTC);
                                                                    //SigafLinea.Kursf = 0;
                                                                    SigafLinea.Kursf = 0;//tipo de cambio 
                                                                /*}
                                                                else
                                                                {
                                                                    if (dec_DiferenciaPago > 0)
                                                                        SigafLinea.Wrbtr = (dec_Pago - dec_DiferenciaPago) * Math.Abs(dec_DiferenciaTC) / ldec_tipo_cambioUSD;
                                                                    else
                                                                        SigafLinea.Wrbtr = (dec_Pago) * Math.Abs(dec_DiferenciaTC) / ((SigafLinea.Waers.Trim() == "USD") ? ldec_tipo_cambioUSD : Math.Round(ldec_tipo_cambioEUR * ldec_tipo_cambioUSD, 5));
                                                                    //SigafLinea.Kursf = 0;
                                                                    SigafLinea.Kursf = (SigafLinea.Waers.Trim() == "USD") ? Math.Round(ldec_tipo_cambioUSD, 5) : Math.Round(ldec_tipo_cambioEUR * ldec_tipo_cambioUSD, 5);//tipo de cambio 
                                                                }*/

                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                        SigafLinea.Wrbtr = Math.Round(SigafLinea.Wrbtr, 2);

                                                        SigafLinea.Zuonr = "";//Zuonr = "Asignacion"
                                                        SigafLinea.Sgtxt = "";//Sgtxt = "Texto"
                                                        if (lbln_Pago && SigafLinea.Bschl.Trim() == "50" && !string.IsNullOrEmpty(Convert.ToString(lds_Pagos.Tables["Table1"].Rows[y]["IdReservaPresupuestaria"]).Trim()))
                                                        {
                                                            lds_ReservasDetallado = lreservasDet.ConsultarReservasDetallado(Convert.ToString(lds_Pagos.Tables["Table1"].Rows[y]["IdReservaPresupuestaria"]).Trim(), "", "", "", "");

                                                            //solo itero si el dataset tiene registros
                                                            if (lds_ReservasDetallado.Tables.Count > 0 && lds_ReservasDetallado.Tables["Table"].Rows.Count > 0)
                                                            {

                                                                SigafLinea.Fipex = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdPosPre"]).Trim(); //"E-10302";//Fipex = posicion presupuestaria
                                                                SigafLinea.Kostl = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdCentroCosto"]).Trim(); //"20613200";//Kostl = Centro de Costo
                                                                SigafLinea.Fistl = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdCentroGestor"]).Trim(); //"20613200";//Fistl = Centro Gestor
                                                                SigafLinea.Geber = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdFondo"]).Trim(); //"001";//Geber = Fondo                                                        }//if de Reservas encontrado
                                                            }
                                                        }
                                                        else
                                                        {
                                                            SigafLinea.Fipex = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdPosPre"]).Trim(); //"E-10302";//Fipex = posicion presupuestaria
                                                            SigafLinea.Kostl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroCosto"]).Trim(); //"20613200";//Kostl = Centro de Costo
                                                            SigafLinea.Fistl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroGestor"]).Trim(); //"20613200";//Fistl = Centro Gestor
                                                            SigafLinea.Geber = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdFondo"]).Trim(); //"001";//Geber = Fondo
                                                        }
                                                        SigafLinea.Fkber = "";//Fkber = Area Funcional
                                                        SigafLinea.Xref2 = ""; //Xref2 = Flujo de efectivo

                                                        SigafLinea.Valut = "";//Valut = Fecha de Valor                                                  
                                                        SigafLinea.Umskz = "";//Umskz = Indicador CME
                                                        SigafLinea.Werks = "";//Werks = Centro
                                                        SigafLinea.Xmwst = ""; //Calculo de Impuesto
                                                        SigafLinea.Xref3 = "";//Flujo de Efectivo
                                                        SigafLinea.Zfbdt = "";//Fecha base
                                                        SigafLinea.Zlsch = "";//Via de pago
                                                        SigafLinea.Projk = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdElementoPEP"]).Trim(); ////Projk = Id Elemento PEP
                                                        if (string.IsNullOrEmpty(lstr_IdCeBe) || !SigafLinea.Hkont.StartsWith("4"))
                                                        {
                                                            SigafLinea.Prctr = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroBeneficio"]).Trim(); ////Prctr = centro de beneficio
                                                        }
                                                        else
                                                        {
                                                            SigafLinea.Prctr = lstr_IdCeBe;
                                                        }
                                                        SigafLinea.Mwskz = "";//Mwskz = indicador de impuesto
                                                        SigafLinea.Measure = "";//Measure = programa presupuestario
                                                        //SigafLinea.Kursf = 0;//Kursf = tipo de cambio
                                                        SigafLinea.Aufnr = "";//Aufnr = Orden
                                                        SigafLinea.Dmbe2 = 0;//Dmbe2 = importe en moneda fuerte
                                                        //if (Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCuentaContable"]).StartsWith("5") )
                                                        //    SigafLinea.Kblnr = Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim(); //Kblnr = documento presupuestario
                                                        //else
                                                        SigafLinea.Kblnr = ""; //Kblnr = documento presupuestario
                                                        SigafLinea.Kblpos = "";//Kblpos = posicion documento presupuestario
                                                        SigafLinea.Rcomp = "";
                                                        SigafLinea.Buzei = "";
                                                        SigafLinea.Mandt = "";
                                                        SigafLinea.Hbkid = "";//Hbkid = banco propio
                                                        if ((lds_Pagos.Tables["Table1"].Rows[y]["IdMoneda"].ToString().Trim() != "CRC") && (
                                                               ((lds_TiposAsiento.Tables["Table"].Rows[z]["Secuencia"].ToString().Trim()) == "3") //&& dec_DiferenciaPago > 0)
                                                            //|| ((lds_TiposAsiento.Tables["Table"].Rows[z]["Secuencia"].ToString().Trim()) == "2" && dec_DiferenciaPago > 0 && dec_DiferenciaTC > 0)
                                                              ))
                                                        {
                                                            SigafLinea.Xref1Hd = "DIFERENCIA"; //"REF_1";//Xref1Hd = "REF_1"

                                                            SigafTablaAsientoDif[lint_lineaDif] = SigafLinea;
                                                            lint_lineaDif++;
                                                        }
                                                        //if ((lds_TiposAsiento.Tables["Table"].Rows[z]["Secuencia"].ToString().Trim()) != "3" || (lds_Pagos.Tables["Table"].Rows[y]["IdMoneda"].ToString().Trim() == "CRC"))
                                                        else
                                                        {
                                                            SigafLinea.Xref1Hd = "PAGADO"; //"REF_1";//Xref1Hd = "REF_1"

                                                            SigafTablaAsientoPago[lint_lineaPago] = SigafLinea;
                                                            lint_lineaPago++;
                                                        }
                                                        //else
                                                        //{
                                                        //    SigafLinea.Xref1Hd = "DIFERENCIA"; //"REF_1";//Xref1Hd = "REF_1"

                                                        //    SigafTablaAsientoDif[lint_lineaDif] = SigafLinea;
                                                        //    lint_lineaDif++;
                                                        //    //lint_lineaPago++;
                                                        //}

                                                        if (!string.IsNullOrEmpty(Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdClaveContable2"])))
                                                        {
                                                            ///cuenta 2
                                                            SigafLinea = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
                                                            //SigafLinea = new wsAsientos.ZfiAsiento();
                                                            SigafLinea.Blart = "RI";//lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar4"].ToString().Trim();  //"SA"; //BLART = CLASE DE DOCUMENTO
                                                            SigafLinea.Bukrs = "";//"G206";//BUKRS = ID de Sociedad
                                                            SigafLinea.Bldat = "";//"01.10.2015";//bldat = Fecha de Documento
                                                            SigafLinea.Budat = "";//"01.10.2015";//budat = Fecha de Contabilizacion
                                                            SigafLinea.Waers = lds_Pagos.Tables["Table1"].Rows[y]["IdMoneda"].ToString().Trim();//"CRC";//waers = IdMoneda
                                                            if (SigafLinea.Waers.Trim() != "CRC")
                                                                SigafLinea.Kursf = (SigafLinea.Waers.Trim() == "USD") ? Math.Round(ldec_tipo_cambioUSD, 5) : Math.Round(ldec_tipo_cambioEUR * ldec_tipo_cambioUSD, 5);//tipo de cambio 

                                                            SigafLinea.Xblnr = "";//"REF";//Xblnr = Referencia
                                                            SigafLinea.Bktxt = ""; //"Texto_Cabecera";//Bktxt = "Texto_Cabecera"
                                                            SigafLinea.Xref1Hd = ""; //"REF_1";//Xref1Hd = "REF_1"
                                                            SigafLinea.Xref2Hd = ""; //"REF_2";//Xref2Hd = "REF_2"
                                                            SigafLinea.Bschl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdClaveContable2"]).Trim(); //"40";//Bschl = Clave Contable
                                                            SigafLinea.Hkont = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCuentaContable2"]).Trim(); //"5120302000";//Hkont = Cuenta Contable de Mayor
                                                            switch ((lds_TiposAsiento.Tables["Table"].Rows[z]["Secuencia"].ToString().Trim()))
                                                            {
                                                                case "1":
                                                                    SigafLinea.Wrbtr = dec_Pago;//Convert.ToDecimal(lds_Pagos.Tables["Table"].Rows[y]["Monto"]); //1505;//Wrbtr = Monto Importe
                                                                    if (dec_DiferenciaPago > 0)
                                                                        SigafLinea.Wrbtr -= dec_DiferenciaPago;
                                                                    //if (dec_DiferenciaTC < 0 && SigafLinea.Waers.Trim() != "CRC")
                                                                    //    SigafLinea.Kursf = (SigafLinea.Waers.Trim() == "USD") ? Math.Round(ldec_tipo_cambioUSD, 5) : Math.Round(ldec_tipo_cambioEUR * ldec_tipo_cambioUSD, 5);//tipo de cambio                                                
                                                                    break;
                                                                case "2":
                                                                    SigafLinea.Wrbtr = Math.Abs(dec_DiferenciaPago);
                                                                    //if (SigafLinea.Waers.Trim() != "CRC")
                                                                    //    SigafLinea.Kursf = (SigafLinea.Waers.Trim() == "USD") ? Math.Round(ldec_tipo_cambioUSD, 5) : Math.Round(ldec_tipo_cambioEUR * ldec_tipo_cambioUSD, 5);//tipo de cambio 
                                                                    break;
                                                                case "3":
                                                                    SigafLinea.Waers = "CRC";
                                                                    //if (dec_DiferenciaTC < 0)
                                                                    //{
                                                                        if (dec_DiferenciaPago > 0)
                                                                            SigafLinea.Wrbtr = (dec_Pago - dec_DiferenciaPago) * Math.Abs(dec_DiferenciaTC);
                                                                        else
                                                                            SigafLinea.Wrbtr = (dec_Pago) * Math.Abs(dec_DiferenciaTC);
                                                                        //SigafLinea.Kursf = 0;
                                                                        SigafLinea.Kursf = 0;//tipo de cambio 
                                                                    /*}
                                                                    else
                                                                    {
                                                                        if (dec_DiferenciaPago > 0)
                                                                            SigafLinea.Wrbtr = (dec_Pago - dec_DiferenciaPago) * Math.Abs(dec_DiferenciaTC) / ldec_tipo_cambioUSD;
                                                                        else
                                                                            SigafLinea.Wrbtr = (dec_Pago) * Math.Abs(dec_DiferenciaTC) / ((SigafLinea.Waers.Trim() == "USD") ? ldec_tipo_cambioUSD : Math.Round(ldec_tipo_cambioEUR * ldec_tipo_cambioUSD, 5));
                                                                        //SigafLinea.Kursf = 0;
                                                                        SigafLinea.Kursf = (SigafLinea.Waers.Trim() == "USD") ? Math.Round(ldec_tipo_cambioUSD, 5) : Math.Round(ldec_tipo_cambioEUR * ldec_tipo_cambioUSD, 5);//tipo de cambio 
                                                                    }*/
                                                                    break;
                                                                default:
                                                                    break;
                                                            }

                                                            SigafLinea.Wrbtr = Math.Round(SigafLinea.Wrbtr, 2);
                                                            SigafLinea.Zuonr = "";//Zuonr = "Asignacion"
                                                            SigafLinea.Sgtxt = "";//Sgtxt = "Texto"
                                                            if (lbln_Pago && SigafLinea.Bschl.Trim() == "50" && !string.IsNullOrEmpty(Convert.ToString(lds_Pagos.Tables["Table1"].Rows[y]["IdReservaPresupuestaria"]).Trim()))
                                                            {
                                                                lds_ReservasDetallado = lreservasDet.ConsultarReservasDetallado(Convert.ToString(lds_Pagos.Tables["Table1"].Rows[y]["IdReservaPresupuestaria"]).Trim(), "", "", "", "");

                                                                //solo itero si el dataset tiene registros
                                                                if (lds_ReservasDetallado.Tables.Count > 0 && lds_ReservasDetallado.Tables["Table"].Rows.Count > 0)
                                                                {

                                                                    SigafLinea.Fipex = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdPosPre"]).Trim(); //"E-10302";//Fipex = posicion presupuestaria
                                                                    SigafLinea.Kostl = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdCentroCosto"]).Trim(); //"20613200";//Kostl = Centro de Costo
                                                                    SigafLinea.Fistl = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdCentroGestor"]).Trim(); //"20613200";//Fistl = Centro Gestor
                                                                    SigafLinea.Geber = Convert.ToString(lds_ReservasDetallado.Tables["Table"].Rows[0]["IdFondo"]).Trim(); //"001";//Geber = Fondo                                                        }//if de Reservas encontrado
                                                                }
                                                            }
                                                            else
                                                            {
                                                                SigafLinea.Fipex = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdPosPre2"]).Trim(); //"E-10302";//Fipex = posicion presupuestaria
                                                                SigafLinea.Kostl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroCosto2"]).Trim(); //"20613200";//Kostl = Centro de Costo
                                                                SigafLinea.Fistl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroGestor2"]).Trim(); //"20613200";//Fistl = Centro Gestor
                                                                SigafLinea.Geber = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdFondo2"]).Trim(); //"001";//Geber = Fondo
                                                            }
                                                            SigafLinea.Fkber = "";//Fkber = Area Funcional
                                                            SigafLinea.Xref2 = ""; //Xref2 = Flujo de efectivo

                                                            SigafLinea.Valut = "";//Valut = Fecha de Valor                                                  
                                                            SigafLinea.Umskz = "";//Umskz = Indicador CME
                                                            SigafLinea.Werks = "";//Werks = Centro
                                                            SigafLinea.Xmwst = ""; //Calculo de Impuesto
                                                            SigafLinea.Xref3 = "";//Flujo de Efectivo
                                                            SigafLinea.Zfbdt = "";//Fecha base
                                                            SigafLinea.Zlsch = "";//Via de pago
                                                            SigafLinea.Projk = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdElementoPEP2"]).Trim(); ////Projk = Id Elemento PEP
                                                            if (string.IsNullOrEmpty(lstr_IdCeBe) || !SigafLinea.Hkont.StartsWith("4"))
                                                            {
                                                                SigafLinea.Prctr = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroBeneficio2"]).Trim(); ////Prctr = centro de beneficio
                                                            }
                                                            else
                                                            {
                                                                SigafLinea.Prctr = lstr_IdCeBe;
                                                            }
                                                            SigafLinea.Mwskz = "";//Mwskz = indicador de impuesto
                                                            SigafLinea.Measure = "";//Measure = programa presupuestario
                                                            //SigafLinea.Kursf = 0;//Kursf = tipo de cambio
                                                            SigafLinea.Aufnr = "";//Aufnr = Orden
                                                            SigafLinea.Dmbe2 = 0;//Dmbe2 = importe en moneda fuerte
                                                            //if (Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCuentaContable2"]).StartsWith("5"))
                                                            //    SigafLinea.Kblnr = Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim();//Kblnr = documento presupuestario
                                                            //else
                                                            SigafLinea.Kblnr = "";//Kblnr = documento presupuestario
                                                            SigafLinea.Kblpos = "";//Kblpos = posicion documento presupuestario
                                                            SigafLinea.Rcomp = "";
                                                            SigafLinea.Buzei = "";
                                                            SigafLinea.Mandt = "";
                                                            SigafLinea.Hbkid = "";//Hbkid = banco propio


                                                            //if ((lds_TiposAsiento.Tables["Table"].Rows[z]["Secuencia"].ToString().Trim()) != "3" || (lds_Pagos.Tables["Table"].Rows[y]["IdMoneda"].ToString().Trim() == "CRC"))
                                                            //{
                                                            if ((lds_Pagos.Tables["Table1"].Rows[y]["IdMoneda"].ToString().Trim() != "CRC") && (
                                                                   ((lds_TiposAsiento.Tables["Table"].Rows[z]["Secuencia"].ToString().Trim()) == "3") // && dec_DiferenciaPago > 0)
                                                                //|| ((lds_TiposAsiento.Tables["Table"].Rows[z]["Secuencia"].ToString().Trim()) == "2" && dec_DiferenciaPago > 0 && dec_DiferenciaTC > 0)
                                                                  ))
                                                            {
                                                                SigafTablaAsientoDif[lint_lineaDif] = SigafLinea;
                                                                lint_lineaDif++;
                                                                //lint_lineaPago++;
                                                            }
                                                            else
                                                            {
                                                                SigafTablaAsientoPago[lint_lineaPago] = SigafLinea;
                                                                lint_lineaPago++;
                                                            }
                                                            //else
                                                            //{
                                                            //    SigafTablaAsientoDif[lint_lineaDif] = SigafLinea;
                                                            //    lint_lineaDif++;
                                                            //    //lint_lineaPago++;
                                                            //}

                                                        }//if (!string.IsNullOrEmpty(Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdClaveContable2"])))
                                                        //SigafAsientoLogPago = lasiento.EnviarAsientoSigaf(SigafTablaAsientoPago);

                                                    }//if de estado Activo de la linea del asiento
                                                }//for de las lineas del asiento
                                            }//if asiento encontrado
                                            else
                                            {
                                                //Tipo de Asiento no Encontrado
                                                lstr_Mensaje = "Tipo de Asiento no Encontrado " + lstr_IdExpediente + "." + lds_Servicios.Tables["Table"].Rows[0]["IdServicio"].ToString().Trim();

                                                resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora("CT", "1", "Envio Asientos", lstr_Mensaje, "1", "Asiento", lstr_IdSociedadGL);
                                                lbln_ErrorAsientoLinea = true;
                                                resultado[0] = "99";
                                                resultado[1] = lstr_Mensaje;
                                            }//Tipo de Asiento no encontrado
                                            #endregion SiExisteExpediente
                                        }//else
                                        #endregion AsientoContingentes
                                    }//if tiene expediente
                                    else
                                    {
                                        lstr_Mensaje = "No tiene Expediente " + lstr_IdExpediente + "." + lds_Pagos.Tables["Table"].Rows[y]["IdServicio"].ToString().Trim();
                                        resultado[0] = "99";
                                        resultado[1] = lstr_Mensaje;
                                        lbln_ErrorAsientoLinea = true;
                                    }
                                    #endregion TiposAsiento
                                }//if servicio encontrado
                                else
                                {
                                    //Servicio no Encontrado
                                    lstr_Mensaje = "Servicio no Encontrado " + lstr_IdExpediente + "." + lds_Pagos.Tables["Table"].Rows[y]["IdServicio"].ToString().Trim();
                                    resultado[0] = "99";
                                    resultado[1] = lstr_Mensaje;
                                    resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora("CT", "1", "Envio Asientos", lstr_Mensaje, "1", "Servicio", lstr_IdSociedadGL);
                                    lbln_ErrorAsientoLinea = true;
                                }//servicio no encontrado
                                #endregion Servicios
                                y++;
                            }//while de pagos
                            /////////////////////////////////////////

                            if (!lbln_ErrorAsientoLinea)
                            {
                                #region Interfaz

                                try //Interfaz
                                {
                                    lbln_ErrorInterfaz = false;


                                    if (lint_lineaPago > 0)
                                    {
                                        #region PruebaAsientos
                                        logAsientoPago = string.Empty;
                                        //SigafAsientoLogPago = asientos.EnviarAsientos(SigafTablaAsientoPago);
                                        logAsientoPago = this.EnviarAsientoSigaf(SigafTablaAsientoPago, "X");
                                        /*for (int w = 0; w < item_resAsientosLogPago.Length; w++)
                                        {
                                            logAsientoPago += "\n" + i + "-" + item_resAsientosLogPago[w];
                                        }*/
                                        //MessageBox.Show("Resultado de contabilización: \n\n"+logAsientoPago);
                                        Log.Info("Resultado de contabilización Pago: \n\n" + logAsientoPago);
                                        if (logAsientoPago.Contains("[E]")
                                            )
                                        {
                                            lbln_ErrorAsientoLinea = true;
                                            lbln_ErrorInterfaz = true;
                                            resultado[0] = "99";
                                            resultado[1] = "Error al contabilizar, verifique bitácora";
                                        }



                                        if (lint_lineaDif > 0 && !lbln_ErrorInterfaz)
                                        {
                                            //logAsientoPago = string.Empty;
                                            //SigafAsientoLogPago = asientos.EnviarAsientos(SigafTablaAsientoPago);
                                            logAsientoPago += this.EnviarAsientoSigaf(SigafTablaAsientoDif, "X");
                                            /*for (int w = 0; w < item_resAsientosLogPago.Length; w++)
                                            {
                                                logAsientoPago += "\n" + i + "-" + item_resAsientosLogPago[w];
                                            }*/
                                            //MessageBox.Show("Resultado de contabilización: \n\n"+logAsientoPago);
                                            Log.Info("Resultado de contabilización Pago: \n\n" + logAsientoPago);
                                            if (logAsientoPago.Contains("[E]")
                                                )
                                            {
                                                lbln_ErrorAsientoLinea = true;
                                                lbln_ErrorInterfaz = true;
                                                resultado[0] = "99";
                                                resultado[1] = "Error al contabilizar, verifique bitácora";
                                            }

                                        }
                                        #endregion PruebaAsientos
                                        if (!lbln_ErrorInterfaz)
                                        {
                                            logAsientoPago = string.Empty;
                                            //SigafAsientoLogPago = asientos.EnviarAsientos(SigafTablaAsientoPago);
                                            logAsientoPago = this.EnviarAsientoSigaf(SigafTablaAsientoPago);
                                            /*for (int w = 0; w < item_resAsientosLogPago.Length; w++)
                                            {
                                                logAsientoPago += "\n" + i + "-" + item_resAsientosLogPago[w];
                                            }*/
                                            //MessageBox.Show("Resultado de contabilización: \n\n"+logAsientoPago);
                                            Log.Info("Resultado de contabilización Pago: \n\n" + logAsientoPago);
                                            if (logAsientoPago.Contains("[E]")
                                                )
                                            {
                                                lbln_ErrorAsientoLinea = true;
                                                lbln_ErrorInterfaz = true;
                                                resultado[0] = "99";
                                                resultado[1] = "Error al contabilizar, verifique bitácora";
                                            }

                                            if (lint_lineaDif > 0 && !lbln_ErrorInterfaz)
                                            {
                                                //logAsientoPago = string.Empty;
                                                //SigafAsientoLogPago = asientos.EnviarAsientos(SigafTablaAsientoPago);
                                                logAsientoPago += this.EnviarAsientoSigaf(SigafTablaAsientoDif);
                                                /*for (int w = 0; w < item_resAsientosLogPago.Length; w++)
                                                {
                                                    logAsientoPago += "\n" + i + "-" + item_resAsientosLogPago[w];
                                                }*/
                                                //MessageBox.Show("Resultado de contabilización: \n\n"+logAsientoPago);
                                                Log.Info("Resultado de contabilización Pago: \n\n" + logAsientoPago);
                                                if (logAsientoPago.Contains("[E]")
                                                    )
                                                {
                                                    lbln_ErrorAsientoLinea = true;
                                                    lbln_ErrorInterfaz = true;
                                                    resultado[0] = "99";
                                                    resultado[1] = "Error al contabilizar, verifique bitácora";
                                                }

                                            }
                                        }

                                    }

                                }
                                catch (Exception ex)
                                {
                                    lstr_Mensaje = "Error al invocar interfaz asientos Sigaf: " + lstr_IdExpedienteFK + " " + ex.ToString();
                                    Log.Error(lstr_Mensaje);
                                    resultado[0] = "99";
                                    resultado[1] = lstr_Mensaje;
                                    //lbln_ErrorAsientoLinea = true;
                                    lbln_ErrorInterfaz = true;
                                }//catch

                                #endregion Interfaz

                                #region Bitacora
                                lbln_ErrorBitacora = false;
                                //Registro de bitacora de movimientos
                                try
                                {
                                    if (!string.IsNullOrEmpty(logAsientoPago))
                                        lstr_Mensaje = logAsientoPago;
                                    resAsientosLogPago = reg_Bitacora.ufnRegistrarAccionBitacora("CT", "1", "Envio Asientos", lstr_Mensaje, lstr_IdExpedienteFK, "Pago", lstr_IdSociedadGL);

                                }//try registro en bitácora
                                catch (Exception err)
                                {
                                    lstr_Mensaje = "Error al registrar en bitácora Pago: " + lstr_IdExpedienteFK + err.Message;
                                    item_resAsientosLogPago[0] = lstr_Mensaje;
                                    resultado[0] = "99";
                                    resultado[1] = lstr_Mensaje;
                                    lbln_ErrorBitacora = true;
                                    Log.Error(lstr_Mensaje);
                                    resAsientosLogPago = reg_Bitacora.ufnRegistrarAccionBitacora("CT", "1", "Envio Asientos", lstr_Mensaje, lstr_IdExpedienteFK, "Pago", lstr_IdSociedadGL);

                                }//catch
                                #endregion Bitacora
                            }// if no Error Asiento Linea


                            #endregion Expediente
                        }
                        catch (Exception e)
                        {
                            lstr_Mensaje = "Error en procesamiento de pagos " + lstr_IdExpediente + " " + lstr_IdSociedadGL;
                            resultado[0] = "99";
                            resultado[1] = lstr_Mensaje;
                            lbln_ErrorAsientoLinea = true;

                        }
                #endregion Formularios
                return resultado;
            }//try 
            catch (Exception ex)
            {
                lstr_Mensaje = ex.ToString(); //+ lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString();
                Log.Error(lstr_Mensaje);
                //resAsientosLogPago = reg_Bitacora.ufnRegistrarAccionBitacora("CI", lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Envio Asientos", lstr_Mensaje, lds_Formularios.Tables["Table"].Rows[i]["IdFormulario"].ToString().Trim(), "Pago", lds_Formularios.Tables["Table"].Rows[i]["IdSociedadGL"].ToString().Trim());
                resultado[0] = "99";
                resultado[1] = lstr_Mensaje;
                return resultado;
                //Console.WriteLine("Error: " + lstr_Mensaje);
                //Console.ReadLine().ToString();
            }//catch
        }



        public string ConsultaMontoReservaSAP(string str_IdReserva, string str_IdPosicion)
        {
            //wrSigafReserva.ZWS_MONTO_RESERVA servicio = new wrSigafReserva.ZWS_MONTO_RESERVA(); cucurucho
            //wrSigafReserva.zws_zint_conf_monto_reserva servicio = new wrSigafReserva.zws_zint_conf_monto_reserva();
            //wrSigafReserva.ZWS_MONTO_RESERVA servicio = new wrSigafReserva.ZWS_MONTO_RESERVA();
            wrSigafReserva.ZWS_MONTO_RESERVA servicio = new wrSigafReserva.ZWS_MONTO_RESERVA();
            //wrSigafReserva.ZWS_ZINT_CONF_MONTO_RESERVA servicio = new wrSigafReserva.ZWS_ZINT_CONF_MONTO_RESERVA();

            wrSigafReserva.ZintConfMontoReserva metodo = new wrSigafReserva.ZintConfMontoReserva();
            wrSigafReserva.ZintConfMontoReservaResponse response = new wrSigafReserva.ZintConfMontoReservaResponse();


            metodo.IReserva = str_IdReserva;
            metodo.IPosicion = str_IdPosicion;

            //string lstr_user = "rodriguezl";//"rodriguezzl";
            string lstr_user = System.Configuration.ConfigurationManager.AppSettings["USER_SAP"];//usuario
            //string lstr_pass = "Luirodzu2";
            string lstr_pass = System.Configuration.ConfigurationManager.AppSettings["PASS_SAP"];//contrasena

            servicio.Credentials = new System.Net.NetworkCredential(lstr_user, lstr_pass);

            response = servicio.ZintConfMontoReserva(metodo);

            return response.ESaldo.ToString().Trim();
        }

      /*  public bool EnviarAsientoDE(string lstr_sociedad, string lstr_idModulo, string lstr_idOperacion, string lstr_tipoPrestamo, string lstr_tipoDesembolso, string lstr_moneda,
                                    decimal ldec_monto, string lstr_abrevAcreedor, string lstr_Id, DateTime? fchContabilizacion, out string lstr_CodResultado, out string lstr_Mensaje)
      */
        public bool EnviarAsientoDE(string lstr_sociedad, string lstr_idModulo, string lstr_idOperacion, string lstr_tipoPrestamo, string lstr_tipoDesembolso, string lstr_moneda,
                                   decimal ldec_monto, decimal ldec_monto2, decimal ldec_monto3, decimal ldec_monto4, string lstr_abrevAcreedor, string lstr_Id, string lstr_IdPrestamo, DateTime? fchContabilizacion, out string lstr_CodResultado, out string lstr_Mensaje, out string lstr_codAsiento)         
        
            {
            // variables locales
            bool bool_enviado = true;
            clsTiposAsiento tiposAsiento = new clsTiposAsiento();

            lstr_codAsiento = String.Empty;
            string str_IdModulo = "DE";
            lstr_CodResultado = "00";
            lstr_Mensaje = "Asiento Enviado";
            decimal ldec_montoCRC = 0;
            decimal ldec_montoUSD = 0;
            decimal ldec_montoEUR = 0;
            decimal ldec_montoCRC2 = 0;
            decimal ldec_montoUSD2 = 0;
            decimal ldec_montoEUR2 = 0;
            decimal ldec_montoCRC3 = 0;
            decimal ldec_montoUSD3 = 0;
            decimal ldec_montoEUR3 = 0;
            decimal ldec_montoCRC4 = 0;
            decimal ldec_montoUSD4 = 0;
            decimal ldec_montoEUR4 = 0;
            decimal ldec_montoTemp = 0;
            decimal ldec_montoTemp2 = 0;
            decimal ldec_montoTemp3 = 0;
            decimal ldec_montoTemp4 = 0;
            clsOpcionesCatalogo oc = new clsOpcionesCatalogo();
            DataSet ds_opciones = new DataSet();
            clsOperaciones loperacion = new clsOperaciones();
            DataSet lds_Operaciones = new DataSet();
            string lstr_PosPreReserva = string.Empty;
            clsReservasDetalle lreservasDet = new clsReservasDetalle();
            DataSet lds_ReservasDetallado = new DataSet();
            DateTime fechaContabilizacion = (fchContabilizacion == null) ? DateTime.Now : Convert.ToDateTime(fchContabilizacion);
            decimal ldec_monto_max = 9999999999999999999;
            //string lstr_Cta_Porcentaje1 = "1234905011";
            //string lstr_Cta_Porcentaje2 = "1275305000";
            string lstr_TransaccionVentaUSD = "3140";
            string lstr_TransaccionCompraUSD = "3280";
            //decimal ldec_POR_DESEMB3_CTA1 = 0;
            //decimal ldec_POR_DESEMB3_CTA2 = 0;
            //Monto maximo que se puede enviar a SAP en un asiento
            DataSet ds_Parametro = lparametro.ConsultarParametros("MTO_MAX_ASIENTO", "IdModulo = 'MA'", fechaContabilizacion, "", "");
            if (ds_Parametro.Tables.Count > 0 && ds_Parametro.Tables["Table"].Rows.Count > 0)
            {
                ldec_monto_max = Convert.ToDecimal(ds_Parametro.Tables["Table"].Rows[0]["Valor"]);
            }//if ds_Parametro

            decimal ldec_Total40 = 0; 
            decimal ldec_Total50 = 0;
            decimal ldec_Diferencia40y50 = 0;

            string lstr_NomOperacion = string.Empty;
            string lstr_ClaseDoc = string.Empty;
            //variables de proceso de asiento
            string[] item_resAsientosLog = new string[100];
            string logAsiento = string.Empty;
            string str_AbrevTipoDesembolso = string.Empty;
            string lstr_MontoReserva = string.Empty;
            string lstr_MontoReservaCRC = string.Empty;
            string lstr_MontoReservaUSD = string.Empty;
            string lstr_MontoReservaEUR = string.Empty;

            decimal ldec_MontoMAXReservaCRC = 0;
            decimal ldec_MontoMAXReservaUSD = 0;
            decimal ldec_MontoMAXReservaEUR = 0;
            decimal ldec_MontoReservaCRC = 0;
            decimal ldec_MontoReservaUSD = 0;
            decimal ldec_MontoReservaEUR = 0;
            decimal ldec_MontoMAXReservaCRC2 = 0;
            decimal ldec_MontoMAXReservaUSD2 = 0;
            decimal ldec_MontoMAXReservaEUR2 = 0;
            decimal ldec_MontoReservaCRC2 = 0;
            decimal ldec_MontoReservaUSD2 = 0;
            decimal ldec_MontoReservaEUR2 = 0;
            decimal ldec_MontoMAXReservaCRC3 = 0;
            decimal ldec_MontoMAXReservaUSD3 = 0;
            decimal ldec_MontoMAXReservaEUR3 = 0;
            decimal ldec_MontoReservaCRC3 = 0;
            decimal ldec_MontoReservaUSD3 = 0;
            decimal ldec_MontoReservaEUR3 = 0;
            decimal ldec_MontoMAXReservaCRC4 = 0;
            decimal ldec_MontoMAXReservaUSD4 = 0;
            decimal ldec_MontoMAXReservaEUR4 = 0;
            decimal ldec_MontoReservaCRC4 = 0;
            decimal ldec_MontoReservaUSD4 = 0;
            decimal ldec_MontoReservaEUR4 = 0;
            DataTable ldt_DatosReserva = new DataTable();

            ldt_DatosReserva.Columns.Add("IdReserva");
            ldt_DatosReserva.Columns.Add("OrdenDeudaExterna");
            ldt_DatosReserva.Columns.Add("IdPosPre");
            ldt_DatosReserva.Columns.Add("Posicion");
            ldt_DatosReserva.Columns.Add("MontoCRC");
            ldt_DatosReserva.Columns.Add("IdMoneda");
            ldt_DatosReserva.Columns.Add("MontoUSD");
            ldt_DatosReserva.Columns.Add("MontoEUR");
            Boolean lbln_ConReserva = false;

            decimal ldec_monto_enviado = 0;

            decimal ldec_monto_enviado_total = 0;

            decimal ldec_monto_falta = ldec_monto;
            decimal ldec_monto_falta_Reserva = ldec_monto;
            decimal ldec_monto_falta_total = ldec_monto;
            decimal ldec_monto_falta2 = ldec_monto2;
            decimal ldec_monto_falta_Reserva2 = ldec_monto2;
            decimal ldec_monto_falta_total2 = ldec_monto2;
            decimal ldec_monto_falta3 = ldec_monto3;
            decimal ldec_monto_falta_Reserva3 = ldec_monto3;
            decimal ldec_monto_falta_total3 = ldec_monto3;
            decimal ldec_monto_falta4 = ldec_monto4;
            decimal ldec_monto_falta_Reserva4 = ldec_monto4;
            decimal ldec_monto_falta_total4 = ldec_monto4;

            decimal ldec_monto_max_moneda = ldec_monto_max;
            decimal ldec_tipo_cambioUSD = 0;
            decimal ldec_tipo_cambioEUR = 0;
            if (lstr_moneda.Trim() != "CRC")//puede venir en CRC, EUR o USD solamente, todas las demas monedas se pasan a USD
            {
                string str_MonedaQry1 = "CRCN";
                DataSet ds_tipoCambio = new DataSet();
                if ((lstr_idOperacion == "RECLA CXC"))
                    ds_tipoCambio = tiposCambio.ConsultarTiposCambio(str_MonedaQry1, fechaContabilizacion, lstr_TransaccionCompraUSD, "N");
                else
                    ds_tipoCambio = tiposCambio.ConsultarTiposCambio(str_MonedaQry1, fechaContabilizacion, lstr_TransaccionVentaUSD, "N");
                if (ds_tipoCambio.Tables.Count > 0 && ds_tipoCambio.Tables["Table"].Rows.Count > 0)
                {
                    // se realiza el cambio a dolares para procesar el asiento
                    ldec_tipo_cambioUSD = Convert.ToDecimal(ds_tipoCambio.Tables["Table"].Rows[0]["Valor"]);

                    ldec_monto_max_moneda = ldec_monto_max / ldec_tipo_cambioUSD;
                }//if ds_tipoCambio
                if (lstr_moneda != "USD")//MONTO max está en USD y se debe pasar a la otra moneda
                {
                    ds_tipoCambio = tiposCambio.ConsultarTiposCambio(lstr_moneda, fechaContabilizacion, string.Empty, "N");
                    if (ds_tipoCambio.Tables.Count > 0 && ds_tipoCambio.Tables["Table"].Rows.Count > 0)
                    {
                        // se realiza el cambio a dolares para procesar el asiento
                        ldec_tipo_cambioEUR = Convert.ToDecimal(ds_tipoCambio.Tables["Table"].Rows[0]["Valor"]);

                        ldec_monto_max_moneda = ldec_monto_max_moneda / ldec_tipo_cambioEUR;
                    }//if ds_tipoCambio
                }
            }//if lstr_moneda

            switch (lstr_moneda.Trim())
            {
                case "CRC":
                    ldec_montoCRC = ldec_monto;
                    ldec_montoUSD = ldec_montoCRC / ldec_tipo_cambioUSD;
                    ldec_montoCRC2 = ldec_monto2;
                    ldec_montoUSD2 = ldec_montoCRC2 / ldec_tipo_cambioUSD;
                    ldec_montoCRC3 = ldec_monto3;
                    ldec_montoUSD3 = ldec_montoCRC3 / ldec_tipo_cambioUSD;
                    ldec_montoCRC4 = ldec_monto4;
                    ldec_montoUSD4 = ldec_montoCRC4 / ldec_tipo_cambioUSD;
                    if (ldec_tipo_cambioEUR != 0)//MONTO está en USD y se debe pasar a la otra moneda
                    {
                        ldec_montoEUR = ldec_montoUSD / ldec_tipo_cambioEUR;
                        ldec_montoEUR2 = ldec_montoUSD2 / ldec_tipo_cambioEUR;
                        ldec_montoEUR3 = ldec_montoUSD3 / ldec_tipo_cambioEUR;
                        ldec_montoEUR4 = ldec_montoUSD4 / ldec_tipo_cambioEUR;
                    }
                    break;
                case "USD":
                    ldec_montoUSD = ldec_monto;
                    ldec_montoCRC = ldec_montoUSD * ldec_tipo_cambioUSD;
                    ldec_montoUSD2 = ldec_monto2;
                    ldec_montoCRC2 = ldec_montoUSD2 * ldec_tipo_cambioUSD;
                    ldec_montoUSD3 = ldec_monto3;
                    ldec_montoCRC3 = ldec_montoUSD3 * ldec_tipo_cambioUSD;
                    ldec_montoUSD4 = ldec_monto4;
                    ldec_montoCRC4 = ldec_montoUSD4 * ldec_tipo_cambioUSD;
                    if (ldec_tipo_cambioEUR != 0)//MONTO está en USD y se debe pasar a la otra moneda
                    {
                        ldec_montoEUR = ldec_montoUSD / ldec_tipo_cambioEUR;
                        ldec_montoEUR2 = ldec_montoUSD2 / ldec_tipo_cambioEUR;
                        ldec_montoEUR3 = ldec_montoUSD3 / ldec_tipo_cambioEUR;
                        ldec_montoEUR4 = ldec_montoUSD4 / ldec_tipo_cambioEUR;
                    }
                    break;
                case "EUR":
                    ldec_montoEUR = ldec_monto;
                    ldec_montoUSD = ldec_montoEUR * ldec_tipo_cambioEUR;
                    ldec_montoCRC = ldec_montoUSD * ldec_tipo_cambioUSD;
                    ldec_montoEUR2 = ldec_monto2;
                    ldec_montoUSD2 = ldec_montoEUR2 * ldec_tipo_cambioEUR;
                    ldec_montoCRC2 = ldec_montoUSD2 * ldec_tipo_cambioUSD;
                    ldec_montoEUR3 = ldec_monto3;
                    ldec_montoUSD3 = ldec_montoEUR3 * ldec_tipo_cambioEUR;
                    ldec_montoCRC3 = ldec_montoUSD3 * ldec_tipo_cambioUSD;
                    ldec_montoEUR4 = ldec_monto4;
                    ldec_montoUSD4 = ldec_montoEUR4 * ldec_tipo_cambioEUR;
                    ldec_montoCRC4 = ldec_montoUSD4 * ldec_tipo_cambioUSD;

                    break;
                default:
                    break;
            }//SWITCH

            lds_Operaciones = loperacion.ConsultarOperaciones(lstr_idOperacion, lstr_idModulo, "");

            if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
            {
                lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();

                lstr_ClaseDoc = lds_Operaciones.Tables["Table"].Rows[0]["IdClaseDoc"].ToString().Trim();

                ds_opciones = oc.ConsultarOpcionesCatalogo(null, "TposDesembolso", null, lstr_tipoDesembolso);
                if (ds_opciones.Tables.Count > 0 && ds_opciones.Tables["Table"].Rows.Count > 0)
                {
                    str_AbrevTipoDesembolso = ds_opciones.Tables["Table"].Rows[0]["ValOpcion"].ToString().Trim();
                }

                string str_abrevAcreedor = (lstr_tipoPrestamo.Trim() == "4") ? "TITULOS" : lstr_abrevAcreedor;
                // se obtienen las tiras del asiento y se itera sobre ellas
                DataSet tiposA = new DataSet();
                DataSet tiposA2 = new DataSet();
                //tiposA2 = tiposAsiento.ConsultarTiposAsiento(lstr_sociedad, lstr_idModulo, lstr_idOperacion, null, null, lstr_moneda, "*", lstr_tipoPrestamo, lstr_tipoDesembolso);
                //if (tiposA2.Tables.Count > 0 && tiposA2.Tables["Table"].Rows.Count == 0)
                //{
                tiposA2 = tiposAsiento.ConsultarTiposAsiento(lstr_sociedad, lstr_idModulo, lstr_idOperacion, null, null, lstr_moneda, "*", lstr_tipoPrestamo, null);
                //}// if tiposA                
                //tiposA = tiposAsiento.ConsultarTiposAsiento(lstr_sociedad, lstr_idModulo, lstr_idOperacion, null, null, lstr_moneda, str_abrevAcreedor, lstr_tipoPrestamo, lstr_tipoDesembolso);
                //if (tiposA.Tables.Count > 0 && tiposA.Tables["Table"].Rows.Count == 0)
                //{
                switch (lstr_idOperacion)
                {
                    case "DEVENGO+":
                    case "DEVENGO-":
                    case "DEVENGO*":
                        lstr_tipoPrestamo = "";
                        break;
                    default:
                        break;
                }

                //lstr_tipoPrestamo = (lstr_tipoPrestamo.Trim() == "4") ? "" : lstr_tipoPrestamo.Trim();
                if (lstr_idOperacion!="RECLA CXC")
                    tiposA = tiposAsiento.ConsultarTiposAsiento(lstr_sociedad, lstr_idModulo, lstr_idOperacion, null, null, lstr_moneda, str_abrevAcreedor, lstr_tipoPrestamo, null);
                else
                    tiposA = tiposAsiento.ConsultarTiposAsiento(lstr_sociedad, lstr_idModulo, lstr_idOperacion, null, null, lstr_moneda, str_abrevAcreedor, null, lstr_IdPrestamo);
                //}// if tiposA
                tiposA.Merge(tiposA2);
                DataTable tbl_Asiento = tiposA.Tables[0];
                if (tbl_Asiento.Rows.Count > 0)
                {
                    try
                    {
                        DataTable dt_tiras = tiposA.Tables[0];

                        // se obtiene la cantidad de líneas que componen este asiento
                        int cantidad_registros = tiposA.Tables[0].Rows.Count * 20;

                        //Coleccion de asientos y tipos de asientos requeridos en SAP expuestos por la referencia del servicio
                        LogicaNegocio.wrSigafAsientos.ZfiAsiento[] tabla_asientos2 = new LogicaNegocio.wrSigafAsientos.ZfiAsiento[cantidad_registros];
                        LogicaNegocio.wrSigafAsientos.ZfiAsiento item_asiento = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
                        //wsAsientos.ZfiAsiento item_asiento = new wsAsientos.ZfiAsiento();
                        //LogicaNegocio.wrSigafAsientos.ZfiAsiento item_asiento = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();

                        bool encabezadoEnviado = false;

                        int i = 0;

                        ldec_Total40 = 0;
                        ldec_Total50 = 0;
                        ldec_Diferencia40y50 = 0;
                        foreach (DataRow dr_asiento in dt_tiras.Rows)
                        {
                            lbln_ConReserva = false;
                            lstr_PosPreReserva = dr_asiento["IdPosPre"].ToString().Trim();
                            if (string.IsNullOrEmpty(dr_asiento["CodigoAuxiliar5"].ToString().Trim()))
                                dr_asiento["CodigoAuxiliar5"] = "100";

                            if (string.IsNullOrEmpty(dr_asiento["CodigoAuxiliar6"].ToString().Trim()))
                                dr_asiento["CodigoAuxiliar6"] = "100";

                            if (lstr_PosPreReserva.StartsWith("E"))
                            {

                                lbln_ConReserva = true;
                                #region Reservas
                                //Consulto solo las reservas de Externa para la PosPre
                                lds_ReservasDetallado = lreservasDet.ConsultarReservasDetallado("", "", "", "", "", "", "", "", "", dr_asiento["IdPosPre"].ToString().Trim(), "S", "", "", "");

                                //solo itero si el dataset tiene registros
                                if (lds_ReservasDetallado.Tables.Count > 0)//&& lds_ReservasDetallado.Tables["Table"].Rows.Count > 0)
                                {

                                    DataTable dt_Reservas = lds_ReservasDetallado.Tables[0];

                                    int z = 0;
                                    ldt_DatosReserva.Rows.Clear();
                                    foreach (DataRow dr_Reserva in dt_Reservas.Rows)
                                    {
                                        try
                                        {
                                            lstr_MontoReserva = this.ConsultaMontoReservaSAP(dr_Reserva["IdReserva"].ToString().Trim(), dr_Reserva["Posicion"].ToString().Trim());
                                        }
                                        catch (Exception ex)
                                        {
                                            lstr_MontoReserva = "0";//dr_Reserva["Monto"].ToString();
                                        }
                                        lstr_MontoReservaCRC = lstr_MontoReserva;

                                        switch (dr_Reserva["IdMoneda"].ToString().Trim())
                                        {
                                            case "CRC":
                                                ldec_MontoReservaCRC = Convert.ToDecimal(lstr_MontoReserva.Replace(",", ""));
                                                ldec_MontoReservaUSD = ldec_MontoReservaCRC / ldec_tipo_cambioUSD;
                                                if (ldec_tipo_cambioEUR != 0)//MONTO está en USD y se debe pasar a la otra moneda
                                                {
                                                    ldec_MontoReservaEUR = ldec_MontoReservaUSD / ldec_tipo_cambioEUR;
                                                }
                                                break;
                                            case "USD":
                                                ldec_MontoReservaUSD = Convert.ToDecimal(lstr_MontoReserva.Replace(",", ""));
                                                ldec_MontoReservaCRC = ldec_MontoReservaUSD * ldec_tipo_cambioUSD;
                                                if (ldec_tipo_cambioEUR != 0)//MONTO está en USD y se debe pasar a la otra moneda
                                                {
                                                    ldec_MontoReservaEUR = ldec_MontoReservaUSD / ldec_tipo_cambioEUR;
                                                }
                                                break;
                                            case "EUR":
                                                ldec_MontoReservaEUR = Convert.ToDecimal(lstr_MontoReserva.Replace(",", ""));
                                                ldec_MontoReservaUSD = ldec_MontoReservaEUR * ldec_tipo_cambioEUR;
                                                ldec_MontoReservaCRC = ldec_MontoReservaUSD * ldec_tipo_cambioUSD;

                                                break;
                                            default:
                                                break;
                                        }//SWITCH
                                        if (Convert.ToDecimal(lstr_MontoReserva) != 0)
                                        {
                                            ldt_DatosReserva.Rows.Add(
                                                    dr_Reserva["IdReserva"].ToString(),
                                                    dr_Reserva["OrdenDeudaExterna"].ToString(),
                                                    dr_Reserva["IdPosPre"].ToString(),
                                                    dr_Reserva["Posicion"].ToString(),
                                                    ldec_MontoReservaCRC.ToString(),
                                                    dr_Reserva["IdMoneda"].ToString(),
                                                    ldec_MontoReservaUSD.ToString(),
                                                    ldec_MontoReservaEUR.ToString());
                                            ldec_MontoMAXReservaCRC += ldec_MontoReservaCRC;
                                            ldec_MontoMAXReservaUSD += ldec_MontoReservaUSD;
                                            ldec_MontoMAXReservaEUR += ldec_MontoReservaEUR;

                                        }
                                    }//FOREACH

                                }// if(lds_ReservasDetallado.Tables.Count > 0)
                                else
                                {
                                    lstr_CodResultado = "01";
                                    lstr_Mensaje = "No se Encontraron Reservas" + lstr_PosPreReserva;

                                    //Log.Info(lstr_Mensaje);
                                    bool_enviado = false;
                                    resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora(str_IdModulo, "1", lstr_idOperacion + "|" + lstr_abrevAcreedor, "Resultado de Contabilización " + lstr_NomOperacion + " " + lstr_Id + ": " + lstr_Mensaje);
                                    break;

                                }//else Tablas ReservasDetallado
                                #endregion Reservas

                            }// if(dr_asiento["IdPosPre"].ToString().Trim().StartsWith("E"))
                            else
                            {
                                ldt_DatosReserva.Rows.Clear();

                                ldt_DatosReserva.Rows.Add(
                                    "*",
                                    "1",
                                    lstr_PosPreReserva,
                                    "1",
                                    ldec_montoCRC.ToString(),
                                    lstr_moneda,
                                    ldec_montoUSD.ToString(),
                                    ldec_montoEUR.ToString());
                            }

                            if (!lbln_ConReserva || ((lstr_moneda == "CRC" && (ldec_montoCRC <= ldec_MontoMAXReservaCRC) && (ldec_montoCRC2 <= ldec_MontoMAXReservaCRC) && (ldec_montoCRC3 <= ldec_MontoMAXReservaCRC) && (ldec_montoCRC4 <= ldec_MontoMAXReservaCRC))
                                                    || (lstr_moneda == "USD" && (ldec_montoUSD <= ldec_MontoMAXReservaUSD) && (ldec_montoUSD2 <= ldec_MontoMAXReservaUSD) && (ldec_montoUSD3 <= ldec_MontoMAXReservaUSD) && (ldec_montoUSD4 <= ldec_MontoMAXReservaUSD))
                                                    || (lstr_moneda == "EUR" && (ldec_montoEUR <= ldec_MontoMAXReservaEUR) && (ldec_montoEUR2 <= ldec_MontoMAXReservaEUR) && (ldec_montoEUR3 <= ldec_MontoMAXReservaEUR) && (ldec_montoEUR4 <= ldec_MontoMAXReservaEUR))
                                                    ))
                            {
                                ldec_monto_enviado_total = 0;
                                ldec_monto_falta_total = ldec_monto;
                                foreach (DataRow dr_Reserva in ldt_DatosReserva.Rows)
                                {
                                    switch (lstr_moneda.Trim())
                                    {
                                        case "CRC":
                                            ldec_montoTemp = Convert.ToDecimal(dr_Reserva["MontoCRC"].ToString());
                                            if (ldec_montoTemp >= ldec_monto_falta_total)
                                            {
                                                ldec_monto_enviado_total = ldec_monto_falta_total;//todo lo que falta cabe en la reserva 

                                            }
                                            else
                                            {
                                                ldec_monto_enviado_total = Convert.ToDecimal(dr_Reserva["MontoCRC"].ToString());//hay que gastar la reserva porque no alcanza

                                            }
                                            break;
                                        case "USD":
                                            ldec_montoTemp = Convert.ToDecimal(dr_Reserva["MontoUSD"].ToString());
                                            if (ldec_montoTemp >= ldec_monto_falta_total)
                                            {
                                                ldec_monto_enviado_total = ldec_monto_falta_total;//todo lo que falta cabe en la reserva 

                                            }
                                            else
                                            {
                                                ldec_monto_enviado_total = Convert.ToDecimal(dr_Reserva["MontoUSD"].ToString());//hay que gastar la reserva porque no alcanza

                                            }
                                            break;
                                        case "EUR":
                                            ldec_montoTemp = Convert.ToDecimal(dr_Reserva["MontoEUR"].ToString());
                                            if (ldec_montoTemp >= ldec_monto_falta_total)
                                            {
                                                ldec_monto_enviado_total = ldec_monto_falta_total;//todo lo que falta cabe en la reserva 

                                            }
                                            else
                                            {
                                                ldec_monto_enviado_total = Convert.ToDecimal(dr_Reserva["MontoEUR"].ToString());//hay que gastar la reserva porque no alcanza

                                            }
                                            break;
                                        default:
                                            break;

                                    }//switch moneda


                                    ldec_monto_enviado_total = Math.Round(ldec_monto_enviado_total, 2);
                                    ldec_monto_falta_total -= ldec_monto_enviado_total;


                                    if ((lstr_idOperacion == "RECLA CXC") || (string.IsNullOrEmpty(dr_asiento["CodigoAuxiliar4"].ToString().Trim()) || dr_asiento["CodigoAuxiliar4"].ToString().Trim().Contains(str_AbrevTipoDesembolso)))
                                    {


                                        ldec_monto_enviado = 0;
                                        ldec_monto_falta = ldec_monto_enviado_total;
                                        // se itera sobre las tiras que componen el asiento y se construye el arreglo a enviar a SIGAF
                                        //for (int i = 0; i < cantidad_registros; i++)
                                        while (ldec_monto_enviado < ldec_monto_enviado_total && ldec_monto_falta > 0)
                                        {
                                            ldec_monto_enviado = (ldec_monto_falta <= ldec_monto_max_moneda) ? ldec_monto_falta : ldec_monto_max_moneda;
                                            ldec_monto_enviado = Math.Round(ldec_monto_enviado, 2);
                                            ldec_monto_falta -= ldec_monto_enviado;
                                            //dr_asiento = dt_tiras.Rows[0];
                                            item_asiento = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
                                            if (!string.IsNullOrEmpty(dr_asiento["IdClaveContable"].ToString().Trim()))
                                            {
                                                ///*************************************************** Encabezado (solo se contruye una vez) *****************************************************/
                                                if (i == 0 && !encabezadoEnviado)
                                                {
                                                    item_asiento.Blart = lstr_ClaseDoc;//dr_asiento["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
                                                    item_asiento.Bukrs = lstr_sociedad;//Sociedad
                                                    item_asiento.Bldat = fechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
                                                    item_asiento.Budat = fechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización

                                                    encabezadoEnviado = true;
                                                }// if i==0
                                                ///***************************************************Cargar cuenta 40 HABER*****************************************************/
                                                item_asiento.Waers = lstr_moneda;//Moneda 
                                                if (lstr_moneda.Trim() != "CRC")
                                                    item_asiento.Kursf = (lstr_moneda.Trim() == "USD") ? Math.Round(ldec_tipo_cambioUSD,5) : Math.Round(ldec_tipo_cambioEUR * ldec_tipo_cambioUSD, 5);//tipo de cambio 
                                                item_asiento.Sgtxt = lstr_idOperacion + " " +lstr_abrevAcreedor + " " + lstr_Id;//char 50
                                                item_asiento.Xblnr = lstr_Id;
                                                item_asiento.Bktxt = lstr_abrevAcreedor;// + " " + lstr_Id;
                                                item_asiento.Xref1Hd = lstr_tipoPrestamo;//numero expediente 
                                                item_asiento.Xref2Hd = lstr_idOperacion;// +" " + lstr_Id;//CT01-AG operacion+codigoprocesal expediente
                                                item_asiento.Bschl = dr_asiento["IdClaveContable"].ToString().Trim();//Clave de contabilización
                                                item_asiento.Hkont = dr_asiento["IdCuentaContable"].ToString().Trim();//Cuenta de mayor
                                                switch (lstr_idOperacion) 
                                                {
                                                    case "DEVENGO+":                                                        
                                                    case "DEVENGO-":
                                                        if (dr_asiento["Secuencia"].ToString().Trim() == "1")
                                                        {
                                                            if (item_asiento.Bschl == "40")
                                                            {
                                                                item_asiento.Wrbtr = (ldec_monto) * (Convert.ToDecimal(dr_asiento["CodigoAuxiliar5"].ToString().Trim().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal)) / 100);//Importe o monto en colones a contabilizar 
                                                            }
                                                            else
                                                            {
                                                                item_asiento.Wrbtr = (ldec_monto3) * (Convert.ToDecimal(dr_asiento["CodigoAuxiliar5"].ToString().Trim().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal)) / 100);//Importe o monto en colones a contabilizar 

                                                            }
                                                        }
                                                        else
                                                        {
                                                            item_asiento.Wrbtr = (ldec_monto4) * (Convert.ToDecimal(dr_asiento["CodigoAuxiliar5"].ToString().Trim().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal)) / 100);//Importe o monto en colones a contabilizar 
                                                        }
                                                        break;
                                                    case "DEVENGO*":
                                                        if (dr_asiento["Secuencia"].ToString().Trim() == "1")
                                                        {
                                                            if (item_asiento.Bschl == "40")
                                                            {
                                                                item_asiento.Wrbtr = (ldec_monto) * (Convert.ToDecimal(dr_asiento["CodigoAuxiliar5"].ToString().Trim().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal)) / 100);//Importe o monto en colones a contabilizar 
                                                            }
                                                            else
                                                            {
                                                                item_asiento.Wrbtr = (ldec_monto4) * (Convert.ToDecimal(dr_asiento["CodigoAuxiliar5"].ToString().Trim().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal)) / 100);//Importe o monto en colones a contabilizar 
                                                                
                                                            }
                                                        }
                                                        else
                                                        {
                                                            item_asiento.Wrbtr = (ldec_monto3) * (Convert.ToDecimal(dr_asiento["CodigoAuxiliar5"].ToString().Trim().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal)) / 100);//Importe o monto en colones a contabilizar 
                                                        }
                                                        break;
                                                    case "COM DESC":
                                                        if (dr_asiento["Secuencia"].ToString().Trim() == "1" )//desembolso neto
                                                            item_asiento.Wrbtr = (ldec_monto_enviado - ldec_monto2) * (Convert.ToDecimal(dr_asiento["CodigoAuxiliar5"].ToString().Trim().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal)) / 100);//Importe o monto en colones a contabilizar 
                                                        else//comision
                                                            item_asiento.Wrbtr = (ldec_monto2) * (Convert.ToDecimal(dr_asiento["CodigoAuxiliar5"].ToString().Trim().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal)) / 100);//Importe o monto en colones a contabilizar 
                                                    break;
                                                        
                                                    default:
                                                    item_asiento.Wrbtr = ldec_monto_enviado * (Convert.ToDecimal(dr_asiento["CodigoAuxiliar5"].ToString().Trim().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal)) / 100);//Importe o monto en colones a contabilizar 
                                                    break;
                                                }
                                                
                                                item_asiento.Wrbtr = Math.Round(item_asiento.Wrbtr, 2);
                                                if (item_asiento.Bschl == "40")
                                                    ldec_Total40 += item_asiento.Wrbtr;
                                                else
                                                    ldec_Total50 += item_asiento.Wrbtr;
                    
                                                item_asiento.Fipex = dr_asiento["IdPosPre"].ToString().Trim();//Posición presupuestaria
                                                item_asiento.Kostl = dr_asiento["IdCentroCosto"].ToString().Trim();//;
                                                item_asiento.Fistl = dr_asiento["IdCentroGestor"].ToString().Trim();//
                                                item_asiento.Geber = dr_asiento["IdFondo"].ToString().Trim();//Fondo
                                                item_asiento.Projk = dr_asiento["IdElementoPEP"].ToString().Trim();//
                                                item_asiento.Prctr = dr_asiento["IdCentroBeneficio"].ToString().Trim();//ldat_TiposAsientos.Rows[i]["IdCentroBeneficio"].ToString();
                                                if (dr_Reserva["IdReserva"].ToString().Trim() == "*" || dr_asiento["IdClaveContable"].ToString().Trim() != "40")
                                                {
                                                    item_asiento.Kblnr = string.Empty;
                                                    item_asiento.Kblpos = string.Empty;
                                                }
                                                else
                                                {
                                                    item_asiento.Kblnr = dr_Reserva["IdReserva"].ToString().Trim();
                                                    item_asiento.Kblpos = dr_Reserva["Posicion"].ToString().Trim();
                                                }


                                                tabla_asientos2[i] = item_asiento;
                                                i++;
                                            }// if !string.IsNullOrEmpty(dr_asiento["IdClaveContable"].ToString().Trim()
                                            item_asiento = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
                                            if (!string.IsNullOrEmpty(dr_asiento["IdClaveContable2"].ToString().Trim()))
                                            {
                                                ///*************************************************** Encabezado (solo se contruye una vez) *****************************************************/
                                                if (i == 0 && !encabezadoEnviado)
                                                {
                                                    item_asiento.Blart = lstr_ClaseDoc;//dr_asiento["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
                                                    item_asiento.Bukrs = lstr_sociedad;//Sociedad
                                                    item_asiento.Bldat = fechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
                                                    item_asiento.Budat = fechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización

                                                    encabezadoEnviado = true;
                                                }//if (i == 0 && !encabezadoEnviado)
                                                ///***************************************************Cargar cuenta 50 DEBE*****************************************************/
                                                item_asiento.Waers = lstr_moneda;//Moneda  
                                                if (lstr_moneda.Trim() != "CRC")
                                                    item_asiento.Kursf = (lstr_moneda.Trim() == "USD") ? Math.Round(ldec_tipo_cambioUSD, 5) : Math.Round(ldec_tipo_cambioEUR * ldec_tipo_cambioUSD, 5);//tipo de cambio 
                                                item_asiento.Sgtxt = lstr_idOperacion + " " + lstr_abrevAcreedor + " " + lstr_Id;//char 50
                                                item_asiento.Xblnr = lstr_Id;
                                                item_asiento.Bktxt = lstr_abrevAcreedor;// + " " + lstr_Id;
                                                item_asiento.Xref1Hd = lstr_tipoPrestamo;//numero expediente 
                                                item_asiento.Xref2Hd = lstr_idOperacion;// +" " + lstr_Id;//CT01-AG operacion+codigoprocesal expediente
                                                item_asiento.Bschl = dr_asiento["IdClaveContable2"].ToString().Trim();//Clave de contabilización
                                                item_asiento.Hkont = dr_asiento["IdCuentaContable2"].ToString().Trim();//Cuenta de mayor

                                                switch (lstr_idOperacion)
                                                {
                                                    case "DEVENGO+":
                                                    case "DEVENGO-":
                                                        if (dr_asiento["Secuencia"].ToString().Trim() == "1")
                                                        {
                                                            if (item_asiento.Bschl == "40")
                                                            {
                                                                item_asiento.Wrbtr = (ldec_monto) * (Convert.ToDecimal(dr_asiento["CodigoAuxiliar5"].ToString().Trim().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal)) / 100);//Importe o monto en colones a contabilizar 
                                                            }
                                                            else
                                                            {
                                                                item_asiento.Wrbtr = (ldec_monto3) * (Convert.ToDecimal(dr_asiento["CodigoAuxiliar5"].ToString().Trim().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal)) / 100);//Importe o monto en colones a contabilizar 

                                                            }
                                                        }
                                                        else
                                                        {
                                                            item_asiento.Wrbtr = (ldec_monto4) * (Convert.ToDecimal(dr_asiento["CodigoAuxiliar5"].ToString().Trim().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal)) / 100);//Importe o monto en colones a contabilizar 
                                                        }
                                                        break;
                                                    case "DEVENGO*":
                                                        if (dr_asiento["Secuencia"].ToString().Trim() == "1")
                                                        {
                                                            if (item_asiento.Bschl == "40")
                                                            {
                                                                item_asiento.Wrbtr = (ldec_monto) * (Convert.ToDecimal(dr_asiento["CodigoAuxiliar5"].ToString().Trim().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal)) / 100);//Importe o monto en colones a contabilizar 
                                                            }
                                                            else
                                                            {
                                                                item_asiento.Wrbtr = (ldec_monto4) * (Convert.ToDecimal(dr_asiento["CodigoAuxiliar5"].ToString().Trim().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal)) / 100);//Importe o monto en colones a contabilizar 

                                                            }
                                                        }
                                                        else
                                                        {
                                                            item_asiento.Wrbtr = (ldec_monto3) * (Convert.ToDecimal(dr_asiento["CodigoAuxiliar5"].ToString().Trim().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal)) / 100);//Importe o monto en colones a contabilizar 
                                                        }
                                                        break;
                                                    case "COM DESC":
                                                        if (dr_asiento["Secuencia"].ToString().Trim() == "1")//desembolso neto
                                                            item_asiento.Wrbtr = (ldec_monto_enviado) * (Convert.ToDecimal(dr_asiento["CodigoAuxiliar5"].ToString().Trim().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal)) / 100);//Importe o monto en colones a contabilizar 
                                                        else//comision
                                                            item_asiento.Wrbtr = (ldec_monto2) * (Convert.ToDecimal(dr_asiento["CodigoAuxiliar5"].ToString().Trim().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal)) / 100);//Importe o monto en colones a contabilizar 
                                                        break;

                                                    default:
                                                        item_asiento.Wrbtr = ldec_monto_enviado * (Convert.ToDecimal(dr_asiento["CodigoAuxiliar5"].ToString().Trim().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal)) / 100);//Importe o monto en colones a contabilizar 
                                                        break;
                                                }
                                                item_asiento.Wrbtr = Math.Round(item_asiento.Wrbtr, 2);
                                                if (item_asiento.Bschl == "40")
                                                    ldec_Total40 += item_asiento.Wrbtr;
                                                else
                                                    ldec_Total50 += item_asiento.Wrbtr;
   
                                                item_asiento.Fipex = dr_asiento["IdPosPre2"].ToString().Trim();//Posición presupuestaria
                                                item_asiento.Kostl = dr_asiento["IdCentroCosto2"].ToString().Trim();//;
                                                item_asiento.Fistl = dr_asiento["IdCentroGestor2"].ToString().Trim();//
                                                item_asiento.Geber = dr_asiento["IdFondo2"].ToString().Trim();//Fondo
                                                item_asiento.Projk = dr_asiento["IdElementoPEP2"].ToString().Trim();//
                                                item_asiento.Prctr = dr_asiento["IdCentroBeneficio2"].ToString().Trim();//
                                                if (dr_Reserva["IdReserva"].ToString().Trim() == "*" || dr_asiento["IdClaveContable2"].ToString().Trim() != "40")
                                                {
                                                    item_asiento.Kblnr = string.Empty;
                                                    item_asiento.Kblpos = string.Empty;
                                                }
                                                else
                                                {
                                                    item_asiento.Kblnr = dr_Reserva["IdReserva"].ToString().Trim();
                                                    item_asiento.Kblpos = dr_Reserva["Posicion"].ToString().Trim();
                                                }
                                                tabla_asientos2[i] = item_asiento;
                                                i++;
                                            }// if (!string.IsNullOrEmpty(dr_asiento["IdClaveContable2"].ToString().Trim()))


                                        }//while (ldec_monto_enviado < ldec_monto && ldec_monto_falta > 0)
                                        //}//while de reservas
                                    }//if (string.IsNullOrEmpty(dr_asiento["CodigoAuxiliar4"].ToString().Trim()) || dr_asiento["CodigoAuxiliar4"].ToString().Trim().Contains(str_AbrevTipoDesembolso))
                                    if (ldec_monto_falta_total <= 0)
                                    {
                                        break;
                                    }//if (ldec_monto_falta_total <= 0)
                                    //}// while (ldec_monto_enviado_total < ldec_monto && ldec_monto_falta_total > 0)

                                }//foreach reservas
                            }//if (!lbln_ConReserva || ....
                            else
                            {
                                lstr_CodResultado = "01";
                                lstr_Mensaje = "Reservas Insuficientes " + lstr_PosPreReserva;

                                //Log.Info(lstr_Mensaje);
                                bool_enviado = false;
                                resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora(str_IdModulo, "1", lstr_idOperacion + "|" + lstr_abrevAcreedor, "Resultado de Contabilización " + lstr_NomOperacion + " " + lstr_Id + ": " + lstr_Mensaje);
                                break;
                            }
                        }//foreach(DataRow dr_asiento in dt_tiras.Rows)



                        ldec_Diferencia40y50 = ldec_Total40 - ldec_Total50;

                        if (lstr_CodResultado == "00")
                        {
                            Boolean lbl_cuadrado = false;
                            LogicaNegocio.wrSigafAsientos.ZfiAsiento[] tabla_asientos = new LogicaNegocio.wrSigafAsientos.ZfiAsiento[i];
                            for (int y = 0; y < i; y++)
                            {
                                tabla_asientos[y] = tabla_asientos2[y];

                                if (!lbl_cuadrado && ldec_Diferencia40y50 != 0)
                                {
                                    if (ldec_Diferencia40y50 > 0 && ldec_Diferencia40y50 < 1 && tabla_asientos[y].Bschl == "50" )
                                    {//es mayor el 40 a los 50, subirle la diferencia al 50
                                        tabla_asientos[y].Wrbtr += ldec_Diferencia40y50;
                                        lbl_cuadrado = true;
                                    }
                                    else
                                    {//es mayor el 40 a los 50, subirle la diferencia al 50
                                        if (ldec_Diferencia40y50 < 0 && ldec_Diferencia40y50 > -1 && tabla_asientos[y].Bschl == "40")
                                        {//es mayor el 50 a los 40, subirle la diferencia al 40

                                            tabla_asientos[y].Wrbtr += Math.Abs(ldec_Diferencia40y50);
                                            lbl_cuadrado = true;
                                        }
                                    }
                                }
                            }//for int y
                            if (i > 0)
                            try
                            {
                                //Carga de Asientos 
                                //envio de asiento mediante servicio web hacia SIGAF
                                //item_resAsientosLog = asientos.EnviarAsientos(tabla_asientos);
                                 
                                logAsiento = this.EnviarAsientoSigaf(tabla_asientos, "X");
                                if (logAsiento.Contains("[E]"))
                                {
                                    lstr_CodResultado = "01";
                                    lstr_Mensaje = "Error al contabilizar asiento. Operación: " + lstr_idOperacion + ". Acreedor: " +
                                        lstr_abrevAcreedor + ". " + logAsiento;
                                    resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora(str_IdModulo, "1", lstr_idOperacion + "|" + lstr_abrevAcreedor, "Resultado de Contabilización " + lstr_NomOperacion + " " + lstr_Id + ": " + lstr_Mensaje);
                                    //Log.Info(lstr_Mensaje);
                                    bool_enviado = false;
                                }
                                else
                                {

                                    logAsiento = this.EnviarAsientoSigaf(tabla_asientos);
                                    if (logAsiento.Contains("[E]"))
                                    {
                                        lstr_CodResultado = "01";
                                        lstr_Mensaje = "Error al contabilizar asiento. Operación: " + lstr_idOperacion + ". Acreedor: " +
                                            lstr_abrevAcreedor + ". " + logAsiento;
                                        resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora(str_IdModulo, "1", lstr_idOperacion + "|" + lstr_abrevAcreedor, "Resultado de Contabilización " + lstr_NomOperacion + " " + lstr_Id + ": " + lstr_Mensaje);
                                        //Log.Info(lstr_Mensaje);
                                        bool_enviado = false;
                                        /**
                                         * Priscilla--- Si no hubo error extraer del log el codigo de asiento y llenar la variable de salida.
                                        **/
                                    }
                                    else 
                                    {
                                        lstr_codAsiento = logAsiento.Substring(logAsiento.IndexOf("BKPFF") + 6, 18);//logAsiento.Length - logAsiento.IndexOf("BKPFF") - 1);
                                        string str_CodRes = string.Empty;
                                        string str_Msg = string.Empty;
                                        int int_Consec = 0;
                                        int int_Secuencia = 0;
                                        try{
                                            this.CrearAsiento(0, "DE", lstr_idOperacion, null, lstr_Id, fechaContabilizacion, "", "C", lstr_codAsiento, logAsiento, "SG", out str_CodRes, out str_Msg, out int_Consec);
                                            if (str_CodRes == "00")
                                            {
                                                for (int y = 0; y < i; y++)
                                                {
                                                    this.CrearAsientoLinea(
                                                        int_Consec, 		
                                                        0,		
                                                        tabla_asientos[y].Bldat ,		
                                                        tabla_asientos[y].Blart	,	
                                                        tabla_asientos[y].Bukrs ,		
                                                        tabla_asientos[y].Budat ,		
                                                        tabla_asientos[y].Waers ,
                                                        Convert.ToDecimal(tabla_asientos[y].Kursf),		
                                                        tabla_asientos[y].Xblnr ,		
                                                        tabla_asientos[y].Xref1Hd, 		
                                                        tabla_asientos[y].Xref2Hd, 		
                                                        tabla_asientos[y].Buzei ,		
                                                        tabla_asientos[y].Bktxt ,		
                                                        tabla_asientos[y].Bschl ,		
                                                        tabla_asientos[y].Hkont ,		
                                                        tabla_asientos[y].Umskz ,		
                                                        Convert.ToDecimal(tabla_asientos[y].Wrbtr) ,		
                                                        Convert.ToDecimal(tabla_asientos[y].Dmbe2) ,		
                                                        tabla_asientos[y].Mwskz ,		
                                                        tabla_asientos[y].Xmwst ,		
                                                        tabla_asientos[y].Zfbdt ,		
                                                        tabla_asientos[y].Zuonr ,		
                                                        tabla_asientos[y].Sgtxt ,		
                                                        tabla_asientos[y].Hbkid ,		
                                                        tabla_asientos[y].Zlsch ,		
                                                        tabla_asientos[y].Kostl ,		
                                                        tabla_asientos[y].Prctr ,		
                                                        tabla_asientos[y].Aufnr ,		
                                                        tabla_asientos[y].Projk ,		
                                                        tabla_asientos[y].Fipex ,		
                                                        tabla_asientos[y].Fistl ,		
                                                        tabla_asientos[y].Measure, 		
                                                        tabla_asientos[y].Geber ,	
                                                        tabla_asientos[y].Werks ,		
                                                        tabla_asientos[y].Valut ,		
                                                        tabla_asientos[y].Kblnr ,		
                                                        tabla_asientos[y].Kblpos ,		
                                                        tabla_asientos[y].Rcomp ,	
                                                        tabla_asientos[y].Xref2 ,		
                                                        tabla_asientos[y].Xref3 ,		
                                                        tabla_asientos[y].Fkber ,
                                                        "SG", out str_CodRes, out str_Msg, out int_Secuencia);
                                                }
                                            }
                                        }
                                        catch (Exception e1){

                                            lstr_CodResultado = "01";
                                            lstr_Mensaje = "Error al guardar asiento final. Operación: " + lstr_idOperacion + ". Acreedor: " +
                                                lstr_abrevAcreedor + ". " + logAsiento;
                                            resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora(str_IdModulo, "1", lstr_idOperacion + "|" + lstr_abrevAcreedor, "Resultado de Contabilización " + lstr_NomOperacion + " " + lstr_Id + ": " + lstr_Mensaje);
                                            //Log.Info(lstr_Mensaje);
                                        }
                                    }
                                   
                                }
                                /*logAsiento = string.Empty;
                                for (int j = 0; j < item_resAsientosLog.Length; j++)
                                {
                                    int x = j + 1;
                                    logAsiento += "\n" + x + "-" + item_resAsientosLog[j];
                                }//for int j
                                */
                            }
                            catch (Exception ex)
                            {
                                lstr_CodResultado = "01";
                                lstr_Mensaje = "Error al contabilizar asiento. Operación: " + lstr_idOperacion + ". Acreedor: " +
                                    lstr_abrevAcreedor + ". " + ex.Message;
                                resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora(str_IdModulo, "1", lstr_idOperacion + "|" + lstr_abrevAcreedor, "Resultado de Contabilización " + lstr_NomOperacion + " " + lstr_Id + ": " + lstr_Mensaje);
                                //Log.Info(lstr_Mensaje);
                                bool_enviado = false;
                            }
                            else
                            {
                                lstr_CodResultado = "01";
                                lstr_Mensaje = "Error al contabilizar No fue posible construir el asiento: " + lstr_idOperacion + ". Acreedor: " +
                                    lstr_abrevAcreedor + ". ";
                                resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora(str_IdModulo, "1", lstr_idOperacion + "|" + lstr_abrevAcreedor, "Resultado de Contabilización " + lstr_NomOperacion + " " + lstr_Id + ": " + lstr_Mensaje);
                                //Log.Info(lstr_Mensaje);
                                bool_enviado = false;
                            }
                            //MessageBox.Show("Resultado de contabilización: \n\n"+logAsiento);
                            Log.Info("Resultado de contabilización: \n\n" + logAsiento);
                        }
                    }//try
                    catch (Exception ex)
                    {
                        lstr_CodResultado = "01";
                        lstr_Mensaje = "Error al contabilizar asiento. Operación: " + lstr_idOperacion + ". Acreedor: " +
                            lstr_abrevAcreedor + ". " + ex.Message;
                        resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora(str_IdModulo, "1", lstr_idOperacion + "|" + lstr_abrevAcreedor, "Resultado de Contabilización " + lstr_NomOperacion + " " + lstr_Id + ": " + lstr_Mensaje);
                        //Log.Info(lstr_Mensaje);
                        bool_enviado = false;
                    }// catch

                    if (lstr_CodResultado == "00")
                        resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora(str_IdModulo, "1", lstr_idOperacion + "|" + str_abrevAcreedor, "Resultado de Contabilización " + lstr_NomOperacion + " " + lstr_Id + ": " + logAsiento, lstr_idOperacion, lstr_Id, lstr_sociedad);
                }//if (tiposA.Tables.Count > 0 && tiposA.Tables["Table"].Rows.Count > 0)
                else
                {
                    lstr_CodResultado = "01";
                    lstr_Mensaje = "Error al determinar Tipo Asiento: " + lstr_idOperacion + " " + lstr_NomOperacion + ". Acreedor: " +
                        lstr_abrevAcreedor + ". Moneda: " + lstr_moneda;
                    resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora(str_IdModulo, "1", lstr_idOperacion + "|" + str_abrevAcreedor, lstr_Mensaje);

                    //Log.Info(lstr_Mensaje);
                    bool_enviado = false;
                }//else if (tiposA.Tables.Count > 0 && tiposA.Tables["Table"].Rows.Count > 0)
            }//if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
            else
            {
                lstr_CodResultado = "01";
                lstr_Mensaje = "Error al determinar Operación: " + lstr_idOperacion + ". Acreedor: " +
                    lstr_abrevAcreedor;
                resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora(str_IdModulo, "1", lstr_idOperacion, lstr_Mensaje);

                //Log.Info(lstr_Mensaje);
                bool_enviado = false;
            }//else if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
            return bool_enviado;
        }

        public bool ReversarAsiento(
              int? lint_Consecutivo, string CodAsiento, DateTime ldt_FechaHasta, out string lstr_CodResultado, out string lstr_Mensaje)
        {
            bool bool_Resultado = true;
            lstr_CodResultado = "00";
            lstr_Mensaje = "Asiento Reversado";
            bool bln_Existe = false;
            bool bln_ExisteLinea = false;


            decimal ldec_tipo_cambioUSD = 0;
            decimal ldec_tipo_cambioEUR = 0;
            string str_MonedaQry1 = "CRCN";

            string lstr_TransaccionVentaUSD = "3140";
            string lstr_TransaccionCompraUSD = "3280";
            if (lint_Consecutivo == null )
            {
                lstr_CodResultado = "99";
                lstr_Mensaje = "Debe indicar el consecutivo";
                bool_Resultado = false;
            }
            else
            {
                try
                {
                    DataSet ds_tipoCambio = tiposCambio.ConsultarTiposCambio(str_MonedaQry1, ldt_FechaHasta, lstr_TransaccionVentaUSD, "N");
                    if (ds_tipoCambio.Tables.Count > 0 && ds_tipoCambio.Tables["Table"].Rows.Count > 0)
                    {
                        // se realiza el cambio a dolares para procesar el asiento
                        ldec_tipo_cambioUSD = Convert.ToDecimal(ds_tipoCambio.Tables["Table"].Rows[0]["Valor"]);

                    }//if ds_tipoCambio
                    /*if (lstr_moneda != "USD")//MONTO max está en USD y se debe pasar a la otra moneda
                    {
                        ds_tipoCambio = tiposCambio.ConsultarTiposCambio(lstr_moneda, ldt_FechaHasta, string.Empty, "N");
                        if (ds_tipoCambio.Tables.Count > 0 && ds_tipoCambio.Tables["Table"].Rows.Count > 0)
                        {
                            // se realiza el cambio a dolares para procesar el asiento
                            ldec_tipo_cambioEUR = Convert.ToDecimal(ds_tipoCambio.Tables["Table"].Rows[0]["Valor"]);

                        }//if ds_tipoCambio
                    }*/

                    DataSet ds_Asiento = this.ConsultarAsientos(lint_Consecutivo,"","","","",null,null);
                    DataTable dt_Asiento = ds_Asiento.Tables[0];
                    foreach (DataRow dr_Asiento in dt_Asiento.Rows )
                    {
                        bln_Existe = true;
                        DataSet ds_Lineas = this.ConsultarAsientosLineas(lint_Consecutivo);
                        DataTable dt_Lineas = ds_Lineas.Tables[0];
                        string logAsiento = string.Empty;
                        string lstr_idOperacion = "Reversion";
                        string lstr_codAsiento = string.Empty;
                        int cantidad_registros = dt_Lineas.Rows.Count;
                        LogicaNegocio.wrSigafAsientos.ZfiAsiento[] tabla_asientos = new LogicaNegocio.wrSigafAsientos.ZfiAsiento[cantidad_registros];
                        int i = 0;
                        foreach(DataRow dr_Linea in dt_Lineas.Rows)
                        {
                            bln_ExisteLinea = true;

                            LogicaNegocio.wrSigafAsientos.ZfiAsiento item_asiento = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
                            item_asiento.Bldat = (string.IsNullOrEmpty(dr_Linea["BLDAT"].ToString()))?"": ldt_FechaHasta.ToString("dd.MM.yyyy");//dr_Linea["BLDAT"].ToString();
                            item_asiento.Budat = (string.IsNullOrEmpty(dr_Linea["BUDAT"].ToString())) ? "" : ldt_FechaHasta.ToString("dd.MM.yyyy");//dr_Linea["BUDAT"].ToString();
                            //item_asiento.Kursf = Convert.ToDecimal(dr_Linea["KURSF"]); // se debe enviar con el tipo de cambio del dia
                            item_asiento.Kursf = Math.Round(ldec_tipo_cambioUSD, 5);
                            item_asiento.Blart = dr_Linea["BLART"].ToString();
                            item_asiento.Bukrs = dr_Linea["BUKRS"].ToString();
                            item_asiento.Waers = dr_Linea["WAERS"].ToString();
                            item_asiento.Xblnr = dr_Linea["XBLNR"].ToString();
                            item_asiento.Xref1Hd = dr_Linea["XREF1_HD"].ToString();
                            item_asiento.Xref2Hd = dr_Linea["XREF2_HD"].ToString();
                            item_asiento.Buzei = dr_Linea["BUZEI"].ToString();
                            item_asiento.Bktxt = dr_Linea["BKTXT"].ToString();
                            if (dr_Linea["BSCHL"].ToString().Trim()== "40")
                                item_asiento.Bschl = "50";
                            else
                                item_asiento.Bschl = "40";
                            item_asiento.Hkont = dr_Linea["HKONT"].ToString();
                            item_asiento.Umskz = dr_Linea["UMSKZ"].ToString();
                            item_asiento.Wrbtr = Convert.ToDecimal(dr_Linea["WRBTR"]);
                            item_asiento.Dmbe2 = Convert.ToDecimal(dr_Linea["DMBE2"]);
                            item_asiento.Mwskz = dr_Linea["MWSKZ"].ToString();
                            item_asiento.Xmwst = dr_Linea["XMWST"].ToString();
                            item_asiento.Zfbdt = dr_Linea["ZFBDT"].ToString();
                            item_asiento.Zuonr = dr_Linea["ZUONR"].ToString();
                            item_asiento.Sgtxt = "Rev. " + dr_Asiento["CodAsiento"].ToString() +" " + dr_Linea["SGTXT"].ToString();
                            item_asiento.Hbkid = dr_Linea["HBKID"].ToString();
                            item_asiento.Zlsch = dr_Linea["ZLSCH"].ToString();
                            item_asiento.Kostl = dr_Linea["KOSTL"].ToString();
                            item_asiento.Prctr = dr_Linea["PRCTR"].ToString();
                            item_asiento.Aufnr = dr_Linea["AUFNR"].ToString();
                            item_asiento.Projk = dr_Linea["PROJK"].ToString();
                            item_asiento.Fipex = dr_Linea["FIPEX"].ToString();
                            item_asiento.Fistl = dr_Linea["FISTL"].ToString();
                            item_asiento.Measure = dr_Linea["MEASURE"].ToString();
                            item_asiento.Geber = dr_Linea["GEBER"].ToString();
                            item_asiento.Werks = dr_Linea["WERKS"].ToString();
                            item_asiento.Valut = dr_Linea["VALUT"].ToString();
                            item_asiento.Kblnr = dr_Linea["KBLNR"].ToString();
                            item_asiento.Kblpos = dr_Linea["KBLPOS"].ToString();
                            item_asiento.Rcomp = dr_Linea["RCOMP"].ToString();
                            item_asiento.Xref2 = dr_Linea["XREF2"].ToString();
                            item_asiento.Xref3 = dr_Linea["XREF3"].ToString();
                            item_asiento.Fkber = dr_Linea["FKBER"].ToString();
                            tabla_asientos[i] = item_asiento;
                            i++;
                        }

                        if (i > 0)
                            try
                            {
                                //Carga de Asientos 
                                //envio de asiento mediante servicio web hacia SIGAF
                                //item_resAsientosLog = asientos.EnviarAsientos(tabla_asientos);

                                logAsiento = this.EnviarAsientoSigaf(tabla_asientos, "X");
                                if (logAsiento.Contains("[E]"))
                                {
                                    lstr_CodResultado = "01";
                                    lstr_Mensaje = "Error al reversar asiento. "+ lint_Consecutivo.ToString()+ ". " + logAsiento;
                                    resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora("DE", "1", lstr_idOperacion + "|" + lint_Consecutivo.ToString() , "Resultado de Reversión "  + lstr_Mensaje);
                                    //Log.Info(lstr_Mensaje);
                                    bool_Resultado = false;
                                }
                                else
                                {

                                    logAsiento = this.EnviarAsientoSigaf(tabla_asientos);
                                    if (logAsiento.Contains("[E]"))
                                    {
                                        lstr_CodResultado = "01";
                                        lstr_Mensaje = "Error al reversar asiento. " + lint_Consecutivo.ToString() + ". " + logAsiento;
                                        resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora("DE", "1", lstr_idOperacion + "|" + lint_Consecutivo.ToString(), "Resultado de Reversión " + lstr_Mensaje);
                                        //Log.Info(lstr_Mensaje);
                                        bool_Resultado = false;
                                        /**
                                         * Priscilla--- Si no hubo error extraer del log el codigo de asiento y llenar la variable de salida.
                                        **/
                                    }
                                    else
                                    {
                                        lstr_codAsiento = logAsiento.Substring(logAsiento.IndexOf("BKPFF") + 1, logAsiento.Length - logAsiento.IndexOf("BKPFF") - 1);
                                        string str_CodRes = string.Empty;
                                        string str_Msg = string.Empty;
                                        resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora("DE", "1", lstr_idOperacion + "|" + lint_Consecutivo.ToString(), "Resultado de Reversión " + lstr_codAsiento);

                                        int int_Consec = 0;
                                        int int_Secuencia = 0;
                                        try
                                        {
                                            this.CrearAsiento(0, "DE", lstr_idOperacion, lint_Consecutivo, dr_Asiento["IdMovimiento"].ToString(), ldt_FechaHasta, "", "C", lstr_codAsiento, logAsiento, "SG", out str_CodRes, out str_Msg, out int_Consec);
                                            if (str_CodRes == "00")
                                            {
                                                for (int y = 0; y < i; y++)
                                                {
                                                    this.CrearAsientoLinea(
                                                        int_Consec,
                                                        0,
                                                        tabla_asientos[y].Bldat,
                                                        tabla_asientos[y].Blart,
                                                        tabla_asientos[y].Bukrs,
                                                        tabla_asientos[y].Budat,
                                                        tabla_asientos[y].Waers,
                                                        Convert.ToDecimal(tabla_asientos[y].Kursf),
                                                        tabla_asientos[y].Xblnr,
                                                        tabla_asientos[y].Xref1Hd,
                                                        tabla_asientos[y].Xref2Hd,
                                                        tabla_asientos[y].Buzei,
                                                        tabla_asientos[y].Bktxt,
                                                        tabla_asientos[y].Bschl,
                                                        tabla_asientos[y].Hkont,
                                                        tabla_asientos[y].Umskz,
                                                        Convert.ToDecimal(tabla_asientos[y].Wrbtr),
                                                        Convert.ToDecimal(tabla_asientos[y].Dmbe2),
                                                        tabla_asientos[y].Mwskz,
                                                        tabla_asientos[y].Xmwst,
                                                        tabla_asientos[y].Zfbdt,
                                                        tabla_asientos[y].Zuonr,
                                                        tabla_asientos[y].Sgtxt,
                                                        tabla_asientos[y].Hbkid,
                                                        tabla_asientos[y].Zlsch,
                                                        tabla_asientos[y].Kostl,
                                                        tabla_asientos[y].Prctr,
                                                        tabla_asientos[y].Aufnr,
                                                        tabla_asientos[y].Projk,
                                                        tabla_asientos[y].Fipex,
                                                        tabla_asientos[y].Fistl,
                                                        tabla_asientos[y].Measure,
                                                        tabla_asientos[y].Geber,
                                                        tabla_asientos[y].Werks,
                                                        tabla_asientos[y].Valut,
                                                        tabla_asientos[y].Kblnr,
                                                        tabla_asientos[y].Kblpos,
                                                        tabla_asientos[y].Rcomp,
                                                        tabla_asientos[y].Xref2,
                                                        tabla_asientos[y].Xref3,
                                                        tabla_asientos[y].Fkber,
                                                        "SG", out str_CodRes, out str_Msg, out int_Secuencia);
                                                }
                                            }
                                            try
                                            {
                                                //cambiar estado de Contabilizado a Reversado en el asiento original
                                                this.CrearAsiento(Convert.ToInt32(lint_Consecutivo), "DE", "", null, dr_Asiento["IdMovimiento"].ToString(), ldt_FechaHasta, "", "R", "", "", "SG", out str_CodRes, out str_Msg, out int_Consec);
                                            }
                                            catch
                                            {

                                            }
                                            
                                        }
                                        catch (Exception e1)
                                        {

                                            lstr_CodResultado = "01";
                                            lstr_Mensaje = "Error al guardar asiento de Reversión. Operación: " + lstr_idOperacion + ". Asiento Origen: " +
                                                lint_Consecutivo.ToString() + ". " + logAsiento;
                                            resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora("DE", "1", lstr_idOperacion + "|" + lint_Consecutivo.ToString(), "Resultado de Contabilización Reversión " + dr_Asiento["IdMovimiento"].ToString() + ": " + lstr_Mensaje);
                                            //Log.Info(lstr_Mensaje);
                                        }
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                lstr_CodResultado = "01";
                                lstr_Mensaje = "Error al Reversar asiento. Operación: " + lstr_idOperacion  + ". " + ex.Message;
                                resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora("DE", "1", lstr_idOperacion + "|" + lint_Consecutivo.ToString(), "Resultado de Reversión: " + lstr_Mensaje);
                                //Log.Info(lstr_Mensaje);
                                bool_Resultado = false;
                            }
                        else
                        {
                            lstr_CodResultado = "01";
                            lstr_Mensaje = "Error al Reversar No fue posible construir el asiento: " + lstr_idOperacion + ". ";
                            resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora("DE", "1", lstr_idOperacion + "|" + lint_Consecutivo.ToString(), "Resultado de Reversión: " + lstr_Mensaje);
                            //Log.Info(lstr_Mensaje);
                            bool_Resultado = false;
                        }
                        //MessageBox.Show("Resultado de reversión: \n\n"+logAsiento);
                        Log.Info("Resultado de reversión: \n\n" + logAsiento);

                    }
                }
                catch (Exception ex)
                {
                    lstr_CodResultado = "99";
                    lstr_Mensaje = ex.ToString();
                    bool_Resultado = false;
                }
            }
            return bool_Resultado;
        }

        public DataSet ConsultarAsientos(
              int? lint_Consecutivo,
            string lstr_IdModulo,
            string lstr_IdMovimiento,
            string lstr_IdOperacion,
            string lstr_CodAsiento,
            DateTime? ldt_FchDesde,
            DateTime? ldt_FchHasta)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarAsientos cr_Procedimiento = new clsConsultarAsientos(lint_Consecutivo,
            lstr_IdModulo,
            lstr_IdMovimiento,
            lstr_IdOperacion,
            lstr_CodAsiento,
            ldt_FchDesde,
            ldt_FchHasta);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }
        public DataSet ConsultarAsientosLineas(
              int? lint_Consecutivo)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarAsientosLineas cr_Procedimiento = new clsConsultarAsientosLineas(lint_Consecutivo);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool BorrarAsientosLineas(
              int lint_Consecutivo, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResBorrado = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty; 
            try
            {
                clsEliminarAsientosLineas cls_ProcBorrarAsientosLineas = new clsEliminarAsientosLineas(lint_Consecutivo);
                str_CodResultado = cls_ProcBorrarAsientosLineas.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcBorrarAsientosLineas.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResBorrado = true;
                }
            }
            catch (Exception ex)
            { str_CodResultado = "99";
            str_Mensaje = ex.ToString();}
            return bool_ResBorrado;
        }

        public bool CrearAsiento(int lint_Consecutivo, string lstr_IdModulo, string lstr_IdOperacion, int? lint_ConsecutivoOrigen,
            string lstr_IdMovimiento, DateTime ldt_Fecha, string lstr_Detalle, string lstr_Estado, string lstr_CodAsiento, string lstr_LogAsiento, string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje,
            out int int_TmpConsecutivo)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsCrearAsiento cr_Procedimiento = new clsCrearAsiento(lint_Consecutivo, lstr_IdModulo, lstr_IdOperacion, lint_ConsecutivoOrigen,
            lstr_IdMovimiento, ldt_Fecha, lstr_Detalle, lstr_Estado, lstr_CodAsiento, lstr_LogAsiento, lstr_UsrCreacion);
                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;
                int_TmpConsecutivo = cr_Procedimiento.Lint_TmpConsecutivo;
                if (String.Equals(str_CodResultado, "00"))
                {
                    lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                    lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
                    bool_ResCreacion = true;
                    if (lint_Consecutivo != 0)
                        int_TmpConsecutivo = lint_Consecutivo;
                    else
                        int_TmpConsecutivo = Convert.ToInt32(lds_TablasConsulta.Tables["Table"].Rows[0]["pTmpConsecutivo"]);
                    return true;
                }
                else
                {
                    int_TmpConsecutivo = -1;
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return bool_ResCreacion;
        }


        public bool CrearAsientoLinea(
              int lint_Consecutivo,
              int lint_Secuencia,
              string lstr_BLDAT,
              string lstr_BLART,
              string lstr_BUKRS,
              string lstr_BUDAT,
              string lstr_WAERS,
              decimal ldec_KURSF,
              string lstr_XBLNR,
              string lstr_XREF1_HD,
              string lstr_XREF2_HD,
              string lstr_BUZEI,
              string lstr_BKTXT,
              string lstr_BSCHL,
              string lstr_HKONT,
              string lstr_UMSKZ,
              decimal ldec_WRBTR,
              decimal ldec_DMBE2,
              string lstr_MWSKZ,
              string lstr_XMWST,
              string lstr_ZFBDT,
              string lstr_ZUONR,
              string lstr_SGTXT,
              string lstr_HBKID,
              string lstr_ZLSCH,
              string lstr_KOSTL,
              string lstr_PRCTR,
              string lstr_AUFNR,
              string lstr_PROJK,
              string lstr_FIPEX,
              string lstr_FISTL,
              string lstr_MEASURE,
              string lstr_GEBER,
              string lstr_WERKS,
              string lstr_VALUT,
              string lstr_KBLNR,
              string lstr_KBLPOS,
              string lstr_RCOMP,
              string lstr_XREF2,
              string lstr_XREF3,
              string lstr_FKBER,
              string lstr_UsrCreacion,
              out string str_CodResultado, out string str_Mensaje,
            out int int_TmpSecuencia)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsCrearAsientoLinea cr_Procedimiento = new clsCrearAsientoLinea(        
               lint_Consecutivo,
               lint_Secuencia,
               lstr_BLDAT,
               lstr_BLART,
               lstr_BUKRS,
               lstr_BUDAT,
               lstr_WAERS,
               ldec_KURSF,
               lstr_XBLNR,
               lstr_XREF1_HD,
               lstr_XREF2_HD,
               lstr_BUZEI,
               lstr_BKTXT,
               lstr_BSCHL,
               lstr_HKONT,
               lstr_UMSKZ,
               ldec_WRBTR,
               ldec_DMBE2,
               lstr_MWSKZ,
               lstr_XMWST,
               lstr_ZFBDT,
               lstr_ZUONR,
               lstr_SGTXT,
               lstr_HBKID,
               lstr_ZLSCH,
               lstr_KOSTL,
               lstr_PRCTR,
               lstr_AUFNR,
               lstr_PROJK,
               lstr_FIPEX,
               lstr_FISTL,
               lstr_MEASURE,
               lstr_GEBER,
               lstr_WERKS,
               lstr_VALUT,
               lstr_KBLNR,
               lstr_KBLPOS,
               lstr_RCOMP,
               lstr_XREF2,
               lstr_XREF3,
               lstr_FKBER,
               lstr_UsrCreacion
                    );
                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;
                int_TmpSecuencia = cr_Procedimiento.Lint_TmpSecuencia;
                if (String.Equals(str_CodResultado, "00"))
                {
                    lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                    lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
                    bool_ResCreacion = true;
                    if (lint_Consecutivo != 0)
                        int_TmpSecuencia = lint_Secuencia;
                    else
                        int_TmpSecuencia = Convert.ToInt32(lds_TablasConsulta.Tables["Table"].Rows[0]["pTmpSecuencia"]);
                    return true;
                }
                else
                {
                    int_TmpSecuencia = -1;
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return bool_ResCreacion;
        }

        public clsTiposAsiento()
        { }

    }
}