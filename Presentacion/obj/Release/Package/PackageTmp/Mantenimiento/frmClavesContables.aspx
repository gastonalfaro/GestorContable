<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmClavesContables.aspx.cs" Inherits="Presentacion.Mantenimiento.frmClavesContables" %>
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
        <asp:Button ID="btnClaveContableNuevo" runat="server" Text="NUEVO" OnClick="btnClaveContableNuevo_Click" CssClass="ButtonNeutro" />
        <asp:Button ID="btnClaveContableGuardar" runat="server" Text="GUARDAR" OnClick="btnClaveContableGuardar_Click" Visible="false" CssClass="ButtonNeutro"/>
        <asp:Button ID="btnClaveContableVolver" runat="server" Text="VOLVER" OnClick="btnClaveContableVolver_Click" Visible="false" CssClass="ButtonNeutro"/>
            
    </div> 
    <div class="col-md-12" id="tblClavesContables">
        <h3>CLAVES CONTABLES</h3>
        <p>Mantenimiento de Claves Contables del Sistema Gestor.</p>
            <div class="col-md-6">
                <div class="col-md-3"> <asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusqIdClaveContable" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-3"> <asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusqNomClaveContable" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-12" style="text-align:center;"> <asp:Button ID="btnClaveContableConsultar" runat="server" Text="CONSULTAR" OnClick="btnClaveContableConsultar_Click" CssClass="ButtonNeutro"/></div>
        <div class="col-md-12" style="text-align:center;"> <asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    </div>
    <div style="width: 100%; height: 100%; overflow: auto">
       
        <asp:GridView ID="grdvClavesContables" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
             CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="grdvClavesContables_SelectedIndexChanged" OnRowEditing="grdvClavesContables_RowEditing"
            OnRowUpdating="grdvClavesContables_RowUpdating" OnPageIndexChanging="grdvClavesContables_PageIndexChanging"
            OnRowCancelingEdit="grdvClavesContables_RowCancelingEdit" PageSize="20" AllowPaging="True">
                        <Columns>

                            <asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Center"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdClaveContable" runat="server" Text='<%# Bind("IdClave") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdClaveContable" runat="server"  Text='<%# Bind("IdClave") %>' MaxLength="4" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomNuevoNomClave" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNomClave" runat="server" Text='<%# Bind("NomClave") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditNomClave" runat="server" Width="90%" Text='<%# Bind("NomClave") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tipo Clave" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomNuevoTipoClave" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTipoClave" runat="server" Text='<%# Bind("TipoClave") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditTipoClave" runat="server" Width="90%" Text='<%# Bind("TipoClave") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Clase Clave" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomNuevoClaseClave" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblClaseClave" runat="server" Text='<%# Bind("ClaseClave") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditClaseClave" runat="server" Width="90%" Text='<%# Bind("ClaseClave") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Estado" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtEstado" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditEstado" runat="server" Width="90%" Text='<%# Bind("Estado") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                        </Columns>  

                        <EditRowStyle BackColor="#999999" />

                    </asp:GridView>             
    </div>
</asp:Content>
