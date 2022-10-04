<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlContactEntry.ascx.vb" Inherits="Kolassa.DesignCentre.UI.ctrlContactEntry" %>
   <asp:Label ID="lblObjectID" runat="server" />
    <asp:UpdatePanel ID="upContactGrid" runat="server">
        <ContentTemplate>
         <div class="container-fluid bcontent">
            <asp:linkbutton  ID="lnkNewContact" runat="server"    tooltip="Add Contact" CssClass="newrec" 
                data-toggle="modal" data-target="#contact_modal" OnClientClick="fnewcontact(this)">
                        <i class='fa fa-plus-circle fa-1x' ></i>

            </asp:linkbutton>
           <div class="card-columns">
             <asp:Repeater ID="rptContact" runat="server" >
                <ItemTemplate>
                    <div class="col-sm-4">
                    <div class="card" style="height: 18rem; width: 18rem;">
                        <div class="card-body">
                            <span id="starspan">
                                <%#  IIf(Trim(Eval("Type")) = "Primary", "<i class='fa fa-star float-right float-top'> </i>", "") %>
                            </span>
                        <h5 class="card-title"><asp:Label ID="lblName" runat="server" Text='<%#Eval("FirstName") & " " & Eval("LastName") & " "   %>' Font-Bold="true"/>  </h5>

                            <a ID="lnkEditContact"  onclick="fSetContactFormValues(this)" href="#" data-toggle="modal" data-target="#contact_modal"
                                data-id="<%#Eval("ID") %>"  
                                data-parentid="<%#Eval("parentid") %>"  
                                data-firstname="<%#Eval("FirstName") %>" 
                                data-lastname="<%#Eval("LastName") %>" 
                                data-fulladdress="<%#Eval("FULLADDRESS") %>" 
                                data-city="<%#Eval("city") %>" 
                                data-stateprovince="<%#Eval("Stateprovince") %>" 
                                data-postalcode="<%#Eval("PostalCode") %>" 
                                data-country="<%#Eval("Country") %>" 
                                data-phone1="<%#Eval("Phone1") %>" 
                                data-phone2="<%#Eval("Phone2") %>"  
                                data-type="<%#Eval("Type") %>"  
                                data-email1="<%#Eval("Email1") %>" 
                                data-email2="<%#Eval("Email2") %>" ><i class='fa fa-edit float-right float-top'></i>
                               </a>
                            
                        <p class="card-text"><asp:literal ID="litAddress" runat="server" Text='<%#Eval("FullAddress") %>' /> 
                            <br /><%#Eval("City") & ", " & Eval("StateProvince") & " " & Eval("PostalCode") %>
                            <br /><%#Eval("Country") %>
                            <br /><%#Eval("Phone1") & ", " & Eval("Phone2") & " " & Eval("Email1") & " " & Eval("Email2") %>
                        </p>
                        </div>
                    </div>
                    </div>
                </ItemTemplate>
              </asp:Repeater>
           </div>
         </div>
            <div class="row">
                <asp:UpdatePanel ID="UpdatePanel3"  runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblmsg" runat="server"  ></asp:Label> 
                    </ContentTemplate> 
                </asp:UpdatePanel>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_save" EventName="Click" />
           <asp:AsyncPostBackTrigger ControlID="lnkDeleteContact" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>




       


    <div class="modal" id="contact_modal"  role="dialog" tabindex="-1" >
        <div class="modal-dialog animated zoomIn ">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" EnableViewState="true">
                <ContentTemplate>     
                   
                    <div class=" modal-content">
                        <div class="modal-header">
                              <button type="button" class="btn btn-default" data-toggle="collapse" data-target="#contactid">Contact Information</button>
                                  <div id="contactid" class="collapse">
                                     <div class="form-row">
                                            <asp:TextBox ID="txtContactID"  CssClass="form-control form-control-sm form-control-plaintext"  runat="server" ></asp:TextBox>
                                            <asp:TextBox ID="txtContactParentID"  CssClass="form-control  form-control-sm form-control-plaintext"  runat="server" ></asp:TextBox>
                                    </div>
                                  </div>
                        </div>
                            <div class="modal-body col-sm-12">
                                <div class="form-group ">
                                    <div class="col-sm-12">
                                        <asp:LinkButton ID="lnkDeleteContact"  data-id='<%#Eval("ID") %>'  runat="server"><i class='fa fa-trash float-right'></i></asp:LinkButton>
                                    <div class="form-row">
                                       <div class="form-group col-sm-5">
                                          <label for="txtContactFirstName" class="col-form-label-sm">First</label>
                                            <asp:TextBox ID="txtContactFirstName" MaxLength="50" placeholder="First name" CssClass="form-control"  runat="server" ></asp:TextBox>
                                       </div>
                                       <div class="form-group col-sm-5">
                                          <label for="txtContactLast Name" class="col-form-label-sm">last</label>
                                           <asp:TextBox ID="txtContactLastName" MaxLength="50"  placeholder="Last name" CssClass="form-control"  runat="server" ></asp:TextBox>
                                        </div>
                                        <div class="form-group col-sm-2">
                                          <label for="chkPrimary" class="col-form-label-sm">Primary</label>
                                           <asp:checkbox ID="chkPrimary" MaxLength="50"  placeholder="Primary" CssClass="form-control"  runat="server" ></asp:checkbox>
                                        </div>
                                      </div>
                                 
                                      <div class="form-group">
                                        <label for="txtContactAddress" class="col-form-label-sm">Address</label>
                                        <asp:TextBox ID="txtContactAddress" MaxLength="255"  TextMode="MultiLine" Rows="3" placeholder="1234 Main St" CssClass="form-control"  runat="server" ></asp:TextBox>
                                      </div>
        
                                      <div class="form-row">
                                        <div class="form-group col-md-5">
                                          <label for="txtContactCity" class="col-form-label-sm">City</label>
                                          <asp:TextBox ID="txtContactCity"  MaxLength="50"  CssClass="form-control form-control-sm"  runat="server" ></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-2">
                                          <label for="txtContactState" class="col-form-label-sm">State</label>
                                          <asp:TextBox ID="txtContactState" MaxLength="10"   CssClass="form-control form-control-sm"  runat="server" ></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3">
                                          <label for="txtContactPostalCode" class="col-form-label-sm">PST</label>
                                          <asp:TextBox ID="txtContactPostalCode"  MaxLength="10"  CssClass="form-control form-control-sm"  runat="server" ></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-2">
                                          <label for="txtContactCountry" class="col-form-label-sm">CTRY</label>
                                          <asp:TextBox ID="txtContactCountry"  MaxLength="10"  CssClass="form-control form-control-sm"  runat="server" ></asp:TextBox>
                                        </div>
                                                 
                                      </div>
                                      <div class="form-row">
                                        <div class="form-group col-md-6">
                                          <label for="txtContactPhone1" class="col-form-label-sm">Phone 1</label>
                                          <asp:TextBox ID="txtContactPhone1"  MaxLength="20"  CssClass="form-control"  runat="server" ></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                          <label for="txtContactPhone2" class="col-form-label-sm">Phone 2</label>
                                           <asp:TextBox ID="txtContactPhone2"  MaxLength="20"  CssClass="form-control"  runat="server" ></asp:TextBox>
                                        </div>
                                      </div>                          
                                      <div class="form-row">
                                        <div class="form-group col-md-12">
                                          <label for="txtContactEmail1" class="col-form-label-sm">Email 1</label>
                                          <asp:TextBox ID="txtContactEmail1"  MaxLength="50"  CssClass="form-control"  runat="server" ></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-12">
                                          <label for="txtContactEmail2" class="col-form-label-sm">Email 2</label>
                                           <asp:TextBox ID="txtContactEmail2"  MaxLength="50"  CssClass="form-control"  runat="server" ></asp:TextBox>
                                        </div>
                                      </div>                                                 
                                        <asp:RequiredFieldValidator ID="rfv1" CssClass="alert-danger" ControlToValidate="txtContactFirstName" ValidationGroup="save-modal" SetFocusOnError="true" Display="Dynamic" runat="server" ErrorMessage="Please Enter Name!!!"></asp:RequiredFieldValidator>
                                        <div class="contactmessage">
                                            <!-- Message Goes Here -->
                                        </div> 
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                    <ProgressTemplate>
                                        <i>LOADING ....</i>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <asp:Button ID="btn_save"  runat="server" CssClass="btn btn-primary" Text="Add" ValidationGroup="save-modal" /> <!--OnClientClick="alert('f3newcontact(this)');"  / -->
                                <button type="button" class="btn btn-default pull-right" onclick="$('#contact_modal').modal('hide');">Close</button>


                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
   </div>
