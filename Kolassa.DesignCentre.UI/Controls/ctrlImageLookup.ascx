<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlImageLookup.ascx.vb" Inherits="Kolassa.DesignCentre.UI.ctrlImageLookup" %>
<script>
    $(document).ready(function () {
      
  $(".search").keyup(function () {
    var searchTerm = $(".search").val();
    var listItem = $('.results tbody').children('tr');
    var searchSplit = searchTerm.replace(/ /g, "'):containsi('")
    
  $.extend($.expr[':'], {'containsi': function(elem, i, match, array){
        return (elem.textContent || elem.innerText || '').toLowerCase().indexOf((match[3] || "").toLowerCase()) >= 0;
    }
  });
    
  $(".results tbody tr").not(":containsi('" + searchSplit + "')").each(function(e){
    $(this).attr('visible','false');
  });

  $(".results tbody tr:containsi('" + searchSplit + "')").each(function(e){
    $(this).attr('visible','true');
  });

  var jobCount = $('.results tbody tr[visible="true"]').length;
    $('.counter').text(jobCount + ' item');

  if(jobCount == '0') {$('.no-result').show();}
    else {$('.no-result').hide();}
		  });
});
</script>
<style>
	body{   padding:20px 20px; }

    .results tr[visible='false'],
    .no-result{
        display:none;
    }

    .results tr[visible='true']{
        display:table-row;
    }

    .counter{
        padding:8px; 
        color:#ccc;
    }

    .ctrlImage:hover {
    position:relative;
    width:auto!important;
    height:auto!important;
    display:block;
    z-index:999;
}
</style>
<script>
    function button_click(objTextBox, objBtnID) {
        if (window.event.keyCode == 13) {
            document.getElementById(objBtnID).focus();
            document.getElementById(objBtnID).click();
        }
    }
</script>

<asp:UpdateProgress ID="upProgressLookup" runat="server" AssociatedUpdatePanelID="upResults" >
    <ProgressTemplate><img src="../images/loadingH.gif" /></ProgressTemplate>
</asp:UpdateProgress>

<asp:UpdatePanel ID="upResults" runat="server"  UpdateMode="Conditional">
    <ContentTemplate>
        <div class="form-inline m-3">
            <table>
                <tr>
                    <td>
                        <asp:textbox  cssclass="form-control m-2" id="txtImageSearch" AutoPostBack="false" onkeydown="return (event.keyCode!=13);" type="search" placeholder="Search" tabindex="0" runat="server" />
                        <asp:LinkButton CssClass="form-control m-2" ID="lnkSearch" runat="server" Text="<i class='fas fa-search'></i>" OnClick="cmdSearch_Click" /> 
                    </td>
                  
               </tr>
            </table>
        </div>
        <span class="float-right"><asp:Label Font-Size="XX-Small" cssclass="form-control-plaintext m-2" ID ="lblObjectID" runat="server" /></span>

      <!-- <button class="btn btn-outline-success my-2 my-sm-0" type="submit"  >Search</button> -->
        <span class="counter pull-right"></span>
          <asp:GridView ID="grdImageLookup" runat="server" EnableViewState="false"   DataKeyNames="ID"  AutoGenerateSelectButton ="true" 
		        class="table table-hover table-bordered results" AllowPaging="true"  PageSize="50" AutoGenerateColumns="False"   >
		        <Columns>
                    <asp:templatefield HeaderText="Select">
                        <itemtemplate>
                            <asp:checkbox ID="cbSelect"
                            CssClass="gridCB" runat="server"></asp:checkbox>
                        </itemtemplate>
                    </asp:templatefield>
			        <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
			        <asp:BoundField DataField="Name" HeaderText="Name" />
			        <asp:BoundField DataField="Description" HeaderText="Description" />
		            <asp:TemplateField>
                        <ItemTemplate>
                            <!-- a href="< %# fGetImage(Eval("sImage"), Eval("sURL")) %>"> -->
                                <asp:Image CssClass="ctrlImage" ID="Image1" runat="server" class="thumbnail" Height="100px" ImageUrl='<%# fGetImage(Eval("sImage"), Eval("sURL")) %>' />
                            </!-->
                        </ItemTemplate>
                    </asp:TemplateField>
		        </Columns>
        </asp:GridView>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkSearch" EventName="Click"  />
        <asp:AsyncPostBackTrigger ControlID="txtImageSearch" EventName="TextChanged"  />
    </Triggers>
</asp:UpdatePanel>

