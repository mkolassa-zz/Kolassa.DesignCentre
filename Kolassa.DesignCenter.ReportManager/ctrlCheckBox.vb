
Imports System.ComponentModel
Imports System.Drawing

<DefaultProperty("Text"), ToolboxData("<{0}:ctrlCheckBox_ runat=server></{0}:ctrlCheckBox>"), ToolboxBitmap(GetType(Calendar))>
Public Class ctrlCheckBox
    Inherits ctrlBase

    Dim lblFieldName As Label
    Dim DropDownList1 As DropDownList
    ' Dim UpdatePanel1 As UpdatePanel
    Dim uPanel1 As Panel 'UpdatePanel
    Dim lblField1 As Label
    Dim lblField2 As Label
    Dim ctrlField1 As CheckBox
    Dim ctrlField2 As CheckBox
    Dim CustomValidator1 As CustomValidator
    Dim trig As AsyncPostBackTrigger
    Public Overrides Property ControlPanelcss() As String
        Get
            ControlPanelcss = msControlPanelcss
        End Get
        Set(sValue As String)
            msControlPanelcss = sValue
            'uPanel1.Attributes.Add("class", sValue)
        End Set
    End Property
    'Public Sub New(rpt As ReportControl)
    'Me.ID = rpt.ControlName
    '    miListItems = rpt.ListItems
    '    msFieldName = rpt.FieldDescription
    '    msDataType = rpt.ControlFieldType
    'End Sub
    Public Sub New()
        mrptCtrl = New ReportControl
    End Sub
    Public Sub New(ByRef lrptctrl As ReportControl)
        mrptCtrl = lrptctrl
    End Sub
    Protected Overrides Sub CreateChildControlsSub()
        Controls.Clear()
        Dim li As ListItem
        Dim liCol As New ListItemCollection

        lblFieldName = New Label
        lblFieldName.ID = "lblFieldName"
        lblFieldName.Visible = False
        DropDownList1 = New DropDownList
        DropDownList1.ID = "DropDownList1"
        DropDownList1.CssClass = "my-1 mr-sm-2"
        li = New ListItem()
        li.Value = "="
        li.Text = "="
        DropDownList1.Items.Add(li)
        li = New ListItem(">")
        DropDownList1.Items.Add(li)
        li = New ListItem(">=")
        DropDownList1.Items.Add(li)
        li = New ListItem("<")
        DropDownList1.Items.Add(li)
        li = New ListItem("<=")
        DropDownList1.Items.Add(li)
        li = New ListItem("Between")
        DropDownList1.Items.Add(li)

        li = New ListItem("")
        DropDownList1.Items.Add(li)


        DropDownList1.AutoPostBack = True
        DropDownList1.ToolTip = "Select the Operator"
        uPanel1 = New Panel ' UpdatePanel
        uPanel1.ID = "uPanel1"
        trig = New AsyncPostBackTrigger
        trig.ControlID = "DropDownList1"
        trig.EventName = "SelectedIndexChanged"
        '  uPanel1.Triggers.Add(trig)
        lblField1 = New Label
        lblField1.ID = "lblField1"
        lblField1.CssClass = "d-none " 'form-check-label"
        lblField2 = New Label
        lblField2.ID = "lblField2"
        lblField2.Visible = False
        ctrlField1 = New CheckBox
        ctrlField1.ID = "ctrlField1"

        If mrptCtrl.Required Then ctrlField1.Attributes.Add("required", "required")
        If mrptCtrl.Read_Only Then ctrlField1.Enabled = False
        If mrptCtrl.ValidationPattern <> "" Then ctrlField1.Attributes.Add("pattern", mrptCtrl.ValidationPattern)
        If mrptCtrl.ValidationTitle <> "" Then ctrlField1.Attributes.Add("title", mrptCtrl.ValidationTitle)


        ctrlField1.CssClass = "form-control border-0 form-control-sm form-control-input"
        ctrlField2 = New CheckBox
        ctrlField2.ID = "ctrlField2"
        ctrlField2.CssClass = "form-control border-0 form-control-sm"
        ctrlField2.Visible = False
        lblFieldName.AssociatedControlID = ctrlField1.ID
        lblField1.AssociatedControlID = ctrlField1.ID
        lblField1.Attributes.Add("for", ctrlField1.ClientID)
        lblField1.Attributes.Add("class", "d-none")
        lblField2.AssociatedControlID = ctrlField2.ID
        CustomValidator1 = New CustomValidator
        CustomValidator1.ID = "CustomValidator1"

        AddHandler DropDownList1.TextChanged, AddressOf DropDownList1_TextChanged
        AddHandler uPanel1.DataBinding, AddressOf UpdatePanel1_DataBinding
        AddHandler CustomValidator1.ServerValidate, AddressOf CustomValidator1_ServerValidate



        uPanel1.Controls.Add(fGetFieldButton)
        uPanel1.Controls.Add(fGetFieldDeleteButton)

        uPanel1.Controls.Add(lblFieldName)
        uPanel1.Controls.Add(DropDownList1)
        Controls.Add(uPanel1)
        uPanel1.Controls.Add(lblField1)
        lblField1.CssClass = "d-none"
        uPanel1.Controls.Add(ctrlField1)
        uPanel1.Controls.Add(lblField2)
        uPanel1.Controls.Add(ctrlField2)
        uPanel1.Controls.Add(CustomValidator1)
        uPanel1.Attributes.Add("class", msControlPanelcss)
        '      Controls.Clear()
        '      Dim li As ListItem
        '      lblFieldName = New Label
        '      lblFieldName.ID = "lblFieldName"
        '      DropDownList1 = New DropDownList
        'DropDownList1.ID = "DropDownList1"
        'DropDownList1.CssClass = "my-1 mr-sm-2"
        '      DropDownList1.Text = "THis"
        '      li = New ListItem()
        '      li.Value = "="
        '      li.Text = "="
        '      DropDownList1.Items.Add(li)
        '      UpdatePanel1 = New UpdatePanel
        '      UpdatePanel1.ID = "UpdatePanel1"
        '      trig = New AsyncPostBackTrigger
        '      trig.ControlID = "DropDownList1"
        '      trig.EventName = "SelectedIndexChanged"
        '      UpdatePanel1.Triggers.Add(trig)
        '      lblField1 = New Label
        '      lblField1.ID = "lblField1"
        '      lblField2 = New Label
        '      lblField2.ID = "lblField2"
        '      ctrlField1 = New CheckBox
        'ctrlField1.ID = "ctrlField1"
        '      ctrlField1.CssClass = "form-control border-0  form-control-sm"
        '      ctrlField2 = New CheckBox
        'ctrlField2.ID = "ctrlField2"
        '      ctrlField2.CssClass = "form-control  form-control-sm"
        '      CustomValidator1 = New CustomValidator
        '      CustomValidator1.ID = "CustomValidator1"

        '      ' AddHandler imgbt.Click, AddressOf imgbt_Click
        '      ' AddHandler cal.SelectionChanged, AddressOf cal_SelectionChanged

        '      Controls.Add(lblFieldName)
        '      Controls.Add(DropDownList1)
        '      Controls.Add(UpdatePanel1)

        '      UpdatePanel1.ContentTemplateContainer.Controls.Add(lblField1)
        '      UpdatePanel1.ContentTemplateContainer.Controls.Add(lblField2)
        '      UpdatePanel1.ContentTemplateContainer.Controls.Add(ctrlField1)
        '      UpdatePanel1.ContentTemplateContainer.Controls.Add(ctrlField2)
        '      UpdatePanel1.ContentTemplateContainer.Controls.Add(CustomValidator1)
        uPanel1.Controls.Add(ctrlHelpText)
    End Sub
    Protected Overrides Sub RecreateChildControls()
        EnsureChildControls()
    End Sub
    Protected Sub UpdatePanel1_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) 'andles UpdatePanel1.DataBinding
        SetFields(Me.DropDownList1.Text, Me.DropDownList1.SelectedValue)
    End Sub
    Protected Sub DropDownList1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles DropDownList1.TextChanged
        SetFields(Me.DropDownList1.Text, Me.DropDownList1.SelectedValue)
        'Me.ctrlField1.Text = ""
    End Sub
    Public Overrides Sub ForceValidation()
        Dim lsReason As String = ""
        mcSelectedItems.Clear()
        mcSelectedItems2.Clear()
        If msDataType = "" Then msDataType = "Text"

        If ctrlField1.Checked = True Then
            miSelectedItem.Value = "True"
            miSelectedItem.Description = "True"
            mcSelectedItems.Add(miSelectedItem)

        Else
            Dim ls As String
            ls = Me.Page.Request.Form(ctrlField1.ClientID.Replace("_", "$"))
            miSelectedItem.Value = "False" ' CStr(ls)
            miSelectedItem.Description = "False" ' CStr(ls)
            mcSelectedItems.Add(miSelectedItem)
        End If

        If ctrlField2.Visible And CStr(ctrlField2.Text) <> "" Then
            miSelectedItem2.Value = CStr(ctrlField2.Text)
            miSelectedItem2.Description = CStr(ctrlField2.Text)
            mcSelectedItems2.Add(miSelectedItem2)
        End If

        mbValid = Validate()
    End Sub
    Protected Sub CustomValidator1_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) 'Handles CustomValidator1.ServerValidate
        mbValid = False
        ForceValidation()
        args.IsValid = mbValid
        CustomValidator1.ErrorMessage = msValidationReason
    End Sub

    Private Sub ctrlCheckbox_Init(sender As Object, e As EventArgs) Handles Me.Init
        Me.ToolTip = Me.ToolTip & "Init:" & Now.ToLongTimeString
    End Sub

    Protected Overrides Sub RenderSubControls(writer As HtmlTextWriter)
        On Error Resume Next
        ' lblFieldName.RenderControl(writer)
        lblField1.RenderControl(writer)
        lblField2.RenderControl(writer)
        uPanel1.RenderControl(writer)
        On Error GoTo 0
        'AddAttributesToRender(writer)
        'writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "1")
        'writer.RenderBeginTag(HtmlTextWriterTag.Table)
        'writer.RenderBeginTag(HtmlTextWriterTag.Tr)
        'writer.RenderBeginTag(HtmlTextWriterTag.Td)
        'tb.RenderControl(writer)
        'writer.RenderEndTag() ' </td>

        'writer.RenderBeginTag(HtmlTextWriterTag.Td)
        'imgbt.RenderControl(writer)
        'writer.RenderEndTag() ' </td>
        'writer.RenderEndTag() ' </tr>
        'writer.RenderEndTag() ' </table>
        'cal.RenderControl(writer)

    End Sub

    Public Overrides Sub SetFields(lsOption As String, lsValue As String)
        '*** Added 6/1/2020  This was missing from Listbox control
        MyBase.SetFields(lsOption, lsValue)
        Dim lrli As ReportListItem
        If SelectedItems.Count > 0 Then
            For Each lrli In SelectedItems
                If UCase(lrli.Value) = "TRUE" Then
                    ctrlField1.Checked = True
                Else
                    ctrlField1.Checked = False
                End If
                'debug.print("CTRLCheckBox Set Fields: " & lblFieldName.Text & " Value: " & ctrlField1.Checked.ToString)
            Next
        End If
    End Sub

    Private Sub ctrlCheckBox_DataBinding(sender As Object, e As EventArgs) Handles Me.DataBinding
        On Error Resume Next
        SetFields(Me.DropDownList1.Text, Me.DropDownList1.SelectedValue)
        'debug.print("<ctrlCheckBox_DataBinding Checked=" & ctrlField1.Checked & " />")
    End Sub
    'Public Event DateSelected As DateSelectedEventHandler
    'Protected Overridable Sub onDateSelection(e As DateSelectedEventArgs)

    '    RaiseEvent DateSelected(Me, e)

    'End Sub

    '<Category("Appearance")>
    '<Description("This is the Selected Date")>
    'Public Property SelectedDate() As DateTime
    '    Get
    '        EnsureChildControls()
    '        If (tb.Text) Is Nothing Then
    '            Return String.Empty
    '        Else
    '            Return Convert.ToDateTime(tb.Text)
    '        End If

    '    End Get
    '    Set(ByVal value As DateTime)
    '        EnsureChildControls()
    '        If IsDBNull(value) Then
    '            tb.Text = ""
    '            cal.VisibleDate = DateTime.Today
    '        Else
    '            tb.Text = value.ToShortDateString()
    '            cal.SelectedDate = value
    '        End If
    '    End Set
    'End Property
    '<Category("Appearance")>
    '<Description("Changes the way it it looks")>
    'Public Property ImageButtonImageURL() As String
    '    Get
    '        EnsureChildControls()
    '        If (imgbt.ImageUrl) Is Nothing Then
    '            Return String.Empty
    '        Else
    '            Return imgbt.ImageUrl
    '        End If

    '    End Get
    '    Set(ByVal value As String)
    '        EnsureChildControls()
    '        imgbt.ImageUrl = value
    '    End Set
    'End Property
    'Private Sub imgbt_Click(sender As Object, e As ImageClickEventArgs)
    '    If cal.Visible Then
    '        cal.Visible = False
    '    Else
    '        cal.Visible = True
    '        If IsDBNull(tb.Text) Then
    '            cal.VisibleDate = DateTime.Today
    '        Else
    '            Dim output As DateTime = DateTime.Today
    '            Dim isDateTimeConversionSuccessful As Boolean = DateTime.TryParse(tb.Text, output)
    '            cal.VisibleDate = output

    '        End If
    '    End If
    'End Sub
    'Private Sub cal_SelectionChanged(sender As Object, e As EventArgs)
    '    tb.Text = cal.SelectedDate.ToShortDateString
    '    Dim eventData As DateSelectedEventArgs = New DateSelectedEventArgs(SelectedDate)
    '    onDateSelection(eventData)
    '    cal.Visible = False
    'End Sub
End Class
'Public Class DateSelectedEventArgs
'    Inherits EventArgs
'    Private _SelectedDate As DateTime
'    Public Sub New(SelectedDate As DateTime)
'        _SelectedDate = SelectedDate
'    End Sub
'    Public Sub DateSelectedEventArgs(SelectedDate As DateTime)
'        _SelectedDate = SelectedDate
'    End Sub
'    Public ReadOnly Property SelectedDate As DateTime
'        Get
'            Return _SelectedDate
'        End Get
'    End Property
'End Class
'Public Delegate Sub DateSelectedEventHandler(Sender As Object, e As DateSelectedEventArgs)
