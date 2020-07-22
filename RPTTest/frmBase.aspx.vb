Imports System.IO
Imports Kolassa.DesignCenter.ReportManager
Partial Class frmBase
	Inherits System.Web.UI.Page
	Private mbBindData As Boolean = True
	Private miTimesLoaded As Integer
	Private mlRecordCount As Long
	Private Sub ShowMessage(Message As String, type As String)
		Dim m As String = fTakeOutQuotes(Message)
		Dim t As String = fTakeOutQuotes(type)
		Dim s As String = "$( document ).ready(function() {ShowMessage('" & m & "','" & t & "');});"
		ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", s, True)

		'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script1", "$( document ).ready(function() {aalert('Stop');});", True)
	End Sub
	Function fTakeOutQuotes(ByVal lsStr As String) As String
		lsStr = Replace(lsStr, "script", "scri_pt")
		lsStr = Replace(lsStr, """", "*")
		lsStr = Replace(lsStr, "'", "*")
		fTakeOutQuotes = Trim(lsStr)
	End Function
	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
		'ShowMessage("Hey", "Error")
		If mlRecordCount = 0 Then
			Exit Sub
		End If
		Dim iRows = grdData.Rows.Count
		If iRows = 0 Then
			mbBindData = True
		Else
			mbBindData = False
		End If
		miTimesLoaded = miTimesLoaded + 1
		'Put user creode to initialize the page here
		'rebindGrid()
		Dim asyncpb As Boolean '= upData.IsInPartialRendering
		Session("objType") = Request.QueryString("objType")
		Exit Sub
		If IsPostBack And Not asyncpb Then
			rebindGrid()
			Return
		End If

	End Sub
	Sub rebindGrid()
		Dim ls As String
		If txtSearch.Text.Trim = "" Then
			lblSearch.Text = ""
		Else
			ls = " SearchText like '%" & Replace(txtSearch.Text, "'", "''") & "%'"
			lblSearch.Text = ls
		End If

		odsData.DataBind()
		grdData.DataBind()

	End Sub
	Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdData.SelectedIndexChanged
		Stop
	End Sub
	Protected Sub cmd_cmdSQL(s As Object, e As EventArgs) Handles cmdSQL.Click
		Dim lsSQL As String = rptBase.f_CreateWhereClause(True)
		Response.Write(lsSQL)
		rptBase.Refresh()

		rptBase.DataBind()
	End Sub
	Protected Sub cmdNew_Click(sender As Object, e As EventArgs) 'Handles cmdNew.Click
		'Stop
		Try

			Dim s As String
			s = "12111111-1111-1111-1111-111122223333"
			LoadRecord(s)
			Exit Sub
		Catch
			Debug.Print(Err.Description)
		End Try
	End Sub
	Protected Sub cmdUPData_Click(sender As Object, e As EventArgs) Handles cmdUPData.Click
		'Stop
		Dim s As String
		s = txtID.Text
		LoadRecord(s)
	End Sub
	Sub LoadRecord(s As String)
		Try
			Dim c As New clsBases
			Dim ds As DataSet
			If s = "" Then
				ds = New DataSet
			Else
				ds = c.GetRecordData("", "", s, Request.QueryString("objType"), True, "ID = '" & s & "'", rptBase.TableName) '" & txtID.Text & "'", rptbase.tablename)
			End If
			rptBase.f_setFromDataRow(ds)
			'rptBase.DataBind()
			rptBase.TableName = rptBase.ReportControl.TableName
			'** The following line is needed to populate the controls on the ReportContainer
			rptBase.DataBind()
			rptBase.debugThis("Debug This")
			Exit Sub
		Catch
			Debug.Print(Err.Description)
		End Try
	End Sub

	Private Sub frm_Init(sender As Object, e As EventArgs) Handles Me.Init
		'Stop
		Debug.Print("frmBase Init")
		'ObjectDataSource1.TypeName="
		If rptBase Is Nothing Then Exit Sub
		If Request.QueryString("objType") Is Nothing Then
		Else
			If rptBase.ReportCategoryType <> Request.QueryString("objType") Then
				rptBase.ReportCategoryType = Request.QueryString("objType")
			End If
			'rptBase.DataBind()

		End If
	End Sub
	'
	Protected Sub ExportToExcel()
		Response.Clear()
		Response.Buffer = True
		Response.AddHeader("content-disposition", "attachment;filename=DataExport.xls")
		Response.Charset = ""
		Response.ContentType = "application/vnd.ms-excel"
		Using sw As New StringWriter()
			Dim hw As New HtmlTextWriter(sw)

			'To Export all pages
			grdData.AllowPaging = False
			grdData.DataBind() 'Me.BindGrid()

			'grdData.HeaderRow.BackColor = Color.White
			For Each cell As TableCell In grdData.HeaderRow.Cells
				cell.BackColor = grdData.HeaderStyle.BackColor
			Next
			'			For Each row As GridViewRow In grdData.Rows
			'	row.BackColor = Color.White
			'	For Each cell As TableCell In row.Cells
			'		If row.RowIndex Mod 2 = 0 Then
			'			cell.BackColor = grdData.AlternatingRowStyle.BackColor
			'				Else
			'				cell.BackColor = grdData.RowStyle.BackColor
			'				End If
			'				cell.CssClass = "textmode"
			'			Next
			'			Next

			grdData.RenderControl(hw)
			'style to format numbers to string
			Dim style As String = "<style> .textmode { } </style>"
			Response.Write(style)
			Response.Output.Write(sw.ToString())
			Response.Flush()
			Response.[End]()
		End Using
	End Sub
	Public Overrides Sub VerifyRenderingInServerForm(control As Control)
		' Verifies that the control is rendered
	End Sub



	Protected Sub grdData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdData.PageIndexChanging
		grdData.PageIndex = e.NewPageIndex
		grdData.DataBind()
	End Sub
	Protected Sub SaveRecord(sender As Object, e As EventArgs) Handles cmdSaveRecord.Click
		'	System.Threading.Thread.Sleep(5000)
		Debug.Print("Where Clause: " & rptBase.f_CreateWhereClause(False))
		rptBase.Refresh()

		If rptBase.f_LoadFromObject.Count = 0 Then
			rptBase.Refresh()


			Exit Sub
		End If
		Dim cBases As New clsBases
		Dim o As New clsBase
		o = cBases.GetObject(Request.QueryString("objType"))

		o.FormValue = rptBase.f_LoadFromObject
		o.TableName = rptBase.TableName
		o.processFormValues()
		If Not o.ErrorMessage Is Nothing Then
			If o.ErrorMessage.Length > 0 Then
				ShowMessage(o.ErrorMessage, "Error")
			End If
		Else
			ShowMessage("Record Saved Successfully", "Success")
		End If
		mbBindData = True
		'rebindGrid()
	End Sub

	Protected Sub odsData_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) ' Handles odsData.Selecting
		If mbBindData Or grdData.Rows.Count = 0 Then
			mbBindData = False
		Else
			e.Cancel = True
		End If
	End Sub
	Protected Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
		Dim c As New clsBases
		Dim ds As DataSet
		c.objType = "base"
		ds = c.GetRecordData("", "", "base", "'a199a285-612a-4e78-90dd-c115dc49acc2'", True, "ID = 'a199a285-612a-4e78-90dd-c115dc49acc2'", rptBase.TableName)
		rptBase.f_setFromDataRow(ds)

		rptBase.DataBind()

		'** The following line is needed to populate the controls on the ReportContainer
		rptBase.DataBind()
	End Sub

	Private Sub frmBase_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
		If IsPostBack Then
		Else
			If rptBase Is Nothing Then
			Else
				If rptBase.Controls.Count > 0 Then
					rptBase.btnSetReport_Click()
				End If

			End If
		End If
	End Sub



	Private Sub odsData_Selected(sender As Object, e As ObjectDataSourceStatusEventArgs) Handles odsData.Selected
		'Stop
		Dim la As DataSet

		la = e.ReturnValue
		mlRecordCount = la.Tables.Count
	End Sub

	Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
		rebindGrid()
	End Sub

	Protected Sub cmdExcelexp_Click(sender As Object, e As EventArgs) Handles cmdExcelexp.Click
		ExportToExcel()
	End Sub
End Class
