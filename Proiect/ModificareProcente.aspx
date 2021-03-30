<%@ Page Title="" Language="C#" MasterPageFile="~/Proiect.Master" AutoEventWireup="true" CodeBehind="ModificareProcente.aspx.cs" Inherits="Proiect.WebForm2" UnobtrusiveValidationMode="none" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .buton {
            border-radius: 12px;
            background-color: cornflowerblue;
            color: ghostwhite;
            padding: 5px;
        }

            .buton:hover {
                background-color: #7FCEC5;
                font-weight: 600;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="border-left-color: cornflowerblue; border-right-color: cornflowerblue; border-bottom-color: cornflowerblue; border-top-color: aliceblue; border-style: solid; border-radius: 0px 0px 12px 12px; padding: 0px 15px 15px 15px;">
        <h2 style="text-align: center;">Modificare procente</h2>
        <br />

        <div id="autentificareDiv" class="lead" style="text-align: center;" runat="server">
            <table style="margin-left: auto; margin-right: auto;">
                <tr>
                    <td style="text-align: left;">
                        <asp:Label ID="Label1" runat="server" Text="Parola"></asp:Label>&nbsp;
                    </td>
                    <td style="text-align: left; padding: 5px;">
                        <asp:TextBox ID="parolaTB" runat="server" TextMode="Password" MaxLength="30"></asp:TextBox>&nbsp;
                    </td>
                    <td>
                        <asp:Button ID="autentificareBtn" runat="server" Text="Autentificare" CssClass="buton" OnClick="autentificareBtn_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center; padding: 5px;">
                        <asp:Label ID="LabelAutentificareErr" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>

        <div id="tabelProcenteDiv" class="lead" style="text-align: center; display: none;" runat="server">
            <div id="modificareParolaDiv" runat="server" style="display: none;">
                <table style="margin-left: auto; margin-right: auto;">
                    <tr>
                        <td>
                            <asp:Button ID="modificareParolaBtn" runat="server" Text="Modificare parola" CssClass="buton" />&emsp;
                            <asp:Button ID="iesireBtn" runat="server" Text="Iesire" CssClass="buton" OnClick="iesireBtn_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Panel ID="parolaNouaPanel" runat="server" HorizontalAlign="Center">
                    <table style="margin-left: auto; margin-right: auto;">
                        <tr>
                            <td style="text-align: left;">
                                <asp:Label ID="Label2" runat="server" Text="Parola veche:"></asp:Label>&nbsp;
                            </td>
                            <td style="text-align: left; padding: 5px;">
                                <asp:TextBox ID="parolaVecheTB" runat="server" TextMode="Password" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center; padding: 5px;">
                                <asp:Label ID="LabelParolaVecheErr" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">
                                <asp:Label ID="Label3" runat="server" Text="Parola noua:"></asp:Label>&nbsp;
                            </td>
                            <td style="text-align: left; padding: 5px;">
                                <asp:TextBox ID="parolaNouaTB" runat="server" TextMode="Password" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center; padding: 5px;">
                                <asp:Label ID="LabelParolaNouaErr" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">
                                <asp:Label ID="Label4" runat="server" Text="Confirmare parola:"></asp:Label>&nbsp;
                            </td>
                            <td style="text-align: left; padding: 5px;">
                                <asp:TextBox ID="confirmareParolaTB" runat="server" TextMode="Password" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center; padding: 5px;">
                                <asp:Label ID="LabelConfirmareParoalErr" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center; padding: 5px;">
                                <asp:Label ID="LabelSalvareParolaErr" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center; padding: 5px;">
                                <asp:Button ID="salvareParolaBtn" runat="server" Text="Salvare" CssClass="buton" OnClick="salvareParolaBtn_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
                    CollapseControlID="modificareParolaBtn" Collapsed="true" ExpandControlID="modificareParolaBtn"
                    TargetControlID="parolaNouaPanel" TextLabelID="modificareParolaBtn" />
            </div>
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="GhostWhite" DataKeyNames="ID" DataSourceID="SqlDataSource1" OnRowDataBound="GridView1_RowDataBound" HorizontalAlign="Center">
                <Columns>
                    <asp:CommandField EditText="Editare" ShowEditButton="True" CancelText="Anulare" UpdateText="Salvare" />
                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" Visible="False" />
                    <asp:TemplateField HeaderText="CAS (%)" SortExpression="CASPROCENT">
                        <EditItemTemplate>
                            <asp:TextBox ID="CASGW" runat="server" Text='<%# Bind("CASPROCENT") %>' TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="CASGW" Display="Dynamic" ErrorMessage="&lt;br /&gt;Nu ati introdsu valoarea CAS-ului!" Font-Size="90%" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="CASGW" Display="Dynamic" ErrorMessage="&lt;br /&gt;Valoarea CAS-ului nu poate fi negativa &lt;br /&gt;si nici mai mare de cat 100!" Font-Size="90%" ForeColor="Red" ValidationExpression="^[0-9]{1,2}$|100"></asp:RegularExpressionValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("CASPROCENT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CASS (%)" SortExpression="CASSPROCENT">
                        <EditItemTemplate>
                            <asp:TextBox ID="CASSGW" runat="server" Text='<%# Bind("CASSPROCENT") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="CASSGW" Display="Dynamic" ErrorMessage="&lt;br /&gt;Nu ati introdsu valoarea CASS-ului!" Font-Size="90%" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="CASSGW" Display="Dynamic" ErrorMessage="&lt;br /&gt;Valoarea CASS-ului nu poate fi negativa &lt;br /&gt;si nici mai mare de cat 100!" Font-Size="90%" ForeColor="Red" ValidationExpression="^[0-9]{1,2}$|100"></asp:RegularExpressionValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("CASSPROCENT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IMPOZIT (%)" SortExpression="IMPOZITPROCENT">
                        <EditItemTemplate>
                            <asp:TextBox ID="impozitGW" runat="server" Text='<%# Bind("IMPOZITPROCENT") %>'></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="impozitGW" Display="Dynamic" ErrorMessage="&lt;br /&gt;Nu ati introdsu valoarea impozitului!" Font-Size="90%" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="impozitGW" Display="Dynamic" ErrorMessage="&lt;br /&gt;Valoarea impozitului nu poate fi negativa &lt;br /&gt;si nici mai mare de cat 100!" Font-Size="90%" ForeColor="Red" ValidationExpression="^[0-9]{1,2}$|100"></asp:RegularExpressionValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("IMPOZITPROCENT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" OldValuesParameterFormatString="original_{0}" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT ID, CASPROCENT, CASSPROCENT, IMPOZITPROCENT FROM PROCENTE WHERE (ID = 1)" UpdateCommand="UPDATE PROCENTE SET CASPROCENT = :CASPROCENT, CASSPROCENT = :CASSPROCENT, IMPOZITPROCENT = :IMPOZITPROCENT WHERE (ID = :original_ID)">
                <UpdateParameters>
                    <asp:Parameter Name="CASPROCENT" Type="Decimal" />
                    <asp:Parameter Name="CASSPROCENT" Type="Decimal" />
                    <asp:Parameter Name="IMPOZITPROCENT" Type="Decimal" />
                    <asp:Parameter Name="original_ID" Type="Decimal" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
