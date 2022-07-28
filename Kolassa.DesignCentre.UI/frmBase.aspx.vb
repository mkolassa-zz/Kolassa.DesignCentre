Imports System.IO
Imports System.Diagnostics
Partial Class frmBase
	Inherits System.Web.UI.Page
	Private mbBindData As Boolean = True
	Private miTimesLoaded As Integer
	Private mlRecordCount As Long
    Protected Sub lnkShowColumns_Click(sender As Object, e As EventArgs) Handles lnkShowColumns.Click
        LoadColumnModal()
    End Sub
    Private Sub ShowMessage(Message As String, type As String)
		Dim m As String = fTakeOutQuotes(Message)
        Dim t As String = fTakeOutQuotes(type)
        m = m.Replace(vbCrLf, " ")
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
		'Dim iRows = grdData.Rows.Count
		'If iRows = 0 Then
		'	mbBindData = True
		'Else
		'	mbBindData = False
		'End If
		miTimesLoaded = miTimesLoaded + 1
		'Put user code to initialize the page here
		'rebindGrid()
		'Dim asyncpb As Boolean '= upData.IsInPartialRendering
		Session("objType") = Request.QueryString("objType")

        Exit Sub
	End Sub

    Protected Sub cmd_cmdSQL(s As Object, e As EventArgs) Handles cmdSQL.Click
		Try
			Dim lsSQL As String = rptBase.f_CreateWhereClause(True)
			Response.Write(lsSQL)
			rptBase.Refresh()

			rptBase.DataBind()
		Catch
			ShowMessage(Err.Description, "Error")
		End Try
	End Sub
	Protected Sub cmdNew_Click(sender As Object, e As EventArgs) 'Handles cmdNew.Click
		'Stop
		Try

			Dim s As String
			s = "12111111-1111-1111-1111-111122223333"
			LoadRecord(s)
			Exit Sub
		Catch
			'debug.print(Err.Description)
		End Try
	End Sub
	Protected Sub cmdUPData_Click(sender As Object, e As EventArgs) Handles cmdUPData.Click
		Dim s As String
		s = txtID.Text
		LoadRecord(s)
	End Sub
	Protected Sub cmdDelData_Click(sender As Object, e As EventArgs) Handles cmdDelData.Click
		Dim s As String
		s = txtID.Text
		DeleteRecord(s)
		ReportResults.bindgv()
	End Sub
	Sub DeleteRecord(s As String)
		rptBase.debugThis("<frmBase.DeleteRecords>" & s)
		Try
			Dim c As New clsBases
			Dim co As New clsBase
			Dim ds As DataSet
			If s = "" Then
				ds = New DataSet
			Else
				Dim sWhere = "ID = '" & s & "'" ') '" & txtID.Text & "'"
				ds = c.GetRecordData("", "", s, Request.QueryString("objType"), True, sWhere, rptBase.TableName)
				co = c.GetObject(Request.QueryString("objType"))
				co.TableName = "tbl" & Request.QueryString("objType")
				co.ID = s
				co.Delete()
			End If

			rptBase.f_setFromDataRow(ds)
			rptBase.TableName = ds.Tables(0).TableName 'MAYBE PUT THIS BACK rptBase.ReportControl.TableName
			'** The following line is needed to populate the controls on the ReportContainer
			rptBase.DataBind()
			rptBase.debugThis("Debug This")
		Catch
			rptBase.debugThis("<Err>" & Err.Description & "</Err>")
			ShowMessage(Err.Description, "Error")
		End Try
		rptBase.debugThis("</frmBase.DeleteRecords>" & s)
	End Sub
	Sub LoadRecord(s As String)
		rptBase.debugThis("<frmBase.LoadRecords>" & s)
		Try
			Dim c As New clsBases
			Dim ds As DataSet
			If s = "" Then
				ds = New DataSet
			Else
				Dim sWhere = "ID = '" & s & "'" ') '" & txtID.Text & "'"
				ds = c.GetRecordData("", "", s, Request.QueryString("objType"), True, sWhere, rptBase.TableName)
			End If

			rptBase.f_setFromDataRow(ds)
			'rptBase.DataBind()
			rptBase.TableName = ds.Tables(0).TableName 'MAYBE PUT THIS BACK rptBase.ReportControl.TableName
			'** The following line is needed to populate the controls on the ReportContainer
			rptBase.DataBind()
			rptBase.debugThis("Debug This")
			ctrlImages1.ObjectID = s
			ctrlContactEntry1.Attributes.Remove("objectID")
			ctrlContactEntry1.ObjectID = s
			'Exit Sub
		Catch
			rptBase.debugThis("<Err>" & Err.Description & "</Err>")
			ShowMessage(Err.Description, "Error")
		End Try
		rptBase.debugThis("</frmBase.LoadRecords>" & s)
	End Sub

	Private Sub frm_Init(sender As Object, e As EventArgs) Handles Me.Init
		rptBase.debugThis("<frmBase Init>")
		'ObjectDataSource1.TypeName="
		If rptBase Is Nothing Then Exit Sub
		If Request.QueryString("objType") Is Nothing Then
		Else
			If rptBase.ReportCategoryType <> Request.QueryString("objType") Then
				rptBase.ReportCategoryType = Request.QueryString("objType")

			End If
		End If
		lblReportLabel.Text = rptBase.ReportName
		rptBase.debugThis("</frmBase Init>")
	End Sub

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
		' Verifies that the control is rendered
	End Sub



    'Protected Sub grdData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdData.PageIndexChanging
    '	grdData.PageIndex = e.NewPageIndex
    '	grdData.DataBind()
    'End Sub
    Protected Sub SaveRecord(sender As Object, e As EventArgs) Handles cmdSaveRecord.Click
		Dim cBases As New clsBases
		Dim o As New clsBase

		'debug.print("Where Clause: " & rptBase.f_CreateWhereClause(False))
		rptBase.Refresh()

		If rptBase.f_LoadFromObject.Count = 0 Then
			rptBase.Refresh()
			Exit Sub
		End If

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
		ReportResults.bindgv()
		'rebindGrid()
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
		lblReportLabel.Text = rptBase.ReportDescription
		lblReportFormLabel.Text = rptBase.ReportDescription & " Entry Form"
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
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        'rebindGrid()
        If txtSearch.Text.Length > 1 Then
            ReportResults.SearchCriteria = " SearchText like '%" & fTakeOutQuotes(txtSearch.Text) & "%' "
        End If
        ReportResults.bindgv()
    End Sub
    '********* From frmReports
    Protected Overrides Sub LoadViewState(savedState As Object)

	End Sub

	Sub LoadColumnModal()
		Dim liReportID = ReportContainer1.f_GetReportID

		ctrlReportColumns1.SetColumns(liReportID)
		ctrlReportColumns1.ReportID = liReportID
		Dim title As String = "Columns"
		Dim body As String = ""
		ClientScript.RegisterStartupScript(Me.GetType(), "Popup", "ShowPopup('" & title & "', '" & body & "');", True)
	End Sub
	Protected Sub btnGetWhereClause_Click(sender As Object, e As EventArgs) Handles lnkGetWhereClause.Click
		' Exit Sub
		ReportContainer1.Refresh()
		lblModalTitle.Text = "SQL Where Clause Results"
		Dim lsOutput As String = "<div class='card'>"
		lsOutput = lsOutput & "<h5 class='card-title'>Actual Where Clause</h5>" & ReportContainer1.WhereClause & "</div>"
		lsOutput = lsOutput & "<div class='card'> "
		lsOutput = lsOutput & "<h5 class='card-title'>Parameters</h5>" & ReportContainer1.ReportOut & "</div>"

		lblModalBody.Text = lsOutput
		'  ObjectDataSource1.SelectParameters.Clear()
		' ObjectDataSource1.SelectParameters.Add("lsSQL", ReportContainer1.SelectStatement)
		' ObjectDataSource1.SelectParameters.Add("lsWhere", ReportContainer1.WhereClause)
		ReportResults.ReportWhereClause = ReportContainer1.WhereClause
		ReportResults.ReportWhereDesc = ReportContainer1.ReportOut


		ReportResults.bindgv()
		ReportResults.Attributes("data-reportdesc") = ReportContainer1.ReportDescription
		ReportResults.Attributes("data-reportwheredesc") = ReportContainer1.ReportOut
		ReportResults.ReportDesc = ReportContainer1.ReportDescription
		'	ShowMessage("Aww, password is wrong", "Error")
	End Sub


    Private Sub _Default_PreInit(sender As Object, e As EventArgs) Handles Me.Init

		If ReportContainer1.ReportID = 0 Then
			If Request.QueryString("rpt") = "" Then
                ReportResults.liReportID = ReportContainer1.f_GetReportID ' Take this out takethisout
            Else
				ReportResults.liReportID = Val(Request.QueryString("rpt"))
			End If
		Else
			ReportResults.liReportID = ReportContainer1.ReportID
		End If
		ReportResults.Attributes("data-reportdesc") = ReportContainer1.ReportDescription
		ReportResults.Attributes("data-reportwheredesc") = ReportContainer1.ReportOut
		ReportResults.ReportDesc = ReportContainer1.ReportDescription
	End Sub

	Protected Sub PrintCurrentPage(ByVal sender As Object, ByVal e As EventArgs) Handles lnkdPrintPage.Click
		Dim gv As GridView
		gv = ReportResults.FindControl("gvResults")
		gv.PagerSettings.Visible = False
		gv.DataBind()
		Dim sw As New System.IO.StringWriter()
		Dim hw As New HtmlTextWriter(sw)

		gv.RenderControl(hw)
		Dim gridHTML As String = sw.ToString().Replace("""", "'").Replace(System.Environment.NewLine, "")
		Dim sb As New StringBuilder()

		sb.Append("<script type = 'text/javascript'>")
		sb.Append("window.onload = new function(){")
		sb.Append("var printWin = window.open('', '', 'left=0")
		sb.AppendLine(",top=0,width=1000,height=600,status=0');")
		sb.Append("printWin.document.write(""")
		Dim lsReportDesc As String = ReportResults.Attributes("data-reportdesc")
		Dim lsWhereDesc As String = ReportResults.Attributes("data-reportwheredesc")
		Dim lsTitle As String = "<h2>" & lsReportDesc & "</h2><br/><h3>" & lsWhereDesc & "</h3><br/>"
		sb.Append(lsTitle)
		sb.Append(gridHTML)
		sb.Append(""");")
		sb.AppendLine("printWin.document.close();")
		sb.AppendLine("printWin.focus();")
		sb.AppendLine("printWin.print();")
		sb.AppendLine("setTimeout(function(){window.close();}, 1); /* In fact timeout doesn't start until print dialog closes */")
		'	sb.Append("printWin.close();")
		sb.Append("};")
		sb.Append("</script>")
		Page.ClientScript.RegisterStartupScript(Me.GetType(), "GridPrint", sb.ToString())
		gv.PagerSettings.Visible = True
		gv.DataBind()

	End Sub
	Protected Sub PrintAllPages(ByVal sender As Object, ByVal e As EventArgs) Handles lnkPrintAll.Click
		Dim gv As GridView
		gv = ReportResults.FindControl("gvResults")
		gv.AllowPaging = False

		gv.DataBind()
		Dim sw As New System.IO.StringWriter()
		Dim hw As New HtmlTextWriter(sw)

		gv.RenderControl(hw)

		Dim gridHTML As String = sw.ToString().Replace("""", "'").Replace(System.Environment.NewLine, "")
		Dim sb As New StringBuilder()

		sb.Append("<script type = 'text/javascript'>")
		sb.AppendLine("window.onload = new function(){")
		sb.Append("var printWin = window.open('', '', 'left=0")
		sb.AppendLine(",top=0,width=1000,height=1000,status=0');")
		sb.Append("printWin.document.write(""")
		Dim lsReportDesc As String = ReportResults.Attributes("data-reportdesc")
		Dim lsWhereDesc As String = ReportResults.Attributes("data-reportwheredesc")
		Dim lsTitle As String = "<h2>" & lsReportDesc & "</h2><br/><h3>" & lsWhereDesc & "</h3><br/>"
		sb.Append(lsTitle)
		sb.Append(gridHTML)
		sb.AppendLine(""");")
		sb.AppendLine("printWin.document.close();")

		sb.AppendLine("printWin.focus();")
		sb.AppendLine("printWin.print();")
		'		sb.AppendLine("setTimeout(function(){window.close();}, 1); /* In fact timeout doesn't start until print dialog closes */")
		'	sb.AppendLine("printWin.close();")
		sb.AppendLine("};")
		sb.Append("</script>")
		Page.ClientScript.RegisterStartupScript(Me.[GetType](), "GridPrint", sb.ToString())
		gv.AllowPaging = True
		gv.DataBind()

	End Sub

	Protected Sub ExportToExcel() Handles lnkExport.Click
		Dim gv As GridView
		gv = ReportResults.FindControl("gvResults")
		Page.Response.Clear()
		Page.Response.Buffer = True
		Page.Response.AddHeader("content-disposition", "attachment;filename=DataExport.xls")
		Page.Response.Charset = ""
		Page.Response.ContentType = "application/vnd.ms-excel"
		Using sw As New System.IO.StringWriter()
			Dim hw As New HtmlTextWriter(sw)

			'To Export all pages
			gv.AllowPaging = False
			gv.DataBind() 'Me.BindGrid()
			If gv.Rows.Count = 0 Then Exit Sub
			'gv.HeaderRow.BackColor = Color.White
			For Each cell As TableCell In gv.HeaderRow.Cells
				cell.BackColor = gv.HeaderStyle.BackColor
			Next


			gv.RenderControl(hw)
			'style to format numbers to string
			Dim style As String = "<style> .textmode { } </style>"
			Page.Response.Write(style)
			Page.Response.Output.Write(sw.ToString())
			Page.Response.Flush()
			Page.Response.[End]()
		End Using
	End Sub

	Private Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        Dim ls As String
        ls = "sdf"
    End Sub
    Protected Sub uploadcomplete(sender As Object, e As AjaxControlToolkit.AjaxFileUploadEventArgs) Handles fuCSV.UploadComplete
        Dim c As New clsTestCSV
        Dim lsFileName As String
        Dim fPath As String = "~/customerfiles"

        If Not Directory.Exists(fPath) Then Directory.CreateDirectory(fPath)

        Dim lsCust As String = Session("NodeID").ToString
        lsCust = "000" & Trim(lsCust)
        lsCust = Right(lsCust, 3)

        '  fPath = fPath & "/cust" & lsCust
        '  If Not Directory.Exists(fPath) Then Directory.CreateDirectory(fPath)
        '  Dim fileNametoupload As String = Server.MapPath(fPath) + "\" + Guid.NewGuid.ToString + e.FileName.ToString()
        '  fileNametoupload = fPath + "\" + Guid.NewGuid.ToString + e.FileName.ToString()


        lsFileName = ConfigurationManager.AppSettings("uploadFolder") & "/" & Guid.NewGuid.ToString & fTakeOutQuotes(e.FileName)
        fuCSV.SaveAs(lsFileName)
        Session("objType") = Request.QueryString("objType")
        c.ObjectType = Session("objType")
        c.csvReadTest(lsFileName)
        c.csvWriteTest()

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Dim lsx As String
		lsx = "asdf"
	End Sub

    Private Sub tcEditData_Load(sender As Object, e As EventArgs) Handles tcEditData.Load
        Dim x As Integer
        x = 4
    End Sub

    'Sub rebindGrid()
    '	Try
    '		Dim ls As String
    '		If txtSearch.Text.Trim = "" Then
    '			lblSearch.Text = ""
    '		Else
    '			ls = " SearchText like '%" & Replace(txtSearch.Text, "'", "''") & "%'"
    '			lblSearch.Text = ls
    '		End If

    '		odsData.DataBind()

    '		grdData.DataBind()
    '	Catch
    '		ShowMessage(Err.Description, "Error")
    '	End Try
    'End Sub
    'Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdData.SelectedIndexChanged
    '	'Stop
    '	Dim liNothing As Integer
    '	For liNothing = 1 To 10
    '		'debug.print("Hey")
    '	Next
    'End Sub
    'Public Sub ShowMessage(Message As String, lsType As String)
    '	'Show Bootstrap Message
    '	ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "myScript" & Guid.NewGuid.ToString, "ShowMessage('" & Message & "','" & lsType & "','');", True)
    'End Sub
    'Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
    '	If Me.IsPostBack Or 1 = 1 Then
    '	End If
    '	If Session("NodeID") Is Nothing Then Session("NodeID") = 0
    '	Session("ProjectID") = "742d682d-278f-4cf3-b527-c9115c5028a7"
    'End Sub
    'Protected Sub odsData_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) ' Handles odsData.Selecting
    '	If mbBindData Or grdData.Rows.Count = 0 Then
    '		mbBindData = False
    '	Else
    '		e.Cancel = True
    '	End If
    'End Sub
    'Private Sub odsData_Selected(sender As Object, e As ObjectDataSourceStatusEventArgs) Handles odsData.Selected
    '	'Stop
    '	Dim la As DataSet

    '	la = e.ReturnValue
    '	If la Is Nothing Then
    '		'ShowMessage("There Seems to be a Problem")
    '		Exit Sub
    '	End If
    '	mlRecordCount = la.Tables.Count
    'End Sub
    '
    'Protected Sub ExportToExcel()
    '	Response.Clear()
    '	Response.Buffer = True
    '	Response.AddHeader("content-disposition", "attachment;filename=DataExport.xls")
    '	Response.Charset = ""
    '	Response.ContentType = "application/vnd.ms-excel"
    '	Using sw As New StringWriter()
    '		Dim hw As New HtmlTextWriter(sw)

    '		'To Export all pages
    '		grdData.AllowPaging = False
    '		grdData.DataBind() 'Me.BindGrid()

    '		'grdData.HeaderRow.BackColor = Color.White
    '		For Each cell As TableCell In grdData.HeaderRow.Cells
    '			cell.BackColor = grdData.HeaderStyle.BackColor
    '		Next
    '		'			For Each row As GridViewRow In grdData.Rows
    '		'	row.BackColor = Color.White
    '		'	For Each cell As TableCell In row.Cells
    '		'		If row.RowIndex Mod 2 = 0 Then
    '		'			cell.BackColor = grdData.AlternatingRowStyle.BackColor
    '		'				Else
    '		'				cell.BackColor = grdData.RowStyle.BackColor
    '		'				End If
    '		'				cell.CssClass = "textmode"
    '		'			Next
    '		'			Next

    '		grdData.RenderControl(hw)
    '		'style to format numbers to string
    '		Dim style As String = "<style> .textmode { } </style>"
    '		Response.Write(style)
    '		Response.Output.Write(sw.ToString())
    '		Response.Flush()
    '		Response.[End]()
    '	End Using
    'End Sub
    '	Protected Sub cmdExcelexp_Click(sender As Object, e As EventArgs) Handles cmdExcelexp.Click
    '		ExportToExcel()
    '	End Sub
End Class
