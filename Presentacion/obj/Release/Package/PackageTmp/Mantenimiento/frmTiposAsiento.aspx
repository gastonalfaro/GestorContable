<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmTiposAsiento.aspx.cs" Inherits="Presentacion.Mantenimiento.frmTiposAsiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">

            <div class="FormatoBotones">
                <asp:Button ID="btnTpoAsientoNuevo" runat="server" Text="NUEVO" OnClick="btnTpoAsientoNuevo_Click" CssClass="ButtonNeutro" />
            </div>
            <div class="col-md-12">
                <h3>Tipos de Asiento</h3>
                <p>Consulta de Tipos de Asiento del Sistema Gestor.</p>
                <div class="col-md-6" runat="server" id="divCod" visible="false">

                    <div class="col-md-5">
                        <asp:Label ID="lblIDOP" runat="server" Text="Cód. Operación:" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtIDOp" runat="server" CssClass="FormatoTextBox"></asp:TextBox>
                    </div>

                </div>
                <div class="col-md-6" id="dviRelleno" runat="server" visible="false"></div>

                <div class="col-md-6">
                    <div class="col-md-5">
                        <asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtBusqIdTpoAsiento" runat="server" CssClass="FormatoTextBox"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="col-md-5">
                        <asp:Label ID="Label2" runat="server" Text="Módulo:" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-md-7">
                        <asp:DropDownList ID="ddlModulo" runat="server" CssClass="FormatoDropDownList" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlModulo_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="col-md-5">
                        <asp:Label ID="Label3" runat="server" Text="Operación:" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-md-7">
                        <asp:DropDownList ID="ddlOperacion" runat="server" CssClass="FormatoTextBox" OnTextChanged="ddlOperacion_TextChanged" AutoPostBack="true" ></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="col-md-5">
                        <asp:Label ID="Label4" runat="server" Text="Cuenta Contable:" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtCuentaContable" runat="server" CssClass="FormatoTextBox"></asp:TextBox>
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="col-md-5">
                        <asp:Label ID="Label6" runat="server" Text="Código Auxiliar:" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtCodigoAux1" runat="server" CssClass="FormatoTextBox"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="col-md-5">
                        <asp:Label ID="Label7" runat="server" Text="Código Auxiliar 2:" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtCodigoAux2" runat="server" CssClass="FormatoTextBox"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="col-md-5">
                        <asp:Label ID="Label8" runat="server" Text="Código Auxiliar 3:" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtCodigoAux3" runat="server" CssClass="FormatoTextBox"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="col-md-5">
                        <asp:Label ID="Label9" runat="server" Text="Código Auxiliar 4:" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtCodigoAux4" runat="server" CssClass="FormatoTextBox"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="col-md-5">
                        <asp:Label ID="Label10" runat="server" Text="Código Auxiliar 5:" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtCodigoAux5" runat="server" CssClass="FormatoTextBox"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="col-md-5">
                        <asp:Label ID="Label11" runat="server" Text="Código Auxiliar 6:" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtCodigoAux6" runat="server" CssClass="FormatoTextBox"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="col-md-5">
                        <asp:Label ID="Label12" runat="server" Text="Secuencia:" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtSecuencia" runat="server" CssClass="FormatoTextBox"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6" style="margin-right: 0px; font-size: 11px;">
                    <div class="col-md-5">
                        <asp:Label ID="Label5" runat="server" Text="Posiciones Presupuestarias:" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox ID="txtPosPre" runat="server" CssClass="FormatoTextBox"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-12" style="text-align: center;">
                    <asp:Button ID="btnTpoAsientoConsultar" runat="server" Text="CONSULTAR" OnClick="btnTpoAsientoConsultar_Click" CssClass="ButtonNeutro" />
                </div>
            </div>


            <div class="col-md-12">
                <asp:Label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true"></asp:Label>
            </div>
            <div style="width: 100%; height: 100%; overflow: auto">
                <asp:GridView ID="grdvTpoAsiento" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnRowDataBound="grdvTpoAsiento_RowDataBound"
                    Width="100%" OnSelectedIndexChanged="grdvTpoAsiento_SelectedIndexChanged" OnRowEditing="grdvTpoAsiento_RowEditing"
                    OnRowUpdating="grdvTpoAsiento_RowUpdating" OnPageIndexChanging="grdvTpoAsiento_PageIndexChanging"
                    OnRowCancelingEdit="grdvTpoAsiento_RowCancelingEdit" PageSize="15" AllowPaging="True" OnRowDeleting="grdvTpoAsiento_RowDeleting">
                    <Columns>

                        <asp:TemplateField HeaderText="FechaModifica" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblFechaModifica" runat="server" Text='<%# Bind("FchModifica") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Módulo">
                            <ItemTemplate>
                                <asp:Label ID="lblIdModulo" runat="server" Text='<%# Bind("IdModulo") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtInsertarIdModulo" runat="server" Text='<%# Bind("IdModulo") %>' MaxLength="4" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Código Operación">
                            <ItemTemplate>
                                <asp:Label ID="lblIdOperacion" runat="server" Text='<%# Bind("IdOperacion") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtInsertarIdOperacion" runat="server" Text='<%# Bind("IdOperacion") %>' MaxLength="4" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Operación">
                            <ItemTemplate>
                                <asp:Label ID="lblNomOperacion" runat="server" Text='<%# Bind("NomOperacion") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtInsertarNomOperacion" runat="server" Text='<%# Bind("NomOperacion") %>' MaxLength="4" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Código">
                            <ItemTemplate>
                                <asp:Label ID="lblIdCodigo" runat="server" Text='<%# Bind("Codigo") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtInsertarIdCodigo" runat="server" Text='<%# Bind("Codigo") %>' MaxLength="4" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Código Auxiliar">
                            <ItemTemplate>
                                <asp:Label ID="lblIdCodigoAux" runat="server" Text='<%# Bind("CodigoAuxiliar") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtInsertarIdCodigoAux" runat="server" Text='<%# Bind("CodigoAuxiliar") %>' MaxLength="4" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Código Auxiliar 2">
                            <ItemTemplate>
                                <asp:Label ID="lblIdCodigoAux2" runat="server" Text='<%# Bind("CodigoAuxiliar2") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtInsertarIdCodigoAux2" runat="server" Text='<%# Bind("CodigoAuxiliar2") %>' MaxLength="4" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Código Auxiliar 3">
                            <ItemTemplate>
                                <asp:Label ID="lblIdCodigoAux3" runat="server" Text='<%# Bind("CodigoAuxiliar3") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtInsertarIdCodigoAux3" runat="server" Text='<%# Bind("CodigoAuxiliar3") %>' MaxLength="4" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Código Auxiliar 4">
                            <ItemTemplate>
                                <asp:Label ID="lblIdCodigoAux4" runat="server" Text='<%# Bind("CodigoAuxiliar4") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtInsertarIdCodigoAux4" runat="server" Text='<%# Bind("CodigoAuxiliar4") %>' MaxLength="4" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Código Auxiliar 5">
                            <ItemTemplate>
                                <asp:Label ID="lblIdCodigoAux5" runat="server" Text='<%# Bind("CodigoAuxiliar5") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtInsertarIdCodigoAux5" runat="server" Text='<%# Bind("CodigoAuxiliar5") %>' MaxLength="4" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Código Auxiliar 6">
                            <ItemTemplate>
                                <asp:Label ID="lblIdCodigoAux6" runat="server" Text='<%# Bind("CodigoAuxiliar6") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtInsertarIdCodigoAux6" runat="server" Text='<%# Bind("CodigoAuxiliar6") %>' MaxLength="4" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Secuencia">
                            <ItemTemplate>
                                <asp:Label ID="lblIdSecuencia" runat="server" Text='<%# Bind("Secuencia") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtInsertarIdSecuencia" runat="server" Text='<%# Bind("Secuencia") %>' MaxLength="4" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Clave Contable">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoIdClaveContable" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIdClaveContable" runat="server" Text='<%# Bind("IdClaveContable") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarIdClaveContable" runat="server" Width="90%" Text='<%# Bind("IdClaveContable") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cuenta Contable">
                            <FooterTemplate>
                                <asp:TextBox ID="txtIdCuentaContable" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIdCuentaContable" runat="server" Text='<%# Bind("IdCuentaContable") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarIdCuentaContable" runat="server" Width="90%" Text='<%# Bind("IdCuentaContable") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Centro Costo">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoIdCentroCosto" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIdCentroCosto" runat="server" Text='<%# Bind("IdCentroCosto") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarIdCentroCosto" runat="server" Width="90%" Text='<%# Bind("IdCentroCosto") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Centro Beneficio">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoIdCentroBeneficio" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIdCentroBeneficio" runat="server" Text='<%# Bind("IdCentroBeneficio") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarIdCentroBeneficio" runat="server" Width="90%" Text='<%# Bind("IdCentroBeneficio") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Elemento PEP">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoIdElementoPEP" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIdElementoPEP" runat="server" Text='<%# Bind("IdElementoPEP") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarIdElementoPEP" runat="server" Width="90%" Text='<%# Bind("IdElementoPEP") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PosPre">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoIdPosPre" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIdPosPre" runat="server" Text='<%# Bind("IdPosPre") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarIdPosPre" runat="server" Width="90%" Text='<%# Bind("IdPosPre") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Centro Gestor">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoIdCentroGestor" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIdCentroGestor" runat="server" Text='<%# Bind("IdCentroGestor") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarIdCentroGestor" runat="server" Width="90%" Text='<%# Bind("IdCentroGestor") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Programa">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoIdPrograma" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIdPrograma" runat="server" Text='<%# Bind("IdPrograma") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarIdPrograma" runat="server" Width="90%" Text='<%# Bind("IdPrograma") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Fondo">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoIdFondo" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIdFondo" runat="server" Text='<%# Bind("IdFondo") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarIdFondo" runat="server" Width="90%" Text='<%# Bind("IdFondo") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Doc Presupuestario">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoDocPresupuestario" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDocPresupuestario" runat="server" Text='<%# Bind("DocPresupuestario") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarDocPresupuestario" runat="server" Width="90%" Text='<%# Bind("DocPresupuestario") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PosDoc Presupuestario">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoPosDocPresupuestario" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPosDocPresupuestario" runat="server" Text='<%# Bind("PosDocPresupuestario") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarPosDocPresupuestario" runat="server" Width="90%" Text='<%# Bind("PosDocPresupuestario") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Flujo Efectivo">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoFlujoEfectivo" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFlujoEfectivo" runat="server" Text='<%# Bind("FlujoEfectivo") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarFlujoEfectivo" runat="server" Width="90%" Text='<%# Bind("FlujoEfectivo") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="NICSP24">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoNICSP24" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblNICSP24" runat="server" Text='<%# Bind("NICSP24") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarNICSP24" runat="server" Width="90%" Text='<%# Bind("NICSP24") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Clave Contable2">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoIdClaveContable2" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIdClaveContable2" runat="server" Text='<%# Bind("IdClaveContable2") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarIdClaveContable2" runat="server" Width="90%" Text='<%# Bind("IdClaveContable2") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cuenta Contable2">
                            <FooterTemplate>
                                <asp:TextBox ID="txtIdCuentaContable2" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIdCuentaContable2" runat="server" Text='<%# Bind("IdCuentaContable2") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarIdCuentaContable2" runat="server" Width="90%" Text='<%# Bind("IdCuentaContable2") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Centro Costo2">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoIdCentroCosto2" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIdCentroCosto2" runat="server" Text='<%# Bind("IdCentroCosto2") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarIdCentroCosto2" runat="server" Width="90%" Text='<%# Bind("IdCentroCosto2") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Centro Beneficio2">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoIdCentroBeneficio2" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIdCentroBeneficio2" runat="server" Text='<%# Bind("IdCentroBeneficio2") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarIdCentroBeneficio2" runat="server" Width="90%" Text='<%# Bind("IdCentroBeneficio2") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Elemento PEP2">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoIdCentroBeneficio2" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIdElementoPEP2" runat="server" Text='<%# Bind("IdElementoPEP2") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarIdElementoPEP2" runat="server" Width="90%" Text='<%# Bind("IdElementoPEP2") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PosPre2">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoIdPosPre2" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIdPosPre2" runat="server" Text='<%# Bind("IdPosPre2") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarIdPosPre2" runat="server" Width="90%" Text='<%# Bind("IdPosPre2") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Centro Gestor2">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoIdCentroGestor2" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIdCentroGestor2" runat="server" Text='<%# Bind("IdCentroGestor2") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarIdCentroGestor2" runat="server" Width="90%" Text='<%# Bind("IdCentroGestor2") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Programa2">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoIdPrograma2" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIdPrograma2" runat="server" Text='<%# Bind("IdPrograma2") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarIdPrograma2" runat="server" Width="90%" Text='<%# Bind("IdPrograma2") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Fondo2">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoIdFondo2" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIdFondo2" runat="server" Text='<%# Bind("IdFondo2") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarIdFondo2" runat="server" Width="90%" Text='<%# Bind("IdFondo2") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Doc Presupuestario2">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoDocPresupuestario2" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDocPresupuestario2" runat="server" Text='<%# Bind("DocPresupuestario2") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarDocPresupuestario2" runat="server" Width="90%" Text='<%# Bind("DocPresupuestario2") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PosDoc Presupuestario2">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoPosDocPresupuestario2" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPosDocPresupuestario2" runat="server" Text='<%# Bind("PosDocPresupuestario2") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarPosDocPresupuestario2" runat="server" Width="90%" Text='<%# Bind("PosDocPresupuestario2") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Flujo Efectivo2">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoFlujoEfectivo2" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFlujoEfectivo2" runat="server" Text='<%# Bind("FlujoEfectivo2") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarFlujoEfectivo2" runat="server" Width="90%" Text='<%# Bind("FlujoEfectivo2") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="NICSP242">
                            <FooterTemplate>
                                <asp:TextBox ID="txtNuevoNICSP242" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblNICSP242" runat="server" Text='<%# Bind("NICSP242") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditarNICSP242" runat="server" Width="90%" Text='<%# Bind("NICSP242") %>' MaxLength="100" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Estado">
                            <FooterTemplate>
                                <asp:CheckBox ID="cbNuevoEstado" runat="server"></asp:CheckBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("Estado")%>' Visible="false"></asp:Label>
                                <asp:CheckBox ID="cbEditarEstado" runat="server" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                            <EditItemTemplate>
                                <asp:LinkButton ID="lnkActualizarParametro" runat="server" CausesValidation="True" CommandName="Update" Text="Actualizar"></asp:LinkButton>
                                <asp:LinkButton ID="lnkCancelarParametro" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtEditarParametro" runat="server" CausesValidation="False" Text="Editar" CommandName="Edit"></asp:LinkButton>
                                <asp:LinkButton ID="lbtEliminarParametro" runat="server" CausesValidation="false" Text="Eliminar" CommandName="Delete" OnClientClick="return confirm('Está seguro que desea eliminar este registro?');"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                    <EditRowStyle BackColor="#999999" />

                </asp:GridView>
            </div>

</asp:Content>
