<%@ Control Language="vb" AutoEventWireup="false"  CodeBehind="ctrlImageNew.ascx.vb" Inherits="Kolassa.DesignCentre.UI.ctrlImagesNew" %>

    
    <asp:Panel ID="pnlURL" runat="server"> 
        <br />Image URL:<br />
        <asp:TextBox ID="txtImageURL" MaxLength="250" runat="server" Width="100%" Height="51px"></asp:TextBox><br />
        <asp:LinkButton ID="lnkbSave" runat="server"><i class="fa fa-save"></i> Save URL</asp:LinkButton>
    </asp:Panel>



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
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="upExisting">
        <ContentTemplate>
            <div id="divIDs" style="display:block;">
                
                Node: <asp:Literal ID ="Literal1" runat="server"></asp:Literal>   objectID: <asp:Literal ID ="Literal2" runat="server" ViewStateMode="Enabled"></asp:Literal>
                <asp:TextBox ID="TextBox2" MaxLength="30" runat="server" Width="226px" CssClass="form-control-plaintext"></asp:TextBox>
            </div>        
        </ContentTemplate> 
    </asp:UpdatePanel>
</div>               


