Public Class ctrlCustomerList
	Inherits System.Web.UI.UserControl

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

	End Sub
	Function GetObjectID() As String
		Dim lsObjectID As String
		Dim lit As Literal
		lit = Me.Parent.FindControl("litID")
		If lit Is Nothing Then
			lit = Me.Parent.Parent.FindControl("litID")
		End If
		If lit Is Nothing Then
			lit = Me.Parent.Parent.Parent.FindControl("litID")
		End If
		If lit Is Nothing Then
			lit = Me.Parent.Parent.Parent.Parent.FindControl("litID")
		End If
		If lit Is Nothing Then
			lsObjectID = "12121212-1212-1212-1212-121212121212"
		Else
			lsObjectID = lit.Text
		End If

		GetObjectID = lsObjectID
	End Function
	Public Function get_Records() As IEnumerable(Of clsCustomer)
        Dim ie As IEnumerable(Of clsCustomer)

        Dim c As New clsCustomers
		Dim llNodeID As Long = Session("NodeID")
        If llNodeID = 0 Then
            ie = c.GetRecords("CustomerName", "", "", True, 0)
        Else
            Dim lsProjectID As String = Session("Project")
            If lsProjectID = "" Then lsProjectID = "11112222-3333-4444-5555-666677778888"
            ie = c.GetRecords("CustomerName", "", "", True, llNodeID)
        End If
        get_Records = ie
	End Function
End Class