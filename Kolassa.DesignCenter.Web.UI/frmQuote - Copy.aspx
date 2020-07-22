<%@ Page Language="vb" EnableEventValidation="false" AutoEventWireup="false" MasterPageFile="~/Site.master" codebehind="frmQuote.aspx.vb" Inherits="frmQuote2"%>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="ReportManager/ReportContainer.ascx" tagname="ReportContainer" tagprefix="uc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent" >
    <asp:FormView runat="server" ID ="frmtest" DataSourceID="odsCategories" >
        <ItemTemplate>
            asdfasdf
        </ItemTemplate>
    </asp:FormView>
<a id="ctl00_ctl00_MainContent_LiveExample_loginstatus1" title="Click to login/logout." class="AspNet-LoginStatus LoginStatus-Skin" href="javascript:__doPostBack('ctl00$ctl00$MainContent$LiveExample$loginstatus1$ctl00','')">logout</a>

<br />

             <asp:Panel Runat="server" ID="pnlCalendar" CssClass="calendarHide"  Visible = "false">
				<asp:Calendar id="mCalendar" runat="server" BackColor="White" BorderColor="Black" 
                     BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" 
                     ForeColor="Black" Height="56px" NextPrevFormat="ShortMonth" Width="48px" >
                    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                    <TodayDayStyle BackColor="#999999" ForeColor="White" />
                    <DayStyle BackColor="#CCCCCC" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                    <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt"
                        ForeColor="White" Height="12pt" />
                </asp:Calendar>
                 <asp:TextBox ID="DD" Text="<!-- rsweb : ReportViewer ID='ReportViewer1' runat='server' -->     </!-->" runat="server" />
			</asp:Panel>
