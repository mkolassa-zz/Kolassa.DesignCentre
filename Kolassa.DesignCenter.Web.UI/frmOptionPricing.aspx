<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kolassa.DesignCenter.Web.UI.frmOptionPricing" Codebehind="frmOptionPricing.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>frmOptionPricing</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
<asp:Label id="lblUnitType_Label" style="Z-INDEX: 101;  Left: 40px; POSITION: absolute; TOP: 80px" runat="server"  Width="65px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Unit Type</asp:Label>
<asp:Label id="lblLocation_Label" style="Z-INDEX: 101;  Left: 115px; POSITION: absolute; TOP: 80px" runat="server"  Width="105px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Location</asp:Label>
<asp:Label id="lblRev_Label" style="Z-INDEX: 101;  Left: 1260px; POSITION: absolute; TOP: 80px" runat="server"  Width="45px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Rev</asp:Label>
<asp:Label id="lblDescription_Label" style="Z-INDEX: 101;  Left: 475px; POSITION: absolute; TOP: 80px" runat="server"  Width="205px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Description</asp:Label>
<asp:Label id="lblCustomerPrice_Label" style="Z-INDEX: 101;  Left: 1095px; POSITION: absolute; TOP: 65px" runat="server"  Width="70px"  Height="35px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Customer Price</asp:Label>
<asp:Label id="lblUpgradeCat_Label" style="Z-INDEX: 101;  Left: 230px; POSITION: absolute; TOP: 80px" runat="server"  Width="114px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Upgrade Category</asp:Label>
<asp:Label id="lblLabel18" style="Z-INDEX: 101;  Left: 350px; POSITION: absolute; TOP: 80px" runat="server"  Width="120px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Upgrade Level</asp:Label>
<asp:Label id="lblModel_Label" style="Z-INDEX: 101;  Left: 735px; POSITION: absolute; TOP: 80px" runat="server"  Width="90px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Model or Style</asp:Label>
<asp:Label id="lblLabel26" style="Z-INDEX: 101;  Left: 1295px; POSITION: absolute; TOP: 80px" runat="server"  Width="45px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Active</asp:Label>
<asp:Label id="lblLabel27" style="Z-INDEX: 101;  Left: 960px; POSITION: absolute; TOP: 80px" runat="server"  Width="85px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Option Status</asp:Label>
<asp:DropDownList id="cmbSearchLocation" style="Z-INDEX: 101;  Left: 80px; POSITION: absolute; TOP: 40px" runat="server"  Width="160px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Label id="lblLabel34" style="Z-INDEX: 101;  Left: 0px; POSITION: absolute; TOP: 40px" runat="server"  Width="75px"  Height="18px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Location</asp:Label>
<asp:DropDownList id="cmbSearchUpgradeCategory" style="Z-INDEX: 101;  Left: 309px; POSITION: absolute; TOP: 10px" runat="server"  Width="346px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Label id="lblLabel35" style="Z-INDEX: 101;  Left: 245px; POSITION: absolute; TOP: 10px" runat="server"  Width="59px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Category</asp:Label>
<asp:DropDownList id="cmbSearchUnitType" style="Z-INDEX: 101;  Left: 145px; POSITION: absolute; TOP: 10px" runat="server"  Width="95px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Label id="lblLabel36" style="Z-INDEX: 101;  Left: 65px; POSITION: absolute; TOP: 10px" runat="server"  Width="75px"  Height="18px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Unit Type</asp:Label>
<asp:DropDownList id="cmbSearchUpgradeLevel" style="Z-INDEX: 101;  Left: 309px; POSITION: absolute; TOP: 40px" runat="server"  Width="346px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Label id="lblLabel37" style="Z-INDEX: 101;  Left: 245px; POSITION: absolute; TOP: 40px" runat="server"  Width="39px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Level</asp:Label>
<asp:DropDownList id="cmbSearchOptionStatus" style="Z-INDEX: 101;  Left: 750px; POSITION: absolute; TOP: 10px" runat="server"  Width="90px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Label id="lblLabel33" style="Z-INDEX: 101;  Left: 659px; POSITION: absolute; TOP: 10px" runat="server"  Width="86px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Option Status</asp:Label>
<asp:Panel id="pnlBox38" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 5px" runat="server"  Width="1280px"  Height="60px"          BorderWidth="1px"></asp:Panel>
<asp:Label id="lblLabel39" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 5px" runat="server"  Width="65px"  Height="25px"  BackColor="white"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="black"   >Search</asp:Label>
<asp:Label id="lblLabel40" style="Z-INDEX: 101;  Left: 1350px; POSITION: absolute; TOP: 80px" runat="server"  Width="94px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Building Phase</asp:Label>
<asp:Label id="lblLabel41" style="Z-INDEX: 101;  Left: 1470px; POSITION: absolute; TOP: 80px" runat="server"  Width="94px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Lead Vendor</asp:Label>
<asp:Label id="lblLabel42" style="Z-INDEX: 101;  Left: 1595px; POSITION: absolute; TOP: 80px" runat="server"  Width="94px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Comments</asp:Label>
<asp:Button id="btncmdClear" style="Z-INDEX: 101;  Left: 865px; POSITION: absolute; TOP: 10px" runat="server"  Width="96px"  Height="25px"   Font-Names=""  Font-Size=""    ForeColor="0"  Text="Clear Search"  ></asp:Button>
<asp:Label id="lblLabel46" style="Z-INDEX: 101;  Left: 1055px; POSITION: absolute; TOP: 80px" runat="server"  Width="25px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Std</asp:Label>
<asp:Label id="lblLabel47" style="Z-INDEX: 101;  Left: 1185px; POSITION: absolute; TOP: 65px" runat="server"  Width="70px"  Height="35px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Developer Price</asp:Label>
<asp:Button id="btncmdExplodeSelections" style="Z-INDEX: 101;  Left: 980px; POSITION: absolute; TOP: 10px" runat="server"  Width="135px"  Height="25px"   Font-Names=""  Font-Size=""    ForeColor="0"  Text="Explode Selections"  ></asp:Button>
<asp:Button id="btncmdExcelExport" style="Z-INDEX: 101;  Left: 1130px; POSITION: absolute; TOP: 10px" runat="server"  Width="135px"  Height="25px"   Font-Names=""  Font-Size=""    ForeColor="0"  Text="Export to Excel"  ></asp:Button>
<asp:Label id="lblLabel54" style="Z-INDEX: 101;  Left: 1925px; POSITION: absolute; TOP: 80px" runat="server"  Width="94px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >File to Print</asp:Label>
<asp:DropDownList id="cmbSearchDescription" style="Z-INDEX: 101;  Left: 750px; POSITION: absolute; TOP: 40px" runat="server"  Width="422px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Label id="lblLabel56" style="Z-INDEX: 101;  Left: 660px; POSITION: absolute; TOP: 40px" runat="server"  Width="72px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Description</asp:Label>
<asp:TextBox id="txtPricingRevNumber" style="Z-INDEX: 101;  Left: 1255px; POSITION: absolute; TOP: 5px" runat="server"  Width="30px"  Height="19px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtDescription" style="Z-INDEX: 101;  Left: 470px; POSITION: absolute; TOP: 5px" runat="server"  Width="255px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtCustomerPrice" style="Z-INDEX: 101;  Left: 1075px; POSITION: absolute; TOP: 5px" runat="server"  Width="85px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtComments" style="Z-INDEX: 101;  Left: 1590px; POSITION: absolute; TOP: 5px" runat="server"  Width="325px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtUpgradeOptionID" style="Z-INDEX: 101;  Left: 0px; POSITION: absolute; TOP: 5px" runat="server"  Width="10px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:DropDownList id="cmbUnitType" style="Z-INDEX: 101;  Left: 35px; POSITION: absolute; TOP: 5px" runat="server"  Width="70px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:DropDownList id="cmbLocation" style="Z-INDEX: 101;  Left: 110px; POSITION: absolute; TOP: 5px" runat="server"  Width="110px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:TextBox id="txtModelOrStyle" style="Z-INDEX: 101;  Left: 730px; POSITION: absolute; TOP: 5px" runat="server"  Width="225px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:DropDownList id="cmbOptionStatus" style="Z-INDEX: 101;  Left: 960px; POSITION: absolute; TOP: 5px" runat="server"  Width="80px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:CheckBox id="chkActive" style="Z-INDEX: 101;  Left: 1288px; POSITION: absolute; TOP: 1px" runat="server"  Width="22px"  Height="20px"         ></asp:CheckBox>
<asp:DropDownList id="cmbBuildingPhase" style="Z-INDEX: 101;  Left: 1340px; POSITION: absolute; TOP: 5px" runat="server"  Width="120px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:DropDownList id="cmbLeadVendor" style="Z-INDEX: 101;  Left: 1465px; POSITION: absolute; TOP: 5px" runat="server"  Width="120px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:DropDownList id="cmbUpgradeCategory" style="Z-INDEX: 101;  Left: 225px; POSITION: absolute; TOP: 5px" runat="server"  Width="115px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:DropDownList id="cmbUpgradeLevel" style="Z-INDEX: 101;  Left: 345px; POSITION: absolute; TOP: 5px" runat="server"  Width="120px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:CheckBox id="chkStandard" style="Z-INDEX: 101;  Left: 1033px; POSITION: absolute; TOP: -2px" runat="server"  Width="25px"  Height="20px"         ></asp:CheckBox>
<asp:TextBox id="txtDeveloperPrice" style="Z-INDEX: 101;  Left: 1165px; POSITION: absolute; TOP: 5px" runat="server"  Width="85px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Button id="btncmdOptionDetails" style="Z-INDEX: 101;  Left: 0px; POSITION: absolute; TOP: 5px" runat="server"  Width="30px"  Height="26px"   Font-Names=""  Font-Size=""    ForeColor="0"  Text="Command51"  ></asp:Button>
<asp:TextBox id="txtAdditionalFileToPrint1" style="Z-INDEX: 101;  Left: 1920px; POSITION: absolute; TOP: 5px" runat="server"  Width="230px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblGenerated" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 130px" runat="server" ForeColor="#E0E0E0">This page is generated using ASP.NET Wizard.</asp:Label>
    </form>
  </body>
</html>
