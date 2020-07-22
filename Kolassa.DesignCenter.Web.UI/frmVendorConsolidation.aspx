<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kolassa.DesignCenter.Web.UI.frmVendorConsolidation" Codebehind="frmVendorConsolidation.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>frmVendorConsolidation</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
<asp:DropDownList id="cmbcboVendor1" style="Z-INDEX: 101;  Left: 15px; POSITION: absolute; TOP: 45px" runat="server"  Width="345px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="0"   ></asp:DropDownList>
<asp:Label id="lblcboVendorToFoldIntoAnotherVendor_Label" style="Z-INDEX: 101;  Left: 10px; POSITION: absolute; TOP: 20px" runat="server"  Width="345px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="0"   >Vendor To Fold Into Another Vendor</asp:Label>
<asp:DropDownList id="cmbcboVendor2" style="Z-INDEX: 101;  Left: 450px; POSITION: absolute; TOP: 45px" runat="server"  Width="380px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="0"   ></asp:DropDownList>
<asp:Label id="lblLabel3" style="Z-INDEX: 101;  Left: 450px; POSITION: absolute; TOP: 20px" runat="server"  Width="360px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="0"   >Vendor To "Get" all Data from other Vendor</asp:Label>
<asp:Label id="lblLabel4" style="Z-INDEX: 101;  Left: 380px; POSITION: absolute; TOP: 30px" runat="server"  Width="60px"  Height="45px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="0"   >a</asp:Label>
<asp:Button id="btncmdConsolidate" style="Z-INDEX: 101;  Left: 505px; POSITION: absolute; TOP: 85px" runat="server"  Width="150px"  Height="35px"   Font-Names=""  Font-Size=""    ForeColor="black"  Text="Consolidate!"  ></asp:Button>
<asp:Button id="btncmdClose" style="Z-INDEX: 101;  Left: 740px; POSITION: absolute; TOP: 85px" runat="server"  Width="52px"  Height="34px"   Font-Names=""  Font-Size=""    ForeColor="black"  Text="&Close"  ></asp:Button>
<asp:Label id="lblGenerated" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 150px" runat="server" ForeColor="#E0E0E0">This page is generated using ASP.NET Wizard.</asp:Label>
    </form>
  </body>
</html>
