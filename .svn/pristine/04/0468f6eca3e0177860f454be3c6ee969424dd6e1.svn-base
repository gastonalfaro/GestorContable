using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.VisualBasic;

namespace LogicaNegocio.CalculosFinancieros
{
    public class ClsUtilitarios
    {
        //variables
        Mantenimiento.clsDinamico dinamica = new Mantenimiento.clsDinamico();
        wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
        Seguridad.tBitacora bitacora = new Seguridad.tBitacora();

        //revisa las diferencias de las fechas
        public bool DiferenciaFechas(DateTime FchPeriodo, DateTime FchVencimiento)
        {
            //evalua si es año bisiesto
            int evaluacion = (FchVencimiento - FchVencimiento.AddYears(-1)).Days;
            int dias = (FchVencimiento - FchPeriodo).Days;
            return (dias >= evaluacion) ? true : false;
        }

        private static decimal Truncate(decimal value, int length)
        {
            return Math.Truncate(value * 100) / 100;
        }

        //
        //public DataTable AsientoReserva(
        //    DataRow ldr_Tira,
        //    decimal ldec_monto,
        //    decimal ldec_TipoCambio,
        //    DateTime ldt_FchContable,
        //    string lstr_Referencia,
        //    string lstr_Detalle,
        //    decimal ldec_TipoCambioColones,
        //    string lstr_MonedaTitulo,
        //    string lstr_DetalleBitacora,
        //    int lint_NroValor,
        //    string lstr_Nemotecnico)
        //{
        //    #region pospre haber

        //    RegistroContable LineaAsiento;
        //    List<RegistroContable> Asiento = new List<RegistroContable>();

        //    string lstr_Monto = string.Empty;
        //    DataTable lds_Datos = new DataTable();
        //    decimal ldec_MontoTotal = 0;
        //    string reservasError = "";
        //    string lstr_NuevoPosPrePago = string.Empty;
        //    DataSet ldat_Reservas = new DataSet();

        //    ldat_Reservas = dinamica.ConsultarDinamico("select * from ma.reservasdetalle where idcuentacontable = '" + ldr_Tira["IdCuentaContable2"].ToString().Trim() + "' and idpospre = '" + ldr_Tira["IdPosPre2"].ToString().Trim() + "'");
        //    //ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim(), string.Empty, ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
        //    if (ldat_Reservas.Tables[0].Rows.Count != 0)
        //    {
        //        DataView dv = ldat_Reservas.Tables[0].DefaultView;
        //        dv.Sort = "OrdenDeudaInterna ASC";

        //        lds_Datos.Columns.Add("IdReserva");
        //        lds_Datos.Columns.Add("OrdenDeudaInterna");
        //        lds_Datos.Columns.Add("IdPosPre");
        //        lds_Datos.Columns.Add("Posicion");
        //        lds_Datos.Columns.Add("Monto");

        //        foreach (DataRow drForm in dv.ToTable().Rows)
        //        {
        //            //if (drForm["IdMoneda"].ToString().Trim().Equals(ldat_AsientoPago.Rows[0]["CodigoAuxiliar"].ToString().Trim()))
        //            if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty) && !drForm["OrdenDeudaInterna"].ToString().Equals("0"))
        //            {
        //                lstr_Monto = wsDeudaInterna.ConsultaMontoReservaSAP(drForm["IdReserva"].ToString().Trim(), drForm["Posicion"].ToString().Trim());
        //                lds_Datos.Rows.Add(
        //                    drForm["IdReserva"].ToString(),
        //                    drForm["OrdenDeudaInterna"].ToString(),
        //                    drForm["IdPosPre"].ToString(),
        //                    drForm["Posicion"].ToString(),
        //                    lstr_Monto);
        //                reservasError += "Reserva :" + drForm["IdPosPre"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
        //                ldec_MontoTotal += Convert.ToDecimal(lstr_Monto);
        //            }
        //        }

        //        if (Convert.ToDecimal(ldec_MontoTotal) >= (ldec_monto * ldec_TipoCambio))
        //        {
        //            //Genera el asiento
        //            decimal ldec_SaldoCont = ldec_monto;
        //            decimal ldec_Saldo = (ldec_monto * ldec_TipoCambio);

        //            foreach (DataRow drForm in lds_Datos.Rows)
        //            {
        //                if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
        //                {
        //                    //lstr_Referencia = lstr_NroValor.Trim().Equals("00") ? "No Asociado" : lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Costo Transaccion";
        //                    decimal reservaTpoCambio = Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio;

