Option Strict Off
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data

<DefaultProperty("Text"), ToolboxData("<{0}:ReportContainer runat=server></{0}:ReportContainer>")>
Public Class ReportContainer
    Inherits CompositeControl

    Dim cboReportCategory As DropDownList
    Dim updatePanel1 As Panel ' UpdatePanel
    Dim updatePanel2 As Panel ' UpdatePanel
    Dim lstReports As ListBox
    Dim txtDebug As TextBox
    Dim litmsg As Literal
    Dim up2 As Panel ' UpdatePanel
    Dim LoadImage As System.Web.UI.WebControls.Image
    Dim tbl As Table
    Dim tblRow As TableRow
    Dim tblCell As TableCell
    Dim btn As Button
    Dim mbDebug As Boolean
    ' Dim asyncPostBackTrigger1 As AsyncPostBackTrigger
    '   Dim asyncPostbackTrigger2 As AsyncPostBackTrigger
    '  Dim UpdateProgress1 As UpdateProgress

    <Category("Appearance")>
    <Description("Show Debug")>
    <DefaultValue("False")>
    Public Property showDebug() As Boolean
        Get
            EnsureChildControls()
            Return mbDebug

        End Get
        Set(ByVal value As Boolean)
            EnsureChildControls()
            mbDebug = value
        End Set
    End Property

    Protected Overrides Sub RecreateChildControls()
        EnsureChildControls()
    End Sub
    Protected Overrides Sub CreateChildControls()
        Controls.Clear()

        btn = New Button
        btn.ID = "btn"
        btn.Text = "Try Me"
        AddHandler btn.Click, AddressOf btn_Click
        cboReportCategory = New DropDownList
        cboReportCategory.ID = "cboReportCategory"
        updatePanel1 = New Panel ' UpdatePanel

        updatePanel2 = New Panel ' UpdatePanel
        up2 = New Panel 'UpdatePanel
        updatePanel1.ID = "updatePanel1"
        '  updatePanel1.
        updatePanel2.ID = "updatePanel2"
        up2.ID = "up2"
        AddHandler up2.Load, AddressOf up2_Load
        txtDebug = New TextBox
        txtDebug.ID = "txtDebug"
        litmsg = New Literal
        litmsg.ID = "litmsg"
        debug("Clear")
        litmsg.Text = "<B>This is the litmsg control</B>"
        debug("CreateChildControls Ya")
        '   asyncPostBackTrigger1 = New AsyncPostBackTrigger
        '  asyncPostBackTrigger1.ControlID = "cboReportCategory"
        ' asyncPostbackTrigger2 = New AsyncPostBackTrigger
        'asyncPostbackTrigger2.ControlID = "lstReports"
        lstReports = New ListBox
        lstReports.ID = "lstReports"
        lstReports.Height = 400
        lstReports.AutoPostBack = True
        AddHandler lstReports.Load, AddressOf lstReports_Load
        tbl = New Table
        tbl.ID = "tbl"
        tblRow = New TableRow
        tblRow.ID = "tblRow"
        tblCell = New TableCell
        tblCell.ID = "tblCell"
        '  UpdateProgress1 = New UpdateProgress
        '  UpdateProgress1.ID = "UpdateProgress1"
        LoadImage = New System.Web.UI.WebControls.Image
        LoadImage.ImageUrl = "images/Loading.gif"
        '  UpdateProgress1.Controls.Add(LoadImage)


        Controls.Add(cboReportCategory)
        AddHandler cboReportCategory.SelectedIndexChanged, AddressOf lstReports_Load
        AddHandler cboReportCategory.TextChanged, AddressOf lstReports_Load
        Controls.Add(btn)
        'updatePanel1.BorderStyle = BorderStyle.Dashed
        'updatePanel1.BorderWidth = 5
        'updatePanel2.BorderStyle = BorderStyle.Double
        'updatePanel2.BorderWidth = 5
        'up2.BorderColor = Color.BurlyWood
        'up2.BorderWidth = 5
        Controls.Add(updatePanel1)
        Controls.Add(updatePanel2)
        Controls.Add(up2)
        Controls.Add(txtDebug)
        Controls.Add(litmsg)
        'updatePanel2.Triggers.Add(asyncPostBackTrigger1)
        'up2.Triggers.Add(asyncPostbackTrigger2)
        up2.Controls.Add(lstReports) 'up2.ContentTemplateContainer.Controls.Add(lstReports)
        tblRow.Cells.Add(tblCell)
        tbl.Rows.Add(tblRow)
        Me.Controls.Add(tbl) 'up2.ContentTemplateContainer.Controls.Add(tbl)
        Me.tbl.BorderWidth = 10

        ' Controls.Add(UpdateProgress1)
        'UpdateProgress1.Controls.Add(LoadImage)
        ReportContainer_Load()
        lstReports_Load()
        ' up2_Load()
        CreateChildControlsSub()
        AddHandler lstReports.SelectedIndexChanged, AddressOf up2_Load
    End Sub
    Protected Overridable Sub CreateChildControlsSub()

    End Sub
    Sub btn_click()
        debug("btnClick" + Now.ToShortTimeString)
    End Sub
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

    Protected Overrides Sub Render(writer As HtmlTextWriter)
        AddAttributesToRender(writer)
        writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "1")
        writer.RenderBeginTag(HtmlTextWriterTag.Table)
        writer.RenderBeginTag(HtmlTextWriterTag.Tr)
        writer.RenderBeginTag(HtmlTextWriterTag.Td)
        cboReportCategory.RenderControl(writer)
        litmsg.RenderControl(writer)
        btn.RenderControl(writer)

        updatePanel1.RenderControl(writer)
        ' lstReports.RenderControl(writer)
        writer.RenderEndTag() ' </td>
        writer.RenderBeginTag(HtmlTextWriterTag.Td)
        up2.RenderControl(writer)
        tbl.RenderControl(writer)
        ' UpdateProgress1.RenderControl(writer)
        ''LoadImage.RenderControl(writer)

        writer.RenderEndTag() ' </td>
        writer.RenderEndTag() ' </tr>
        writer.RenderEndTag() ' </table>
        RenderSubControls(writer)
        ReportContainer_Load() 'Me, New EventArgs)
    End Sub
    Protected Overridable Sub RenderSubControls(writer As HtmlTextWriter)

    End Sub

    '***********************************************************
    '**** CodeBehind
    '***********************************************************
    Dim mdlDataLoader As New clsDataLoader
    Dim mcReports As New Collection
    Dim mcReportCategories As New Collection
    Dim msReportCategoryType As String = ""
    Public ReportOut As String = ""
    Dim msOpenArgs As String
    Dim msCnStr As String
    Dim mb_UseFilter As Boolean
    Dim rptCtrls As ReportControls
    Dim ReportControl As ReportControl
    Dim msWhereClause As String = ""

    Public Sub New()
        ' debug("New Report Container")
    End Sub

    Public Property WhereClause() As String
        Get
            If rptCtrls.Count > 0 Then
                WhereClause = msWhereClause
                WhereClause = f_CreateWhereClause(True)
                'MsgBox(WhereClause)
            Else
                WhereClause = ""
            End If
        End Get
        Set(ByVal value As String)

        End Set
    End Property
    Public Property ReportCategoryType() As String
        Get
            ReportCategoryType = msReportCategoryType
        End Get
        Set(ByVal value As String)
            msReportCategoryType = value
        End Set
    End Property

    Public Property ReportName() As String
        Get
            If lstReports.SelectedItem Is Nothing Then
                ReportName = ""
            Else
                ReportName = lstReports.SelectedItem.ToString
            End If

        End Get
        Set(ByVal value As String)

        End Set
    End Property


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        '*** Load All the Data sets used by the control
        ' debug("Page_init")
        If System.Web.HttpContext.Current.Session("mdlDataLoader") Is Nothing Then
            mdlDataLoader = New clsDataLoader
            mdlDataLoader.LoadReportCategories()
            mdlDataLoader.LoadAllControls()
            System.Web.HttpContext.Current.Session("mdlDataLoader") = mdlDataLoader
        End If
        mdlDataLoader = System.Web.HttpContext.Current.Session("mdlDataLoader")

        If System.Web.HttpContext.Current.Session("rptCtrls") Is Nothing Then
            rptCtrls = New ReportControls
            rptCtrls.Load()
            System.Web.HttpContext.Current.Session("rptctrls") = rptCtrls
        End If

        rptCtrls = System.Web.HttpContext.Current.Session("rptCtrls")
        mcReports = System.Web.HttpContext.Current.Session("Reports")
    End Sub

    Private Sub ReportContainer_Load() 'ByVal sender As Object, ByVal e As System.EventArgs) 'Handles Me.Load
        If Me.cboReportCategory.Items.Count <= 0 Then

            Dim cat As ReportCategory
            Dim lstItm As ListItem
            Dim ds As DataSet = mdlDataLoader.mdsReportCategories
            Dim dt As DataTable = ds.Tables("Categories")
            Dim dr As DataRow

            debug("Begin")

            cboReportCategory.Items.Clear()
            If mcReportCategories Is Nothing Then
                mcReportCategories = New Collection
            End If
            mcReportCategories.Clear()


            For Each dr In dt.Rows
                If msReportCategoryType = "" Or dr("ReportCategoryType") = msReportCategoryType Then
                    cat = New ReportCategory
                    lstItm = New ListItem
                    cat.HideReportLists = CBool(dr("ReportCategoryHideLists"))
                    'MsgBox(dr("reportCategoryID")) & " " &  dr("reportCategoryDescription")
                    cat.CategoryID = CInt(dr("reportCategoryID"))
                    cat.CategoryDescription = dr("reportCategoryDescription")

                    lstItm.Value = cat.CategoryID
                    lstItm.Text = cat.CategoryDescription
                    cboReportCategory.Items.Add(lstItm)
                    mcReportCategories.Add(cat)
                    lstItm = Nothing
                End If
            Next dr
            dr = Nothing
            dt = Nothing
            ds = Nothing


            '*** Load the Report Controls into the ReportControls Collection
            rptCtrls = New ReportControls
            rptCtrls.Load()

            lstReports.ClearSelection()

            '*** If a starting category has been passed, goto that category
            If Len(msOpenArgs) > 0 Then
                cboReportCategory.SelectedValue = msOpenArgs
                cboReportCategory.Text = msOpenArgs
                ' Call cboReportCategory_SelectedIndexChanged(Me, e)
            End If

        End If

    End Sub



    Private Sub ResizeContainer(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Me.Height = Me.ParentForm.Height - Me.Top - 30
        ' Me.Width = Me.ParentForm.Width - Me.Left - 15
    End Sub

    Protected Sub lstReports_Load() 'ByVal sender As Object, ByVal e As System.EventArgs) 'Handles lstReports.Load
        Dim cat As New ReportCategory
        Dim cat2 As New ReportCategory
        Dim rpt As New Report
        Dim llistitem As New ListItem
        Dim li As Integer = 0
        debug("lstReports.Load")
        If lstReports.SelectedValue <> "" Then
            li = CInt(lstReports.SelectedValue)
        End If

        cat = New ReportCategory
        llistitem = cboReportCategory.SelectedItem
        If llistitem Is Nothing Then Exit Sub
        cat.CategoryID = llistitem.Value
        For Each cat2 In mcReportCategories
            If cat2.CategoryID = llistitem.Value Then
                cat.HideReportLists = cat2.HideReportLists
                Exit For
            End If
        Next
        Me.lstReports.Items.Clear()

        cat.Load()
        mcReports = cat.GetReports
        ' Session("Reports") = mcReports
        For Each rpt In mcReports
            llistitem = New ListItem
            llistitem.Value = rpt.ReportID
            llistitem.Text = rpt.ReportDescription
            Me.lstReports.Items.Add(llistitem)
            llistitem = Nothing
        Next
        On Error Resume Next
        lstReports.SelectedValue = li
        On Error GoTo 0
        If cat.HideReportLists = True Then 'And lstReports.SelectedIndex = -1 And lstReports.Items.Count >= 1 Then
            updatePanel2.Visible = False
            lstReports.SelectedIndex = 0
            lstReports.Visible = False
            cboReportCategory.Visible = False
            txtDebug.Visible = False
        End If

        '*** disable controls on form
        ' sDisableControls()

        '  lstReports.ClearSelection()
        'lstReports.refresh

    End Sub

    Function f_CreateWhereClause(ByVal lbUseFilter As Boolean) As String
        debug("WHereClause")
        f_CreateWhereClause = ""
        Dim lsSQL As String = ""
        Dim ctrl As Control

        Dim lsType As String = ""
        Dim lsTag As String = ""
        Dim rpt As Report
        Dim rptCtrl As ReportControl
        Dim lsDescription As String = ""
        Dim lsCtrlName As String = ""
        sUpdateControls()
        Dim tbl As Table
        Try
            tbl = Me.FindControl("tbl")
            If lbUseFilter = True Then
                debug(tbl.Controls(0).Controls(0).Controls.Count.ToString)
                For Each ctrl In tbl.Controls(0).Controls(0).Controls
                    debug(TypeName(ctrl) & ctrl.ID)

                    '*************************************************************************
                    '*** Check For Mandatory Fields.  They are designated be the Back Color
                    '*************************************************************************
                    lsCtrlName = ctrl.ID
                    debug("checking for Manadatory Fileds ReportContainer f_createwherechause")
                    debug("FindByName: " & ctrl.ID)
                    rptCtrl = rptCtrls.FindByName(lsCtrlName)
                    debug("Found: " & rptCtrl.FieldName)
                    lsType = TypeName(ctrl)
                    lsTag = IIf(Nothing = (rptCtrl.Type), "", rptCtrl.Type)
                    If lsType <> "Label" And lsTag = "ReportControl" Then

                        If rptCtrl.Required And (IsDBNull(rptCtrl.Value) Or rptCtrl.Value = "" Or rptCtrl.Value = "-1") Then
                            '*If ctrl.BackColor = Color.Red And (IsDBNull(ctrl.Text) Or ctrl.Text = "" Or ctrl.Text = "-1") Then
                            ' MsgBox("Required Field must be filled")
                            f_CreateWhereClause = "Cancel"
                            ctrl.Focus()
                            Exit Function
                        End If
                    End If
                Next


                Dim liCounter As Integer
                '***************************************************************
                '*** Build Report Criteria
                '***************************************************************
                If lstReports.SelectedValue = "" Then
                    f_CreateWhereClause = ""
                    Exit Function
                End If

                rpt = FindReportByID(lstReports.SelectedValue)
                rpt.LoadControls("")
                sUpdateControls() '*** Updates rptCtrls with updated Ctrl Values

                For Each ReportControl In rpt.Controls
                    'For Each ReportControl In tbl.Controls(0).Controls(0).Controls
                    If ReportControl.DataOperator = "" Then
                    Else
                        ReportOut = ReportOut & "<tr><td valign='top'>" & ReportControl.FieldDescription
                        ReportOut = ReportOut & "</td><td valign='top'>"
                        ReportOut = ReportOut & "" & ReportControl.DataOperator.ToString
                        ReportOut = ReportOut & "</td><td valign='top'>"
                        For liCounter = 1 To ReportControl.SelectedItems.Count
                            ReportOut = ReportOut & "" & ReportControl.SelectedItems(liCounter).ToString & "<br />"
                        Next
                        ReportOut = ReportOut & "</td><td valign='top'>"
                        For liCounter = 1 To ReportControl.SelectedItems2.Count
                            ReportOut = ReportOut & "" & ReportControl.SelectedItems2(liCounter).ToString & "<br />"
                        Next

                        ReportOut = ReportOut & "</td></tr>"
                        lsSQL = lsSQL & ReportControl.SQL
                    End If
                Next

                '*** Cut Last AND off the Where Clause
                If lsSQL.Length > 4 Then
                    lsSQL = lsSQL.Substring(0, lsSQL.Length - 4)
                Else
                    lsSQL = ""
                End If

                ReportOut = "<table>" & ReportOut & "</table><br />SQL:<br/>"

            Else
                'MsgBox("No Criteria")
                f_CreateWhereClause = ""
            End If

            f_CreateWhereClause = lsSQL
CreateWhere_Exit:
            Exit Function
        Catch err As Exception
            '  Dim st As StackTrace = New StackTrace(Err, True)
            '// Get the top stack frame
            ' Dim frame As StackFrame = st.GetFrame(0)
            ' // Get the line number from the stack frame
            'Dim line As Long = frame.GetFileLineNumber()
            MsgBox(err.Message)
        Finally
            'Resume CreateWhere_Exit
        End Try
    End Function


    Function nz(ByVal lv As VariantType, ByVal lvv As VariantType) As VariantType
        If IsDBNull(lv) Then
            nz = lvv
        Else
            nz = lv
        End If
    End Function
    Public Sub sDispose()
        Dim liCount As Integer = 1
        Dim ctrl As Control
        Dim rptctrl As New ReportControl
        Dim lsCtrlName As String = ""

        Do While liCount > 0

            liCount = 0
            For Each ctrl In Me.Controls
                If ctrl Is Nothing Then
                    lsCtrlName = ""
                Else
                    lsCtrlName = ctrl.ID
                End If
                If lsCtrlName Is Nothing Then
                    lsCtrlName = ""
                End If
                '  txtDebug.Text = txtDebug.Text & Chr(13) & Chr(10) & lsCtrlName & " " '& ctrl.
                If lsCtrlName <> "" Then
                    rptctrl = rptCtrls.FindByName(lsCtrlName)
                    If rptctrl Is Nothing Then
                        ' If ctrl.Tag = "ReportControl" Then
                    Else
                        If lsCtrlName.ToLower <> "ctrltextbox1" And lsCtrlName.ToLower <> "ph" And lsCtrlName.ToLower <> "ctrlbase" And lsCtrlName.ToLower <> "cboreportcategory" And lsCtrlName.ToLower <> "litmsg" And lsCtrlName.ToLower <> "txtdebug" And lsCtrlName.ToLower <> "lstreports" Then
                            ' MsgBox("Disposing of " & ctrl.Name)
                            liCount = liCount + 1
                            ctrl.Dispose()
                        End If
                    End If
                End If
                rptctrl = Nothing
            Next
        Loop

        liCount = liCount


    End Sub
    Function getControlFromName(ByRef containerObj As Object, ByVal name As String) As Control
        Try
            Dim tempCtrl As Control
            For Each tempCtrl In containerObj.Controls
                ' Debug.Print(tempCtrl.Name)
                If tempCtrl.ID.ToUpper.Trim = name.ToUpper.Trim Then
                    Return tempCtrl
                    Exit Function
                End If
            Next tempCtrl
        Catch ex As Exception
        End Try
        Return Nothing
    End Function


    Private Sub sUpdateControls()
        '*** Get GUI Control Values and Update Report Control Objects
        '*** This is done to separate the SQL Logic from the User Controls
        '*** This should allow users to build user controls easier
        ' Exit Sub
        Try
            Dim ctrl As Control
            Dim ctrlb As ctrlBase
            Dim rpt As Report

            Dim rptctrl As ReportControl
            Dim rptctrl2 As ReportControl

            If lstReports.SelectedValue = "" Then Exit Sub
            rpt = FindReportByID(lstReports.SelectedValue)
            rpt.LoadControls(rpt.ReportName)

            '*** Iterate through all Controls, looking for controls with Criteria
            tbl = Me.FindControl("tbl")
            debug(tbl.Controls(0).Controls(0).Controls.Count.ToString)
            For Each ctrl In tbl.Controls(0).Controls(0).Controls

                debug("sUpdateControls:Up2-" & ctrl.ID)
                '*** Controls capable of Criteria have a ReportControl Tag
                rptctrl2 = Nothing
                rptctrl2 = rpt.Controls.FindByName(ctrl.ID)
                If rptctrl2 Is Nothing Then
                    '*** Not a Report Control
                Else
                    'If ctrl.Tag = "ReportControl" Then
                    '*** Get the Control and assign it to ctrlb
                    ctrlb = ctrl
                    ctrlb.mbRefreshList = False
                    '*** Iterate through the controls in the REPORT Object
                    '*** Find out if they have a corresponding User Control
                    For Each rptctrl In rpt.Controls
                        '*** Is THIS the control I am looking for?
                        If rptctrl.ControlName = ctrl.ID Then '*Was ctrl.name
                            '*** Yes, If the Control has been Validated,
                            '*** get the Values
                            If ctrlb.Valid = True Then
                                rptctrl.DataOperator = (ctrlb.DataOperator)
                                rptctrl.DataType = ctrlb.DataType
                                rptctrl.SelectedItems = ctrlb.SelectedItems
                                rptctrl.SelectedItems2 = ctrlb.SelectedItems2
                                rptctrl.Enabled = True
                            Else
                                rptctrl.DataOperator = Nothing
                                rptctrl.DataType = Nothing
                                If rptctrl.SelectedItems.Count > 0 Then
                                    rptctrl.SelectedItems.Clear()
                                End If
                                If rptctrl.SelectedItems2.Count > 0 Then
                                    rptctrl.SelectedItems2.Clear()
                                End If
                                rptctrl.Enabled = False
                            End If
                        End If
                    Next
                End If
            Next
            '**********************************************************************
            '*** Update Module Level Variable with Updated Controls Collection
            '*********************************************************************
            rptCtrls = rpt.Controls
        Catch err As Exception
            debug(err.Message) '& frame.ToString)
        Finally
            'Resume CreateWhere_Exit
        End Try
    End Sub
    Private Sub sUpdateChildren(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Exit Sub
        '*** Updates the List Controls That depend on this object for Criteria
        '*** Example: Kids Listbox would be refilled everytime a new Value was selected from
        '***          The Family Drop Down

        '*** Get GUI Control Values and Update Report Control Objects
        '*** This is done to separate the SQL Logic from the User Controls
        '*** This should allow users to build user controls easier
        Dim rptctrl As ReportControl
        Dim rpt As Report
        rpt = FindReportByID(lstReports.SelectedValue)
        Dim ctrlb As ctrlBase

        Dim ctrlChild As New ReportControl
        Dim childctrl As ReportControlChildControl


        '*** Get the Control and assign it to ctrlb
        ctrlb = sender
        ctrlb.ForceValidation()
        '*** Iterate through the controls in the REPORT Object
        '*** Find out if they have a corresponding User Control
        For Each rptctrl In rpt.Controls
            '*** Is THIS the control I am looking for?
            If rptctrl.ControlName = ctrlb.ID Then
                '*** Yes, If the Control has been Validated,
                '*** get the Values
                If ctrlb.Valid = True Then
                    rptctrl.DataOperator = (ctrlb.DataOperator)
                    rptctrl.DataType = ctrlb.DataType
                    rptctrl.SelectedItems = ctrlb.SelectedItems
                    rptctrl.SelectedItems2 = ctrlb.SelectedItems2
                    rptctrl.Enabled = True
                    For Each childctrl In rptctrl.ControlChildren
                        SetChildListItems(childctrl.Description, rptctrl.SQL)
                    Next
                Else
                    rptctrl.DataOperator = Nothing
                    rptctrl.DataType = Nothing
                    If rptctrl.SelectedItems.Count > 0 Then
                        rptctrl.SelectedItems.Clear()
                    End If
                    If rptctrl.SelectedItems2.Count > 0 Then
                        rptctrl.SelectedItems2.Clear()
                    End If
                    rptctrl.Enabled = False
                End If
            End If
        Next

Exit_Cmdrunreport_Click:
        Exit Sub

Err_Cmdrunreport_Click:
        If Err.Number <> 2501 Then
            MsgBox(Err.Description & " - " & Err.Number)
        End If
        '        Resume
        Resume Exit_Cmdrunreport_Click
    End Sub


    Sub SetChildListItems(ByVal lsControlName As String, ByVal lsSQL As String)
        Dim rptctrl As ReportControl
        Dim lsRowSource As String = ""
        Dim ctrl As ctrlBase
        Dim rpt As Report
        rpt = FindReportByID(lstReports.SelectedValue)

        For Each rptctrl In rpt.Controls
            '*** Is THIS the control I am looking for?
            If rptctrl.ControlName = lsControlName Then
                '*** Yes, If the Control has been Validated,
                '*** get the Values
                '*** Cut Last "and " off the SQL
                If lsSQL.Length >= 4 Then
                    If lsSQL.Substring(lsSQL.Length - 4, 4) = "and " Then
                        lsSQL = lsSQL.Substring(0, lsSQL.Length - 4)
                    End If
                End If
                '*** Cut the Old Where Clause Off the Row Source
                lsRowSource = rptctrl.RowSource
                If lsRowSource.Length >= 5 Then
                    lsRowSource = lsRowSource.ToUpper
                    If lsRowSource.Contains("WHERE") Then
                        lsRowSource = lsRowSource.Substring(0, lsRowSource.IndexOf("WHERE"))
                    End If
                End If
                '*** Add the New Where Clause to the Row Source
                If lsSQL.Length > 5 Then
                    lsSQL = " Where " & lsSQL
                End If
                rptctrl.RowSource = (lsRowSource) & lsSQL
                rptctrl.LoadListItems()
                ctrl = getControlFromName(Me, lsControlName)
                ctrl.ListItems = rptctrl.ListItems
                ctrl.RefreshLists()
            End If
        Next

    End Sub

    Sub debug(ByVal msg As String)
        If mbDebug = True Then
            If msg = "Clear" Then
                litmsg.Text = ""
            Else
                litmsg.Text = litmsg.Text & msg & "<br/>" '  Chr(13) & Chr(10)
            End If
        End If
    End Sub


    Protected Sub up2_Load() 'ByVal sender As Object, ByVal e As System.EventArgs) 'Handles up2.Load

        debug("Clear")
        '  Exit Sub
        Dim strScript As String = "Alert('Yes');"
        If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript")) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType, "clientScript", strScript, True)
        End If
        Dim rpt As New Report
        Dim ctrl As ctrlBase
        Dim reportCtrls As ReportControls

        '*** Get rid of Current Report Controls
        Me.tblCell.Controls.Clear()

        '*** initialize selection criteria fields
        mb_UseFilter = True

        '*** If No Report Selected, Exit Sub
        If lstReports.SelectedIndex < 0 Then Exit Sub

        '*** Get Selected Report Object
        rpt = FindReportByID(lstReports.SelectedValue)

        '*** Load NON Graphical Report Control Objects
        reportCtrls = rpt.LoadControls(msCnStr)
        debug("rpt.LoadControls" & msCnStr)

        '*** Load Graphical User Control Report Objects
        For Each ReportControl In reportCtrls
            debug("ReportControl.Type:" & ReportControl.Type)
            Select Case UCase(ReportControl.Type)
                Case "DATEBOX"
                    ctrl = New ctrlDateBox(ReportControl) 'ctrlTextBox '(ReportControl)
                    ctrl.mrptCtrl = ReportControl
                Case "LISTBOX"
                    ctrl = New ctrlListBox(ReportControl)
                    ctrl.mrptCtrl = ReportControl
                Case "CHECKBOX"
                    ctrl = New ctrlCheckBox(ReportControl)
                    debug("ctrl.FIeldName: " & ctrl.FieldName)
                    ctrl.mrptCtrl = ReportControl
                    debug("Desc:" & ReportControl.Description)
                        ''  ctrl = New ctrlListbox(ReportControl)
                        'ctrl = LoadControl("ctrlCheckBox.ascx")
                        'ctrl.mrptCtrl = ReportControl

                Case "COMBOBOX"
                    ctrl = New ctrlComboBox(ReportControl)
                    ctrl.mrptCtrl = ReportControl
                    ' AddHandler ctrl.ControlUpdated, AddressOf sUpdateChildren '(sender, e) ' sUpdateChildren
                Case Else
                    ctrl = New ctrlTextBox(ReportControl) 'LoadControl("ctrlTextBox.ascx")
                    ctrl.mrptCtrl = ReportControl
            End Select
            If ctrl Is Nothing Then
                debug("CTRL is Nothing")
            Else
                debug("CTRL ID is:" & ctrl.ID)
            End If
            '    AddHandler  ctrl.Leave, AddressOf sUpdateChildren
            ctrl.Tag = "ReportControl"
            ctrl.ID = ReportControl.ControlName
            ctrl.FieldName = ReportControl.FieldDescription
            ctrl.CssClass = "CustomControl"
            If ReportControl IsNot Nothing Then
                On Error Resume Next
                tblCell.Controls.Add(ctrl)
                On Error GoTo 0
                ctrl = Nothing
            End If
        Next
        EnsureChildControls()
    End Sub

    Function FindReportByID(ByVal llReportID As Long) As Report
        Dim rpt As Report
        For Each rpt In mcReports
            If llReportID = rpt.ReportID Then
                FindReportByID = rpt
                Exit Function
            End If
        Next
        FindReportByID = New Report
    End Function
    Public Sub Refresh()
        debug("Refresh")
        msWhereClause = f_CreateWhereClause(True)
        debug(msWhereClause)
    End Sub



    Private Sub ReportContainer_DataBinding(sender As Object, e As EventArgs) Handles Me.DataBinding
        debug("data Binding")
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
