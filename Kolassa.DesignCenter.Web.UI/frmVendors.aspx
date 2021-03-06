<%@ Page Language="vb" EnableEventValidation=false AutoEventWireup="false" MasterPageFile="~/Site.master" Inherits="Kolassa.DesignCenter.Web.UI.frmVendors" Codebehind="frmVendors.aspx.vb" %>

<%@ Register Assembly="Kolassa.DesignCenter.ReportManager" Namespace="Kolassa.DesignCenter.ReportManager" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<asp:Content ID=Content1 runat=server ContentPlaceHolderID="MainContent" >
    <asp:ScriptManager ID="ScriptManager2" runat="server">
    </asp:ScriptManager>

        <asp:Panel ID="Panel2" BackColor=Bisque runat="server" CssClass="collapsePanelHeader" Height="30px"> 
            <div style="padding:5px; cursor: pointer; vertical-align: middle;">
                <div style="float: left;"></div>
                <div style="float: left; margin-left: 20px;">
                    <asp:Label ID="Label2" runat="server"></asp:Label>  
          
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </div>
                <div style="float: right; vertical-align: middle;">
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </div>
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
        <cc2:ReportContainer ID="ReportContainer2" runat="server"  
         ReportCategoryType="frmVendors"  />
    <asp:Button ID="cmdRunReport" runat="server" Text="Filter" /><br />
    <asp:Literal ID="litMsg" runat="server"></asp:Literal><br />
       
    </asp:Panel>

<table style="width: 100%;"><tr><td>
    <asp:Literal ID="Literal1" runat="server" Text="Records Per Page  "></asp:Literal>


    <asp:DropDownList ID="ddlPagelength" runat="server" AutoPostBack="True">
    <asp:ListItem Text='10' Value="10" />
    <asp:ListItem Text='20' Value="20" />
    <asp:ListItem Text='50' Value="50" />
    </asp:DropDownList>
</td><td style="align-content:flex-end;"">
 <asp:Literal ID="Literal2" runat="server" Text="Actions  "></asp:Literal>


    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
        <asp:ListItem Text='[Select One]' Value="SelectOne" />
        <asp:ListItem Text='Export To Excel' Value="ExportToExcel" />
    </asp:DropDownList>
