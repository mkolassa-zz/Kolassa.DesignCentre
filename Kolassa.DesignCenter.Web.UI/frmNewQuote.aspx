<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kolassa.DesignCenter.Web.UI.frmNewQuote" Codebehind="frmNewQuote.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>frmNewQuote</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
<asp:Label id="lblQuoteID_Label" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 5px" runat="server"  Width="60px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >QuoteID</asp:Label>
<asp:Label id="lblCustomerID_Label" style="Z-INDEX: 101;  Left: 70px; POSITION: absolute; TOP: 5px" runat="server"  Width="185px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >CustomerID</asp:Label>
<asp:Label id="lblUnitID_Label" style="Z-INDEX: 101;  Left: 260px; POSITION: absolute; TOP: 5px" runat="server"  Width="185px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >UnitID</asp:Label>
<asp:TextBox id="txtQuoteID" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 5px" runat="server"  Width="60px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:DropDownList id="cmbCustomerID" style="Z-INDEX: 101;  Left: 70px; POSITION: absolute; TOP: 5px" runat="server"  Width="185px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:DropDownList id="cmbUnitID" style="Z-INDEX: 101;  Left: 260px; POSITION: absolute; TOP: 5px" runat="server"  Width="185px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Label id="lblGenerated" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 56px" runat="server" ForeColor="#E0E0E0">This page is generated using ASP.NET Wizard.</asp:Label>
    </form>
  </body>
</html>
