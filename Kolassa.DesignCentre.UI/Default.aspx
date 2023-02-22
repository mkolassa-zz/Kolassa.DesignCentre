<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="Kolassa.DesignCentre.UI._Default" %>

<%@ Register Src="~/Controls/ctrlImages.ascx" TagPrefix="uc1" TagName="ctrlImages" %>
<%@ Register Src="~/Controls/ctrlImageNew.ascx" TagPrefix="uc1" TagName="ctrlImageNew" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<style>	kcard-body > img {
  max-height: 100%;
  max-width: 100%;
}
</style>
	
		<asp:LoginView ID="LoginView1" runat="server">
			<AnonymousTemplate>
				<div class="jumbotron">
					<table>
						<tr>
							<td>
           
							&nbsp;
							<video controls autoplay loop width="500">
								<source src="images/dc.webm" type="video/webm">
								Your browser does not support the video tag.
							</video>
								</td>
								<td>
							<img alt="Design Centre"  title="Construction Applications for the Rest of Us!!" 
								src="images/NewLogo.png" style="width: 279px; height: 46px" />
									<br />Selection Application
							</td>
						</tr>
					 </table>


					<p class="lead">Design Centre enables developers and contractors a Cloud friendly way of providing customer facing project and upgrade selections.</p>
					<p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
				</div>
				<div class="row">
					<div class="col-md-4">
						<h2>Getting Started
                        </h2>
						<p>
							Design Centre enables you to build out options, units, plans and other data intensive structures quickly to get right down to business selling products and services.
						</p>
				
					</div>
					<div class="col-md-4">
						<h2>Drive Revenue</h2>
						<p>
							Enabling the visual selection of upgrade options, you are now selling customers all the things they need!</p>
						<p>
							<a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
						</p>
					</div>
					<div class="col-md-4">
						<h2>In The Cloud</h2>
						<p>
							Design Centre is a cloud application with no headaches or expensive equipment to maintain.</p>
						<p>
							<a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
						</p>
					</div>
				</div>
            </AnonymousTemplate>
 
			<LoggedInTemplate>
                 <style>
						#test {
							width:100%;
							height:100%;
						}
                  table {   margin: 0 auto; /* or margin: 0 auto 0 auto */
						}

                </style>
                <asp:ObjectDataSource ID="odsUnits" runat="server" 
					SelectMethod="LoadUnits" TypeName="Kolassa.DesignCentre.Data.clsSelectDataLoader" 
					DeleteMethod="DeleteUnits" InsertMethod="InsertUnits" UpdateMethod="UpdateUnits"   >
      
      
            <UpdateParameters>
                <asp:Parameter Name="llNodeID" Type="Int64" />
                <asp:Parameter Name="lsUnitName" Type="String" />
                <asp:Parameter Name="lsFloorID" Type="String" />
                <asp:Parameter Name="lsUnitTypeID" Type="String" />
                <asp:Parameter Name="lsAvailable" Type="String" />
            
                <asp:Parameter Name="UnitID" Type="String" />
                <asp:Parameter Name="lsTier" Type="String" />
                <asp:Parameter Name="lsDepositType" Type="String" />
            
                <asp:Parameter Name="lsActive" Type="String" />
            	<asp:Parameter Name="lsID" Type="String" />
            </UpdateParameters>
    
            <SelectParameters>
                <asp:Parameter Name="llNodeID" Type=Int64 />
                <asp:Parameter Name="lsWhere" Type="String" />
                <asp:Parameter Name="lbActive" Type="Boolean" />
                <asp:Parameter Name="llID" Type="Int64" />
            	<asp:Parameter Name="lsID" Type="String" />
            </SelectParameters>
      
            <DeleteParameters>
                <asp:Parameter Name="lsID" Type="String" />
            </DeleteParameters>
        
            <InsertParameters>
                <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID" 
                    Type="Int64" />
                <asp:Parameter Name="lsUnitID" Type="String" />
                <asp:Parameter Name="lsUnitName" Type="String" />
                <asp:Parameter Name="lsFloorID" Type="String" />
                <asp:Parameter Name="lsUnitTypeID" Type="String" />
                <asp:Parameter Name="lsAvailable" Type="String" />
                <asp:Parameter Name="lsTier" Type="String" />
                <asp:Parameter Name="lsDepositType" Type="String" />
            </InsertParameters>
    </asp:ObjectDataSource>
<!-- 
				<div class="dropdown">
  <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
    Dropdown button
  </button>
  <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
    <a class="dropdown-item" href="#">Action</a>
    <a class="dropdown-item" href="#">Another action</a>
    <a class="dropdown-item" href="#">Something else here</a>
  </div>
</div>
-->

					<div id="test">
						<br />
						<h5>Select a Project</h5>
						<div class="card-deck" >
							<asp:Repeater ID="Repeater1" runat="server" DataSourceID="odsProjects" >
								<ItemTemplate>
									<div class="card mb-3" style="width: 18rem;">
									  <asp:image  ID="imgProj" runat="server"  style="height:400px;clip-path:inset(0px,100px,200px,0px,round,50px);"  cssclass="card-img-top kcard-body img-thumbnail" src='<%#Eval("imageURL") %>' AlternateText='<%#Eval("ProjectTYpeName") %>' title="Project" onError="this.onerror=null;this.src='/images/newconstruction.png';" />
									  <div class="card-body">
										<h5 class="card-title"><a href="frmProjects.aspx?ProjectID=<%#Eval("ID") %>"><%#Eval("Name") %></a></h5>
										<p class="card-text"> <%#Eval("Description") %>
										<br /><%#Eval("ProjectTypeName") %></p>
									  </div>
									</div>
								</ItemTemplate>
							</asp:Repeater>
						</div>
  					</div>


            <asp:ObjectDataSource ID="odsProjects" runat="server" 
				SelectMethod="LoadProjects" TypeName="Kolassa.DesignCentre.Data.clsSelectDataLoader" 
				InsertMethod="InsertProjects" UpdateMethod="UpdateProjects" 
				DeleteMethod="DeleteProjects"   >
    
				<UpdateParameters>
					<asp:Parameter Name="llNodeID" Type="Int64" />
					<asp:Parameter Name="lsName" Type="String" />
					<asp:Parameter Name="lsDescription" Type="String" />
					<asp:Parameter Name="lsImage" Type="String" />
					<asp:Parameter Name="lsProjectType" Type="String" />
					<asp:Parameter Name="lsActive" Type="String" />            
					<asp:Parameter Name="ID" Type="String" />
				</UpdateParameters>
          
				<SelectParameters>
					<asp:SessionParameter Name="llNodeID" SessionField="NodeID" DefaultValue="0" />
					<asp:Parameter Name="lsWhere" Type="String" DefaultValue="" />
					<asp:Parameter Name="lbActive" Type="Boolean" DefaultValue="True" />
					<asp:Parameter Name="lsID" Type="String" DefaultValue="" />
				</SelectParameters>
      
				<DeleteParameters>
					<asp:Parameter Name="RecordID" Type="String"  />
				</DeleteParameters>
      
				<InsertParameters>
					<asp:Parameter DefaultValue="2" Name="llNodeID" Type="Int64" />
					<asp:Parameter Name="lsName" Type="String" />
					<asp:Parameter Name="lsDescription" Type="String" />
					<asp:Parameter Name="lsImage" Type="String" />
					<asp:Parameter Name="lsProjectType" Type="String" />
				</InsertParameters>
                   
			</asp:ObjectDataSource>
<script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
		</LoggedInTemplate>
	</asp:LoginView>


</asp:Content>
