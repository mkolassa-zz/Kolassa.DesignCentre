<%@ Page Language="vb" EnableEventValidation=false AutoEventWireup="false" MasterPageFile="~/Site.Master" Inherits="frmUnits" Codebehind="frmUnits.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="ReportManager/ReportContainer.ascx" tagname="ReportContainer" tagprefix="uc1" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>



<asp:Content ID=Content1 runat=server ContentPlaceHolderID="MainContent" >
    <style>
        table.glyphicon-hover .glyphicon { visibility: hidden; }
        table.glyphicon-hover td:hover .glyphicon {  visibility: visible; }
    </style>

    <asp:Panel ID="Panel2"  runat="server"  Height="30px"> 
        <div>
            <asp:Label ID="Label2" runat="server"></asp:Label>  
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </div>
        <div >
                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)"/>
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
        <uc1:ReportContainer ID="ReportContainer1" runat="server"  ReportCategoryType="frmVendors"  />
        <asp:Button ID="cmdRunReport" runat="server" Text="Filter" />
        <br />
        <asp:Literal ID="litMsg" runat="server"></asp:Literal><br />
    </asp:Panel>

<table style="width: 100%;"><tr><td>
    &nbsp;<asp:Literal ID="Literal1" runat="server" Text="Records Per Page  "></asp:Literal>
    <asp:DropDownList ID="ddlPagelength" runat="server" AutoPostBack="True">
        <asp:ListItem Text='10' Value="10" />
        <asp:ListItem Text='20' Value="20" />
        <asp:ListItem Text='50' Value="50" />
    </asp:DropDownList>
    </td><td  style="align-content:center;">
        <asp:Literal ID="Literal2" runat="server" Text="Actions  "></asp:Literal>
        <asp:ImageButton ID="imgExcel" runat="server" 
            ImageUrl="~/images/excel.jpg" Width="22px" AlternateText="Export To Excel" 
            Height="27px" />
    </td></tr></table>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate> 

            <asp:gridview id="DataGridDisplay" runat="server" DataSourceID="ObjectDataSource1"
                 CssClasss="GridView" 
                   CssClass="table table-striped table-bordered table-hover glyphicon-hover"
                 GridLines="Vertical" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="UnitID">           
             
                      
               <Columns>
               <asp:CommandField
                   ButtonType="Link" ControlStyle-CssClass="btn glyphicon-hover " HeaderText="Edit" ShowHeader="True" 
                   ShowSelectButton="True"  
                    SelectText="<i aria-hidden='true' class='glyphicon glyphicon-Check'></i>"
                    EditText=  "<i aria-hidden='true' class='glyphicon glyphicon-pencil'></i>"
                    DeleteText="<i aria-hidden='true' class='glyphicon glyphicon-remove'></i>"
                    UpdateText="<i aria-hidden='true' class='glyphicon glyphicon-ok'></i>"
                    CancelText="<i aria-hidden='true' class='glyphicon glyphicon-ban-circle'></i>"
                    InsertText="<i aria-hidden='true' class='glyphicon glyphicon-plus'></i>"
                    
                    ValidationGroup="Edit"
                    ShowEditButton="true"
                    ShowDeleteButton="true"
                    ShowCancelButton="true"
                    EditImageUrl="images/edit.jpg"
                    DeleteImageUrl="images/Delete.png"                   
                    UpdateImageUrl="images/OK.png"                   
                    CancelImageUrl="~/images/cancel.png"      
                                
                    ItemStyle-HorizontalAlign="right"
                    ItemStyle-VerticalAlign="top"
                    ItemStyle-Wrap="false" >
                 
                   <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False" />
               </asp:CommandField>
               
                   <asp:Templatefield HeaderText="ID" SortExpression="ID">          
                        <itemTemplate>            
                            <asp:Literal id="ID" runat="server"  Text='<%# Bind("ID") %>'  />
                        </ItemTemplate> 
                        <EdititemTemplate>
                            <asp:Literal id="ID" runat="server"  Text='<%# Container.DataItem("ID") %>'  />
                        </EditItemTemplate>                  
                    </asp:TemplateField>


                      <asp:Templatefield HeaderText="Unit ID" SortExpression="UnitID"> 
                        <FooterTemplate>
                            <asp:TextBox id="UnitID" MaxLength=10 runat="server" />
                        </FooterTemplate>        
                        <itemTemplate>            
                            <asp:Literal id="UnitID" runat="server"  Text='<%# Bind("UnitID") %>'  />
                        </ItemTemplate> 
                        <EdititemTemplate>
                             <asp:TextBox Width=100px id="UnitID" MaxLength=10 runat="server" Text='<%# Container.DataItem("UnitID") %>'/>
                        </EditItemTemplate>                  
                    </asp:TemplateField>
                     
  
                 <asp:TemplateField HeaderText="Name" SortExpression="UnitName">
                        <FooterTemplate>
                            <asp:TextBox id="UnitName" MaxLength=10 runat="server" />
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox Width=100px id="UnitName" MaxLength=10 runat="server" Text='<%# Container.DataItem("UnitName") %>'/>
                        </EditItemTemplate>
                        <itemTemplate>
                            <asp:Literal id="UnitName" runat="server"  Text='<%# Container.DataItem("UnitName") %>'  />
                        </ItemTemplate>             
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Unit Type" SortExpression="UnitTypeID">
                        <FooterTemplate>
                           
                            <asp:DropDownList ID="cboUnitTypeID" runat="server" DataTextField="UnitTypeName" DataValueField="UnitTypeID"  DataSourceid="odsUnitTypes" > </asp:DropDownList>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox id="UnitTypeID" MaxLength=50 runat="server" Text='<%# Container.DataItem("UnitTypeID") %>' Visible=false  />
                            <asp:DropDownList ID="cboUnitTypeID" runat="server" DataTextField="UnitTypeName" DataValueField="UnitTypeID"  DataSourceid="odsUnitTypes" SelectedValue='<%# Container.DataItem("UnitTypeID") %>'/> 
                        </EditItemTemplate>
                         <itemTemplate>
                               <asp:Literal id="UnitTypeID" runat="server" Visible=false  Text='<%# Container.DataItem("UnitTypeID") %>'  />
                            <asp:Literal id="UnitTypeName" runat="server"  Text='<%# Container.DataItem("UnitTypeName") %>'  />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Deposit Type" SortExpression="DepositType">
                        <FooterTemplate>
                           
                            <asp:DropDownList ID="cboDepositTypeID" runat="server" DataTextField="DepositTypeName" DataValueField="DepositTypeID"  DataSourceid="odsDepositConditions" /> 
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox id="DepositType" MaxLength=5 runat="server" Text='<%# Container.DataItem("DepositTypeID") %>' Visible=false  />
                            <asp:DropDownList ID="cboDepositTypeID" runat="server" DataTextField="DepositTypeName" DataValueField="DepositTypeID"  DataSourceid="odsDepositConditions" SelectedValue='<%# Container.DataItem("DepositTypeID") %>'/> 
                        </EditItemTemplate>
                         <itemTemplate>
                            <asp:Literal id="DepositTypeID" runat="server"  Text='<%# Container.DataItem("DepositTypeID") %>' Visible=false  />
                             <asp:Literal id="DepositType" runat="server"  Text='<%# Container.DataItem("DepositTypeName") %>'  />
                        </ItemTemplate>
                     </asp:TemplateField>   
               
                     <asp:TemplateField HeaderText="Tier" SortExpression="Tier">
                        <FooterTemplate>
                            
                            <asp:DropDownList ID="cboTierID" runat="server" DataTextField="TierName" DataValueField="TierID"  DataSourceid="odsTiers" /> 
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox id="Tier" MaxLength=5 runat="server" Text='<%# Container.DataItem("TierID") %>' Visible=false  />
                            <asp:DropDownList ID="cboTierID" runat="server" DataTextField="TierName" DataValueField="TierID"  DataSourceid="odsTiers" SelectedValue='<%# Container.DataItem("TierID") %>'/> 
                        </EditItemTemplate>
                         <itemTemplate>
                             <asp:Literal id="TierID" runat="server"  Text='<%# Container.DataItem("TierID") %>' Visible=false  />
                             <asp:Literal id="Tier" runat="server"  Text='<%# Container.DataItem("TierName") %>'  />
                        </ItemTemplate>
                     </asp:TemplateField>   
                     
                     <asp:TemplateField HeaderText="Floor" SortExpression="FloorID">
                        <FooterTemplate>
                            
                            <asp:DropDownList ID="cboFloorID" runat="server" DataTextField="FloorName" DataValueField="FloorID"  DataSourceid="odsFloors" /> 
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox id="FloorID" MaxLength=5 runat="server" Text='<%# Container.DataItem("FloorID") %>' Visible=false  />
                             <asp:DropDownList ID="cboFloorID" runat="server" DataTextField="FloorName" DataValueField="FloorID"  DataSourceid="odsFloors" SelectedValue='<%# Container.DataItem("FloorID") %>' /> 
                        </EditItemTemplate>
                         <itemTemplate>
                            <asp:Literal id="FloorID" runat="server"  Text='<%# Container.DataItem("FloorID") %>' Visible=false  />
                             <asp:Literal id="FloorName" runat="server"  Text='<%# Container.DataItem("FloorName") %>'  />
                        </ItemTemplate>
                     </asp:TemplateField>    
                     
                         <asp:TemplateField HeaderText="Available" SortExpression="Available">
                        <FooterTemplate>
                            <asp:Checkbox id="Available" runat="server"  Enabled=True  />
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:Checkbox id="Available" runat="server"  Checked='<%# Container.DataItem("Available") %>' Enabled=True  />
                        </EditItemTemplate>
                         <itemTemplate>
                             <asp:Checkbox id="Available" runat="server"  Checked='<%# Container.DataItem("Available") %>' Enabled=false  />
                        </ItemTemplate>
                     </asp:TemplateField>          
                     
                    <asp:TemplateField HeaderText="Active" SortExpression="Active">
                        <FooterTemplate>
                           <asp:Button ID="cmdAdd" Text="Add" runat="server" onclick="cmdAdd_Click"/>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox id="Active" runat=server Checked='<%# Container.DataItem("Active") %>' />
                        </EditItemTemplate>
                         <itemTemplate>
                            <asp:CheckBox id="Active" runat=server Checked='<%# Container.DataItem("Active") %>' Enabled=false />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    
   
                </Columns>
           
            </asp:gridview>
           <asp:Button ID="cmdAddNew" Text="Add New Record" runat="server" Visible="False"/>
            <asp:ImageButton ID="imgAdd" runat="server" ImageUrl="~/images/add.png" 
                AlternateText="Add New Record" />
             <!--   <Pag erStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />  -->
        
  </ContentTemplate>
   
        <triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlPagelength" 
                EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cmdAddNew" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="cmdRunReport" EventName="Click" />
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
      SelectMethod="LoadUnits" TypeName="clsSelectDataLoader" InsertMethod="InsertUnits" UpdateMethod="UpdateUnits"
       DeleteMethod="DeleteUnits"   >
    
        <SelectParameters>
            <asp:SessionParameter DefaultValue="2" Name="llNodeID" SessionField="NodeID" Type="Int64" />
            <asp:Parameter Name="lsWhere" Type="String" />
            <asp:Parameter Name="lbActive" Type="Boolean" />
            <asp:Parameter Name="llID" Type="Int64" />
            <asp:Parameter Name="lsID" Type="String" />
        </SelectParameters>
      
        <DeleteParameters>
            <asp:Parameter Name="lsID" Type="String" />
        </DeleteParameters>
      
        <InsertParameters>
            <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID" Type="Int64" />
            <asp:Parameter Name="lsUnitID" Type="String" />
            <asp:Parameter Name="lsUnitName" Type="String" />
            <asp:Parameter Name="lsFloorID" Type="String" />
            <asp:Parameter Name="lsUnitTypeID" Type="String" />
            <asp:Parameter Name="lsAvailable" Type="String" />
            <asp:Parameter Name="lsTier" Type="String" />
            <asp:Parameter Name="lsDepositType" Type="String" />
        </InsertParameters>
        
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
        
