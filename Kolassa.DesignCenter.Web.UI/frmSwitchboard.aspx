<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.master" Inherits="Kolassa.DesignCenter.Web.UI.frmSwitchboard" Codebehind="frmSwitchboard.aspx.vb" %>

<%@ Register src="ReportManager/ctrlDateBox.ascx" tagname="ctrlDateBox" tagprefix="uc1" %>

<asp:Content runat=server ContentPlaceHolderId=Main>
   <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <asp:Button ID="cmdSave"        runat="server" Text="Save" />
    <asp:Button ID="Button1" runat="server" Text="Button" />
    <br/>
    <asp:CheckBox ID="chkMaster"    runat="server" Text="Master Bedroom"    /><br/>
    <asp:CheckBox ID="chkBed1"      runat="server" Text="Bed 1 (Queen Bed)" /><br/>
    <asp:CheckBox ID="ChkBed2"      runat="server" Text="Bed 2 (2 Twins)"   /><br/>
    <asp:CheckBox ID="chkExclusive" runat="server" Text="Exclusive"   /><br/>
    <asp:Literal  ID="litSubject"   runat="server" Text="Subject" />
    <asp:TextBox  ID="txtSubject"   runat="server" />
<link href="css/report.css" rel="stylesheet" type="text/css" />
<style>
.calendarHide
  {
	display: none;
	position: relative;
  }
.calendarShow
  {
	position: absolute;
	z-index: 0;
	background-color: Silver;
  }
</style>


     
            <asp:TextBox ID="ctrlField1"  runat="server" Width="80px"></asp:TextBox> 
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/calendar.gif" OnClick="ImageButton1_Click" /> 
            <!-- cssclass="w8em format-m-d-y highlight-days-67" -->
            <asp:Panel Runat="server" ID="Panel1" CssClass="calendarHide" Visible = false>
				<asp:Calendar id="Calendar1" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="176px" NextPrevFormat="ShortMonth" Width="168px" OnSelectionChanged="Calendar1_SelectionChanged" >
                    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                    <TodayDayStyle BackColor="#999999" ForeColor="White" />
                    <DayStyle BackColor="#CCCCCC" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                    <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt"
                        ForeColor="White" Height="12pt" />
                </asp:Calendar>
			</asp:Panel>
            
            
            <asp:Literal runat=server ID=litmsg />
            <asp:Label ID="lblField2"  runat="server" Text="Label"></asp:Label>
            <asp:TextBox ID="ctrlField2"  runat="server" Width="88px"></asp:TextBox>&nbsp;
            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/calendar.gif" OnClick="ImageButton2_Click" />
            
            <asp:Panel Runat="server" ID="Panel2" CssClass="calendarHide" Visible = false>
				<asp:Calendar id="Calendar2" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="56px" NextPrevFormat="ShortMonth" Width="48px" OnSelectionChanged="Calendar2_SelectionChanged" >
                    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                    <TodayDayStyle BackColor="#999999" ForeColor="White" />
                    <DayStyle BackColor="#CCCCCC" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                    <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt"
                        ForeColor="White" Height="12pt" />
                </asp:Calendar>
			</asp:Panel>
            <br />
  
            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="CustomValidator"></asp:CustomValidator>&nbsp;&nbsp;
        
    <br/>
    
    <iframe src="http://www.google.com/calendar/embed?height=600&amp;wkst=1&amp;bgcolor=%23FFFFFF&amp;src=mkolassa%40gmail.com&amp;color=%232952A3&amp;ctz=Pacific%2FApia" style=" border-width:0 " width="800" height="600" frameborder="0" scrolling="no"></iframe>

    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    <asp:Button ID="Button2" runat="server" Text="Button" />

</asp:Content>