
Imports System.ComponentModel
Imports System.Drawing

<DefaultProperty("Text"), ToolboxData("<{0}:ctrlBase_ runat=server></{0}:ctrlBase>"), ToolboxBitmap(GetType(Calendar))>
Public Class ctrlBase
    Inherits clsBase
    Dim lblTitle As Label
    Dim lblTitle2 As Label
    Dim ddl As DropDownList

	'*** Not so sure I can have these here
	Dim ctrlField1 As TextBox
	Dim ctrlField2 As TextBox
	'******************************************

	Dim mnuCTRL1 As Menu
	Dim mnuCTRL As DropDownList

	Public initReportControl As ReportControl
	Public Sub UpdateParent()
	End Sub
	Private Sub ctrlBase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Tag = "ReportControl"
		UpdateParent()
	End Sub
    Public Sub New()
        mrptCtrl = New ReportControl
    End Sub
    Public Sub New(ByRef lrptctrl As ReportControl)
        mrptCtrl = lrptctrl
    End Sub
	Protected Overrides Sub RecreateChildControls()
		EnsureChildControls()
		' Debug( "Recreate")
	End Sub
	Protected Overrides Sub CreateChildControls()
		Controls.Clear()


		mnuCTRL1 = New Menu
        mnuCTRL1.ID = "mnuCTRL1"
        mnuCTRL1.ToolTip = msFieldName

		mnuCTRL = New DropDownList
        mnuCTRL.ID = "mnuCTRL"
		mnuCTRL.Text = msFieldName
		mnuCTRL.CssClass = "form-control control-label  form-control-sm"
		Dim li As New ListItem
        Dim rli As ReportListItem
        For Each rli In miListItems
            li = New ListItem
            li.Text = rli.Description
            li.Value = rli.Value
            mnuCTRL.Items.Add(li)
        Next
		' AddHandler imgbt.Click, AddressOf imgbt_Click
		' AddHandler cal.SelectionChanged, AddressOf cal_SelectionChanged


		Controls.Add(mnuCTRL)
		Controls.Add(mnuCTRL1)
		lblTitle = New Label
        lblTitle.ID = "lbl" & msFieldName
		lblTitle.Text = msFieldName
		lblTitle.CssClass = "control-label font-weight-bold"
		If msReportType = "form" Then
			lblTitle.Visible = False
		End If
		Controls.Add(lblTitle)
		CreateChildControlsSub()
    End Sub
    Protected Overridable Sub CreateChildControlsSub()
        lblTitle2 = New Label
        lblTitle2.ID = "mylabel"
        lblTitle2.Text = "This SubControl should be overrided in the Sub Controls event"
        Controls.Add(lblTitle2)
    End Sub


	Protected Overrides Sub Render(writer As HtmlTextWriter)

		'mnuCTRL1.RenderControl(writer)
		'mnuCTRL.RenderControl(writer)
		lblTitle.RenderControl(writer)

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
		RenderSubControls(writer)
    End Sub
    Protected Overridable Sub RenderSubControls(writer As HtmlTextWriter)
        lblTitle2.RenderControl(writer)
    End Sub

    Private Sub ctrlBase_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim t As DateTime = Now
    End Sub

	Public Overridable Sub setValues()
		'*** Set the controls = the selected Value
		If SelectedItems.Count > 0 Then ctrlField1.Text = SelectedItems(0).ToString
		If SelectedItems2.Count > 0 Then ctrlField2.Text = SelectedItems2(0).ToString
		If mbRequired = True Then ctrlField1.Attributes.Add("Required", "Required")
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

	'Public mbRefreshList As Boolean = True

	'Public mrptCtrl As ReportControl
	'Public msTag As String
	'Public mcSelectedItems As Collection = New Collection
	'Public mcSelectedItems2 As Collection = New Collection
	'Public miSelectedItem As ReportListItem = New ReportListItem
	'Public miSelectedItem2 As ReportListItem = New ReportListItem

	'Public mbValid As Boolean = False
	'Public msFieldName As String
	'Public msOperator As String = ""
	'Public msDataType As String = ""
	'Public miListItems As ReportListItems = New ReportListItems


	'Public Property ListItems() As ReportListItems
	'    Get
	'        ListItems = miListItems
	'    End Get
	'    Set(ByVal value As ReportListItems)
	'        miListItems = value
	'    End Set
	'End Property
	'Public Property SelectedItems() As Collection
	'    Get
	'        SelectedItems = mcSelectedItems
	'    End Get
	'    Set(ByVal value As Collection)
	'        mcSelectedItems = value
	'    End Set
	'End Property
	'Public Property SelectedItems2() As Collection
	'    Get
	'        SelectedItems2 = mcSelectedItems2
	'    End Get
	'    Set(ByVal value As Collection)
	'        mcSelectedItems2 = value
	'    End Set
	'End Property
	'Public Property DataOperator() As String
	'    Get
	'        DataOperator = msOperator
	'    End Get
	'    Set(ByVal value As String)
	'        msOperator = value
	'    End Set
	'End Property
	'Public Property DataType() As String
	'    Get
	'        DataType = msDataType
	'    End Get
	'    Set(ByVal value As String)
	'        msDataType = value
	'    End Set
	'End Property

	'Public Property Valid() As Boolean
	'    Get
	'        Valid = mbValid
	'    End Get
	'    Set(ByVal value As Boolean)

	'    End Set
	'End Property


	'Public Overridable Sub ForceValidation()
	'    '   Me.Validate()
	'    UpdateParent()
	'End Sub
	'Public Property FieldName() As String
	'    Get
	'        FieldName = msFieldName
	'    End Get
	'    Set(ByVal value As String)
	'        msFieldName = value

	'        mnuCTRL.Text = msFieldName
	'    End Set
	'End Property
	'Public Property Tag() As String
	'    Get
	'        Tag = msFieldName
	'    End Get
	'    Set(ByVal value As String)
	'        msTag = value
	'    End Set
	'End Property 
	'Public Overridable Sub SetFields(ByVal lsOption As String, ByVal lsValue As String)
	'End Sub
	'Public Overridable Sub RefreshLists()
	'End Sub
	'Public Event ControlUpdated(ByVal sender As System.Object, ByVal e As System.EventArgs)
End Class
''Public Class DateSelectedEventArgs
''    Inherits EventArgs
''    Private _SelectedDate As DateTime
''    Public Sub New(SelectedDate As DateTime)
''        _SelectedDate = SelectedDate
''    End Sub
''    Public Sub DateSelectedEventArgs(SelectedDate As DateTime)
''        _SelectedDate = SelectedDate
''    End Sub
''    Public ReadOnly Property SelectedDate As DateTime
''        Get
''            Return _SelectedDate
''        End Get
''    End Property
''End Class
''Public Delegate Sub DateSelectedEventHandler(Sender As Object, e As DateSelectedEventArgs)
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