<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kolassa.DesignCenter.Web.UI.frmPayments" Codebehind="frmPayments.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>frmPayments</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
<asp:Label id="lblPaymentAmount_Label" style="Z-INDEX: 101;  Left: 300px; POSITION: absolute; TOP: 25px" runat="server"  Width="95px"  Height="50px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Actual Payment Amount</asp:Label>
<asp:Label id="lblPaymentDate_Label" style="Z-INDEX: 101;  Left: 200px; POSITION: absolute; TOP: 40px" runat="server"  Width="90px"  Height="35px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Actual Payment Date</asp:Label>
<asp:Label id="lblPaymentComment_Label" style="Z-INDEX: 101;  Left: 575px; POSITION: absolute; TOP: 55px" runat="server"  Width="235px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Payment Comment</asp:Label>
<asp:Label id="lblBuildingPhase_Label" style="Z-INDEX: 101;  Left: 490px; POSITION: absolute; TOP: 40px" runat="server"  Width="85px"  Height="35px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Building Phase</asp:Label>
<asp:Label id="lblLabel23" style="Z-INDEX: 101;  Left: 100px; POSITION: absolute; TOP: 40px" runat="server"  Width="95px"  Height="35px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Payment Due Amount</asp:Label>
<asp:Label id="lblLabel24" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 40px" runat="server"  Width="90px"  Height="35px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Payment Due Date</asp:Label>
<asp:Label id="lblLabel26" style="Z-INDEX: 101;  Left: 400px; POSITION: absolute; TOP: 40px" runat="server"  Width="85px"  Height="35px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Check Number</asp:Label>
<asp:Button id="btnPayments" style="Z-INDEX: 101;  Left: 690px; POSITION: absolute; TOP: 5px" runat="server"  Width="136px"  Height="34px"   Font-Names=""  Font-Size=""    ForeColor="0"  Text="Calculate Payments"  ></asp:Button>
<asp:TextBox id="txtPaymentID" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 5px" runat="server"  Width="0px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtPaymentAmount" style="Z-INDEX: 101;  Left: 295px; POSITION: absolute; TOP: 10px" runat="server"  Width="100px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtPaymentDate" style="Z-INDEX: 101;  Left: 200px; POSITION: absolute; TOP: 10px" runat="server"  Width="90px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtPaymentComment" style="Z-INDEX: 101;  Left: 580px; POSITION: absolute; TOP: 10px" runat="server"  Width="235px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:DropDownList id="cmbQuoteID" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 5px" runat="server"  Width="0px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:DropDownList id="cmbBuildingPhase" style="Z-INDEX: 101;  Left: 490px; POSITION: absolute; TOP: 10px" runat="server"  Width="85px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:TextBox id="txtPaymentDueDate" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 10px" runat="server"  Width="90px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtPaymentDueAmount" style="Z-INDEX: 101;  Left: 100px; POSITION: absolute; TOP: 10px" runat="server"  Width="95px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtCheckNumber" style="Z-INDEX: 101;  Left: 400px; POSITION: absolute; TOP: 10px" runat="server"  Width="85px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblGenerated" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 105px" runat="server" ForeColor="#E0E0E0">This page is generated using ASP.NET Wizard.</asp:Label>
    </form>
  </body>
</html>
