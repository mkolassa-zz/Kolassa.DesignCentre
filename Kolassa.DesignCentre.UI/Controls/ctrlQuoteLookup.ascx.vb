Public Class ctrlQuoteLookup
	Inherits System.Web.UI.UserControl

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

	End Sub

	Protected Sub grdQuoteLookup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdQuoteLookup.SelectedIndexChanged
		Dim lsID As String
		Dim lGID As Guid
		lGID = grdQuoteLookup.SelectedValue
		lsID = lGID.ToString
		Session("QuoteID") = lsID
		Dim c As New clsPersonalData
		c.GuidValue = lsID
		c.UsageType = "Quote"
		c.Insert()
		Session("Quote") = New clsQuote
		Response.Redirect(Request.RawUrl)
		'Stop
	End Sub
End Class