<%@ Page Language="vb" AutoEventWireup="false" Inherits="frmLevels" Codebehind="frmLevels.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>frmLevels</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
<asp:DropDownList id="cmbcboEntityType" style="Z-INDEX: 101;  Left: 25px; POSITION: absolute; TOP: 35px" runat="server"  Width="95px"  Height="21px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:DropDownList>
<asp:Panel id="pnlFrame18" style="Z-INDEX: 101;  Left: 10px; POSITION: absolute; TOP: 15px" runat="server"  Width="760px"  Height="755px"          BorderWidth="1px"></asp:Panel>
<asp:Label id="lblLabel19" style="Z-INDEX: 101;  Left: 20px; POSITION: absolute; TOP: 5px" runat="server"  Width="111px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="32768"   >Original Data</asp:Label>
<asp:Panel id="pnlFrame20" style="Z-INDEX: 101;  Left: 775px; POSITION: absolute; TOP: 80px" runat="server"  Width="295px"  Height="570px"          BorderWidth="1px"></asp:Panel>
<asp:Label id="lblLabel21" style="Z-INDEX: 101;  Left: 805px; POSITION: absolute; TOP: 70px" runat="server"  Width="110px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""  Font-Bold="True"    ForeColor="10040115"   >Current Levels</asp:Label>
<asp:Button id="btncmdSave" style="Z-INDEX: 101;  Left: 775px; POSITION: absolute; TOP: 25px" runat="server"  Width="51px"  Height="34px"   Font-Names=""  Font-Size=""    ForeColor="0"  Text="Save"  ></asp:Button>
<asp:Button id="btncmdCancel" style="Z-INDEX: 101;  Left: 860px; POSITION: absolute; TOP: 25px" runat="server"  Width="61px"  Height="34px"   Font-Names=""  Font-Size=""    ForeColor="0"  Text="&Cancel"  ></asp:Button>
<asp:Button id="btncmdClose" style="Z-INDEX: 101;  Left: 940px; POSITION: absolute; TOP: 25px" runat="server"  Width="52px"  Height="34px"   Font-Names=""  Font-Size=""    ForeColor="0"  Text="&Close"  ></asp:Button>
<asp:ListBox id="lstcboClass2" style="Z-INDEX: 101;  Left: 790px; POSITION: absolute; TOP: 115px" runat="server"  Width="275px"  Height="385px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:ListBox>
<asp:TextBox id="txttxtNewLevel" style="Z-INDEX: 101;  Left: 795px; POSITION: absolute; TOP: 550px" runat="server"  Width="270px"  Height="30px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:TextBox>
<asp:Label id="lblLabel37" style="Z-INDEX: 101;  Left: 795px; POSITION: absolute; TOP: 525px" runat="server"  Width="74px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >New Level:</asp:Label>
<asp:ListBox id="lstcboDescription1" style="Z-INDEX: 101;  Left: 60px; POSITION: absolute; TOP: 435px" runat="server"  Width="700px"  Height="165px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:ListBox>
<asp:Label id="lblLabel39" style="Z-INDEX: 101;  Left: 15px; POSITION: absolute; TOP: 435px" runat="server"  Width="38px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Desc</asp:Label>
<asp:ListBox id="lstcboModelOrStyle1" style="Z-INDEX: 101;  Left: 60px; POSITION: absolute; TOP: 605px" runat="server"  Width="700px"  Height="160px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:ListBox>
<asp:Label id="lblLabel38" style="Z-INDEX: 101;  Left: 15px; POSITION: absolute; TOP: 605px" runat="server"  Width="46px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Model:</asp:Label>
<asp:ListBox id="lstcboClass1" style="Z-INDEX: 101;  Left: 60px; POSITION: absolute; TOP: 260px" runat="server"  Width="700px"  Height="170px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:ListBox>
<asp:Label id="lblLabel40" style="Z-INDEX: 101;  Left: 20px; POSITION: absolute; TOP: 260px" runat="server"  Width="42px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Level:</asp:Label>
<asp:Button id="btncmdResetStatus" style="Z-INDEX: 101;  Left: 870px; POSITION: absolute; TOP: 720px" runat="server"  Width="52px"  Height="34px"   Font-Names=""  Font-Size=""    ForeColor="0"  Text="&Reset"  ></asp:Button>
<asp:ListBox id="lstcboRoom1" style="Z-INDEX: 101;  Left: 170px; POSITION: absolute; TOP: 65px" runat="server"  Width="130px"  Height="190px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:ListBox>
<asp:Label id="lblLabel42" style="Z-INDEX: 101;  Left: 165px; POSITION: absolute; TOP: 40px" runat="server"  Width="69px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Room:</asp:Label>
<asp:ListBox id="lstcboCategory1" style="Z-INDEX: 101;  Left: 310px; POSITION: absolute; TOP: 65px" runat="server"  Width="450px"  Height="190px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:ListBox>
<asp:Label id="lblLabel41" style="Z-INDEX: 101;  Left: 305px; POSITION: absolute; TOP: 40px" runat="server"  Width="69px"  Height="20px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   >Category:</asp:Label>
<asp:ListBox id="lstcboEntity" style="Z-INDEX: 101;  Left: 25px; POSITION: absolute; TOP: 65px" runat="server"  Width="135px"  Height="190px"  BackColor="white"  Font-Names=""  Font-Size=""    ForeColor="black"   ></asp:ListBox>
<asp:Button id="btncmdClearHistory" style="Z-INDEX: 101;  Left: 950px; POSITION: absolute; TOP: 720px" runat="server"  Width="100px"  Height="35px"   Font-Names=""  Font-Size=""    ForeColor="0"  Text="&Clear History"  ></asp:Button>
<asp:Label id="lblGenerated" style="Z-INDEX: 101; LEFT: 10px; POSITION: absolute; TOP: 800px" runat="server" ForeColor="#E0E0E0">This page is generated using ASP.NET Wizard.</asp:Label>
    </form>
  </body>
</html>
