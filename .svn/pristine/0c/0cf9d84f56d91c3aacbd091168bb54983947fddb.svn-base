<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmParametros.aspx.cs" Inherits="Presentacion.Mantenimiento.frmParametros" %>

<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoJS" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones">
        <asp:Button ID="btnParametroNuevo" runat="server" Text="NUEVO" OnClick="btnParametroNuevo_Click" CssClass="ButtonNeutro"/>
        <asp:Button ID="btnParametroGuardar" runat="server" Text="GUARDAR" OnClick="btnParametroGuardar_Click" Visible="false" CssClass="ButtonNeutro"/>
        <asp:Button ID="btnParametroVolver" runat="server" Text="VOLVER" OnClick="btnParametroVolver_Click" Visible="false" CssClass="ButtonNeutro"/>
               
    </div> 
    <div class="col-md-12" id="tblParametros">
        <h3>PARÁMETROS</h3>
        <p>Mantenimiento de Parámetros del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqIdParametro" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqDesParametro" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label4" runat="server" Text="Id Módulo:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:DropDownList ID="ddlBusqIdModulo" runat="server" CssClass="FormatoDropDownList"></asp:DropDownList></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label3" runat="server" Text="Fecha Vigencia:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"> 
                <asp:TextBox ID="txtBusqFchVigencia" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"> <asp:Label ID="Label5" runat="server" Text="Tipo Parámetro:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqTipoParametro" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnParametroConsultar" runat="server" Text="CONSULTAR" OnClick="btnParametroConsultar_Click" CssClass="ButtonNeutro" /></div>
    </div>
    <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    <div style="width: 100%; height: 100%; overflow: auto">
        <asp:GridView ID="grdvParametros" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
             CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="grdvParametros_SelectedIndexChanged" OnRowEditing="grdvParametros_RowEditing"
            OnRowUpdating="grdvParametros_RowUpdating" OnRowUpdated="grdvParametros_RowUpdated" OnPageIndexChanging="grdvParametros_PageIndexChanging"
            OnRowCancelingEdit="grdvParametros_RowCancelingEdit" OnSorting="grdvParametros_Sorting" AllowSorting="false"
                         AllowPaging="true" PageSize="20">
                        <Columns>

                            <asp:TemplateField HeaderText="Código" SortExpression="IdParametro"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdParametro" runat="server" Text='<%# Bind("IdParametro") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdParametro" runat="server"  Text='<%# Bind("IdParametro") %>' MaxLength="15" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" SortExpression="DesParametro">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomNuevoDesParametro" runat="server" Text='<%# Bind("DesParametro") %>'></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDesParametro" runat="server" Text='<%# Bind("DesParametro") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditDesParametro" runat="server" Width="90%" Text='<%# Bind("DesParametro") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Fecha Vigencia" SortExpression="FchVigencia">
                         <%--       <FooterTemplate>
                                    <asp:TextBox ID="txtNuevaFchVigencia" runat="server"></asp:TextBox>
                                </FooterTemplate>--%>
                                <ItemTemplate>
                                    <asp:Label ID="lblFchVigencia" runat="server"  Text='<%# Bind("FchVigencia") %>'></asp:Label>
                                </ItemTemplate>
                                <%--<EditItemTemplate>
                                    <ew:CalendarPopup ID="calEditFchVigencia" runat="server" SelectedDate='<%# Bind("FchVigencia") %>' style="margin-right: 0px" Culture="es-MX" Nullable="True" ShowGoToToday="True">
                                    </ew:CalendarPopup>
				                </EditItemTemplate> --%>
                            </asp:TemplateField>

                        <asp:TemplateField HeaderText="Fecha Modifica" Visible="false" >
                                <ItemTemplate>
                                    <asp:Label ID="lblFchModifica" runat="server"  Text='<%# Bind("FchModifica") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Módulo" SortExpression="IdModulo"> 
                                <FooterTemplate>
				                    <asp:TextBox ID="txtNuevaIdModulo" runat="server" Text='<%# Bind("IdModulo") %>'></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdModulo" runat="server" Text='<%# Bind("IdModulo") %>'></asp:Label>
                                </ItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tipo" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNuevaTipoParametro" runat="server" Text='<%# Bind("TipoParametro") %>'></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTipoParametro" runat="server" Text='<%# Bind("TipoParametro") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditTipoParametro" runat="server" Width="90%" Text='<%# Bind("TipoParametro") %>' MaxLength="2"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Valor" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNuevaValor" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblValor" runat="server" Text='<%# Bind("Valor") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditValor" runat="server" Width="90%" Text='<%# Bind("Valor") %>' MaxLength="300"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                                <EditItemTemplate> 
                                    <asp:LinkButton ID="lnkActualizarParametro" runat="server" CausesValidation="True" CommandName="Update" Text="Aceptar"></asp:LinkButton> 
                                    <asp:LinkButton ID="lnkCancelarParametro" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton> 
                                </EditItemTemplate> 
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtEditarParametro" runat="server" CausesValidation="False" Text="Editar" CommandName="Edit" OnClick="lbtEditarParametro_Click"></asp:LinkButton>
                                    <asp:LinkButton ID="lbtDuplicarParametro" runat="server" CausesValidation="False" Text="Duplicar" CommandName="Update" OnClick="lbtDuplicarParametro_Click"></asp:LinkButton>
                                </ItemTemplate>
                                <InsertItemTemplate>

                                </InsertItemTemplate>
                            </asp:TemplateField>

                        </Columns>  

                        <EditRowStyle BackColor="#999999" />

                    </asp:GridView>
    </div>
</asp:Content>
