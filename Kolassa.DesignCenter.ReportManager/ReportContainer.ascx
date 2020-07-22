<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ReportContainer.ascx.vb" Inherits="Select2017.ReportContainer" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="ctrlBase.ascx" TagName="ctrlBase" TagPrefix="uc1" %>
<%@ Register Src="ctrlTextBox.ascx" TagName="ctrlTextBox" TagPrefix="uc2" %>
<%@ Reference Control="ctrlTextBox.ascx" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>
<!-- link href="css/report.css" rel="stylesheet" type="text/css" / -->
<br />
<table id=tblReportContainer >
    <tr id=firstrow>
        <td style="vertical-align:top; width:0px" class="criteria" >
            <asp:DropDownList ID="cboReportCategory" runat="server" Width="256px" AutoPostBack="True">
            </asp:DropDownList>
            <asp:UpdatePanel id="UpdatePanel1" runat="server">
                <contenttemplate>
            <asp:ListBox ID="lstReports" runat="server" Height="232px" Width="256px" AutoPostBack="True"></asp:ListBox>
            </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="cboReportCategory" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
            <asp:TextBox ID="txtDebug" runat="server" Width="256px"></asp:TextBox>&nbsp;
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Literal ID="Litmsg" runat="server"></asp:Literal>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="vertical-align:top;">
            <asp:UpdatePanel ID="up2" runat="server">
                <ContentTemplate>
                    <asp:Table ID="tbl" runat="server" Height="40px" Width="136px">
                        <asp:TableRow ID="tblrow" runat="server">
                            <asp:TableCell ID="tblcell" runat="server"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lstReports" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
            &nbsp; &nbsp;


            <asp:UpdateProgress id="UpdateProgress1" runat="server" >
                <progresstemplate >
                    <div id="OuterTableCellOverlay">
                        <div id="InnerTableCellOverlay">
                            <b>... Please Wait ...</b>                              
                            <asp:Image ID="LoadImage" runat="server" ImageUrl="images/loading.gif" />
                         </div>
                    </div>
                </progresstemplate>
            </asp:UpdateProgress>

          
    </tr>
</table>
<br />
