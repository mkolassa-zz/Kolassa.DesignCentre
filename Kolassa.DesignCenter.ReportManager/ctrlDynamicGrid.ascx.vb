Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls


Public Class ReportManager_DynamicGrid

    Inherits System.Web.UI.UserControl
    Public msDS As DataSet
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

    Public Property GridDataSet() As DataSet
        Get
            GridDataSet = Session("msDS")
        End Get
        Set(ByVal value As DataSet)
            Session("msds") = value
        End Set
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If IsPostBack Then
            If Session("msDS") Is Nothing Then
                ' GetDataSet(TableGridView.DataSource)
            Else
                '   GetDataSet(Session("msDS")) '  
                CreateTemplatedGridView()
            End If
        End If
    End Sub

 
    Sub GetDataSet(ByVal ds As DataSet)
        If ds Is Nothing Then
            ds = GridDataSet
        Else
            GridDataSet = ds
        End If
        If ds Is Nothing Then Exit Sub
        Dim dt As DataTable

        dt = ds.Tables(0)

        '*** clear any existing columns
        grdMain.Columns.Clear()
        Dim dr As DataControlRowType
        Dim colName As String
        Dim lsTypeName As String
        '       Dim BtnTmpField As TemplateField = New TemplateField()
        '     BtnTmpField.ItemTemplate = New DynamicallyTemplatedGridViewHandler(ListItemType.Item, "...", "Command")
        '     BtnTmpField.HeaderTemplate = New DynamicallyTemplatedGridViewHandler(ListItemType.Header, "...", "Command")
        '     BtnTmpField.EditItemTemplate = New DynamicallyTemplatedGridViewHandler(ListItemType.EditItem, "...", "Command")
        '     grdMain.Columns.Add(BtnTmpField)
        '*** walk the DataTable and add columns to the GridView
        Dim i As Integer
        For i = 0 To dt.Columns.Count - 1

            Dim tf As TemplateField = New TemplateField()

            '*** create the data rows
            'On Error Resume Next
            dr = DataControlRowType.DataRow
            colName = dt.Columns(i).ColumnName
            lsTypeName = dt.Columns(i).DataType.Name
            'On Error GoTo 0
            tf.ItemTemplate = New DynamicallyTemplatedGridViewHandler(dr, colName, lsTypeName)
            tf.EditItemTemplate = New DynamicallyTemplatedGridViewHandler(dr, colName, lsTypeName)
            '*** create the header
            tf.HeaderTemplate = New DynamicallyTemplatedGridViewHandler(DataControlRowType.Header, dt.Columns(i).ColumnName, dt.Columns(i).DataType.Name)
            '*** add to the GridView
            grdMain.Columns.Add(tf)
        Next

        '*** bind and display the data
        System.Diagnostics.Debug.WriteLine("Edit Index" & grdMain.EditIndex)
        ' TableGridView.EditIndex = 0
        grdMain.DataSource = dt
        grdMain.DataBind()
        System.Diagnostics.Debug.WriteLine("Edit Index" & grdMain.EditIndex)
        grdMain.Visible = True
    End Sub

    Protected Sub btnDisplay_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDisplay.Click

        '*** create new DataTable from user input
        Dim lsCN As String
        lsCN = System.Configuration.ConfigurationManager.ConnectionStrings.Item("ReportManager").ToString
        ' "Data Source=localhost\\SQL2005Initial Catalog=AdventureWorks"                 + "Integrated Security=True"
        Dim conn As New OleDbConnection 'SqlConnection(conn)
        conn = New OleDbConnection(lsCN)
        '***conn = New SqlConnection(connectionString)
        Dim dtReport As DataTable
        dtReport = New DataTable()
        'SqlCommand(cmd = New SqlCommand(txtQuery.Text))
        Dim cmd As OleDbCommand
        cmd = New OleDbCommand(txtQuery.Text)
        cmd.CommandType = CommandType.Text
        cmd.Connection = conn
        Dim da As New OleDbDataAdapter
        'SqlDataAdapter(da = New SqlDataAdapter())
        da.SelectCommand = cmd
        da.Fill(dtReport)

        '*** clear any existing columns
        grdMain.Columns.Clear()
        Dim dr As DataControlRowType
        Dim colName As String
        Dim lsTypeName As String

        '*** walk the DataTable and add columns to the GridView
        Dim i As Integer
        For i = 0 To dtReport.Columns.Count - 1

            Dim tf As TemplateField = New TemplateField()

            '*** create the data rows
            'On Error Resume Next
            dr = DataControlRowType.DataRow
            colName = dtReport.Columns(i).ColumnName
            lsTypeName = dtReport.Columns(i).DataType.Name
            'On Error GoTo 0 

            tf.ItemTemplate = New DynamicallyTemplatedGridViewHandler(dr, colName, lsTypeName)
            '*** create the header
            tf.HeaderTemplate = New DynamicallyTemplatedGridViewHandler(DataControlRowType.Header, dtReport.Columns(i).ColumnName, dtReport.Columns(i).DataType.Name)
            '*** add to the GridView
            grdMain.Columns.Add(tf)
        Next

        '*** bind and display the data
        grdMain.DataSource = dtReport
        grdMain.DataBind()
        grdMain.Visible = True
    End Sub



    Public Property WhereClause() As String
        Get
            If rptCtrls.Count > 0 Then
                WhereClause = msWhereClause
                'WhereClause = f_CreateWhereClause(True)
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



    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        '*** Load All the Data sets used by the control
     
    End Sub

    Private Sub ReportContainer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub



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


  
    Private Sub sUpdateChildren(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '*** Updates the List Controls That depend on this object for Criteria
        '*** Example: Kids Listbox would be refilled everytime a new Value was selected from
        '***          The Family Drop Down

        '*** Get GUI Control Values and Update Report Control Objects
        '*** This is done to separate the SQL Logic from the User Controls
        '*** This should allow users to build user controls easier
        Dim rptctrl As ReportControl
        Dim rpt As Report = New Report
        '     rpt = FindReportByID(lstReports.SelectedValue)
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
        Dim rpt As Report = New Report
        'rpt = FindReportByID(lstReports.SelectedValue)

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
  
    Protected Sub grdMain_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdMain.RowEditing
        grdMain.EditIndex = e.NewEditIndex
        grdMain.DataBind()
        Session("SelecetdRowIndex") = e.NewEditIndex
        grdMain.EditIndex = e.NewEditIndex
    End Sub

    Sub CreateTemplatedGridView()

        '  // fill the table which is to bound to the GridView
        Dim table As DataTable = GridDataSet.Tables(0) 'New DataTable


        For i = 0 To table.Columns.Count - 1
            Dim ItemTmpField As TemplateField = New TemplateField()
            '  // create HeaderTemplate
            ItemTmpField.HeaderTemplate = New DynamicallyTemplatedGridViewHandler(ListItemType.Header, _
                                                          table.Columns(i).ColumnName, _
                                                          table.Columns(i).DataType.Name)
            '    // create ItemTemplate
            ItemTmpField.ItemTemplate = New DynamicallyTemplatedGridViewHandler(ListItemType.Item, _
                                                          table.Columns(i).ColumnName, _
                                                          table.Columns(i).DataType.Name)
            '  //create EditItemTemplate
            ItemTmpField.EditItemTemplate = New DynamicallyTemplatedGridViewHandler(ListItemType.EditItem, _
                                                          table.Columns(i).ColumnName, _
                                                          table.Columns(i).DataType.Name)
            '  // then add to the GridView
            grdMain.Columns.Add(ItemTmpField)
        Next

        '// bind and display the data
        grdMain.DataSource = table
        grdMain.DataBind()
    End Sub

    Protected Sub grdMain_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdMain.RowCancelingEdit
        grdMain.EditIndex = -1
        grdMain.DataBind()
        Session("SelecetdRowIndex") = -1
    End Sub

    Protected Sub grdMain_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdMain.RowUpdating
        grdMain.EditIndex = -1
        grdMain.DataBind()
        Session("SelecetdRowIndex") = -1
    End Sub
End Class
