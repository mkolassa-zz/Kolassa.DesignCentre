<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlComboBox.ascx.vb" Inherits="Select2017.ctrlCOmboBox" %>
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
        <table ><tr><td class="criteria" valign="top"><asp:Label ID="lblField1" runat="server" Text="Label"></asp:Label>
            &nbsp;
            <asp:DropDownList ID="ctrlField1" runat="server"></asp:DropDownList>
<asp:Label ID="lblField2" runat="server" Text="Label"></asp:Label>
            <a href="App_Data/eppm.mdb"></a>&nbsp;
            <asp:DropDownList     ID="ctrlField2" runat="server"></asp:DropDownList><br />
            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="CustomValidator"></asp:CustomValidator>&nbsp;
        </td></tr></table>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="DropDownList1"  EventName="SelectedIndexChanged" />     
    </Triggers>

</asp:UpdatePanel>