        //                    ldat_Asiento.Rows.Add(
        //                        lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
        //                        ldt_FchContable.ToString("dd.MM.yyyy"),
        //                        ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim(),
        //                        ldat_Tira.Rows[index]["IdClaveContable2"].ToString().Trim(),
        //                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim(),
        //                        lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
        //                        ldat_Tira.Rows[index]["IdCentroCosto2"].ToString().Trim(),
        //                        ldat_Tira.Rows[index]["IdCentroBeneficio2"].ToString().Trim(),
        //                        ldat_Tira.Rows[index]["IdElementoPEP2"].ToString().Trim(),
        //                        ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim(),
        //                        ldat_Tira.Rows[index]["IdCentroGestor2"].ToString().Trim(),
        //                        ldat_Tira.Rows[index]["IdFondo2"].ToString().Trim(),
        //                        drForm["IdReserva"].ToString().Trim(),
        //                        drForm["Posicion"].ToString().Trim(),
        //                        Truncate(ldec_SaldoCont > reservaTpoCambio ? reservaTpoCambio : ldec_SaldoCont, 2)
        //                    );

        //                    LineaAsiento = new RegistroContable();
        //                    LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
        //                    LineaAsiento.Lstr_Fecha = ldt_FchContable.ToString("dd.MM.yyyy");
        //                    LineaAsiento.Lstr_Cuenta = ldr_Tira["IdCuentaContable2"].ToString().Trim();
        //                    LineaAsiento.Lstr_ClaveContable = ldr_Tira["IdClaveContable2"].ToString().Trim();
        //                    LineaAsiento.Lstr_Moneda = ldr_Tira["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
        //                    LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
        //                    LineaAsiento.Lstr_CentroCosto = ldr_Tira["IdCentroCosto2"].ToString().Trim();
        //                    LineaAsiento.Lstr_CentroBeneficio = ldr_Tira["IdCentroBeneficio2"].ToString().Trim();
        //                    LineaAsiento.Lstr_ElementoPEP = ldr_Tira["IdElementoPEP2"].ToString().Trim();
        //                    LineaAsiento.Lstr_PosPre = ldr_Tira["IdPosPre2"].ToString().Trim();
        //                    LineaAsiento.Lstr_CentroGestor = ldr_Tira["IdCentroGestor2"].ToString().Trim();
        //                    LineaAsiento.Lstr_Fondo = ldr_Tira["IdFondo2"].ToString().Trim();
        //                    LineaAsiento.Lstr_DocPres = drForm["IdReserva"].ToString().Trim();
        //                    LineaAsiento.Lstr_PosDocPres = drForm["Posicion"].ToString().Trim();
        //                    LineaAsiento.Ldec_Monto = Truncate(ldec_SaldoCont > Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio ? Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio : ldec_SaldoCont, 2);
        //                    LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
        //                    LineaAsiento.Lstr_MonedaTiutlo = lstr_MonedaTitulo;
        //                    Asiento.Add(LineaAsiento);
        //                }

        //                //Resta el saldo
        //                ldec_SaldoCont = ldec_SaldoCont - (Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio);
        //                ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
        //            }
        //        }
        //        else
        //        {
        //            //Almacena en bitácora de que no lo hizo
        //            bitacora.ufnRegistrarAccionBitacora("DI", "123", lstr_DetalleBitacora, "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, auxiliares[0], lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
        //        }
        //    }
        //    else
        //    {
        //        if (ldat_Tira.Rows[index]["IdCentroBeneficio2"].ToString().Trim().Equals(""))
        //        {
        //            ldat_Asiento.Rows.Add(
        //                lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
        //                ldt_FchContable.ToString("dd.MM.yyyy"),
        //                ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim(),
        //                ldat_Tira.Rows[index]["IdClaveContable2"].ToString().Trim(),
        //                ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim(),
        //                lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
        //                ldat_Tira.Rows[index]["IdCentroCosto2"].ToString().Trim(),
        //                ldat_Tira.Rows[index]["IdCentroBeneficio2"].ToString().Trim(),
        //                ldat_Tira.Rows[index]["IdElementoPEP2"].ToString().Trim(),
        //                ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim(),
        //                ldat_Tira.Rows[index]["IdCentroGestor2"].ToString().Trim(),
        //                ldat_Tira.Rows[index]["IdFondo2"].ToString().Trim(),
        //                ldat_Tira.Rows[index]["DocPresupuestario2"].ToString().Trim(),
        //                ldat_Tira.Rows[index]["PosDocPresupuestario2"].ToString().Trim(),
        //                Truncate(ldec_monto, 2)
        //            );
        //        }
        //        else
        //        {
        //            bitacora.ufnRegistrarAccionBitacora("DI", "123", lstr_DetalleBitacora, "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldr_Tira["IdCuentaContable2"].ToString().Trim() + " con fondo " + ldr_Tira["IdPosPre2"].ToString().Trim(), auxiliares[0], lint_NroValor + "-" + lstr_Nemotecnico, "G206");
        //        }
        //    }
        //    #endregion
        //}
    }
    [Serializable]
    public class CCSS
    {
        string lstr_EstadoValor;

