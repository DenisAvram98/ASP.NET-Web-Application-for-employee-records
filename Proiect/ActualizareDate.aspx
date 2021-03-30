<%@ Page Title="" Language="C#" MasterPageFile="~/Proiect.Master" AutoEventWireup="true" CodeBehind="ActualizareDate.aspx.cs" Inherits="Proiect.WebForm3" MaintainScrollPositionOnPostback="true" UnobtrusiveValidationMode="none"%>

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
        <h2 style="text-align: center;">Actualizare date angajati</h2>
        <br />

        <div class="lead">
            <div style="text-align: center;">
                <table style="width: 100%;">
                    <tr>
                        <td style="padding-left: 50px;">
                            <table>
                                <tr>
                                    <td colspan="2">
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
                                    <td colspan="2" style="padding: 5px; text-align: center; width: 363px;">
                                        <asp:Label ID="LabelCautareErr" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding: 5px; text-align: center;">
                                        <asp:Button ID="cautaBtn" runat="server" Text="Cauta" CssClass="buton" OnClick="cautaBtn_Click" />&emsp;
                                        <asp:Button ID="anulareBtn" runat="server" Text="Anulare cautare" CssClass="buton" OnClick="anulareBtn_Click" CausesValidation="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>&emsp;&emsp;&emsp;
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td id="TableDataFileUpload" style="padding: 5px; visibility: hidden;" runat="server">
                                        <asp:Label ID="Label3" runat="server" Text="Schimbati sau adaugati poza angajatului"></asp:Label>&nbsp;
                                        <asp:FileUpload ID="pozaAngajatFU" runat="server" Width="340px" onchange="loadFile(event)" />
                                        <script type="text/javascript">
                                            var loadFile = function (event) {
                                                var control = document.getElementById('<%=pozaAngajatImg.ClientID%>');
                                                var inputFile = document.getElementById('<%=pozaAngajatFU.ClientID%>');
                                                var button = document.getElementById('<%=salvareImgBtn.ClientID%>');
                                                if (control.style.visibility === 'hidden') {
                                                    if (inputFile.files.length !== 0) {
                                                        control.style.visibility = '';
                                                        control.src = URL.createObjectURL(event.target.files[0]);
                                                        button.style.visibility = 'visible';
                                                    }
                                                    else {
                                                        control.style.visibility = '';
                                                        button.style.visibility = 'hidden';
                                                        control.src = '';
                                                    }
                                                }
                                                else {
                                                    if (inputFile.files.length !== 0) {
                                                        control.style.visibility = '';
                                                        button.style.visibility = 'visible';
                                                        control.src = URL.createObjectURL(event.target.files[0]);
                                                    }
                                                    else {
                                                        control.style.visibility = '';
                                                        button.style.visibility = 'hidden';
                                                        control.src = '';
                                                    }
                                                }
                                            };
                                        </script>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px; text-align: left;">
                                        <asp:Button ID="salvareImgBtn" runat="server" Text="Salvare" Style="vertical-align: top; visibility: hidden;" OnClick="salvareImgBtn_Click" />
                                        <asp:Image ID="pozaAngajatImg" runat="server" AlternateText="Angajatul nu are poza." ForeColor="Red" Width="260px" Style="visibility: hidden;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px; text-align: center;">
                                        <asp:Label ID="LabelSalvareImagineErr" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>

            <br />
            <div id="dvScroll" style="text-align: center; overflow: auto; height: 250px;">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="NRCRT" DataSourceID="SqlDataSource1" BackColor="GhostWhite" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" OnRowUpdated="GridView1_RowUpdated">
                    <Columns>
                        <asp:CommandField ShowEditButton="True" ShowSelectButton="True" CancelImageUrl="~/Icons/Delete.png" CancelText="Anulare" EditImageUrl="~/Icons/Edit.png" EditText="Editare" SelectImageUrl="~/Icons/Add.png" SelectText="Selectare" UpdateImageUrl="~/Icons/save.JPG" UpdateText="Salvare" />
                        <asp:BoundField DataField="NRCRT" HeaderText="Numar curent" ReadOnly="True" SortExpression="NRCRT" />
                        <asp:TemplateField HeaderText="Nume" SortExpression="NUME">
                            <EditItemTemplate>
                                <asp:TextBox ID="numeGV" runat="server" Text='<%# Bind("NUME") %>' MaxLength="30"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="numeGV" Display="Dynamic" ErrorMessage="&lt;br /&gt;Nu ati introdus numele!" Font-Size="90%" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="numeGV" Display="Dynamic" ErrorMessage="&lt;br /&gt;Numele poate sa contina doar litele &lt;br /&gt;si caractere speciale(',-, )." Font-Size="90%" ForeColor="Red" ValidationExpression="^[a-zA-Z][a-zA-Z''-' ]{0,29}$"></asp:RegularExpressionValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("NUME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prenume" SortExpression="PRENUME">
                            <EditItemTemplate>
                                <asp:TextBox ID="prenumeGW" runat="server" Text='<%# Bind("PRENUME") %>' MaxLength="30"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="prenumeGW" Display="Dynamic" ErrorMessage="&lt;br /&gt;Nu ati introdus prenumele!" Font-Size="90%" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="prenumeGW" Display="Dynamic" ErrorMessage="&lt;br /&gt;Prenumele poate sa contina doar litele &lt;br /&gt;nsi caractere speciale(',-, )." Font-Size="90%" ForeColor="Red" ValidationExpression="^[a-zA-Z][a-zA-Z''-' ]{0,29}$"></asp:RegularExpressionValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("PRENUME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Functie" SortExpression="FUNCTIE">
                            <EditItemTemplate>
                                <asp:TextBox ID="functieGW" runat="server" Text='<%# Bind("FUNCTIE") %>' MaxLength="30"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="functieGW" Display="Dynamic" ErrorMessage="&lt;br /&gt;Nu ati introdus functia!" Font-Size="90%" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="functieGW" Display="Dynamic" ErrorMessage="&lt;br /&gt;Numele functiei poate sa contina doar litele, &lt;br /&gt;cifre si caractere speciale(',-, )." Font-Size="90%" ForeColor="Red" ValidationExpression="^[a-zA-Z''-' 0-9]{1,30}$"></asp:RegularExpressionValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("FUNCTIE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salariu de baza" SortExpression="SALARBAZA">
                            <EditItemTemplate>
                                <asp:TextBox ID="salarBazaGW" runat="server" Text='<%# Bind("SALARBAZA") %>' TextMode="Number"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="salarBazaGW" Display="Dynamic" ErrorMessage="&lt;br /&gt;Nu ati introdus salariul de baza!" Font-Size="90%" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="salarBazaGW" Display="Dynamic" ErrorMessage="&lt;br /&gt;Salariul de baza nu poate fi negativ sau 0!" Font-Size="90%" ForeColor="Red" MaximumValue="999999" MinimumValue="1"></asp:RangeValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="&lt;br /&gt;Valoarea maxima nu poate depasi 999999 LEI!" ControlToValidate="salarBazaGW" Font-Size="90%" ForeColor="Red" Display="Dynamic" ValidationExpression="^[0-9]{1,6}$"></asp:RegularExpressionValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("SALARBAZA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Spor (%)" SortExpression="SPORPROCENT">
                            <EditItemTemplate>
                                <asp:TextBox ID="sporProcentGW" runat="server" Text='<%# Bind("SPORPROCENT") %>' TextMode="Number"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="sporProcentGW" Display="Dynamic" ErrorMessage="&lt;br /&gt;Nu ati introdus valoarea sporului!" Font-Size="90%" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="&lt;br /&gt;Valoarea maxima nu poate depasi 200%, &lt;br /&gt;iar valoarea minima nu poate fi negativa!" ControlToValidate="sporProcentGW" Display="Dynamic" ForeColor="Red" ValidationExpression="^[0-9]{1,2}$|^1[0-9]{2}$|200" Font-Size="90%"></asp:RegularExpressionValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("SPORPROCENT") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Premii brute" SortExpression="PREMIIBRUTE">
                            <EditItemTemplate>
                                <asp:TextBox ID="premiiBruteGW" runat="server" Text='<%# Bind("PREMIIBRUTE") %>' TextMode="Number"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="premiiBruteGW" Display="Dynamic" ErrorMessage="&lt;br /&gt;Nu ati introdus valoarea premiilor brute!" Font-Size="90%" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="premiiBruteGW" Display="Dynamic" ErrorMessage="&lt;br /&gt;Valoarea premiilor brute nu poate fi negativa!" Font-Size="90%" ForeColor="Red" MaximumValue="999999" MinimumValue="0"></asp:RangeValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="&lt;br /&gt;Valoarea maxima nu poate depasi 999999 LEI!" ControlToValidate="premiiBruteGW" Font-Size="90%" ForeColor="Red" Display="Dynamic" ValidationExpression="^[0-9]{1,6}$"></asp:RegularExpressionValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("PREMIIBRUTE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TOTALBRUT" HeaderText="Total brut" SortExpression="TOTALBRUT" ReadOnly="True" />
                        <asp:BoundField DataField="BRUTIMPOZABIL" HeaderText="Brut impozabil" ReadOnly="True" SortExpression="BRUTIMPOZABIL" />
                        <asp:BoundField DataField="IMPOZIT" HeaderText="Impozit" ReadOnly="True" SortExpression="IMPOZIT" />
                        <asp:BoundField DataField="CAS" HeaderText="CAS" ReadOnly="True" SortExpression="CAS" />
                        <asp:BoundField DataField="CASS" HeaderText="CASS" ReadOnly="True" SortExpression="CASS" />
                        <asp:TemplateField HeaderText="Retineri salariale" SortExpression="RETINERI">
                            <EditItemTemplate>
                                <asp:TextBox ID="retineriGW" runat="server" Text='<%# Bind("RETINERI") %>' TextMode="Number"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="retineriGW" Display="Dynamic" ErrorMessage="&lt;br /&gt;Nu ati introdus valoarea retinerilor salariale!" Font-Size="90%" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="retineriGW" Display="Dynamic" ErrorMessage="&lt;br /&gt;Retinerile salariale nu pot fi negative!" Font-Size="90%" ForeColor="Red" MaximumValue="999999" MinimumValue="0"></asp:RangeValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage="&lt;br /&gt;Valoarea maxima nu poate depasi 999999 LEI!" ControlToValidate="retineriGW" Font-Size="90%" ForeColor="Red" Display="Dynamic" ValidationExpression="^[0-9]{1,6}$"></asp:RegularExpressionValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("RETINERI") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="VIRATCARD" HeaderText="Virat pe card" ReadOnly="True" SortExpression="VIRATCARD" />
                    </Columns>
                    <SelectedRowStyle BorderColor="CornflowerBlue" BorderStyle="Solid" BorderWidth="4px" />
                </asp:GridView>

                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" OldValuesParameterFormatString="original_{0}" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT NRCRT, NUME, PRENUME, FUNCTIE, SALARBAZA, SPORPROCENT, PREMIIBRUTE, TOTALBRUT, BRUTIMPOZABIL, IMPOZIT, CAS, CASS, RETINERI, VIRATCARD FROM ANGAJATI ORDER BY NRCRT" UpdateCommand="UPDATE ANGAJATI SET NUME = :NUME, PRENUME = :PRENUME, FUNCTIE = :FUNCTIE, SALARBAZA = :SALARBAZA, SPORPROCENT = :SPORPROCENT, PREMIIBRUTE = :PREMIIBRUTE, RETINERI = :RETINERI WHERE (NRCRT = :original_NRCRT)">
                    <UpdateParameters>
                        <asp:Parameter Name="NUME" Type="String" />
                        <asp:Parameter Name="PRENUME" Type="String" />
                        <asp:Parameter Name="FUNCTIE" Type="String" />
                        <asp:Parameter Name="SALARBAZA" Type="Decimal" />
                        <asp:Parameter Name="SPORPROCENT" Type="Decimal" />
                        <asp:Parameter Name="PREMIIBRUTE" Type="Decimal" />
                        <asp:Parameter Name="RETINERI" Type="Decimal" />
                        <asp:Parameter Name="original_NRCRT" Type="Decimal" />
                    </UpdateParameters>
                </asp:SqlDataSource>

            </div>
            <input type="hidden" id="div_position" name="div_position" />
            <script type="text/javascript">
                window.onload = function () {
                    var div = document.getElementById("dvScroll");
                    var div_position = document.getElementById("div_position");
                    var position = parseInt('<%=Request.Form["div_position"] %>');
                    if (isNaN(position)) {
                        position = 0;
                    }
                    div.scrollTop = position;
                    div.onscroll = function () {
                        div_position.value = div.scrollTop;
                    };
                };
            </script>
        </div>
    </div>
</asp:Content>
