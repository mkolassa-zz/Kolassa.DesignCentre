<%@ Page Language="vb" EnableEventValidation="false" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="frmTasks.aspx.vb" Inherits="Kolassa.DesignCentre.UI.frmTasks" %>
<%@ Register Assembly="Kolassa.DesignCenter.ReportManager" Namespace="Kolassa.DesignCenter.ReportManager" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ctrlNotificationsFull.ascx" TagPrefix="uc1" TagName="ctrlNotificationsFull" %>



<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent" >
    <link  rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
    <div class="container">
        <h2>Notifications</h2>
         <asp:LinkButton id="lnkNoteRefresh" runat="Server"><i class="fas fa-recycle  fa-1x"></i></asp:LinkButton>
        <asp:UpdatePanel id="upNotificationsFull" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>
                <uc1:ctrlNotificationsFull runat="server" id="ctrlNotifications" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkNoteRefresh" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="upProgress" runat="server">
            <ProgressTemplate>
                <img src="images/loadingH.gif" /><br />Loading . . .
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>