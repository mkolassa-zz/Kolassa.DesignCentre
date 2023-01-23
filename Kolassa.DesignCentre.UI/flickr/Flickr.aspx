<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"   %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Styles/colorbox.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="scripts/jquery.min.js"></script>
    <script type="text/javascript" src="scripts/jquery.colorbox-min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("a[rel='images']").colorbox({ transition: "fade" });
        });

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                $("a[rel='images']").colorbox({ transition: "fade" });
            }
        }
    </script>


    <p>
        List of default user sets:
        <br />
        <asp:DropDownList ID="ddlSets" runat="server" AutoPostBack="True" DataSourceID="odsSets"
            DataTextField="Title" DataValueField="PhotosetId" Height="21px" Width="450px"
            OnSelectedIndexChanged="ddlSets_SelectedIndexChanged">
        </asp:DropDownList>
    </p>
    <p><asp:FileUpload ID="FileUpload1" runat="server"  />
                <asp:Button ID="cmdUpload" runat="server" OnClick="Button1_Click" Text="Upload" />
        <asp:Label ID="lblMessage" runat="server" />
        <asp:UpdatePanel ID="upImages" runat="server">
            <ContentTemplate>
                
                <asp:ListView ID="lvImages" runat="server" DataSourceID="odsPhotos">
                    <EmptyDataTemplate>
                        <span>No data was returned.</span>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <a href="<%# Eval("MediumUrl") %>" rel="images" title="<%# Eval("Title") %>">
                            <img alt="" src="<%# Eval("SquareThumbnailUrl") %>" /></a>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <div id="itemPlaceholderContainer" runat="server" style="">
                            <span runat="server" id="itemPlaceholder" />
                        </div>
                        <div style="">
                            <asp:DataPager ID="DataPager1" runat="server" PageSize="<%$ appSettings:defaultPageSize %>">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Image" ShowFirstPageButton="true" ShowNextPageButton="false"
                                        ShowPreviousPageButton="true" FirstPageImageUrl="~/images/first.gif" PreviousPageImageUrl="~/images/previous.gif" />
                                    <asp:TemplatePagerField>
                                        <PagerTemplate>
                                            Page
                                            <asp:Label runat="server" ID="labelCurrentPage" Text="<%# Container.TotalRowCount > 0 ? (Container.StartRowIndex / Container.PageSize) + 1 : 0 %>" />
                                            of
                                            <asp:Label runat="server" ID="labelTotalPages" Text="<%#  Math.Ceiling ((double)Container.TotalRowCount / Container.PageSize) %>" />
                                        </PagerTemplate>
                                    </asp:TemplatePagerField>
                                    <asp:NextPreviousPagerField ButtonType="Image" ShowLastPageButton="true" ShowNextPageButton="true"
                                        ShowPreviousPageButton="false" LastPageImageUrl="~/images/last.gif" NextPageImageUrl="~/images/next.gif" />
                                    <asp:TemplatePagerField>
                                        <PagerTemplate>
                                            <br />
                                            Total Pictures in this set:
                                            <asp:Label runat="server" ID="labelTotalPictures" Text="<%#  (double)Container.TotalRowCount %>" />
                                        </PagerTemplate>
                                    </asp:TemplatePagerField>
                                </Fields>
                            </asp:DataPager>
                        </div>
                    </LayoutTemplate>
                </asp:ListView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlSets" />
            </Triggers>
        </asp:UpdatePanel>
    </p>
    <asp:ObjectDataSource ID="odsSets" runat="server" OldValuesParameterFormatString="original_{0}"
        OnSelecting="odsSets_Selecting" SelectMethod="GetPhotoSetsByUser" TypeName="Infrastructure.BLL.FlickrBLL">
        <SelectParameters>
            <asp:Parameter Name="userId" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsPhotos" runat="server" EnablePaging="True" OldValuesParameterFormatString="original_{0}"
        SelectCountMethod="GetPagedSetCount" SelectMethod="GetPagedSet" TypeName="Infrastructure.BLL.FlickrBLL"
        OnSelecting="odsPhotos_Selecting">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlSets" DefaultValue="0" Name="setId" PropertyName="SelectedValue"
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
