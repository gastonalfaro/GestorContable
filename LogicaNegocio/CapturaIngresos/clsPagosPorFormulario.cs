using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos.CapturaIngresos;
using System.Data;
using log4net;
using log4net.Config;

namespace LogicaNegocio.CapturaIngresos
{
    public class clsPagosPorFormulario
    {
        #region Parámetros

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private int lint_IdFormulario;
        private int lint_Anno;
        private int lint_IdPago;
        private DateTime ldt_FchIngreso;
        private DateTime ldt_FchPago;
        private string lstr_IdInstitucion;
        private string lstr_IdServicio;
        private string lstr_CtaMayor;
        private string lstr_IdOficina;
        private string lstr_IdPosPre;
        private string lstr_IdReservaPresupuestaria;
        private string lstr_NroExpediente;
        private string lstr_IdMoneda;
        private decimal ldec_Monto;
        private string lstr_Periodo;
        private string lstr_Estado;
        //private string lstr_TipoIdPersonaPago;
        //private string lstr_IdPersonaPago;
        //private string lstr_CtaCliente;       
        private string lstr_Usuario;
        private int lint_TmpIdPago;

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

        public int Lint_IdPago
        {
            get { return lint_IdPago; }
            set { lint_IdPago = value; }
        }

        public DateTime Ldt_FchIngreso
        {
            get { return ldt_FchIngreso; }
            set { ldt_FchIngreso = value; }
        }

