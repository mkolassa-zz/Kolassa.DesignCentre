<%@ Control Language="vb" AutoEventWireup="true"  CodeBehind="ctrlImages.ascx.vb"  Inherits="Kolassa.DesignCentre.UI.ctrlImages"  %>


<%@ Register Src="~/Controls/ctrlImageNew.ascx" TagPrefix="uc1" TagName="ctrlImageNew" %>

<style  >
    .ctrlImage img
   {
    border: 1px solid #ddd;
    border-radius: 4px;
    padding: 5px;
    width: 150px;
   }

   ctrlImage img:hover {
    box-shadow: 0 0 2px 1px rgba(0, 140, 186, 0.5);
}

    .rounded_corners
    {
        border: 1px solid #A1DCF2;
        -webkit-border-radius: 8px;
        -moz-border-radius: 8px;
        border-radius: 8px;
        overflow: hidden;
    }
    .rounded_corners td, .rounded_corners th
    {
        border: 1px solid #A1DCF2;
        font-family: Arial;
        font-size: 10pt;
        text-align: center;
    }
    .rounded_corners table table td
    {
        border-style: none;
    }
    .dclink {
        color: black;
    }
</style>
<script>
    function onError(img) {
        delete img.onerror;
        // Change the url
        //img.src = 'images/noload.jpg';
    
        // or just hide it (jQuery)
        jQuery(img).hide();
    }
</script>



<asp:ObjectDataSource ID="odsImages" runat="server" SelectMethod="LoadImages" TypeName="Kolassa.DesignCentre.Data.clsSelectDataLoader" 
	DeleteMethod="DeleteImages" UpdateMethod="Updateimages">
    <DeleteParameters>
        <asp:Parameter Name="RecordID" Type="String" />
        <asp:Parameter Name="llNodeID" Type="Int64" />
    </DeleteParameters>
    <SelectParameters>
        <asp:Parameter DefaultValue="12121212-1212-1212-1212-121212121212" Name="lsObjectID" Type="String" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="lsObjectID" Type="String" />
        <asp:Parameter Name="llNodeID" Type="Int64" />
        <asp:Parameter Name="lsName" Type="String" />
        <asp:Parameter Name="lsDescription" Type="String" />
        <asp:Parameter Name="lsImage" Type="String" />
        <asp:Parameter Name="lsImageURL" Type="String" />
        <asp:Parameter Name="lsActive" Type="String" />
        <asp:Parameter Name="ID" Type="String" />
        <asp:Parameter Name="liOrder" Type="Int32" />
    </UpdateParameters>
</asp:ObjectDataSource>


    <div class="container">
      <h5  data-toggle="collapse" data-target="#imageid">Image Information</h5>
        <div id="imageid" class="collapse">
            <div class="form-row">
                <asp:label ID="lblObjectID" runat="server" />
             </div>
        </div>
        <br />   
        <!-- Trigger the modal with a button -->
        <label  data-toggle="modal" data-target="#modal-image-url"><i class='fa fa-plus-square  padding-left:80px;'></i> Add Url </label>&nbsp;&nbsp;
        <asp:linkbutton ID="lnkImageUpload" runat="server" CssClass="dclink" ><i class=" dclink fa fa-upload padding-left:80px;"></i> Upload </asp:linkbutton>&nbsp;&nbsp;
        <label  data-toggle="modal" data-target="#modal-image"><i class='fa fa-search  padding-left:80px;'></i> Find Existing</label>&nbsp;&nbsp;
        <asp:linkbutton ID="lnkrefresh" runat="server" CssClass="dclink"><i class=' dclink fa fa-recycle  padding-left:80px; text-decoration-none !important'></i> Refresh</asp:linkbutton>
        <label  data-toggle="modal" data-target="#modal-image-upload" class="d-none"><i class='fa fa-upload d-none padding-left:80px;'></i> Upload </label>&nbsp;&nbsp;
        <!-- Modal -->
        <div class="modal fade" id="modal-image-url" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" style="background-color:deepskyblue;">
                        <div class="modal-title" >Add New Image</div>
                        <button type="button" class="close" onclick="$('#modal-image-url').modal('hide');" >&times;</button>
                    </div>
                    <div class="modal-body">
                       
                        <uc1:ctrlImageNew ID="ctrlImageNewURL" imagetype="URL" runat="server"  /> 
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default"  onclick="$('#modal-image-url').modal('hide');">Cancel</button>
                    </div>
                </div>
            </div>
        </div>        
        <div class="modal fade" id="modal-image-upload" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" style="background-color:deepskyblue;">
                        <div class="modal-title">Add New Image</div>
                        <button type="button" class="close" onclick="$('#modal-image-upload').modal('hide');" >&times;</button>
                    </div>
                    <div class="modal-body">
                  
                        <uc1:ctrlImageNew ID="ctrlImageNew1" imagetype="UPLOAD" runat="server"  /> 
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default"  onclick="$('#modal-image-upload').modal('hide');">Cancel</button>
                    </div>
                </div>
            </div>
        </div>        
        <div class="modal fade" id="modal-image-existing" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="modal-title">Add New Image</div>
                        <button type="button" class="close" onclick="$('#modal-image-existing').modal('hide');" >&times;</button>
                    </div>
                    <div class="modal-body">
                        <p>Select an Image.</p> 
                        <uc1:ctrlImageNew ID="ctrlImageNew2" imagetype="EXISTING" runat="server"  /> 
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default"  onclick="$('#modal-image-existing').modal('hide');">Cancel</button>
                    </div>
                </div>
            </div>
        </div>        
        <!-- *** End Modals *** -->
    </div>