        public string Lstr_EstadoValor
        {
            get { return lstr_EstadoValor; }
            set { lstr_EstadoValor = value; }
        }
        string lstr_Nemotecnico;

        public string Lstr_Nemotecnico
        {
            get { return lstr_Nemotecnico; }
            set { lstr_Nemotecnico = value; }
        }
        string lstr_Tipo;

        public string Lstr_Tipo
        {
            get { return lstr_Tipo; }
            set { lstr_Tipo = value; }
        }
        string lstr_TipoNegociacion;

        public string Lstr_TipoNegociacion
        {
            get { return lstr_TipoNegociacion; }
            set { lstr_TipoNegociacion = value; }
        }
        int lint_NumValor;

        public int Lint_NumValor
        {
            get { return lint_NumValor; }
            set { lint_NumValor = value; }
        }
        string lstr_Moneda;

        public string Lstr_Moneda
        {
            get { return lstr_Moneda; }
            set { lstr_Moneda = value; }
        }
        decimal ldec_ValorFacial;

        public decimal Ldec_ValorFacial
        {
            get { return ldec_ValorFacial; }
            set { ldec_ValorFacial = value; }
        }
        string ldt_FchValor;

        public string Ldt_FchValor
        {
            get { return ldt_FchValor; }
            set { ldt_FchValor = value; }
        }
        string lstr_PlazoValor;

        public string Lstr_PlazoValor
        {
            get { return lstr_PlazoValor; }
            set { lstr_PlazoValor = value; }
        }
        string ldt_FchCancelacion;

        public string Ldt_FchCancelacion
        {
            get { return ldt_FchCancelacion; }
            set { ldt_FchCancelacion = value; }
        }
        string ldt_FchVencimiento;

        public string Ldt_FchVencimiento
        {
            get { return ldt_FchVencimiento; }
            set { ldt_FchVencimiento = value; }
        }
        decimal ldec_ValorTransadoBruto;

        public decimal Ldec_ValorTransadoBruto
        {
            get { return ldec_ValorTransadoBruto; }
            set { ldec_ValorTransadoBruto = value; }
        }
        decimal ldec_ValorTransadoNeto;

        public decimal Ldec_ValorTransadoNeto
        {
            get { return ldec_ValorTransadoNeto; }
            set { ldec_ValorTransadoNeto = value; }
        }
        decimal ldec_TasaBruta;

        public decimal Ldec_TasaBruta
        {
            get { return ldec_TasaBruta; }
            set { ldec_TasaBruta = value; }
        }
        decimal ldec_TasaNeta;

        public decimal Ldec_TasaNeta
        {
            get { return ldec_TasaNeta; }
            set { ldec_TasaNeta = value; }
        }
        string lstr_NroEmisionSerie;

        public string Lstr_NroEmisionSerie
        {
            get { return lstr_NroEmisionSerie; }
            set { lstr_NroEmisionSerie = value; }
        }
        string ldt_FchCreacionT;

        public string Ldt_FchCreacionT
        {
            get { return ldt_FchCreacionT; }
            set { ldt_FchCreacionT = value; }
        }
        string lstr_SistemaNegociacion;

        public string Lstr_SistemaNegociacion
        {
            get { return lstr_SistemaNegociacion; }
            set { lstr_SistemaNegociacion = value; }
        }
        string lstr_MotivoAnulacion;

        public string Lstr_MotivoAnulacion
        {
            get { return lstr_MotivoAnulacion; }
            set { lstr_MotivoAnulacion = value; }
        }
        decimal ldec_RendimientoPorDescuento;

        public decimal Ldec_RendimientoPorDescuento
        {
            get { return ldec_RendimientoPorDescuento; }
            set { ldec_RendimientoPorDescuento = value; }
        }
        decimal ldec_Premio;