<script>
    function fnewcontact(tthis) {
        var contact = $(tthis);
        $('#<%= txtContactID.ClientID %>').val('');
       var sID = contact.data('parentid');
        $('#<%= txtContactParentid.ClientID %>').val(sID);
        $('#<%= txtContactFirstName.ClientID %>').val('');
        $('#<%= txtContactLastName.ClientID %>').val('');
        $('#<%= txtContactAddress.ClientID %>').val('');
        $('#<%= txtContactcity.ClientID %>').val('');
        $('#<%= txtContactState.ClientID %>').val('');
        $('#<%= txtContactPostalCode.ClientID %>').val('');
        $('#<%= txtContactCountry.ClientID %>').val('');
        $('#<%= txtContactemail1.ClientID %>').val('');
        $('#<%= txtContactemail2.ClientID %>').val('');
        $('#<%= txtContactPhone1.ClientID %>').val('');
        $('#<%= txtContactPhone2.ClientID %>').val('');
        $('#<%= chkPrimary.ClientID %>').checked = false;
    }
    function fSetContactFormValues(tthis) {
        var contact = $(tthis);
        var sID = contact.data("id");

        if (sID === undefined) { sID = contact.data("parentid"); }
        $('#<%= txtContactID.ClientID %>').val(sID);
        sID = contact.data("parentid");
        if (sID === undefined) { sID = contact.data("parentid"); }
        $('#<%= txtContactParentid.ClientID %>').val(sID);
        sID = contact.data("firstname");
        $('#<%= txtContactFirstName.ClientID %>').val(sID);
        sID = contact.data("lastname");
        $('#<%= txtContactLastName.ClientID %>').val(sID);

        sID = contact.data("fulladdress");
        $('#<%= txtContactAddress.ClientID %>').val(sID);
        sID = contact.data("city");
        $('#<%= txtContactcity.ClientID %>').val(sID);
        sID = contact.data("stateprovince");
        $('#<%= txtContactState.ClientID %>').val(sID);
        sID = contact.data("postalcode");
        $('#<%= txtContactPostalCode.ClientID %>').val(sID);
        sID = contact.data("country");
        $('#<%= txtContactCountry.ClientID %>').val(sID);
        sID = contact.data("email1");
        $('#<%= txtContactemail1.ClientID %>').val(sID);
        sID = contact.data("email2");
        $('#<%= txtContactemail2.ClientID %>').val(sID);
        sID = contact.data("phone1");
        $('#<%= txtContactPhone1.ClientID %>').val(sID);
        sID = contact.data("phone2");
        $('#<%= txtContactPhone2.ClientID %>').val(sID);
        sID = contact.data("type");
        sID = sID.trim();
        document.getElementById("<%= chkPrimary.ClientID %>").checked = false;
        if (sID == 'Primary') { 
            document.getElementById("<%= chkPrimary.ClientID %>").checked = true;
    }
        $('#<%= txtContactID.ClientID %>').attr("data-Action", "update");
         //    document.getElementById('<%= btn_save.ClientID %>').click()
    //    $(tthis).addClass('table-active').siblings().removeClass('table-active');
   //     $(tthis).attr('data-toggle', 'modal');
   //     $(tthis).attr('data-target', '#contact_modal');
      
    }

    function ShowContactMessage(message, messagetype) {
        var cssclass;
        switch (messagetype) {
            case 'Success':
                cssclass = 'alert-success'
                break;
            case 'Error':
                cssclass = 'alert-danger'
                break;
            case 'Warning':
                cssclass = 'alert-warning'
                break;
            default:
                cssclass = 'alert-info'
        }

        var divname;
       // divname = document.getElementsByClassName('contactmessage')
        $(".contactmessage").append('<div id="divcontactmessage"  class="alert alert-dismissible ' + cssclass + '"><a href="#" class="close" data-dismiss="alert"   aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');
        $('#divcontactmessage').show();
        var divvar = 'Alert'
        $('#My' + divvar).show();
    }
