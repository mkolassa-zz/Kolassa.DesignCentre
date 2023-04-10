<%@ Control Language="vb" AutoEventWireup="false"  CodeBehind="ctrlImageNew.ascx.vb" Inherits="Kolassa.DesignCentre.UI.ctrlImagesNew"  %>
    
    <asp:Panel ID="pnlURL" runat="server"> 
        <br />Image URL:<br />
        <asp:TextBox ID="txtImageURL" MaxLength="250" runat="server" Width="100%" Height="51px"></asp:TextBox>
        <br /><br />
        <asp:LinkButton ID="lnkbSaveImageURL" CssClass="btn btn-primary" runat="server"><i class="fa fa-save"></i> Save URL</asp:LinkButton>
    </asp:Panel>

    <asp:TextBox ID ="txtfileupload" runat="server" Text="Bob" />
    <asp:FileUpload ID="fu" runat="server"  EnableViewState="true"/>
    <asp:Button ID="cmdfu" runat="server" Text="File Upload" />
    <div>
         <asp:UpdatePanel runat="server" ID="upFileUploader">
            <ContentTemplate><a id="showUploadid" href="#" onclick="$('#divIDs').toggle();" style="font-size:xx-small">Current Item</a>
                <div id="divID" style="display:none;">
                    Node: <asp:Literal ID ="litNode" runat="server"></asp:Literal>   objectID: <asp:Literal ID ="litobject" runat="server" ViewStateMode="Enabled"></asp:Literal>
                    <asp:TextBox ID="TextBox1" MaxLength="30" runat="server" Width="226px" CssClass="form-control-plaintext"></asp:TextBox>
                    <asp:Label ID="lblObjectID" runat="server"  /><br/> 
                    <asp:TextBox ID="txtDescription" MaxLength="30" runat="server" Width="226px" CssClass="d-none"></asp:TextBox>
                </div>    
                <ajaxToolkit:AjaxFileUpload ID="AjaxFileUpload1" runat="server" 
                    OnUploadCompleteAll ="UploadCompleteAll" MaximumNumberOfFiles="5" AllowedFileTypes="png,jpeg,jpg,gif"
                    OnUploadComplete="uploadcomplete"/> 
            </ContentTemplate> 
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnMessage" EventName="Click" />
        </Triggers>    
        </asp:UpdatePanel>

        <div id="divMessage">
            <asp:Literal ID="litMessage" runat="server" Text="Message" />
            <asp:Button ID ="btnMessage" runat="server" Text="message"  />
            <asp:HiddenField ID="hdMessage" runat="server"  />
        </div>

        <asp:UpdatePanel runat="server" ID="upExisting">
            <ContentTemplate>
                <div id="divIDs" style="display:block;">
                    Node: <asp:Literal ID ="Literal1" runat="server"></asp:Literal>   objectID: <asp:Literal ID ="Literal2" runat="server" ViewStateMode="Enabled"></asp:Literal>
                    <asp:TextBox ID="TextBox2" MaxLength="30" runat="server" Width="226px" CssClass="form-control-plaintext"></asp:TextBox>
                </div>        
            </ContentTemplate> 
        </asp:UpdatePanel>
    </div>               


