Imports System
Imports System.Data
Imports System.Data.OleDb

Partial Class ReportContainer
    Inherits System.Web.UI.UserControl
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
        If Session("mdlDataLoader") Is Nothing Then
            mdlDataLoader = New clsDataLoader
            mdlDataLoader.LoadReportCategories()
            mdlDataLoader.LoadAllControls()
            Session("mdlDataLoader") = mdlDataLoader
        End If
        mdlDataLoader = Session("mdlDataLoader")

        If Session("rptCtrls") Is Nothing Then
            rptCtrls = New ReportControls
            rptCtrls.Load()
            Session("rptctrls") = rptCtrls
        End If

        rptCtrls = Session("rptCtrls")
        mcReports = Session("Reports")
    End Sub

    Private Sub ReportContainer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.cboReportCategory.Items.Count <= 0 Then

            Dim cat As ReportCategory
            Dim lstItm As ListItem
            Dim ds As DataSet = mdlDataLoader.mdsReportCategories
            Dim dt As DataTable = ds.Tables("Categories")
            Dim dr As DataRow



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

    Protected Sub lstReports_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstReports.Load
        Dim cat As New ReportCategory
        Dim cat2 As New ReportCategory
        Dim rpt As New Report
        Dim llistitem As New ListItem
        Dim li As Integer = 0

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
        Session("Reports") = mcReports
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
            UpdatePanel2.Visible = False
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
        Dim lsSQL As String = ""
        Dim ctrl As Control

        Dim lsType As String = ""
        Dim lsTag As String = ""
        Dim rpt As Report
        Dim rptCtrl As ReportControl
        Dim lsDescription As String = ""
        Dim lsCtrlName As String = ""
        sUpdateControls()

        On Error GoTo CreateWhere_Error

        If lbUseFilter = True Then
            For Each ctrl In Me.Controls
                txtDebug.Text = txtDebug.Text & "<br />" & (TypeName(ctrl) & ctrl.ID)

                '*************************************************************************
                '*** Check For Mandatory Fields.  They are designated be the Back Color
                '*************************************************************************
                lsCtrlName = ctrl.ID
                rptCtrl = rptCtrls.FindByName(lsCtrlName)
                '   debug("FindByName: " & ctrl.ID)
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
            For Each ReportControl In rpt.Controls
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
CreateWhere_Error:
        MsgBox(Err.Description)
        '  Resume
        Resume CreateWhere_Exit
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
        On Error GoTo sUpdateControls_Error
        Dim rpt As Report

        '*** Get GUI Control Values and Update Report Control Objects
        '*** This is done to separate the SQL Logic from the User Controls
        '*** This should allow users to build user controls easier
        Dim rptctrl As ReportControl
        Dim rptctrl2 As ReportControl
        If lstReports.SelectedValue = "" Then Exit Sub
        rpt = FindReportByID(lstReports.SelectedValue)
        Dim ctrl As Control
        Dim ctrlb As clsBase

        '*** Iterate through all Controls, looking for controls with Criteria
        For Each ctrl In Me.tblcell.Controls

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
sUpdateControls_Exit:
        Exit Sub
sUpdateControls_Error:
        MsgBox(Err.Number & " " & Err.Description)
        Resume Next
    End Sub
    Private Sub sUpdateChildren(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
        ' litmsg.text = litmsg.text & msg & Chr(13) & Chr(10)
    End Sub


    Protected Sub UpdatePanel2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles up2.Load
        'System.Threading.Thread.Sleep(2000)
        'Exit Sub
        Dim strScript As String = "Alert('Yes');"
        If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript")) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType, "clientScript", strScript, True)
        End If
        Dim rpt As New Report
        Dim ctrl As clsBase '* was ctrlbase
        Dim reportCtrls As ReportControls
        Dim iTop As Integer
        'Dim iLeft As Integer
        Dim iHeight As Integer
        Dim iWidth As Integer = 2
        '  Dim ctrlTemp As Control
        '*** Get rid of Current Report Controls
        Me.tblcell.Controls.Clear()

        '   sDispose()

        '*** initialize selection criteria fields
        mb_UseFilter = True

        '*** If No Report Selected, Exit Sub
        If lstReports.SelectedIndex < 0 Then
            Exit Sub
        End If

        '*** Get Selected Report Object
        rpt = FindReportByID(lstReports.SelectedValue)

        '*** Load NON Graphical Report Control Objects
        reportCtrls = rpt.LoadControls(msCnStr)

        '*** Load Graphical User Control Report Objects
        iHeight = 20
        '*iLeft = lstReports.Width + lstReports.Left + 2


        For Each ReportControl In reportCtrls
            Select Case UCase(ReportControl.Type)
                Case "DATEBOX"
                    '  ctrl = New ctrlDate(ReportControl)
                    ctrl = New ctrlTextBox(ReportControl)
                    ctrl = LoadControl("ctrlDateBox.ascx")
                    ctrl.mrptCtrl = ReportControl

                Case "LISTBOX"
                    '  ctrl = New ctrlListbox(ReportControl)
                    ctrl = LoadControl("ctrlListBox.ascx")
                    ctrl.mrptCtrl = ReportControl

                Case "CHECKBOX"
                    '  ctrl = New ctrlListbox(ReportControl)
                    ctrl = LoadControl("ctrlCheckBox.ascx")
                    ctrl.mrptCtrl = ReportControl

                Case "COMBOBOX"
                    'ctrl = New ctrlCombobox(ReportControl)
                    ctrl = LoadControl("ctrlComboBox.ascx")
                    ctrl.mrptCtrl = ReportControl
                    ' AddHandler ctrl.ControlUpdated, AddressOf sUpdateChildren '(sender, e) ' sUpdateChildren
                Case Else
                    ctrl = LoadControl("ctrlTextBox.ascx")
                    ctrl.mrptCtrl = ReportControl
            End Select
            '    AddHandler  ctrl.Leave, AddressOf sUpdateChildren

            'might need this  ctrl.Tag = "ReportControl"
            ctrl.FieldName = ReportControl.FieldDescription
            ctrl.ListItems = ReportControl.ListItems
            ctrl.DataType = ReportControl.ControlFieldType
            ctrl.ID = ReportControl.ControlName '*Was ctrl.name
            '*ctrl.Left = iLeft + 60
            '*ctrl.Top = iTop

            ' Dim nusrctrl As ctrlTextBox = CType(LoadControl("ctrlTextBox.ascx"), ASP.ctrltextbox_ascx)
            ' Controls.Add(nusrctrl)
            On Error Resume Next
            tblcell.Controls.Add(ctrl)
            On Error GoTo 0


            '*    iHeight = ctrl.Height
            ctrl = Nothing

            iTop = iTop + iHeight + 1


        Next
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
        msWhereClause = f_CreateWhereClause(True)
    End Sub
End Class
