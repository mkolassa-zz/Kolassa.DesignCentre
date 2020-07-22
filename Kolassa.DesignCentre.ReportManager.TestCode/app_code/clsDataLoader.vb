Imports System.Data
Imports System.Data.SqlClient

Imports System.IO
Imports Microsoft.VisualBasic
Public Class clsDataLoader
    Dim mdsReports As DataSet = New DataSet
    Public mdsReportCategories As DataSet
    Dim mdsReportControls As DataSet
    Public mdsAllControls As DataSet
    Dim mdsListItems As DataSet
    Dim mdsChildren As DataSet
    Dim cns As SqlConnection
    Dim cno As OleDb.OleDbConnection
    Dim mscnStr As String
    Dim mscnType As String
    Dim mlCategoryID As Long
    Dim NL As String = Chr(13) & Chr(10)
    Public Sub New()
        mscnType = "SQLConnection" '"OLEDB"
        mscnStr = System.Configuration.ConfigurationManager.ConnectionStrings.Item("ReportManager").ToString

        ' mscnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\data\parallax\pinnacle\pinnacle data.mdb;User Id=admin;Password=;"
        'LoadReports(0)
    End Sub
    Public Function LoadReports(ByVal llCategoryID As Long) As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadReports = Nothing

        '*** Check for No Selected Category
        If llCategoryID = 0 Then
            ' Debug("No Category Selectedd")
            Exit Function
        End If

        lsSQL = "SELECT tblReportDescriptions.ReportID,          " & NL &
                "       tblReportDescriptions.ReportDescription, " & NL &
                "       tblReportDescriptions.ReportName         " & NL &
                "FROM   tblReportCategoryMap INNER JOIN          " & NL &
                "       tblReportDescriptions ON                 " & NL &
                "       tblReportCategoryMap.ReportID = tblReportDescriptions.ReportID " & NL &
                "WHERE  tblReportCategoryMap.CategoryID=" & llCategoryID & NL &
                " order by tblReportDescriptions.ReportName ;"

        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Reports")

        '*** Debugging Start
        Dim dt As DataTable = ds.Tables.Item("Reports")
        Dim rowCustomer As DataRow

        For Each rowCustomer In dt.Rows
            Console.WriteLine(rowCustomer.Item("ReportDescription"))
        Next
        '*** Debugging End

        mdsReports = ds
        LoadReports = mdsReports
    End Function
    Public Function LoadReportCategories() As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadReportCategories = Nothing

        lsSQL = "Select * from tblReportCategories"

        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Categories")
        '*** Debugging Start
        Dim dt As DataTable = ds.Tables.Item("Categories")
        Dim rowCustomer As DataRow
        ' Debug.WriteLine("Begin Debug Categories")
        For Each rowCustomer In dt.Rows
            ' Debug.WriteLine(rowCustomer.Item("ReportCategoryDescription"))
        Next
        mdsReportCategories = ds
        LoadReportCategories = mdsReportCategories
    End Function
    Public Function LoadAllControls() As DataSet
        Dim NL As String = Chr(13) & Chr(10)
        Dim lsSQL As String

        '*** Initialize
        LoadAllControls = Nothing

        lsSQL = "Select * from tblReportControls"

        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Controls")

        mdsAllControls = ds
        LoadAllControls = mdsAllControls
    End Function
    Public Function LoadReportControls(ByVal llReportID As Long) As DataSet
        Dim lscnType As String = "OLEDB"
        Dim lsSQL As String

        '*** Initialize
        LoadReportControls = Nothing

        lsSQL = "SELECT tblReportControls.*, " &
                "       tblReportFields.* " &
                "From   tblReportFields inner join tblReportControls on " &
                "       tblreportfields.reportcontrol = tblreportcontrols.controlid " &
                "WHERE  ReportID= " & llReportID

        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "ReportControls")

        mdsReportControls = ds
        LoadReportControls = mdsReportControls
    End Function
    Public Function LoadListItems(ByVal lsCnStr As String, ByVal lsSQL As String) As DataSet
        Dim lscnType As String = "OLEDB"
        On Error GoTo LoadListItemsError

        LoadListItems = New DataSet
        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(lscnType, lsCnStr, lsSQL, "ListItems")

        mdsListItems = ds
        LoadListItems = mdsListItems

LoadListItemsExit:
        Exit Function
LoadListItemsError:
        MsgBox(Err.Description)
        Resume LoadListItemsExit
    End Function

    Public Function LoadChildren(ByVal lsCnStr As String, ByVal lsSQL As String) As DataSet
        Dim lscnType As String = ""

        On Error GoTo LoadChildrenError
        LoadChildren = New DataSet

        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(lscnType, lsCnStr, lsSQL, "Children")

        mdsChildren = ds
        LoadChildren = mdsChildren

LoadChildrenExit:
        Exit Function
LoadChildrenError:
        MsgBox(Err.Description)
        Resume LoadChildrenExit
    End Function
    '  Private Function CheckdbConnection(ByVal lsConnectionType, ByVal lsConnectionString) As Boolean
    '      If cn Is Nothing Then
    '          cn = New Data.SqlClient.SqlConnection
    '      End If
    '      If cn.State = ConnectionState.Closed Then
    '  Dim lscn As String
    '          lscn = "DRIVER={Microsoft Access Driver (*.mdb)};DBQ=c:\data\parallax\pinnacle\pinnacle data.mdb"
    '          lscn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\data\parallax\pinnacle\pinnacle data.mdb;User Id=admin;Password=;"
    '          cn.ConnectionString = lscn
    '  '"Server=azshdsd17;user id=scored;password=scored;Database=Quality;Pooling=false;"
    '          cn.Open()
    '      End If
    '      If cn.State <> ConnectionState.Open Then
    '          CheckdbConnection = False
    '      Else
    '         CheckdbConnection = True
    '     End If
    ' End Function
    Private Function fGetDataset(ByVal lsConnectionType As String, ByVal lsCn As String, ByVal lsSQL As String, ByVal lsTableName As String) As DataSet

        Dim ds As DataSet = New DataSet
        If lsCn = "" Then
            lsCn = mscnStr
            lsConnectionType = mscnType
        End If
        If lsConnectionType = "" Then
            lsConnectionType = mscnType
            lsCn = mscnStr
        End If

        Select Case lsConnectionType
            Case "SQLConnection"
                cns = New SqlClient.SqlConnection
                cns.ConnectionString = lsCn
                cns.Open()
                ' CheckdbConnection("SQLServer", lsCn)
                '*** Set up a data set command object.
                Dim dscmd As New SqlDataAdapter(lsSQL, cns)

                '*** Load a data set.
                dscmd.Fill(ds, lsTableName)
                cns.Close()
            Case "OLEDB"
                cno = New OleDb.OleDbConnection
                cno.ConnectionString = lsCn
                cno.Open()
                ' CheckdbConnection("OLEDB", lsCn)
                '*** Set up a data set command object.
                Dim dscmdO As New OleDb.OleDbDataAdapter(lsSQL, cno)

                '*** Load a data set.
                dscmdO.Fill(ds, lsTableName)
                cno.Close()
        End Select

        fGetDataset = ds
    End Function
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

End Class

