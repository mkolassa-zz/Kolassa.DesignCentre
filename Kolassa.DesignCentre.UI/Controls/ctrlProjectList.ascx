<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlProjectList.ascx.vb" Inherits="Kolassa.DesignCentre.UI.ctrlProjectList" %>
          <div id="test">
			  <asp:DataList ID="dlProject" runat="server" DataSourceID="odsProjects" DataKeyField="ID" >
				  <ItemTemplate>
                        <table class="table" >
							<% if InStr(Page.AppRelativeVirtualPath.ToString.ToUpper, "NEWQUOTE") > 0 Then %>
								<tr><td style="font-weight:700;"><a href='<%="../NewQuote.aspx/?ProjectID="%><%#Eval("ID") %>'><%#Eval("Name") %></a>
							<% else %>
									<tr><td style="font-weight:700;"><a href='<%="../frmProjects.aspx?ProjectID="%><%#Eval("ID") %>'><%#Eval("Name") %></a>
							<% End If %>
                            
                            <br /><%#Eval("Description") %>
                            <br /><%#Eval("ProjectTYpeName") %>
                               <br />
                 
                                </td>
                        </table>
				  </ItemTemplate>
			  </asp:DataList>
         
                    <!-- uc1 :ctrlImages ru nat="ser ver" i d="ctr lImages" / -->
                </div>
                
                <asp:ObjectDataSource ID="odsProjects" runat="server" 
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
            <asp:SessionParameter DefaultValue="2" Name="llNodeID" SessionField="NodeID" Type="Int64" />
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