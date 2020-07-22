<%@ Control Language="vb" AutoEventWireup="false"  CodeBehind="ctrlImages.ascx.vb"  Inherits="Kolassa.DesignCentre.UI.ctrlImages"  %>
<!-- % @  Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" % !-->


<%@ Register Src="~/Controls/ctrlImageNew.ascx" TagPrefix="uc1" TagName="ctrlImageNew" %>

<style  >
    .ctrlImage img
   {
    border: 1px solid #ddd;
    border-radius: 4px;
    padding: 5px;
    width: 150px;
   }

   ctrlImage img:hover {
    box-shadow: 0 0 2px 1px rgba(0, 140, 186, 0.5);
}

    .rounded_corners
    {
        border: 1px solid #A1DCF2;
        -webkit-border-radius: 8px;
        -moz-border-radius: 8px;
        border-radius: 8px;
        overflow: hidden;
    }
    .rounded_corners td, .rounded_corners th
    {
        border: 1px solid #A1DCF2;
        font-family: Arial;
        font-size: 10pt;
        text-align: center;
    }
    .rounded_corners table table td
    {
        border-style: none;
    }
</style>



<asp:ObjectDataSource ID="odsImages" runat="server" SelectMethod="LoadImages" TypeName="Kolassa.DesignCentre.Data.clsSelectDataLoader" 
	DeleteMethod="DeleteImages" InsertMethod="InsertImages" OldValuesParameterFormatString="original_{0}" UpdateMethod="Updateimages">
    <DeleteParameters>
        <asp:Parameter Name="RecordID" Type="String" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="lsObjectID" Type="String" />
        <asp:Parameter Name="llNodeID" Type="Int64" />
        <asp:Parameter Name="lsName" Type="String" />
        <asp:Parameter Name="lsDescription" Type="String" />
        <asp:Parameter Name="liOrder" Type="Int32" />
        <asp:Parameter Name="lsImage" Type="Object" />
        <asp:Parameter Name="lsType" Type="String" />
        <asp:Parameter Name="lsURL" Type="String" />
    </InsertParameters>
    <SelectParameters>
        <asp:Parameter DefaultValue="12121212-1212-1212-1212-121212121212" Name="lsObjectID" Type="String" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="lsObjectID" Type="String" />
        <asp:Parameter Name="llNodeID" Type="Int64" />
        <asp:Parameter Name="lsName" Type="String" />
        <asp:Parameter Name="lsDescription" Type="String" />
        <asp:Parameter Name="lsImage" Type="String" />
        <asp:Parameter Name="lsImageURL" Type="String" />
        <asp:Parameter Name="lsActive" Type="String" />
        <asp:Parameter Name="ID" Type="String" />
        <asp:Parameter Name="liOrder" Type="Int32" />
    </UpdateParameters>
</asp:ObjectDataSource>


    <div class="container">
        <h2>Images</h2>
        <!-- Trigger the modal with a button -->
        <button type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#myModal">Open Modal</button>

        <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Add New Image</h4>
                    </div>
                    <div class="modal-body">
                        <p>Select an Image.</p> 
						<uc1:ctrlImageNew ID="asdf" runat="server" />
                        <uc1:ctrlImageNew ID="ctrlImageNew1" runat="server"  /> 
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                    </div>
                </div>

            </div>
        </div>
</div>


<table >
    <tr><td style="width:100px;"></td><td></td></tr>
        <tr><td></td><td>
            <asp:GridView ID="GridView1" runat="server" DataSourceID="odsImages" CssClass="table table-striped table-bordered table-hover glyphicon-hover" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText ="Picture Name" />
                    <asp:BoundField DataField="Description" HeaderText ="Picture Desc" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a href="<%#"data:" + Trim(Eval("Type")) + ";base64," + Convert.ToBase64String(Eval("Image")) %>">
                                <asp:Image CssClass="ctrlImage" ID="Image1" runat="server" Height="100px" ImageUrl='<%#"data:" + Trim(Eval("Type")) + ";base64," + Convert.ToBase64String(Eval("Image")) %>' />
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    No Images Found.
                </EmptyDataTemplate>
            </asp:GridView>

            </td></tr>

</table>
      

            


