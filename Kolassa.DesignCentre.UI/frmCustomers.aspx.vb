Imports System.IO
Partial Class frmCustomers
	Inherits System.Web.UI.Page



	Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		'Put user creode to initialize the page here
		'rebindGrid()

		If IsPostBack Then
			rebindGrid()
			Return
		End If
	End Sub

	Protected Sub grdCustomers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles grdCustomers.SelectedIndexChanged
		Stop
	End Sub

	Protected Sub cmdUPCustomer_Click(sender As Object, e As EventArgs) Handles cmdUPCustomer.Click
		'Stop
		ctrlCustomers1.DataBind()
	End Sub

	Private Sub frmCustomers_Init(sender As Object, e As EventArgs) Handles Me.Init
		'ObjectDataSource1.TypeName="
	End Sub

	Protected Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
		rebindGrid()
	End Sub
	Sub rebindGrid()
		Dim ls As String = "SearchText like '%" & Replace(txtSearch.Text, "'", "''") & "%'"
		lblSearch.Text = ls
		'odsData.DataBind()
		grdCustomers.DataBind()

	End Sub
	Protected Sub ExportToExcel()
		Response.Clear()
		Response.Buffer = True
		Response.AddHeader("content-disposition", "attachment;filename=CustomerExport.xls")
		Response.Charset = ""
		Response.ContentType = "application/vnd.ms-excel"
		Using sw As New StringWriter()
			Dim hw As New HtmlTextWriter(sw)

			'To Export all pages
			grdCustomers.AllowPaging = False
			grdCustomers.DataBind() 'Me.BindGrid()

			'grdCustomers.HeaderRow.BackColor = Color.White
			For Each cell As TableCell In grdCustomers.HeaderRow.Cells
				cell.BackColor = grdCustomers.HeaderStyle.BackColor
			Next
			'			For Each row As GridViewRow In grdCustomers.Rows
			'	row.BackColor = Color.White
			'	For Each cell As TableCell In row.Cells
			'		If row.RowIndex Mod 2 = 0 Then
			'			cell.BackColor = grdCustomers.AlternatingRowStyle.BackColor
			'				Else
			'				cell.BackColor = grdCustomers.RowStyle.BackColor
			'				End If
			'				cell.CssClass = "textmode"
			'			Next
			'			Next

			grdCustomers.RenderControl(hw)
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

	Protected Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
		ExportToExcel()
	End Sub

	Protected Sub grdCustomers_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdCustomers.PageIndexChanging
		grdCustomers.PageIndex = e.NewPageIndex
		grdCustomers.DataBind()
	End Sub

	Protected Sub cmdNewCustomer_Click(sender As Object, e As EventArgs) Handles cmdNewCustomer.Click
		ctrlCustomers1.Insert()
		ctrlCustomers1.NewCustomerRecord()
	End Sub
	Protected Sub cmdNewCust_Click(sender As Object, e As EventArgs) Handles cmdNewCust.Click
		ctrlCustomers1.Insert()
		ctrlCustomers1.NewCustomerRecord()
	End Sub
	Sub NewCust()
		ctrlCustomers1.Insert()
		ctrlCustomers1.NewCustomerRecord()
	End Sub
	Protected Sub cmdNewRec_Click(sender As Object, e As EventArgs) Handles cmdNewRec.Click
		NewCust()
		Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "none", "<script>popup();</script>", False)
	End Sub
End Class