</asp:ObjectDataSource>




    <asp:ObjectDataSource ID="odsUnitTypes" runat="server" 
        SelectMethod="LoadUnitTypes" TypeName="clsSelectDataLoader">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID"                 Type="Int64" />
            <asp:Parameter DefaultValue="" Name="lsWhere" Type="String" />
            <asp:Parameter DefaultValue="True" Name="lbActive" Type="Boolean" />
            <asp:Parameter DefaultValue="" Name="llID" Type="Int64" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    
    <asp:ObjectDataSource ID="odsTiers" runat="server" 
        SelectMethod="LoadTiers" TypeName="clsSelectDataLoader">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID"                 Type="Int64" />
            <asp:Parameter DefaultValue="" Name="lsWhere" Type="String" />
            <asp:Parameter Name="lbActive" Type="Boolean" />
            <asp:Parameter Name="llID" Type="Int64" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
        <asp:ObjectDataSource ID="OdsFloors" runat="server" 
        SelectMethod="LoadFloors" TypeName="clsSelectDataLoader">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID"                 Type="Int64" />
            <asp:Parameter DefaultValue="" Name="lsWhere" Type="String" />
            <asp:Parameter Name="lbActive" Type="Boolean" />
            <asp:Parameter Name="llID" Type="Int64" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <asp:ObjectDataSource ID="odsDepositConditions" runat="server" 
        SelectMethod="LoadDepositConditions" TypeName="clsSelectDataLoader">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID"                 Type="Int64" />
            <asp:Parameter DefaultValue="" Name="lsWhere" Type="String" />
            <asp:Parameter Name="lbActive" Type="Boolean" />
            <asp:Parameter Name="llID" Type="Int64" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </asp:Content>