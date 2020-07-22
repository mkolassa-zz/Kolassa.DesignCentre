<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDateBox.ascx.vb" Inherits="Select2017.ctrlDateBox" %>
<%@ Register Assembly="MenuPilot" Namespace="MenuPilot.Web.Ui" TagPrefix="MenuPilot" %>



<link href="css/report.css" rel="stylesheet" type="text/css" />
<style>
.calendarHide
  {
	display: none;
	position: relative;
  }
.calendarShow
  {
	position: absolute;
	z-index: 0;
	background-color: Silver;
  }
</style>

<table width="100%"><tr><td class="menu" >

<asp:Label ID="lblFieldname" runat="server" Text="Label"></asp:Label>&nbsp;

<asp:DropDownList  ID="DropDownList1" runat="server" AutoPostBack="True" ForeColor="Navy" ToolTip="Select the operator" CssClass="dropdownmenu" >
    <asp:ListItem>=</asp:ListItem>
    <asp:ListItem>&gt;</asp:ListItem>
    <asp:ListItem>&gt;=</asp:ListItem>
    <asp:ListItem>&lt;</asp:ListItem>
    <asp:ListItem>&lt;=</asp:ListItem>
    <asp:ListItem>Between</asp:ListItem>
    <asp:ListItem>Year To Date</asp:ListItem>
    <asp:ListItem>Month To Date</asp:ListItem>
    <asp:ListItem>Quarter To Date</asp:ListItem>
    <asp:ListItem>This Quarter</asp:ListItem>
    <asp:ListItem>Last Quarter</asp:ListItem>
    <asp:ListItem>This Month</asp:ListItem>
    <asp:ListItem>Last Month</asp:ListItem>
    <asp:ListItem>Today</asp:ListItem>
    <asp:ListItem>Last Year</asp:ListItem>
</asp:DropDownList>&nbsp;</td></tr></table>
<asp:UpdatePanel ID="UpdatePanel1"   runat="server" >
    <ContentTemplate >
        <table class="criteria"><tr><td class="criteria"><asp:Label ID="lblField1" runat="server" Text="Label"></asp:Label>
            <asp:TextBox ID="ctrlField1"  runat="server" type="Date" Width="80px"></asp:TextBox> 


            
            
            
            <asp:Label ID="lblField2"  runat="server" Text="Label"></asp:Label>
            <asp:TextBox ID="ctrlField2"  runat="server" Type="Date" Width="88px"></asp:TextBox>&nbsp;

            <br />
  
            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="CustomValidator"></asp:CustomValidator>&nbsp;&nbsp;
        </td></tr></table>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="DropDownList1"  EventName="SelectedIndexChanged" />    
    </Triggers>
    
</asp:UpdatePanel>
