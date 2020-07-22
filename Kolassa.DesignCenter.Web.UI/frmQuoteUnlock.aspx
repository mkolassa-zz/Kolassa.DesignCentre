<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kolassa.DesignCenter.Web.UI.frmQuoteUnlock" Codebehind="frmQuoteUnlock.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>frmQuoteUnlock</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
<asp:Label id="lblQuoteID_Label" style="Z-INDEX: 101;  Left: 0px; POSITION: absolute; TOP: 50px" runat="server"  Width="85px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="6723891"   >Unit ID</asp:Label>
<asp:Label id="lblQuoteStatus_Label" style="Z-INDEX: 101;  Left: 90px; POSITION: absolute; TOP: 50px" runat="server"  Width="135px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="6723891"   >Quote Status</asp:Label>
<asp:Button id="btncmdExport_GC_Details" style="Z-INDEX: 101;  Left: 30px; POSITION: absolute; TOP: 10px" runat="server"  Width="124px"  Height="34px"   Font-Names=""  Font-Size=""    ForeColor="0"  Text="Export GC Details"  ></asp:Button>
<asp:DropDownList id="cmbPhase1Status" style="Z-INDEX: 101;  Left: 90px; POSITION: absolute; TOP: 5px" runat="server"  Width="135px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:DropDownList id="cmbQuoteID" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 5px" runat="server"  Width="80px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Label id="lblGenerated" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 100px" runat="server" ForeColor="#E0E0E0">This page is generated using ASP.NET Wizard.</asp:Label>
    </form>
  </body>
</html>
