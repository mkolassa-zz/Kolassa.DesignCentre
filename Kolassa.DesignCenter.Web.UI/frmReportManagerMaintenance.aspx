<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kolassa.DesignCenter.Web.UI.frmReportManagerMaintenance" Codebehind="frmReportManagerMaintenance.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>frmReportManagerMaintenance</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
<asp:TextBox id="txtReportDescription" style="Z-INDEX: 101;  Left: 170px; POSITION: absolute; TOP: 40px" runat="server"  Width="285px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="0"   ></asp:TextBox>
<asp:Label id="lblLabel1" style="Z-INDEX: 101;  Left: 55px; POSITION: absolute; TOP: 40px" runat="server"  Width="110px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="0"   >Description:</asp:Label>
<asp:TextBox id="txtReportID" style="Z-INDEX: 101;  Left: 35px; POSITION: absolute; TOP: 10px" runat="server"  Width="60px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="0"   ></asp:TextBox>
<asp:Label id="lblLabel2" style="Z-INDEX: 101;  Left: 0px; POSITION: absolute; TOP: 10px" runat="server"  Width="29px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="0"   >ID:</asp:Label>
<asp:Panel id="pnlsubReportCategoryMap" style="Z-INDEX: 101;  Left: 10px; POSITION: absolute; TOP: 115px" runat="server"  Width="340px"  Height="290px"         ></asp:Panel>
<asp:Label id="lblsubReportCategoryMap_Label" style="Z-INDEX: 101;  Left: 40px; POSITION: absolute; TOP: 95px" runat="server"  Width="116px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="0"   >Categories</asp:Label>
<asp:Panel id="pnlsubReportFields" style="Z-INDEX: 101;  Left: 365px; POSITION: absolute; TOP: 115px" runat="server"  Width="355px"  Height="290px"         ></asp:Panel>
<asp:Label id="lblsubReportFields_Label" style="Z-INDEX: 101;  Left: 395px; POSITION: absolute; TOP: 95px" runat="server"  Width="71px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="0"   >Fields</asp:Label>
<asp:TextBox id="txtcboReportName" style="Z-INDEX: 101;  Left: 170px; POSITION: absolute; TOP: 10px" runat="server"  Width="285px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="0"   ></asp:TextBox>
<asp:Label id="lblLabel0" style="Z-INDEX: 101;  Left: 110px; POSITION: absolute; TOP: 10px" runat="server"  Width="55px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="0"   >Name:</asp:Label>
<asp:Label id="lblRequired_Label" style="Z-INDEX: 101;  Left: 595px; POSITION: absolute; TOP: 95px" runat="server"  Width="71px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="black"   >Required</asp:Label>
<asp:Label id="lblGenerated" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 435px" runat="server" ForeColor="#E0E0E0">This page is generated using ASP.NET Wizard.</asp:Label>
    </form>
  </body>
</html>
