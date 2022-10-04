<%@ Page Title="" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="ManageLogins.aspx.vb" Inherits="Kolassa.DesignCentre.UI.ManageLogins" EnableEventValidation = "false" %>
<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(function () {
            $("[id*=grdUsers] td").click(function () {
                var sender = this
                populate($(sender).closest("tr"));
                if (sender.className == "password") {
                   // alert('Reset Pass');
                    window.$('#PasswordModal').modal();
                }
                else {
                    window.$('#UserModal').modal();
                  //  usermodal.modal('show');
                }
            });
        });
        function populate(tthis) {
            var sender = (tthis.attr('ID'))
            var currow = $(tthis).closest('tr');
            sID = currow.find('span:eq(0)').data("ID");
            if (sID === undefined) { sID = currow.find('label:eq(0)').data("id"); }

            {
                fResetForm()
            }

            var sID = currow.attr('data-id');
            var email = currow.attr('data-email');
            var username = currow.attr('data-username');
            var friendlyname = currow.attr('data-friendlyname');
            var nodeid = currow.attr('data-node');
            var roles = currow.attr('data-roles');
            $('#<%= txtID.ClientID %>').val(sID);
            $('#<%= txtEmail.ClientID %>').val(email);
            $('#<%= txtUserName.ClientID %>').val(username);
            $('#<%= txtFriendlyName.ClientID %>').val(friendlyname);
            $('#<%= txtNodeID.ClientID %>').val(nodeid);
           // $('#UserModal').show //modal('toggle')
            //  $('#showmodal').click
            const arrRoles = roles.split(",");
           // alert('length: ' + arrRoles.length )
            var elements = document.getElementsByClassName('chk');
            // CLEAR THE CHECKBOXES
            for (var i = 0, element; element = elements[i++];) {
                var chk = element.childNodes[0];
                chk.checked = false;
             }
            for (let r = 0; r < arrRoles.length; r++) { 
                var therole = arrRoles[r];
                if (therole == '') {
                    //alert('nothing to do');
                }
               else {
                    for (let i = 0, element; element = elements[i++];) {
                        chk = element.childNodes[0];
                        var chkid = chk.id
                        if (chkid.includes(therole)) {
                           // alert("Checkbox:" + chkid + ' ' + therole);
                            chk.checked = true;
                       
                        }
                    }
                }
            }
        }
        function fResetForm() {
            var x = document.getElementsByClassName("editmode")
            for (var c of x) {
                c.style.clear;
                if (c.style.display == "block") {
                    c.style.display = "none";
                }
                else {
                    c.style.display = "block";
                }
            }
        }
    </script>
    <h2>Manage your external logins.</h2>
    <asp:PlaceHolder runat="server" ID="SuccessMessagePlaceholder" Visible="false" ViewStateMode="Disabled">
            <p class="text-success">  %: SuccessMessage % </p>
        </asp:PlaceHolder>
    <div>
        <section id="externalLoginsForm">



            <asp:GridView ID="grdUsers" AutoGenerateColumns="false" runat="server" 
                AllowPaging="True" 
                DataKeyNames="ID"
                onrowdatabound="OnRowDataBound"
                 PageSize="50">
                <Columns>
                  <asp:TemplateField ItemStyle-CssClass="password" ><Itemtemplate>
                      <a id="passwordreset" href="#ResetPassword"><i class="fa fa-pause-circle"</a>
                                     </Itemtemplate></asp:TemplateField> 
                    <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="d-none" ItemStyle-CssClass ="d-none" />
                     <asp:BoundField DataField="UserFriendlyName" HeaderText="Friendly Name" />
                    <asp:BoundField DataField="Email" headertext ="Email"/>
                    <asp:BoundField DataField="UserName" Headertext="User Name" />
                    <asp:BoundField DataField="NodeID" HeaderText= "Node"/>
                    <asp:BoundField DataField="LockoutEndDateUtc" Headertext="Locked" DataFormatString="{0:d}" />

                </Columns>
            </asp:GridView>
        </section>
    </div>


    <div id="UserModal" class="modal" tabindex="-1" role="dialog">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Edit User Information</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
                <asp:panel ID="pnlmodal" runat="server">
             
                    <!-- uc:OpenAuthProviders run at= server ReturnUrl="~/Account/ManageLogins" / -->
  
                    <div class="form-group">
                        <label for="<%= txtID.ClientID %>">ID</label>
                        <asp:TextBox ID="txtID"  runat="server" CssClass="form-control-plaintext"></asp:TextBox>
                          
                    <div class="form-group">
                        <label for="<%= txtEmail.ClientID %>">Email address</label>
                        <asp:textbox textmode="Email" ID="txtEmail" runat="server" class="form-control" MaxLength ="60"  /> 
                        <small id="emailHelp" class="form-text text-muted">We'll never share your email with anyone else.</small>
                    </div>
                    <div class="form-group">
                        <label for="<%= txtFriendlyName.ClientID %>">Friendly Name</label>
                        <asp:textbox  ID="txtFriendlyName" runat="server" class="form-control" MaxLength ="50"  />
                        <small id="FriendlyHelp" class="form-text text-muted">The name you'd like to be referred by..</small>
                    </div>

                    <div class="form-group">
                        <label for="<%= txtUserName.ClientID %>">User Name</label>
                        <asp:textbox  ID="txtUserName" runat="server" class="form-control" MaxLength ="60"  />
                    </div>
                    <div class="form-group">
                        <label for="<%= txtNodeID.ClientID %>">Node ID</label>
                        <asp:textbox Type="number" ID="txtNodeID" runat="server" class="form-control" MaxLength ="5"  />
                    </div>
                   
                   
                </div>
         
                </asp:panel>
              <asp:Panel runat="server" ID ="pnlRoles" />
              </div>
          <div class="modal-footer">
            <asp:button runat="server" ID="cmdSubmit" type ="submit" class="btn btn-primary" text="Save" />
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
          </div>
        </div>
  </div>
</div>


    
    <div id="PasswordModal" class="modal" tabindex="-1" role="dialog">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Reset Password</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
                <asp:panel ID="Panel1" runat="server">
                    <div class="form-group">
                <label for="<%= txtPassword.ClientID %>">New Password</label>
                        <asp:textbox  ID="txtPassword" runat="server" class="form-control" MaxLength ="15"  />
                    </div>
                </asp:panel>
          </div>
          <div class="modal-footer">
            <asp:button runat="server" ID="cmdResetPassword" type ="submit" class="btn btn-primary" text="Save" />
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
          </div>
        </div>
  </div>
</div>
</asp:Content>