
Imports System.ComponentModel
Imports System.Drawing

<DefaultProperty("Text"), ToolboxData("<{0}:ctrlCheckBox_ runat=server></{0}:ctrlCheckBox>"), ToolboxBitmap(GetType(Calendar))>
Public Class ctrlTextBox
    Inherits ctrlBase

    Dim lblFieldName As Label
    Dim DropDownList1 As DropDownList
    Dim uPanel1 As Panel 'UpdatePanel
    Dim lblField1 As Label
    Dim lblField2 As Label
    Dim ctrlField1 As TextBox
    Dim ctrlField2 As TextBox
    Dim CustomValidator1 As CustomValidator
    Dim trig As AsyncPostBackTrigger
	Public Overrides Sub SetFields(lsOption As String, lsValue As String)
		MyBase.SetFields(lsOption, lsValue)
		Dim lrli As ReportListItem
		If SelectedItems.Count > 0 Then
			For Each lrli In SelectedItems
                ctrlField1.Text = Trim(lrli.Value)
                'debug.print("CTRLTextBox Set Fields: " & lblFieldName.Text & " Value: " & ctrlField1.Text)
            Next
        End If
    End Sub

    Public Overrides Property ControlPanelcss() As String
        Get
            ControlPanelcss = msControlPanelcss
        End Get
        Set(sValue As String)
            msControlPanelcss = sValue

        End Set
    End Property
    Protected Overrides Sub CreateChildControlsSub()
        Controls.Clear()
        Dim li As ListItem
        Dim liCol As New ListItemCollection

        lblFieldName = New Label
        lblFieldName.ID = "lblFieldName"
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
        lblField2 = New Label
        lblField2.ID = "lblField2"
        lblField2.Visible = False
        ctrlField1 = New TextBox
        ctrlField1.ID = "ctrlField1"
        ctrlField1.CssClass = "form-control form-control-sm"
        Select Case mrptCtrl.ControlFieldType.ToUpper
            Case "DATE" : ctrlField1.TextMode = TextBoxMode.Date
            Case "DATETIME" : ctrlField1.TextMode = TextBoxMode.DateTimeLocal
            Case "DATETIMELOCAL" : ctrlField1.TextMode = TextBoxMode.DateTimeLocal
            Case "MULTILINE" : ctrlField1.TextMode = TextBoxMode.MultiLine
            Case "EMAIL" : ctrlField1.TextMode = TextBoxMode.Email
            Case "PHONE" : ctrlField1.TextMode = TextBoxMode.Phone
            Case "COLOR" : ctrlField1.TextMode = TextBoxMode.Color
            Case "SINGLELINE" : ctrlField1.TextMode = TextBoxMode.SingleLine
            Case "NUMBER" : ctrlField1.TextMode = TextBoxMode.Number
            Case "MONTH" : ctrlField1.TextMode = TextBoxMode.Month
            Case "URL" : ctrlField1.TextMode = TextBoxMode.Url
            Case "WEEK" : ctrlField1.TextMode = TextBoxMode.Week
        End Select

        If mrptCtrl.Required Then ctrlField1.Attributes.Add("required", "required")
        If mrptCtrl.Read_Only Then ctrlField1.ReadOnly = True
        If mrptCtrl.ValidationPattern <> "" Then ctrlField1.Attributes.Add("pattern", mrptCtrl.ValidationPattern)
        If mrptCtrl.ValidationTitle <> "" Then ctrlField1.Attributes.Add("title", mrptCtrl.ValidationTitle)
        If mrptCtrl.FieldLength > 0 Then ctrlField1.MaxLength = mrptCtrl.FieldLength

        ctrlField1.CssClass = "form-control form-control-sm"
        ctrlField2 = New TextBox
        ctrlField2.ID = "ctrlField2"
        ctrlField2.CssClass = "form-control form-control-sm"
        ctrlField2.Visible = False
        lblFieldName.AssociatedControlID = ctrlField1.ID
        lblField1.AssociatedControlID = ctrlField1.ID
        lblField1.Attributes.Add("for", ctrlField1.ClientID)
        lblField2.AssociatedControlID = ctrlField2.ID
        CustomValidator1 = New CustomValidator
        CustomValidator1.ID = "CustomValidator1"

        AddHandler DropDownList1.TextChanged, AddressOf DropDownList1_TextChanged
        AddHandler uPanel1.DataBinding, AddressOf UpdatePanel1_DataBinding
        AddHandler CustomValidator1.ServerValidate, AddressOf CustomValidator1_ServerValidate

        uPanel1.Controls.Add(lblFieldName)
        uPanel1.Controls.Add(ctrlHelpText)




        uPanel1.Controls.Add(fGetFieldButton)
        uPanel1.Controls.Add(fGetFieldDeleteButton)

        Controls.Add(DropDownList1)
        Controls.Add(uPanel1)

        uPanel1.Controls.Add(lblField1)
        uPanel1.Controls.Add(ctrlField1)
        uPanel1.Controls.Add(lblField2)
        uPanel1.Controls.Add(ctrlField2)
        uPanel1.Controls.Add(CustomValidator1)
        uPanel1.Attributes.Add("class", msControlPanelcss)
        ' UpdatePanel1.ContentTemplateContainer.Controls.Add(lblField1)
        ' UpdatePanel1.ContentTemplateContainer.Controls.Add(ctrlField1)
        ' UpdatePanel1.ContentTemplateContainer.Controls.Add(lblField2)
        ' UpdatePanel1.ContentTemplateContainer.Controls.Add(ctrlField2)
        ' UpdatePanel1.ContentTemplateContainer.Controls.Add(CustomValidator1)
        '  SetFields(Me.DropDownList1.Text, Me.DropDownList1.SelectedValue)
        uPanel1.Controls.Add(ctrlHelpText)
    End Sub
    Protected Overrides Sub RecreateChildControls()
        EnsureChildControls()
    End Sub
    Public Overrides Sub setValues()
        If SelectedItems.Count > 0 Then ctrlField1.Text = Trim(SelectedItems(0).ToString)
        If SelectedItems2.Count > 0 Then ctrlField2.Text = Trim(SelectedItems2(0).ToString)

    End Sub

    Protected Overrides Sub RenderSubControls(writer As HtmlTextWriter)
        On Error GoTo Error_Catch
        DropDownList1.RenderControl(writer)
        'lblFieldName.RenderControl(writer)
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
        Exit Sub
