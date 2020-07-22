
Imports System.ComponentModel
Imports System.Drawing

<DefaultProperty("Text"), ToolboxData("<{0}:ctrlCheckBox_ runat=server></{0}:ctrlCheckBox>"), ToolboxBitmap(GetType(Calendar))>
Public Class ctrlCheckBox
    Inherits ctrlBase

    Dim lblFieldName As Label
    Dim DropDownList1 As DropDownList
    Dim UpdatePanel1 As UpdatePanel
    Dim lblField1 As Label
    Dim lblField2 As Label
    Dim ctrlField1 As CheckBox
    Dim ctrlField2 As CheckBox
    Dim CustomValidator1 As CustomValidator
    Dim trig As AsyncPostBackTrigger

    Public Sub New(rpt As ReportControl)
        Me.ID = rpt.ControlName
        miListItems = rpt.ListItems
        msFieldName = rpt.FieldDescription
        msDataType = rpt.ControlFieldType
    End Sub

    Protected Overrides Sub CreateChildControlsSub()
        Controls.Clear()

        lblFieldName = New Label
        lblFieldName.ID = "lblFieldName"
        DropDownList1 = New DropDownList
        DropDownList1.ID = "DropDownList1"
        UpdatePanel1 = New UpdatePanel
        UpdatePanel1.ID = "UpdatePanel1"
        trig = New AsyncPostBackTrigger
        trig.ControlID = "DropDownList1"
        trig.EventName = "SelectedIndexChanged"
        UpdatePanel1.Triggers.Add(trig)
        lblField1 = New Label
        lblField1.ID = "lblField1"
        lblField2 = New Label
        lblField2.ID = "lblField2"
        ctrlField1 = New CheckBox
        ctrlField1.ID = "ctrlField1"
        ctrlField2 = New CheckBox
        ctrlField2.ID = "ctrlField2"
        CustomValidator1 = New CustomValidator
        CustomValidator1.ID = "CustomValidator1"

        ' AddHandler imgbt.Click, AddressOf imgbt_Click
        ' AddHandler cal.SelectionChanged, AddressOf cal_SelectionChanged

        Controls.Add(lblFieldName)
        Controls.Add(DropDownList1)
        Controls.Add(UpdatePanel1)
        UpdatePanel1.ContentTemplateContainer.Controls.Add(lblField1)
        UpdatePanel1.ContentTemplateContainer.Controls.Add(lblField2)
        UpdatePanel1.ContentTemplateContainer.Controls.Add(ctrlField1)
        UpdatePanel1.ContentTemplateContainer.Controls.Add(ctrlField2)
        UpdatePanel1.ContentTemplateContainer.Controls.Add(CustomValidator1)

    End Sub
    Protected Overrides Sub RecreateChildControls()
        EnsureChildControls()
    End Sub


    Protected Overrides Sub RenderSubControls(writer As HtmlTextWriter)
        On Error Resume Next
        lblFieldName.RenderControl(writer)
        lblField1.RenderControl(writer)
        lblField2.RenderControl(writer)
        UpdatePanel1.RenderControl(writer)
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
