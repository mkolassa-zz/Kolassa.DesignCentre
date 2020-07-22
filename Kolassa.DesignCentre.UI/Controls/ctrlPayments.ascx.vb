Public Class ctrlPayments
	Inherits System.Web.UI.UserControl

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

		Page.Form.Enctype = "multipart/form-data"
	End Sub


	Protected Sub odsCommunications_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles odsPayments.Selecting
		Dim lsOID As String = GetObjectID()
		e.InputParameters("lsObjectID") = lsOID
		litID.Text = lsOID
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

	Protected Sub cmdPost_Click(sender As Object, e As EventArgs) Handles cmdPost.Click
		InsertRecord()
		odsPayments.DataBind()
		txtActualPaymentAmount.Text = ""
		txtActualPaymentDate.Text = ""
		txtCheckNumber.Text = ""
		txtPaymentComment.Text = ""
		txtPaymentDueAmount.Text = ""
		txtPaymentDueDate.Text = ""
	End Sub




	Private Sub InsertRecord()
		Dim cPay As New clsPayment()
		Dim lsOID As String = GetObjectID()
		cPay.ActualPaymentDate = txtActualPaymentDate.Text
		cPay.ObjectID = lsOID
		litID.Text = lsOID
		cPay.PaymentDueDate = txtPaymentDueDate.Text
		cPay.PaymentDueAmount = Val(txtPaymentDueAmount.Text)

		cPay.ActualPaymentAmount = Val(txtActualPaymentAmount.Text)
		cPay.BuildingPhase = ""
		cPay.CheckNumber = txtCheckNumber.Text
		cPay.PaymentComment = txtPaymentComment.Text
		'e.InputParameters("lsPaymentType") = txt.Text
		cPay.Insert()
	End Sub

	Protected Sub btnEdit_Click(sender As Object, e As EventArgs)
		'// Get the currently selected row using the SelectedRow property.
		Dim row As GridViewRow = sender.namingcontainer

		'// And you respective cell's value
		txtActualPaymentDate.Text = row.Cells(3).Text
		txtActualPaymentAmount.Text = row.Cells(5).Text
		txtCheckNumber.Text = row.Cells(7).Text
		txtPaymentComment.Text = row.Cells(6).Text
		txtPaymentDueAmount.Text = row.Cells(4).Text
		txtPaymentDueDate.Text = row.Cells(2).Text
		txtID.Text = row.Cells(9).Text
	End Sub
End Class