<table >
    <tr><td style="width:100px;"></td><td></td></tr>
        <tr><td></td><td>
            <asp:UpdatePanel ID="upImages" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvImages" runat="server" DataSourceID="odsImages" CssClass="table table-striped table-bordered table-hover glyphicon-hover" 
                        AutoGenerateColumns="False" DataKeyNames="RecordID">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                <asp:LinkButton Runat="server" 
                                    OnClientClick="return confirm('Are you sure you want to DELETE this image?');" 
                                    CommandName="Delete"><i class='fa fa-trash  padding-left:30px;'></i></asp:LinkButton>
                                <asp:LinkButton Runat="server" 
                                    OnClientClick="return confirm('Are you sure you want to set this image as PRIMARY?');" 
                                    CommandName="Primary"  CommandArgument='<%#Eval("ID")%>'><i class='fa dc-Phases  padding-left:30px;'></i></asp:LinkButton>
                                <asp:LinkButton Runat="server" 
                                    OnClientClick="return confirm('Are you sure you want to set this image as the SWATCH?');" 
                                    CommandName="Swatch"  CommandArgument='<%#Eval("ID")%>'><i class='fa dc-stats-dots  padding-left:30px;'></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText ="ID" Visible="false" />
                            <asp:BoundField DataField="Name" HeaderText ="Picture Name" />
                            <asp:BoundField DataField="Description" HeaderText ="Picture Desc" />
                            <asp:BoundField DataField="PrimaryItem" HeaderText ="Primary" />
                            <asp:BoundField DataField="Swatch" HeaderText ="Swatch" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                   <!-- <a href="< % #"data:" + Trim(Eval("Type")) + ";base64," + Convert.ToBase64String(Eval("Image")) % >">
                                        <asp:Image CssClass="ctrlImage" ID="Image1" runat="server" Height="100px" ImageUrl='< % #"data:" + Trim(Eval("Type")) + ";base64," + Convert.ToBase64String(Eval("Image")) %>' />
                                    </a> -->
                                    <img src =<%# Eval("ImageURL") %> alt="No Image" onerror="onError(this)" height="50" />

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            No Images Found.
                        </EmptyDataTemplate>
                    </asp:GridView>
                    </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gvImages" EventName="RowDeleted" />
                    <asp:AsyncPostBackTrigger ControlID="ctrlImageNewURL" EventName="SomethingHappened" />
                    <asp:AsyncPostBackTrigger ControlID="lnkRefresh" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </td></tr>

</table>
<style>
    body {
  padding: 30px 0;
  position: relative;
}

.gallery {
  width: 600px;
  margin: auto;
  border-radius: 3px;
  overflow: hidden;
  //position: relative;
}
.img-c {
  width: 200px;
  height: 200px;
  float: left;
  position: relative;
  overflow: hidden;
}

.img-w {
  position: absolute;
  width: 100%;
  height: 100%;
  background-size: cover;
  background-position: center;
  cursor: pointer;
  transition: transform ease-in-out 300ms;
}

