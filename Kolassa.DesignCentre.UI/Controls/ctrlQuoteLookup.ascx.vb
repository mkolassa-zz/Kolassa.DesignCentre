Public Class ctrlQuoteLookup
	Inherits System.Web.UI.UserControl

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

	End Sub

	Protected Sub grdQuoteLookup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdQuoteLookup.SelectedIndexChanged
		Dim lsID As String
        Dim lGID As Guid

        '*** Clear the ID in the  querystring
        Dim nvc As NameValueCollection = HttpUtility.ParseQueryString(Request.Url.Query)
        nvc.Remove("quoteid")
        Dim url As String = Request.Url.AbsolutePath + "?" + nvc.ToString()
        '  Response.Redirect(url)

        lGID = grdQuoteLookup.SelectedValue
		lsID = lGID.ToString
		Session("QuoteID") = lsID
		Dim c As New clsPersonalData
		c.GuidValue = lsID
		c.UsageType = "Quote"
		c.Insert()
        Session("Quote") = New clsQuote
        Dim newURL As String = url & "quoteid=" & lsID ' Request.RawUrl
        Response.Redirect(newURL)
        'Stop
    End Sub
End Class