Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Public Class clsSelectData
    Dim mdsUnits As DataSet = New DataSet
    Dim mdsUnitProfiles As DataSet = New DataSet
    Dim mdsQuotes As DataSet = New DataSet
    Dim mdsFloors As DataSet = New DataSet
    Dim mdsVendors As DataSet = New DataSet
    Dim mdsUnitTypes As DataSet = New DataSet
    Dim mdsEventLogs As DataSet = New DataSet
    Dim mdsLookups As DataSet = New DataSet
    Dim mdsLogins As DataSet = New DataSet
    Dim mdsDepositConditions As DataSet = New DataSet
    Dim mdsCustomers As DataSet = New DataSet
    Dim mdsCompanyInfo As DataSet = New DataSet
    Public mdsReportCategories As DataSet
    Dim mdsReportControls As DataSet
    Public mdsAllControls As DataSet
    Dim mdsListItems As DataSet
    Dim mdsChildren As DataSet
    Dim cns As Data.SqlClient.SqlConnection
    Dim cno As Data.OleDb.OleDbConnection
    Dim mscnStr As String
    Dim mscnType As String
    Dim mlCategoryID As Long
    Dim NL As String = Chr(13) & Chr(10)
    Public Sub New()
        mscnType = "OLEDB"
        mscnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\data\mkolassa\Visual Studio\Projects\WebSites\Select\App_Data\legadata.mdb;User Id=Master;Password=Cubs1;Jet OLEDB:System Database=C:\data\mkolassa\Visual Studio\Projects\WebSites\Select\App_Data\Parallax.mdw;"
        mscnStr = System.Configuration.ConfigurationManager.ConnectionStrings.Item("ReportManager").ToString
    End Sub

    '****************************************************
    '*** Units
    Public Function LoadUnits(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal llID As Long) As DataSet
        Dim lsSQL As String
        llNodeID = 1
        '*** Initialize
        LoadUnits = Nothing
        If lsWhere = Nothing Then lsWhere = ""
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "Select UnitID from tblUnits"
        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Units")

        mdsUnits = ds
        LoadUnits = mdsUnits
    End Function
    Public Function DeleteUnits(ByVal UnitID As Long) As Boolean
        Dim lsSQL As String = "Update tblUnits Set Active=False  WHERE  UnitID=" & UnitID
        DeleteUnits = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function InsertUnits(ByVal llNodeID As Long, ByVal lsUnitName As String, ByVal lsFloorID As String, _
                                ByVal lsUnitTypeID As String, ByVal lsAvailable As String, _
                                ByVal lsTier As String, ByVal lsDepositType As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertUnits = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "INSERT INTO tblUnits ( UnitName, FloorID, UnitTypeID, Available, TierID, DepositTypeID, " & _
                "                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " & _
                "Values ( """ & fTakeOutQuotes(lsUnitName) & """, " & NL & _
                           lsFloorID & ", " & NL & _
                           lsUnitTypeID & ", " & NL & _
                           lsAvailable & ", " & NL & _
                           lsTier & ", " & NL & _
                           lsDepositType & ", " & NL & _
                           """" & Now.ToString & """, " & NL & _
                           """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL & _
                           """" & Now.ToString & """, " & NL & _
                           """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL & _
                           "true, " & llNodeID & "); "
        '*** Run The SQL.
        InsertUnits = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function UpdateU(ByVal llNodeID As Long, ByVal UnitID As Long) As Boolean
        UpdateU = True
    End Function
    Public Function UpdateUnits(ByVal llNodeID As Long, ByVal lsUnitName As String, ByVal lsFloorID As String, _
                              ByVal lsUnitTypeID As String, ByVal lsAvailable As String, _
                              ByVal lsTier As String, ByVal lsDepositType As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        UpdateUnits = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "INSERT INTO tblUnits ( UnitName, FloorID, UnitTypeID, Available, TierID, DepositTypeID, " & _
                "                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " & _
                "Values ( """ & fTakeOutQuotes(lsUnitName) & """, " & NL & _
                           lsFloorID & ", " & NL & _
                           lsUnitTypeID & ", " & NL & _
                           lsAvailable & ", " & NL & _
                           lsTier & ", " & NL & _
                           lsDepositType & ", " & NL & _
                           """" & Now.ToString & """, " & NL & _
                           """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL & _
                           """" & Now.ToString & """, " & NL & _
                           """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL & _
                           "true, " & llNodeID & "); "
        '*** Run The SQL.
        UpdateUnits = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function

    Private Function fGetDataset(ByVal lsConnectionType As String, ByVal lsCn As String, ByVal lsSQL As String, ByVal lsTableName As String) As DataSet
        Dim ds As DataSet = New DataSet
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
    Private Function fRunSQL(ByVal lsConnectionType As String, ByVal lsCn As String, ByVal lsSQL As String) As Boolean

        Select Case lsConnectionType
            Case "SQLConnection"
                cns = New SqlClient.SqlConnection
                Dim cms As New SqlClient.SqlCommand
                cns.ConnectionString = lsCn
                cns.Open()
                cms.CommandText = lsSQL
                cms.ExecuteNonQuery()
                cns.Close()
            Case "OLEDB"
                cno = New OleDb.OleDbConnection
                cno.ConnectionString = lsCn
                cno.Open()
                ' CheckdbConnection("OLEDB", lsCn)
                '*** Set up a data set command object.

                Dim cmo As New OleDb.OleDbCommand
                cmo.Connection = cno
                cmo.CommandText = lsSQL
                cmo.ExecuteNonQuery()
                cno.Close()
        End Select
        fRunSQL = True
    End Function
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Function fTakeOutQuotes(ByVal lsStr As String) As String
        lsStr = Replace(lsStr, "script", "scri_pt")
        fTakeOutQuotes = Replace(lsStr, """", """""")
    End Function
    Function fGetUser() As String
        fGetUser = "dd"
    End Function
End Class
