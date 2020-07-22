Imports System.IO
Partial Class frmRooms
	Inherits System.Web.UI.Page



	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Put user creode to initialize the page here
		'rebindGrid()

		If IsPostBack Then
			rebindGrid()
			Return
		End If
	End Sub

	Protected Sub grd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdData.SelectedIndexChanged
		Stop
	End Sub

	Protected Sub cmdUPData_Click(sender As Object, e As EventArgs) Handles cmdUPData.Click
		'Stop
		ctrlCustomers1.DataBind()
	End Sub

	Private Sub frm_Init(sender As Object, e As EventArgs) Handles Me.Init
		'ObjectDataSource1.TypeName="
	End Sub

	Protected Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
		rebindGrid()
	End Sub
	Sub rebindGrid()
		Dim ls As String = "SearchText like '%" & Replace(txtSearch.Text, "'", "''") & "%'"
		lblSearch.Text = ls
		'odsData.DataBind()
		grdData.DataBind()

	End Sub
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

	Protected Sub cmdExcel_Click(sender As Object, e As ImageClickEventArgs) Handles cmdExcel.Click
		ExportToExcel()
	End Sub

	Protected Sub grdData_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdData.PageIndexChanging
		grdData.PageIndex = e.NewPageIndex
		grdData.DataBind()
	End Sub
End Class
