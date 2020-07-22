<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlGoogleChartPie.ascx.vb" Inherits="CtrlGoogleChartPie" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
       <asp:DropDownList ID="DropDownList1" AutoPostBack="true" runat="server"
           >
        </asp:DropDownList>
<asp:Chart ID="Chart1" runat="server" DataSourceID="ObjectDataSource1">
    <Series>
        <asp:Series Name="Series1" ChartType="Pie"></asp:Series>
    </Series>
    <ChartAreas>
        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
    </ChartAreas>
</asp:Chart>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="LoadAdhoc" TypeName="clsSelectDataLoader">
    <SelectParameters>
        <asp:Parameter Name="llNodeID" Type="Int64" />
        <asp:ControlParameter ControlID="txtSQL" Name="lsSQL" PropertyName="Text" Type="String" />
        <asp:Parameter Name="lbActive" Type="Boolean" />
        <asp:Parameter Name="lsID" Type="String" />
        <asp:Parameter Name="lsObjectID" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:TextBox ID="txtSQL" runat="server" Height="36px" Width="327px"></asp:TextBox>

