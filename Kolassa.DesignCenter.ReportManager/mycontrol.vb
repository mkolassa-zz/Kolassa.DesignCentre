Imports System.ComponentModel
Imports System.Drawing

<DefaultProperty("Text"), ToolboxData("<{0}:mycontrol_ runat=server></{0}:mycontrol_>"), ToolboxBitmap(GetType(Calendar))>
Public Class mycontrol_
    Inherits CompositeControl


    Dim cmdrfSave As Button
    Dim cmdrfClose As Button
    Dim txtRFReportID, txtRFSortOrder, txtRFReportControl, txtRFNodeID, txtRFValidation, txtRFValidationPatern, txtValidationTitle, txtrfTableName, txtRFFieldName, txtRFFieldLength, txtRFFieldTitle, txtRFID, txtRFName As TextBox
    Dim chkRFRequired, chkRFActive, chkrfReadOnly As CheckBox
    Dim cboFieldType As DropDownList

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
        Dim ds As DataSet = c.LoadReportControls(ReportNum, ControlNum, FieldID)
        Dim dr As DataRow
        Dim dt As DataTable
        If txtRFFieldName Is Nothing Then Exit Sub '  *** Form is not Active on the screen
        If ds.Tables.Count > 0 Then
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                dr = dt.Rows(0)
                txtRFID.Text = Trim(IIf(IsDBNull(dr("ReportControlFieldID")), "", dr("ReportControlFieldID").ToString))
                txtRFID.ToolTip = "Report Control ID: " & Trim(IIf(IsDBNull(dr("ID")), "", dr("ID").ToString))
                txtRFReportID.Text = Trim(IIf(IsDBNull(dr("ReportID")), "", dr("ReportID")))
                txtRFFieldName.Text = Trim(IIf(IsDBNull(dr("FieldName")), "", dr("FieldName")))
                txtrfTableName.Text = Trim(IIf(IsDBNull(dr("TableName")), "", dr("TableName")))


                txtRFName.Text = Trim(IIf(IsDBNull(dr("Name")), "", dr("Name")))
                txtRFNodeID.Text = Trim(IIf(IsDBNull(dr("NodeID")), "", dr("NodeID")))
                txtRFFieldLength.Text = Trim(IIf(IsDBNull(dr("FieldLength")), "", dr("FieldLength")))
                txtRFFieldTitle.Text = Trim(IIf(IsDBNull(dr("FieldTitle")), "", dr("FieldTitle")))
                txtValidationTitle.Text = Trim(IIf(IsDBNull(dr("FieldValidationTitle")), "", dr("FieldValidationTitle")))
                txtRFValidationPatern.Text = Trim(IIf(IsDBNull(dr("FieldValidationPattern")), "", dr("FieldValidationPattern")))
                txtRFValidation.Text = Trim(IIf(IsDBNull(dr("Validation")), "", dr("Validation")))

                txtRFReportControl.Text = Trim(IIf(IsDBNull(dr("ReportControl")), "", dr("ReportControl")))
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
        cmdrfSave.CssClass = "btn btn-primary btn-sm"
        '  cmdrfSave.OnClientClick = "ShowDivSaveRecord('block')"
        cmdrfSave.Attributes.Add("onmousedown", "document.getElementById('divsaverecord').style.display = 'block';")
        AddHandler cmdrfSave.Click, AddressOf cmdrfSave_click
        Controls.Add(cmdrfSave)

        cmdrfClose = New Button
        cmdrfClose.ID = "cmdrfClose"
        cmdrfClose.Attributes.Add("onmousedown", "document.getElementById('divsaverecord').style.display = 'block';")
        cmdrfClose.Text = "Close"
        cmdrfClose.CssClass = "btn btn-secondary btn-sm"
        Controls.Add(cmdrfClose)

        p = New Panel
        p.ID = "pnlRF-modal"
        p.CssClass = "form form-group"
        p.TabIndex = "-1"
        p.Attributes.Add("role", "dialog")
        p.Attributes.Add("aria-hidden", "true")

        txtRFReportID = New TextBox
        txtRFReportID.CssClass = lsClass

        txtRFReportControl = New TextBox
        txtRFReportControl.ID = "txtrfReportControlID"
        txtRFReportControl.CssClass = lsClass

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

        chkRFRequired = New CheckBox
        chkRFRequired.ID = "chkRFRequired"

        chkRFActive = New CheckBox
        chkRFActive.ID = "chkRFActive"

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



        p.Controls.Add(fGetLabel("Report", txtRFReportID, "txt"))
        p.Controls.Add(fGetLabel("Report Control", txtRFReportControl, "txt"))
        p.Controls.Add(fGetLabel("Node", txtRFNodeID, "txt"))
        p.Controls.Add(fGetLabel("Validation", txtRFValidation, "txt"))
        p.Controls.Add(fGetLabel("Validation Pattern", txtRFValidationPatern, "txt"))
        p.Controls.Add(fGetLabel("Validation Title", txtValidationTitle, "txt"))
        p.Controls.Add(fGetLabel("Table Name", txtrfTableName, "txt"))
        p.Controls.Add(fGetLabel("FIeld Name", txtRFFieldName, "txt"))
        p.Controls.Add(fGetLabel("Length", txtRFFieldLength, "txt"))
        p.Controls.Add(fGetLabel("Title", txtRFFieldTitle, "txt"))
        p.Controls.Add(fGetLabel("Field ID", txtRFID, "txt"))
        p.Controls.Add(fGetLabel("Name", txtRFName, "txt"))
        p.Controls.Add(fGetLabel("Field Type", cboFieldType, "txt"))
        p.Controls.Add(fGetLabel("Sort Order", txtRFSortOrder, "txt"))

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

        span.Attributes("class") = "form-group row"
        span2.Attributes("class") = "col-sm-" & slen & " col-form-label"
        span3.Attributes("class") = "col-sm-8"
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
        rf.ReportID = Trim(txtRFReportID.Text)
        rf.ReportControl = Trim(txtRFReportControl.Text)
        rf.SortOrder = Trim(txtRFSortOrder.Text)

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
