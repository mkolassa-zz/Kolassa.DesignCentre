Public Class ctrlPayments
	Inherits System.Web.UI.UserControl
    Dim msBuildingPhase As String
    Public Property BuildingPhase As String
        Get
            BuildingPhase = msBuildingPhase
        End Get
        Set(value As String)
            msBuildingPhase = value
            txtBuildingPhase.Text = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Form.Enctype = "multipart/form-data"
        Dim lsDate As String = Now.ToString("yyyy-MM-dd")
        txtActualPaymentDate.Text = lsDate
        txtCheckNumber.Text = ""
        txtPaymentComment.Text = ""
        txtPaymentDueAmount.Text = ""
        txtPaymentDueDate.Text = lsDate
    End Sub


    Protected Sub odsPaymens_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles odsPayments.Selecting
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
        txtBuildingPhase.Text = BuildingPhase
        txtActualPaymentAmount.Text = ""
        Dim lsDate As String = Now.ToString("yyyy-MM-dd")
        txtActualPaymentDate.Text = lsDate
        txtCheckNumber.Text = ""
        txtPaymentComment.Text = ""
        txtPaymentDueAmount.Text = ""
        txtPaymentDueDate.Text = lsDate
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
        cPay.BuildingPhase = txtBuildingPhase.Text
        cPay.CheckNumber = txtCheckNumber.Text
		cPay.PaymentComment = txtPaymentComment.Text
		'e.InputParameters("lsPaymentType") = txt.Text
		cPay.Insert()
	End Sub

	Protected Sub btnEdit_Click(sender As Object, e As EventArgs)
		'// Get the currently selected row using the SelectedRow property.
		Dim row As GridViewRow = sender.namingcontainer

        '// And you respective cell's value
        txtActualPaymentDate.Text = row.Cells(4).Text
        txtActualPaymentAmount.Text = row.Cells(5).Text
        txtCheckNumber.Text = row.Cells(8).Text
        txtPaymentComment.Text = row.Cells(7).Text
        txtPaymentDueAmount.Text = row.Cells(4).Text
		txtPaymentDueDate.Text = row.Cells(2).Text
        txtID.Text = row.Cells(9).Text
        txtBuildingPhase.Text = row.Cells(3).Text
    End Sub
End Class