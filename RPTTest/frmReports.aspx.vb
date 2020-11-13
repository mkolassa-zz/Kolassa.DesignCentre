Public Class frmReports
    Inherits System.Web.UI.Page

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
        ShowMessage("Aww, password is wrong", "Error")
    End Sub
    Public Sub ShowMessage(Message As String, lsType As String)
        'Show Bootstrap Message
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "myScript" & Guid.NewGuid.ToString, "ShowMessage('" & Message & "','" & lsType & "','');", True)
    End Sub
    Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack Or 1 = 1 Then
        End If
        If Session("NodeID") Is Nothing Then Session("NodeID") = 0
        'Session("ProjectID") = "742d682d-278f-4cf3-b527-c9115c5028a7"
    End Sub

    Private Sub _Default_PreInit(sender As Object, e As EventArgs) Handles Me.Init

        ReportResults.liReportID = ReportContainer1.ReportID
        ReportResults.Attributes("data-reportdesc") = ReportContainer1.ReportDescription
        ReportResults.Attributes("data-reportwheredesc") = ReportContainer1.ReportOut
        ReportResults.ReportDesc = ReportContainer1.ReportDescription
    End Sub
    'Protected Sub ObjectDataSource1_Selected(sender As Object, e As ObjectDataSourceStatusEventArgs) Handles ObjectDataSource1.Selected
    'End Sub
    Protected Overrides Sub LoadViewState(savedState As Object)

    End Sub



    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        '*** This is here to get the Server Controls to kick off form level events
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
        sb.Append("printWin.close();};")
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
        sb.AppendLine("setTimeout(function(){window.close();}, 1); /* In fact timeout doesn't start until print dialog closes */")
        sb.AppendLine("printWin.close();};")
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
            '			For Each row As GridViewRow In gv.Rows
            '	row.BackColor = Color.White
            '	For Each cell As TableCell In row.Cells
            '		If row.RowIndex Mod 2 = 0 Then
            '			cell.BackColor = gv.AlternatingRowStyle.BackColor
            '				Else
            '				cell.BackColor = gv.RowStyle.BackColor
            '				End If
            '				cell.CssClass = "textmode"
            '			Next
            '			Next

            gv.RenderControl(hw)
            'style to format numbers to string
            Dim style As String = "<style> .textmode { } </style>"
            Page.Response.Write(style)
            Page.Response.Output.Write(sw.ToString())
            Page.Response.Flush()
            Page.Response.[End]()
        End Using
    End Sub

    Protected Sub lnkLoadColumns_Click(sender As Object, e As EventArgs)
        ctrlReportColumns1.SetColumns(Val(Request.QueryString("rpt")))
        Dim title As String = "Columns"
        Dim body As String = ""
        ClientScript.RegisterStartupScript(Me.GetType(), "Popup", "ShowPopup('" & title & "', '" & body & "');", True)
    End Sub
End Class