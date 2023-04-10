<%@ Page Language="vb" EnableEventValidation="false" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="frmTest.aspx.vb" Inherits="Kolassa.DesignCentre.UI.frmTest" %>
<%@ Register Assembly="Kolassa.DesignCenter.ReportManager" Namespace="Kolassa.DesignCenter.ReportManager" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ctrlIncompatibilities.ascx" TagPrefix="uc1" TagName="ctrlIncompatibilities" %>
<%@ Register Src="~/Controls/ctrlNotifications.ascx" TagPrefix="uc1" TagName="ctrlNotifications" %>



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
                    <div class=""><asp:TextBox ID="txtSaveAs" runat="server" />
                        <asp:Literal ID="litinfo" runat="server" />
                                <p> <asp:Label ID="lblfileupload" runat="server" AssociatedControlID="fuCSV" Text="file: " />
                                   <ajaxToolkit:AjaxFileUpload ID="fuCSV" runat="server" CssClass="lbtn lbtn-light"  />
				                   <asp:Button ID="cmdCSV" class="btn btn-primary" runat="server" Text="Upload CSV" /> 
                                    <asp:FileUpload ID="fu" runat="server" />
                                    <asp:Button ID="cmdfu" runat="server" Text="File Upload" />
				                </p>
                            </div>      
        <asp:button runat="server"  type="button" class="btn btn-info btn-lg" id="cmdSendEmail" Text="Send Email" />

  <h2>Activate Modal with JavaScript</h2>
  <!-- Trigger the modal with a button -->
  <button type="button" class="btn btn-info btn-lg" id="myBtn">Open Modal</button>

  <!-- Modal -->
  <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
    
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">Modal Header</h4>
        </div>
        <div class="modal-body">
          <p>Some text in the modal.</p>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
      
    </div>
  </div>
        <asp:UpdatePanel id="upChecks" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>
                <asp:Button CssClass="btn btn-floating" ID="Button1" runat="server" Text="Button" />
                <button class="btn btn-primary" onclick="fbind();">Bind</button>
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
      
      <uc1:ctrlNotifications runat="server" id="ctrlNotifications" />
</div>

<script>
$(document).ready(function(){
  $("#myBtn").click(function(){
    $("#myModal").modal();
  });


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
            url: "dcwebservices.asmx/fchange",
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
    <uc1:ctrlIncompatibilities runat="server" id="ctrlIncompatibilities1" />
</asp:Content>