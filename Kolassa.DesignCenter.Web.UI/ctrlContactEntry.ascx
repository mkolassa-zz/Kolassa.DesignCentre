<%@ Control Language="vb" AutoEventWireup="false"  Inherits="Kolassa.DesignCenter.Web.UI.ctrlContactEntry" Codebehind="ctrlContactEntry.ascx.vb" %>
<asp:ObjectDataSource ID="odsContact" runat="server" 
        SelectMethod="GetContacts" 
        UpdateMethod="UpdateContacts" 
        TypeName="clsContacts" 
        OldValuesParameterFormatString="original_{0}" 
        DataObjectTypeName="clsContact" 
        DeleteMethod="DeleteContacts" 
        InsertMethod="InsertContacts">
    <SelectParameters>
        <asp:Parameter Name="ID" Type="String" />
    <asp:Parameter Name="lsObjectID" Type="String" />
        <asp:Parameter Name="NodeID" Type="Int32" />
        <asp:Parameter Name="Active" Type="Boolean" />
</SelectParameters>
    
</asp:ObjectDataSource>

    <script>
        $(document).ready(function () {
            $("#btn_open_modal").click(function (event) {
                $(".modal-dialog").css({ position: "absolute", top: event.pageY });
                $('#sample_modal').modal('show'); 
            })
            });

    </script>

<asp:GridView ID="gvContacts" runat="server" DataKeyNames="ID" DataSourceID="odsContact"></asp:GridView>
    <div class="row">
        <div class="col-lg-12"></div>
        <asp:UpdatePanel ID="UpdatePanel3"  runat="server">
            <ContentTemplate>
               
      <asp:Label ID="lblmsg" runat="server"  ></asp:Label> 
                </ContentTemplate> 
            </asp:UpdatePanel>
        </div>
  
<div class="form-group ">
  
    <label class="col-sm-2 control-label">Category</label>
 <div class="col-sm-7">
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
    <asp:DropDownList ID="ddlCat" CssClass="form-control " runat="server" >
  

    </asp:DropDownList>
          </ContentTemplate>

           </asp:UpdatePanel>

   </div> 
  <div class="col-sm-3">
                               
                                  <button type="button" id="btn_open_modal" class="btn btn-primary btn-lg ">Add New Category</button>
                       
                            </div>
</div>

        <div class="form-group ">
 <label class="col-sm-2 control-label">Name</label>
 <div class="col-sm-10">
     <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>

   </div> 

</div>

        <div class="form-group  ">
 <label class="col-sm-2 control-label">Roll</label>
 <div class="col-sm-10">
     <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server"></asp:TextBox>

   </div> 
 
</div>
        <div class="form-group ">
             <div class="col-sm-2 ">Mandetory fields are *</div>
 <div class="col-sm-7">
     <asp:Button ID="btn_Submit" runat="server" CssClass="btn btn-lg btn-primary" Text="Submit" />

     </div>

        </div>
       


   <div class="modal   " id="sample_modal" role="dialog" tabindex="-1" >
         <div class="modal-dialog animated zoomIn ">
      <asp:UpdatePanel ID="UpdatePanel2" runat="server" EnableViewState="true">
                <ContentTemplate>         
    <div class=" modal-content">
        <div class="modal-header">Modal Heading</div>
        <div class="modal-body col-sm-12">
            <div class="form-group ">
                            <label class="col-sm-4 control-label">New Category</label>
                            <div class="col-sm-8">

                                <asp:TextBox ID="txtCat"  CssClass="form-control"  runat="server" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv1" CssClass="alert-danger" ControlToValidate="txtCat" ValidationGroup="save-modal" SetFocusOnError="true" Display="Dynamic" runat="server" ErrorMessage="Please Enter category!!!"></asp:RequiredFieldValidator>
                            </div>
                        </div>
            </div>
        <div class="modal-footer">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <i>LOADING ....</i>
                </ProgressTemplate>

            </asp:UpdateProgress>

                          <asp:Button ID="btn_save"  runat="server" CssClass="btn btn-primary" Text="Add" ValidationGroup="save-modal"   />
                  
                    
            
      
                      <button type="button" class="btn btn-default pull-right" data-dismiss="modal">Close</button>
      
      </div>
        </div>
                    </ContentTemplate>
          </asp:UpdatePanel>
      </div>
       </div>