<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmImagePopOut.aspx.vb" Inherits="Kolassa.DesignCentre.UI.frmImagePopOut" %>
<%@ Register Src="~/Controls/ctrlImageNew.ascx" TagPrefix="uc1" TagName="ctrlImageNew" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Image Pop Out Form</title>
</head>
<body>
    <form id="frmImagePopOut" runat="server" >
        <asp:ScriptManager runat="server" EnablePageMethods="true" /> 
        <div>
            <ajaxToolkit:AjaxFileUpload ID="AjaxFileUpload1" runat="server" />
            <asp:Label ID="lblObjectID" runat="server" />
            <asp:Literal ID="litMessage" runat="server" Text="<b>litMessage</b>" />
            <asp:TextBox ID ="txtDescription" runat="server" Visible="false" />
            <br/><asp:TextBox ID ="txtPath" runat="server" Visible="True" TextMode="MultiLine" Rows="5"/><br/>
            <input id="btnCloseWindow" type="button" value="Close Window" onClick="javascript:window.close();"/>
            <asp:Button ID="btnpost" runat="server" Text="Post"  />
        </div>
    </form>
</body>
</html>
