<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCustomerList.ascx.vb" Inherits="Kolassa.DesignCentre.UI.ctrlCustomerList" %>

<script>
$(document).ready(function() {
  $(".searchc").keyup(function () {
    var searchTerm = $(".searchc").val();
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

	<input class="searchc form-control" type="text" placeholder="Search">
			
<br />

<span class="counter pull-right"></span>
	<asp:GridView ID="grdCustomerLookup" runat="server" AutoGenerateSelectButton="True"  ItemType="Kolassa.DesignCentre.UI.clsCustomer" 
		SelectMethod="Get_Records"
		class="table table-hover table-bordered results" AutoGenerateColumns="False" DataKeyNames="ID">
		<Columns>
			
			<asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
			<asp:BoundField DataField="CustomerName" HeaderText="Name" />
			<asp:BoundField DataField="CustomerCity" HeaderText="City" />
			<asp:BoundField DataField="StateProvince" HeaderText="State" />
			<asp:ImageField DataImageUrlField="ImageURL">
				<ControlStyle CssClass="img-thumbnail" />
			</asp:ImageField>
		</Columns>
</asp:GridView>
	
      
                
                