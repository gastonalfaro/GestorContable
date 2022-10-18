﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presentacion.Compartidas;
using System.Globalization;
using System.IO;
using Logica.SubirArchivo;
using System.Web.UI.HtmlControls;

namespace Presentacion.RevelacionNotas
{
    public partial class CapturaIngresos : BASE
    {
        private static Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string lst_IdRevelacion = String.Empty;
        //private clsListaArchivos gcls_Archivos = new clsListaArchivos();
        private string str_Usuario = String.Empty;
        private string str_Institucion = String.Empty;

        /// <summary>
        /// Lista de archivos a subir
        /// </summary>
        public clsListaArchivos g_ListaArchivos
        {
            get
            {
                if (this.ViewState["Archivos"] == null)
                    return null;

                return (clsListaArchivos)this.ViewState["Archivos"];
            }
            set { this.ViewState["Archivos"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            str_Usuario = clsSesion.Current.LoginUsuario;
            str_Institucion = (clsSesion.Current.SociedadUsr + "          ").Substring(0, 10);

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(str_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(str_Usuario, Master, "OBJ_RN"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        MostrarElementos(str_Usuario);
                        g_ListaArchivos = new clsListaArchivos();
                        this.ddlAuxCuentas.SelectedIndex = -1;
                        DateTime ldt_Fecha = DateTime.Now;
                        txtAnno.Text = (Convert.ToString(ldt_Fecha.Year));
                        ddlMesAno.SelectedValue = ObtenerNombreMesAnno(ldt_Fecha);
                        ddlMesAno.Visible = true;
                        CargarDDLs();
                        ddlNomIntitucion.SelectedValue = str_Institucion;
                        ddlNomIntitucion.Enabled = false;
                        this.CargarDirecciones();
                        if (Request.QueryString["Rev"] != null && Request.QueryString["Tipo"] != null)
                        {
                            lst_IdRevelacion = Request.QueryString["Rev"];
                            ConsultarRevelacion(lst_IdRevelacion);
                            btnCrear.Visible = false;
                            if (Request.QueryString["Tipo"] == "Pendiente")
                            {
                                //OcultarMesActual();
                                btnTerminar.Visible = false;
                                txtAnno.ReadOnly = true;// false;
                                ddlMesAno.Enabled = false;//true;
                            }
                            else
                            {
                                txtAnno.ReadOnly = true;
                                ddlMesAno.Enabled = false;
                            }
                        }
                        else if (Request.QueryString["Tipo"] != null && Request.QueryString["Rev"] == null)
                        {
                            btnGuardar.Visible = false;
                            if (Request.QueryString["Tipo"] == "Pendiente")
                            {
                                //OcultarMesActual();
                                lst_IdRevelacion = Request.QueryString["Rev"];
                                btnTerminar.Visible = false;
                                txtAnno.ReadOnly = false;
                                ddlMesAno.Enabled = true;
                            }
                            else
                            {
                                txtAnno.ReadOnly = true;
                                ddlMesAno.Enabled = false;
                            }
                        }
                        else if (Request.QueryString["Rev"] != null && Request.QueryString["Tipo"] == null)
                        {

                            ConsultarRevelacion(lst_IdRevelacion);
                            btnCrear.Visible = false;
                            ddlMesAno.Enabled = false;
                            txtAnno.ReadOnly = true;
                        }
                        else
                        {
                            btnGuardar.Visible = false;
                            ddlMesAno.Enabled = true;
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Login.aspx", true);
                }

            }
            else
            {
                if (string.IsNullOrEmpty(str_Usuario))
                    Response.Redirect("~/Login.aspx", true);

            }
        }

        private void CargarDDLs()
        {
            string str_PlanCuentas = ConfigurationManager.AppSettings["PlanCuentas"];
            DataTable ldt_Instituciones = ws_SGService.uwsConsultarSociedadesGL("", "", "", "", "").Tables[0];
            DataSet lds_AuxCuentas = ws_SGService.uwsConsultarCuentasContablesTipo("");
            DataTable ldt_Auxiliares = lds_AuxCuentas.Tables["Table"];
            DataRow ldr_nuevaFila = ldt_Auxiliares.NewRow();

            ddlAuxCuentas.Items.Clear();
            ldr_nuevaFila["NomCuentaContable"] = "";
            ldr_nuevaFila["IdCuentaContable"] = "";
            ldt_Auxiliares.Rows.InsertAt(ldr_nuevaFila, 0);

            //ddlAuxCuentas.DataSource = ldt_Auxiliares;
            foreach (DataRow vFIla in ldt_Auxiliares.Rows)
            {
                ddlAuxCuentas.Items.Add(new ListItem(vFIla["IdCuentaContable"].ToString() + "--" + vFIla["NomCuentaContable"].ToString(), vFIla["IdCuentaContable"].ToString()));
            }

            //ddlAuxCuentas.DataTextField = "NomCuentaContable";
            //ddlAuxCuentas.DataValueField = "IdCuentaContable";
            //ddlAuxCuentas.DataBind();

            ddlNomIntitucion.DataSource = ldt_Instituciones;
            ddlNomIntitucion.DataTextField = "NomSociedad";
            ddlNomIntitucion.DataValueField = "IdSociedadGL";
            ddlNomIntitucion.DataBind();
            ddlNomIntitucion.Items.Insert(0, new ListItem("", ""));
            ddlNomIntitucion.SelectedValue = clsSesion.Current.SociedadUsr;
        }

        /// <summary>
        /// Carga las entidades de la sociedad elegida en el dropdownlist 
        /// </summary>
        /// <param name="str_Sociedad"></param>
        private void CargarDDLEntidad(string str_Sociedad)
        {
            DataSet lds_Entidades = ws_SGService.uwsConsultarDirecciones("", str_Sociedad.Trim(), "");
            if (lds_Entidades.Tables.Count > 0)
            {
                DataTable ldt_Entidades = lds_Entidades.Tables[0];
                ddlEntidad.Items.Clear();
                if (ldt_Entidades.Rows.Count > 0)
                {
                    DataRow ldr_nuevaFila = ldt_Entidades.NewRow();

                    ldr_nuevaFila["NomDireccion"] = "";
                    ldr_nuevaFila["IdDireccion"] = "";
                    ldt_Entidades.Rows.InsertAt(ldr_nuevaFila, 0);

                    ddlEntidad.DataSource = ldt_Entidades;
                    ddlEntidad.DataTextField = "NomDireccion";
                    ddlEntidad.DataValueField = "IdDireccion";
                    ddlEntidad.DataBind();
                }
                ddlEntidad.SelectedValue = clsSesion.Current.SociedadUsr;
                ddlEntidad.Enabled = false;
            }
        }

        public static Control FindControlRecursive(Control root, string id)
        {
            if (id == string.Empty)
                return null;

            if (root.ID == id)
                return root;

            foreach (Control c in root.Controls)
            {
                Control t = FindControlRecursive(c, id);
                if (t != null)
                    return t;
            }
            return null;
        }

        /// <summary>
        /// Muestra los elementos a los que el usuario puede tener acceso
        /// </summary>
        /// <param name="str_usuario"></param>
        private void MostrarElementos(string str_usuario)
        {
            DataSet ldt_PermisosUsuario = ws_SGService.uwsConsultarPermisosUsuarios(str_usuario, "");

            for (int i = 0; ldt_PermisosUsuario.Tables["Table"].Rows.Count > i; i++)
            {
                string lstr_IdObjeto = ldt_PermisosUsuario.Tables["Table"].Rows[i]["IdObjeto"].ToString();
                bool lbool_Actualizar = (bool)ldt_PermisosUsuario.Tables["Table"].Rows[i]["Actualizar"];
                bool lbool_Consultar = (bool)ldt_PermisosUsuario.Tables["Table"].Rows[i]["Consultar"];
                string lstr_IdliEncabezado = lstr_IdObjeto;

                if (lbool_Consultar)
                {
                    try
                    {
                        HtmlGenericControl hgcMenuEncabezado = (HtmlGenericControl)FindControlRecursive(Master.Page, lstr_IdliEncabezado);

                        if (hgcMenuEncabezado != null)
                            hgcMenuEncabezado.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        WebControl hgcMenuEncabezado = (WebControl)FindControlRecursive(Master.Page, lstr_IdliEncabezado);

                        if (hgcMenuEncabezado != null)
                            hgcMenuEncabezado.Visible = true;
                    }
                }
            }

        }

        /// <summary>
        /// Carga las oficinas en dropdownlist basandose en la sociedad y entidad
        /// </summary>
        /// <param name="str_Sociedad"></param>
        /// <param name="str_Entidad"></param>
        private void CargarDDLOficina(string str_Sociedad, string str_Entidad)
        {
            if (String.IsNullOrEmpty(str_Entidad))
                str_Entidad = string.Empty;

            DataSet lds_Oficinas = ws_SGService.uwsConsultarOficinas("", str_Sociedad, str_Entidad, "");
            if (lds_Oficinas.Tables.Count > 0)
            {
                DataTable ldt_Oficinas = lds_Oficinas.Tables[0];
                ddlOficina.Items.Clear();
                if (ldt_Oficinas.Rows.Count > 0)
                {
                    DataRow ldr_nuevaFila = ldt_Oficinas.NewRow();

                    ldr_nuevaFila["NomOficina"] = "";
                    ldr_nuevaFila["IdOficina"] = "";
                    ldt_Oficinas.Rows.InsertAt(ldr_nuevaFila, 0);

                    ddlOficina.DataSource = ldt_Oficinas;
                    ddlOficina.DataTextField = "NomOficina";
                    ddlOficina.DataValueField = "IdOficina";
                    ddlOficina.DataBind();
                }
            }
        }

        /// <summary>
        /// Evento que se dispara al hacer clic sobre btnCrear
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCrear_Click(object sender, EventArgs e)
        {
            string[] lstr_ResCreacion = new string[2];

            int int_anno = Convert.ToInt32(txtAnno.Text.Trim());
            DateTime ldt_Fecha = DateTime.Now;
            string str_PlanCuentas = ConfigurationManager.AppSettings["PlanCuentas"];
            string lstr_Institucion = ddlNomIntitucion.SelectedValue.Trim();
            string lstr_Entidad = ddlEntidad.SelectedValue.Trim();
            string lstr_Oficina = "";
            if (!String.IsNullOrEmpty(ddlOficina.SelectedValue))
            {
                lstr_Oficina = ddlOficina.SelectedValue;
            }
            string lstr_ClaseCuenta = ddlClaseCuentas.SelectedValue.Trim();
            string lstr_Cuenta = hdAuxiliarCuenta.Value;
            string lstr_Concepto = txtConcepto.Text.Trim();
            string lstr_Justificacion = txtJustificacion.Text.Trim();
            bool lboo_EstaPendiente = false;
            bool lboo_ResultadoAdjuntos = false;
            if (InformacionEstaCompleta())
            {
                if (Request.QueryString["Tipo"] != null)
                {
                    if (Request.QueryString["Tipo"] == "Pendiente")
                    {
                        if (FechaEsCorrecta(int_anno, ddlMesAno.SelectedIndex, ldt_Fecha))
                        {
                            lstr_ResCreacion = ws_SGService.uwsCrearRevelacionPendiente(txtAnno.Text, ddlMesAno.SelectedValue,
                                lstr_Institucion, lstr_Entidad, lstr_Oficina, str_PlanCuentas,
                                lstr_ClaseCuenta, lstr_Cuenta, lstr_Concepto, lstr_Justificacion, "Creada", str_Usuario,
                                hdRubroCuenta.Value, hdSubCuenta.Value, hdSubCuentaAnexa.Value, hdnAux.Value);
                            lboo_EstaPendiente = true;
                        }
                        else
                        {
                            lstr_ResCreacion[0] = "99";
                            MessageBox.Show("La fecha del formulario debe ser anterior a la fecha de creación");
                        }
                    }
                    else
                    {
                        lstr_ResCreacion = ws_SGService.uwsCrearFormulario(lstr_Institucion, lstr_Entidad, lstr_Oficina, str_PlanCuentas,
                                lstr_ClaseCuenta, lstr_Cuenta, lstr_Concepto, lstr_Justificacion, "", "", "Creada", str_Usuario,
                                 hdRubroCuenta.Value, hdSubCuenta.Value, hdSubCuentaAnexa.Value, hdnAux.Value);//clsSesion.Current.LoginUsuario);
                    }
                    if (g_ListaArchivos != null && lstr_ResCreacion[0] == "00")
                    {
                        long lint_IdRev = Convert.ToInt64(lstr_ResCreacion[1]);
                        lboo_ResultadoAdjuntos = AdjuntarArchivos(lint_IdRev, lboo_EstaPendiente);
                    }
                    if (lboo_ResultadoAdjuntos && !lboo_EstaPendiente)
                    {
                        Response.Redirect("~/RevelacionNotas/Formularios.aspx");
                    }
                    if (lboo_ResultadoAdjuntos && lboo_EstaPendiente)
                    {
                        Response.Redirect("~/RevelacionNotas/FormulariosPendientes.aspx");
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar todos los datos solicitados.");
            }

        }

        /// <summary>
        /// Obtiene la informacion de la revelacion.
        /// </summary>
        /// <param name="str_IdRevelacion"></param>
        private void ConsultarRevelacion(string str_IdRevelacion)
        {
            DateTime ldt_Fecha = DateTime.Now;
            DataSet lds_Revelacion = new DataSet();
            DataTable ldt_Revelacion = new DataTable();
            if (Request.QueryString["Tipo"] != null)
            {
                if (Request.QueryString["Tipo"] == "Pendiente")
                {
                    lds_Revelacion = ws_SGService.uwsConsultarRevelacionPendiente(str_IdRevelacion,
                        "", "", "", "", "", "");
                    ldt_Revelacion = lds_Revelacion.Tables["Table"];
                }
                else
                {
                    lds_Revelacion = ws_SGService.uwsConsultarFormulario(str_IdRevelacion);
                    ldt_Revelacion = lds_Revelacion.Tables["Table"];
                    bool lbool_ConPermisosCambios = Convert.ToBoolean(ldt_Revelacion.Rows[0]["PoseeAutorizacionCambios"].ToString());
                }
            }
            if (ldt_Revelacion.Rows.Count > 0)
            {
                string lstr_Entidad = ldt_Revelacion.Rows[0]["Entidad"].ToString();
                string lstr_Institucion = ldt_Revelacion.Rows[0]["Institucion"].ToString();
                string lstr_ClaseCuentas = ldt_Revelacion.Rows[0]["ClaseCuentas"].ToString();
                string lstr_NomClaseCuentas = ldt_Revelacion.Rows[0]["NomGrupoCuenta"].ToString();
                string lstr_Cuenta = ldt_Revelacion.Rows[0]["Cuentas"].ToString();
                string lstr_NomAux = ldt_Revelacion.Rows[0]["NomCuentaContable"].ToString();
                string lstr_IdOficina = ldt_Revelacion.Rows[0]["IdOficina"].ToString();
                txtConcepto.Text = ldt_Revelacion.Rows[0]["Concepto"].ToString();
                txtJustificacion.Text = ldt_Revelacion.Rows[0]["Justificacion"].ToString();
                CargarDDLEntidad(lstr_Institucion);
                CargarDDLOficina(lstr_Institucion, lstr_Entidad);
                ddlEntidad.SelectedValue = lstr_Entidad;
                ddlNomIntitucion.SelectedValue = lstr_Institucion;
                ddlClaseCuentas.SelectedValue = lstr_ClaseCuentas.Trim();
                ddlAuxCuentas.SelectedValue = lstr_Cuenta.Trim();
                ddlOficina.SelectedValue = lstr_IdOficina;
                ddlMesAno.Visible = true;
                ddlMesAno.SelectedValue = ldt_Revelacion.Rows[0]["PeriodoMensual"].ToString();
                txtAnno.Text = ldt_Revelacion.Rows[0]["PeriodoAnual"].ToString();
                clsSesion.Current.FechaUltimaConsulta = ldt_Revelacion.Rows[0]["FchModifica"].ToString().Trim();
                hdnEstado.Value = ldt_Revelacion.Rows[0]["EstadoRevelacion"].ToString();
                string lstr_FechaLimite = String.Empty;

                hdRubroCuenta.Value = ldt_Revelacion.Rows[0]["RubroCuenta"].ToString();
                hdSubCuenta.Value = ldt_Revelacion.Rows[0]["SubCuenta"].ToString();
                hdSubCuentaAnexa.Value = ldt_Revelacion.Rows[0]["SubCuentaAnexa"].ToString();
                hdAuxiliarCuenta.Value = ldt_Revelacion.Rows[0]["AuxiliarCuenta"].ToString();


                if (Request.QueryString["Tipo"] == "Pendiente")
                {
                    lstr_FechaLimite = ldt_Revelacion.Rows[0]["UltimoDiaModifica"].ToString();
                }
                else
                {
                    lstr_FechaLimite = ldt_Revelacion.Rows[0]["UltimoDiaModificacion"].ToString();
                }
                string lstr_FechaActual = DateTime.Today.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ldat_FechaLimite = Convert.ToDateTime(lstr_FechaLimite);
                DateTime ldat_FechaActual = Convert.ToDateTime(lstr_FechaActual);
                if (ldat_FechaLimite <= ldat_FechaActual)
                {
                    btnGuardar.Visible = false;
                    btnTerminar.Visible = false;
                    pnlDatosRevelacion.Enabled = false;
                    gvFiles.Columns[6].Visible = false;
                    pnlArchivosSubir.Visible = false;
                }



                DataSet fileList = new DataSet();
                if (Request.QueryString["Tipo"] == "Pendiente")
                {
                    fileList = ws_SGService.uwsObtenerArchivoPorIdRvelacionPendiente(str_IdRevelacion);
                }
                else
                {
                    fileList = ws_SGService.uwsObtenerArchivoPorIdRvelacion(str_IdRevelacion);
                }
                gvFiles.DataSource = fileList;
                gvFiles.DataBind();

            }
        }

        /// <summary>
        /// Verifica que la fecha sea posterior a la actual
        /// </summary>
        /// <param name="int_Anno"></param>
        /// <param name="int_Mes"></param>
        /// <param name="dat_FechaActual"></param>
        /// <returns></returns>
        private bool FechaEsCorrecta(int int_Anno, int int_Mes, DateTime dat_FechaActual)
        {
            bool lboo_Resultado = false;
            if (dat_FechaActual.Year > int_Anno)
            {
                lboo_Resultado = true;
            }
            if (dat_FechaActual.Year == int_Anno)
            {
                if ((dat_FechaActual.Month - 1) > int_Mes)
                {
                    lboo_Resultado = true;
                }
            }
            return lboo_Resultado;
        }

        /// <summary>
        /// Obtiene el numero del mes dado
        /// </summary>
        /// <param name="str_Mes"></param>
        /// <returns></returns>
        private int ObtenerMesAnno(string str_Mes)
        {
            int lint_NumMes = 0;
            switch (str_Mes)
            {
                case "Enero":
                    lint_NumMes = 1;
                    break;
                case "Febrero":
                    lint_NumMes = 2;
                    break;
                case "Marzo":
                    lint_NumMes = 3;
                    break;
                case "Abril":
                    lint_NumMes = 4;
                    break;
                case "Mayo":
                    lint_NumMes = 5;
                    break;
                case "Junio":
                    lint_NumMes = 6;
                    break;
                case "Julio":
                    lint_NumMes = 7;
                    break;
                case "Agosto":
                    lint_NumMes = 8;
                    break;
                case "Setiembre":
                    lint_NumMes = 9;
                    break;
                case "Octubre":
                    lint_NumMes = 10;
                    break;
                case "Noviembre":
                    lint_NumMes = 11;
                    break;
                case "Diciembre":
                    lint_NumMes = 11;
                    break;
                default:
                    break;
            }
            return lint_NumMes;
        }

        private string ObtenerNombreMesAnno(DateTime dt_Fecha)
        {
            string lstr_MesAnno = string.Empty;
            int lint_NumMes = dt_Fecha.Month;
            switch (lint_NumMes)
            {
                case 1:
                    lstr_MesAnno = "Enero";
                    break;
                case 2:
                    lstr_MesAnno = "Febrero";
                    break;
                case 3:
                    lstr_MesAnno = "Marzo";
                    break;
                case 4:
                    lstr_MesAnno = "Abril";
                    break;
                case 5:
                    lstr_MesAnno = "Mayo";
                    break;
                case 6:
                    lstr_MesAnno = "Junio";
                    break;
                case 7:
                    lstr_MesAnno = "Julio";
                    break;
                case 8:
                    lstr_MesAnno = "Agosto";
                    break;
                case 9:
                    lstr_MesAnno = "Setiembre";
                    break;
                case 10:
                    lstr_MesAnno = "Octubre";
                    break;
                case 11:
                    lstr_MesAnno = "Noviembre";
                    break;
                default:
                    lstr_MesAnno = "Diciembre";
                    break;
            }
            return lstr_MesAnno;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Tipo"]))
            {
                if (Request.QueryString["Tipo"] == "Pendiente")
                {
                    Response.Redirect("~/RevelacionNotas/FormulariosPendientes.aspx");
                }
                else
                {
                    Response.Redirect("~/RevelacionNotas/Formularios.aspx");
                }
            }
            else
            {
                Response.Redirect("~/RevelacionNotas/Formularios.aspx");
            }
        }

        /// <summary>
        /// Evento que se dispara al hacer clic sobre btnGuardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["Rev"] != null)
            {

                lst_IdRevelacion = Request.QueryString["Rev"];


                int int_anno = Convert.ToInt32(txtAnno.Text.Trim());
                if (DatosEdicionEstanCompletos())
                {
                    string[] lstr_Resultado = new string[2];
                    bool lboo_ResActualizacion = false;
                    lstr_Resultado[0] = "99";
                    string lstr_Institucion = ddlNomIntitucion.SelectedValue;
                    string lstr_Entidad = "";
                    if (!String.IsNullOrEmpty(ddlEntidad.SelectedValue))
                    {
                        lstr_Entidad = ddlEntidad.SelectedValue;
                    }
                    string lstr_Oficina = "";
                    if (!String.IsNullOrEmpty(ddlOficina.SelectedValue))
                    {
                        lstr_Oficina = ddlOficina.SelectedValue;
                    }
                    bool lboo_EstaPendiente = false;
                    string lstr_GrupoCuentas = ddlClaseCuentas.SelectedValue;
                    string lstr_Cuentas = hdAuxiliarCuenta.Value;
                    string lstr_Concepto = txtConcepto.Text;
                    string lstr_Justificacion = txtJustificacion.Text;
                    string lstr_Estado = "Modificada";
                    string lstr_Fecha = clsSesion.Current.FechaUltimaConsulta;
                    DateTime ldt_Fecha = DateTime.Now;
                    DateTime lstr_FechaModificado = Convert.ToDateTime(lstr_Fecha);
                    //lstr_Fecha = lstr_FechaModificado.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    lstr_Fecha = lstr_FechaModificado.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                    string lstr_RubroCuenta = hdRubroCuenta.Value;
                    string lstr_SubCuenta = hdSubCuenta.Value;
                    string lstr_SubCuentaAnexa = hdSubCuentaAnexa.Value;
                    string lstr_AuxilarCuenta = ddlAuxCuentas.SelectedValue;

                    if (Request.QueryString["Tipo"] != null)
                    {
                        if (Request.QueryString["Tipo"] == "Pendiente")
                        {
                            if (FechaEsCorrecta(int_anno, ddlMesAno.SelectedIndex, ldt_Fecha))
                            {
                                lstr_Resultado = ws_SGService.uwsModificarRevelacionPendiente(lst_IdRevelacion, lstr_Institucion, lstr_Entidad, lstr_Oficina,
                                    "OPER", lstr_GrupoCuentas, lstr_Cuentas, lstr_Concepto, lstr_Justificacion, lstr_Estado
                                    , lstr_Fecha, str_Usuario, lstr_RubroCuenta, lstr_SubCuenta, lstr_SubCuentaAnexa, lstr_AuxilarCuenta);
                                lboo_EstaPendiente = true;
                            }
                            else
                            {
                                lstr_Resultado[0] = "88";

                            }

                            if (lstr_Resultado[0] == "00")
                            {
                                DataSet lds_Revelacion = ws_SGService.uwsConsultarRevelacionPendiente(lst_IdRevelacion, "",
                                    "", "", "", "", "");
                                clsSesion.Current.FechaUltimaConsulta = lds_Revelacion.Tables["Table"].Rows[0]["FchModifica"].ToString();
                            }
                        }
                        else
                        {
                            lboo_ResActualizacion = ws_SGService.uwsModificarFormulario(lst_IdRevelacion, lstr_Institucion, lstr_Entidad,
                                lstr_Oficina, lstr_GrupoCuentas, lstr_Cuentas, lstr_Concepto, lstr_Justificacion, lstr_Estado, lstr_Fecha, str_Usuario,
                                lstr_RubroCuenta, lstr_SubCuenta, lstr_SubCuentaAnexa, lstr_AuxilarCuenta);// clsSesion.Current.LoginUsuario);
                            if (lboo_ResActualizacion)
                            {
                                DataSet lds_Revelacion = ws_SGService.uwsConsultarFormulario(lst_IdRevelacion);
                                clsSesion.Current.FechaUltimaConsulta = lds_Revelacion.Tables["Table"].Rows[0]["FchModifica"].ToString();
                            }
                        }
                        if (lboo_ResActualizacion || lstr_Resultado[0] == "00")
                        {
                            if (g_ListaArchivos != null)
                            {
                                long lint_IdRev = Convert.ToInt64(lst_IdRevelacion);
                                bool lboo_ResultadoAdjuntos = AdjuntarArchivos(lint_IdRev, lboo_EstaPendiente);
                            }
                            MessageBox.Show("Los datos han sido actualizados.");
                            g_ListaArchivos = new clsListaArchivos();
                            gvArchivoPorSubir.DataSource = null;
                            gvArchivoPorSubir.DataBind();
                            gvArchivoPorSubir.Visible = false;
                            lblMensajeArchSubir.Visible = true;

                            DataSet fileList = new DataSet();
                            if (lboo_EstaPendiente)
                            {
                                fileList = ws_SGService.uwsObtenerArchivoPorIdRvelacionPendiente(lst_IdRevelacion);
                            }
                            else
                            {
                                fileList = ws_SGService.uwsObtenerArchivoPorIdRvelacion(lst_IdRevelacion);
                            }
                            gvFiles.DataSource = fileList;
                            gvFiles.DataBind();

                        }
                        else if (lstr_Resultado[0] == "88")
                        {
                            MessageBox.Show("La fecha del formulario debe ser anterior a la fecha de creación");
                        }
                        else
                        {
                            MessageBox.Show("Error al realizar la acción.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Debe ingresar todos los datos solicitados.");
                }

            }
        }

        [WebMethod]
        public static object ConsultarAuxiliares(string str_GrupoCuentas)
        {
            var objList = (new[] { new { Text = "", Value = "" } }).ToList();

            string str_PlanCuentas = ConfigurationManager.AppSettings["PlanCuentas"];
            DataSet lds_Auxiliares = ws_SGService.uwsConsultarCuentasContables("", str_PlanCuentas, str_GrupoCuentas, "", "", "", "", "");
            DataTable ldt_Auxiliares = lds_Auxiliares.Tables["Table"];
            if (ldt_Auxiliares.Rows.Count > 0)
            {
                for (int i = 0; i < ldt_Auxiliares.Rows.Count; i++)
                {
                    string lstr_Opcion = Convert.ToString(ldt_Auxiliares.Rows[i]["NomCuentaContable"]);
                    string lstr_Valor = Convert.ToString(ldt_Auxiliares.Rows[i]["IdCuentaContable"]);
                    objList.Add(new { Text = lstr_Opcion, Value = lstr_Valor });
                }
            }

            return objList;
        }

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            string str_PlanCuentas = ConfigurationManager.AppSettings["PlanCuentas"];
            bool lboo_ResActualizacion = false;
            string[] lstr_ResultadoCrear = new string[2];
            lstr_ResultadoCrear[0] = "99";
            string lstr_Institucion = ddlNomIntitucion.SelectedValue;
            string lstr_Entidad = "";
            if (!String.IsNullOrEmpty(ddlEntidad.SelectedValue))
            {
                lstr_Entidad = ddlEntidad.SelectedValue;
            }
            string lstr_Oficina = "";
            if (!String.IsNullOrEmpty(ddlOficina.SelectedValue))
            {
                lstr_Oficina = ddlOficina.SelectedValue;
            }
            string lstr_GrupoCuentas = ddlClaseCuentas.SelectedValue;
            string lstr_Cuentas = hdAuxiliarCuenta.Value;
            string lstr_Concepto = txtConcepto.Text;
            string lstr_Justificacion = txtJustificacion.Text;
            DateTime ldt_Fecha = DateTime.Now;
            bool lboo_EstaPendiente = false;
            string lstr_Estado = "Cerrada";
            string lstr_RubroCuenta = hdRubroCuenta.Value;
            string lstr_SubCuenta = hdSubCuenta.Value;
            string lstr_SubCuentaAnexa = hdSubCuentaAnexa.Value;
            string lstr_AuxilarCuenta = ddlAuxCuentas.SelectedValue;


            if (Request.QueryString["Rev"] != null)
            {
                lst_IdRevelacion = Request.QueryString["Rev"];
                DateTime lstr_FechaModificado = Convert.ToDateTime(clsSesion.Current.FechaUltimaConsulta);
                //string lstr_Fecha = lstr_FechaModificado.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                string lstr_Fecha = lstr_FechaModificado.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                if (Request.QueryString["Tipo"] != null)
                {
                    if (Request.QueryString["Tipo"] == "Pendiente")
                    {
                        lstr_ResultadoCrear = ws_SGService.uwsModificarRevelacionPendiente(lst_IdRevelacion, lstr_Institucion, lstr_Entidad, lstr_Oficina,
                            "OPER", lstr_GrupoCuentas, lstr_Cuentas, lstr_Concepto, lstr_Justificacion, lstr_Estado
                            , lstr_Fecha, str_Usuario, lstr_RubroCuenta, lstr_SubCuenta, lstr_SubCuentaAnexa, lstr_AuxilarCuenta);
                        lboo_EstaPendiente = true;
                    }
                    else
                    {
                        lboo_ResActualizacion = ws_SGService.uwsModificarFormulario(lst_IdRevelacion, lstr_Institucion, lstr_Entidad, lstr_Oficina,
                        lstr_GrupoCuentas, lstr_Cuentas, lstr_Concepto, lstr_Justificacion, lstr_Estado, lstr_Fecha, str_Usuario,
                        lstr_RubroCuenta, lstr_SubCuenta, lstr_SubCuentaAnexa, lstr_AuxilarCuenta);// clsSesion.Current.LoginUsuario);
                        if (lboo_ResActualizacion)
                        {
                            lstr_ResultadoCrear[0] = "00";
                        }
                    }
                }
                else
                {
                    lboo_ResActualizacion = ws_SGService.uwsModificarFormulario(lst_IdRevelacion, lstr_Institucion, lstr_Entidad, lstr_Oficina,
                    lstr_GrupoCuentas, lstr_Cuentas, lstr_Concepto, lstr_Justificacion, lstr_Estado, lstr_Fecha, str_Usuario,
                      lstr_RubroCuenta, lstr_SubCuenta, lstr_SubCuentaAnexa, lstr_AuxilarCuenta);// clsSesion.Current.LoginUsuario);
                    if (lboo_ResActualizacion)
                    {
                        lstr_ResultadoCrear[0] = "00";
                    }
                }
            }
            else
            {
                if (InformacionEstaCompleta())
                {
                    if (Request.QueryString["Tipo"] != null)
                    {
                        if (Request.QueryString["Tipo"] == "Pendiente")
                        {
                            lstr_ResultadoCrear = ws_SGService.uwsCrearRevelacionPendiente(txtAnno.Text, ddlMesAno.SelectedValue,
                                lstr_Institucion, lstr_Entidad, lstr_Oficina, str_PlanCuentas,
                               lstr_GrupoCuentas, lstr_Cuentas, lstr_Concepto, lstr_Justificacion, "Creada", str_Usuario,
                               lstr_RubroCuenta, lstr_SubCuenta, lstr_SubCuentaAnexa, lstr_AuxilarCuenta);
                            lboo_EstaPendiente = true;
                        }
                        else
                        {
                            lstr_ResultadoCrear = ws_SGService.uwsCrearFormulario(lstr_Institucion, lstr_Entidad, lstr_Oficina, str_PlanCuentas
                           , lstr_GrupoCuentas, lstr_Cuentas, lstr_Concepto, lstr_Justificacion, "", "", lstr_Estado, str_Usuario,
                           lstr_RubroCuenta, lstr_SubCuenta, lstr_SubCuentaAnexa, lstr_AuxilarCuenta);//clsSesion.Current.LoginUsuario);
                        }
                    }
                    else
                    {
                        lstr_ResultadoCrear = ws_SGService.uwsCrearFormulario(lstr_Institucion, lstr_Entidad, lstr_Oficina, str_PlanCuentas,
                       lstr_GrupoCuentas, lstr_Cuentas, lstr_Concepto, lstr_Justificacion, "", "", lstr_Estado, str_Usuario,
                       lstr_RubroCuenta, lstr_SubCuenta, lstr_SubCuentaAnexa, lstr_AuxilarCuenta);//clsSesion.Current.LoginUsuario);
                    }

                    bool lboo_ResultadoAdjuntos = false;
                    if (g_ListaArchivos != null && lstr_ResultadoCrear[0] == "00")
                    {
                        long lint_IdRev = Convert.ToInt64(lstr_ResultadoCrear[1]);
                        lboo_ResultadoAdjuntos = AdjuntarArchivos(lint_IdRev, lboo_EstaPendiente);
                    }
                    else if (gvFiles.Rows.Count > 0 && lstr_ResultadoCrear[0] == "00")
                    {
                        lboo_ResultadoAdjuntos = true;
                    }
                    if (lboo_ResultadoAdjuntos && !lboo_EstaPendiente)
                    {
                        Response.Redirect("~/RevelacionNotas/Formularios.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/RevelacionNotas/FormulariosPendientes.aspx");
                    }
                }
                else
                {
                    MessageBox.Show("Debe ingresar todos los datos solicitados.");
                }


            }
            if (lstr_ResultadoCrear[0] == "00")
            {
                Response.Redirect("~/RevelacionNotas/Formularios.aspx");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnArchivos_Click(object sender, EventArgs e)
        {
            List<clsArchivos> l_ListaArchivos = new List<clsArchivos>();
            Request.ContentType = "multipart/form-data";
            HttpFileCollection files = HttpContext.Current.Request.Files;
            foreach (string fileTagName in files)
            {
                HttpPostedFile file = Request.Files[fileTagName];
                if (file.ContentLength > 0)
                {


                    int lint_tamano = file.ContentLength;
                    string lstr_nombre = file.FileName;
                    int position = lstr_nombre.LastIndexOf("\\");
                    lstr_nombre = lstr_nombre.Substring(position + 1);
                    string tipoContenido = file.ContentType;
                    byte[] archivoDato = new byte[lint_tamano];
                    file.InputStream.Read(archivoDato, 0, lint_tamano);
                    clsArchivos l_Archivo = new clsArchivos(lint_tamano, lstr_nombre, tipoContenido);
                    l_Archivo.Lbyt_Datos = archivoDato;
                    l_ListaArchivos.Add(l_Archivo);
                    g_ListaArchivos.L_ListaArchivos.Add(l_Archivo);
                    gvArchivoPorSubir.DataSource = LlenarTablaVacia();
                    gvArchivoPorSubir.DataBind();
                    //ws_SGService.uwsGuardarArchivo(lstr_nombre, tipoContenido, lint_tamano, archivoDato, 4, "", "fsolano");
                }
            }
        }

        /// <summary>
        /// Crea una tabla vacia de archivos
        /// </summary>
        /// <returns></returns>
        public DataTable LlenarTablaVacia()
        {
            DataTable ldt_ArchivosPorSubir = new DataTable();
            ldt_ArchivosPorSubir.Columns.Add("NombreArchivo", typeof(string));
            ldt_ArchivosPorSubir.Columns.Add("Tamano", typeof(string));
            int lint_TamanoTabla = 0;
            if (g_ListaArchivos.L_ListaArchivos.Count > 0)
            {
                foreach (clsArchivos archivo in g_ListaArchivos.L_ListaArchivos)
                {
                    DataRow ldr_FilaTabla = ldt_ArchivosPorSubir.NewRow();

                    ldr_FilaTabla["NombreArchivo"] = archivo.Lstr_Nombre;
                    ldr_FilaTabla["Tamano"] = Convert.ToString(archivo.Lint_Tamano);
                    ldt_ArchivosPorSubir.Rows.InsertAt(ldr_FilaTabla, lint_TamanoTabla);
                    lint_TamanoTabla++;
                }
            }
            lblMensajeArchSubir.Visible = false;
            return ldt_ArchivosPorSubir;
        }



        private void EliminarArchivo(string str_nombre, int int_tamano)
        {

        }

        /// <summary>
        /// Metodo utilizado para la subida de archivos
        /// </summary>
        /// <param name="int_IdRev"></param>
        /// <param name="boo_EstaPendiente"></param>
        /// <returns></returns>

        private bool AdjuntarArchivos(long int_IdRev, bool boo_EstaPendiente)
        {
            bool lboo_Resultado = false;

            if (!boo_EstaPendiente)
            {
                foreach (clsArchivos Archivo in g_ListaArchivos.L_ListaArchivos)
                {
                    ws_SGService.uwsGuardarArchivo(Archivo.Lstr_Nombre, Archivo.Lstr_TipoContenido, Archivo.Lint_Tamano,
                        Archivo.Lbyt_Datos, int_IdRev, "", 0, "", 0, "", 0, clsSesion.Current.LoginUsuario);
                }
            }
            else
            {
                foreach (clsArchivos Archivo in g_ListaArchivos.L_ListaArchivos)
                {
                    ws_SGService.uwsGuardarArchivo(Archivo.Lstr_Nombre, Archivo.Lstr_TipoContenido, Archivo.Lint_Tamano,
                        Archivo.Lbyt_Datos, 0, "", 0, "", int_IdRev, "", 0, clsSesion.Current.LoginUsuario);
                }
            }
            lboo_Resultado = true;

            return lboo_Resultado;
        }

        /// <summary>
        /// Evento que se dispara al seleccionar clase de cuenta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlClaseCuentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet lds_Auxiliares = ws_SGService.uwsConsultarCuentasContablesTipo(ddlClaseCuentas.SelectedValue);
            DataTable ldt_Auxiliares = lds_Auxiliares.Tables["Table"];

            if (ldt_Auxiliares.Rows.Count > 0)
            {
                DataRow ldr_nuevaFila = ldt_Auxiliares.NewRow();

                ldr_nuevaFila["NomCuentaContable"] = "";
                ldr_nuevaFila["IdCuentaContable"] = "";
                ldt_Auxiliares.Rows.InsertAt(ldr_nuevaFila, 0);
                ddlAuxCuentas.Items.Clear();

                foreach (DataRow vFIla in ldt_Auxiliares.Rows)
                {
                    ddlAuxCuentas.Items.Add(new ListItem(vFIla["IdCuentaContable"].ToString() + "--" + vFIla["NomCuentaContable"].ToString(), vFIla["IdCuentaContable"].ToString()));
                }
                //              ddlAuxCuentas.DataSource = ldt_Auxiliares;
                //ddlAuxCuentas.DataTextField = "NomCuentaContable";
                //ddlAuxCuentas.DataValueField = "IdCuentaContable";
                //ddlAuxCuentas.DataBind();
            }
        }

        /// <summary>
        /// Elimina un archivo pendiente de subir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvArchivoPorSubir_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            if (gvArchivoPorSubir.Rows.Count > 0)
            {
                int int_indice = Convert.ToInt32(e.RowIndex);
                Label lblNombre = (Label)gvArchivoPorSubir.Rows[int_indice].Cells[0].FindControl("lblNombre");
                Label lblTamano = (Label)gvArchivoPorSubir.Rows[int_indice].Cells[1].FindControl("lblTamano");
                string lstr_NombreArchivo = lblNombre.Text;
                int lint_Tamano = Convert.ToInt32(lblTamano.Text);
                g_ListaArchivos.EliminarArchivo(lstr_NombreArchivo, lint_Tamano);
                LlenarTablaVacia();
                gvArchivoPorSubir.DataSource = LlenarTablaVacia();
                gvArchivoPorSubir.DataBind();

            }
            if (gvArchivoPorSubir.Rows.Count == 0)
            {
                lblMensajeArchSubir.Visible = true;
            }
            else
            {
                lblMensajeArchSubir.Visible = false;
            }
        }

        /// <summary>
        /// Elimina los archivos 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvFiles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string[] lstr_ResEliminacion = new string[2];

            if (gvFiles.Rows.Count > 1)
            {
                int int_indice = Convert.ToInt32(e.RowIndex);
                Label lblFchModificacion = (Label)gvFiles.Rows[int_indice].Cells[4].FindControl("lblFecha");
                Label lblIdArchivo = (Label)gvFiles.Rows[int_indice].Cells[0].FindControl("lblIdArchivo");
                DateTime lstr_FechaModificado = Convert.ToDateTime(lblFchModificacion.Text);
                string lstr_fecha = String.Empty;
                //lstr_fecha = lstr_FechaModificado.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                lstr_fecha = lstr_FechaModificado.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                lstr_ResEliminacion = ws_SGService.uwsEliminarArchivo(lblIdArchivo.Text, lstr_fecha);
                if (Request.QueryString["Rev"] != null && Request.QueryString["Tipo"] != null && lstr_ResEliminacion[0] == "00")
                {
                    string lst_IdRevelacion = Request.QueryString["Rev"];
                    DataSet fileList = new DataSet();
                    if (Request.QueryString["Tipo"] == "Pendiente")

                    {
                        fileList = ws_SGService.uwsObtenerArchivoPorIdRvelacionPendiente(lst_IdRevelacion);
                        gvFiles.DataSource = fileList;
                        gvFiles.DataBind();
                    }
                    else
                    {
                        fileList = ws_SGService.uwsObtenerArchivoPorIdRvelacion(lst_IdRevelacion);
                        gvFiles.DataSource = fileList;
                        gvFiles.DataBind();
                    }
                }
            }
            else
            {
                MessageBox.Show("La revelación debe contener un archivo como mínimo.");
            }
        }

        /// <summary>
        /// Verifica que los campos requeridos esten completos
        /// </summary>
        /// <returns></returns>
        private bool InformacionEstaCompleta()
        {
            bool lboo_ResValidacion = false;
            if (//!String.IsNullOrEmpty(ddlAuxCuentas.SelectedValue) &&
                 !String.IsNullOrEmpty(ddlClaseCuentas.SelectedValue) && !String.IsNullOrEmpty(ddlNomIntitucion.SelectedValue)
                && !String.IsNullOrEmpty(txtConcepto.Text.Trim()) && !String.IsNullOrEmpty(txtJustificacion.Text.Trim())
                && g_ListaArchivos.L_ListaArchivos != null && !String.IsNullOrEmpty(ddlEntidad.SelectedValue) && !String.IsNullOrEmpty(ddlOficina.SelectedValue))
            {
                if (g_ListaArchivos.L_ListaArchivos.Count > 0)
                {
                    lboo_ResValidacion = true;
                }
            }
            return lboo_ResValidacion;
        }

        /// <summary>
        /// Verifica que los datos de edicion estan completos
        /// </summary>
        /// <returns></returns>
        private bool DatosEdicionEstanCompletos()
        {
            bool lboo_ResValidacion = false;
            if (gvFiles.Rows.Count > 0)
            {
                if (//!String.IsNullOrEmpty(ddlAuxCuentas.SelectedValue) && 
                    !String.IsNullOrEmpty(ddlClaseCuentas.SelectedValue) && !String.IsNullOrEmpty(ddlNomIntitucion.SelectedValue)
                    && !String.IsNullOrEmpty(txtConcepto.Text.Trim()) && !String.IsNullOrEmpty(txtJustificacion.Text.Trim()) && !String.IsNullOrEmpty(ddlOficina.SelectedValue))
                {
                    lboo_ResValidacion = true;
                }
            }
            else if (//!String.IsNullOrEmpty(ddlAuxCuentas.SelectedValue) && 
                    !String.IsNullOrEmpty(ddlClaseCuentas.SelectedValue) && !String.IsNullOrEmpty(ddlNomIntitucion.SelectedValue)
                    && !String.IsNullOrEmpty(txtConcepto.Text.Trim()) && !String.IsNullOrEmpty(txtJustificacion.Text.Trim())
                    && g_ListaArchivos.L_ListaArchivos != null && !String.IsNullOrEmpty(ddlOficina.SelectedValue))
            {
                if (g_ListaArchivos.L_ListaArchivos.Count > 0)
                {
                    lboo_ResValidacion = true;
                }
            }
            return lboo_ResValidacion;
        }

        private void CargarDirecciones()
        {
            //Cucurucho VARO 16/01/2020
            ddlEntidad.Enabled = true;

            ddlEntidad.Items.Clear();
            ddlOficina.Items.Clear();
            //Cucurucho VARO 15/01/2020
            string str_consul = "SELECT LTRIM(RTRIM(IdDireccion)) as IdDireccion, NomDireccion  FROM ma.Direcciones WHERE IdSociedadGL='" + clsSesion.Current.SociedadUsr + "'";
            DataSet lds_Entidades = ws_SGService.uwsConsultarDinamico(str_consul);

            //Cucurucho VARO 15/01/2020
            //DataSet lds_Entidades = ws_SGService.uwsConsultarDirecciones("", ddlNomIntitucion.SelectedValue, "");

            if (lds_Entidades.Tables.Count > 0)
            {
                DataTable ldt_Entidades = lds_Entidades.Tables["Table"];


                if (ldt_Entidades.Rows.Count > 0)
                {
                    DataRow ldr_nuevaFila = ldt_Entidades.NewRow();

                    ldr_nuevaFila["NomDireccion"] = "";
                    ldr_nuevaFila["IdDireccion"] = "";
                    ldt_Entidades.Rows.InsertAt(ldr_nuevaFila, 0);

                    ddlEntidad.DataSource = ldt_Entidades;
                    ddlEntidad.DataTextField = "NomDireccion";
                    ddlEntidad.DataValueField = "IdDireccion";
                    ddlEntidad.DataBind();
                }
                //ddlEntidad.SelectedValue = clsSesion.Current.SociedadUsr;
            }
        }
        /// <summary>
        /// Evento al seleccionar una institucion en el dropdownlist de ddlNomIntitucion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlNomIntitucion_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CargarDirecciones();
        }

        /// <summary>
        /// Evento que se da cuando se selecciona una entidad diferete en ddlEntidad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlEntidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlOficina.Items.Clear();
            //Cucurucho VARO 15/01/2020
            if (!String.IsNullOrEmpty(ddlEntidad.SelectedValue)) //&& !String.IsNullOrEmpty(ddlNomIntitucion.SelectedValue))
            {
                //Cucurucho VARO 15/01/2020
                //DataSet lds_Oficinas = ws_SGService.uwsConsultarOficinas("", ddlEntidad.SelectedValue,  ddlNomIntitucion.SelectedValue,  "");

                //Cucurucho VARO 16/01/2020
                string str_consul = "SELECT IdOficina, NomOficina FROM ma.Oficinas WHERE IdDireccion='" + ddlEntidad.SelectedValue.Trim() + "' AND IdSociedadGL='" + clsSesion.Current.SociedadUsr + "'";
                DataSet lds_Oficinas = ws_SGService.uwsConsultarDinamico(str_consul);

                if (lds_Oficinas.Tables.Count > 0)
                {
                    DataTable ldt_Oficinas = lds_Oficinas.Tables["Table"];

                    if (ldt_Oficinas.Rows.Count > 0)
                    {
                        DataRow ldr_nuevaFila = ldt_Oficinas.NewRow();

                        ldr_nuevaFila["NomOficina"] = "";
                        ldr_nuevaFila["IdOficina"] = "";
                        ldt_Oficinas.Rows.InsertAt(ldr_nuevaFila, 0);

                        ddlOficina.DataSource = ldt_Oficinas;
                        ddlOficina.DataTextField = "NomOficina";
                        ddlOficina.DataValueField = "IdOficina";
                        ddlOficina.DataBind();
                    }
                    ddlEntidad.Enabled = false;
                }
            }
            else
            {
                ddlOficina.Items.Clear();
            }
        }

        protected void gvFiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "consulta")
            {
                string index = Convert.ToString(e.CommandArgument.ToString());
                clsArchivoSubir utilidad = new clsArchivoSubir();
                // Get the file from the database
                DataSet file = utilidad.ufnObtenerArchivoPorId(index);
                DataRow row = file.Tables[0].Rows[0];

                string name = (string)row["NombreArchivo"];
                string contentType = (string)row["TipoContenido"];
                Byte[] data = (Byte[])row["Dato"];

                // Send the file to the browser
                Response.AddHeader("Content-type", contentType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + name);
                Response.BinaryWrite(data);
                Response.Flush();
                Response.End();
            }
        }

        protected void ddlAuxCuentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Auxiliar = ddlAuxCuentas.SelectedValue.ToString();

            hdRubroCuenta.Value = Auxiliar.Substring(0, 3) + "0000000";
            hdSubCuenta.Value = Auxiliar.Substring(0, 7) + "000";
            hdSubCuentaAnexa.Value = Auxiliar.Substring(0, 9) + "0";
            hdAuxiliarCuenta.Value = Auxiliar.Substring(0, 5) + "00000"; //Cuenta

        }

    }
}