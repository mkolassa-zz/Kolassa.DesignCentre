Public Class RptTest
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

	End Sub

	Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		'Stop
		Dim x As Integer = 23
		MsgBox(x)
	End Sub

End Class
