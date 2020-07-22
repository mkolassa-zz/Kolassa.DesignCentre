Public Class frmUnitProfiles
	Inherits System.Web.UI.Page

	Dim msFilter As String = ""
	Dim mlRecordID As Long
	Dim mlUnitTypeID As Long
	Dim mlTierID As Long
	Dim mlFloorID As Long
	Dim mlDepositTypeID As Long


	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Put user code to initialize the page here
		System.Threading.Thread.Sleep(500)
		Session("NodeID") = 1
		If IsPostBack Then
			Return
		Else : Session("msFilter") = ""
		End If

		'    Me.imgAdd.Visible = True

	End Sub



	Protected Sub DataGridDisplay_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DataGridDisplay.RowDataBound
		If DataGridDisplay.EditIndex <> -1 Then

			DataGridDisplay.ShowFooter = False

			'Me.cmdAddNew.Visible = False
			Me.imgAdd.Visible = False
		Else
			'  Me.cmdAddNew.Visible = True
			Me.imgAdd.Visible = True
		End If
	End Sub



	Protected Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
		Dim d As DropDownList
		d = Me.DataGridDisplay.FooterRow.FindControl("cboUnitTypeID")
		Dim lsValidate As String = ""
		lsValidate = d.Text()
		d = Me.DataGridDisplay.FooterRow.FindControl("cboRoomID")

		If lsValidate = "" Or d.Text = "" Then
			'MsgBox("You must enter a Unit Type Name.")
		Else
			ObjectDataSource1.Insert()
			DataGridDisplay.ShowFooter = False
		End If
	End Sub

	Protected Sub ObjectDataSource1_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles ObjectDataSource1.Inserting
		Dim t As TextBox
		Dim c As CheckBox
		Dim d As DropDownList

		t = Me.DataGridDisplay.FooterRow.FindControl("UnitName")
		If Not t Is Nothing Then
			Dim lsUnitName As String = t.Text
			e.InputParameters("lsUnitName") = lsUnitName
		End If

		d = Me.DataGridDisplay.FooterRow.FindControl("cboUnitTypeID")
		If Not d Is Nothing Then
			Dim lsUnitTypeID As String = d.SelectedValue
			e.InputParameters("lsUnitTypeID") = IIf(lsUnitTypeID = "", "", lsUnitTypeID)
		End If

		d = Me.DataGridDisplay.FooterRow.FindControl("cboRoomID")
		If Not d Is Nothing Then
			Dim lsRoomID As String = d.SelectedValue
			e.InputParameters("lsRoomID") = IIf(lsRoomID = "", "", lsRoomID)
		End If

		d = Me.DataGridDisplay.FooterRow.FindControl("cboDepositTypeID")
		If Not d Is Nothing Then
			Dim lsDepositType As String = d.SelectedValue
			e.InputParameters("lsDepositType") = IIf(lsDepositType = "", "N/A", lsDepositType)
		End If

		d = Me.DataGridDisplay.FooterRow.FindControl("cboTierID")
		If Not d Is Nothing Then
			Dim lsTier As String = d.SelectedValue
			e.InputParameters("lsTier") = IIf(lsTier = "", "", lsTier)
		End If

		c = Me.DataGridDisplay.FooterRow.FindControl("Available")
		If Not c Is Nothing Then
			Dim lsAvailable As String = c.Checked
			e.InputParameters("lsAvailable") = IIf(lsAvailable = "", "N/A", lsAvailable)
		End If

		d = Me.DataGridDisplay.FooterRow.FindControl("cboFloorID")
		If Not d Is Nothing Then
			Dim lsFloorID As String = d.SelectedValue
			e.InputParameters("lsFloorID") = IIf(lsFloorID = "", "N/A", lsFloorID)
		End If

		e.InputParameters("llNodeID") = Session("NodeID")

	End Sub

	Protected Sub cmdAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
		DataGridDisplay.EditIndex = -1
		DataGridDisplay.ShowFooter = True
	End Sub

	Protected Sub DataGridDisplay_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridDisplay.Load
		If DataGridDisplay.EditIndex <> -1 Then
			DataGridDisplay.ShowFooter = False
		End If
	End Sub

	Protected Sub DataGridDisplay_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles DataGridDisplay.RowEditing
		DataGridDisplay.ShowFooter = False
		' Dim t As TextBox
		Dim r As GridViewRow
		'  Dim C As CheckBox
		' Dim d As DropDownList
		Dim l As Literal
		Dim liRow As Integer


		liRow = e.NewEditIndex
		If liRow = -1 Then
		Else
			r = DataGridDisplay.Rows(liRow) 'DataGridDisplay.Rows(0) 'liRow) 
			Dim lsType As String
			lsType = r.FindControl("UnitTypeID").GetType.FullName
			If lsType <> "System.Web.UI.WebControls.Literal" Then
				Exit Sub
			End If
			l = r.FindControl("UnitTypeID")

			mlUnitTypeID = l.Text
			l = r.FindControl("TierID")
			If Not l Is Nothing Then mlTierID = l.Text
			l = r.FindControl("FloorID")
			If Not l Is Nothing Then mlFloorID = l.Text
			l = r.FindControl("DepositTypeID")
			If Not l Is Nothing Then mlDepositTypeID = l.Text



		End If
	End Sub

	Protected Sub ObjectDataSource1_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles ObjectDataSource1.Updating

		'    Dim t As TextBox
		Dim r As GridViewRow
		Dim C As CheckBox
		Dim d As DropDownList
		If Session("NodeID") Is Nothing Then Session("NodeID") = 0
		r = DataGridDisplay.Rows(DataGridDisplay.EditIndex)
		' t = r.FindControl("UnitName")
		' Dim lsUnitName As String = t.Text ' ((TextBox)gvIngredient.headerRow.FindControl("tbIngredient")).Text;

		' d = r.FindControl("cboDepositTypeID")
		' Dim lsDepositType As String = d.SelectedValue
		' d = r.FindControl("cboTierID")
		' Dim lsTier As String = d.SelectedValue
		' C = r.FindControl("Available")
		' Dim lsAvailable As String = C.Checked
		' d = r.FindControl("cboFloorID")
		' Dim lsFloorID As String = d.SelectedValue
		Dim l As Literal
		' l = r.FindControl("UnitID")
		' Dim lsUnitID As String = l.Text
		l = r.FindControl("UnitProfileID")
		Dim lsUnitProfileID As String = l.Text
		C = r.FindControl("Active")
		Dim lsActive As String = IIf(C.Checked = True, "True", "False")

		d = r.FindControl("cboUnitTypeID")
		Dim lsUnitTypeID As String = d.SelectedValue
		lsUnitTypeID = IIf(lsUnitTypeID = "", "", lsUnitTypeID)

		d = r.FindControl("cboRoomID")
		Dim lsRoomID As String = d.SelectedValue
		lsRoomID = IIf(lsRoomID = "", "", lsRoomID)

		'lsTier = IIf(lsTier = "", "", lsTier)
		'lsDepositType = IIf(lsDepositType = "", "N/A", lsDepositType)
		'lsAvailable = IIf(lsAvailable = "", "True", lsAvailable)
		'lsFloorID = IIf(lsFloorID = "", "1", lsFloorID)
		Dim lsNodeID As String = Session("NodeID")


		'e.InputParameters("lsUnitName") = lsUnitName
		e.InputParameters("lsUnitTypeID") = lsUnitTypeID
		'e.InputParameters("lsTier") = lsTier
		'e.InputParameters("lsDepositType") = lsDepositType

		' e.InputParameters("lsAvailable") = lsAvailable
		' e.InputParameters("lsFloorID") = lsFloorID
		e.InputParameters("lsRoomID") = lsRoomID
		e.InputParameters("llNodeID") = lsNodeID
		e.InputParameters("UnitProfileID") = lsUnitProfileID
		e.InputParameters("lsActive") = lsActive



	End Sub



	Protected Sub cmdRunReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRunReport.Click
		'ReportContainer1.Refresh()
		'Session("msFilter") = ReportContainer1.WhereClause
	End Sub


	Protected Sub ddlPagelength_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPagelength.SelectedIndexChanged
		DataGridDisplay.PageSize = ddlPagelength.SelectedValue
	End Sub

	Protected Sub DataGridDisplay_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DataGridDisplay.PageIndexChanging
		'   Stop
	End Sub

	Public Overrides Sub VerifyRenderingInServerForm(ByVal Control As Control)
		Return
	End Sub

	Protected Sub odsUnitTypes_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles odsUnitTypes.Selecting

		Dim lsNodeID As String = Session("NodeID")
		e.InputParameters("llNodeID") = lsNodeID
		e.InputParameters("lsWhere") = ""
		If mlUnitTypeID > 0 Then
			e.InputParameters("llID") = mlUnitTypeID
			e.InputParameters("lbActive") = True
		End If

	End Sub

	Protected Sub odsDepositConditions_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles odsDepositConditions.Selecting
		If Session("NodeID") Is Nothing Then Session("NodeID") = 1
		Dim lsNodeID As String = Session("NodeID")
		e.InputParameters("llNodeID") = lsNodeID
		e.InputParameters("lsWhere") = ""
		If mlUnitTypeID > 0 Then
			e.InputParameters("llID") = mlDepositTypeID
			e.InputParameters("lbActive") = True
		End If
	End Sub

	Protected Sub odsTiers_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles odsTiers.Selecting
		If Session("NodeID") Is Nothing Then Session("NodeID") = 1
		Dim lsNodeID As String = Session("NodeID")
		e.InputParameters("llNodeID") = lsNodeID
		e.InputParameters("lsWhere") = ""
		If mlUnitTypeID > 0 Then
			e.InputParameters("llID") = mlTierID
			e.InputParameters("lbActive") = True
		End If
	End Sub

	Protected Sub OdsFloors_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles OdsFloors.Selecting
		If Session("NodeID") Is Nothing Then Session("NodeID") = 1
		Dim lsNodeID As String = Session("NodeID")
		e.InputParameters("llNodeID") = lsNodeID
		e.InputParameters("lsWhere") = ""
		If mlUnitTypeID > 0 Then
			e.InputParameters("llID") = mlFloorID
			e.InputParameters("lbActive") = True
		End If
	End Sub


	Protected Sub imgExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgExcel.Click


		If Session("Export") = "Export" Then
			Session("Export") = ""
			Exit Sub
		End If
		Session("Export") = "Export"
		DataGridDisplay.AllowPaging = False
		DataGridDisplay.AllowSorting = False
		DataGridDisplay.DataBind()

		Dim strFileName As String = Trim(Left(Trim(Me.Title) & "                                    ", 31)) & ".xls"
		Dim ctrl As Control
		For Each ctrl In Me.Controls
			If ctrl.ID <> "DataGridDisplay" Then
				ctrl.Visible = False
			End If
		Next
		'Me.cboSurveyName.Visible = False

		Me.Header.Visible = False
		Response.Clear()
		Response.Buffer = True
		Response.ContentType = "application/vnd.ms-excel"
		Response.AddHeader("Content-Disposition", "attachment; filename=""" & strFileName & """")
		Response.ContentEncoding = System.Text.Encoding.UTF7
		Response.Charset = ""
		EnableViewState = False
		Dim oStringWriter As New System.IO.StringWriter()
		Dim oHTMLTextWriter As New System.Web.UI.HtmlTextWriter(oStringWriter)
		DataGridDisplay.RenderControl(oHTMLTextWriter)
		'form1.RenderControl(oHTMLTextWriter)
		Dim lsString As String = oStringWriter.ToString()
		Response.Write(lsString)
		EnableViewState = True
		DataGridDisplay.AllowPaging = True
		DataGridDisplay.AllowSorting = True
		DataGridDisplay.DataBind()

		DataGridDisplay.Focus()

	End Sub

	Protected Sub imgAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAdd.Click
		DataGridDisplay.EditIndex = -1
		DataGridDisplay.ShowFooter = True
	End Sub


End Class

