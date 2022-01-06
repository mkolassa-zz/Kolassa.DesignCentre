<%@ Page Language="vb" EnableEventValidation=false AutoEventWireup="false" MasterPageFile="~/Site.master" Codebehind="frmRooms.aspx.vb" Inherits="Kolassa.DesignCentre.UI.frmRooms"%>

<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>





<asp:Content ID="BodyContent" runat=server ContentPlaceHolderID="MainContent">
	<style>
		tr:not(:first-child):hover {  text-decoration: underline; color: rgb(0,0,255)}
		
		label:hover { text-decoration: underline; color: rgb(0,0,255)}

		.input-group.md-form.form-sm.form-1 input{
			border: 1px solid #bdbdbd;
			border-top-right-radius: 0.25rem;
			border-bottom-right-radius: 0.25rem;
		}
		.input-group.md-form.form-sm.form-2 input {
			border: 1px solid #bdbdbd;
			border-top-left-radius: 0.25rem;
			border-bottom-left-radius: 0.25rem;
		}
		.input-group.md-form.form-sm.form-2 input.red-border  {
			border: 1px solid #ef9a9a;
		}
		.input-group.md-form.form-sm.form-2 input.lime-border  {
			border: 1px solid #cddc39;
		}
		.input-group.md-form.form-sm.form-2 input.amber-border  {
			border: 1px solid #ffca28;
		}
</style>	


        

<div class="Jumbotron">
		<h2>Rooms</h2>
	<div class="input-group md-form form-sm form-1 pl-0 my-1">
    <div class="input-group-prepend">
        <span class="input-group-text pink lighten-3 my-1" id="basic-text1"><i class="fa fa-search text-white" aria-hidden="true"></i></span>
    </div>
    <asp:textbox id="txtSearch" runat="server" class="form-control my-1 py-1" type="text" placeholder="Search" aria-label="Search" AutoPostBack="True" />
		   <asp:label id="lblSearch" runat="server" class=" d-none form-control my-1 py-1"  />
		<asp:Button Visible="false" ID="cmdSearch" runat="server" Text="Search" ></asp:button>
</div>

	<asp:imagebutton  id="cmdExcel" runat="server" imageurl="~/images/Excel.png" Height="30px"></asp:imagebutton>
    	   <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="False" 
						   class="table table-bordered table-sm" AllowPaging="True"
							 AllowSorting="True"	    PageSize="20"
						   DataSourceID="odsData" >
        
    	       <Columns>
					<asp:BoundField DataField="UnitType" HeaderText="Unit Type" />
					<asp:BoundField DataField="BuildingPhase" HeaderText="Phase" />
					<asp:BoundField DataField="Location" HeaderText="Location" />
					<asp:BoundField DataField="UpgradeCategory" HeaderText="Upgrade Category" />
					<asp:BoundField DataField="UpgradeLevel" HeaderText="Upgrade Level" />
					<asp:BoundField DataField="Description" HeaderText="Description" />
					<asp:BoundField DataField="ModelOrStyle" HeaderText="Style" />
					<asp:BoundField DataField="CustomerPrice" HeaderText="Price" />
				   	<asp:BoundField DataField="DeveloperPrice" HeaderText="Dev $" />
				   	<asp:BoundField DataField="ToVendorPrice" HeaderText="Vendor $" />
				   	<asp:BoundField DataField="Comments" HeaderText="Comments" />
               </Columns>
        
    	   <RowStyle CssClass="clickable-row" />
       
    </asp:GridView>


    <asp:ObjectDataSource ID="odsData" runat="server" SelectMethod="LoadUpgradeOptions" TypeName="Kolassa.DesignCentre.Data.clsSelectDataLoader" >
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="llNodeID" SessionField="NodeID" Type="Int64" />
            <asp:Parameter Name="lsRoom" Type="String" />
            <asp:Parameter Name="lsPhase"      Type="String" />
        	<asp:Parameter Name="lsQuoteID"     Type="String"  />
        	<asp:ControlParameter ControlID="lblSearch" Name="lsWhere" PropertyName="Text" Type="String" />
			<asp:Parameter Name="lbActive"       Type="Boolean" DefaultValue="True" />
        	<asp:Parameter Name="lsID" Type="String" />
            <asp:Parameter Name="lsCat" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

	
    


	<!-- THIS IS THE PANEL FOR DATA -->
	<asp:Panel Runat="server" ID="pnlData"   Visible = "true">
		<!-- Modal  THIS IS THE LOOKUP MODAL FORM FOR Data -->
		<div class="modal fade" id="DataModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
		  <div class="modal-dialog" role="document">
			<div class="modal-content">
			  <div class="modal-header">
				<h5 class="modal-title" id="Data">Data</h5>
					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					  <span aria-hidden="true">&times;</span>
					</button>
				</div>
				<div class="modal-body">
					<asp:UpdatePanel ID="upData" runat="server">
						<ContentTemplate >
							
							<div style="display:none">
								ID:<asp:textbox ID="txtID" runat="server" ></asp:textbox>-ID
								<asp:Button ID="cmdUPData" runat="server" />
							</div>
							
						</ContentTemplate>
					 <Triggers>
					   <asp:AsyncPostBackTrigger ControlID="cmdUPData" EventName="Click" />
				  </Triggers>
					</asp:UpdatePanel>

				</div>
			  <div class="modal-footer">

			  </div>
	
			</div>
		  </div>
		</div>
	</asp:Panel>
	<!-- END Payment PANEL -->
	




    </div>

	

  
		  <script>
		  $(document).ready(function () {
			 // alert('d');
			  $('#MainContent_grdData').on('click', '.clickable-row', function (event) {
				 // alert('FunctionRunning');
				  fSetPhase(this);

			  });


			  function fSetPhase(tthis) {
				  $(tthis).addClass('table-active').siblings().removeClass('table-active');
				  $(tthis).attr('data-toggle', 'modal');
				  $(tthis).attr('data-target', '#DataModal');
				  			  
				  var currow = $(tthis).closest('tr');
				  //var sID = currow.find('td:eq(0)').text();
				  var sID = currow.find('label:eq(0)').data("id");
				  $('#<%= txtID.ClientID %>').val(sID);
				  document.getElementById('<%= cmdUPData.ClientID %>').click()
				  } 
		  });
  	</script>
	
</asp:Content>
