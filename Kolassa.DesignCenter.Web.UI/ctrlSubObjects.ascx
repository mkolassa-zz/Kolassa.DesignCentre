<%@ Control Language="vb" AutoEventWireup="false"  CodeBehind="ctrlSubObjects.ascx.vb" Inherits="ctrlSubObjects" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Src="~/ctrlContacts.ascx" TagPrefix="uc1" TagName="ctrlContacts" %>
<%@ Register Src="~/ctrlCustomers.ascx" TagPrefix="uc1" TagName="ctrlCustomers" %>
<%@ Register Src="~/ctrlImages.ascx" TagPrefix="uc1" TagName="ctrlImages" %>




<telerik:radtabstrip RenderMode="Lightweight" ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1" Width="100%" Align="Justify"
OnTabClick="RadTabStrip1_TabClick" AutoPostBack="True" SelectedIndex="0" >
<Tabs>
<telerik:RadTab PageViewID="rpvDetails" Text="Details" ImageUrl="images/details.png" Selected="True" />
<telerik:RadTab PageViewID="rpvContacts" Text="Contacts" ImageUrl="images/Contacts.png" />
<telerik:RadTab PageViewID="rpvImages" Text="Images" ImageUrl="images/Images.png" Selected="True" />
</Tabs>
</telerik:radtabstrip>
       
<telerik:radmultipage ID="RadMultiPage1" CssClass="RadMultiPage" runat="server" Width="718">
    <telerik:RadPageView ID="RadPageView1" runat="server" CssClass="pageView1" Selected="true">
        <div class="details">
            <asp:Label ID="lblSelectedTab" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
            <uc1:ctrlCustomers runat="server" ID="ctrlCustomers" />
        </div>  
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView2" runat="server" CssClass="pageView2">
        <uc1:ctrlContacts runat="server" ID="ctrlContacts" />
    </telerik:RadPageView>
    <telerik:RadPageView ID="RadPageView3" runat="server">
        <uc1:ctrlImages runat="server" ID="ctrlImages" />
    </telerik:RadPageView>
</telerik:radmultipage>
<telerik:RadComboBox ID="RadComboBox1" Runat="server">
</telerik:RadComboBox>

  <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server">
        <div class="demo-container no-bg size-narrow">
            <h2>TabStrip representing different types of alignment.</h2>
            <telerik:RadTabStrip RenderMode="Lightweight" runat="server" ID="RadTabStrip3"  SelectedIndex="0" Skin="Default">
                <Tabs>
                    <telerik:RadTab Text="Products">
                        <Tabs>
                            <telerik:RadTab Text="Telerik UI for ASP.NET">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Telerik UI for WinForms">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Telerik Reporting">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Sitefinity CMS">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTab>
                    <telerik:RadTab Text="Services">
                        <Tabs>
                            <telerik:RadTab Text="Enterprise Services">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Enterprise Account Manager">
                            </telerik:RadTab>
                            <telerik:RadTab Text="On-site Training">
                            </telerik:RadTab>
                            <telerik:RadTab Text="GoToMeeting Sessions">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTab>
                    <telerik:RadTab Text="Corporate">
                        <Tabs>
                            <telerik:RadTab Text="Corporate Info">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Press Center">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Customers">
                            </telerik:RadTab>
                            <telerik:RadTab Text="Awards">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
        </div>
    </telerik:RadAjaxPanel>

<telerik:radajaxloadingpanel ID="LoadingPanel1" runat="server" />

