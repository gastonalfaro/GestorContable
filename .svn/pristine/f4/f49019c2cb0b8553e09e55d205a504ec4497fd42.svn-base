<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmCuentasBancarias.aspx.cs" Inherits="Presentacion.Mantenimiento.frmCuentasBancarias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    
    <div class="col-md-12" style="text-align:center;">
        <asp:Button ID="btnBancoVolver" runat="server" Text="VOLVER" OnClick="btnCtaBancoVolver_Click" CssClass="ButtonNeutro"/>
    </div>
    <div class="col-md-12"><h3>Detalle de la cuenta bancaria</h3></div>
     
    <div style="width:100%;">
        <asp:GridView ID="grdvCuentasBancos" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
             CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnPageIndexChanging="grdvBancos_PageIndexChanging" AllowPaging="True" PageSize="20">
                        <Columns>

                            <asp:TemplateField HeaderText="Id de Cuenta Bancaria" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIDCuentaBancaria" runat="server" Text='<%# Bind("IDCuentaBancaria") %>'></asp:Label>
                                </ItemTemplate>
                        
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Cuenta Bancaria" >  
                                <ItemTemplate>
                                    <asp:Label ID="lblCuentaBancaria" runat="server" Text='<%# Bind("CuentaBancaria") %>'></asp:Label>
                                </ItemTemplate>                             
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="ID Cuenta Contable" >                               
                                <ItemTemplate>
                                    <asp:Label ID="lblIDCuentaContable" runat="server" Text='<%# Bind("IDCuentaContable") %>'></asp:Label>
                                </ItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sociedad" >
                                <ItemTemplate>
                                    <asp:Label ID="lblSociedad" runat="server" Text='<%# Bind("IDSociedadFi") %>'></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tipo de Cuenta" >                               
                              <ItemTemplate>
                                    <asp:Label ID="lblTipoCuenta" runat="server" Text='<%# Bind("TipoCuenta") %>'></asp:Label>
                                </ItemTemplate>   
                            </asp:TemplateField>

                        </Columns>  

            <EditRowStyle BackColor="#999999" />
                    </asp:GridView>

    </div>

</asp:Content>
