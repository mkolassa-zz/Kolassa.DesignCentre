<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kolassa.DesignCenter.Web.UI.frmCustomersold" Codebehind="frmCustomersold.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>frmCustomersold</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
<asp:Label id="lblLabel18" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 10px" runat="server"  Width="160px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Customer Name</asp:Label>
<asp:Label id="lblLabel19" style="Z-INDEX: 101;  Left: 175px; POSITION: absolute; TOP: 10px" runat="server"  Width="160px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Current Address Line 1</asp:Label>
<asp:Label id="lblLabel20" style="Z-INDEX: 101;  Left: 370px; POSITION: absolute; TOP: 10px" runat="server"  Width="160px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Current Address Line 2</asp:Label>
<asp:Label id="lblLabel21" style="Z-INDEX: 101;  Left: 760px; POSITION: absolute; TOP: 10px" runat="server"  Width="160px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Email</asp:Label>
<asp:Label id="lblLabel22" style="Z-INDEX: 101;  Left: 955px; POSITION: absolute; TOP: 10px" runat="server"  Width="90px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Home Phone</asp:Label>
<asp:Label id="lblLabel23" style="Z-INDEX: 101;  Left: 1155px; POSITION: absolute; TOP: 10px" runat="server"  Width="95px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Target Close</asp:Label>
<asp:Label id="lblLabel24" style="Z-INDEX: 101;  Left: 1055px; POSITION: absolute; TOP: 10px" runat="server"  Width="95px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Fax</asp:Label>
<asp:Label id="lblLabel27" style="Z-INDEX: 101;  Left: 565px; POSITION: absolute; TOP: 10px" runat="server"  Width="96px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Purchase Price</asp:Label>
<asp:Label id="lblLabel28" style="Z-INDEX: 101;  Left: 660px; POSITION: absolute; TOP: 10px" runat="server"  Width="96px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Parking Price</asp:Label>
<asp:TextBox id="txtCustomerID" style="Z-INDEX: 101;  Left: 85px; POSITION: absolute; TOP: 5px" runat="server"  Width="55px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblCustomerID_Label" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 5px" runat="server"  Width="75px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >CustomerID</asp:Label>
<asp:TextBox id="txtCustomerName" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 5px" runat="server"  Width="165px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtCurrentAddressLine1" style="Z-INDEX: 101;  Left: 175px; POSITION: absolute; TOP: 5px" runat="server"  Width="192px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtCurrentAddressLine2" style="Z-INDEX: 101;  Left: 370px; POSITION: absolute; TOP: 5px" runat="server"  Width="192px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtEmailAddress1" style="Z-INDEX: 101;  Left: 760px; POSITION: absolute; TOP: 5px" runat="server"  Width="192px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtHomePhone" style="Z-INDEX: 101;  Left: 955px; POSITION: absolute; TOP: 5px" runat="server"  Width="95px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtTargetClosingDate" style="Z-INDEX: 101;  Left: 1155px; POSITION: absolute; TOP: 5px" runat="server"  Width="95px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtFax" style="Z-INDEX: 101;  Left: 1055px; POSITION: absolute; TOP: 5px" runat="server"  Width="95px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtPurchasePrice" style="Z-INDEX: 101;  Left: 565px; POSITION: absolute; TOP: 5px" runat="server"  Width="95px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtParkingCost" style="Z-INDEX: 101;  Left: 665px; POSITION: absolute; TOP: 5px" runat="server"  Width="90px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblGenerated" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 61px" runat="server" ForeColor="#E0E0E0">This page is generated using ASP.NET Wizard.</asp:Label>
    </form>
  </body>
</html>
