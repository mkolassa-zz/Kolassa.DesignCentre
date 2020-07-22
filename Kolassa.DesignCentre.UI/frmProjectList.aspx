<%@ Page Language="vb" EnableEventValidation=false AutoEventWireup="false" MasterPageFile="~/Site.Master" Inherits="Kolassa.DesignCentre.UI.frmProjectList" Codebehind="frmProjectList.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<%@ Register assembly="Kolassa.DesignCenter.ReportManager" namespace="Kolassa.DesignCenter.ReportManager" tagprefix="cc2" %>
<%@ Register src="Controls/ctrlGoogleChartPie.ascx" tagname="ctrlGoogleChartPie" tagprefix="uc1" %>
<asp:Content ID=Content1 runat=server ContentPlaceHolderID="MainContent" >
	<uc1:ctrlGoogleChartPie ID="ctrlGoogleChartPie1" runat="server" />
        <asp:Panel ID="Panel2"  runat="server"  Height="100px" Width="100%"> 
            <div style="padding-top: 70px;">
                <asp:Label ID="Label2" runat="server"></asp:Label>  
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            
            </div>
            <div >
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)" BorderStyle="None"/>
            </div>

        </asp:Panel>
   
      
 <cc1:CollapsiblePanelExtender ID="cpe" runat="Server"
    TargetControlID="Panel1"
    CollapsedSize="0"
    
    Collapsed="True"
    ExpandControlID="Panel2"
    CollapseControlID="Panel2"
    AutoCollapse="False"
    AutoExpand="False"
    ScrollContents="True"
    TextLabelID="Label1"
    CollapsedText="Show Filter..."
    ExpandedText="Hide Filter" 
    ImageControlID="Image1"
    ExpandedImage="~/images/expand.jpg"
    CollapsedImage="~/images/collapse.jpg"
    ExpandDirection="Vertical" />
   
    <asp:Panel ID="Panel1" runat="server" >
       <asp:label runat="server" ID="dd" text=" <!-- c   c 2 : R e p o r t Container ID=ReportContainer2 runat=server / -->" Visible="false" />
        <asp:Button ID="cmdRunReport" runat="server" Text="Filter" />
        <br />
        <asp:Literal ID="litMsg" runat="server"></asp:Literal><br />    
    </asp:Panel>

<table ><tr><td>
    &nbsp;<asp:Literal ID="Literal1" runat="server" Text="Records Per Page  "></asp:Literal>


    <asp:DropDownList ID="ddlPagelength" runat="server" AutoPostBack="True">
    <asp:ListItem Text='10' Value="10" />
    <asp:ListItem Text='20' Value="20" />
    <asp:ListItem Text='50' Value="50" />
    </asp:DropDownList>
</td><td >
 <asp:Literal ID="Literal2" runat="server" Text="Actions  "></asp:Literal>


        <asp:ImageButton ID="imgExcel" runat="server" 
            ImageUrl="~/images/excel.jpg" Width="22px" AlternateText="Export To Excel" 
            Height="27px" BorderStyle="None" />
</td></tr></table>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate> 

              <asp:gridview id="DataGridDisplay" runat="server" DataSourceID="ObjectDataSource1"
                 CssClass="GridView" 
                 GridLines="Vertical" AllowPaging="True" 
                 AllowSorting="True" AutoGenerateColumns="False">           
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle"  />            
                <PagerStyle          BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle    BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle         BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:gridview>
           <asp:Button ID="cmdAddNew" Text="Add New Record" runat="server" Visible="False"/>
           <asp:ImageButton ID="imgAdd" runat="server" ImageUrl="~/images/add.png" AlternateText="Add New Record" Height="16px" Width="16px" />
        </ContentTemplate>
  
        
        <triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlPagelength" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cmdAddNew"     EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="cmdRunReport"  EventName="Click" />
         
        </triggers>
    </asp:UpdatePanel>

    <asp:UpdateProgress runat="server" ID="UP1" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate >
            <div id="OuterTableCellOverlay">
                <div id="InnerTableCellOverlay">
                    <b>... Please Wait ...</b>                              
                        <asp:Image ID="LoadImage" runat="server" ImageUrl="images/loading.gif" />
                 </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <br />

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
      SelectMethod="LoadProjects" TypeName="Kolassa.DesignCentre.Data.clsSelectDataLoader" InsertMethod="InsertProjects" UpdateMethod="UpdateProjects" DeleteMethod="DeleteProjects"   >
    
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




    <asp:ObjectDataSource ID="odsProjectTypes" runat="server" 
        SelectMethod="LoadProjectTypes" TypeName="Kolassa.DesignCentre.data.clsSelectDataLoader">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="llNodeID" SessionField="NodeID"                 Type="Int64" />
            <asp:Parameter DefaultValue="" Name="lsWhere" Type="String" />
            <asp:Parameter DefaultValue="True" Name="lbActive" Type="Boolean" />
            <asp:Parameter DefaultValue="" Name="llID" Type="Int64" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
        <asp:GridView ID="GridView1" runat="server" DataSourceID="odsProjectTypes">
        </asp:GridView>
    
  
    </asp:Content>