.img-w img {
  display: none;
}

.img-c {
    transition: width ease 400ms, height ease 350ms, left cubic-bezier(0.4, 0, 0.2, 1) 420ms, top cubic-bezier(0.4, 0, 0.2, 1) 420ms;
}

.img-c:hover .img-w {
  transform: scale(1.08);
  transition: transform cubic-bezier(0.4, 0, 0.2, 1) 450ms;
}

.img-c.active {
  width: 100% !important;
  height: 100% !important;
  position: absolute;
  z-index: 2;
  //transform: translateX(-50%);
}

.img-c.postactive {
  position: absolute;
  z-index: 2;
  pointer-events: none;
}

.img-c.active.positioned {
  left: 0 !important;
  top: 0 !important;
  transition-delay: 50ms;
}
</style>
<hr />
<div class="gallery">
    <asp:Repeater ID="rpt" runat="server" DataSourceID="odsImages">
    <ItemTemplate >
    <div class="img-w">
        <img src="<%# Eval("ImageURL") %>" alt="Image Not Found" />
     </div>
    </ItemTemplate>
    </asp:Repeater>
</div>
      <div class="gallery">
  <div class="img-w">
    <img src="https://images.unsplash.com/photo-1485766410122-1b403edb53db?dpr=1&auto=format&fit=crop&w=1500&h=846&q=80&cs=tinysrgb&crop=" alt="" /></div>
  <div class="img-w"><img src="https://images.unsplash.com/photo-1485793997698-baba81bf21ab?dpr=1&auto=format&fit=crop&w=1500&h=1000&q=80&cs=tinysrgb&crop=" alt="" /></div>
  <div class="img-w"><img src="https://images.unsplash.com/photo-1485871800663-71856dc09ec4?dpr=1&auto=format&fit=crop&w=1500&h=2250&q=80&cs=tinysrgb&crop=" alt="" /></div>
  <div class="img-w"><img src="https://images.unsplash.com/photo-1485871882310-4ecdab8a6f94?dpr=1&auto=format&fit=crop&w=1500&h=2250&q=80&cs=tinysrgb&crop=" alt="" /></div>
  <div class="img-w"><img src="https://images.unsplash.com/photo-1485872304698-0537e003288d?dpr=1&auto=format&fit=crop&w=1500&h=1000&q=80&cs=tinysrgb&crop=" alt="" /></div>
  <div class="img-w"><img src="https://images.unsplash.com/photo-1485872325464-50f17b82075a?dpr=1&auto=format&fit=crop&w=1500&h=1000&q=80&cs=tinysrgb&crop=" alt="" /></div>
  <div class="img-w"><img src="https://images.unsplash.com/photo-1470171119584-533105644520?dpr=1&auto=format&fit=crop&w=1500&h=886&q=80&cs=tinysrgb&crop=" alt="" /></div>
  <div class="img-w"><img src="https://images.unsplash.com/photo-1485881787686-9314a2bc8f9b?dpr=1&auto=format&fit=crop&w=1500&h=969&q=80&cs=tinysrgb&crop=" alt="" /></div>
  <div class="img-w"><img src="https://images.unsplash.com/photo-1485889397316-8393dd065127?dpr=1&auto=format&fit=crop&w=1500&h=843&q=80&cs=tinysrgb&crop=" alt="" /></div>
</div>
<hr />

            
<script>
    $(function () {
        $(".img-w").each(function () {
            $(this).wrap("<div class='img-c'></div>")
            let imgSrc = $(this).find("img").attr("src");
            $(this).css('background-image', 'url(' + imgSrc + ')');
        })

        $(".img-c").click(function () {
            let w = $(this).outerWidth()
            let h = $(this).outerHeight()
            let x = $(this).offset().left
            let y = $(this).offset().top

            $(".active").not($(this)).remove()
            let copy = $(this).clone();
            copy.insertAfter($(this)).height(h).width(w).delay(500).addClass("active")
            $(".active").css('top', y - 8);
            $(".active").css('left', x - 8);

            setTimeout(function () {
                copy.addClass("positioned")
            }, 0)
        })
    })

    $(document).on("click", ".img-c.active", function () {
        let copy = $(this)
        copy.removeClass("positioned active").addClass("postactive")
        setTimeout(function () {
            copy.remove();
        }, 500)
    })
</script>