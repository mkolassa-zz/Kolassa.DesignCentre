Partial Class frmUnitTypes
    Inherits System.Web.UI.Page
    Dim msFilter As String = ""
    Dim mlRecordID As Long
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.

    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        System.Threading.Thread.Sleep(500)

        If IsPostBack Then
            ' MsgBox(Session("msFilter"))
            Return
        Else : Session("msFilter") = ""
        End If
    End Sub


    Protected Sub DataGridDisplay_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridDisplay.SelectedIndexChanged
        If DataGridDisplay.SelectedIndex <> -1 Then
            DataGridDisplay.ShowFooter = False
        End If
    End Sub

    Protected Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim t As TextBox

        t = Me.DataGridDisplay.FooterRow.FindControl("UnitTypeName")
        Dim lsName As String = t.Text ' ((TextBox)gvIngredient.headerRow.FindControl("tbIngredient")).Text;
        't = Me.DataGridDisplay.FooterRow.FindControl("VendorContact")
        'Dim VendorContact As String = t.Text
        't = Me.DataGridDisplay.FooterRow.FindControl("VendorAbbreviation")
        'Dim VendorAbbreviation As String = t.Text
        If lsName = "" Then
            MsgBox("You must enter a Unit Type Name.")
        Else
            ObjectDataSource1.Insert()
            DataGridDisplay.ShowFooter = False
        End If


    End Sub



    Protected Sub ObjectDataSource1_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles ObjectDataSource1.Inserting
        Dim t As TextBox
        t = Me.DataGridDisplay.FooterRow.FindControl("UnitTypeName")
        Dim UnitTypeName As String = t.Text ' ((TextBox)gvIngredient.headerRow.FindControl("tbIngredient")).Text;
        t = Me.DataGridDisplay.FooterRow.FindControl("UnitTypeDescription")
        Dim UnitTypeDescription As String = t.Text
        t = Me.DataGridDisplay.FooterRow.FindControl("SubType")
        Dim SubType As String = t.Text
        t = Me.DataGridDisplay.FooterRow.FindControl("Level")
        Dim Level As String = t.Text
        t = Me.DataGridDisplay.FooterRow.FindControl("ModelUnit")
        Dim ModelUnit As String = t.Text

        e.InputParameters("lsUnitTypeName") = UnitTypeName
        e.InputParameters("lsUnitTypeDescription") = IIf(UnitTypeDescription = "", "", UnitTypeDescription)
        e.InputParameters("lsSubType") = IIf(SubType = "", "", SubType)
        e.InputParameters("lsLevel") = IIf(Level = "", "N/A", Level)
        e.InputParameters("lsModelUnit") = IIf(ModelUnit = "", "N/A", ModelUnit)
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
    End Sub

    Protected Sub ObjectDataSource1_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles ObjectDataSource1.Updating
        Dim t As TextBox
        Dim r As GridViewRow
        r = DataGridDisplay.Rows(DataGridDisplay.EditIndex)

        t = r.FindControl("UnitTypeName")
        Dim UnitTypeName As String = t.Text ' ((TextBox)gvIngredient.headerRow.FindControl("tbIngredient")).Text;
        t = r.FindControl("UnitTypeDescription")
        Dim UnitTypeDescription As String = t.Text
        t = r.FindControl("SubType")
        Dim SubType As String = t.Text
        t = r.FindControl("Level")
        Dim level As String = t.Text
        t = r.FindControl("ModelUnit")
        Dim ModelUnit As String = t.Text
        Dim c As CheckBox
        c = r.FindControl("chkActive")
        Dim lsActive = IIf(c.Checked = True, "True", "False")
        Dim l As Literal
        l = r.FindControl("UnitTypeID")
        Dim UnitTypeID As String = l.Text
        e.InputParameters("UnitTypeID") = UnitTypeID
        e.InputParameters("UnitTypeName") = UnitTypeName
        e.InputParameters("UnitTypeDescription") = IIf(UnitTypeDescription = "", "", UnitTypeDescription)
        e.InputParameters("SubType") = IIf(SubType = "", "", SubType)
        e.InputParameters("lslevel") = IIf(level = "", "", level)
        e.InputParameters("ModelUnit") = IIf(ModelUnit = "", "", ModelUnit)
        e.InputParameters("lsActive") = lsActive

    End Sub



    Protected Sub cmdRunReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRunReport.Click
		'   ReportContainer1.Refresh()
		'Session("msFilter") = ReportContainer1.WhereClause
	End Sub


    Protected Sub ddlPagelength_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPagelength.SelectedIndexChanged
        DataGridDisplay.PageSize = ddlPagelength.SelectedValue
    End Sub

    Protected Sub DataGridDisplay_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DataGridDisplay.PageIndexChanging
        '   Stop
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        If DropDownList1.SelectedValue = "ExportToExcel" Then

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
            Me.DropDownList1.Visible = False
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

            DataGridDisplay.AllowPaging = True
            DataGridDisplay.AllowSorting = True
            DataGridDisplay.DataBind()

        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal Control As Control)
        Return
    End Sub
    ' Protected Sub DataGridDisplay_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DataGridDisplay.RowDeleting
    ' Dim lit As Literal
    '     lit = DataGridDisplay.Rows(e.RowIndex).Cells(0).FindControl("RecordID")
    '     mlRecordID = CLng(lit.Text)
    ' End Sub
    ' Protected Sub ObjectDataSource1_Deleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles ObjectDataSource1.Deleting
    '     e.InputParameters("RecordID") = mlRecordID
    ' End Sub
End Class
