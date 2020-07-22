<%@ Page Language="vb" EnableEventValidation=false AutoEventWireup="false" MasterPageFile="~/Site.master" Inherits="frmFloors" Codebehind="frmFloors.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="ReportManager/ReportContainer.ascx" tagname="ReportContainer" tagprefix="uc1" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<asp:Content ID=Content1 runat=server ContentPlaceHolderID="MainContent" >

   
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
        TargetControlID="Panel1"    CollapsedSize="0"    Collapsed="True"     ExpandControlID="Panel2"   
        CollapseControlID="Panel2"  AutoCollapse="False" AutoExpand="False"   ScrollContents="True"
        TextLabelID="Label1"        CollapsedText="Show Filter..."            ExpandedText="Hide Filter" 
        ImageControlID="Image1"     ExpandedImage="~/images/expand.jpg"       CollapsedImage="~/images/collapse.jpg"
        ExpandDirection="Vertical" >
    </cc1:CollapsiblePanelExtender>

   
    <asp:Panel ID="Panel1" runat="server" >
     <!--   <uc1:ReportContainer ID="ReportContainer1" runat="server"  ReportCategoryType="frmVendors"  /> -->
        <asp:Literal ID="litMsg" runat="server"></asp:Literal><br />
    </asp:Panel>
    
    <asp:Button ID="cmdRunReport"   runat="server" Text="Filter" /><br />  <!-- autopostback=true -->
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
                <asp:ListItem Text='10' Value="10" />
                <asp:ListItem Text='20' Value="20" />
                <asp:ListItem Text='50' Value="50" />
                </asp:DropDownList>
    <table >
        <tr>
            <td>
                &nbsp;<asp:Literal ID="Literal1" runat="server" Text="Records Per Page  "></asp:Literal>
                <asp:DropDownList ID="ddlPagelength" runat="server" AutoPostBack="True">
                <asp:ListItem Text='10' Value="10" />
                <asp:ListItem Text='20' Value="20" />
                <asp:ListItem Text='50' Value="50" />
                </asp:DropDownList>
            </td>
            <td style="align-content:center;">
                <asp:Literal ID="Literal2" runat="server" Text="Actions  "></asp:Literal>
                <asp:ImageButton ID="imgExcel" runat="server" 
                    ImageUrl="~/images/excel.jpg" Width="22px" AlternateText="Export To Excel" 
                    Height="27px" />
            </td>
        </tr>
    </table>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate> 

            <asp:gridview id="DataGridDisplay" runat="server" DataSourceID="ObjectDataSource1"
                 CssClass="GridView" 
                
                 GridLines="Vertical" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="FloorID">           
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle"  />            
               <Columns>
               <asp:CommandField
                    ButtonType="image"
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
               
                      <asp:Templatefield HeaderText="ID" SortExpression="FloorID">          
                        <itemTemplate>
                            <asp:Literal id="FloorID" runat="server"  Text='<%#  Bind("FloorID") %>'  />
                        </ItemTemplate> 
                        <EdititemTemplate>
                            <asp:Literal id="FloorID" runat="server" /> <!-- Text='< %# Container.DataItem("FloorID") % >'  /> -->
                        </EditItemTemplate>                  
                    </asp:TemplateField>

     <asp:TemplateField HeaderText="Name" SortExpression="FloorName">
                        <FooterTemplate>
                            <asp:TextBox id="FloorName" MaxLength=10 runat="server" />   
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox Width=100px id="FloorName" MaxLength=10 runat="server" Text='<%# Container.DataItem("FloorName") %>'/>
                        </EditItemTemplate>
                        <itemTemplate>
                            <asp:Literal id="FloorName" runat="server"  Text='<%# Container.DataItem("FloorName") %>'  />
                        </ItemTemplate>             
                    </asp:TemplateField>
                    
                    
                 <asp:TemplateField HeaderText="Description" SortExpression="FloorDescription">
                        <FooterTemplate>
                            <asp:TextBox id="FloorDescription" MaxLength=10 runat="server" />   
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox Width=100px id="FloorDescription" MaxLength=10 runat="server" Text='<%# Container.DataItem("FloorDescription") %>'/>
                        </EditItemTemplate>
                        <itemTemplate>
                            <asp:Literal id="FloorDescription" runat="server"  Text='<%# Container.DataItem("FloorDescription") %>'  />
                        </ItemTemplate>             
                   </asp:TemplateField>
                    
            
                        <asp:TemplateField HeaderText="Building Level" SortExpression="BuildingLevel">
                        <FooterTemplate>
                            <asp:DropDownList ID="cboBuildingLevel" runat="server" DataTextField="LookupDescription" DataValueField="LookupID"  DataSourceid="odsBuildingLevel" > </asp:DropDownList>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox id="BuildingLevel" MaxLength=50 runat="server" Text='<%# Container.DataItem("BuildingLevel") %>' Visible=false  />
                            <asp:DropDownList ID="cboBuildingLevel" runat="server" DataTextField="LookupDescription" DataValueField="LookupID"  DataSourceid="odsBuildingLevel" SelectedValue='<%# Container.DataItem("BuildingLevel") %>'/> 
                        </EditItemTemplate>
                         <itemTemplate>
                            <asp:Literal id="BuildingLevelID" runat="server" Visible=false  Text='<%# Container.DataItem("BuildingLevel") %>'  />
                            <asp:Literal id="BuildingLevel" runat="server"  Text='<%# Container.DataItem("BuildingLevelText") %>'  />
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
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:gridview>
           <asp:Button ID="cmdAddNew" Text="Add New Record" runat="server" Visible="False"/>
            <asp:ImageButton ID="imgAdd" runat="server" ImageUrl="~/images/add.png" 
                AlternateText="Add New Record" />
  </ContentTemplate>
  
        
        <triggers>
            <asp:PostBackTrigger ControlID="cmdRunReport"  />
            <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlPagelength" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cmdAddNew" EventName="Click" />
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

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="clsSelectDataLoader" 
      SelectMethod="LoadFloors" 
       
      DeleteMethod="DeleteFloors"
      UpdateMethod="UpdateFloors"
      InsertMethod="InsertFloors" >
 
    </asp:ObjectDataSource>




    <asp:ObjectDataSource ID="odsBuildingLevel" runat="server" 
        SelectMethod="LoadLookups" TypeName="clsSelectDataLoader">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID"                 Type="Int64" />
            <asp:Parameter DefaultValue="" Name="lsWhere" Type="String" />
            <asp:Parameter DefaultValue=True Name="lbActive" Type="Boolean" />
            <asp:Parameter DefaultValue="" Name="llID" Type="Int64" />
            <asp:Parameter DefaultValue="BuildingLevel" Name="lsLookupType" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    
    </asp:Content>