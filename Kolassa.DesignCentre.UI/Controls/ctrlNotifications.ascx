<%@ Control Language="vb" AutoEventWireup="false"  CodeBehind="ctrlNotifications.ascx.vb"  Inherits="Kolassa.DesignCentre.UI.ctrlNotifications"  %>

<asp:UpdatePanel ID="pnlComm" runat="server">
    <ContentTemplate>
        <div style="display:none">
            Quote:	<asp:Literal ID="litID" runat="server"></asp:Literal><br />
        </div>
	    <div Class="d-none table table-striped table-bordered table-hover glyphicon-hover  padding: 10px 10px 10x 10px;">
		    <asp:gridview ID="gvData" runat="server"  AutoGenerateColumns="false" OnRowCreated="gvData_RowCreated"> 
                <Columns>
                    <asp:BoundField DataField="AssignedTo" HeaderText="ID" />
                    <asp:BoundField DataField="Project" HeaderText="Project" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Read" HeaderText="Read" />
                    <asp:BoundField DataField="Complete" HeaderText="Complete" />
                    <asp:BoundField DataField="Comments" HeaderText="Comments" />
                    <asp:BoundField DataField="Createdate" HeaderText="Created" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Code" HeaderText="Code" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                </Columns>	 
            </asp:GridView>
	    </div>
        <asp:Repeater id="repeaterData" runat="server">
              <HeaderTemplate>
                 <table border="1">
                  
              </HeaderTemplate>
             
              <ItemTemplate>
                 <tr  <%#IIf(DataBinder.Eval(Container.DataItem, "Read") = "False", "class='tbl tbl-primary'", "class='tbl tbl-secondary'") %>>
                    <td > Customer: <%# DataBinder.Eval(Container.DataItem, "Name") %> &nbsp;<b>
                         <%# iif(DataBinder.Eval(Container.DataItem, "Read") = "False", "Unread", "Read") %>
                         <%# iif(DataBinder.Eval(Container.DataItem, "Complete") = "False", "Active", "Complete") %></b> <br />
                        <div class="d-none">" <%# DataBinder.Eval(Container.DataItem, "Project") %> <br />
                         <%# DataBinder.Eval(Container.DataItem, "Email") %> <br />
                         <%# DataBinder.Eval(Container.DataItem, "Code") %> <br /></div>
                         <%# DataBinder.Eval(Container.DataItem, "Description") %> <br />
                         <%# DataBinder.Eval(Container.DataItem, "CreateDate") %>Assigned To: <%# DataBinder.Eval(Container.DataItem, "AssignedToEmail") %>  <br />
                         <%# DataBinder.Eval(Container.DataItem, "Comments") %> <br />
                    </td>
                    <td> 
                       <a href="<%# DataBinder.Eval(Container.DataItem, "AppURL") %>">Go</a> <br />
                  <div class="d-none">      <%# DataBinder.Eval(Container.DataItem, "CreateUserName") %> <br />
                        <%# DataBinder.Eval(Container.DataItem, "updateusername") %> </div>
                    </td>
                 </tr>
              </ItemTemplate>
              <FooterTemplate>
                 </table>
              </FooterTemplate>            
           </asp:Repeater>
        <div class="d-none">
            <label for="txtComment" title="Comment" />
	        <asp:TextBox ID="txtComment" class="form-control" runat="server" TextMode="MultiLine" ></asp:TextBox><br />
        </div>
       <asp:UpdateProgress ID="uprg" runat="server">
        <ProgressTemplate>
            Loading <img src="../images/loadingH.gif" />
        </ProgressTemplate>
	</asp:UpdateProgress>
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
