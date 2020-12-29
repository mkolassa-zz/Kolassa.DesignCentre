Imports System.ComponentModel
Imports System.Drawing

<DefaultProperty("Text"), ToolboxData("<{0}:mycontrol_ runat=server></{0}:mycontrol_>"), ToolboxBitmap(GetType(Calendar))>
Public Class mycontrol_
    Inherits CompositeControl

    Dim l, r As New ListItemCollection
    Dim cmdrfSave As Button
    Dim cmdrfClose As Button
    'Dim txtRFReportID, txtRFReportControl As textbox
    Dim txtRFContainerName, txtRFColumnSize, txtRFSortOrder, txtRFNodeID, txtRFValidation, txtRFValidationPatern, txtValidationTitle, txtrfTableName, txtRFFieldName, txtRFFieldLength, txtRFFieldTitle, txtRFID, txtRFName As TextBox
    Dim chkRFRequired, chkRFActive, chkrfReadOnly As CheckBox
    Dim cboFieldType, cboRFReportControl, cboRFReportID As DropDownList
    Dim div1, div2, div3, div4, div5, div6, div7, div8, div9 As System.Web.UI.HtmlControls.HtmlGenericControl
    Dim p, p1 As New Panel
    Dim lbl As New Label

    <Category("Appearance")>
    <Description("This is the Selected Date")>
    Public Property SelectedDate() As DateTime
        Get
            EnsureChildControls()
            If (cmdrfClose.Text) Is Nothing Then
                Return DateTime.MinValue
            Else
                Return Convert.ToDateTime(txtRFFieldName.Text)
            End If

        End Get
        Set(ByVal value As DateTime)
            EnsureChildControls()
            If IsDBNull(value) Then
                cmdrfClose.Text = ""
                'cal.VisibleDate = DateTime.Today
            Else
                'tb.Text = value.ToShortDateString()
                'cal.SelectedDate = value
            End If
        End Set
    End Property

    Protected Overrides Sub RecreateChildControls()
        EnsureChildControls()
    End Sub
    Public Sub LoadForm(ReportNum As Long, ControlNum As Long, FieldID As String)
        Dim c As New clsDataLoader
        Dim lic, lir As ListItem
        Dim ds As DataSet = c.LoadReportControls(ReportNum, ControlNum, FieldID)
        If l.Count = 0 Then
            cboRFReportControl.Items.Clear()
            Dim dsAllControls As DataSet = c.LoadAllControls()
            For Each r As DataRow In dsAllControls.Tables(0).Rows
                lic = New ListItem(r("ControlName"), r("ControlID"))
                l.Add(lic)
                cboRFReportControl.Items.Add(lic)
            Next
        End If
        If r.Count = 0 Then
            cboRFReportID.Items.Clear()
            Dim dsAllReports As DataSet = c.LoadReports(0, "ALLREPORTS", 0)
            For Each rr As DataRow In dsAllReports.Tables(0).Rows
                lir = New ListItem(rr("ReportName"), rr("ReportID"))
                l.Add(lir)
                cboRFReportID.Items.Add(lir)
            Next
        End If
        '   cboRFReportControl.DataSource = l
        ' cboRFReportControl.DataBind()
        Dim dr As DataRow
        Dim dt As DataTable
        If txtRFFieldName Is Nothing Then Exit Sub '  *** Form is not Active on the screen
        If ds.Tables.Count > 0 Then
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                dr = dt.Rows(0)
                txtRFID.Text = Trim(IIf(IsDBNull(dr("ReportControlFieldID")), "", dr("ReportControlFieldID").ToString))
                txtRFID.ToolTip = "Report Control ID: " & Trim(IIf(IsDBNull(dr("ID")), "", dr("ID").ToString))
                '   txtRFReportID.Text = Trim(IIf(IsDBNull(dr("ReportID")), "", dr("ReportID")))
                Dim lsReportID As String = Trim(IIf(IsDBNull(dr("ReportID")), "", dr("ReportID")))
                For Each i As ListItem In cboRFReportID.Items
                    If i.Value = lsReportID Then cboRFReportID.SelectedValue = i.Value
                Next
                txtRFFieldName.Text = Trim(IIf(IsDBNull(dr("FieldName")), "", dr("FieldName")))
                txtrfTableName.Text = Trim(IIf(IsDBNull(dr("TableName")), "", dr("TableName")))


                txtRFName.Text = Trim(IIf(IsDBNull(dr("Name")), "", dr("Name")))
                txtRFNodeID.Text = Trim(IIf(IsDBNull(dr("NodeID")), "", dr("NodeID")))
                txtRFFieldLength.Text = Trim(IIf(IsDBNull(dr("FieldLength")), "", dr("FieldLength")))
                txtRFFieldTitle.Text = Trim(IIf(IsDBNull(dr("FieldTitle")), "", dr("FieldTitle")))
                txtValidationTitle.Text = Trim(IIf(IsDBNull(dr("FieldValidationTitle")), "", dr("FieldValidationTitle")))
                txtRFValidationPatern.Text = Trim(IIf(IsDBNull(dr("FieldValidationPattern")), "", dr("FieldValidationPattern")))
                txtRFValidation.Text = Trim(IIf(IsDBNull(dr("Validation")), "", dr("Validation")))
                txtRFContainerName.Text = Trim(IIf(IsDBNull(dr("ContainerName")), "", dr("ContainerName")))
                txtRFCOlumnSize.Text = Trim(IIf(IsDBNull(dr("ColumnSize")), "", dr("ColumnSize")))
                '   txtRFReportControl.Text = Trim(IIf(IsDBNull(dr("ReportControl")), "", dr("ReportControl")))
                Dim lsReportControlID As String = Trim(IIf(IsDBNull(dr("ReportControl")), "", dr("ReportControl")))
                For Each i As ListItem In cboRFReportControl.Items
                    If i.Value = lsReportControlID Then cboRFReportControl.SelectedValue = i.Value
                Next
                '      cboRFReportControl.SelectedItem = Trim(IIf(IsDBNull(dr("ReportControl")), "", dr("ReportControl")))

                txtRFSortOrder.Text = Trim(IIf(IsDBNull(dr("SortOrder")), "0", dr("SortOrder")))
                Dim lsReadonly As String = IIf(IsDBNull(dr("FieldReadOnly")), "0", dr("FieldReadOnly"))
                chkrfReadOnly.Checked = IIf(Trim(lsReadonly) = "0", False, True)
                chkRFActive.Checked = Trim(IIf(IsDBNull(dr("Active")), False, IIf(dr("Active") = True, True, False)))
                chkRFRequired.Checked = Trim(IIf(IsDBNull(dr("Required")), False, IIf(dr("Required") = True, True, False)))
                If Not IsDBNull("FieldType") Then
                    Dim lsVal As String = Trim(dr("FieldType"))
                    Dim li As ListItem = New ListItem(lsVal)
                    If cboFieldType.Items.FindByValue(lsVal) Is Nothing Then cboFieldType.Items.Add(li)
                    cboFieldType.SelectedValue = lsVal
                End If

            End If
        End If

    End Sub
    Protected Overrides Sub CreateChildControls()
        Controls.Clear()
        Dim lsClass As String = "form-control form-control-sm "
        cmdrfSave = New Button
        cmdrfSave.ID = "cmdrfSave"
        cmdrfSave.Text = "Save"
        cmdrfSave.CssClass = "btn btn-primary btn-sm px-1"
        '  cmdrfSave.OnClientClick = "ShowDivSaveRecord('block')"
        cmdrfSave.Attributes.Add("onmousedown", "document.getElementById('divsaverecord').style.display = 'block';")
        AddHandler cmdrfSave.Click, AddressOf cmdrfSave_click
        Controls.Add(cmdrfSave)

        cmdrfClose = New Button
        cmdrfClose.ID = "cmdrfClose"
        cmdrfClose.Attributes.Add("onmousedown", "document.getElementById('divsaverecord').style.display = 'block';")
        cmdrfClose.Text = "Close"
        cmdrfClose.CssClass = "btn btn-secondary btn-sm px-1"
        Controls.Add(cmdrfClose)

        p = New Panel
        p.ID = "pnlRF-modal"
        p.CssClass = "form"
        p.TabIndex = "-1"
        p.Attributes.Add("role", "dialog")
        p.Attributes.Add("aria-hidden", "true")

        '*** Create form Rows
        div1 = New System.Web.UI.HtmlControls.HtmlGenericControl("DIV")
        div2 = New System.Web.UI.HtmlControls.HtmlGenericControl("DIV")
        div3 = New System.Web.UI.HtmlControls.HtmlGenericControl("DIV")
        div4 = New System.Web.UI.HtmlControls.HtmlGenericControl("DIV")
        div5 = New System.Web.UI.HtmlControls.HtmlGenericControl("DIV")
        div6 = New System.Web.UI.HtmlControls.HtmlGenericControl("DIV")
        div7 = New System.Web.UI.HtmlControls.HtmlGenericControl("DIV")
        div8 = New System.Web.UI.HtmlControls.HtmlGenericControl("DIV")
        div9 = New System.Web.UI.HtmlControls.HtmlGenericControl("DIV")
        div1.Attributes("class") = "form-row"
        div2.Attributes("class") = "form-row"
        div3.Attributes("class") = "form-row"
        div4.Attributes("class") = "form-row"
        div5.Attributes("class") = "form-row"
        div6.Attributes("class") = "form-row"
        div7.Attributes("class") = "form-row"
        div8.Attributes("class") = "form-row"
        div9.Attributes("class") = "form-row"

        '   txtRFReportID = New TextBox
        '  txtRFReportID.CssClass = lsClass
        cboRFReportID = New DropDownList
        cboRFReportID.CssClass = lsClass

        ' txtRFReportControl = New TextBox
        ' txtRFReportControl.ID = "txtrfReportControlID"
        ' txtRFReportControl.CssClass = lsClass

        txtRFNodeID = New TextBox
        txtRFNodeID.CssClass = lsClass
        txtRFNodeID.ID = "txtrfNodeID"

        txtRFValidation = New TextBox
        txtRFValidation.CssClass = lsClass
        txtRFValidation.ID = "txtRFValidation"

        txtRFValidationPatern = New TextBox
        txtRFValidationPatern.CssClass = lsClass
        txtRFValidationPatern.ID = "txtRFValidationPattern"

        txtValidationTitle = New TextBox
        txtValidationTitle.CssClass = lsClass
        txtValidationTitle.ID = "txtValidationTitle"

        txtrfTableName = New TextBox
        txtrfTableName.CssClass = lsClass
        txtrfTableName.ID = "txtrfTableName"

        txtRFFieldName = New TextBox
        txtRFFieldName.CssClass = lsClass
        txtRFFieldName.ID = "txtRFFieldName"

        txtRFFieldLength = New TextBox
        txtRFFieldLength.CssClass = lsClass
        txtRFFieldLength.ID = "txtRFFieldLength"

        txtRFFieldTitle = New TextBox
        txtRFFieldTitle.CssClass = lsClass
        txtRFFieldTitle.ID = "txtRFFieldTitle"

        txtRFSortOrder = New TextBox
        txtRFSortOrder.CssClass = lsClass
        txtRFSortOrder.ID = "txtRFSortOrder"
        txtRFSortOrder.Attributes.Add("TypeOf", "Number")

        chkrfReadOnly = New CheckBox
        chkrfReadOnly.ID = "chkrfReadOnly"

        txtRFID = New TextBox
        txtRFID.CssClass = lsClass
        txtRFID.ID = "txtRFID"

        txtRFName = New TextBox
        txtRFName.CssClass = lsClass
        txtRFName.ID = "txtRFName"

        txtRFContainerName = New TextBox
        txtRFContainerName.CssClass = lsClass
        txtRFContainerName.ID = "txtRFContainerName"
        txtRFColumnSize = New TextBox
        txtRFColumnSize.CssClass = lsClass
        txtRFColumnSize.ID = "txtRFColumnSize"
        txtRFColumnSize.Attributes("Type") = "number"
        txtRFColumnSize.Attributes("min") = "1"
        txtRFColumnSize.Attributes("max") = "12"
        chkRFRequired = New CheckBox
        chkRFRequired.ID = "chkRFRequired"

        chkRFActive = New CheckBox
        chkRFActive.ID = "chkRFActive"
        cboRFReportControl = New DropDownList
        cboRFReportControl.ID = "cboRFReportControl"
        cboRFReportControl.CssClass = lsClass


        cboFieldType = New DropDownList
        cboFieldType.ID = "cboFieldType"
        Dim lstitm As ListItem
        lstitm = New ListItem
        lstitm.Value = 1
        lstitm.Text = "Report"
        cboFieldType.Items.Add(lstitm)

        lstitm = New ListItem
        lstitm.Value = 2
        lstitm.Text = "Report No Control"
        cboFieldType.Items.Add(lstitm)

        lstitm = New ListItem
        lstitm.Value = 3
        lstitm.Text = "Form"
        cboFieldType.Items.Add(lstitm)



        ' p.Controls.Add(fGetLabel("Report", txtRFReportID, "txt"))
        div1.Controls.Add(fGetLabel("Report", cboRFReportID, "txt"))
        '  p.Controls.Add(fGetLabel("Report Control", txtRFReportControl, "txt"))
        div1.Controls.Add(fGetLabel("Report Control", cboRFReportControl, "txt"))
        div2.Controls.Add(fGetLabel("Node", txtRFNodeID, "txt"))
        div2.Controls.Add(fGetLabel("Validation", txtRFValidation, "txt"))
        div3.Controls.Add(fGetLabel("Validation Pattern", txtRFValidationPatern, "txt"))
        div3.Controls.Add(fGetLabel("Validation Title", txtValidationTitle, "txt"))
        div4.Controls.Add(fGetLabel("Table Name", txtrfTableName, "txt"))
        div4.Controls.Add(fGetLabel("FIeld Name", txtRFFieldName, "txt"))
        div5.Controls.Add(fGetLabel("Length", txtRFFieldLength, "txt"))
        div5.Controls.Add(fGetLabel("Title", txtRFFieldTitle, "txt"))
        div6.Controls.Add(fGetLabel("Field ID", txtRFID, "txt"))
        div6.Controls.Add(fGetLabel("Name", txtRFName, "txt"))
        div7.Controls.Add(fGetLabel("Field Type", cboFieldType, "txt"))
        div7.Controls.Add(fGetLabel("Sort Order", txtRFSortOrder, "txt"))
        div8.Controls.Add(fGetLabel("Container Name", txtRFContainerName, "txt"))
        div8.Controls.Add(fGetLabel("Column Size (1-12)", txtRFColumnSize, "txt"))
        p.Controls.Add(div1)
        p.Controls.Add(div2)
        p.Controls.Add(div3)
        p.Controls.Add(div4)
        p.Controls.Add(div5)
        p.Controls.Add(div6)
        p.Controls.Add(div7)
        p.Controls.Add(div8)
        p1 = New Panel
        p1.CssClass = "form-inline {margin-right:27px;}"
        chkRFActive.CssClass = ".mr-2"
        chkRFRequired.CssClass = ".mr-2"
        p1.Controls.Add(fGetLabel("Required", chkRFRequired, "chk"))
        p1.Controls.Add(fGetLabel("Active", chkRFActive, "chk"))
        p1.Controls.Add(fGetLabel("Read Only", chkrfReadOnly, "chk"))
        p.Controls.Add(p1)
        Controls.Add(p)
    End Sub
    Private Function fGetLabel(s As String, c As Control, sType As String) As HtmlGenericControl
        Dim span, span2, span3 As HtmlGenericControl
        span = New HtmlGenericControl
        span2 = New HtmlGenericControl
        span3 = New HtmlGenericControl
        Dim slen As String = "4"
        If sType = "chk" Then slen = "12"

        span.Attributes("class") = "form-group col-md-6"
        span2.Attributes("class") = " col-form-label" '& "col-sm-" & slen 
        span3.Attributes("class") = "" ' "col-sm-8"
        lbl = New Label
        lbl.CssClass = "form form-label"
        lbl.Text = s
        span2.Controls.Add(lbl)

        span3.Controls.Add(c)
        span.Controls.Add(span2)
        span.Controls.Add(span3)
        fGetLabel = span

    End Function
    Private Sub cmdrfSave_click(sender As Object, e As EventArgs)
        'Stop
        Dim rf As New clsReportField
        rf.FieldName = Trim(txtRFFieldName.Text)
        rf.TableName = Trim(txtrfTableName.Text)
        rf.ReportFieldID = Trim(txtRFID.Text)
        rf.Name = Trim(txtRFName.Text)
        rf.NodeID = Trim(txtRFNodeID.Text)
        rf.FieldLength = Val(txtRFFieldLength.Text)
        rf.Title = Trim(txtRFFieldTitle.Text)
        rf.ValidationTitle = Trim(txtValidationTitle.Text)
        rf.ValidationPattern = Trim(txtRFValidationPatern.Text)
        rf.Validation = Trim(txtRFValidation.Text)
        rf.ReportID = Trim(cboRFReportID.Text)
        rf.ReportControl = Trim(cboRFReportControl.SelectedValue)
        rf.SortOrder = Trim(txtRFSortOrder.Text)
        rf.containerName = Trim(txtRFContainerName.Text)
        rf.columnsize = Trim(txtRFColumnSize.Text)
        rf.FieldReadOnly = IIf(chkrfReadOnly.Checked, 1, 0)
        rf.Active = IIf(chkRFActive.Checked = True, 1, 0)
        rf.Required = IIf(chkRFRequired.Checked = True, 1, 0)
        rf.FieldType = cboFieldType.SelectedValue
        rf.Update()

    End Sub

    Protected Overrides Sub Render(writer As HtmlTextWriter)
        AddAttributesToRender(writer)
        'writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "1")
        'writer.RenderBeginTag(HtmlTextWriterTag.Table)
        'writer.RenderBeginTag(HtmlTextWriterTag.Tr)
        'writer.RenderBeginTag(HtmlTextWriterTag.Td)
        '    tb.RenderControl(writer)
        'writer.RenderEndTag() ' </td>

        ' writer.RenderBeginTag(HtmlTextWriterTag.Td)
        cmdrfSave.RenderControl(writer)
        cmdrfClose.RenderControl(writer)
        p.RenderControl(writer)
        '    writer.RenderEndTag() ' </td>
        '    writer.RenderEndTa g() ' </tr>
        '    writer.RenderEndTag() ' </table>
        '   cal.RenderControl(writer)

    End Sub
    Public Event DateSelected As DateSelectedEventHandler
    Protected Overridable Sub onDateSelection(e As DateSelectedEventArgs)
        RaiseEvent DateSelected(Me, e)
    End Sub
End Class
Public Class DateSelectedEventArgs
    Inherits EventArgs
    Private _SelectedDate As DateTime
    Public Sub New(SelectedDate As DateTime)
        _SelectedDate = SelectedDate
    End Sub
    Public Sub DateSelectedEventArgs(SelectedDate As DateTime)
        _SelectedDate = SelectedDate
    End Sub
    Public ReadOnly Property SelectedDate As DateTime
        Get
            Return _SelectedDate
        End Get
    End Property
End Class

'Maybe Remove Below
Public Delegate Sub DateSelectedEventHandler(Sender As Object, e As DateSelectedEventArgs)
