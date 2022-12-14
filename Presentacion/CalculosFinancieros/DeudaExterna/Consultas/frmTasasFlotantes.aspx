<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmTasasFlotantes.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaExterna.frmTasasFlotantes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
    <style type="text/css">
        .divStyle {
            margin-top: 1%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="col-md-12">
        <h3>Tasas Flotantes</h3>
    </div>
    <div class="col-md-12">Consulta de Tasas Flotantes del Sistema Gestor.</div>

    <div class="col-md-12 divStyle">
        <div class="col-md-6 divStyle">
            <div class="col-md-4">
                <asp:Label ID="Label1" runat="server" Text="Id Préstamo:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtBusqIdPrestamo" runat="server" MaxLength="15" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-4">
                <asp:Label ID="NumeroTramoLabel" runat="server" Text="Número Tramo: "></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtBusqIdTramo" runat="server" MaxLength="15" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-4">
                <asp:Label ID="APartirLabel" runat="server" Text="A Partir de: "></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtAPartir" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-12 divStyle">
            <div style="text-align: center;">
                <asp:Button ID="btnFormulariosConsultar" runat="server" Text="CONSULTAR" OnClick="btnFormulariosConsultar_Click" CssClass="ButtonNeutro" />
            </div>
        </div>

        <div class="col-md-12 divStyle">
            <asp:Label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true"></asp:Label>
        </div>
    </div>

    <div style="width: 100%; height: 100%; overflow: auto">
        <div class="divStyle">
            <asp:GridView ID="grdvFormularios" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                Width="100%" CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                PageSize="15" AllowPaging="True" OnPageIndexChanging="grdvFormularios_PageIndexChanging">
                <Columns>

                    <asp:TemplateField HeaderText="Id Préstamo" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIdPrestamo" runat="server" Text='<%# Bind("IdPrestamo") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtInsertIdPrestamo" runat="server" Text='<%# Bind("IdPrestamo") %>' MaxLength="32" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Id Tramo" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIdTramo" runat="server" Text='<%# Bind("IdTramo") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtIdtramo" runat="server" Text='<%# Bind("IdTramo") %>' MaxLength="32" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="A Partir de" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblAPartir" runat="server" Text='<%# (Eval("APartir")??"").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAPartir" runat="server" Text='<%# (Eval("APartir")??"").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>' MaxLength="32" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Tasa" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblTasa" runat="server" Text='<%# Bind("Tasa") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtTasa" runat="server" Text='<%# Bind("Tasa") %>' MaxLength="32" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtEstado" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditEstado" runat="server" Width="90%" Text='<%# Bind("Estado") %>' MaxLength="100" />
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                </Columns>

                <EditRowStyle BackColor="#999999" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
