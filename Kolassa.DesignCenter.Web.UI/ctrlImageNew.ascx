<%@ Control Language="vb" AutoEventWireup="false" Inherits="Kolassa.DesignCenter.Web.UI.ctrlImagesNew" Codebehind="ctrlImageNew.ascx.vb" %>

<div  >
    <br />Image Description:<br />
    <asp:TextBox ID="txtDescription" MaxLength="30" runat="server" Width="226px"></asp:TextBox>

    <br />

    <br />

    <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Please Select File" />
    <br />
    <asp:Button class="btn btn-info btn-lg" ID="btnUpload"  data-dismiss="modal" runat="server" Text="Upload" />


</div>
      
                  


