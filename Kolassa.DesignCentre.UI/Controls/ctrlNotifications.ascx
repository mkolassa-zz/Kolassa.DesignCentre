<%@ Control Language="vb" AutoEventWireup="false"  CodeBehind="ctrlNotifications.ascx.vb"  Inherits="Kolassa.DesignCentre.UI.ctrlNotifications"  %>

<asp:UpdatePanel ID="pnlComm" runat="server"><ContentTemplate>
<div style="display:none">
Quote:	<asp:Literal ID="litID" runat="server"></asp:Literal><br />

</div>
	<div Class="table table-striped table-bordered table-hover glyphicon-hover  padding: 10px 10px 10x 10px;">
		<asp:gridview ID="gvData" runat="server"  >
	 
        </asp:GridView>
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