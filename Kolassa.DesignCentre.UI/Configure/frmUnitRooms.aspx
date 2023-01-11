<%@ Page Language="vb" EnableEventValidation="false" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="frmUnitRooms.aspx.vb" Inherits="Kolassa.DesignCentre.UI.frmUnitRooms" %>
<%@ Register Assembly="Kolassa.DesignCenter.ReportManager" Namespace="Kolassa.DesignCenter.ReportManager" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ctrlIncompatibilities.ascx" TagPrefix="uc1" TagName="ctrlIncompatibilities" %>


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

 <div class="container">

    <h2>Unit Types Room Configuration Form</h2>
    <asp:UpdatePanel id="upChecks" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:Button CssClass="btn btn-primary" ID="cmdLoad" runat="server" Text="Load Grid" />
            <!-- button class="btn btn-primary" onclick="fbind();">Bind</!-->
            <asp:GridView ID="gvCrosstab" runat="server"> </asp:GridView>
        </ContentTemplate>
        <Triggers>

        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="upProgress" runat="server">
        <ProgressTemplate>
            <img src="images/loadingH.gif" /><br />Loading . . .
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>

<script>
    $(document).ready(function(){

    });

    function fchange(tthis) {
       //  alert('mike: ' + tthis.dataset.id);
         var cls = tthis.closest('span');
         var lsid = cls.dataset.id;
         var lsproject = cls.dataset.project;
         var lsnode = cls.dataset.node;
         var lsuid = cls.dataset.unittypeid;
         var lsrid = cls.dataset.roomid;
         var lsactive = tthis.checked;
         var datas = '{"ID": "'+lsid+'", "lsRoomID": "'+lsrid+'", "lsUnitTypeID": "'+lsuid+'", "lsActive": "'+lsactive+'", "lsproject": "'+lsproject+'", "lsnode": "'+lsnode+'"}'
        $.ajax({
            type: "POST",
            url: "dcConfigure.asmx/fchange",
            data: datas,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $("#Content").text(response.d);
            },
            failure: function (response) {
                alert(response.d);
            }
        });
    };
</script>
   
</asp:Content>