Public Class ctrlCustomers
	Inherits System.Web.UI.UserControl
	Private msParentID As String
	Public Sub Insert()
		fvCustomer.ChangeMode(FormViewMode.Insert)
	End Sub
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

	Public Sub NewCustomerRecord()
		'Exit Sub
		'Stop
		fvCustomer.ChangeMode(FormViewMode.Insert)
		Exit Sub
		Dim t As TextBox
		Dim d As DropDownList
		Dim l As LiteralControl
		For Each c As Control In Me.Controls
			If TypeOf (c) Is TextBox Then
				t = c
				t.Text = ""
			End If
			If TypeOf (c) Is DropDownList Then
				d = c
				d.ClearSelection()
			End If
			If TypeOf (c) Is LiteralControl Then
				l = c
				l.Text = ""
			End If
		Next


	End Sub



	Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
		If Not IsPostBack Then

		End If
		txtParentID.Text = msParentID
	End Sub





	Protected Sub odsCustomer_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles odsCustomer.Selecting
		Dim rg As GridView
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
	Protected Sub cmdInsert_Click(sender As Object, e As EventArgs)
		fvCustomer.ChangeMode(FormViewMode.Insert)
	End Sub
	Protected Sub cmdSaveCustomer_Click(sender As Object, e As EventArgs)
		SaveRecord("edit")
	End Sub
	Protected Sub cmdInsertCustomer_Click(sender As Object, e As EventArgs)
		SaveRecord("insert")
	End Sub
	Sub SaveRecord(lsType As String)
		Dim c As New clsCustomer
		Dim t As TextBox
		Dim lsID As String = ""
		Dim lsMessage As String = ""
		Dim DC As New GlobalFunctionsDC
		t = Me.FindControl("txtParentID")

		Dim l As Literal
		l = fvCustomer.FindControl("litID")
		Try
			If DC.isGUIDString(l.Text) And lsType = "edit" Then
				lsID = l.Text
				c.GetCustomer(l.Text)
			Else
				lsMessage = "No CustomerID found, lets insert"
				'Exit Sub
			End If
			t = fvCustomer.FindControl("txtCustomerName")

			c.NAME = t.Text
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
			If DC.isGUIDString(lsID) Then
				c.Update()
			Else
				lsMessage = "No CustomerID found"
				c.Insert()
			End If


			Response.Redirect("~/frmCustomers")
		Catch ex As Exception
			lsMessage = Err.Description
		End Try
	End Sub

End Class