Partial Class ctrlDatebox
    Inherits clsBase

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
    Protected Sub UpdatePanel1_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdatePanel1.DataBinding
        SetFields(Me.DropDownList1.Text, Me.DropDownList1.SelectedValue)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UpdatePanel1.DataBind()
    End Sub
    Protected Sub DropDownList1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList1.TextChanged
        SetFields(Me.DropDownList1.Text, Me.DropDownList1.SelectedValue)
    End Sub
    Protected Sub CustomValidator1_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
        mbValid = False
        ForceValidation()
        args.IsValid = mbValid
        CustomValidator1.ErrorMessage = msValidationReason
    End Sub
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
End Class