</script>
<script>
 /*   function bindContactEvents() {
       // alert('f');
    }
    function fbindContactEvents() {
        $('< %= gvContacts.ClientID %>').on('click', '.clickable-row', function (event) {
            var target = $(event.nodeName);
            var dhref = event.target.nodeName;

            if (dhref != "A") {
                fSetContactFormValues(this);
            } else {
                event.stopPropagation
                event.preventDefault
                var currow = $(this).closest('tr');
                var sID = currow.find('span:eq(0)').data("id");
                if (sID === undefined) { sID = currow.find('label:eq(0)').data("id"); }
                $('#< %= txtcontactid.ClientID %>').val(sID);
                    $('#< %= txtcontactid.ClientID %>').attr("data-Action", "delete");
                   // document.getElementById('< %= cmdDelcontact.ClientID %>').click()
                    
                }
            }
            );


            $(".del4").click(function (e) {
                e.preventDefault();
                e.stopPropagation();
                // alert("Button Clicked");
                var z = confirm("Are you sure you would like to Delete this record?");
                if (z == true) {
                    var currow = $(this).closest('tr');
                    var sID = currow.find('span:eq(0)').data("id");
                    if (sID === undefined) { sID = currow.find('label:eq(0)').data("id"); }
                    $('#< %= txtcontactid.ClientID %>').val(sID);
                    $('#< %= txtcontactid.ClientID %>').attr("data-Action", "delete");
                //    document.getElementById('< %= cmdDelcontact.ClientID %>').click()
                }
            }); 

    }

    $(document).ready(
        function () {
            bindContactEvents(); //Binds fsetphase to the Phase Grid              
            $('#< %= gvContacts.ClientID %>').on('click', '.clickable-row', function (event) {
                fSetContactFormValues(this);
            }
            );
        }
    ); */
</script>