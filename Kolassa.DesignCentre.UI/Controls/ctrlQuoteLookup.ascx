<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlQuoteLookup.ascx.vb" Inherits="Kolassa.DesignCentre.UI.ctrlQuoteLookup" %>
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

	<input class="search form-control" id="txtQuoteSearch" type="text" placeholder="Search" tabindex="0" >
			
	<!-- <button class="btn btn-outline-success my-2 my-sm-0" type="submit"  >Search</button> -->

<span class="counter pull-right"></span>
	<asp:GridView ID="grdQuoteLookup" runat="server" AutoGenerateSelectButton="True" DataSourceID="odsQuoteLookup"
		class="table table-hover table-bordered results" AutoGenerateColumns="False" DataKeyNames="ID">
		<Columns>
			<asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
			<asp:BoundField DataField="CustomerName" HeaderText="Customer" />
			<asp:BoundField DataField="UnitName" HeaderText="Unit Name" />
			<asp:BoundField DataField="UnitTypeName" HeaderText="Unit Type Name" />
			<asp:BoundField DataField="UnitTypeDescription" HeaderText="Type Description" />
			<asp:BoundField DataField="UnitTypeCode" HeaderText="Unit Type Code" />
		</Columns>
</asp:GridView>

	<asp:ObjectDataSource ID="odsQuoteLookup" runat="server" 
                SelectMethod="LoadAllQuotes" TypeName="Kolassa.DesignCentre.Data.clsSelectDataLoader">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="1" Name="llNodeID" SessionField="NodeID"      Type="Int64" />
            <asp:Parameter DefaultValue="" Name="lsWhere" Type="String" />
            <asp:Parameter DefaultValue="True" Name="lbActive" Type="Boolean" />
            <asp:Parameter DefaultValue="" Name="llID" Type="Int64" />
			<asp:SessionParameter DefaultValue="" Name="ParentID"  SessionField="Project" Type="String" />
			<asp:SessionParameter DefaultValue="" Name="ProjectID"  SessionField="Project" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>