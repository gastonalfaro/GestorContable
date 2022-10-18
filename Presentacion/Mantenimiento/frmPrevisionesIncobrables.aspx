<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmPrevisionesIncobrables.aspx.cs" Inherits="Presentacion.Mantenimiento.frmPrevisionesIncobrables" %>
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
        <asp:Button ID="btnNuevosPrevisionesIncobrables" runat="server" Text="NUEVO" OnClick="btnNuevosPrevisionesIncobrables_Click" CssClass="ButtonNeutro" />
              
    </div> 
    <div class="col-md-12" id="tblPrevisionesIncobrables">
        <h3>PrevisionesIncobrables</h3>
        <p>Mantenimiento de Previsiones Incobrables del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblDiasMorosidad" runat="server" Text="Días Morosidad:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtDiasMorosidad" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblPorcEstimacion" runat="server" Text="Porcentaje de Estimación:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtPorcEstimacion" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblDescripcion" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"> <asp:TextBox ID="txtDescripcion" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnConsultarPrevisionIncobrable" runat="server" Text="CONSULTAR" OnClick="btnConsultarPrevisionIncobrable_Click" CssClass="ButtonNeutro"/></div>
    </div>
    <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    <div style="width: 100%; height: 100%; overflow: auto">
<asp:GridView ID="grdPrevisionesIncobrables" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
        CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
        Width="100%" OnSelectedIndexChanged="grdPrevisionesIncobrables_SelectedIndexChanged" OnRowEditing="grdPrevisionesIncobrables_RowEditing"
        OnRowUpdating="grdPrevisionesIncobrables_RowUpdating" OnPageIndexChanging="grdPrevisionesIncobrables_PageIndexChanging"

        OnRowCancelingEdit="grdPrevisionesIncobrables_RowCancelingEdit" AllowPaging="true" PageSize="20">
                    <Columns>
                        <asp:TemplateField HeaderText="Días de Morosidad:" > 
                            <ItemTemplate>
                                <asp:Label ID="lblDiasMorosidad" runat="server" Text='<%# Bind("DiasMorosidad") %>'></asp:Label>
                            </ItemTemplate> 
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Porcentaje de Estimación" > 
                            <ItemTemplate>
                                <asp:Label ID="lblPorcEstimacion" runat="server"  Text='<%# Bind("PorcEstimacion") %>'></asp:Label>
                            </ItemTemplate> 
                             <EditItemTemplate>
				                <asp:TextBox ID="txtPorcEstimacion" runat="server" Text='<%# Bind("PorcEstimacion") %>'  />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Descripción" >
                            <ItemTemplate>
                                <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
				                <asp:TextBox ID="txtEditarDescripcion" runat="server" Text='<%# Bind("Descripcion") %>' MaxLength="100"  />
                            </EditItemTemplate> 
                        </asp:TemplateField>
                                        
                        <asp:TemplateField HeaderText="FchModifica" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblFchModifica" runat="server" Text='<%# Bind("FchModifica") %>'></asp:Label>
                            </ItemTemplate>                        

                        </asp:TemplateField>

                        
                            <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                                <EditItemTemplate> 
                                    <asp:LinkButton ID="lnkActualizarDir" runat="server" CausesValidation="True" CommandName="Update" Text="Actualizar"></asp:LinkButton> 
                                    <asp:LinkButton ID="lnkCancelarDir" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton> 
                                </EditItemTemplate> 
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtEditarPrevisionIncobrables" runat="server" CausesValidation="False" Text="Editar" CommandName="Edit" ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                    </Columns>  

                    <EditRowStyle BackColor="#999999" />

                </asp:GridView>

    </div>
</asp:Content>