<table id="box-table-a" border="1">
                <tr  style="vertical-align:top;" >
                    <td >

                     <asp:Panel runat="server" ID="pnlQuote">      </asp:Panel>
                     <table><tr><td  style="vertical-align:top;">
                         
                        <asp:DetailsView  ID="DetailsView2" runat="server" DataSourceID="odsQuotes" AutoGenerateRows="False">
                            <Fields>
                                <asp:BoundField DataField="Customer" ReadOnly="true" HeaderText="Customer" />
                                <asp:BoundField DataField="Unit" ReadOnly="true" HeaderText="Unit" />
                                <asp:BoundField DataField="Unit Type" ReadOnly="true" HeaderText="Unit Type" />
                             </Fields>
                        </asp:DetailsView>
                        </td><td  style="vertical-align:top;">
                             <asp:FormView ID="FormView1" runat="server" DataSourceID="odsQuotes">
                             <EditItemTemplate >
                            
                                 <table>
                                     <tr>
                                        <td>  <asp:LinkButton ID="SaveButton" Text="save" CommandName="Save" RunAt="server"/>
                                             <asp:Label ID="lblQuoteStatus" runat="server" Height="20px">Quote Status</asp:Label>
                                         </td>
                                         <td style="width: 100%;">
                                            <asp:DropDownList runat="server" ID ="cmbQuoteStatus" DataTextField="LookupDescription" 
                                              DataValueField="LookupDescription" DataSourceID="odsQuoteStatus" AutoPostBack="True"></asp:DropDownList>
                                         </td>
                                         <td style="width: 100%;">
                                             <asp:Label ID="lblTargetDate" runat="server" Height="20px">Target Date</asp:Label>
                                         </td>
                                         <td style="width: 100%;">
                                             <asp:Label ID="lblQuoteDate" runat="server" Height="19px">Comp Date</asp:Label>
                                         </td>
                                     </tr>
                                     <tr>
                                         <td style="width:  100%;">
                                             <asp:Label ID="lblStructStatus" runat="server" Height="20px">Structural Status</asp:Label>
                                         </td>
                                         <td style="width: 1000px;">
                                             <asp:DropDownList ID="cmbPhase1Status" runat="server" 
                                                 onselectedindexchanged="cmbPhase1Status_SelectedIndexChanged" DataTextField="LookupDescription" 
                                              DataValueField="LookupDescription" DataSourceID="odsPhase1Status" AutoPostBack="True" SelectedValue='<%# Bind("Phase1Status")%>' >
                                             </asp:DropDownList>
                                         </td>
                                         <td style="width: 200px;">
                                              <asp:textbox id="txtPhase1TargetDate"  Width ="200px"  text='<%# Bind("Phase1TargetDate") %>' 
                                                  runat="server" AutoPostBack="True" 
                                                  ontextchanged="txtPhase1TargetDate_TextChanged" ></asp:textbox>   
                                                   <asp:ImageButton ID="imgPhase1TargetDate"   runat="server" 
                                                  ImageUrl="~/images/calendar.gif" onclick="imgPhase1TargetDate_Click"  />      
                                         </td>
                                         <td style="width: 200px;">
                                             <asp:TextBox ID="txtPhase1CompleteDate"  Width ="200px" runat="server" Text='<%# Bind("Phase1CompleteDate") %>'></asp:TextBox>
                                              <asp:ImageButton ID="imgPhase1CompleteDate"  runat="server" 
                                                 ImageUrl="~/images/calendar.gif" Height="16px" 
                                                 onclick="imgPhase1CompleteDate_Click"  /> 
                                         </td>
                                     </tr>
                                     <tr>
                                         <td style="width: 100%;">
                                             <asp:Label ID="lblFinishStatus" runat="server" Height="20px">Finish Status</asp:Label>
                                         </td>
                                         <td style="width: 100%;">
                                             <asp:DropDownList ID="cmbPhase2Status" runat="server" DataTextField="LookupDescription" 
                                              DataValueField="LookupDescription" DataSourceID="odsPhase2Status" 
                                                 AutoPostBack="True" 
                                                 onselectedindexchanged="cmbPhase2Status_SelectedIndexChanged">
                                             </asp:DropDownList>
                                         </td>
                                         <td style="width: 100%;">
                                                
                                             <asp:TextBox  Width ="200px"  ID="txtPhase2TargetDate" runat="server" Text='<%# Bind("Phase2TargetDate") %>'></asp:TextBox>
                                             <asp:ImageButton ID="imgPhase2TargetDate" runat="server" ImageUrl="~/images/calendar.gif" OnClick="imgPhase2TargetDate_Click" /> 
                                         </td>
                                         <td style="width: 100%;">
                                             <asp:TextBox  Width ="200px" ID="txtPhase2CompleteDate" runat="server" Text='<%# Bind("Phase2CompleteDate") %>' ></asp:TextBox>
                                             <asp:ImageButton ID="imgPhase2CompleteDate" runat="server" 
                                                 ImageUrl="~/images/calendar.gif" onclick="imgPhase2CompleteDate_Click"  /> 
                                         </td>
                                     </tr>
                                     <tr>
                                         <td style="width: 100%;">
                                             <asp:Button ID="btnAutoPop" runat="server" Height="15px" 
                                                 Text="Auto-&amp;Populate" />
                                         </td>
                                     </tr>
                                 </table>
                             </EditItemTemplate>
                             <ItemTemplate>
                                 <table>
                                     <tr>
                                         <td style="width: 100%;">
                                            <asp:LinkButton ID="EditButton" Text="Edit" CommandName="Edit" RunAt="server"/>
                                             <asp:Label ID="lblQuoteStatus" runat="server" Height="20px">Quote Status</asp:Label>
                                         </td>
                                         <td style="width: 100%;">
                                             <%#Eval("QuoteStatus")%>
                                         </td>
                                         <td style="width: 100%;">
                                             <asp:Label ID="lblLabel104" runat="server" Height="20px">Target Date</asp:Label>
                                         </td>
                                         <td style="width: 100%;">
                                             <asp:Label ID="lblQuoteDate" runat="server" Height="19px">Comp Date</asp:Label>
                                         </td>
                                     </tr>
                                     <tr>
                                         <td style="width: 100%;">
                                             <asp:Label ID="lblStructStatus" runat="server" Height="20px">Structural Status</asp:Label>
                                         </td>
                                         <td style="width: 100%;">
                                             <%#Eval("Phase1Status")%>
                                     
                                             
                                         </td>
                                         <td style="width: 100%;">
                                            <%#Eval("Phase1TargetDate")%>
                                         </td>
                                         <td style="width: 100%;">
                                             <%#Eval("Phase1CompleteDate")%>
                                         </td>
                                     </tr>
                                     <tr>
                                         <td style="width: 100%;">
                                             <asp:Label ID="lblFinishStatus" runat="server" Height="20px">Finish Status</asp:Label>
                                         </td>
                                         <td style="width: 100%;">
                                             <%#Eval("Phase2Status")%>
                                         </td>
                                         <td style="width: 100%;">
                                             <%#Eval("Phase2TargetDate")%>
                                         </td>
                                         <td style="width: 100%;">
                                             <%#Eval("Phase2CompleteDate")%>
                                         </td>
                                     </tr>
                                     <tr>
                                         <td style="width: 100%;">
                                             <asp:Button ID="btnAutoPop" runat="server" Text="Auto-&amp;Populate" />
                                         </td>
                                     </tr>
                                 </table>
                             </ItemTemplate>
                         </asp:FormView>
                         </td></tr></table>
                        <!-- / a s p : P a n e l -->  
                    </td>
              
    
                    <td >
                      
                         Building Phase<br />
                            <asp:RadioButtonList  ID="rblPhase" runat="server" TextAlign="Right" 
                             AutoPostBack="True">
                                <asp:ListItem Value="1">Structural</asp:ListItem>
                                <asp:ListItem Value="2">Finishes</asp:ListItem>
                                <asp:ListItem Value="3">Change Order</asp:ListItem>
                            </asp:RadioButtonList>
                       
                    </td>
                </tr>
            </table>

    
    <table style="width:98%; border-width:thick;" >
    <tr>
        <td>Location<br />
          <asp:ListBox      id="lstRooms" runat="server"  Width="90%"  Height="100%" DataSourceID="odsRooms" DataTextField="RoomName" DataValueField="RoomName" AutoPostBack="True"          ></asp:ListBox>
        </td>
        <td>Categories<br />
        <asp:UpdatePanel ID="updpnlCategory" runat="server" ><ContentTemplate>
                    <asp:ListBox      id="lstClasses" runat="server" Width="90%"  Height="100%" DataSourceID="odsCategories" DataTextField="UpgradeCategory" DataValueField="UpgradeCategory"          AutoPostBack="True"></asp:ListBox>
                </ContentTemplate>
               <Triggers >  
                   <asp:AsyncPostBackTrigger ControlID="lstRooms"   EventName="SelectedIndexChanged"  />
                     <asp:AsyncPostBackTrigger ControlID="rblPhase"   EventName="SelectedIndexChanged"  />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="updpnlCategory">
                <ProgressTemplate>            <asp:Image runat="server" ImageUrl="~/images/loading.gif" />            </ProgressTemplate>
             </asp:UpdateProgress>
        </td>
        <td>Levels<br />
             <asp:UpdatePanel ID="updpnlLevels" runat="server">
                <ContentTemplate>
                    <asp:ListBox      id="lstUpgradesAvail"  runat="server"  Width="90%" Height="100%" DataSourceID="odsLevels" DataTextField="UpgradeLevel"      DataValueField="UpgradeLevel" AutoPostBack="True" />
                </ContentTemplate>
               <Triggers >  
                   <asp:AsyncPostBackTrigger ControlID="lstClasses"   EventName="SelectedIndexChanged"  />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updpnlLevels">
                <ProgressTemplate>            <asp:Image ID="imgLoading" runat="server" ImageUrl="~/images/loading.gif" />            </ProgressTemplate>
             </asp:UpdateProgress>
            
        </td>
        <td>
            <asp:Button       ID="cmdAddNewOption" runat="server" 
                Height="20px"                       Text="Add New Upgrade Option" 
                Width="135px" />
            <asp:Button       id="cmdSearchQuotes"  runat="server"  Width="115px"  Height="25px"      Text="Search Quotes"  ></asp:Button><br />
            <asp:Button       id="cmdAutoPick"  runat="server"  Width="115px"  Height="25px"          Text="Auto Pick"  ></asp:Button><br />
            <asp:Button       id="cmdMissingSelections"  runat="server"  Width="115px"  Height="25px" Text="Missing Rpt "  ></asp:Button>
            <asp:Button       id="cmdCommunications" runat="server"  Width="115px"  Height="25px"     Text="Communications"  ></asp:Button>
            <br />
            <asp:Button       id="cmdPayments" runat="server"  Width="115px"  Height="25px"           Text="Payments"  ></asp:Button>
            <asp:Button       id="cmdAdjustments"  runat="server"  Width="115px"  Height="25px"       Text="Adjustments"  ></asp:Button><br />
            <asp:Button       id="cmdCustomerReceipt" runat="server"  Width="115px"  Height="25px"    Text="Preview Receipt"  ></asp:Button>
        </td>
     </tr>
     <tr>
        <td style="align-content:flex-start;" colspan="4"> 
            <asp:UpdatePanel ID="updpnlStyle" runat="server">
            <ContentTemplate>
                <asp:Panel runat="server" ID="pnlStyle" Height="250px" ScrollBars="Vertical">                
                     <asp:ListView ID="lstStyle" runat="server" DataSourceID="odsStyle" 
                         DataKeyNames="CustomerPrice,Style"> 
                        <LayoutTemplate>
                            <table style="width:100%" id="gradient-style">
                                <tr>
                                   
                                    <th style="width:5%">Insert</th>
                                    <th style="width:60%">Description</th>
                                    <th style="width:15%">Style</th>
                                    <th style="width:15%">Customer Price</th>
                                
                               </tr>
                               <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                           </table>
                       </LayoutTemplate>
                       <ItemTemplate>
                            <tr>
                            
                                <td><asp:LinkButton runat="server" ID="InsertItem" Text="Insert" CommandName="InsertRequestedItem" CommandArgument='<%#Eval("UpgradeOptionID") %>' /></td>
                                <td><%#Eval("Description")%></td>
                                <td><%#Eval("Style")%></td>
                                <td><%#Eval("CustomerPrice")%></td>
                            </tr>
                        </ItemTemplate>
                          <SelectedItemTemplate>
                              
                                 <td><asp:LinkButton runat="server" ID="InsertItem" Text="Insert" CommandName="InsertRequestedItem" CommandArgument='<%#Eval("UpgradeOptionID") %>' /></td>
                                <td><%#Eval("Description")%></td>
                                <td><%#Eval("Style")%></td>
                                <td><%#Eval("CustomerPrice")%></td>
                          </SelectedItemTemplate>

                    </asp:ListView>
                  
                    </asp:Panel>
                    <br />
                </ContentTemplate>
               <Triggers >  
                   <asp:AsyncPostBackTrigger ControlID="lstUpgradesAvail"   EventName="SelectedIndexChanged"  />
                   <asp:AsyncPostBackTrigger ControlID="lstStyle" EventName="ItemCommand" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="updprogStyle" runat="server" AssociatedUpdatePanelID="updpnlStyle">
                <ProgressTemplate>            <asp:Image ID="ImageStyle" runat="server" ImageUrl="~/images/loading.gif" />            </ProgressTemplate>
             </asp:UpdateProgress>                  
             
    
  
  
            <asp:UpdatePanel ID="updpnlRequestedUpgrades" runat="server">
            <ContentTemplate>
                <asp:Panel runat="server" ID="pnlRequestedUpgrades" Width="100%" Height="250px" ScrollBars="Vertical">                
                 <asp:ListView ID="lstSelectedUpgrade" runat="server" 
                         DataSourceID="odsRequestedUpgrade"> 
                         
                        <LayoutTemplate>
                            <table style="width:100%" id="gradient-style">
                                <tr>
                                    <th style="width:5%">Delete</th>
                                    <th style="width:10%">Category</th>
                                    <th style="width:10%">Upgrade Level</th>
                                    <th style="width:55%">Description</th>
                                    <th style="width:10%">Style</th>
                                    <th style="width:10%">Cost</th>                             
                               </tr>
                               <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                           </table>
                       </LayoutTemplate>
                       <ItemTemplate>
                            <tr>
                                <td style="width:40px"><asp:LinkButton runat="server" ID="DeleteCategoryButton" CommandName="DeleteRequestedItem" CommandArgument='<%#Eval("RequestedUpgradeID") %>'  Text="Delete"  /> </td>
                                <td><%#Eval("Category")%></td>
                                <td><%#Eval("UpgradeLevel")%></td>
                                <td><%#Eval("Description")%></td>
                                <td><%#Eval("Style")%></td>
                                <td><%#Eval("Cost")%></td>
                            </tr>
                        </ItemTemplate>
                     
                        </asp:ListView>
                               </asp:Panel>
                    <br />
                </ContentTemplate>
               <Triggers >  
                                    <asp:AsyncPostBackTrigger ControlID="lstRooms"   EventName="SelectedIndexChanged"  />
                        <asp:AsyncPostBackTrigger ControlID="lstStyle"   EventName="ItemCommand"  />
                       <asp:AsyncPostBackTrigger ControlID="lstRequestedUpgrades" 
                           EventName="ItemCommand" />
                       <asp:AsyncPostBackTrigger ControlID="lstSelectedUpgrade" 
                           EventName="ItemCommand" />      </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updpnlStyle">
                <ProgressTemplate>            <asp:Image ID="ImageStyle1" runat="server" ImageUrl="~/images/loading.gif" />            </ProgressTemplate>
             </asp:UpdateProgress>                  
             
    
  
  
            <asp:UpdatePanel ID="updpnlReqUpgrades" runat="server">
            <ContentTemplate>
                <asp:Panel runat="server" ID="pnlReqUpgrades" Width="100%" Height="250px" ScrollBars="Vertical">                
  
                     <asp:ListView ID="lstRequestedUpgrades" runat="server" 
                         DataSourceID="odsRequestedUpgrades"> 
                        <LayoutTemplate>
                            <table style="width:100%" id="gradient-style">
                                <tr>
                                    <th style="width:5%">Delete</th>
                                    <th style="width:10%">Category</th>
                                    <th style="width:10%">Upgrade Level</th>
                                    <th style="width:55%">Description</th>
                                    <th style="width:10%">Style</th>
                                    <th style="width:10%">Cost</th>                             
                               </tr>
                               <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                           </table>
                       </LayoutTemplate>
                       <ItemTemplate>
                            <tr>
                                <td style="width:40px"><asp:LinkButton runat="server" ID="SelectCategoryButton" Text="Remove" CommandName="DeleteRequestedItem" CommandArgument='<%#Eval("RequestedUpgradeID") %>'   /> </td>
                                <td><%#Eval("Category")%></td>
                                <td><%#Eval("UpgradeLevel")%></td>
                                <td><%#Eval("Description")%></td>
                                <td><%#Eval("Style")%></td>
                                <td><%#Eval("Cost")%></td>
                            </tr>
                        </ItemTemplate>
                          <SelectedItemTemplate>
                                <td style="width:40px"><asp:LinkButton runat="server" ID="SelectCategoryButton" Text="Remove" CommandName="DeleteRequestedItem" CommandArgument='<%#Eval("UpgradeOptionID") %>'    /> </td>
                                <td><%#Eval("Category")%></td>
                                <td><%#Eval("UpgradeLevel")%></td>
                                <td><%#Eval("Description")%></td>
                                <td><%#Eval("Style")%></td>
                                <td><%#Eval("Cost")%></td>
                            </SelectedItemTemplate>
                        </asp:ListView>
                        </asp:Panel>       
                        <br />
                    </ContentTemplate>
                   <Triggers >  
                       <asp:AsyncPostBackTrigger ControlID="lstRooms"   EventName="SelectedIndexChanged"  />
                        <asp:AsyncPostBackTrigger ControlID="lstStyle"   EventName="ItemCommand"  />
                       <asp:AsyncPostBackTrigger ControlID="lstRequestedUpgrades" 
                           EventName="ItemCommand" />
                       <asp:AsyncPostBackTrigger ControlID="lstSelectedUpgrade" 
                           EventName="ItemCommand" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updpnlStyle">
                        <ProgressTemplate>            <asp:Image ID="ImageRequestedStyle" runat="server" ImageUrl="~/images/loading.gif" />            </ProgressTemplate>
                </asp:UpdateProgress>   
        </td>
    </tr>
