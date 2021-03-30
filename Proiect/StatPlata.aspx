<%@ Page Title="" Language="C#" MasterPageFile="~/Proiect.Master" AutoEventWireup="true" CodeBehind="StatPlata.aspx.cs" Inherits="Proiect.WebForm7" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="border-left-color: cornflowerblue; border-right-color: cornflowerblue; border-bottom-color: cornflowerblue; border-top-color: aliceblue; border-style: solid; border-radius: 0px 0px 12px 12px; padding: 0px 15px 15px 15px;">
        <h2 style="text-align: center;">Stat plata</h2>
        <br />

        <div class="lead">
            <div style="text-align:center; overflow:auto;">
                <asp:Label ID="LabelPrincipalErr" runat="server" Text=""></asp:Label>
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None" />
            </div>
        </div>
    </div>
</asp:Content>
