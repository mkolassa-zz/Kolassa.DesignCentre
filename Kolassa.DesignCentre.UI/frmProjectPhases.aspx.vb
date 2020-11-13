Imports System.IO

Public Class frmProjectPhases
	Inherits System.Web.UI.Page
	Dim msFilter As String = ""
	Dim mlRecordID As Long
	Dim msID As String
	Dim mlRow As Long


	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'*** Put user code to initialize the page here
		Page.Form.Enctype = "multipart/form-data"
		System.Threading.Thread.Sleep(500)
		If Session("NodeID") Is Nothing Then Session("NodeID") = 0
		If IsPostBack Then
			' response.write(Session("msFilter"))
			Return
		Else : Session("msFilter") = ""
		End If

	End Sub

	Protected Sub DataGridDisplay_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPhases.RowDataBound
		If gvPhases.EditIndex <> -1 Then
			gvPhases.ShowFooter = False
			'Me.cmdAddNew.Visible = False

		Else
			'  Me.cmdAddNew.Visible = True

		End If
	End Sub



	Protected Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
		Dim t As TextBox
		t = Me.gvPhases.FooterRow.FindControl("Name")
		Dim lsName As String = t.Text ' ((TextBox)gvIngredient.headerRow.FindControl("tbIngredient")).Text;
		If lsName = "" Then
			'response.write("You must enter a Name.")
		Else
			odsPhases.Insert()
			gvPhases.ShowFooter = False
		End If


	End Sub

	Protected Sub ObjectDataSource1_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles odsPhases.Inserting
		Dim t As TextBox
		'Dim c As CheckBox
		Dim d As DropDownList
		t = Me.gvPhases.FooterRow.FindControl("Name")
		Dim lsName As String = t.Text ' ((TextBox)gvIngredient.headerRow.FindControl("tbIngredient")).Text;
		t = Me.gvPhases.FooterRow.FindControl("txtDescription")
		Dim lsDescription As String = t.Text
		' t = Me.DataGridDisplay.FooterRow.FindControl("Image")
		' Dim lsImage As String = t.Text


		d = Me.gvPhases.FooterRow.FindControl("cboProjectTYpe")
		Dim lsProjectType As String = d.SelectedValue
		'   c = Me.DataGridDisplay.FooterRow.FindControl("Active")
		'   Dim lsActive As String = c.Checked


		e.InputParameters("lsName") = lsName
		e.InputParameters("lsDescription") = IIf(lsDescription = "", "", lsDescription)
		' e.InputParameters("Image") = IIf(lsImage = "", "", lsImage)
		e.InputParameters("lsProjectType") = IIf(lsProjectType = "", "N/A", lsProjectType)
		e.InputParameters("llNodeID") = Session("NodeID")

	End Sub

	Protected Sub cmdAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
		gvPhases.EditIndex = -1
		gvPhases.ShowFooter = True
	End Sub

	Protected Sub DataGridDisplay_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvPhases.Load
		If gvPhases.EditIndex <> -1 Then
			gvPhases.ShowFooter = False
		End If
	End Sub

	Protected Sub DataGridDisplay_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvPhases.RowEditing
		gvPhases.ShowFooter = False
		'Dim t As TextBox
		'Dim C As CheckBox
		'Dim d As DropDownList
		Exit Sub
		Dim r As GridViewRow
		Dim l As Literal
		Dim liRow As Integer

		liRow = e.NewEditIndex
		If liRow = -1 Then
		Else
			r = gvPhases.Rows(liRow) 'DataGridDisplay.Rows(0) 'liRow) 
			Dim lsType As String
			lsType = r.FindControl("ID").GetType.FullName
			If lsType <> "System.Web.UI.WebControls.Literal" Then
				Exit Sub
			End If
			l = r.FindControl("ID")
			msID = l.Text
			mlRow = liRow
			Session("ID") = msID
			Session("Row") = liRow
		End If
	End Sub

	Protected Sub ObjectDataSource1_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles odsPhases.Updating
		Dim l As Literal
		Dim t As TextBox
		Dim r As GridViewRow
		Dim C As CheckBox
		Dim d As DropDownList
		System.Threading.Thread.Sleep(2000)
		Exit Sub
		If Session("NodeID") Is Nothing Then Session("NodeID") = 0
		r = gvPhases.Rows(gvPhases.EditIndex)
		t = r.FindControl("txtName")
		Dim lsName As String = t.Text
		t = r.FindControl("txtDescription")
		Dim lsDescription As String = t.Text

		l = r.FindControl("litID")
		Dim lsID As String = l.Text
		C = r.FindControl("chkActive")
		Dim lsActive As String = IIf(C.Checked = True, "1", "0")
		d = r.FindControl("cboProjectTYpe")
		Dim lsProjectType As String = d.SelectedValue
		Dim lsImage As String = ""

		lsID = IIf(lsID = "", "", lsID)
		lsName = IIf(lsName = "", "", lsName)
		lsDescription = IIf(lsDescription = "", "N/A", lsDescription)
		lsActive = IIf(lsActive = "", "1", lsActive)
		lsImage = IIf(lsImage = "", "1", lsImage)
		Dim lsNodeID As String = Session("NodeID")

		e.InputParameters("lsName") = (lsName)
		e.InputParameters("lsDescription") = IIf(lsDescription = "", "", lsDescription)
		' e.InputParameters("Image") = IIf(lsImage = "", "", lsImage)
		e.InputParameters("lsProjectType") = IIf(lsProjectType = "", "N/A", lsProjectType)
		e.InputParameters("llNodeID") = Session("NodeID")
		e.InputParameters("ID") = lsID
		e.InputParameters("lsActive") = lsActive
	End Sub


	Public Overrides Sub VerifyRenderingInServerForm(ByVal Control As Control)
		Return
	End Sub







	Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs)
		' Read the file and convert it to Byte Array

		Dim r As GridViewRow = gvPhases.Rows(Session("Row"))

		Dim lbl As Label
		lbl = r.FindControl("lblMessage")
		If lbl Is Nothing Then
			Response.Write("Something went wrong.")
			Exit Sub
		End If


		Dim filePath As String
		Dim fu As FileUpload = New FileUpload

		fu = r.FindControl("FileUpload1")
		filePath = fu.PostedFile.FileName
		Dim filename As String = Path.GetFileName(filePath)
		Dim ext As String = Path.GetExtension(filename)
		Dim contenttype As String = String.Empty

		'Set the contenttype based on File Extension
		Select Case ext
			Case ".doc"
				contenttype = "application/vnd.ms-word"
				Exit Select
			Case ".docx"
				contenttype = "application/vnd.ms-word"
				Exit Select
			Case ".xls"
				contenttype = "application/vnd.ms-excel"
				Exit Select
			Case ".xlsx"
				contenttype = "application/vnd.ms-excel"
				Exit Select
			Case ".jpg"
				contenttype = "image/jpg"
				Exit Select
			Case ".png"
				contenttype = "image/png"
				Exit Select
			Case ".gif"
				contenttype = "image/gif"
				Exit Select
			Case ".pdf"
				contenttype = "application/pdf"
				Exit Select
		End Select
		If contenttype <> String.Empty Then
			Dim fs As Stream = fu.PostedFile.InputStream
			Dim br As New BinaryReader(fs)
			Dim bytes As Byte() = br.ReadBytes(fs.Length)

			'insert the file into database
			Dim strQuery As String = "insert into tblFiles" _
			& "(Name, ContentType, Data)" _
			& " values (@Name, @ContentType, @Data)"
			' Dim cmd As New SqlCommand(strQuery)

			'cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = filename
			'cmd.Parameters.Add("@ContentType", SqlDbType.VarChar).Value = contenttype
			'cmd.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes
			'InsertUpdateData(cmd)
			lbl.ForeColor = System.Drawing.Color.Green
			lbl.Text = "File Uploaded Successfully"
		Else
			lbl.ForeColor = System.Drawing.Color.Red
			lbl.Text = "File format not recognised." _
			& " Upload Image/Word/PDF/Excel formats"
		End If
	End Sub

	Private Sub DataGridDisplay_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) ' Handles gvPhases.RowDeleting
		Exit Sub
		Dim llRow As Long = e.RowIndex
		Dim r As GridViewRow = gvPhases.Rows(llRow)

		msID = ""
		Dim l As Literal
		l = r.FindControl("ID")
		If l IsNot Nothing Then
			msID = l.Text
		End If
	End Sub

	Protected Sub DataGridDisplay_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPhases.RowCommand
		Exit Sub
		If e.CommandName.Equals("update") Then
			'//GridView Row Index
			Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString().Trim())
			Dim r As GridViewRow = gvPhases.Rows(rowIndex)
			' // ID of the Current Row

			Dim l As Literal = r.Cells(1).FindControl("litID")
			Dim id As String = l.Text

			'//File Upload Instance of the Current Row
			Dim fileUpload As FileUpload = gvPhases.Rows(rowIndex).FindControl("FileUpload1")

			' //File
			Dim Stream As System.IO.Stream = fileUpload.PostedFile.InputStream

			'//File Length
			Dim length As Integer = fileUpload.PostedFile.ContentLength

			' //Content of the File
			Dim data As Byte()
			'// THis might throw error.  INt that case, delete the line.
			Dim hexvalue As Long
			data = New Byte() {Byte.Parse(hexvalue, System.Globalization.NumberStyles.HexNumber)}

			'//Fill data
			Stream.Read(data, 0, length)

			' //File Extension
			Dim extension As String = System.IO.Path.GetExtension(fileUpload.PostedFile.FileName)
		End If
	End Sub



	Protected Sub insertPhase(sender As Object, e As EventArgs)

	End Sub

	Protected Sub cmdInsertPhase_Click(sender As Object, e As EventArgs) Handles cmdInsertPhase.Click
		Dim c As New clsPhase
		Dim liSortOrder As Int16
		Dim lsCode, lsName, lsDesc As String
		lsCode = txtPhaseCode.Text
		lsName = txtPhaseName.Text
		lsDesc = txtPhaseDescription.Text
		liSortOrder = Val(txtSortOrder.Text)
		If lsCode = "" Or lsDesc = "" Then
			Response.Write("Code and Name are required!")
			Exit Sub
		End If
		c.Active = True
		c.NodeID = Session("NodeID")
		c.ObjectID = Session("Project")
		c.Code = txtPhaseCode.Text
		c.Name = txtPhaseName.Text
		c.SortOrder = liSortOrder
		c.Description = txtPhaseDescription.Text
		c.Insert()

		If c.ErrorMessage = "" Then
			BootstrapAlerts.BootstrapAlert(litMsg, "Congrats! You've won a dismissable booty message.", BootstrapAlerts.BootstrapAlertType.Success, True)

		Else

			BootstrapAlerts.BootstrapAlert(litMsg, c.ErrorMessage, BootstrapAlerts.BootstrapAlertType.Warning, True)
		End If
	End Sub
End Class