        public decimal Ldec_Premio
        {
            get { return ldec_Premio; }
            set { ldec_Premio = value; }
        }
        decimal ldec_ImpuestoPagado;

        public decimal Ldec_ImpuestoPagado
        {
            get { return ldec_ImpuestoPagado; }
            set { ldec_ImpuestoPagado = value; }
        }


        decimal ldec_monto_pagado_efectivo;

        public decimal Ldec_monto_pagado_efectivo
        {
            get { return ldec_monto_pagado_efectivo; }
            set { ldec_monto_pagado_efectivo = value; }
        }

        string lstr_UsrCreacion;

        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }
        string lstr_ModuloSINPE;

        public string Lstr_ModuloSINPE
        {
            get { return lstr_ModuloSINPE; }
            set { lstr_ModuloSINPE = value; }
        }

        string lstr_EntidadCustodia;

        public string Lstr_EntidadCustodia
        {
            get { return lstr_EntidadCustodia; }
            set { lstr_EntidadCustodia = value; }
        }

        public string Lstr_2110201041
        {
            get
            {
                return lstr_2110201041;
            }

            set
            {
                lstr_2110201041 = value;
            }
        }

        public string Lstr_2110201050
        {
            get
            {
                return lstr_2110201050;
            }

            set
            {
                lstr_2110201050 = value;
            }
        }

        public string Lstr_2110201070
        {
            get
            {
                return lstr_2110201070;
            }

            set
            {
                lstr_2110201070 = value;
            }
        }

        public string Lstr_2110201080
        {
            get
            {
                return lstr_2110201080;
            }

            set
            {
                lstr_2110201080 = value;
            }
        }

        public string Lstr_2110201090
        {
            get
            {
                return lstr_2110201090;
            }

            set
            {
                lstr_2110201090 = value;
            }
        }

        public string Lstr_2110201100
        {
            get
            {
                return lstr_2110201100;
            }

            set
            {
                lstr_2110201100 = value;
            }
        }

        public string Lstr_2116421000
        {
            get
            {
                return lstr_2116421000;
            }

            set
            {
                lstr_2116421000 = value;
            }
        }

        public string Lstr_2116422000
        {
            get
            {
                return lstr_2116422000;
            }

            set
            {
                lstr_2116422000 = value;
            }
        }

        public string Lstr_2116423000
        {
            get
            {
                return lstr_2116423000;
            }

            set
            {
                lstr_2116423000 = value;
            }
        }

        public string Lstr_2116424000
        {
            get
            {
                return lstr_2116424000;
            }

            set
            {
                lstr_2116424000 = value;
            }
        }

