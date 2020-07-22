<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kolassa.DesignCenter.Web.UI.frmPaymentsMultiQuote" Codebehind="frmPaymentsMultiQuote.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>frmPaymentsMultiQuote</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
<asp:Label id="lblQuoteID_Label" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 35px" runat="server"  Width="180px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Unit Number / Customer</asp:Label>
<asp:Label id="lblPaymentAmount_Label" style="Z-INDEX: 101;  Left: 485px; POSITION: absolute; TOP: 5px" runat="server"  Width="95px"  Height="50px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Actual Payment Amount</asp:Label>
<asp:Label id="lblPaymentDate_Label" style="Z-INDEX: 101;  Left: 385px; POSITION: absolute; TOP: 20px" runat="server"  Width="90px"  Height="35px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Actual Payment Date</asp:Label>
<asp:Label id="lblPaymentComment_Label" style="Z-INDEX: 101;  Left: 760px; POSITION: absolute; TOP: 35px" runat="server"  Width="235px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Payment Comment</asp:Label>
<asp:Label id="lblBuildingPhase_Label" style="Z-INDEX: 101;  Left: 675px; POSITION: absolute; TOP: 20px" runat="server"  Width="85px"  Height="35px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Building Phase</asp:Label>
<asp:Label id="lblLabel23" style="Z-INDEX: 101;  Left: 285px; POSITION: absolute; TOP: 20px" runat="server"  Width="95px"  Height="35px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Payment Due Amount</asp:Label>
<asp:Label id="lblLabel24" style="Z-INDEX: 101;  Left: 190px; POSITION: absolute; TOP: 20px" runat="server"  Width="90px"  Height="35px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Payment Due Date</asp:Label>
<asp:Label id="lblLabel26" style="Z-INDEX: 101;  Left: 585px; POSITION: absolute; TOP: 20px" runat="server"  Width="85px"  Height="35px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Check Number</asp:Label>
<asp:TextBox id="txtPaymentID" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 5px" runat="server"  Width="0px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtPaymentAmount" style="Z-INDEX: 101;  Left: 480px; POSITION: absolute; TOP: 5px" runat="server"  Width="100px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtPaymentDate" style="Z-INDEX: 101;  Left: 385px; POSITION: absolute; TOP: 5px" runat="server"  Width="90px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtPaymentComment" style="Z-INDEX: 101;  Left: 765px; POSITION: absolute; TOP: 5px" runat="server"  Width="235px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:DropDownList id="cmbQuoteID" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 5px" runat="server"  Width="180px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:DropDownList id="cmbBuildingPhase" style="Z-INDEX: 101;  Left: 675px; POSITION: absolute; TOP: 5px" runat="server"  Width="85px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:TextBox id="txtPaymentDueDate" style="Z-INDEX: 101;  Left: 190px; POSITION: absolute; TOP: 5px" runat="server"  Width="90px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtPaymentDueAmount" style="Z-INDEX: 101;  Left: 285px; POSITION: absolute; TOP: 5px" runat="server"  Width="95px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtCheckNumber" style="Z-INDEX: 101;  Left: 585px; POSITION: absolute; TOP: 5px" runat="server"  Width="85px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblGenerated" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 85px" runat="server" ForeColor="#E0E0E0">This page is generated using ASP.NET Wizard.</asp:Label>
    </form>
  </body>
</html>
