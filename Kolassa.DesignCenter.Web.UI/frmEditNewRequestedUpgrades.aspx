<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kolassa.DesignCenter.Web.UI.frmEditNewRequestedUpgrades" Codebehind="frmEditNewRequestedUpgrades.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>frmEditNewRequestedUpgrades</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
<asp:TextBox id="txtRequestedUpgradeID" style="Z-INDEX: 101;  Left: 2px; POSITION: absolute; TOP: 10px" runat="server"  Width="2px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblRequestedUpgradeID_Label" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 10px" runat="server"  Width="5px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >RequestedUpgradeID</asp:Label>
<asp:DropDownList id="cmbQuoteID" style="Z-INDEX: 101;  Left: 15px; POSITION: absolute; TOP: 40px" runat="server"  Width="0px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Label id="lblQuoteID_Label" style="Z-INDEX: 101;  Left: 0px; POSITION: absolute; TOP: 40px" runat="server"  Width="5px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >QuoteID</asp:Label>
<asp:TextBox id="txtUpgradeDescription" style="Z-INDEX: 101;  Left: 225px; POSITION: absolute; TOP: 30px" runat="server"  Width="195px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblUpgradeDescription_Label" style="Z-INDEX: 101;  Left: 225px; POSITION: absolute; TOP: 5px" runat="server"  Width="128px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Upgrade Description</asp:Label>
<asp:TextBox id="txtUpgradeClass" style="Z-INDEX: 101;  Left: 425px; POSITION: absolute; TOP: 30px" runat="server"  Width="115px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblUpgradeCategory_Label" style="Z-INDEX: 101;  Left: 425px; POSITION: absolute; TOP: 5px" runat="server"  Width="114px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Upgrade Level</asp:Label>
<asp:TextBox id="txtStyleDescription" style="Z-INDEX: 101;  Left: 545px; POSITION: absolute; TOP: 30px" runat="server"  Width="165px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblStyleDescription_Label" style="Z-INDEX: 101;  Left: 545px; POSITION: absolute; TOP: 5px" runat="server"  Width="105px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Style Description</asp:Label>
<asp:TextBox id="txtPrice" style="Z-INDEX: 101;  Left: 745px; POSITION: absolute; TOP: 30px" runat="server"  Width="85px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblPrice_Label" style="Z-INDEX: 101;  Left: 745px; POSITION: absolute; TOP: 5px" runat="server"  Width="85px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Price</asp:Label>
<asp:TextBox id="txtAdjustments" style="Z-INDEX: 101;  Left: 835px; POSITION: absolute; TOP: 30px" runat="server"  Width="90px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblAdjustments_Label" style="Z-INDEX: 101;  Left: 835px; POSITION: absolute; TOP: 5px" runat="server"  Width="90px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Adjustments</asp:Label>
<asp:TextBox id="txtComments" style="Z-INDEX: 101;  Left: 95px; POSITION: absolute; TOP: 55px" runat="server"  Width="830px"  Height="40px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblComments_Label" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 55px" runat="server"  Width="85px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Comments</asp:Label>
<asp:DropDownList id="cmbBuildingPhase" style="Z-INDEX: 101;  Left: 275px; POSITION: absolute; TOP: 60px" runat="server"  Width="120px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Label id="lblLabel23" style="Z-INDEX: 101;  Left: 155px; POSITION: absolute; TOP: 60px" runat="server"  Width="94px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >BuildingPhase:</asp:Label>
<asp:DropDownList id="cmbRoomDescription" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 30px" runat="server"  Width="100px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Label id="lblRoomDescription_Label" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 5px" runat="server"  Width="100px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Location</asp:Label>
<asp:DropDownList id="cmbUpgradeCategory" style="Z-INDEX: 101;  Left: 110px; POSITION: absolute; TOP: 30px" runat="server"  Width="110px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Label id="lblLabel25" style="Z-INDEX: 101;  Left: 110px; POSITION: absolute; TOP: 5px" runat="server"  Width="114px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Upgrade Category</asp:Label>
<asp:TextBox id="txtRequestedUpgradeFlexText1" style="Z-INDEX: 101;  Left: 535px; POSITION: absolute; TOP: 70px" runat="server"  Width="120px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblLabel26" style="Z-INDEX: 101;  Left: 415px; POSITION: absolute; TOP: 70px" runat="server"  Width="184px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >RequestedUpgradeFlexText1:</asp:Label>
<asp:TextBox id="txtQuantity" style="Z-INDEX: 101;  Left: 715px; POSITION: absolute; TOP: 30px" runat="server"  Width="25px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblLabel28" style="Z-INDEX: 101;  Left: 715px; POSITION: absolute; TOP: 5px" runat="server"  Width="26px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Qty</asp:Label>
<asp:Label id="lblGenerated" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 125px" runat="server" ForeColor="#E0E0E0">This page is generated using ASP.NET Wizard.</asp:Label>
    </form>
  </body>
</html>
