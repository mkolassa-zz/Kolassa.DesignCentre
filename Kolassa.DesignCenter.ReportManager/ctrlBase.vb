
Imports System.ComponentModel
Imports System.Drawing

<DefaultProperty("Text"), ToolboxData("<{0}:ctrlBase_ runat=server></{0}:ctrlBase>"), ToolboxBitmap(GetType(Calendar))>
Public Class ctrlBase
    Inherits clsBase
    Dim lblTitle As Label
    Dim lblTitle2 As Label
    Dim ddl As DropDownList
	'Public  uPanel1 As UpdatePanel
	'*** Not so sure I can have these here
	Dim ctrlField1 As TextBox
	Dim ctrlField2 As TextBox
	Public ctrlHelpText As Label
	'******************************************
	Public msControlPanelcss As String
	Dim mnuCTRL1 As Menu
	Dim mnuCTRL As DropDownList
	Dim cmdFieldDef As LinkButton
	Dim cmdFieldDeflnk As LinkButton
	Dim cmdFieldDel As LinkButton
	Public initReportControl As ReportControl
	Public Function fGetFieldButton() As LinkButton
		cmdFieldDef = New LinkButton

		cmdFieldDef.Text = "<i class='fa fa-forward' aria-hidden='false'></i>" ' "+"
		cmdFieldDef.Text = "<i class='fa fa-edit'></i>" '"<i class='icon-edit'></i>"
		'cmdFieldDef.UseSubmitBehavior = False

		cmdFieldDef.ID = "cmdPopulateFV" ' & mrptCtrl.ReportControlFieldID
		cmdFieldDef.CssClass = "float-right px-1 editmode"
		cmdFieldDef.Style.Add("display", "none")
		'cmdFieldDef.Attributes.Add("onmousedown", "document.getElementById('divsaverecord').style.display = 'none';")
		cmdFieldDef.Attributes.Add("onmouseup", "$('#lnkshowrf')[0].click();")
		'	cmdFieldDef.Attributes.Add("Data-toggle", "modal")
		'	cmdFieldDef.Attributes.Add("Data-target", "#field_edit_modal")

		cmdFieldDef.Attributes.Add("data-reportid", mrptCtrl.ReportID)
		cmdFieldDef.Attributes.Add("data-reportcontrolid", mrptCtrl.ReportControlID)
		cmdFieldDef.Attributes.Add("data-reportcontrolfieldnumid", mrptCtrl.ReportControlNumID)
		cmdFieldDef.Attributes.Add("data-reportcontrolfieldid", mrptCtrl.ReportControlFieldID)
		cmdFieldDef.Attributes.Add("data-reportcontrolfieldHelpText", mrptCtrl.ReportControlHelpText)

		'		cmdFieldDef.Attributes.Add("style", "visubility:hidden;")
		'*** Show RF EDIT Buttons if the TARGET Request has CMDEDITMODE Set to True
		'*** this changes Visibility to the edit buttons
		Dim lsCTRL As String = ""
		If Not Page Is Nothing Then lsCTRL = Page.Request.Params.Get("__EVENTTARGET")

		If lsCTRL.ToUpper.Contains("CMDEDITMODE") = True Then EditMode = True
		'If EditMode = True Then
		'	cmdFieldDef.Visible = True
		'	'cmdFieldDef.Attributes.Add("", mrptCtrl.ReportControlHelpText)
		'Else
		'	cmdFieldDef.Visible = False
		'End If

		AddHandler cmdFieldDef.Click, AddressOf LoadReportFields
		fGetFieldButton = cmdFieldDef
	End Function
	Public Function fGetFieldDeleteButton() As LinkButton

		cmdFieldDel = New LinkButton
		cmdFieldDel.Text = "<i class='fa fa-forward' aria-hidden='false'></i>" ' "+"
		cmdFieldDel.Text = "<i class='fa fa-trash  padding-left:30px;'></i>" ' "\xF135"
		'cmdFieldDel.UseSubmitBehavior = False


		'cmdFieldDel.Attributes.Add("onmousedown", "document.getElementById('divsaverecord').style.display = 'none';")
		cmdFieldDel.CssClass = "float-right px-1 editmode" '< i Class='fa fa-plus'></i>" float-right"

		cmdFieldDel.ID = "cmdDeleteFieldValues" & mrptCtrl.ReportControlFieldID
		cmdFieldDel.Attributes.Add("data-reportid", mrptCtrl.ReportID)
		cmdFieldDel.Attributes.Add("data-reportcontrolid", mrptCtrl.ReportControlID)
		cmdFieldDel.Attributes.Add("data-reportcontrolfieldnumid", mrptCtrl.ReportControlNumID)
		cmdFieldDel.Attributes.Add("data-reportcontrolfieldid", mrptCtrl.ReportControlFieldID)
		cmdFieldDel.Attributes.Add("data-mode", "editmode")
		cmdFieldDel.Style.Add("display", "none")
		'	cmdFieldDel.Attributes.Add("style", "visubility:hidden;")
		AddHandler cmdFieldDel.Click, AddressOf DeleteReportFields
		'If EditMode = True Then
		'	cmdFieldDel.Visible = True
		'Else
		'	cmdFieldDel.Visible = False
		'End If
		fGetFieldDeleteButton = cmdFieldDel


	End Function
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

		Dim cmd As Button
		mnuCTRL1 = New Menu
        mnuCTRL1.ID = "mnuCTRL1"
		mnuCTRL1.ToolTip = msclsBaseFieldName

		mnuCTRL = New DropDownList
        mnuCTRL.ID = "mnuCTRL"
		mnuCTRL.Text = msclsBaseFieldName
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


		Dim span As New Panel
		span.Attributes("class") = "form-group row"
		span.ID = "ctTitle"
		span.Attributes.Add("Titles", "TitleStyle")

		Dim a, b, c As Label
		a = New Label
		b = New Label
		c = New Label
		a.Text = "a start"
		b.Text = "B Start"
		c.Text = "c start"
		span.Controls.Add(a)


		cmd = New Button 'fGetFieldButton()
		'	Controls.Add(cmd)
		lblTitle = New Label
		lblTitle.ID = "lbl" & msclsBaseFieldName
		lblTitle.Text = msclsBaseFieldName
		lblTitle.CssClass = "control-label font-weight-bold"
		If msReportType = "Form" Or msReportType = "" Then
			lblTitle.Visible = False
		End If
		'	span.Controls.Add(lblTitle)
		span.Controls.Add(b)
		span.Controls.Add(cmd)
		span.Controls.Add(c)
		ctrlHelpText = New Label
		ctrlHelpText.CssClass = "form-text text-muted"
		ctrlHelpText.Attributes.Add("style", "font-size:xx-small; Order:10;")
		ctrlHelpText.Text = msclsBaseHelpText ' & "Here is the Help Text"


		Controls.Add(span)

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
		'	Dim lsCtrl As String = Page.Request.Params.Get("__EVENTTARGET")
		'	If lsCtrl.ToUpper.Contains("CMDEDITMODE") Then EditMode = True
		'If
		'EditMode = True Then
		'	cmdFieldDef.Visible = True
		'	cmdFieldDel.Visible = True
		'Else
		'	If cmdFieldDel Is Nothing Then
		'	Else
		'		cmdFieldDel.Visible = False
		'		cmdFieldDef.Visible = False
		'	End If
		'End If
		RenderSubControls(writer)
    End Sub
    Protected Overridable Sub RenderSubControls(writer As HtmlTextWriter)
        lblTitle2.RenderControl(writer)
    End Sub

	Private Sub ctrlBase_Init(sender As Object, e As EventArgs) Handles Me.Init
		Dim t As DateTime = Now
	End Sub
	Public Delegate Sub cmdFieldDelegate(sender As Object, e As EventArgs)
	Public Event cmdField As cmdFieldDelegate
	Public Event EditReportField(ReportID As Long, ControlNum As Long, FieldID As String)
	Public Overridable Sub LoadReportFields(sender As Object, e As EventArgs)

		RaiseBubbleEvent(sender, e)
		'		Dim cbut As cmdFieldDelegate = New cmdFieldDelegate(AddressOf TestButton)
		'		cbut.Invoke(Me, New EventArgs)
	End Sub
	Public Overridable Sub DeleteReportFields(sender As Object, e As EventArgs)

		RaiseBubbleEvent(sender, e)

	End Sub
	Sub TestButton(sender As Object, e As EventArgs)
		MyBase.RaiseBubbleEvent(sender, e)
		RaiseBubbleEvent(sender, e)
		Stop
	End Sub
	Public Overridable Property ControlPanelcss() As String
		Get
			ControlPanelcss = msControlPanelcss
		End Get
		Set(sValue As String)
			msControlPanelcss = sValue
			'uPanel1.Attributes.Add("class", sValue)
		End Set
	End Property

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