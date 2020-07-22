<%@ Control Language="VB" AutoEventWireup="false" Inherits="Select2017.ReportManager_ctrlDynamicDataGrid" Codebehind="ctrlDynamicDataGrid.ascx.vb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

   
  <asp:GridView ID="TableGridView"  
      OnRowEditing ="TableGridView_RowEditing" 
      OnRowCancelingEdit="TableGridView_RowCancelingEdit" 
      OnRowUpdating="TableGridView_RowUpdating"
      OnRowDeleting="TableGridView_RowDeleting"
      OnPageIndexChanging="TableGridView_PageIndexChanging"      
      runat="server"  AutoGenerateColumns="False" AllowPaging="True" 
    AllowSorting="True"  >
    
 </asp:GridView>
 
 
     
        
        <asp:Panel ID="MsgPanel"  runat="server" Height="151px" Width="443px" Visible="False">

            <asp:Label ID="msg_lbl" runat="server" Text="" style="left: 6px; position: absolute; top: 3px" ></asp:Label>

        <asp:LinkButton ID="msg_button" runat="server" OnClick="msg_button_Click" style="">Hide Message</asp:LinkButton>
        </asp:Panel>
       
  