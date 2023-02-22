<%@ Control Language="vb" AutoEventWireup="false"  CodeBehind="ctrlImagesDisplay.ascx.vb"  Inherits="Kolassa.DesignCentre.UI.ctrlImagesDisplay"  %>

<asp:ObjectDataSource ID="odsImages" runat="server" SelectMethod="LoadImages" TypeName="Kolassa.DesignCentre.Data.clsSelectDataLoader" >
    <SelectParameters>
        <asp:Parameter DefaultValue="12121212-1212-1212-1212-121212121212" Name="lsObjectID" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>

<asp:textbox Visible="false" ID="txtImageObjectID" runat="server" />
<div class="gallery">
    <asp:Repeater ID="rpt" runat="server" DataSourceID="odsImages">
    <ItemTemplate >
    <div class="img-w">
        <img src="<%# Eval("ImageURL") %>" alt="Image Not Found" />
     </div>
    </ItemTemplate>
        <FooterTemplate>
            <asp:Label ID="defaultItem" runat="server"                 Visible='<%# rpt.Items.Count = 0 %>' Text="No items found" />
        </FooterTemplate>

    </asp:Repeater>
</div>
    <div class="gallery">
        <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
  <ol class="carousel-indicators">
    <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
    <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
    <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
    <li data-target="#carouselExampleIndicators" data-slide-to="3"></li>
  </ol>
  <div class="carousel-inner small d-flex justify-content-center bg-secondary " >
    <div class="carousel-item active ">
      <img class="d-block " style="max-height:300px;" src="https://miltonelectric.com/wp-content/uploads/2021/09/electrical-outlet.jpg" alt="First slide">
    </div>
    <div class="carousel-item">
      <img class="d-block " style="max-height:300px;" src="https://cdn-tp3.mozu.com/24645-37138/cms/37138/files/2e4d5c45-5f6a-4e2e-b08b-6af6c2acb006?quality=60&_mzcb=_1649148331752=" alt="Second slide">
    </div>
    <div class="carousel-item">
      <img class="d-block " style="max-height:300px;" src="https://www.homelectrical.com/_next/image?url=https%3A%2F%2Fwww.homelectrical.com%2Fsites%2Fdefault%2Ffiles%2Fstyles%2Fimage_500x500%2Fpublic%2Fimages%2Fproduct%2Funsorted1%2FHOM-PSHREC20W_1.jpg%3Ftimestamp%3D1446699579&w=1920&q=75" alt="Third slide">
    </div>
    <div class="carousel-item">
      <img class="d-block"  style="max-height:300px;" src="https://cdn.mscdirect.com/global/images/ProductImages/5403045-23.jpg" alt="Fourth slide">
    </div>
  </div>
  <a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
    <span class="sr-only">Previous</span>
  </a>
  <a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
    <span class="carousel-control-next-icon" aria-hidden="true"></span>
    <span class="sr-only">Next</span>
  </a>
</div>
</div>

