<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kolassa.DesignCenter.Web.UI.frmIncompatlist" Codebehind="frmIncompatlist.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>frmIncompatlist</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
<asp:Label id="lblEntityType_Label" style="Z-INDEX: 101;  Left: 40px; POSITION: absolute; TOP: 50px" runat="server"  Width="150px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="16777215"   >Entity </asp:Label>
<asp:Label id="lblLocation1_Label" style="Z-INDEX: 101;  Left: 255px; POSITION: absolute; TOP: 50px" runat="server"  Width="150px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="16777215"   >Location</asp:Label>
<asp:Label id="lblCategory1_Label" style="Z-INDEX: 101;  Left: 425px; POSITION: absolute; TOP: 50px" runat="server"  Width="150px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="16777215"   >Category</asp:Label>
<asp:Label id="lblClass1_Label" style="Z-INDEX: 101;  Left: 595px; POSITION: absolute; TOP: 50px" runat="server"  Width="150px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="16777215"   >Class</asp:Label>
<asp:Label id="lblDescription1_Label" style="Z-INDEX: 101;  Left: 760px; POSITION: absolute; TOP: 50px" runat="server"  Width="290px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="16777215"   >Description</asp:Label>
<asp:Label id="lblDescription2_Label" style="Z-INDEX: 101;  Left: 1015px; POSITION: absolute; TOP: 50px" runat="server"  Width="150px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="16777215"   >Model</asp:Label>
<asp:Label id="lblLabel28" style="Z-INDEX: 101;  Left: 0px; POSITION: absolute; TOP: 0px" runat="server"  Width="418px"  Height="34px"  BackColor="white"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="16777215"   >Incompatibility / Required Items List</asp:Label>
<asp:Button id="btncmdNew" style="Z-INDEX: 101;  Left: 455px; POSITION: absolute; TOP: 5px" runat="server"  Width="75px"  Height="34px"   Font-Names=""  Font-Size=""    ForeColor="0"  Text="&Add New"  ></asp:Button>
<asp:TextBox id="txttxtID" style="Z-INDEX: 101;  Left: 45px; POSITION: absolute; TOP: 5px" runat="server"  Width="35px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:DropDownList id="cmbEntityType" style="Z-INDEX: 101;  Left: 45px; POSITION: absolute; TOP: 5px" runat="server"  Width="95px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="32768"   ></asp:DropDownList>
<asp:TextBox id="txtEntityID" style="Z-INDEX: 101;  Left: 45px; POSITION: absolute; TOP: 30px" runat="server"  Width="60px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="32768"   ></asp:TextBox>
<asp:TextBox id="txtLocation1" style="Z-INDEX: 101;  Left: 270px; POSITION: absolute; TOP: 5px" runat="server"  Width="150px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtLocation2" style="Z-INDEX: 101;  Left: 270px; POSITION: absolute; TOP: 30px" runat="server"  Width="150px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="10040115"   ></asp:TextBox>
<asp:TextBox id="txtCategory2" style="Z-INDEX: 101;  Left: 430px; POSITION: absolute; TOP: 30px" runat="server"  Width="150px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="10040115"   ></asp:TextBox>
<asp:TextBox id="txtClass2" style="Z-INDEX: 101;  Left: 600px; POSITION: absolute; TOP: 30px" runat="server"  Width="150px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="10040115"   ></asp:TextBox>
<asp:TextBox id="txtDescription1" style="Z-INDEX: 101;  Left: 765px; POSITION: absolute; TOP: 5px" runat="server"  Width="250px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtDescription2" style="Z-INDEX: 101;  Left: 765px; POSITION: absolute; TOP: 30px" runat="server"  Width="250px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="10040115"   ></asp:TextBox>
<asp:TextBox id="txtModelOrStyle1" style="Z-INDEX: 101;  Left: 1025px; POSITION: absolute; TOP: 5px" runat="server"  Width="220px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtModelOrStyle2" style="Z-INDEX: 101;  Left: 1025px; POSITION: absolute; TOP: 30px" runat="server"  Width="220px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="10040115"   ></asp:TextBox>
<asp:DropDownList id="cmbSeverity" style="Z-INDEX: 101;  Left: 150px; POSITION: absolute; TOP: 5px" runat="server"  Width="110px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="255"   ></asp:DropDownList>
<asp:TextBox id="txtCategory1" style="Z-INDEX: 101;  Left: 430px; POSITION: absolute; TOP: 5px" runat="server"  Width="150px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:TextBox id="txtClass1" style="Z-INDEX: 101;  Left: 600px; POSITION: absolute; TOP: 5px" runat="server"  Width="150px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Button id="btncmdDelete" style="Z-INDEX: 101;  Left: 5px; POSITION: absolute; TOP: 10px" runat="server"  Width="30px"  Height="30px"   Font-Names=""  Font-Size=""    ForeColor="0"  Text="Delete"  ></asp:Button>
<asp:TextBox id="txtIncompatibilityID" style="Z-INDEX: 101;  Left: 145px; POSITION: absolute; TOP: 30px" runat="server"  Width="60px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblGenerated" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 100px" runat="server" ForeColor="#E0E0E0">This page is generated using ASP.NET Wizard.</asp:Label>
    </form>
  </body>
</html>
