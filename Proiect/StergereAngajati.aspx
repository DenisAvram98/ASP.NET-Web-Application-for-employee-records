<%@ Page Title="" Language="C#" MasterPageFile="~/Proiect.Master" AutoEventWireup="true" CodeBehind="StergereAngajati.aspx.cs" Inherits="Proiect.WebForm5" MaintainScrollPositionOnPostback="true" UnobtrusiveValidationMode="none"%>

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
        <h2 style="text-align: center;">Stergere angajati</h2>
        <br />

        <div class="lead">
            <div style="padding-left: 50px;">
                <table>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <p>Cautati angajatul dupa:</p>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label ID="Label1" runat="server" Text="Nume"></asp:Label>&nbsp;
                        </td>
                        <td style="text-align: left; padding: 5px;">
                            <asp:TextBox ID="numeTB" runat="server" MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; padding: 5px; font-size: 90%;">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Numele poate sa contina doar litele <br />si caractere speciale(',-, )." EnableClientScript="true" ControlToValidate="numeTB" ForeColor="Red" Display="Dynamic" ValidationExpression="^[a-zA-Z][a-zA-Z''-' ]{0,29}$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label ID="Label2" runat="server" Text="Prenume"></asp:Label>&nbsp;
                        </td>
                        <td style="text-align: left; padding: 5px;">
                            <asp:TextBox ID="prenumeTB" runat="server" MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; padding: 5px; font-size: 90%;">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Prenumele poate sa contina doar litele <br />si caractere speciale(',-, )." EnableClientScript="true" ControlToValidate="prenumeTB" ForeColor="Red" Display="Dynamic" ValidationExpression="^[a-zA-Z][a-zA-Z''-' ]{0,29}$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="padding: 5px; text-align: center; width:363px;">
                            <asp:Label ID="LabelCautareErr" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 5px; text-align:center;">
                            <asp:Button ID="cautaBtn" runat="server" Text="Cauta" CssClass="buton" OnClick="cautaBtn_Click" />&emsp;
                            <asp:Button ID="anulareBtn" runat="server" Text="Anulare cautare" CssClass="buton" OnClick="anulareBtn_Click" CausesValidation="False" />&emsp;
                            <asp:Button ID="stergeBtn" runat="server" Text="Sterge" CssClass="buton" Visible="false" OnClick="stergeBtn_Click" OnClientClick="return ConfirmareStergere()"/>
                            <script type="text/javascript">
                                function ConfirmareStergere() {
                                    return confirm("Confirmati stergerea?");
                                }
                            </script>
                        </td>
                    </tr>
                </table>
            </div>

            <br />
            <div style="text-align: center; overflow: auto; height: 250px;">
                <asp:Label ID="LabelPrincipalErr" runat="server" Text=""></asp:Label>
                <asp:GridView ID="GridView1" runat="server" BackColor="GhostWhite" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                    <SelectedRowStyle BorderColor="CornflowerBlue" BorderStyle="Solid" BorderWidth="4px" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
