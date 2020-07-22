<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kolassa.DesignCenter.Web.UI.frmMissingSelections" Codebehind="frmMissingSelections.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>frmMissingSelections</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
<asp:Label id="lblLocation1_Label" style="Z-INDEX: 101;  Left: 190px; POSITION: absolute; TOP: 90px" runat="server"  Width="150px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="128"   >Location</asp:Label>
<asp:Label id="lblCategory1_Label" style="Z-INDEX: 101;  Left: 425px; POSITION: absolute; TOP: 90px" runat="server"  Width="150px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="128"   >Category</asp:Label>
<asp:Label id="lblLabel28" style="Z-INDEX: 101;  Left: 0px; POSITION: absolute; TOP: 0px" runat="server"  Width="326px"  Height="34px"  BackColor="white"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="128"   >Missing Selections List</asp:Label>
<asp:Label id="lblLabel38" style="Z-INDEX: 101;  Left: 0px; POSITION: absolute; TOP: 90px" runat="server"  Width="150px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="128"   >Phase</asp:Label>
<asp:Button id="btncmdPrint" style="Z-INDEX: 101;  Left: 450px; POSITION: absolute; TOP: 0px" runat="server"  Width="38px"  Height="36px"   Font-Names=""  Font-Size=""    ForeColor="0"  Text="Command39"  ></asp:Button>
<asp:TextBox id="txtUnitName" style="Z-INDEX: 101;  Left: 75px; POSITION: absolute; TOP: 35px" runat="server"  Width="120px"  Height="24px"  BackColor="white"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblLabel40" style="Z-INDEX: 101;  Left: 0px; POSITION: absolute; TOP: 35px" runat="server"  Width="46px"  Height="24px"  BackColor="white"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="black"   >Unit:</asp:Label>
<asp:TextBox id="txtText41" style="Z-INDEX: 101;  Left: 305px; POSITION: absolute; TOP: 35px" runat="server"  Width="205px"  Height="24px"  BackColor="white"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblLabel42" style="Z-INDEX: 101;  Left: 215px; POSITION: absolute; TOP: 35px" runat="server"  Width="98px"  Height="24px"  BackColor="white"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="black"   >Print Date:</asp:Label>
<asp:TextBox id="txtLocation" style="Z-INDEX: 101;  Left: 185px; POSITION: absolute; TOP: 0px" runat="server"  Width="230px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtUpgradeCategory" style="Z-INDEX: 101;  Left: 420px; POSITION: absolute; TOP: 0px" runat="server"  Width="515px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:DropDownList id="cmbBuildingPhase" style="Z-INDEX: 101;  Left: 0px; POSITION: absolute; TOP: 0px" runat="server"  Width="150px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Button id="btncmdOK" style="Z-INDEX: 101;  Left: 440px; POSITION: absolute; TOP: 5px" runat="server"  Width="60px"  Height="34px"   Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="0"  Text="&OK"  ></asp:Button>
<asp:Button id="btncmdClose" style="Z-INDEX: 101;  Left: 550px; POSITION: absolute; TOP: 5px" runat="server"  Width="52px"  Height="34px"   Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="0"  Text="&Close"  ></asp:Button>
<asp:Label id="lbllblContinue" style="Z-INDEX: 101;  Left: 0px; POSITION: absolute; TOP: 5px" runat="server"  Width="419px"  Height="34px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="128"   >Warning! Do you want to Continue?</asp:Label>
<asp:Label id="lblGenerated" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 140px" runat="server" ForeColor="#E0E0E0">This page is generated using ASP.NET Wizard.</asp:Label>
    </form>
  </body>
</html>
