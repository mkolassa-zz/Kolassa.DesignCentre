<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTextBox.ascx.vb" Inherits="Select2017.ctrlTextBox" %>
<%@ Register Assembly="MenuPilot" Namespace="MenuPilot.Web.Ui" TagPrefix="MenuPilot" %>

<link href="css/report.css" rel="stylesheet" type="text/css" />
<table width="100%"><tr><td class="menu">
<asp:Label ID="lblFieldname" runat="server" Text="Label"></asp:Label>&nbsp;

<asp:DropDownList  ID="DropDownList1" runat="server" AutoPostBack="True" ForeColor="Navy" ToolTip="Select the operator" CssClass="dropdownmenu" >
    <asp:ListItem>=</asp:ListItem>
    <asp:ListItem>&gt;</asp:ListItem>
    <asp:ListItem>&gt;=</asp:ListItem>
    <asp:ListItem>&lt;</asp:ListItem>
    <asp:ListItem>&lt;=</asp:ListItem>
    <asp:ListItem>Between</asp:ListItem>
</asp:DropDownList>&nbsp;</td></tr></table>
<asp:UpdatePanel ID="UpdatePanel1"   runat="server">
    <ContentTemplate >
        <table class="criteria"><tr><td class="criteria"  >
        <asp:Label ID="lblField1" runat="server" Text="Label"></asp:Label>
<asp:TextBox ID="ctrlField1" runat="server" Width="56px"></asp:TextBox>&nbsp;
<asp:Label ID="lblField2" runat="server" Text="Label"></asp:Label>
<asp:TextBox ID="ctrlField2" runat="server" Width="88px"></asp:TextBox><a href="App_Data/eppm.mdb"></a>&nbsp;<br />
            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="CustomValidator"></asp:CustomValidator>&nbsp;
        </td></tr></table>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="DropDownList1"  EventName="SelectedIndexChanged" />
       
    </Triggers>
</asp:UpdatePanel>