        string lstr_2110201041;
        string lstr_2110201050;
        string lstr_2110201070;
        string lstr_2110201080;
        string lstr_2110201090;
        string lstr_2110201100;
        string lstr_2116421000;
        string lstr_2116422000;
        string lstr_2116423000;
        string lstr_2116424000;



    }
    [Serializable]
    public class Magisterio
    {
        string lstr_Nemotecnico;

        public string Lstr_Nemotecnico
        {
            get { return lstr_Nemotecnico; }
            set { lstr_Nemotecnico = value; }
        }
        int lint_NumValor;

        public int Lint_NumValor
        {
            get { return lint_NumValor; }
            set { lint_NumValor = value; }
        }
    }

    public class RegistroContable
    {
        string lstr_Sociedad;
        public string Lstr_Sociedad
        {
          get { return lstr_Sociedad; }
          set { lstr_Sociedad = value; }
        }

        string lstr_Referencia;
        public string Lstr_Referencia
        {
          get { return lstr_Referencia; }
          set { lstr_Referencia = value; }
        }

        string lstr_Fecha;
        public string Lstr_Fecha
        {
            get { return lstr_Fecha; }
            set { lstr_Fecha = value; }
        }

        string lstr_Cuenta;
        public string Lstr_Cuenta
        {
          get { return lstr_Cuenta; }
          set { lstr_Cuenta = value; }
        }

        string lstr_ClaveContable;
        public string Lstr_ClaveContable
        {
          get { return lstr_ClaveContable; }
          set { lstr_ClaveContable = value; }
        }

        string lstr_Moneda;
        public string Lstr_Moneda
        {
          get { return lstr_Moneda; }
          set { lstr_Moneda = value; }
        }

        string lstr_TextoInfo;
        public string Lstr_TextoInfo
        {
          get { return lstr_TextoInfo; }
          set { lstr_TextoInfo = value; }
        }

        string lstr_CentroCosto;
        public string Lstr_CentroCosto
        {
          get { return lstr_CentroCosto; }
          set { lstr_CentroCosto = value; }
        }

        string lstr_CentroBeneficio;
        public string Lstr_CentroBeneficio
        {
          get { return lstr_CentroBeneficio; }
          set { lstr_CentroBeneficio = value; }
        }

        string lstr_ElementoPEP;
        public string Lstr_ElementoPEP
        {
          get { return lstr_ElementoPEP; }
          set { lstr_ElementoPEP = value; }
        }
        
        string lstr_PosPre;
        public string Lstr_PosPre
        {
          get { return lstr_PosPre; }
          set { lstr_PosPre = value; }
        }

        string lstr_CentroGestor;
        public string Lstr_CentroGestor
        {
          get { return lstr_CentroGestor; }
          set { lstr_CentroGestor = value; }
        }
        
        string lstr_Fondo;
        public string Lstr_Fondo
        {
          get { return lstr_Fondo; }
          set { lstr_Fondo = value; }
        }
        
        string lstr_DocPres;
        public string Lstr_DocPres
        {
          get { return lstr_DocPres; }
          set { lstr_DocPres = value; }
        }
        
        string lstr_PosDocPres;
        public string Lstr_PosDocPres
        {
          get { return lstr_PosDocPres; }
          set { lstr_PosDocPres = value; }
        }
        
        decimal ldec_Monto;
        public decimal Ldec_Monto
        {
          get { return ldec_Monto; }
          set { ldec_Monto = value; }
        }
        
        decimal ldec_TipoCambio;
        public decimal Ldec_TipoCambio
        {
          get { return ldec_TipoCambio; }
          set { ldec_TipoCambio = value; }
        }

        string lstr_MonedaTiutlo;
        public string Lstr_MonedaTiutlo
        {
            get { return lstr_MonedaTiutlo; }
            set { lstr_MonedaTiutlo = value; }
        }
    }

    public class RegistroDiferencial
    {
        string lstr_MonedaTiutlo;
        public string Lstr_MonedaTiutlo
        {
            get { return lstr_MonedaTiutlo; }
            set { lstr_MonedaTiutlo = value; }
        }
        
        string lstr_Nemotecnico;
        public string Lstr_Nemotecnico
        {
            get { return lstr_Nemotecnico; }
            set { lstr_Nemotecnico = value; }
        }
        
        string lstr_Plazo;
        public string Lstr_Plazo
        {
            get { return lstr_Plazo; }
            set { lstr_Plazo = value; }
        }
        
        string lstr_Propietario;
        public string Lstr_Propietario
        {
            get { return lstr_Propietario; }
            set { lstr_Propietario = value; }
        }
        
        string lstr_DescripcionCuenta;
        public string Lstr_DescripcionCuenta
        {
            get { return lstr_DescripcionCuenta; }
            set { lstr_DescripcionCuenta = value; }
        }
        
        string lstr_TpoCambioUDE;
        public string Lstr_TpoCambioUDE
        {
            get { return lstr_TpoCambioUDE; }
            set { lstr_TpoCambioUDE = value; }
        }
        
        decimal ldec_TpoCambioUDE;
        public decimal Ldec_TpoCambioUDE
        {
            get { return ldec_TpoCambioUDE; }
            set { ldec_TpoCambioUDE = value; }
        }

        decimal ldec_MontoUDE;
        public decimal Ldec_MontoUDE
        {
            get { return ldec_MontoUDE; }
            set { ldec_MontoUDE = value; }
        }

        string lstr_Cuenta;
        public string Lstr_Cuenta
        {
            get { return lstr_Cuenta; }
            set { lstr_Cuenta = value; }
        }
        string lstr_IdClaveContable;
        public string Lstr_IdClaveContable
        {
            get { return lstr_IdClaveContable; }
            set { lstr_IdClaveContable = value; }
        }
    }

    public class Cupones
    {
        decimal tasaBruta;
        decimal tasaNeta;
        decimal interesBruto;
        decimal interesNeto;
        DateTime fechaInicio;
        DateTime fechaFin;
        decimal periodicidad;

        public decimal TasaBruta
        {
            get { return tasaBruta; }
            set { tasaBruta = value; }
        }
        public decimal TasaNeta
        {
            get { return tasaNeta; }
            set { tasaNeta = value; }
        }
        public decimal InteresBruto
        {
            get { return interesBruto; }
            set { interesBruto = value; }
        }
        public decimal InteresNeto
        {
            get { return interesNeto; }
            set { interesNeto = value; }
        }
        public DateTime FechaInicio
        {
            get { return fechaInicio; }
            set { fechaInicio = value; }
        }
        public DateTime FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; }
        }

        public decimal Periodicidad
        {
            get
            {
                return periodicidad;
            }

            set
            {
                periodicidad = value;
            }
        }
    }

    public class XTIR
    {
        decimal monto;
        DateTime fecha;

        public decimal Monto
        {
            get { return monto; }
            set { monto = value; }
        }
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
    }

    public class PeriodosCupones
    {
        DateTime fchInicio;
        DateTime fchFin;
        int periodicidad;

        public int Periodicidad
        {
            get { return periodicidad; }
            set { periodicidad = value; }
        }
        public DateTime FchFin
        {
            get { return fchFin; }
            set { fchFin = value; }
        }
        public DateTime FchInicio
        {
            get { return fchInicio; }
            set { fchInicio = value; }
        }
    }

    public struct Pair<T, Z>
    {
        public Pair(T first, Z second) { First = first; Second = second; }
        public readonly T First;
        public readonly Z Second;
    }

    public static class CalcXirr
    {
        internal static decimal CalculateXNPV(List<XTIR> cfs, decimal r)
        {
            if (r <= -1)
                r = -0.99999999m; 

            return (from cf in cfs
                    let startDate = cfs.OrderBy(cf1 => cf1.Fecha).First().Fecha
                    select cf.Monto / (decimal)Math.Pow((double)(1 + r),((cf.Fecha - startDate).Days / 365.0))).Sum();
        }

        internal static Pair<decimal, decimal> FindBrackets(Func<List<XTIR>, decimal, decimal> func, List<XTIR> cfs, decimal guess = 0.1m)
        {

            const int maxIter = 100;
            const decimal bracketStep = 0.5m;
            //const decimal guess = 0.1m; //ggarcia se pasa como parámetro para hacerlo igual que excel.
            decimal leftBracket = guess - bracketStep;
            //ggarcia, primero debe averiguar la tir positiva
            //if (guess > 0) 
            //    leftBracket = (leftBracket < 0) ? 0 : leftBracket;
            decimal rightBracket = guess + bracketStep;
            var iter = 0;

            //while (func(cfs, leftBracket) * func(cfs, rightBracket) > 0 && iter++ < maxIter)
            while (func(cfs, rightBracket) * func(cfs, leftBracket) > 0 && iter++ < maxIter)
            {
                leftBracket -= bracketStep;
                //ggarcia, primero debe averiguar la tir positiva
            //    if (guess > 0)
            //        leftBracket = (leftBracket < 0) ? 0 : leftBracket;
                rightBracket += bracketStep;
            }

            if (iter >= maxIter)
                return new Pair<decimal, decimal>(0, 0);

            return new Pair<decimal, decimal>(leftBracket, rightBracket);

        }

        internal static decimal Bisection(Func<decimal, decimal> func, Pair<decimal, decimal> brackets, decimal tol, int maxIters)
        {
            int iter = 1;
            decimal f3 = 0;
            decimal x3 = 0;
            decimal x1 = brackets.First;//ggarcia
            decimal x2 = brackets.Second;//ggarcia

            do {
                var f1 = func(x1);
                var f2 = func(x2);

                if (f1 == 0 && f2 == 0)
                    return x1;
                if (f1 * f2 > 0)
                    return 0;

                x3 = (x1 + x2) / 2;
                f3 = func(x3);

                if (f3 * f1 < 0)
                    x2 = x3;
                else
                    x1 = x3;
                /*if (f3 * f2 < 0 )
                    x1 = x3;
                else
                    x2 = x3;*/
                iter++;
            } while (Math.Abs(x1 - x2) / 2 > tol && f3 != 0 && iter < maxIters);

            if (f3 == 0)
                return x3;
            if (Math.Abs(x1 - x2) / 2 < tol)
                return x3;
            if (iter > maxIters)
                return 0;

            return 0;
        }


        public static decimal CalculateXIRR(List<XTIR> lstXirr, decimal tolerance, int maxIters)
        {
            var brackets = FindBrackets(CalculateXNPV, lstXirr);
            if (brackets.First == brackets.Second)
                return 0;

            decimal x = Bisection(r => CalculateXNPV(lstXirr, r), brackets, tolerance, maxIters);
            return x;
        }
    }
}