</table>


    <asp:ObjectDataSource ID="odsCategories" runat="server" 
        SelectMethod="LoadRoomCategories" TypeName="clsSelectDataLoader">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID" 
                Type="Int64" />
            <asp:SessionParameter DefaultValue="R134A" Name="lsUnitType" 
                SessionField="UnitType" Type="String" />
            <asp:ControlParameter ControlID="rblPhase" DefaultValue="1" Name="llPhase" 
                PropertyName="SelectedValue" Type="Int64" />
            <asp:ControlParameter ControlID="lstRooms" DefaultValue="" Name="lsRoom" 
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="odsRooms" runat="server" SelectMethod="LoadUnitRooms" 
        TypeName="clsSelectDataLoader">
        <SelectParameters>
            <asp:Parameter Name="llNodeID" Type="Int64" DefaultValue="1" />
            <asp:Parameter DefaultValue="" Name="lsWhere" Type="String" />
            <asp:Parameter Name="lbActive" Type="Boolean" />
            <asp:Parameter DefaultValue="1" Name="llID" Type="Int64" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="odsQuotes" runat="server" 
                SelectMethod="LoadQuotes" TypeName="clsSelectDataLoader">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID"      Type="Int64" />
                    <asp:Parameter DefaultValue="" Name="lsWhere" Type="String" />
                    <asp:Parameter DefaultValue="True" Name="lbActive" Type="Boolean" />
                    <asp:SessionParameter DefaultValue="1" Name="llID" SessionField="QuoteID"  Type="Int64" />
                </SelectParameters>
            </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="odsQuoteStatus" runat="server" 
                SelectMethod="LoadLookups" TypeName="clsSelectDataLoader">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID"      Type="Int64" />
                    <asp:Parameter DefaultValue="&quot;&quot;" Name="lsWhere" Type="String" />
                    <asp:Parameter DefaultValue="QuoteStatus" Name="lsLookupType" Type="String" />
                    <asp:Parameter DefaultValue="True" Name="lbActive" Type="Boolean" />
                    <asp:Parameter DefaultValue="0" Name="llID" Type="Int64" />
                </SelectParameters>
            </asp:ObjectDataSource>
            
               <asp:ObjectDataSource ID="odsPhase1Status" runat="server" 
                SelectMethod="LoadLookups" TypeName="clsSelectDataLoader">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID"      Type="Int64" />
                    <asp:Parameter DefaultValue="" Name="lsWhere" Type="String" />
                    <asp:Parameter DefaultValue="Phase1Status" Name="lsLookupType" Type="String" />
                    <asp:Parameter DefaultValue="True" Name="lbActive" Type="Boolean" />
                    <asp:Parameter DefaultValue="0" Name="llID" Type="Int64" />
                </SelectParameters>
            </asp:ObjectDataSource>
            
            <asp:ObjectDataSource ID="odsPhase2Status" runat="server" 
                SelectMethod="LoadLookups" TypeName="clsSelectDataLoader">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID" Type="Int64" />
                    <asp:Parameter DefaultValue="" Name="lsWhere" Type="String" />
                    <asp:Parameter DefaultValue="Phase2Status" Name="lsLookupType" Type="String" />
                    <asp:Parameter DefaultValue="True" Name="lbActive" Type="Boolean" />
                    <asp:Parameter DefaultValue="0" Name="llID" Type="Int64" />
                </SelectParameters>
            </asp:ObjectDataSource>
            
    <asp:ObjectDataSource ID="odsLevels" runat="server" 
        SelectMethod="LoadRoomCategoryLevels" TypeName="clsSelectDataLoader">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID" 
                Type="Int64" />
            <asp:SessionParameter DefaultValue="R134A" Name="lsUnitType" 
                SessionField="UnitType" Type="String" />
            <asp:ControlParameter ControlID="rblPhase" DefaultValue="1" Name="llPhase" 
                PropertyName="SelectedValue" Type="Int64" />
            <asp:ControlParameter ControlID="lstRooms" DefaultValue="" Name="lsRoom" 
                PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="lstClasses" Name="lsCategory" 
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
        <asp:ObjectDataSource ID="odsStyle" runat="server" 
        SelectMethod="LoadRoomCategoryLevelStyles" TypeName="clsSelectDataLoader">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID" 
                Type="Int64" />
            <asp:SessionParameter DefaultValue="R134A" Name="lsUnitType" 
                SessionField="UnitType" Type="String" />
            <asp:ControlParameter ControlID="rblPhase" DefaultValue="1" Name="llPhase" 
                PropertyName="SelectedValue" Type="Int64" />
            <asp:ControlParameter ControlID="lstRooms" DefaultValue="Bath 1" Name="lsRoom" 
                PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="lstClasses" DefaultValue="Bath Tub" Name="lsCategory" 
                PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="lstUpgradesAvail" Name="lsLevel" 
                PropertyName="SelectedValue" Type="String" DefaultValue="Options" />
            <asp:Parameter Name="llID" Type="Int64" />
        </SelectParameters>
    </asp:ObjectDataSource>
