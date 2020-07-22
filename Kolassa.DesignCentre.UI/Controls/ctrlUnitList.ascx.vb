Public Class ctrlUnitList
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
	Public Function get_Records() As IEnumerable(Of clsUnit)
		Dim ie As IEnumerable(Of clsUnit)
		Dim c As New clsUnits
		Dim lsProjectID As String = Session("Project")
		Dim llNodeID As Long = Session("NodeID")
		If lsProjectID = "" Then lsProjectID = "11112222-3333-4444-5555-666677778888"
		ie = c.GetRecords("", "", lsProjectID, llNodeID)
		get_Records = ie
	End Function
End Class