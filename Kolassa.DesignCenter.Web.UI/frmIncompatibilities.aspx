<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kolassa.DesignCenter.Web.UI.frmIncompatibilities" Codebehind="frmIncompatibilities.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>frmIncompatibilities</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
<asp:TextBox id="txtIncompatibilityID" style="Z-INDEX: 101;  Left: 140px; POSITION: absolute; TOP: 10px" runat="server"  Width="120px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblIncompatibilityID_Label" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 10px" runat="server"  Width="130px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Incompatibility ID</asp:Label>
<asp:DropDownList id="cmbcboCategory1" style="Z-INDEX: 101;  Left: 75px; POSITION: absolute; TOP: 200px" runat="server"  Width="385px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:DropDownList id="cmbSeverity" style="Z-INDEX: 101;  Left: 350px; POSITION: absolute; TOP: 10px" runat="server"  Width="100px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Label id="lblSeverity_Label" style="Z-INDEX: 101;  Left: 285px; POSITION: absolute; TOP: 10px" runat="server"  Width="54px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Severity</asp:Label>
<asp:Panel id="pnlFrame16" style="Z-INDEX: 101;  Left: 60px; POSITION: absolute; TOP: 50px" runat="server"  Width="190px"  Height="85px"          BorderWidth="1px"></asp:Panel>
<asp:Label id="lblLabel17" style="Z-INDEX: 101;  Left: 70px; POSITION: absolute; TOP: 40px" runat="server"  Width="50px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="128"   >Entity</asp:Label>
<asp:DropDownList id="cmbcboEntityType" style="Z-INDEX: 101;  Left: 80px; POSITION: absolute; TOP: 70px" runat="server"  Width="95px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:DropDownList id="cmbcboEntity" style="Z-INDEX: 101;  Left: 80px; POSITION: absolute; TOP: 100px" runat="server"  Width="135px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Panel id="pnlFrame18" style="Z-INDEX: 101;  Left: 60px; POSITION: absolute; TOP: 155px" runat="server"  Width="430px"  Height="170px"          BorderWidth="1px"></asp:Panel>
<asp:Label id="lblLabel19" style="Z-INDEX: 101;  Left: 70px; POSITION: absolute; TOP: 145px" runat="server"  Width="51px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="32768"   >Item 1</asp:Label>
<asp:Panel id="pnlFrame20" style="Z-INDEX: 101;  Left: 60px; POSITION: absolute; TOP: 350px" runat="server"  Width="430px"  Height="170px"          BorderWidth="1px"></asp:Panel>
<asp:Label id="lblLabel21" style="Z-INDEX: 101;  Left: 75px; POSITION: absolute; TOP: 340px" runat="server"  Width="51px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="10040115"   >Item 2</asp:Label>
<asp:DropDownList id="cmbcboClass1" style="Z-INDEX: 101;  Left: 75px; POSITION: absolute; TOP: 230px" runat="server"  Width="385px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Button id="btncmdSave" style="Z-INDEX: 101;  Left: 255px; POSITION: absolute; TOP: 530px" runat="server"  Width="51px"  Height="34px"   Font-Names=""  Font-Size=""    ForeColor="0"  Text="Save"  ></asp:Button>
<asp:Button id="btncmdCancel" style="Z-INDEX: 101;  Left: 325px; POSITION: absolute; TOP: 530px" runat="server"  Width="61px"  Height="34px"   Font-Names=""  Font-Size=""    ForeColor="0"  Text="&Cancel"  ></asp:Button>
<asp:Button id="btncmdClose" style="Z-INDEX: 101;  Left: 415px; POSITION: absolute; TOP: 530px" runat="server"  Width="52px"  Height="34px"   Font-Names=""  Font-Size=""    ForeColor="0"  Text="&Close"  ></asp:Button>
<asp:DropDownList id="cmbcboRoom1" style="Z-INDEX: 101;  Left: 75px; POSITION: absolute; TOP: 170px" runat="server"  Width="385px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:DropDownList id="cmbcboDescription1" style="Z-INDEX: 101;  Left: 75px; POSITION: absolute; TOP: 260px" runat="server"  Width="385px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:DropDownList id="cmbcboModelOrStyle1" style="Z-INDEX: 101;  Left: 75px; POSITION: absolute; TOP: 290px" runat="server"  Width="385px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:DropDownList id="cmbcboCategory2" style="Z-INDEX: 101;  Left: 75px; POSITION: absolute; TOP: 400px" runat="server"  Width="385px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:DropDownList id="cmbcboClass2" style="Z-INDEX: 101;  Left: 75px; POSITION: absolute; TOP: 430px" runat="server"  Width="385px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:DropDownList id="cmbcboRoom2" style="Z-INDEX: 101;  Left: 75px; POSITION: absolute; TOP: 370px" runat="server"  Width="385px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:DropDownList id="cmbcboDescription2" style="Z-INDEX: 101;  Left: 75px; POSITION: absolute; TOP: 460px" runat="server"  Width="385px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:DropDownList id="cmbcboModelOrStyle2" style="Z-INDEX: 101;  Left: 75px; POSITION: absolute; TOP: 490px" runat="server"  Width="385px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Label id="lblGenerated" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 594px" runat="server" ForeColor="#E0E0E0">This page is generated using ASP.NET Wizard.</asp:Label>
    </form>
  </body>
</html>
