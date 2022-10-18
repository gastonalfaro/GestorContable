<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmPaises.aspx.cs" Inherits="Presentacion.Mantenimiento.frmPaises" %>

<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones">
        <asp:Button ID="btnPaisNuevo" runat="server" Text="NUEVO" OnClick="btnPaisNuevo_Click" Visible="false" CssClass="ButtonNeutro"/>
        <asp:Button ID="btnPaisGuardar" runat="server" Text="GUARDAR" OnClick="btnPaisGuardar_Click" Visible="false" CssClass="ButtonNeutro"/>
        <asp:Button ID="btnPaisesVolver" runat="server" Text="VOLVER" OnClick="btnPaisesVolver_Click" Visible="false" CssClass="ButtonNeutro"/>
            
    </div> 
    <div class="col-md-12" id="tblPaiss">
         <h3>PAÍSES</h3>
        <p>Consulta de Países del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqIdPais" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqNomPais" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnPaisesConsultar" runat="server" Text="CONSULTAR" OnClick="btnPaisesConsultar_Click" CssClass="ButtonNeutro"/></div>
        <div class="col-md-12"><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    </div>
    <div style="width: 100%; height: 100%; overflow: auto">
        
        <asp:GridView ID="grdvPaises" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
             CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="grdvPaises_SelectedIndexChanged" OnRowEditing="grdvPaises_RowEditing"
            OnPageIndexChanging="grdvPaises_PageIndexChanging"
            OnRowCancelingEdit="grdvPaises_RowCancelingEdit" AllowPaging="true" PageSize="20">
                        <Columns>

                            <asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Center"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdPais" runat="server" Text='<%# Bind("IdPais") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdPais" runat="server"  Text='<%# Bind("IdPais") %>' MaxLength="4" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomNuevoPais" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNomPais" runat="server" Text='<%# Bind("NomPais") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditNomPais" runat="server" Width="90%" Text='<%# Bind("NomPais") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Nacionalidad" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomNuevoPais" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNomPais" runat="server" Text='<%# Bind("Nacionalidad") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditNomPais" runat="server" Width="90%" Text='<%# Bind("Nacionalidad") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Moneda" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtMonedaNuevo" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMoneda" runat="server" Text='<%# Bind("IdMoneda") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditMoneda" runat="server" Width="90%" Text='<%# Bind("IdMoneda") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>
                        </Columns>  

            <EditRowStyle BackColor="#999999" />

                    </asp:GridView>
    </div>
</asp:Content>
