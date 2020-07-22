Imports System.IO
Public Class frmProjects
    Inherits System.Web.UI.Page
    Dim msFilter As String = ""
    Dim mlRecordID As Long
    Dim msID As String
    Dim mlRow As Long


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '*** Put user code to initialize the page here
        Page.Form.Enctype = "multipart/form-data"
        System.Threading.Thread.Sleep(500)
        Session("NodeID") = 1
        If IsPostBack Then
            ' MsgBox(Session("msFilter"))
            Return
        Else : Session("msFilter") = ""
        End If
        'Me.cmdAddNew.Visible = True
        Me.imgAdd.Visible = True

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
        Dim t As TextBox
        t = Me.DataGridDisplay.FooterRow.FindControl("Name")
        Dim lsName As String = t.Text ' ((TextBox)gvIngredient.headerRow.FindControl("tbIngredient")).Text;
        If lsName = "" Then
            'MsgBox("You must enter a Name.")
        Else
            ObjectDataSource1.Insert()
            DataGridDisplay.ShowFooter = False
        End If


    End Sub

    Protected Sub ObjectDataSource1_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles ObjectDataSource1.Inserting
        Dim t As TextBox
        'Dim c As CheckBox
        Dim d As DropDownList
        t = Me.DataGridDisplay.FooterRow.FindControl("Name")
        Dim lsName As String = t.Text ' ((TextBox)gvIngredient.headerRow.FindControl("tbIngredient")).Text;
        t = Me.DataGridDisplay.FooterRow.FindControl("txtDescription")
        Dim lsDescription As String = t.Text
        ' t = Me.DataGridDisplay.FooterRow.FindControl("Image")
       ' Dim lsImage As String = t.Text


        d = Me.DataGridDisplay.FooterRow.FindControl("cboProjectTYpe")
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
        'Dim t As TextBox
        'Dim C As CheckBox
        'Dim d As DropDownList
        Dim r As GridViewRow
        Dim l As Literal
        Dim liRow As Integer

        liRow = e.NewEditIndex
        If liRow = -1 Then
        Else
            r = DataGridDisplay.Rows(liRow) 'DataGridDisplay.Rows(0) 'liRow) 
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

    Protected Sub ObjectDataSource1_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles ObjectDataSource1.Updating
        Dim l As Literal
        Dim t As TextBox
        Dim r As GridViewRow
        Dim C As CheckBox
        Dim d As DropDownList
        If Session("NodeID") Is Nothing Then Session("NodeID") = 0
        r = DataGridDisplay.Rows(DataGridDisplay.EditIndex)
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
    Private Sub ObjectDataSource1_Deleting(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles ObjectDataSource1.Deleting

        e.InputParameters("RecordID") = msID
    End Sub


    Protected Sub cmdRunReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRunReport.Click
		' ReportContainer1.Refresh()
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

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Read the file and convert it to Byte Array

        Dim r As GridViewRow = DataGridDisplay.Rows(Session("Row"))

        Dim lbl As Label
        lbl = r.FindControl("lblMessage")
        If lbl Is Nothing Then
            MsgBox("Something went wrong.")
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

    Private Sub DataGridDisplay_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles DataGridDisplay.RowDeleting
        Dim llRow As Long = e.RowIndex
        Dim r As GridViewRow = DataGridDisplay.Rows(llRow)

        msID = ""
        Dim l As Literal
        l = r.FindControl("ID")
        If l IsNot Nothing Then
            msID = l.Text
        End If
    End Sub

    Protected Sub DataGridDisplay_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles DataGridDisplay.RowCommand
        If e.CommandName.Equals("update") Then
            '//GridView Row Index
            Dim rowIndex As Integer = Integer.Parse(e.CommandArgument.ToString().Trim())
            Dim r As GridViewRow = DataGridDisplay.Rows(rowIndex)
            ' // ID of the Current Row

            Dim l As Literal = r.Cells(1).FindControl("litID")
            Dim id As String = l.Text

            '//File Upload Instance of the Current Row
            Dim fileUpload As FileUpload = DataGridDisplay.Rows(rowIndex).FindControl("FileUpload1")

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
End Class
