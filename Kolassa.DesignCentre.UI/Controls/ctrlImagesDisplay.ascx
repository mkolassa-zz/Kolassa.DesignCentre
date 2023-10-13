<%@ Control Language="vb" AutoEventWireup="false"  CodeBehind="ctrlImagesDisplay.ascx.vb"  Inherits="Kolassa.DesignCentre.UI.ctrlImagesDisplay"  %>

<asp:ObjectDataSource ID="odsImages" runat="server" SelectMethod="LoadImages" TypeName="Kolassa.DesignCentre.Data.clsSelectDataLoader" >
    <SelectParameters>
        <asp:Parameter DefaultValue="12121212-1212-1212-1212-121212121212" Name="lsObjectID" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
<div class="d-none">
    <asp:textbox Visible="true" ID="txtImageObjectID" runat="server" />
    <asp:Button runat="server" id="btnGo" name="btnImageGo"  title="refresh" Text="Refresh" />
    <button class="btn btnrefresh" onclick="  __doPostBack('<%= upImages.UniqueID %>', '');">HTML Refresh</button>  <asp:Button ID="Button1" runat="server" Text="Button" />
</div>
<asp:UpdatePanel ID ="upImages" runat="server" >
    <ContentTemplate>
   

            <div id="carouselInd" class="carousel slide" data-ride="carousel">
                <ol class="carousel-indicators">
                    <asp:Repeater runat="server" ID="rptInd"   >
                        <ItemTemplate>
                        <li data-target="#carouselInd" data-slide-to="<%# Container.ItemIndex %>"></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ol>
                <div class="carousel-inner small d-flex  bg-secondary" style="min-height:300px;">
                    <asp:Repeater runat="server" ID="rptImages"   >
                        <ItemTemplate>
                            <div class="carousel-item <%# If(Container.ItemIndex = 0, " active ", "") %>">
                                <div class="d-flex justify-content-center w-100 h-100">
                                <img class="d-block" style="max-height:300px;" src="<%#Eval("ImageURL") %>" alt="<%#Eval("Name") %>" onerror="imgError(this);" /> 
                                    </div>
                            </div>
                        </ItemTemplate>
                        
                    </asp:Repeater>
                    <a class="carousel-control-prev" href="#carouselInd" role="button" data-slide="prev">
                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                        <span class="sr-only">Previous</span>
                    </a>
                    <a class="carousel-control-next" href="#carouselInd" role="button" data-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                        <span class="sr-only">Next</span>
                    </a>
                </div>
            </div> 
 
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="txtImageObjectID" EventName="TextChanged" />
           <asp:AsyncPostBackTrigger ControlID="btnGo" EventName="click" />
    </Triggers>
</asp:UpdatePanel>
<asp:UpdateProgress runat="server"><ProgressTemplate><img src="../images/loading.gif" /></ProgressTemplate></asp:UpdateProgress>
<script>
    function imgError(image) {
        image.onerror = "";
        image.src = "images/NoImage.png";
        return true;
    }
     
</script>