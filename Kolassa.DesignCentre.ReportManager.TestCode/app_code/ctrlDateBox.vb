
Imports System.ComponentModel
Imports System.Drawing

<DefaultProperty("Text"), ToolboxData("<{0}:ctrlCheckBox_ runat=server></{0}:ctrlCheckBox>"), ToolboxBitmap(GetType(Calendar))>
Public Class ctrlDateBox
    Inherits ctrlBase

    Dim lblFieldName As Label
    Dim DropDownList1 As DropDownList
    Dim UpdatePanel1 As UpdatePanel
    Dim lblField1 As Label
    Dim lblField2 As Label
    Dim ctrlField1 As TextBox
    Dim ctrlField2 As TextBox
    Dim CustomValidator1 As CustomValidator
    Dim trig As AsyncPostBackTrigger
    Dim ImageButton1, ImageButton2 As ImageButton
    Dim Calendar1, Calendar2 As Calendar
    Dim Panel1, Panel2 As Panel

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
        li = New ListItem("Year To Date")
        DropDownList1.Items.Add(li)
        li = New ListItem("Month To Date")
        DropDownList1.Items.Add(li)
        li = New ListItem("Quarter To Date")
        DropDownList1.Items.Add(li)
        li = New ListItem("This Quarter")
        DropDownList1.Items.Add(li)
        li = New ListItem("Last Quarter")
        DropDownList1.Items.Add(li)
        li = New ListItem("This Month")
        DropDownList1.Items.Add(li)
        li = New ListItem("Last Month")
        DropDownList1.Items.Add(li)
        li = New ListItem("Today")
        DropDownList1.Items.Add(li)
        li = New ListItem("Last Year")
        DropDownList1.Items.Add(li)

        DropDownList1.AutoPostBack = True
        DropDownList1.ToolTip = "Select the Operator"
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
        lblField2.Visible = False
        ctrlField1 = New TextBox
        ctrlField1.ID = "ctrlField1"
        ctrlField2 = New TextBox
        ctrlField2.ID = "ctrlField2"
        ctrlField2.Visible = False
        CustomValidator1 = New CustomValidator
        CustomValidator1.ID = "CustomValidator1"
        ImageButton1 = New ImageButton
        ImageButton1.ID = "ImageButton1"
        ImageButton2 = New ImageButton
        ImageButton2.ID = "ImageButton2"
        Calendar1 = New Calendar
        Calendar1.ID = "Calendar1"
        Calendar2 = New Calendar
        Calendar2.ID = "Calendar2"
        Panel1 = New Panel
        Panel1.ID = "Panel1"
        Panel1.Visible = True
        Panel2 = New Panel
        Panel2.ID = "Panel2"
        Panel2.Visible = True

        AddHandler DropDownList1.TextChanged, AddressOf DropDownList1_TextChanged
        AddHandler UpdatePanel1.DataBinding, AddressOf UpdatePanel1_DataBinding
        AddHandler CustomValidator1.ServerValidate, AddressOf CustomValidator1_ServerValidate
        AddHandler ImageButton1.Click, AddressOf ImageButton1_Click
        AddHandler ImageButton2.Click, AddressOf ImageButton2_Click
        AddHandler Calendar1.SelectionChanged, AddressOf Calendar1_SelectionChanged
        AddHandler Calendar2.SelectionChanged, AddressOf Calendar2_SelectionChanged
        Controls.Add(lblFieldName)
        Controls.Add(DropDownList1)
        Controls.Add(UpdatePanel1)
        UpdatePanel1.ContentTemplateContainer.Controls.Add(lblField1)
        UpdatePanel1.ContentTemplateContainer.Controls.Add(ctrlField1)
        UpdatePanel1.ContentTemplateContainer.Controls.Add(ImageButton1)
        UpdatePanel1.ContentTemplateContainer.Controls.Add(lblField2)
        UpdatePanel1.ContentTemplateContainer.Controls.Add(ctrlField2)
        UpdatePanel1.ContentTemplateContainer.Controls.Add(ImageButton2)
        UpdatePanel1.ContentTemplateContainer.Controls.Add(CustomValidator1)
        Controls.Add(Panel1)
        Controls.Add(Panel2)
        Panel1.Controls.Add(Calendar1)
        Panel2.Controls.Add(Calendar2)

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
        UpdatePanel1.RenderControl(writer)
        Panel1.RenderControl(writer)
        Panel2.RenderControl(writer)
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
    <Category("Appearance")>
    <Description("Changes the way it it looks")>
    Public Property ImageButtonImageURL() As String
        Get
            EnsureChildControls()
            If (ImageButton1.ImageUrl) Is Nothing Then
                Return String.Empty
            Else
                Return ImageButton1.ImageUrl
            End If

        End Get
        Set(ByVal value As String)
            EnsureChildControls()
            ImageButton1.ImageUrl = value
            ImageButton2.ImageUrl = value
        End Set
    End Property

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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UpdatePanel1.DataBind()
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
    '*** New From CTRLDATEBOX
    Public Sub PickDate()
        Me.ctrlField1.Text = Calendar1.SelectedDate.ToShortDateString()
        ToggleCalendar()
    End Sub
    Private Sub ToggleCalendar()
        If Panel1.Visible Then
            Panel1.Visible = False
            Panel1.CssClass = "calendarHide"
        Else
            Panel1.Visible = True
            Panel1.CssClass = "calendarShow"
        End If
    End Sub
    Public Sub PickDate2()
        Me.ctrlField2.Text = Calendar2.SelectedDate.ToShortDateString()
        ToggleCalendar2()
    End Sub
    Private Sub ToggleCalendar2()
        If Panel2.Visible Then
            Panel2.Visible = False
            Panel2.CssClass = "calendarHide"
        Else
            Panel2.Visible = True
            Panel2.CssClass = "calendarShow"
        End If
    End Sub
    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        ToggleCalendar()
    End Sub

    Protected Sub Calendar1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PickDate()
    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        ToggleCalendar2()
    End Sub

    Protected Sub Calendar2_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PickDate2()
    End Sub

    Public Overrides Sub SetFields(ByVal lsOption As String, ByVal lsValue As String)
        Dim lblfn As Label = Me.FindControl("lblFieldName")
        Dim lblf1 As Label = Me.FindControl("lblField1")
        Dim lblf2 As Label = Me.FindControl("lblField2")
        Dim ctrl2 As Control = Me.FindControl("ctrlField2")
        msOperator = lsOption
        Dim ls1 As String = ""
        Dim ls2 As String = ""
        lblfn.Text = msFieldName
        Me.ImageButton2.Visible = False
        Select Case lsOption
            Case ("Equals"), "="
                lblf1.Text = "="
                lblf2.Visible = False
                ctrl2.Visible = False
            Case ("GreaterThan"), ">"
                lblf1.Text = ">"
                lblf2.Visible = False
                ctrl2.Visible = False
            Case ("LessThan"), "<"
                lblf1.Text = "<"
                lblf2.Text = "And <="
                lblf2.Visible = False
                ctrl2.Visible = False
            Case ("GreaterThanEqual"), ">="
                lblf1.Text = ">="
                lblf2.Text = "And <="
                lblf2.Visible = False
                ctrl2.Visible = False
            Case ("LessThanEqual"), "<="
                lblf1.Text = "<="
                lblf2.Text = "And <="
                lblf2.Visible = False
                ctrl2.Visible = False
            Case ("Between"), "Year To Date", "Month To Date", "Last Year", "Last Month", "This Quarter", "Last Quarter", "Quarter To Date",
                   "This Year", "This Month", "Today"
                lblf1.Text = ">="
                lblf2.Text = "And <="
                lblf2.Visible = True
                ctrl2.Visible = True
                ImageButton2.Visible = True

                fGetDateRange(lsOption, ls1, ls2)
                ctrlField1.Text = ls1
                ctrlField2.Text = ls2
            Case Else
                lblf1.Text = ">="
                lblf2.Text = "And <="
                lblf2.Visible = True
                lblf2.Visible = True
        End Select
    End Sub
    Function fGetDateRange(ByVal lsType As String, ByRef lsDate1 As String, ByRef lsDate2 As String) As Boolean
        Dim ldTemp As Date
        Dim ldDate1 As Date
        Dim ldDate2 As Date
        Dim lbClear As Boolean = False
        fGetDateRange = True
        Select Case lsType
            Case "Last Month"
                ldTemp = DateAdd(DateInterval.Month, -1, Now)
                ldDate1 = CDate(ldTemp.Month & "/1/" & ldTemp.Year)
                ldDate2 = DateAdd(DateInterval.Month, 1, ldDate1)
                ldDate2 = DateAdd(DateInterval.Day, -1, ldDate2)
            Case "This Month"
                ldTemp = Now
                ldDate1 = CDate(ldTemp.Month & "/1/" & ldTemp.Year)
                ldDate2 = DateAdd(DateInterval.Month, 1, ldDate1)
                ldDate2 = DateAdd(DateInterval.Day, -1, ldDate2)
            Case "Month To Date"
                ldTemp = Now
                ldDate1 = CDate(ldTemp.Month & "/1/" & ldTemp.Year)
                ldDate2 = ldTemp
            Case "Last Year"
                ldTemp = DateAdd(DateInterval.Year, -1, Now)
                ldDate1 = CDate("1/1/" & ldTemp.Year)
                ldDate2 = CDate("12/31/" & ldTemp.Year)
            Case "This Year"
                ldTemp = Now
                ldDate1 = CDate("1/1/" & ldTemp.Year)
                ldDate2 = CDate("12/31/" & ldTemp.Year)
            Case "Year To Date"
                ldTemp = Now
                ldDate1 = CDate("1/1/" & ldTemp.Year)
                ldDate2 = ldTemp
            Case "Last Quarter"
                ldTemp = DateAdd(DateInterval.Quarter, -1, Now)
                fGetDateRange = fGetQuarter(ldTemp, ldDate1, ldDate2)
            Case "This Quarter"
                ldTemp = Now
                fGetDateRange = fGetQuarter(ldTemp, ldDate1, ldDate2)
            Case "Quarter To Date"
                ldTemp = Now
                fGetDateRange = fGetQuarter(ldTemp, ldDate1, ldDate2)
                ldDate2 = Now
            Case "Today"
                ldTemp = Now.Date
                ldDate1 = ldTemp
                ldDate2 = ldTemp.AddDays(1)
            Case "Between"
                lbClear = True


        End Select
        If lbClear = True Then
            lsDate1 = ""
            lsDate2 = ""
        Else
            lsDate1 = ldDate1
            lsDate2 = ldDate2
        End If
    End Function
    Function fGetQuarter(ByVal ldTemp As Date, ByRef ldDate1 As Date, ByRef ldDate2 As Date) As Boolean
        Dim liMonth As Integer
        fGetQuarter = True
        liMonth = (Int((ldTemp.Month - 1) / 3) * 3) + 1
        ldDate1 = CDate(liMonth & "/1/" & ldTemp.Year)
        ldDate2 = DateAdd(DateInterval.Quarter, 1, ldDate1)
        ldDate2 = DateAdd(DateInterval.Day, -1, ldDate2)

    End Function



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
