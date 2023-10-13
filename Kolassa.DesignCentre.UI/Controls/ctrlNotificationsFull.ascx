<%@ Control Language="vb" AutoEventWireup="false"  CodeBehind="ctrlNotificationsFull.ascx.vb"  Inherits="Kolassa.DesignCentre.UI.ctrlNotificationsFull"  %>
<style>
    body{
    margin-top:10px;
    background-color: #f0f2f5;
}
.dropdown-list-image {
    position: relative;
    height: 2.5rem;
    width: 2.5rem;
}
.dropdown-list-image img {
    height: 2.5rem;
    width: 2.5rem;
}
.btn-light {
    color: #2cdd9b;
    background-color: #e5f7f0;
    border-color: #d8f7eb;
}
</style>
<asp:UpdatePanel ID="pnlNotifications" runat="server" >
    <ContentTemplate>
        <div style="display:none">
            Quote:	<asp:Literal ID="litID" runat="server"></asp:Literal><br />
        </div>

        <div class="container">
            <div class="row">
                <div class="col-lg-12 right">

                    <asp:Repeater id="repeaterData" runat="server"  Visible="True">
                          <HeaderTemplate>
                            <div class="box-body p-0">
                          </HeaderTemplate>
             
                          <ItemTemplate>
                              <asp:literal ID="litHeading" runat="server"></asp:literal>

                             <div <%#IIf(DataBinder.Eval(Container.DataItem, "Read") = "False", "class='tbl tbl-primary'", "class='tbl tbl-secondary'") %> ></div>
                                <div class="p-1 d-flex align-items-center <%# iif(DataBinder.Eval(Container.DataItem, "Read") = "False", "bg-light", "") %>  border-bottom osahan-post-header">
                                    <div class="dropdown-list-image mr-3">
                                        <img class="rounded-circle" src="https://bootdey.com/img/Content/avatar/avatar3.png" alt="" />
                                    </div>
                                    <div class="font-weight-bold mr-3">
                                        <div class="text-truncate">Customer: <%# DataBinder.Eval(Container.DataItem, "Name") %> &nbsp;</div>
                                        <div class="small">
                                           <asp:linkbutton ID="lnkRead" runat="server" Text="readread" CommandName="LNKREAD"  CommandArgument='<%# Eval("ID") %>'></asp:linkbutton>
                                           <asp:linkbutton ID="lnkComplete" runat="server" Text="compcom" CommandName="LNKCOMPLETE"  CommandArgument='<%# Eval("ID") %>'></asp:linkbutton> 
                                            <%# iif(DataBinder.Eval(Container.DataItem, "Complete") = "False", "Mark Complete", "Complete") %> <br />
                                            <div class="d-none">" <%# DataBinder.Eval(Container.DataItem, "Project") %> <br />
                                             <%# DataBinder.Eval(Container.DataItem, "ID") %> <br />
                                             <%# DataBinder.Eval(Container.DataItem, "Email") %> <br />
                                             <%# DataBinder.Eval(Container.DataItem, "Code") %> <br /></div>
                                             <%# DataBinder.Eval(Container.DataItem, "Description") %> <br />
                                             Assigned To: <%# DataBinder.Eval(Container.DataItem, "AssignedToEmail") %>  <br />
                                             <%# DataBinder.Eval(Container.DataItem, "Comments") %>                 
                                            <div class="d-none">
                                                <%# DataBinder.Eval(Container.DataItem, "CreateUserName") %> <br /><%# DataBinder.Eval(Container.DataItem, "updateusername") %> <%# DataBinder.Eval(Container.DataItem, "Createdate") %> 
                                            </div>
                                         </div>
                                    </div>

                                    <span class="ml-auto mb-auto">
                                        <div class="btn-group">
                                            <button type="button" class="btn btn-light btn-sm rounded" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                <i class="mdi mdi-dots-vertical"></i>
                                            </button>
                                            <div class="dropdown-menu dropdown-menu-right">
                                                <button class="dropdown-item" type="button"><i class="mdi mdi-delete"></i> Delete</button>
                                                <button class="dropdown-item" type="button"><i class="mdi mdi-close"></i> Turn Off</button>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="text-right text-muted pt-1"><%# DateDiff("d", DataBinder.Eval(Container.DataItem, "CreateDate"), Now()) %>d<br />
                                            <a href="<%# DataBinder.Eval(Container.DataItem, "AppURL") %>">Go</a> <br />
                                        </div>
                                    </span>
                                </div>
                             </ItemTemplate>
                          <FooterTemplate>
                            </div>
                          </FooterTemplate>            
                       </asp:Repeater>
                    </div>
                </div>
            </div>


        <div class="d-none">
            <label for="txtComment" title="Comment" />
	        <asp:TextBox ID="txtComment" class="form-control" runat="server" TextMode="MultiLine" ></asp:TextBox><br />
        </div>
       <asp:UpdateProgress ID="uprg" runat="server">
        <ProgressTemplate>
            Loading <img src="../images/loadingH.gif" />
        </ProgressTemplate>
	</asp:UpdateProgress>
        </label>
    </ContentTemplate>
	<Triggers>
		<asp:AsyncPostBackTrigger ControlID="cmdPostComm" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="cmdBindData" EventName="Click" />
	</Triggers>
</asp:UpdatePanel>

<div>
	<asp:Button class="btn btn-primary d-none" ID="cmdPostComm"  UseSubmitBehavior="false" runat="server" Text="Post" />
</div>
<p>
	<asp:Button class="btn btn-primary btnbind d-none" ID="cmdBindData"  UseSubmitBehavior="false" runat="server" Text="Bind" />
</p>
