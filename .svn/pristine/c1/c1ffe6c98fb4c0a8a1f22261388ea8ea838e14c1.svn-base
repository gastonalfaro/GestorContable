<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmNemotecnicos.aspx.cs" Inherits="Presentacion.Mantenimiento.frmNemotecnicos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones"></div> 
    <div class="col-md-12" id="tblNemotecnicos">
            <h3>NEMOTÉCNICOS</h3>
            <p>Mantenimiento de Nemotécnicos del Sistema Gestor.</p>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="lblIdNemotecnico" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusquedaIdNemotecnico" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="lblBusquedaNomNemotecnico" runat="server" Text="Nombre:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusquedaNomNemotecnico" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="lblBusquedaIdSociedadFi" runat="server" Text="Sociedad:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusquedaIdSociedadFi" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="lblBusquedaIdMoneda" runat="server" Text="Moneda:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusquedaIdMoneda" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="lblBusquedaTipoNemotecnico" runat="server" Text="Tipo Nemotécnico:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusquedaTipoNemotecnico" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnConsultarNemotecnicos" runat="server" Text="CONSULTAR" OnClick="btnConsultarNemotecnicos_Click" CssClass="ButtonNeutro" /></div>
             <div class="col-md-12"><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>    
    </div>
    <div style="width: 100%; height: 100%; overflow: auto">
        <asp:GridView ID="grdNemotecnicos" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
            Width="100%" OnSelectedIndexChanged="grdNemotecnicos_SelectedIndexChanged" OnRowEditing="grdNemotecnicos_RowEditing"
            OnRowUpdating="grdNemotecnicos_RowUpdating" OnPageIndexChanging="grdNemotecnicos_PageIndexChanging"
            OnDataBound="grdNemotecnicos_DataBound" OnRowDataBound="grdNemotecnicos_RowDataBound"
            OnRowCancelingEdit="grdNemotecnicos_RowCancelingEdit" OnSorting="grdNemotecnicos_Sorting" AllowSorting="True"
            AllowPaging="true" PageSize="20" ShowFooter="true"  CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                        <Columns>
                            <asp:TemplateField HeaderText="FechaModifica" Visible="false"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblFechaModifica" runat="server" Text='<%# Bind("FchModifica") %>'></asp:Label>
                                </ItemTemplate>  
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Center" SortExpression="IdNemotecnico"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdNemotecnico" runat="server" Text='<%# Bind("IdNemotecnico") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertarIdNemotecnico" runat="server" Text='<%# Bind("IdNemotecnico") %>'/>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Nombre" SortExpression="NomNemotecnico" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblNomNemotecnico" runat="server"  Text='<%# Bind("NomNemotecnico") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditarNomNemotecnico" runat="server"  Text='<%# Bind("NomNemotecnico") %>'></asp:TextBox>
                                </EditItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertarNomNemotecnico" runat="server" ></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sociedad" ItemStyle-HorizontalAlign="Center" SortExpression="IdSociedadFi"> 
                                <ItemTemplate>
                                    <%--<asp:DropDownList ID="ddlItemIdSociedadFi" runat="server" Enabled="false" ></asp:DropDownList>--%>
                                    <asp:Label ID="lblIdSociedadFi" runat="server"  Text='<%# Bind("IdSociedadFi") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblIdSociedadFi" runat="server" Text='<%# Eval("IdSociedadFi")%>' Visible = "false"></asp:Label>
                                    <asp:DropDownList ID="ddlEditarIdSociedadFi" runat="server" ></asp:DropDownList>
                                </EditItemTemplate>                           
                                <FooterTemplate>
				                    <asp:DropDownList ID="ddlInsertarIdSociedadFi" runat="server" ></asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Moneda" ItemStyle-HorizontalAlign="Center" SortExpression="IdMoneda"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdMoneda" runat="server"  Text='<%# Bind("IdMoneda") %>'></asp:Label>
                                    <%--<asp:DropDownList ID="ddlItemMoneda" runat="server" Enabled="false"></asp:DropDownList>--%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblIdMoneda" runat="server" Text='<%# Eval("IdMoneda")%>' Visible = "false"></asp:Label>
                                    <asp:DropDownList ID="ddlEditarIdMoneda" runat="server" ></asp:DropDownList>
                                </EditItemTemplate>                           
                                <FooterTemplate>
				                    <asp:DropDownList ID="ddlInsertarIdMoneda" runat="server" ></asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Tipo Nemotécnico" ItemStyle-HorizontalAlign="Center" SortExpression="TipoNemotecnico"> 
                                <ItemTemplate>
                                   <%--<asp:DropDownList ID="ddlTipoNemotecnico" runat="server" Enabled="false"></asp:DropDownList>--%>
                                   <asp:Label ID="lblTipoNemotecnico" runat="server"  Text='<%# Bind("TipoNemotecnico") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblTipoNemotecnico" runat="server" Text='<%# Eval("TipoNemotecnico")%>' Visible = "false"></asp:Label>
                                    <asp:DropDownList ID="ddlEditarTipoNemotecnico" runat="server"></asp:DropDownList>
                                </EditItemTemplate>                           
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlInsertarTipoNemotecnico" runat="server" ></asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
 
                            <asp:TemplateField HeaderText="Módulo SINPE" ItemStyle-HorizontalAlign="Center" SortExpression="ModuloSINPE"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblModuloSINPE" runat="server"  Text='<%# Bind("ModuloSINPE") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblModuloSINPE" runat="server" Text='<%# Eval("ModuloSINPE")%>' Visible = "false"></asp:Label>
                                    <asp:DropDownList ID="ddlEditarModuloSINPE" runat="server" ></asp:DropDownList>
                                </EditItemTemplate>                           
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlInsertarModuloSINPE" runat="server" ></asp:DropDownList>    
                                </FooterTemplate>
                            </asp:TemplateField>
 
                            <asp:TemplateField HeaderText="Id Cuenta Contable CP" ItemStyle-HorizontalAlign="Center" Visible="false" SortExpression="IdCuentaContableCP"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdCuentaContableCP" runat="server"  Text='<%# Bind("IdCuentaContableCP") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditarContableCP" runat="server" Text='<%# Bind("IdCuentaContableCP") %>'></asp:TextBox>
                                 </EditItemTemplate>                           
                                <FooterTemplate>
                                    <asp:TextBox ID="txtInsertarContableCP" runat="server" ></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Id Cuenta Contable LP" ItemStyle-HorizontalAlign="Center"  Visible="false"  SortExpression="IdCuentaContableLP"> 
                                <ItemTemplate>
                                   <asp:Label ID="lblIdCuentaContableLP" runat="server"  Text='<%# Bind("IdCuentaContableLP") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditarIdCuentaContableLP" runat="server" Text='<%# Bind("IdCuentaContableLP") %>'></asp:TextBox>
                                 </EditItemTemplate>                           
                                <FooterTemplate>
                                    <asp:TextBox ID="txtInsertarIdCuentaContableLP" runat="server" ></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                       
                            <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center" >
                                <ItemTemplate>
                                    <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                     <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("Estado")%>' Visible = "false"></asp:Label>
				                    <asp:CheckBox ID="cbEditarEstado" runat="server" />
                                </EditItemTemplate> 
                                <FooterTemplate>
                                    <asp:CheckBox ID="cbInsertarEstado" runat="server"></asp:CheckBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                                <EditItemTemplate> 
                                    <asp:LinkButton ID="lnkActualizarDir" runat="server" CausesValidation="True" CommandName="Update" Text="Actualizar"></asp:LinkButton> 
                                    <asp:LinkButton ID="lnkCancelarDir" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton> 
                                </EditItemTemplate> 
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtEditarDireccion" runat="server" CausesValidation="False" Text="Editar" CommandName="Edit" ></asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtAgregarDireccion" runat="server" CausesValidation="False" Text="Agregar" CommandName="Select" ></asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateField>

                            </Columns>  

                            <EditRowStyle BackColor="#999999" HorizontalAlign="Center" />

                        </asp:GridView>

    </div>
</asp:Content>