        public DateTime Ldt_FchPago
        {
            get { return ldt_FchPago; }
            set { ldt_FchPago = value; }
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

        public string Lstr_CtaMayor
        {
            get { return lstr_CtaMayor; }
            set { lstr_CtaMayor = value; }
        }

        public string Lstr_IdOficina
        {
            get { return lstr_IdOficina; }
            set { lstr_IdOficina = value; }
        }

        public string Lstr_IdPosPre
        {
            get { return lstr_IdPosPre; }
            set { lstr_IdPosPre = value; }
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

        public string Lstr_Usuario
        {
            get { return lstr_Usuario; }
            set { lstr_Usuario = value; }
        }

        public int Lint_TmpIdPago
        {
            get { return lint_TmpIdPago; }
            set { lint_TmpIdPago = value; }
        }


        #endregion
        
        #region Metodos

        public bool CambiarEstadoPago(int lint_IdFormulario, int lint_Anno, int lint_IdPago, string lstr_Estado, string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsDeshabilitarPago cr_Procedimiento = new clsDeshabilitarPago(lint_IdFormulario, lint_Anno, lint_IdPago, lstr_Estado, lstr_UsrModifica, ldt_FchModifica);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResCreacion;
        } 

        /// <summary>
        /// Método encargado de insertar los pagos asociados a un formulario
        /// </summary>
        /// <param name="lint_IdFormulario"></param>
        /// <param name="lint_Anno"></param>
        /// <param name="lint_IdPago"></param>
        /// <param name="lint_IdInstitucion"></param>
        /// <param name="lint_IdServicio"></param>
        /// <param name="lint_IdOficina"></param>
        /// <param name="lstr_PosPre"></param>
        /// <param name="lint_IdMoneda"></param>
        /// <param name="ldec_Monto"></param>
        /// <param name="lint_Periodo"></param>
        /// <param name="lint_TipoIdPersonaPago"></param>
        /// <param name="lstr_IdPersonaPago"></param>
        /// <param name="lstr_CtaCliente"></param>
        /// <param name="lstr_Usuario"></param>
        /// <param name="lstr_FchCreacion"></param>
        /// <param name="lstr_UsrModifica"></param>
        /// <param name="lstr_FchModifica"></param>
        /// <param name="str_CodResultado"></param>
        /// <param name="str_Mensaje"></param>
        /// <returns></returns>

        public bool InsertarPagosFormularioCaptura(int lint_IdFormulario,
                                                 int lint_Anno,
                                                 int lint_IdPago,
                                                 DateTime ldt_FchIngreso,
                                                 DateTime ldt_FchPago,
                                                 string lstr_IdInstitucion,
                                                 string lstr_IdServicio,
                                                 string lstr_CtaMayor,
                                                 string lstr_IdOficina,
                                                 string lstr_IdPosPre,
                                                 string lstr_IdReservaPresupuestaria,
                                                 string lstr_NroExpediente, 
                                                 string lstr_IdMoneda,
                                                 decimal ldec_Monto,
                                                 string lstr_Periodo,
                                                 string lstr_Estado,
            //string lstr_TipoIdPersonaPago,
            //string lstr_IdPersonaPago,
            //string lstr_CtaCliente,    
                                                 string lstr_Usuario,
                                             out string str_CodResultado, out string str_Mensaje, out int int_TmpIdPago)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            int_TmpIdPago = 0;
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsInsertarPagosFormularioCaptura cr_Procedimiento = new clsInsertarPagosFormularioCaptura(lint_IdFormulario,
                                                                                                                 lint_Anno,
                                                                                                                 lint_IdPago,
                                                                                                                 ldt_FchIngreso,
                                                                                                                 ldt_FchPago,
                                                                                                                 lstr_IdInstitucion,
                                                                                                                 lstr_IdServicio,
                                                                                                                 lstr_CtaMayor,
                                                                                                                 lstr_IdOficina,
                                                                                                                 lstr_IdPosPre,
                                                                                                                 lstr_IdReservaPresupuestaria,
                                                                                                                 lstr_NroExpediente, 
                                                                                                                 lstr_IdMoneda,
                                                                                                                 ldec_Monto,
                                                                                                                 lstr_Periodo,
                                                                                                                 lstr_Estado,
                    //lint_TipoIdPersonaPago,
                    //lstr_IdPersonaPago,
                    //lstr_CtaCliente,
                                                                                                                 lstr_Usuario);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;
                int_TmpIdPago = cr_Procedimiento.Lint_TmpIdPago;

                if (String.Equals(str_CodResultado, "00"))
                {
                    lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                    lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
                    int_TmpIdPago = Convert.ToInt32(lds_TablasConsulta.Tables["Table"].Rows[0]["pTmpIdPago"]);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ConsultarPagosFormulario(int? lint_IdFormulario,
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
                                                 string lstr_Estado)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarPagoFormulario cr_Procedimiento = new clsConsultarPagoFormulario(lint_IdFormulario,
                                                 lint_Anno,
                                                 lint_IdPago,
                                                 ldt_FchPagoDesde,
                                                 ldt_FchPagoHasta,
                                                 lstr_IdInstitucion,
                                                 lstr_IdServicio,
                    //lstr_CtaMayor,
                                                 lstr_IdOficina,
                    //lstr_IdPosPre,
                                                 lstr_IdMoneda,
                    //ldec_Monto,
                                                 lstr_Periodo,
                                                 lstr_Estado);
                if (String.Equals(cr_Procedimiento.Lstr_CodigoResultado, "00"))
                {
                    lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                    lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
                }
            }
            catch (Exception ex)
            { }
            return lds_TablasConsulta;
        }

        public DataSet ConsultarPagosPorFormulario(int? lint_IdFormulario,
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
                                                 string lstr_Estado)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarPagosPorFormulario cr_Procedimiento = new clsConsultarPagosPorFormulario(lint_IdFormulario,
                                                 lint_Anno,
                                                 lint_IdPago,
                                                 ldt_FchPagoDesde,
                                                 ldt_FchPagoHasta,
                                                 lstr_IdInstitucion,
                                                 lstr_IdServicio,
                                                 //lstr_CtaMayor,
                                                 lstr_IdOficina,
                                                 //lstr_IdPosPre,
                                                 lstr_IdMoneda,
                                                 //ldec_Monto,
                                                 lstr_Periodo,
                                                 lstr_Estado);
                if (String.Equals(cr_Procedimiento.Lstr_CodigoResultado, "00"))
                {
                    lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                    lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
                }
            }
            catch (Exception ex)
            { }
            return lds_TablasConsulta;
        }
        #endregion

        #region Constructor
        public clsPagosPorFormulario()
        { }
        #endregion
    }
}