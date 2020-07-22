<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kolassa.DesignCenter.Web.UI.frmOptionPricingQuickLoad" Codebehind="frmOptionPricingQuickLoad.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>frmOptionPricingQuickLoad</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
<asp:DropDownList id="cmbUnitType" style="Z-INDEX: 101;  Left: 140px; POSITION: absolute; TOP: 10px" runat="server"  Width="85px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Label id="lblUnitType_Label" style="Z-INDEX: 101;  Left: 70px; POSITION: absolute; TOP: 10px" runat="server"  Width="65px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Unit Type</asp:Label>
<asp:DropDownList id="cmbLocation" style="Z-INDEX: 101;  Left: 405px; POSITION: absolute; TOP: 10px" runat="server"  Width="155px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Label id="lblLocation_Label" style="Z-INDEX: 101;  Left: 335px; POSITION: absolute; TOP: 10px" runat="server"  Width="65px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Location</asp:Label>
<asp:DropDownList id="cmbUpgradeCategory" style="Z-INDEX: 101;  Left: 140px; POSITION: absolute; TOP: 40px" runat="server"  Width="160px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Label id="lblUpgradeCat_Label" style="Z-INDEX: 101;  Left: 20px; POSITION: absolute; TOP: 40px" runat="server"  Width="115px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Upgrade Category</asp:Label>
<asp:DropDownList id="cmbUpgradeLevel" style="Z-INDEX: 101;  Left: 405px; POSITION: absolute; TOP: 40px" runat="server"  Width="155px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Label id="lblLabel18" style="Z-INDEX: 101;  Left: 305px; POSITION: absolute; TOP: 40px" runat="server"  Width="95px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Upgrade Level</asp:Label>
<asp:TextBox id="txtDescription" style="Z-INDEX: 101;  Left: 140px; POSITION: absolute; TOP: 70px" runat="server"  Width="420px"  Height="19px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblDescription_Label" style="Z-INDEX: 101;  Left: 40px; POSITION: absolute; TOP: 70px" runat="server"  Width="95px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Description</asp:Label>
<asp:TextBox id="txtModelOrStyle" style="Z-INDEX: 101;  Left: 140px; POSITION: absolute; TOP: 100px" runat="server"  Width="420px"  Height="19px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblModel_Label" style="Z-INDEX: 101;  Left: 40px; POSITION: absolute; TOP: 100px" runat="server"  Width="95px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Model or Style</asp:Label>
<asp:TextBox id="txtUpgradeOptionID" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 5px" runat="server"  Width="115px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtPricingRevNumber" style="Z-INDEX: 101;  Left: 770px; POSITION: absolute; TOP: 125px" runat="server"  Width="30px"  Height="19px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblRev_Label" style="Z-INDEX: 101;  Left: 720px; POSITION: absolute; TOP: 125px" runat="server"  Width="45px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Rev</asp:Label>
<asp:TextBox id="txtCustomerPrice" style="Z-INDEX: 101;  Left: 680px; POSITION: absolute; TOP: 40px" runat="server"  Width="85px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblCustomerPrice_Label" style="Z-INDEX: 101;  Left: 570px; POSITION: absolute; TOP: 40px" runat="server"  Width="105px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Customer Price</asp:Label>
<asp:TextBox id="txtComments" style="Z-INDEX: 101;  Left: 140px; POSITION: absolute; TOP: 130px" runat="server"  Width="420px"  Height="19px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblLabel42" style="Z-INDEX: 101;  Left: 40px; POSITION: absolute; TOP: 130px" runat="server"  Width="94px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Comments</asp:Label>
<asp:DropDownList id="cmbOptionStatus" style="Z-INDEX: 101;  Left: 680px; POSITION: absolute; TOP: 10px" runat="server"  Width="80px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Label id="lblLabel27" style="Z-INDEX: 101;  Left: 590px; POSITION: absolute; TOP: 10px" runat="server"  Width="85px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Option Status</asp:Label>
<asp:CheckBox id="chkActive" style="Z-INDEX: 101;  Left: 663px; POSITION: absolute; TOP: 123px" runat="server"  Width="22px"  Height="20px"         ></asp:CheckBox>
<asp:Label id="lblLabel26" style="Z-INDEX: 101;  Left: 625px; POSITION: absolute; TOP: 130px" runat="server"  Width="50px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Active</asp:Label>
<asp:DropDownList id="cmbBuildingPhase" style="Z-INDEX: 101;  Left: 680px; POSITION: absolute; TOP: 100px" runat="server"  Width="120px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Label id="lblLabel40" style="Z-INDEX: 101;  Left: 580px; POSITION: absolute; TOP: 100px" runat="server"  Width="95px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Building Phase</asp:Label>
<asp:DropDownList id="cmbLeadVendor" style="Z-INDEX: 101;  Left: 680px; POSITION: absolute; TOP: 70px" runat="server"  Width="120px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Label id="lblLabel41" style="Z-INDEX: 101;  Left: 580px; POSITION: absolute; TOP: 70px" runat="server"  Width="95px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Lead Vendor</asp:Label>
<asp:Panel id="pnlBox43" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 5px" runat="server"  Width="820px"  Height="150px"          BorderWidth="1px"></asp:Panel>
<asp:Label id="lblGenerated" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 185px" runat="server" ForeColor="#E0E0E0">This page is generated using ASP.NET Wizard.</asp:Label>
    </form>
  </body>
</html>
