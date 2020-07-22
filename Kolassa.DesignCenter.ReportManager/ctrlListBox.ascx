<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlListBox.ascx.vb" Inherits="Select2017.ctrlListBox" %>

<%@ Register Assembly="MenuPilot" Namespace="MenuPilot.Web.Ui" TagPrefix="MenuPilot" %>

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
        <table width=400px><tr><td class="criteria"><asp:Label ID="lblField1" runat="server" Text="Label"></asp:Label>
            &nbsp;
            <asp:ListBox ID="ctrlField1" runat="server"></asp:ListBox>
<asp:Label ID="lblField2" runat="server" Text="Label"></asp:Label>
            <a href="App_Data/eppm.mdb"></a>&nbsp;<asp:ListBox
    ID="ctrlField2" runat="server" OnSelectedIndexChanged="ctrlField2_SelectedIndexChanged"></asp:ListBox>
            <br />
            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="CustomValidator"></asp:CustomValidator>&nbsp;
        </td></tr></table>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="DropDownList1"  EventName="SelectedIndexChanged" />     
    </Triggers>
</asp:UpdatePanel>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
</asp:UpdatePanel>