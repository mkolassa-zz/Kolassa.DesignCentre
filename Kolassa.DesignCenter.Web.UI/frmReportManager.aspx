<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kolassa.DesignCenter.Web.UI.frmReportManager" Codebehind="frmReportManager.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>frmReportManager</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
<asp:Button id="btnCmdClose" style="Z-INDEX: 101;  Left: 652px; POSITION: absolute; TOP: 445px" runat="server"  Width="60px"  Height="35px"   Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="8388608"  Text="&Close"  ></asp:Button>
<asp:Button id="btnCmdrunreport" style="Z-INDEX: 101;  Left: 652px; POSITION: absolute; TOP: 405px" runat="server"  Width="96px"  Height="35px"   Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="8388608"  Text="&Run Report"  ></asp:Button>
<asp:ListBox id="lstlstReports" style="Z-INDEX: 101;  Left: 26px; POSITION: absolute; TOP: 85px" runat="server"  Width="259px"  Height="360px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="8388608"   ></asp:ListBox>
<asp:Label id="lblLabel6" style="Z-INDEX: 101;  Left: 25px; POSITION: absolute; TOP: 60px" runat="server"  Width="91px"  Height="21px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="8388608"   >Reports List</asp:Label>
<asp:Panel id="pnlfraReportType" style="Z-INDEX: 101;  Left: 654px; POSITION: absolute; TOP: 315px" runat="server"  Width="89px"  Height="77px"          BorderWidth="1px"></asp:Panel>
<asp:RadioButton id="rbnOptPreview" style="Z-INDEX: 101;  Left: 662px; POSITION: absolute; TOP: 332px" runat="server"  Width="22px"  Height="20px"         ></asp:RadioButton>
<asp:Label id="lblLabel11" style="Z-INDEX: 101;  Left: 682px; POSITION: absolute; TOP: 330px" runat="server"  Width="56px"  Height="21px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="8388608"   >Preview</asp:Label>
<asp:RadioButton id="rbnOptPrint" style="Z-INDEX: 101;  Left: 662px; POSITION: absolute; TOP: 360px" runat="server"  Width="22px"  Height="20px"         ></asp:RadioButton>
<asp:Label id="lblLabel13" style="Z-INDEX: 101;  Left: 682px; POSITION: absolute; TOP: 358px" runat="server"  Width="32px"  Height="21px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="8388608"   >Print</asp:Label>
<asp:RadioButton id="rbnOptFile" style="Z-INDEX: 101;  Left: 692px; POSITION: absolute; TOP: 367px" runat="server"  Width="22px"  Height="20px"         ></asp:RadioButton>
<asp:Label id="lblLabel15" style="Z-INDEX: 101;  Left: 712px; POSITION: absolute; TOP: 365px" runat="server"  Width="26px"  Height="21px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="8388608"   >File</asp:Label>
<asp:DropDownList id="cmbcboReportCategory" style="Z-INDEX: 101;  Left: 25px; POSITION: absolute; TOP: 25px" runat="server"  Width="240px"  Height="21px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="0"   ></asp:DropDownList>
<asp:Label id="lblReportCategoryDescription_Label" style="Z-INDEX: 101;  Left: 25px; POSITION: absolute; TOP: 5px" runat="server"  Width="70px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="8388608"   >Category</asp:Label>
<asp:DropDownList id="cmbcboCustomerName" style="Z-INDEX: 101;  Left: 442px; POSITION: absolute; TOP: 255px" runat="server"  Width="240px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="8388608"   ></asp:DropDownList>
<asp:Label id="lblLabel52" style="Z-INDEX: 101;  Left: 298px; POSITION: absolute; TOP: 255px" runat="server"  Width="140px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="8388608"   >Customer Name</asp:Label>
<asp:TextBox id="txttxtDateStart" style="Z-INDEX: 101;  Left: 445px; POSITION: absolute; TOP: 95px" runat="server"  Width="80px"  Height="21px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="0"   ></asp:TextBox>
<asp:Label id="lblLabel60" style="Z-INDEX: 101;  Left: 300px; POSITION: absolute; TOP: 95px" runat="server"  Width="140px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="8388608"   >Date</asp:Label>
<asp:TextBox id="txttxtDateEnd" style="Z-INDEX: 101;  Left: 575px; POSITION: absolute; TOP: 95px" runat="server"  Width="80px"  Height="21px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="0"   ></asp:TextBox>
<asp:Label id="lblLabel62" style="Z-INDEX: 101;  Left: 530px; POSITION: absolute; TOP: 95px" runat="server"  Width="40px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="8388608"   >Thru</asp:Label>
<asp:DropDownList id="cmbcboBuildingPhase" style="Z-INDEX: 101;  Left: 442px; POSITION: absolute; TOP: 285px" runat="server"  Width="120px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="8388608"   ></asp:DropDownList>
<asp:Label id="lblLabel68" style="Z-INDEX: 101;  Left: 298px; POSITION: absolute; TOP: 285px" runat="server"  Width="140px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="8388608"   >Building Phase</asp:Label>
<asp:DropDownList id="cmbcboFloorStart" style="Z-INDEX: 101;  Left: 442px; POSITION: absolute; TOP: 315px" runat="server"  Width="75px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="8388608"   ></asp:DropDownList>
<asp:Label id="lblLabel72" style="Z-INDEX: 101;  Left: 298px; POSITION: absolute; TOP: 315px" runat="server"  Width="140px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="8388608"   >Floor</asp:Label>
<asp:DropDownList id="cmbcboCommunicationType" style="Z-INDEX: 101;  Left: 442px; POSITION: absolute; TOP: 345px" runat="server"  Width="145px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="8388608"   ></asp:DropDownList>
<asp:Label id="lblLabel80" style="Z-INDEX: 101;  Left: 285px; POSITION: absolute; TOP: 345px" runat="server"  Width="152px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="8388608"   >Communication Type</asp:Label>
<asp:DropDownList id="cmbcboUpgradeCategory" style="Z-INDEX: 101;  Left: 442px; POSITION: absolute; TOP: 375px" runat="server"  Width="200px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="8388608"   ></asp:DropDownList>
<asp:Label id="lblLabel82" style="Z-INDEX: 101;  Left: 298px; POSITION: absolute; TOP: 375px" runat="server"  Width="140px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="8388608"   >Upgrade Category</asp:Label>
<asp:DropDownList id="cmbcboPurchaseStatus" style="Z-INDEX: 101;  Left: 442px; POSITION: absolute; TOP: 465px" runat="server"  Width="145px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="8388608"   ></asp:DropDownList>
<asp:Label id="lblLabel84" style="Z-INDEX: 101;  Left: 298px; POSITION: absolute; TOP: 465px" runat="server"  Width="140px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="8388608"   >Purchase Status</asp:Label>
<asp:DropDownList id="cmbcboOptionGrouping" style="Z-INDEX: 101;  Left: 442px; POSITION: absolute; TOP: 405px" runat="server"  Width="145px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="8388608"   ></asp:DropDownList>
<asp:Label id="lblLabel86" style="Z-INDEX: 101;  Left: 298px; POSITION: absolute; TOP: 405px" runat="server"  Width="140px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="8388608"   >Option Grouping</asp:Label>
<asp:DropDownList id="cmbcboFloorEnd" style="Z-INDEX: 101;  Left: 572px; POSITION: absolute; TOP: 315px" runat="server"  Width="75px"  Height="22px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="8388608"   ></asp:DropDownList>
<asp:Label id="lblLabel90" style="Z-INDEX: 101;  Left: 518px; POSITION: absolute; TOP: 315px" runat="server"  Width="50px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="8388608"   >Thru</asp:Label>
<asp:TextBox id="txtReportComments" style="Z-INDEX: 101;  Left: 305px; POSITION: absolute; TOP: 10px" runat="server"  Width="425px"  Height="30px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="0"   ></asp:TextBox>
<asp:TextBox id="txttxtHeading" style="Z-INDEX: 101;  Left: 445px; POSITION: absolute; TOP: 50px" runat="server"  Width="275px"  Height="35px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="0"   ></asp:TextBox>
<asp:Label id="lblLabel94" style="Z-INDEX: 101;  Left: 295px; POSITION: absolute; TOP: 50px" runat="server"  Width="145px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="8388608"   >Report Heading</asp:Label>
<asp:DropDownList id="cmbcboLocation" style="Z-INDEX: 101;  Left: 442px; POSITION: absolute; TOP: 435px" runat="server"  Width="145px"  Height="22px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="8388608"   ></asp:DropDownList>
<asp:Label id="lblLabel96" style="Z-INDEX: 101;  Left: 298px; POSITION: absolute; TOP: 435px" runat="server"  Width="140px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="8388608"   >Location</asp:Label>
<asp:ListBox id="lstcboUnitNumber" style="Z-INDEX: 101;  Left: 445px; POSITION: absolute; TOP: 125px" runat="server"  Width="90px"  Height="120px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="8388608"   ></asp:ListBox>
<asp:Label id="lblLabel64" style="Z-INDEX: 101;  Left: 300px; POSITION: absolute; TOP: 125px" runat="server"  Width="140px"  Height="20px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="8388608"   >Unit Number</asp:Label>
<asp:ListBox id="lstcboUnitType" style="Z-INDEX: 101;  Left: 625px; POSITION: absolute; TOP: 125px" runat="server"  Width="90px"  Height="120px"  BackColor="16777215"  Font-Names=""  Font-Size=""    ForeColor="8388608"   ></asp:ListBox>
<asp:Label id="lblLabel66" style="Z-INDEX: 101;  Left: 550px; POSITION: absolute; TOP: 125px" runat="server"  Width="70px"  Height="21px"  BackColor="16777215"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="8388608"   >Unit Type</asp:Label>
<asp:Label id="lblGenerated" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 515px" runat="server" ForeColor="#E0E0E0">This page is generated using ASP.NET Wizard.</asp:Label>
    </form>
  </body>
</html>
