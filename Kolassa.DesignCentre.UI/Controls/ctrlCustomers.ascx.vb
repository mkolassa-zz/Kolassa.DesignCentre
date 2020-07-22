Public Class ctrlCustomers
	Inherits System.Web.UI.UserControl
	Private msParentID As String

	Public Property ParentID() As String
		Get
			Return msParentID

		End Get
		Set(ByVal value As String)
			msParentID = value
			txtParentID.Text = value
			odsCustomer.DataBind()
		End Set
	End Property





	Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If Not IsPostBack Then

		End If
		txtParentID.Text = msParentID
	End Sub





	Protected Sub odsCustomer_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles odsCustomer.Selecting
		Dim rg As GridView ' Telerik.Web.UI.RadGrid
		Dim lsControl As String = "grdCustomers" '"rgMaster"
		Dim txt As TextBox
		Dim lsID As String
		txt = Me.Parent.FindControl("txtID")

		If Not txt Is Nothing Then
			lsID = txt.Text
			e.InputParameters("lsObjectID") = lsID
			txtParentID.Text = lsID

		Else

			rg = Me.Parent.FindControl(lsControl)
			If rg Is Nothing Then
				rg = Me.Parent.Parent.FindControl(lsControl)
			End If
			If rg Is Nothing Then
				rg = Me.Parent.Parent.Parent.FindControl(lsControl)
			End If
			If rg Is Nothing Then
				rg = Me.Parent.Parent.Parent.Parent.FindControl(lsControl)
			End If
			If rg Is Nothing Then
				e.InputParameters("lsObjectID") = "12121212-1212-1212-1212-121212121212"
			Else
				e.InputParameters("lsObjectID") = rg.SelectedValue
				txtParentID.Text = rg.SelectedValue
			End If
		End If
	End Sub


	Protected Sub cmdEdit_Click(sender As Object, e As EventArgs)
		fvCustomer.ChangeMode(FormViewMode.Edit)
	End Sub

	Protected Sub cmdSaveCustomer_Click(sender As Object, e As EventArgs)
		Dim c As New clsCustomer
		Dim t As TextBox
		Dim lsID As String = ""
		Dim lsMessage As String = ""
		Dim DC As New GlobalFunctionsDC
		t = Me.FindControl("txtParentID")

		Dim l As Literal
		l = fvCustomer.FindControl("litID")
		Try
			If DC.isGUIDString(l.Text) Then
				c.GetCustomer(l.Text)
			Else
				lsMessage = "No CustomerID found"
				Exit Sub
			End If
			t = fvCustomer.FindControl("txtCustomerName")

			c.CustomerName = t.Text
			t = fvCustomer.FindControl("txtAddress")
			c.CustomerAddress = t.Text

			t = fvCustomer.FindControl("txtCity")
			c.CustomerCity = t.Text

			t = fvCustomer.FindControl("txtEmail")
			c.CustomerEmail = t.Text

			t = fvCustomer.FindControl("txtPhoneNum")
			c.CustomerPhone = t.Text

			t = fvCustomer.FindControl("txtState")
			c.StateProvince = t.Text

			t = fvCustomer.FindControl("txtZip")
			c.Postal_Code = t.Text

			c.Update()
			Response.Redirect("~/frmCustomers")
		Catch ex As Exception
			lsMessage = Err.Description
		End Try
	End Sub

End Class