<%@ Page Language="vb" EnableEventValidation="false" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="frmDataAdmin.aspx.vb" Inherits="Kolassa.DesignCentre.UI.frmDataAdmin" %>
<%@ Register Assembly="Kolassa.DesignCenter.ReportManager" Namespace="Kolassa.DesignCenter.ReportManager" TagPrefix="cc1" %>




<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent" >

    <link  rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>
    <style>
       th {
                -webkit-transform: translate(5px, 1px) rotate(315deg);
                -ms-transform: translate(5px, 1px) rotate(315deg);
                transform: translate(5px, 1px) rotate(315deg);
                white-space: nowrap;
                height: 200px;
                width: 30px;
                color:blue;
                border-collapse: collapse;
                border: hidden;
                border-bottom:double;
                padding: 5px 10px;
                text-align:left;
       }
       .table-header-rotated {
  border-collapse: collapse;
}
.table-header-rotated td {
  width: 30px;
}
 .table-header-rotated th {
  padding: 5px 10px;
}
.table-header-rotated td {
  text-align: center;
  padding: 10px 5px;
  border: 1px solid #ccc;
}
 .table-header-rotated th.rotate {
  height: 140px;
  white-space: nowrap;
}
 .table-header-rotated th.rotate > div {
  -webkit-transform: translate(25px, 51px) rotate(315deg);
      -ms-transform: translate(25px, 51px) rotate(315deg);
          transform: translate(25px, 51px) rotate(315deg);
  width: 30px;
}
 .table-header-rotated th.rotate > div > span {
  border-bottom: 1px solid #ccc;
  padding: 5px 10px;
}
.table-header-rotated th.row-header {
  padding: 0 10px;
  border-bottom: 1px solid #ccc;
}
</style>

    <asp:ListBox ID="lstProject" runat="server" ></asp:ListBox>
    <asp:ListBox ID="lstPhase" runat="server" ></asp:ListBox>
    <asp:ListBox ID="lstUnitType" runat="server" ></asp:ListBox>
    <asp:ListBox ID="lstRoom" runat="server" ></asp:ListBox>
    <asp:ListBox ID="lstCategoryDetail" runat="server" AutoPostBack="True"></asp:ListBox>
    <asp:ListBox ID="lstUpgradeOption" runat="server" AutoPostBack="True"></asp:ListBox>
    <asp:ListBox ID="lstSelectedUpgrade" runat="server" ></asp:ListBox>

<asp:Button ID="cmdRefresh" runat="server" Text="Refresh" />

 

<script>

    
</script>

</asp:Content>