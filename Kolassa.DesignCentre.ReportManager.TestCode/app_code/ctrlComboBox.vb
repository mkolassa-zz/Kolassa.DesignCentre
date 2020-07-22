
Imports System.ComponentModel
Imports System.Drawing

<DefaultProperty("Text"), ToolboxData("<{0}:ctrlCheckBox_ runat=server></{0}:ctrlCheckBox>"), ToolboxBitmap(GetType(Calendar))>
Public Class ctrlComboBox
    Inherits ctrlBase

    Dim lblFieldName As Label
    Dim DropDownList1 As DropDownList
    Dim uPanel1 As UpdatePanel
    Dim lblField1 As Label
    Dim lblField2 As Label
    Dim ctrlField1 As DropDownList
    Dim ctrlField2 As DropDownList
    Dim CustomValidator1 As CustomValidator
    Dim trig As AsyncPostBackTrigger



    Protected Overrides Sub CreateChildControlsSub()
        Controls.Clear()
        Dim li As ListItem
        Dim liCol As New ListItemCollection

        lblFieldName = New Label
        lblFieldName.ID = "lblFieldName"
        DropDownList1 = New DropDownList
        DropDownList1.ID = "DropDownList1"
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

        DropDownList1.AutoPostBack = True
        DropDownList1.ToolTip = "Select the Operator"
        uPanel1 = New UpdatePanel
        uPanel1.ID = "uPanel1"
        trig = New AsyncPostBackTrigger
        trig.ControlID = "DropDownList1"
        trig.EventName = "SelectedIndexChanged"
        uPanel1.Triggers.Add(trig)
        lblField1 = New Label
        lblField1.ID = "lblField1"
        lblField2 = New Label
        lblField2.ID = "lblField2"
        lblField2.Visible = False
        ctrlField1 = New DropDownList
        ctrlField1.ID = "ctrlField1"
        ctrlField2 = New DropDownList
        ctrlField2.ID = "ctrlField2"
        ctrlField2.Visible = False
        CustomValidator1 = New CustomValidator
        CustomValidator1.ID = "CustomValidator1"

        AddHandler DropDownList1.TextChanged, AddressOf DropDownList1_TextChanged
        AddHandler uPanel1.DataBinding, AddressOf UpdatePanel1_DataBinding
        AddHandler CustomValidator1.ServerValidate, AddressOf CustomValidator1_ServerValidate

        Controls.Add(lblFieldName)
        Controls.Add(DropDownList1)
        Controls.Add(uPanel1)

        uPanel1.ContentTemplateContainer.Controls.Add(lblField1)
        uPanel1.ContentTemplateContainer.Controls.Add(ctrlField1)
        uPanel1.ContentTemplateContainer.Controls.Add(lblField2)
        uPanel1.ContentTemplateContainer.Controls.Add(ctrlField2)
        uPanel1.ContentTemplateContainer.Controls.Add(CustomValidator1)
        '  SetFields(Me.DropDownList1.Text, Me.DropDownList1.SelectedValue)
    End Sub
    Protected Overrides Sub RecreateChildControls()
        EnsureChildControls()
    End Sub


    Protected Overrides Sub RenderSubControls(writer As HtmlTextWriter)
        On Error Resume Next
        DropDownList1.RenderControl(writer)
        lblFieldName.RenderControl(writer)
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


    '*** From .vb codebehind
    Dim maList1Values(1000, 2) As Array
    Dim malist2Values(1000, 2) As Array
    Public Property List1Source() As Array
        Get
            List1Source = maList1Values
        End Get
        Set(ByVal value As Array)
            maList1Values = value
            refreshLists()
        End Set
    End Property
    Public Property List2Source() As Array
        Get
            List2Source = malist2Values
        End Get
        Set(ByVal value As Array)
            malist2Values = value
            '         RefreshList2()
        End Set
    End Property
    Public Overrides Sub refreshLists()

        Dim rli As New ReportListItem
        Dim li As New ListItem
        '*** Clear the Items from the List
        ctrlField1.Items.Clear()
        ctrlField2.Items.Clear()

        '*** Refresh the ReportListITems Collection
        '*** NOT DONE YET

        '*** Iterate through the list Items and add to list Control
        For Each rli In mrptCtrl.ListItems
            li = New ListItem
            li.Value = rli.Value
            li.Text = rli.Description
            ctrlField1.Items.Add(li)
            ' If ctrlField2.Visible Then
            ctrlField2.Items.Add(li)
            'End If
            li = Nothing
        Next

    End Sub
    Public Overrides Sub ForceValidation()
        Dim lsReason As String = ""
        mcSelectedItems.Clear()
        mcSelectedItems2.Clear()
        If msDataType = "" Then msDataType = "Text"

        If ctrlField1.Text <> "" Then
            miSelectedItem.Value = CStr(ctrlField1.Text)
            miSelectedItem.Description = CStr(ctrlField1.Text)
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
    Protected Sub UpdatePanel1_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles UpdatePanel1.DataBinding
        SetFields(Me.DropDownList1.Text, Me.DropDownList1.SelectedValue)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        uPanel1.DataBind()
        refreshLists()
    End Sub
    Protected Sub DropDownList1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles DropDownList1.TextChanged
        SetFields(Me.DropDownList1.Text, Me.DropDownList1.SelectedValue)
    End Sub
    Protected Sub CustomValidator1_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) 'Handles CustomValidator1.ServerValidate
        mbValid = False
        ForceValidation()
        args.IsValid = mbValid
        CustomValidator1.ErrorMessage = msValidationReason
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
