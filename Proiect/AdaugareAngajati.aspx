<%@ Page Title="" Language="C#" MasterPageFile="~/Proiect.Master" AutoEventWireup="true" CodeBehind="AdaugareAngajati.aspx.cs" Inherits="Proiect.WebForm4" UnobtrusiveValidationMode="none" MaintainScrollPositionOnPostback="true" %>

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
        <h2 style="text-align: center;">Adaugare angajati</h2>
        <br />

        <div class="lead">
            <div style="text-align: center;">
                <asp:Label ID="LabelPrincipalErr" runat="server" Text=""></asp:Label>
            </div>
            <p style="text-align: center; color: silver;">Campurile marcate cu * sunt obligatorii</p>
            <div style="text-align: center;">
                <table style="margin-left: auto; margin-right: auto;">
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label ID="Label1" runat="server" Text="* Nume"></asp:Label>&nbsp;
                        </td>
                        <td style="text-align: left; padding: 5px;">
                            <asp:TextBox ID="numeTB" runat="server" placeholder="" MaxLength="30"></asp:TextBox>
                        </td>
                        <td>&emsp;&emsp;&emsp;&emsp;
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="Label2" runat="server" Text="* Prenume"></asp:Label>&nbsp;
                        </td>
                        <td style="text-align: left; padding: 5px;">
                            <asp:TextBox ID="prenumeTB" runat="server" MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; padding: 5px; font-size: 90%;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Nu ati introdus numele!" EnableClientScript="true" ControlToValidate="numeTB" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Numele poate sa contina doar litele <br />si caractere speciale(',-, )." EnableClientScript="true" ControlToValidate="numeTB" ForeColor="Red" Display="Dynamic" ValidationExpression="^[a-zA-Z][a-zA-Z''-' ]{0,29}$"></asp:RegularExpressionValidator>
                        </td>
                        <td></td>
                        <td colspan="2" style="text-align: center; padding: 5px; font-size: 90%;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Nu ati introdus prenumele!" EnableClientScript="true" ControlToValidate="prenumeTB" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Prenumele poate sa contina doar litele <br />nsi caractere speciale(',-, )." EnableClientScript="true" ControlToValidate="prenumeTB" ForeColor="Red" Display="Dynamic" ValidationExpression="^[a-zA-Z][a-zA-Z''-' ]{0,29}$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label ID="Label3" runat="server" Text="* Functie"></asp:Label>&nbsp;
                        </td>
                        <td style="text-align: left; padding: 5px;">
                            <asp:TextBox ID="functieTB" runat="server" MaxLength="30"></asp:TextBox>
                        </td>
                        <td></td>
                        <td rowspan="6" colspan="2" style="padding: 5px; text-align: center;">
                            <asp:Image ID="pozaAngajatImg" runat="server" AlternateText="Poza" Width="260px" Style="visibility: hidden;" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; padding: 5px; font-size: 90%;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Nu ati introdus functia!" EnableClientScript="true" ControlToValidate="functieTB" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Numele functiei poate sa contina doar litele, <br />cifre si caractere speciale(',-, )." EnableClientScript="true" ControlToValidate="functieTB" ForeColor="Red" Display="Dynamic" ValidationExpression="^[a-zA-Z''-' 0-9]{1,30}$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label ID="Label11" runat="server" Text="Poza angajatului"></asp:Label>&nbsp;
                        </td>
                        <td colspan="2" style="text-align: left; padding: 5px;">
                            <asp:FileUpload ID="pozaAngajat" runat="server" Width="340px" onchange="loadFile(event)" />
                            <script type="text/javascript">
                                var loadFile = function (event) {
                                    var control = document.getElementById('<%=pozaAngajatImg.ClientID%>');
                                    var inputFile = document.getElementById('<%=pozaAngajat.ClientID%>');
                                    if (control.style.visibility === 'hidden') {
                                        if (inputFile.files.length !== 0) {
                                            control.style.visibility = '';
                                            control.src = URL.createObjectURL(event.target.files[0]);
                                        }
                                        else {
                                            control.style.visibility = 'hidden';
                                            control.src = '';
                                        }
                                    }
                                    else {
                                        if (inputFile.files.length !== 0) {
                                            control.style.visibility = '';
                                            control.src = URL.createObjectURL(event.target.files[0]);
                                        }
                                        else {
                                            control.style.visibility = 'hidden';
                                            control.src = '';
                                        }
                                    }
                                };
                            </script>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label ID="Label4" runat="server" Text="* Salariu de baza"></asp:Label>&nbsp;
                        </td>
                        <td style="text-align: left; padding: 5px;">
                            <asp:TextBox ID="salarBazaTB" runat="server" Width="150px" TextMode="Number" Style="text-align: right;" AutoPostBack="true" OnTextChanged="salarBazaTB_TextChanged" CausesValidation="true"></asp:TextBox>
                            LEI
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; padding: 5px; font-size: 90%;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Nu ati introdus salariul de baza!" EnableClientScript="true" ControlToValidate="salarBazaTB" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Salariul de baza nu poate fi negativ sau 0!" EnableClientScript="true" ControlToValidate="salarBazaTB" ForeColor="Red" Display="Dynamic" MaximumValue="999999" MinimumValue="1"></asp:RangeValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Valoarea maxima nu poate depasi 999999 LEI!" EnableClientScript="true" ControlToValidate="salarBazaTB" ForeColor="Red" Display="Dynamic" ValidationExpression="^[0-9]{1,6}$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label ID="Label5" runat="server" Text="* Spor"></asp:Label>&nbsp;
                        </td>
                        <td style="text-align: left; padding: 5px;">
                            <asp:TextBox ID="sporProcentTB" runat="server" Width="100px" TextMode="Number" Style="text-align: right;" AutoPostBack="True" CausesValidation="True" OnTextChanged="sporProcentTB_TextChanged"></asp:TextBox>
                            %
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; padding: 5px; font-size: 90%;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Nu ati introdus valoarea sporului!" EnableClientScript="true" ControlToValidate="sporProcentTB" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Valoarea maxima nu poate depasi 200%, <br />iar valoarea minima nu poate fi negativa!" EnableClientScript="true" ControlToValidate="sporProcentTB" Display="Dynamic" ForeColor="Red" ValidationExpression="^[0-9]{1,2}$|^1[0-9]{2}$|200"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label ID="Label6" runat="server" Text="* Premii brute"></asp:Label>&nbsp;
                        </td>
                        <td style="text-align: left; padding: 5px;">
                            <asp:TextBox ID="premiiBruteTB" runat="server" Width="150px" TextMode="Number" Style="text-align: right;" AutoPostBack="True" CausesValidation="True" OnTextChanged="premiiBruteTB_TextChanged"></asp:TextBox>
                            LEI
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; padding: 5px; font-size: 90%;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Nu ati introdus valoarea premiilor brute!" EnableClientScript="true" ControlToValidate="premiiBruteTB" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator3" runat="server" ErrorMessage="Valoarea premiilor brute nu poate fi negativa!" EnableClientScript="true" ControlToValidate="premiiBruteTB" ForeColor="Red" Display="Dynamic" MaximumValue="999999" MinimumValue="0"></asp:RangeValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Valoarea maxima nu poate depasi 999999 LEI!" EnableClientScript="true" ControlToValidate="premiiBruteTB" ForeColor="Red" Display="Dynamic" ValidationExpression="^[0-9]{1,6}$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label ID="Label7" runat="server" Text="Total brut"></asp:Label>&nbsp;
                        </td>
                        <td style="text-align: left; padding: 5px;">
                            <asp:TextBox ID="totalBrutTB" runat="server" Width="150px" TextMode="Number" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                            LEI
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label ID="LabelCAS" runat="server" Text="CAS ()"></asp:Label>&nbsp;
                        </td>
                        <td style="text-align: left; padding: 5px;">
                            <asp:TextBox ID="casTB" runat="server" Width="150px" TextMode="Number" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                            LEI
                        </td>
                        <td></td>
                        <td style="text-align: left;">
                            <asp:Label ID="LabelCASS" runat="server" Text="CASS ()"></asp:Label>&nbsp;
                        </td>
                        <td style="text-align: left; padding: 5px;">
                            <asp:TextBox ID="cassTB" runat="server" Width="150px" TextMode="Number" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                            LEI
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label ID="Label8" runat="server" Text="Brut impozabil"></asp:Label>&nbsp;
                        </td>
                        <td style="text-align: left; padding: 5px;">
                            <asp:TextBox ID="brutImpozabilTB" runat="server" Width="150px" TextMode="Number" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                            LEI
                        </td>
                        <td></td>
                        <td style="text-align: left;">
                            <asp:Label ID="LabelImpozit" runat="server" Text="Impozit ()"></asp:Label>&nbsp;
                        </td>
                        <td style="text-align: left; padding: 5px;">
                            <asp:TextBox ID="impozitTB" runat="server" Width="150px" TextMode="Number" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                            LEI
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label ID="Label10" runat="server" Text="* Retineri salariale"></asp:Label>&nbsp;
                        </td>
                        <td style="text-align: left; padding: 5px;">
                            <asp:TextBox ID="retineriTB" runat="server" Width="150px" TextMode="Number" Style="text-align: right;" AutoPostBack="True" CausesValidation="True" OnTextChanged="retineriTB_TextChanged"></asp:TextBox>
                            LEI
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; padding: 5px; font-size: 90%;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Nu ati introdus valoarea retinerilor salariale!" EnableClientScript="true" ControlToValidate="retineriTB" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator4" runat="server" ErrorMessage="Retinerile salariale nu pot fi negative!" EnableClientScript="true" ControlToValidate="retineriTB" ForeColor="Red" Display="Dynamic" MaximumValue="999999" MinimumValue="0"></asp:RangeValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Valoarea maxima nu poate depasi 999999 LEI!" EnableClientScript="true" ControlToValidate="retineriTB" ForeColor="Red" Display="Dynamic" ValidationExpression="^[0-9]{1,6}$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label ID="Label9" runat="server" Text="Virat pe card"></asp:Label>&nbsp;
                        </td>
                        <td style="text-align: left; padding: 5px;">
                            <asp:TextBox ID="viratCardTB" runat="server" Width="150px" TextMode="Number" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                            LEI
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="padding: 5px;">
                            <asp:Label ID="LabelRezultatAdaugare" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="padding: 5px;">
                            <asp:Button ID="adaugaAngajatBtn" runat="server" Text="Adauga angajat nou" CssClass="buton" OnClick="adaugaAngajatBtn_Click" />&emsp;
                            <asp:Button ID="anulareBtn" runat="server" Text="Anulare" CssClass="buton" CausesValidation="false" OnClick="anulareBtn_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
