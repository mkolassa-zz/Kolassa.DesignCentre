<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Src="~/ctrlImages.ascx" TagPrefix="uc1" TagName="ctrlImages" %>
<%@ Register Src="~/ctrlGoogleChartPie.ascx" TagPrefix="uc1" TagName="ctrlGoogleChartPie" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" >
 
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
            <h2>Getting Started</h2>
            <p>
                Design Centre enables you to build out options, units, plans and other data intensive structures quickly to get right down to business selling products and services.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
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
            <LoggedInTemplate >
                            </LoggedInTemplate>
    </asp:LoginView>
                <style>
                      #test {
                    width:100%;
                    height:100%;
                    }
                  table {
                    margin: 0 auto; /* or margin: 0 auto 0 auto */
                       }

                </style>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
          SelectMethod="LoadUnits" TypeName="clsSelectDataLoader" 
            DeleteMethod="DeleteUnits" InsertMethod="InsertUnits" UpdateMethod="UpdateUnits"   >
      
      
            <UpdateParameters>
                <asp:Parameter Name="llNodeID" Type="Int64" />
                <asp:Parameter Name="lsUnitName" Type="String" />
                <asp:Parameter Name="lsFloorID" Type="String" />
                <asp:Parameter Name="lsUnitTypeID" Type="String" />
                <asp:Parameter Name="lsAvailable" Type="String" />
                <asp:Parameter Name="lsTier" Type="String" />
                <asp:Parameter Name="lsDepositType" Type="String" />
            
                <asp:Parameter Name="UnitID" Type="String" />
                <asp:Parameter Name="lsActive" Type="String" />
            </UpdateParameters>
    
            <SelectParameters>
                <asp:Parameter Name="llNodeID" Type=Int64 />
                <asp:Parameter Name="lsWhere" Type="String" />
                <asp:Parameter Name="lbActive" Type="Boolean" />
                <asp:Parameter Name="llID" Type="Int64" />
            </SelectParameters>
      
            <DeleteParameters>
                <asp:Parameter Name="UnitID" Type="Int64" />
            </DeleteParameters>
        
            <InsertParameters>
                <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID" 
                    Type="Int64" />
                <asp:Parameter Name="lsUnitName" Type="String" />
                <asp:Parameter Name="lsFloorID" Type="String" />
                <asp:Parameter Name="lsUnitTypeID" Type="String" />
                <asp:Parameter Name="lsAvailable" Type="String" />
                <asp:Parameter Name="lsTier" Type="String" />
                <asp:Parameter Name="lsDepositType" Type="String" />
            </InsertParameters>
    </asp:ObjectDataSource>


                <div id="test">
                    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="odsProjects" >
                    <ItemTemplate>
                        <table class="table" >
                            <tr><td style="font-weight:700;"><a href="frmUnits.aspx/?ProjectID=<%#Eval("ID") %>"><%#Eval("Name") %></a>
                            <br /><%#Eval("Description") %>
                            <br /><%#Eval("ProjectTYpeName") %>
                               <br />
                 
                                </td>
                        </table>
                    </ItemTemplate>

                </asp:Repeater>
                    <uc1:ctrlImages runat="server" id="ctrlImages" />
                </div>
                
                <asp:ObjectDataSource ID="odsProjects" runat="server" 
      SelectMethod="LoadProjects" TypeName="clsSelectDataLoader" InsertMethod="InsertProjects" UpdateMethod="UpdateProjects" DeleteMethod="DeleteProjects"   >
    
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
            <asp:Parameter DefaultValue="2" Name="llNodeID"  Type="Int64" />
            <asp:Parameter Name="lsWhere" Type="String" DefaultValue="" />
            <asp:Parameter Name="lbActive" Type="Boolean" DefaultValue="True" />
            <asp:Parameter Name="llID" Type="Int64" DefaultValue="1" />
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

                <asp:Panel ID="pnlDashboard" runat="server" BorderWidth="10px">
                    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" /> <uc1:ctrlGoogleChartPie runat="server" ID="ctrlGoogleChartPie" /></asp:Panel>

   
</asp:Content>
