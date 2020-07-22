<%@ Control Language="VB" AutoEventWireup="false" Inherits="Select2017.ReportManager_DynamicGrid" Codebehind="ctrlDynamicGrid.ascx.vb" %>
<%@ Register src="ReportContainer.ascx" tagname="ReportContainer" tagprefix="uc1" %>
<%@ Register Src="ctrlBase.ascx" TagName="ctrlBase" TagPrefix="uc1" %>
<%@ Register Src="ctrlTextBox.ascx" TagName="ctrlTextBox" TagPrefix="uc2" %>
<%@ Reference Control="ctrlTextBox.ascx" %>

<link href="css/report.css" rel="stylesheet" type="text/css" />
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>



<div class="photo-frame" >
    <br />
  


        <asp:GridView ID="grdMain" runat="server" Visible="False" AutoGenerateColumns="False"
        AllowPaging="True" AllowSorting="True"  PageSize="50" BackColor="White" BorderColor="#DEDFDE" 
        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"  
        GridLines="Vertical" AutoGenerateEditButton="True">
        <FooterStyle BackColor="#CCCC99" />
        <RowStyle BackColor="#F7F7DE" />
        <Columns>
         
        </Columns>
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    


    <asp:Literal ID="litMsg" runat="server"></asp:Literal>
    
<br />
<div>
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Query:"></asp:Label><br />
        <asp:TextBox ID="txtQuery" runat="server" Height="111px" TextMode="MultiLine" Width="706px"></asp:TextBox><br />
        &nbsp;<asp:Button ID="btnDisplay" runat="server" Text="Display Results" OnClick="btnDisplay_Click" />
    
    </div>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
    SelectMethod="LoadCustomers" TypeName="clsSelectDataLoader">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID" 
                Type="Int64" />
            <asp:ControlParameter ControlID="ReportContainer1" Name="lsWhere" 
                PropertyName="WhereClause" Type="String" />
        </SelectParameters>
</asp:ObjectDataSource>

 </div>

