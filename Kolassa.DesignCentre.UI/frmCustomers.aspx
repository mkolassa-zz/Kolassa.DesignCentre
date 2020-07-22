<%@ Page Language="vb" EnableEventValidation=false AutoEventWireup="false" MasterPageFile="~/Site.master" Codebehind="frmCustomers.aspx.vb" Inherits="Kolassa.DesignCentre.UI.frmCustomers"%>

<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register src="Controls/ctrlContacts.ascx" tagname="ctrlContacts" tagprefix="uc2" %>
<%@ Register Src="~/Controls/ctrlCustomers.ascx" TagPrefix="uc2" TagName="ctrlCustomers" %>




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
	<h2>Customers</h2>
	<div class="input-group md-form form-sm form-1 pl-0 my-1">
    <div class="input-group-prepend">
        <span class="input-group-text pink lighten-3 my-1" id="basic-text1"><i class="fa fa-search text-white" aria-hidden="true"></i></span>
    </div>
    <asp:textbox id="txtSearch" runat="server" class="form-control my-1 py-1" type="text" placeholder="Search" aria-label="Search" AutoPostBack="True" />
	<asp:label id="lblSearch" runat="server" class=" d-none form-control my-1 py-1"  />
	<asp:Button Visible="false" ID="cmdSearch" runat="server" Text="Search" ></asp:button>
</div>

<asp:imagebutton  id="cmdExcel" runat="server" imageurl="~/images/Excel.png" Height="30px"></asp:imagebutton>
<asp:GridView ID="grdCustomers" runat="server" AutoGenerateColumns="False" 
					 class="table table-bordered table-sm" AllowPaging="True"
					 AllowSorting="True"	    PageSize="20"
					 DataSourceID="odsData"     DataKeyNames="ID" >
	<Columns>
		<asp:TemplateField HeaderText="Name">
			<ItemTemplate>
				<label id="lbl" data-id="<%# Eval("ID") %>"><%# Eval("CustomerName") %></label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:BoundField DataField="CustomerAddress" HeaderText="Address" />
		<asp:BoundField DataField="CustomerCity" HeaderText="City" />
		<asp:BoundField DataField="StateProvince" HeaderText="State" />
		<asp:BoundField DataField="Postal_code" HeaderText="Zip" />
		<asp:BoundField DataField="CustomerPhone" HeaderText="Phone" />
		<asp:BoundField DataField="CustomerEmail" HeaderText="Email" />
    </Columns>   
    <RowStyle CssClass="clickable-row" />
</asp:GridView>


    <asp:ObjectDataSource ID="odsData" runat="server" SelectMethod="GetCustomers" TypeName="Kolassa.DesignCentre.UI.clsCustomers" >
        <SelectParameters>
            <asp:Parameter Name="SortExpression" Type="String" />
            <asp:Parameter Name="SortOrder"      Type="String" />
        	<asp:Parameter Name="lsObjectID"     Type="String"  DefaultValue=""  />
			<asp:Parameter Name="lbActive"       Type="Boolean" DefaultValue="True" />
        	<asp:ControlParameter ControlID="lblSearch" Name="lsWhere" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

	
    


	<!-- THIS IS THE PANEL FOR Customers -->
	<asp:Panel Runat="server" ID="pnlCustomers"   Visible = "true">
		<!-- Modal  THIS IS THE LOOKUP MODAL FORM FOR Customers -->
		<div class="modal fade" id="CustomersModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
		  <div class="modal-dialog" role="document">
			<div class="modal-content">
			  <div class="modal-header">
				<h5 class="modal-title" id="Customer">Customer</h5>
					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
					  <span aria-hidden="true">&times;</span>
					</button>
				</div>
				<div class="modal-body">
					<asp:UpdateProgress runat="server" AssociatedUpdatePanelID="upCustomer" id="ajaxoverlay" >
						<ProgressTemplate>
							<div><strong>  Loading...</strong> <img src="images/loadingH.gif" style="height:20px;" /></div>
						</ProgressTemplate>
					</asp:UpdateProgress>
					<asp:UpdatePanel ID="upCustomer" runat="server">
						<ContentTemplate >
							<div style="display:none">
								ID:<asp:textbox ID="txtID" runat="server" ></asp:textbox>-ID
								<asp:Button ID="cmdUPCustomer" runat="server" />
							</div>
							<uc2:ctrlCustomers runat="server" ID="ctrlCustomers1" />
						</ContentTemplate>
					 <Triggers>
					   <asp:AsyncPostBackTrigger ControlID="cmdUPCustomer" EventName="Click" />
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
			  $('#MainContent_grdCustomers').on('click', '.clickable-row', function (event) {
				 // alert('FunctionRunning');
				  fSetPhase(this);

			  });
			 	 // alert('OK');
		//		 
		//});

	//		  //On UpdatePanel Refresh
	//		  var prm = Sys.WebForms.PageRequestManager.getInstance();
	//		  if (prm != null) {
	//			  prm.add_endRequest(function (sender, e) {
	//				  if (sender._postBackSettings.panelsToUpdate != null) {
	//					  fSetPhase(this);
	//				  }
	//			  });
	//		  };

			  function fSetPhase(tthis) {
				  $(tthis).addClass('table-active').siblings().removeClass('table-active');
				  $(tthis).attr('data-toggle', 'modal');
				  $(tthis).attr('data-target', '#CustomersModal');

				  
				  var currow = $(tthis).closest('tr');
				  //var sID = currow.find('td:eq(0)').text();
				  var sID = currow.find('label:eq(0)').data("id");
				  $('#<%= txtID.ClientID %>').val(sID);
				  document.getElementById('<%= cmdUPCustomer.ClientID %>').click()
				//  var sStatus = currow.find('td:eq(1)').text();
				//  var sTarget = currow.find('td:eq(2)').text();
				//  var sCompleted = currow.find('td:eq(3)').text();
				//  var sPhaseID = currow.find('label:eq(0)').data("id");
				  // var sPhaseID = currow.cells[1].childNodes[0].data("id");
				 // $("#MainContent_txtPhaseName").val(sPhase);
	

			  } 
		  });
  	</script>
		<script>
            $('#CustomersModal').on('show.bs.modal', function (e) {
                // do something...
                // alert('mike');
                document.getElementById("ctl01").reset();
            })
</script>
</asp:Content>