<asp:Literal ID="litMsg" runat="server" />
        <asp:ObjectDataSource ID="odsRequestedUpgrade" runat="server" 
        SelectMethod="LoadRequestedUpgrades" TypeName="clsSelectDataLoader" 
        DeleteMethod="DeleteRequestedUpgrades" 
        InsertMethod="InsertRequestedUpgrades">
            <DeleteParameters>
                <asp:Parameter Name="RecordID" Type="Int64" />
            </DeleteParameters>
        <SelectParameters>
            <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID" Type="Int64" />
            <asp:ControlParameter ControlID="lstRooms" DefaultValue="Bath 1" Name="lsRoom"  
                PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="rblPhase" DefaultValue="0" Name="lsPhase" 
                PropertyName="SelectedValue" Type="String" />
            <asp:SessionParameter DefaultValue="1" Name="lsQuoteID" 
                SessionField="QuoteID" Type="String" />
            <asp:Parameter Name="lsWhere" Type="String" />
            <asp:Parameter Name="lbActive" Type="Boolean" />
            <asp:Parameter Name="llID" Type="Int64" />
                <asp:ControlParameter ControlID="lstClasses" Name="lsCat" 
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
            <InsertParameters>
                <asp:Parameter Name="llNodeID" Type="Int64" />
                <asp:Parameter Name="llQuoteID" Type="Int64" />
                <asp:Parameter Name="lsRoomDesc" Type="String" />
                <asp:Parameter Name="lsUpgradeDesc" Type="String" />
                <asp:Parameter Name="lsUpgradeCategory" Type="String" />
                <asp:Parameter Name="lsPhase" Type="String" />
                <asp:Parameter Name="lsUpgradeClass" Type="String" />
                <asp:Parameter Name="lsStyle" Type="String" />
                <asp:Parameter Name="lsPrice" Type="String" />
                <asp:Parameter Name="lsTypeHold" Type="String" />
                <asp:Parameter Name="llUpgradeOptionID" Type="Int64" />
            </InsertParameters>
    </asp:ObjectDataSource>
    
    
        <asp:ObjectDataSource ID="odsRequestedUpgrades" runat="server" 
        SelectMethod="LoadRequestedUpgrades" TypeName="clsSelectDataLoader" 
        DeleteMethod="DeleteRequestedUpgrades" InsertMethod="InsertRequestedUpgrades">
            <DeleteParameters>
                <asp:Parameter Name="RecordID" Type="Int64" />
            </DeleteParameters>
        <SelectParameters>
            <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID" Type="Int64" />
            <asp:ControlParameter ControlID="lstRooms" DefaultValue="Bath 1" Name="lsRoom"  
                PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="rblPhase" DefaultValue="0" Name="lsPhase" 
                PropertyName="SelectedValue" Type="String" />
            <asp:SessionParameter DefaultValue="1" Name="lsQuoteID" 
                SessionField="QuoteID" Type="String" />
            <asp:Parameter Name="lsWhere" Type="String" />
            <asp:Parameter Name="lbActive" Type="Boolean" />
            <asp:Parameter Name="llID" Type="Int64" />
               <asp:Parameter Name="lsCat" Type="String" />
        </SelectParameters>
            <InsertParameters>
                <asp:Parameter Name="llNodeID" Type="Int64" />
                <asp:Parameter Name="llQuoteID" Type="Int64" />
                <asp:Parameter Name="lsRoomDesc" Type="String" />
                <asp:Parameter Name="lsUpgradeDesc" Type="String" />
                <asp:Parameter Name="lsUpgradeCategory" Type="String" />
                <asp:Parameter Name="lsPhase" Type="String" />
                <asp:Parameter Name="lsUpgradeClass" Type="String" />
                <asp:Parameter Name="lsStyle" Type="String" />
                <asp:Parameter Name="lsPrice" Type="String" />
                <asp:Parameter Name="lsTypeHold" Type="String" />
                <asp:Parameter Name="llUpgradeOptionID" Type="Int64" />
            </InsertParameters>
    </asp:ObjectDataSource>

  <asp:ObjectDataSource ID ="odsMissing" runat="server" SelectMethod="LoadMissingUpgrades" TypeName="clsSelectDataLoader">
 <SelectParameters>
            <asp:SessionParameter DefaultValue="1" Name="lsQuoteID" 
                SessionField="QuoteID" Type="String" />
 </SelectParameters> 
  </asp:ObjectDataSource>
  
</asp:Content>
