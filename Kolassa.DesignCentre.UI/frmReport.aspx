<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmReport.aspx.vb" Inherits="Kolassa.DesignCentre.UI.frmReport" %>



<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    	<asp:ScriptManager ID="ScriptManager1" runat="server">
		</asp:ScriptManager>
    	<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" ProcessingMode="Remote" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
			<ServerReport ReportPath="mkolassa-001\rptmissing.rdl" ReportServerUrl="http://sql5030.site4now.net/Reports" />
		</rsweb:ReportViewer>
    </form>
</body>
</html>
