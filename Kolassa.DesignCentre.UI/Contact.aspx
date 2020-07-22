<%@ Page Title="Contact" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.vb" Inherits="Kolassa.DesignCentre.UI.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <p>Find us!!</p>

    <address>
        One Design Centre Way<br />
        Profitstown, WA 98052-6399<br />
        <abbr title="Phone">P:</abbr>
        312.489.1234
    </address>

    <address>
        <strong>Support:</strong><a href="mailto:Support@DesignCentre.com">Support@DesignCentre.com</a><br />
        <strong>Marketing:</strong><a href="mailto:Marketing@DesignCentre.com">Marketing@DesignCentre.com</a>
    </address>
</asp:Content>