</td></tr></table>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate> 

            <asp:gridview id="DataGridDisplay" runat="server" DataSourceID="ObjectDataSource1"
                AutoGenerateDeleteButton="True" DataKeyNames="VendorID" 
                AutoGenerateColumns="False" 
                AutoGenerateEditButton="True"
                 CssClass="GridView" 
                
                 GridLines="Vertical" AllowPaging="True" 
                AllowSorting="True">           
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle"  />
               
                
                <Columns>
                    <asp:Templatefield HeaderText="ID" SortExpression="VendorID">          
                        <itemTemplate>
                            <asp:Literal id="VendorID" runat="server"  Text='<%# Container.DataItem("VendorID") %>'  />
                        </ItemTemplate>                  
                    </asp:TemplateField>
  
                    <asp:TemplateField HeaderText="Name" SortExpression="VendorName">
                        <FooterTemplate>
                            <asp:TextBox id="VendorName" MaxLength=50 runat="server" Text='<%# bind("VendorName") %>'/>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="VendorName" ErrorMessage="* Required Field" ></asp:RequiredFieldValidator>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox id="VendorName" MaxLength=50 runat="server" Text='<%# Container.DataItem("VendorName") %>'/>
                        </EditItemTemplate>
                        <itemTemplate>
                            <asp:Literal id="VendorName" runat="server"  Text='<%# Container.DataItem("VendorName") %>'  />
                        </ItemTemplate>             
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Contact" SortExpression="VendorContact">
                        <FooterTemplate>
                            <asp:TextBox id="VendorContact" MaxLength=50 runat="server" Text='<%# Bind("VendorContact") %>'/>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox id="VendorContact" MaxLength=50 runat="server" Text='<%# Container.DataItem("VendorContact") %>'/>
                        </EditItemTemplate>
                         <itemTemplate>
                            <asp:Literal id="VendorContact" runat="server"  Text='<%# Container.DataItem("VendorContact") %>'  />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Abbreviation" SortExpression="VendorAbbreviation">
                        <FooterTemplate>
                            <asp:TextBox id="VendorAbbreviation" MaxLength=5 runat="server" Text='<%# Bind("VendorAbbreviation") %>'/>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:TextBox id="VendorAbbreviation" MaxLength=5 runat="server" Text='<%# Container.DataItem("VendorAbbreviation") %>'/>
                        </EditItemTemplate>
                         <itemTemplate>
                             <asp:Literal id="VendorAbbreviation" runat="server"  Text='<%# Container.DataItem("VendorAbbreviation") %>'  />
                        </ItemTemplate>
                     </asp:TemplateField>   
                       
                    
                    <asp:TemplateField HeaderText="Active" SortExpression="Active">
                 <FooterTemplate>
                           <asp:Button ID="cmdAdd" Text="Add" runat="server" onclick="cmdAdd_Click"/>
                        </FooterTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox id="chkActive" runat=server Checked='<%# Container.DataItem("Active") %>' />
                        </EditItemTemplate>
                         <itemTemplate>
                            <asp:Literal id="litActive" runat="server"  Text='<%# Container.DataItem("Active") %>'  />
                        </ItemTemplate>
                    </asp:TemplateField>
   
                </Columns>
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:gridview>
           <asp:Button ID="cmdAddNew" Text="Add New Record" runat="server"/>
  </ContentTemplate>
  
        
        <triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlPagelength" 
                EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="cmdAddNew" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="cmdRunReport" EventName="Click" />
        </triggers>
  
        
    </asp:UpdatePanel>
    <asp:UpdateProgress runat= server ID="UP1" AssociatedUpdatePanelID="UpdatePanel1">
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
    SelectMethod="LoadVendors" TypeName="clsSelectDataLoader" 
        DeleteMethod="DeleteVendors" InsertMethod="InsertVendors" 
        UpdateMethod="UpdateVendors">

      
        <SelectParameters>
            <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID"                 Type="Int64" />
            <asp:SessionParameter  Name="lsWhere"     SessionField="msFilter" Type="String" />
        </SelectParameters>
        <InsertParameters>
            <asp:sessionParameter Name="llNodeID" Type="Int64" SessionField="NodeID" DefaultValue="1"  />
            <asp:ControlParameter Name="lsName"   ControlID="DataGridDisplay" Type="String"  PropertyName="SelectedValue" />
            <asp:ControlParameter Name="lsContact" ControlID="DataGridDisplay"   Type="String" PropertyName="SelectedValue" />
            <asp:ControlParameter Name="lsAbbreviation" ControlID="DataGridDisplay"  Type="String" PropertyName="SelectedValue" />
        </InsertParameters>
        
        <DeleteParameters>
            <asp:Parameter Name="VendorID" Type="Int64" />
        </DeleteParameters>
        
    <UpdateParameters>       
            <asp:sessionParameter Name="llNodeID" Type="Int64" SessionField="NodeID" DefaultValue="1"  />
            <asp:ControlParameter Name="lsName"         ControlID="DataGridDisplay" 
                Type="String" PropertyName="SelectedValue" DefaultValue="z" />
            <asp:ControlParameter Name="lsContact"      ControlID="DataGridDisplay" 
                Type="String" PropertyName="SelectedValue" DefaultValue="z" />
            <asp:ControlParameter Name="lsAbbreviation" ControlID="DataGridDisplay" 
                Type="String" PropertyName="SelectedValue" DefaultValue="z" />
            <asp:ControlParameter Name="lsActive"       ControlID="DataGridDisplay" 
                Type="String" PropertyName="SelectedValue" DefaultValue="True" />
            <asp:ControlParameter Name="VendorID"     ControlID="DataGridDisplay" 
                Type="String" PropertyName="SelectedValue" DefaultValue="4" />
       </UpdateParameters>
</asp:ObjectDataSource>


    </asp:Content>