Error_Catch:
        'debug.print("<RenderSubControls Error='" & Err.Description & "' />")
        Resume Next
    End Sub


	'*** From .vb codebehind
	Public Overrides Sub ForceValidation()
        Dim lsReason As String = ""
        mcSelectedItems.Clear()
        mcSelectedItems2.Clear()
        If msDataType = "" Then msDataType = "Text"

		If ctrlField1.Text <> "" Then
			miSelectedItem.Value = CStr(ctrlField1.Text)
			miSelectedItem.Description = CStr(ctrlField1.Text)
			mcSelectedItems.Add(miSelectedItem)

		Else
			Dim ls As String
			ls = Me.Page.Request.Form(ctrlField1.ClientID.Replace("_", "$"))
			miSelectedItem.Value = CStr(ls)
			miSelectedItem.Description = CStr(ls)
			mcSelectedItems.Add(miSelectedItem)
		End If

        If ctrlField2.Visible And CStr(ctrlField2.Text) <> "" Then
            miSelectedItem2.Value = CStr(ctrlField2.Text)
            miSelectedItem2.Description = CStr(ctrlField2.Text)
            mcSelectedItems2.Add(miSelectedItem2)
        End If

        mbValid = Validate()
    End Sub
    Public Sub New()
        mrptCtrl = New ReportControl
    End Sub
    Public Sub New(ByRef lrptctrl As ReportControl)
        mrptCtrl = lrptctrl
    End Sub
    Protected Sub UpdatePanel1_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) 'andles UpdatePanel1.DataBinding
        SetFields(Me.DropDownList1.Text, Me.DropDownList1.SelectedValue)
    End Sub
    Protected Sub DropDownList1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles DropDownList1.TextChanged
        SetFields(Me.DropDownList1.Text, Me.DropDownList1.SelectedValue)
        'Me.ctrlField1.Text = ""
    End Sub
    Protected Sub iPager_Load(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles Me.Load
        If uPanel1 IsNot Nothing Then
            uPanel1.DataBind()
        End If
    End Sub
    Protected Sub CustomValidator1_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) 'Handles CustomValidator1.ServerValidate
        mbValid = False
        ForceValidation()
        args.IsValid = mbValid
        CustomValidator1.ErrorMessage = msValidationReason
    End Sub

    Private Sub ctrlTextBox_Init(sender As Object, e As EventArgs) Handles Me.Init
        Me.ToolTip = Me.ToolTip & "Init:" & Now.ToLongTimeString
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
