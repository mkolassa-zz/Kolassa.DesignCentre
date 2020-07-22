<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlUnitList.ascx.vb" Inherits="Kolassa.DesignCentre.UI.ctrlUnitList" %>

<script>
$(document).ready(function() {
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
		body{
  padding:20px 20px;
}

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
	</style>

	<input class="search form-control" type="text" placeholder="Search">
			
<br />

<span class="counter pull-right"></span>
	<asp:GridView ID="grdUnitLookup" runat="server" AutoGenerateSelectButton="True"  ItemType="Kolassa.DesignCentre.UI.clsUnit" SelectMethod="Get_Records"
		class="table table-hover table-bordered results" AutoGenerateColumns="False" DataKeyNames="ID">
		<Columns>
			<asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
			<asp:BoundField DataField="Code" HeaderText="Unit Code" />
			<asp:BoundField DataField="Name" HeaderText="Unit Name" />
			<asp:BoundField DataField="Tier" HeaderText="Unit Tier" visible="false"/>
			<asp:BoundField DataField="UnitTYpeID" HeaderText="Type Description"  Visible="true"/>
		</Columns>
</asp:GridView>
	
      
                
                