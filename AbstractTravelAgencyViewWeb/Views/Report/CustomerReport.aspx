<%@Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerReport.aspx.cs" Inherits="AbstractTravelAgencyViewWeb.Views.Report.CustomerReport" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>
 
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
    </head>
    <body>
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="919px">
            </rsweb:ReportViewer>
        </form>
    </body>
</html>
