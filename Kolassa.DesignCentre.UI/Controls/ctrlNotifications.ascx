<%@ Control Language="vb" AutoEventWireup="false"  CodeBehind="ctrlNotifications.ascx.vb"  Inherits="Kolassa.DesignCentre.UI.ctrlNotifications"  %>

<asp:ObjectDataSource ID="odsCommunications" runat="server" SelectMethod="LoadCommunications" 
	TypeName="Kolassa.DesignCentre.Data.clsSelectDataLoader" DeleteMethod="DeleteCommunications" 
	InsertMethod="InsertCommunications" OldValuesParameterFormatString="original_{0}" >
    <DeleteParameters>
        <asp:Parameter Name="RecordID" Type="String" />
    </DeleteParameters>
    <InsertParameters>
        <asp:SessionParameter Name="llNodeID" SessionField="NodeID" Type="Int64" />
		<asp:ControlParameter Name="lsComments" ControlID="txtComment" Type="String" />
		<asp:Parameter DefaultValue="12121212-1212-1212-1212-121212121212" Name="lsObjectID" Type="String" />
        <asp:Parameter Name="lsCategory" Type="String" DefaultValue="Quote"/>
    </InsertParameters>
    <SelectParameters>
        <asp:SessionParameter Name="llNodeID" SessionField="NodeID" Type="Int64" />
		<asp:Parameter Name="lsWhere" Type="String" />
        <asp:Parameter DefaultValue="12121212-1212-1212-1212-121212121212" Name="lsObjectID" Type="String" />
    	<asp:Parameter Name="lsID" Type="String" />
		<asp:Parameter DefaultValue="True" Name="lbActive" Type="Boolean" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:UpdatePanel ID="pnlComm" runat="server"><ContentTemplate>
<div style="display:none">
Quote:	<asp:Literal ID="litID" runat="server"></asp:Literal><br />

</div>
	<div Class="table table-striped table-bordered table-hover glyphicon-hover  padding: 10px 10px 10x 10px;">
            <asp:repeater ID="rptDiscussion" runat="server" DataSourceID="odsCommunications"  >
				<ItemTemplate>
					<blockquote class="blockquote text-right">
  <p class="mb-0 text-success"><%#"User: " + Trim(IIf(IsDBNull(Eval("cuname")), "Unknown User", Eval("cuname"))) + "  " %><i class="fa fa-user" aria-hidden="true"></i></p>
  <footer class="blockquote-footer"><cite title="Source Title"><small><%#"" + Trim(Eval("createdate")) + "  " %></small></cite></footer>
</blockquote>
					
                 <div class="font-italic">  <%#"" + Trim(Eval("Comments")) %> </div>
				</ItemTemplate>
            </asp:repeater>
	</div>
<div>
	<asp:TextBox ID="txtComment" class="form-control" runat="server" TextMode="MultiLine" ></asp:TextBox><br />

</div>


</ContentTemplate>
	<Triggers>
		<asp:AsyncPostBackTrigger ControlID="cmdPostComm" EventName="Click" />
	</Triggers>
</asp:UpdatePanel>
	
<div>
	<asp:Button class="btn btn-primary" ID="cmdPostComm"  UseSubmitBehavior="false" runat="server" Text="Post" />
</div>