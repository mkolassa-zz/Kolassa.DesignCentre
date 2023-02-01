Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports Microsoft.AspNet.identity
Imports System.IO
Imports Microsoft.VisualBasic
Public Class clsDataLoader
    Dim mdsReports As DataSet = New DataSet
    Public mdsReportCategories As DataSet
    Dim mdsReportControls As DataSet
    Public mdsAllControls As DataSet
    Dim mdsListItems As DataSet
    Public msErrorMsg As String
    Public ReadOnly Property ErrorMessage As String = msErrorMsg
    Dim mdsChildren As DataSet
    Dim cns As SqlConnection
    Dim cno As OleDb.OleDbConnection
    Dim msSQLParameter As SqlParameter
    Public mscnStr As String
    Dim mscnType As String
	Dim mlCategoryID As Long
	Public NodeID As Long
	Public ProjectID As String = "11112222-3333-4444-5555-666677778888"
	Dim NL As String = Chr(13) & Chr(10)
    Public Sub New()
        Try
            Dim s As String = ""
            If System.Web.HttpContext.Current.Session("Project") Is Nothing Then
            Else
                s = System.Web.HttpContext.Current.Session("Project")
            End If
            If s Is Nothing Then
            Else
                If s.Length = 36 Then
                    ProjectID = System.Web.HttpContext.Current.Session("Project")
                End If
                If System.Web.HttpContext.Current.Session("NodeID").ToString.Length > 0 Then
                    NodeID = System.Web.HttpContext.Current.Session("NodeID")
                End If
            End If
        Catch
        End Try
        'NodeID = System.Web.HttpContext.Current.Session("NodeID")
        ' ProjectID = System.Web.HttpContext.Current.Session("Project")
        '      If Not IsNothing(ProjectID) Then Stop
        mscnType = "SQLConnection" '"OLEDB"
        mscnStr = System.Configuration.ConfigurationManager.ConnectionStrings.Item("ReportManager").ToString
        '	response.write(mscnStr)
        ' mscnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\data\parallax\pinnacle\pinnacle data.mdb;User Id=admin;Password=;"
        'LoadReports(0)
    End Sub



    Public Function LoadSQL(ByVal SQL As String) As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadSQL = Nothing

        lsSQL = SQL

        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Reports")

        '*** Debugging Start
        Dim dt As DataTable = ds.Tables.Item("Reports")
        'Dim rowCustomer As DataRow


        '*** Debugging End

        mdsReports = ds
        LoadSQL = mdsReports
    End Function
    Public Function LoadReport(ByVal liReportID As Integer, Optional ByVal ReportName As String = "") As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadReport = Nothing

        '*** Check for No Selected Category
        If liReportID = 0 And ReportName = "" Then
            ' Debug("No Category Selectedd")
            msErrorMsg = "No Report Specified"
            Exit Function
        End If


        lsSQL = "SELECT tblReportDescriptions.ReportID,          " & NL &
                "       tblReportDescriptions.ReportDescription, " & NL &
                "       tblReportDescriptions.ReportName,   s.SelectStatement ,  SearchClause,    " & NL &
                "       tblReportDescriptions.EntityType, tblReportDescriptions.TableName         " & NL &
                "FROM   tblReportDescriptions           " & NL &
                  "       Left Join tblReportSQL S on tblReportDescriptions.ReportID = S.ReportID " & NL &
                "WHERE  1=1 "
        If liReportID > 0 Then
            lsSQL = lsSQL & "   And   tblReportDescriptions.ReportID=" & liReportID & NL
        Else
            lsSQL = lsSQL & "   And   tblReportDescriptions.ReportName='" & ReportName & "'" & NL
        End If

        lsSQL &= " order by tblReportDescriptions.ReportName ;"

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
        LoadReport = mdsReports
    End Function
    Public Function LoadReports(ByVal llCategoryID As Long, ReportName As String, llReportID As Long) As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadReports = Nothing

        '*** Check for No Selected Category
        If llCategoryID = 0 And ReportName = "" And llReportID = 0 Then
            ' Debug("No Category Selectedd")
            Exit Function
        End If
        If llReportID > 0 Then
            lsSQL = "Select tblReportDescriptions.ReportID,          " & NL &
                "       tblReportDescriptions.ReportDescription, " & NL &
                "       tblReportDescriptions.ReportName,         " & NL &
                "       tblReportDescriptions.EntityType , tblReportDescriptions.TableName ,   s.SearchClause       " & NL &
                "FROM   tblReportDescriptions Left Join tblReportSQL s On tblReportDescriptions.ReportID = s.ReportID " & NL &
                "WHERE  tblReportDescriptions.ReportID=" & llReportID & " " & NL &
                " order by tblReportDescriptions.ReportName ;"
        Else
            If ReportName = "" Or ReportName Is Nothing Then
                lsSQL = "Select tblReportDescriptions.ReportID,          " & NL &
                    "       tblReportDescriptions.ReportDescription, " & NL &
                    "       tblReportDescriptions.ReportName,   s.SelectStatement ,     " & NL &
                    "       tblReportDescriptions.EntityType, tblReportDescriptions.TableName  , s.SearchClause       " & NL &
                    "FROM   tblReportCategoryMap INNER JOIN          " & NL &
                    "       tblReportDescriptions On                 " & NL &
                    "       tblReportCategoryMap.ReportID = tblReportDescriptions.ReportID " & NL &
                    "       Left Join tblReportSQL S On tblReportDescriptions.ReportID = S.ReportID " & NL &
                    "WHERE  tblReportCategoryMap.CategoryID=" & llCategoryID & NL &
                    " order by tblReportDescriptions.ReportName ;"
            Else
                If ReportName = "ALLREPORTS" Then

                    lsSQL = "Select tblReportDescriptions.ReportID,          " & NL &
                    "       tblReportDescriptions.ReportDescription, " & NL &
                    "       tblReportDescriptions.ReportName,         " & NL &
                    "       tblReportDescriptions.EntityType , tblReportDescriptions.TableName           " & NL &
                    "FROM   tblReportDescriptions  " & NL &
                    "WHERE  1=1 " & NL &
                    " order by tblReportDescriptions.ReportName ;"
                Else
                    lsSQL = "Select tblReportDescriptions.ReportID,          " & NL &
                    "       tblReportDescriptions.ReportDescription, " & NL &
                    "       tblReportDescriptions.ReportName,         " & NL &
                    "       tblReportDescriptions.EntityType , tblReportDescriptions.TableName           " & NL &
                    "FROM   tblReportDescriptions  " & NL &
                    "WHERE  upper(ReportName)='" & ReportName.ToUpper & "'" & NL &
                    " order by tblReportDescriptions.ReportName ;"
                End If
            End If
        End If
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
    Public Function LoadReportResults(ByVal lsSQL As String, lsWhere As String, liReportID As Integer, Optional SearchClause As String = "") As DataSet
        Dim ds As New DataSet()
        Dim lsSearchClause As String = ""
        If SearchClause Is Nothing Then
            SearchClause = ""
        End If
        If SearchClause.Length > 3 Then lsSearchClause = SearchClause
        '*** Initialize
        LoadReportResults = Nothing

        '*** Check for Empty SQL
        If lsSQL = "" Then
            If liReportID < 1 Then
                ' Debug("No Category Selectedd")
                Exit Function
            Else
                ds = LoadReport(liReportID)
                If ds.Tables.Count > 0 Then
                    Dim dt As DataTable
                    Dim dr As DataRow

                    dt = ds.Tables(0)
                    dr = dt.Rows(0)
                    lsSQL = dr("SelectStatement")
                    lsSearchClause = dr("SearchClause")

                End If
            End If
        End If
        If lsSearchClause = "" Then lsSearchClause = " isnull([code],' ' ) + isnull( [name],' ') + isnull( [description],' ') "
        lsSQL = Replace(lsSQL, "@WHERE@", lsWhere)
        If ProjectID.Length = 36 Then
            lsSQL = Replace(lsSQL, "@OBJECTID@", "  ObjectID = '" & ProjectID & "' AND ")
            lsSQL = Replace(lsSQL, "@PROJECTID@", "  ProjectID = '" & ProjectID & "' AND ")
        Else
            lsSQL = Replace(lsSQL, "@OBJECTID@", "")
            lsSQL = Replace(lsSQL, "@PROJECTID@", "")
        End If
        lsSQL = Replace(lsSQL, "SearchText", " " + lsSearchClause + " ")
        If Right(lsSQL, 4).Trim.ToUpper = "AND" Then lsSQL = Left(lsSQL, lsSQL.Length - 4)

        '*** Load a data set.

        ds = fGetDataset(mscnType, mscnStr, lsSQL, "ReportResults")

        mdsReports = ds
        LoadReportResults = mdsReports
    End Function
    Public Function LoadReportCategories(lsCat As String) As DataSet
		Dim lsSQL As String

		'*** Initialize
		LoadReportCategories = Nothing

		lsSQL = "Select * from tblReportCategories"
		If lsCat = "" Or lsCat Is Nothing Then
			lsCat = ""
		Else
			lsSQL = lsSQL & " Where ReportCategoryDescription='" & lsCat & "'"
		End If

		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "Categories")
		'*** Debugging Start
		Dim dt As DataTable = New DataTable("ReportCategoryType")
		dt.Columns.Add("ReportCategoryName", Type.GetType("System.String"))
		Dim dr As DataRow
		dr = dt.NewRow()
		dr("ReportCategoryName") = lsCat
		dt.Rows.Add(dr)
		ds.Tables.Add(dt)

		mdsReportCategories = ds
		LoadReportCategories = mdsReportCategories
	End Function
    Public Function LoadAllControls() As DataSet
        Dim NL As String = Chr(13) & Chr(10)
        Dim lsSQL As String

        '*** Initialize
        LoadAllControls = Nothing

        lsSQL = "Select * from tblReportControls "


        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Controls")

        mdsAllControls = ds
        LoadAllControls = mdsAllControls
    End Function
    Public Function LoadAvailableControlsForForm(liReportID As Integer, liControlID As Integer) As DataSet
        '*** Since each control has unique name,  eliminate the controls already on the form
        '*** Might switch this later if I can get the ControlName attribute on the ReportFIeld Table to Drive the control Name
        Dim NL As String = Chr(13) & Chr(10)
        Dim lsSQL As String

        '*** Initialize
        LoadAvailableControlsForForm = Nothing

        lsSQL = "SELECT * FROM tblreportcontrols c left join
                (Select ReportControl from tblreportfields where reportid=" & liReportID & ") a
                    On c.controlid = a.ReportControl
                WHERE a.reportcontrol is null or c.controlID = " & liControlID


        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Controls")

        mdsAllControls = ds
        LoadAvailableControlsForForm = mdsAllControls
    End Function
    Public Function LoadReportControls(ByVal llReportID As Long, Optional llControlNum As Long = 0, Optional lsFieldID As String = "") As DataSet
        Dim lscnType As String = "OLEDB"
        Dim lsSQL As String

        '*** Initialize
        LoadReportControls = Nothing

        lsSQL = "SELECT c.*
                       , c.ID as ReportControlID
                       , f.* 
                       , f.ID as ReportControlFieldID , f.Name as ReportControlFieldsName " &
                "From   tblReportFields f inner join tblReportControls c on " &
                "       f.reportcontrol = c.controlid " &
                "WHERE  1=1  " &
                        IIf(llReportID > 0, " AND ReportID= " & llReportID & " ", " ") &
                        IIf(llControlNum > 0, " AND ReportControl =" & llControlNum & " ", "") &
                        IIf(lsFieldID = "", "", " AND f.ID ='" & lsFieldID & "' ") &
                "ORDER BY SortOrder "

        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "ReportControls")

        mdsReportControls = ds
        LoadReportControls = mdsReportControls
    End Function


    Public Function LoadReportViews(lsReportID As String) As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadReportViews = Nothing

        lsSQL = "Select * from tblReportViews"
        If lsReportID = "" Or lsReportID Is Nothing Then
            lsReportID = ""
        Else
            lsSQL = lsSQL & " Where (Type = 'Global' or createuser ='" & fGetUser() & "') and (isnull(NodeID,0) = 0 or NodeID = " & NodeID & ") AND ReportID='" & lsReportID & "'"
        End If

        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Views")
        '*** Debugging Start


        mdsReportCategories = ds
        LoadReportViews = mdsReportCategories
    End Function
    Public Function LoadAllReportColumns() As DataSet
        Dim NL As String = Chr(13) & Chr(10)
        Dim lsSQL As String

        '*** Initialize
        LoadAllReportColumns = Nothing

        lsSQL = "Select * from TBLREPORTVIEWCOLUMNS "


        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Controls")

        mdsAllControls = ds
        LoadAllReportColumns = mdsAllControls
    End Function
    Public Function LoadReportViewColumns(ByVal lsViewID As String) As DataSet
        Dim lscnType As String = "OLEDB"
        Dim lsSQL As String

        '*** Initialize
        LoadReportViewColumns = Nothing

        lsSQL = "SELECT v.name as viewname, v.type as viewtype, c.* " &
                "FROM   TBLREPORTVIEWCOLUMNS c INNER JOIN TBLREPORTVIEWS v on v.ID = c.ViewID " &
                "WHERE  c.ViewID= '" & lsViewID & "' " &
                "ORDER BY columnvisible desc, ColumnOrder "

        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "ViewColumns")

        'mdsReportControls = ds
        LoadReportViewColumns = ds
    End Function

    Public Function DeleteReportView(ByVal lsViewID As String) As Boolean
        Dim lscnType As String = "OLEDB"
        Dim lsSQL As String

        '*** Initialize
        DeleteReportView = Nothing

        '*** First Delete the Columns
        DeleteReportViewColumns(lsViewID)

        '*** Now Delete the Views
        lsSQL = "Delete " &
                "FROM   TBLREPORTVIEWS " &
                "WHERE  ID= '" & lsViewID & "' "


        '*** Load a data set.
        Dim ds As New DataSet()
        Dim cmd As New SqlCommand
        cmd.CommandText = lsSQL
        Dim c As New Collection
        c.Add(cmd)

        Dim b As Boolean = fRunSQLCommands(mscnType, mscnStr, c)

        DeleteReportView = True
    End Function
    Public Function DeleteReportViewColumns(ByVal lsViewID As String) As Boolean
        Dim lscnType As String = "OLEDB"
        Dim lsSQL As String

        '*** Initialize
        DeleteReportViewColumns = Nothing

        lsSQL = "Delete " &
                "FROM   TBLREPORTVIEWCOLUMNS " &
                "WHERE  ViewID= '" & lsViewID & "' "


        '*** Load a data set.
        Dim ds As New DataSet()
        Dim cmd As New SqlCommand
        cmd.CommandText = lsSQL
        Dim c As New Collection
        c.Add(cmd)

        Dim b As Boolean = fRunSQLCommands(mscnType, mscnStr, c)

        DeleteReportViewColumns = True
    End Function
    Public Function InsertReportViewColumns(ByVal lsViewID As String, ByVal lsID As String,
                                    ByVal lsFieldName As String, ByVal lsColumnName As String, lsColumnFormat As String,
                                    ByVal liColumnOrder As Integer, ByVal lbVIsible As Boolean) As Boolean
        Dim lgID As New Guid
        Dim msSQLcmd As SqlCommand
        lgID = Guid.NewGuid
        Dim lsSQL As String
        Dim lscnStr As String = mscnStr
        '  Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertReportViewColumns = False
        '*** Check for No Selected Category
        'If llNodeID = 0 Then
        ' Exit Function
        ' End If

        If isGUID(lsID) Then
        Else
            lsID = Guid.NewGuid.ToString
        End If
        lsSQL = "INSERT INTO tblReportViewColumns (ID, VIEWID, FIELDNAME, COLUMNNAME, COLUMNORDER, COLUMNFORMAT, COLUMNVISIBLE ) " &
                "Values ( @idGuid, @viewid, @fieldname, @columnname, @columnorder, @columnformat, @columnvisible);"
        msSQLcmd = New SqlCommand
        msSQLcmd.CommandText = lsSQL
        msSQLcmd.Parameters.Clear()
        msSQLcmd.Parameters.AddWithValue("@idGuid", New Guid(lgID.ToString))
        msSQLcmd.Parameters.AddWithValue("@viewid", New Guid(lsViewID))
        msSQLcmd.Parameters.AddWithValue("@fieldname", (lsFieldName))
        msSQLcmd.Parameters.AddWithValue("@columnname", fTakeOutQuotes(lsColumnName))
        msSQLcmd.Parameters.AddWithValue("@columnorder", liColumnOrder)
        msSQLcmd.Parameters.AddWithValue("@columnvisible", lbVIsible)
        msSQLcmd.Parameters.AddWithValue("@columnformat", fTakeOutQuotes(lsColumnFormat))

        '*** Run The SQL.
        InsertReportViewColumns = fExecuteSQLCmd("SQLConnection", lscnStr, msSQLcmd)
    End Function
    Function fGetUser() As String
        fGetUser = "11111111-2222-3333-4444-555566667777" '
        fGetUser = Web.HttpContext.Current.User.Identity.GetUserId()
    End Function
    Public Function InsertReportView(ByVal lsViewID As String, ByVal lsName As String,
                                    ByVal lsTYpe As String, ByVal lsForm As String, liReportID As Integer,
                                    ByVal liNodeID As Integer) As Boolean
        Dim lgID As New Guid
        Dim lsUserID As String = fGetUser()
        Dim msSQLcmd As SqlCommand
        lgID = Guid.NewGuid
        Dim lsSQL As String
        Dim lscnStr As String = mscnStr
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertReportView = False
        '*** Check for No Selected Category
        'If llNodeID = 0 Then
        ' Exit Function
        ' End If

        lsSQL = "INSERT INTO tblReportViews (ID, CODE, Name, TYPE, FORM, REPORTID, NODEID, ACTIVE ,CREATEUSER, CREATEDATE) " &
                "Values ( @idGuid,@Code, @Name, @type, @form, @reportid, @nodeid, 1,@CurrentUser, getdate());"
        msSQLcmd = New SqlCommand
        msSQLcmd.CommandText = lsSQL
        msSQLcmd.Parameters.Clear()
        msSQLcmd.Parameters.AddWithValue("@idGuid", (lsViewID))
        msSQLcmd.Parameters.AddWithValue("@Name", (lsName))
        msSQLcmd.Parameters.AddWithValue("@Code", (lsName))
        msSQLcmd.Parameters.AddWithValue("@type", (lsTYpe))
        msSQLcmd.Parameters.AddWithValue("@form", fTakeOutQuotes(lsForm))
        msSQLcmd.Parameters.AddWithValue("@reportid", liReportID)
        msSQLcmd.Parameters.AddWithValue("@nodeid", liNodeID)
        msSQLcmd.Parameters.AddWithValue("@CurrentUser", lsCurrentUser)


        '*** Run The SQL.
        InsertReportView = fExecuteSQLCmd("SQLConnection", lscnStr, msSQLcmd)
    End Function
    Public Function InsertReportField(liReportID As Integer) As Boolean
        Dim lgID As New Guid
        Dim lsUserID As String = fGetUser()
        Dim msSQLcmd As SqlCommand
        lgID = Guid.NewGuid
        Dim lsSQL As String
        Dim lscnStr As String = mscnStr
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertReportField = False
        '*** Check for No Selected Category
        'If llNodeID = 0 Then
        ' Exit Function
        ' End If

        lsSQL = "  INSERT INTO [tblReportFields]
           ([ReportID]           ,[ReportControl]           ,[Fieldtype]           ,[Required]           ,[Active]           ,[NodeID]
           ,[CreateDate]           ,[Createuser]           ,[UpdateDate]           ,[UpdateUser]           ,[SortOrder]           ,[Validation]
           ,[FieldValidationPattern]           ,[FieldValidationTitle]           ,[TableName]           ,[FieldName]           ,[FieldReadOnly]
             ,[ID]           ,[Name])
     VALUES
          (@ReportID           ,0           , 'String'           , 0           , 1           ,0
           ,getdate()           ,@UserID            ,getdate()           ,@UserID           ,0           ,''
           ,''           ,''           ,''           ,''           ,0           ,newid()           ,'NF" & Right("000000" & Int((100000 * Rnd())), 6) & "') "



        msSQLcmd = New SqlCommand
        msSQLcmd.CommandText = lsSQL
        msSQLcmd.Parameters.Clear()
        msSQLcmd.Parameters.AddWithValue("@ReportID", liReportID)
        msSQLcmd.Parameters.AddWithValue("@UserID", lsCurrentUser)

        '*** Run The SQL.
        InsertReportField = fExecuteSQLCmd("SQLConnection", lscnStr, msSQLcmd)
    End Function
    Public Function UpdateReportViewScope(ByVal lsViewID As String, ByVal lsViewType As String, ByVal liNodeID As Integer) As Boolean
        Dim lgID As New Guid
        Dim lsUserID As String = fGetUser()
        Dim msSQLcmd As SqlCommand
        lgID = Guid.NewGuid
        Dim lsSQL As String
        Dim lscnStr As String = mscnStr
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        UpdateReportViewScope = False
        '*** Check for No Selected Category
        'If llNodeID = 0 Then
        ' Exit Function
        ' End If

        lsSQL = "UPDATE tblReportViews 
                 SET TYPE = '" & lsViewType & "',  
                     UPDATEDATE = getdate()
                 WHERE ID = '" & lsViewID & "' AND NODEID = " & liNodeID & ";"
        msSQLcmd = New SqlCommand
        msSQLcmd.CommandText = lsSQL

        '*** Run The SQL.
        UpdateReportViewScope = fExecuteSQLCmd("SQLConnection", lscnStr, msSQLcmd)
    End Function
    Public Function isGUID(ByVal QFormUID As String) As Boolean
        If QFormUID Is Nothing Then
            QFormUID = "00" ' "00000000-0000-0000-0000-000000000000"
        End If
        Dim guidRegEx As Regex = New Regex("^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$")
        If guidRegEx.IsMatch(QFormUID) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function fExecuteSQLCmd(ByVal lsConnectionType As String, ByVal lsCn As String, ByVal cms As SqlCommand) As Boolean

        Select Case lsConnectionType
            Case "SQLConnection"
                cns = New SqlClient.SqlConnection

                cns.ConnectionString = lsCn
                cns.Open()
                cms.Connection = cns

                On Error GoTo fexecuteSQLError
                cms.ExecuteNonQuery()
                msSQLParameter = Nothing
                cns.Close()
            Case "OLEDB"
                cno = New OleDb.OleDbConnection
                cno.ConnectionString = lsCn
                cno.Open()
                ' CheckdbConnection("OLEDB", lsCn)
                '*** Set up a data set command object.

                Dim cmo As New OleDb.OleDbCommand
                cmo.Connection = cno
                cmo.CommandText = "" 'lsSQL
                cmo.ExecuteNonQuery()
                cno.Close()
        End Select
        fExecuteSQLCmd = True
        msErrorMsg = ""
        Return True
fexecuteSQLError:
        msErrorMsg = Err.Description
        Debug.Print("<error>" & msErrorMsg & "</error>")
    End Function
    Function fTakeOutQuotes(ByVal lsStr As String) As String
        lsStr = Replace(lsStr, "script", "scri_pt")
        lsStr = Replace(lsStr, "descri_ption", "Description")
        lsStr = Replace(lsStr, """", """""")
        lsStr = Replace(lsStr, "'", "''")
        fTakeOutQuotes = Trim(lsStr)
    End Function
    Public Function fRunSQLCommands(ByVal lsConnectionType As String, ByVal lsCn As String, ByVal cCommands As Collection) As Boolean
        Try
            Select Case lsConnectionType
                Case "SQLConnection"
                    Dim trans As SqlClient.SqlTransaction
                    cns = New SqlClient.SqlConnection
                    Dim cms As New SqlClient.SqlCommand
                    cns.ConnectionString = lsCn
                    cns.Open()

                    trans = cns.BeginTransaction("SampleTransaction")
                    Try
                        For Each cms In cCommands
                            cms.Connection = cns
                            cms.Transaction = trans
                            cms.ExecuteNonQuery()
                        Next
                        trans.Commit()
                    Catch ex As Exception
                        msErrorMsg = ex.Message
                        trans.Rollback()
                        fRunSQLCommands = False
                        Exit Function
                    End Try
                    cns.Close()
                Case "OLEDB"
                    cno = New OleDb.OleDbConnection
                    cno.ConnectionString = lsCn
                    cno.Open()
                    ' CheckdbConnection("OLEDB", lsCn)
                    '*** Set up a data set command object.


                    Dim cms As New SqlCommand
                    Dim cmo As New OleDb.OleDbCommand
                    cmo.Connection = cno
                    For Each cms In cCommands
                        cmo.CommandText = cms.CommandText
                        cmo.ExecuteNonQuery()
                    Next
                    cno.Close()
            End Select
            fRunSQLCommands = True
        Catch
            msErrorMsg = msErrorMsg & Err.Description
            fRunSQLCommands = False
        End Try
    End Function
    Public Function LoadListItems(ByVal lsCnStr As String, ByVal lsSQL As String) As DataSet
        Dim lscnType As String = "OLEDB"
        On Error GoTo LoadListItemsError

        If ProjectID Is Nothing Then ProjectID = "11112222-3333-4444-5555-666677778888"
        LoadListItems = New DataSet
        '*** Load a data set.
        lsSQL = lsSQL.Replace("@ProjectID@", ProjectID)
        lsSQL = lsSQL.Replace("@NodeID@", NodeID)
        Dim ds As New DataSet()
        ds = fGetDataset(lscnType, lsCnStr, lsSQL, "ListItems")

        mdsListItems = ds
        LoadListItems = mdsListItems

LoadListItemsExit:
        Exit Function
LoadListItemsError:
        Dim t As DataTable
        t = New DataTable
        t.Columns.Add("ID")
        t.Columns.Add("Description")
        Dim r As DataRow
        r = t.NewRow

        r(0) = 0
        r(1) = Err.Description

        t.Rows.Add(r)
        ds.Tables.Add(t)
        'debug.print("<error msg='" & Err.Description & "' />")
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
        'debug.print("<error msg='" & Err.Description & "' />")
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
        'Debug.Print("<clsDataLoader.fGetDataset>" & lsSQL & "</clsDataLoader.fGetDataset>")
        msErrorMsg = ""
        If lsSQL.Length > 4 Then
            If Right(Trim(lsSQL), 4).ToUpper = " AND" Then
                lsSQL = Trim(lsSQL)
                lsSQL = Left(lsSQL, lsSQL.Length - 4)
            End If
        Else
            fGetDataset = New DataSet
            Exit Function
        End If

        Dim ds As DataSet = New DataSet
        If lsCn = "" Then
            lsCn = mscnStr
            lsConnectionType = mscnType
        End If
        If lsConnectionType = "" Then
            lsConnectionType = mscnType
            lsCn = mscnStr
        End If
        ''debug.print(lsSQL)
        Select Case lsConnectionType
            Case "SQLConnection"
                cns = New SqlClient.SqlConnection
                '	response.write(lsCn)
                cns.ConnectionString = lsCn
                cns.Open()
                ' CheckdbConnection("SQLServer", lsCn)
                '*** Set up a data set command object.
                Dim dscmd As New SqlDataAdapter(lsSQL, cns)

                '*** Load a data set.
                Try
                    dscmd.Fill(ds, lsTableName)
                Catch e As Exception
                    msErrorMsg = e.Message
                    ds = New DataSet
                    Dim dt As New DataTable("Empty")
                    Dim dc As New DataColumn("Name", GetType(String))

                    dt.Columns.Add(dc)
                    dt.Rows.Add(fTakeOutQuotes(msErrorMsg))
                    ds.Tables.Add(dt)
                End Try
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
    '*******************************************************
    '*** Report Fields - Field Level Info for the Report Controls
    '*******************************************************
    Public Function LoadReportFields(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As String) As DataSet
        Dim lsSQL As String
        Dim lscnStr As String = mscnStr
        '*** Initialize
        LoadReportFields = Nothing
        'llNodeID = 2
        lsWhere = Replace(lsWhere, "SearchText", " code + name + description ")
        If lsWhere = Nothing Then lsWhere = ""
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'response.write("No Project Selectedd")
            Exit Function
        End If

        lsSQL = "SELECT *                  " & NL &
                "FROM TBLREPORTFIELDS      " & NL &
                "WHERE  ( NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " And " & lsWhere, "") &
                    IIf(lbActive = True, " And Active = 1 ", "") & ")" & NL &
                    IIf(isGUID(lsID), " AND ID = '" & lsID & "' ", "")


        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "Projects")

        LoadReportFields = ds
    End Function
    Public Function DeleteReportField(ByVal lsID As String) As Boolean
        Dim lscnType As String = "OLEDB"
        Dim lsSQL As String

        '*** Initialize
        DeleteReportField = Nothing

        '*** First Delete the Columns
        DeleteReportViewColumns(lsID)

        '*** Now Delete the Views
        lsSQL = "Delete FROM   TBLREPORTFIELDS " &
                              "WHERE  ID= '" & lsID & "' "


        '*** Load a data set.
        Dim ds As New DataSet()
        Dim cmd As New SqlCommand
        cmd.CommandText = lsSQL
        Dim c As New Collection
        c.Add(cmd)

        Dim b As Boolean = fRunSQLCommands(mscnType, mscnStr, c)

        DeleteReportField = True
    End Function
    Public Function fRFFieldExists(nodeID As Long, fieldname As String, ReportID As String, ReportFieldID As String, controlname As String, ByRef errMessage As String) As Boolean
        fRFFieldExists = False
        Dim lsSQL As String
        lsSQL = "SELECT * From
                    (SELECT case when isnull(f.name,'')='' then c.controlname else f.name end as theControlname,
                        case when isnull(f.fieldname,'')='' then c.controlfieldname else f.name end as theFieldname,
	                    f.reportid, f.NodeID , f.id 
                     FROM tblReportControls as c inner join tblReportFields f 
                        on c.ControlID = f.ReportControl ) as A
                 WHERE A.NodeID=" & nodeID & " and A.ReportID=" & ReportID & " and  A.thecontrolname='" & Trim(controlname) & "' and A.ID !='" & ReportFieldID & "'"
        Dim ds As New DataSet()
        ds = fGetDataset("SQLConnection", mscnStr, lsSQL, "Projects")
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                errMessage = "Control Name already exists for this control"
                Return True
            End If
        End If

        '*** Field Needs to be updated
        lsSQL = "SELECT * From
                    (SELECT case when isnull(f.name,'')='' then c.controlname else f.name end as theControlname,
                        case when isnull(f.fieldname,'')='' then c.controlfieldname else f.name end as theFieldname,
	                    f.reportid, f.NodeID , f.id 
                     FROM tblReportControls as c inner join tblReportFields f 
                        on c.ControlID = f.ReportControl ) as A
                 WHERE A.NodeID=" & nodeID & " and A.ReportID=" & ReportID & " and  A.thefieldname='" & Trim(fieldname) & "' and A.ID !='" & ReportFieldID & "'"
        ds = New DataSet()
        ds = fGetDataset("SQLConnection", mscnStr, lsSQL, "Projects")
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                errMessage = "A  Control For this field already exists!"
                Return True
            End If
        End If
        Return False
    End Function
    '*********************************
    '*** Autonumber
    '*********************************

    Public Function fGetNextCode(NodeID As Long, ObjectType As String, TableName As String, ProjectID As String, ErrMsg As String) As String
        fGetNextCode = ""
        Dim lsSQL As String
        Dim lscnStr As String = mscnStr
        Dim llAutonumber As Double = 0
        Dim lsCodePattern As String = Left(ObjectType, 1) & "@"
        Dim lsID = "12341234-4321-5432-6543-653465346534"
        Dim cmd As SqlCommand
        cmd = New SqlCommand
        Dim lbRetVal As Boolean = False
        '*** Initialize

        If NodeID = 0 Then
            'response.write("No Project Selectedd")
            Exit Function
        End If
        If ProjectID Is Nothing Then ProjectID = "00000000-0000-0000-0000-000000000000"

        lsSQL = "Select * from tblAutonumber
                 WHERE  NodeID = " & NodeID & " 
                    And Code = '" & ObjectType & "' 
                    And Active = 1
                    and ObjectID = '" & ProjectID & "'  "


        '*** Load a data set.
        Dim ds As New DataSet()
        Dim t As DataTable
        Dim r As DataRow
        ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "Projects")

        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                t = ds.Tables(0)
                r = t.Rows(0)
                llAutonumber = r("CurrentCount")
                lsCodePattern = r("CodePattern")
                lsID = r("ID").ToString

            Else
                lsID = Guid.NewGuid.ToString
                Dim lsCurrentUser As String = fGetUser()
                lsSQL = "INSERT INTO tblAutoNumber
                        (ID   ,Code   ,Name  ,Description ,ObjectID   ,CodePattern ,CurrentCount
					    ,UpdateDate  ,UpdateUser ,CreateDate  ,CreateUser,Active,NodeID)
    			    Values (  '" & fTakeOutQuotes(lsID) & "', " & NL &
                            "'" & fTakeOutQuotes(ObjectType) & "', " & NL &
                            "'" & fTakeOutQuotes(ObjectType) & "', " & NL &
                            "'" & fTakeOutQuotes(ObjectType) & "', " & NL &
                            "'" & fTakeOutQuotes(ProjectID) & "', " & NL &
                            "'" & Left(ObjectType, 1) & "@', 1, " & NL &
                            "Getdate(), " & NL &
                            "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
                            "GetDate(), " & NL &
                            "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
                            " 1, " & NodeID & "); "
                cmd.CommandText = lsSQL
                lbRetVal = fExecuteSQLCmd("SQLConnection", lscnStr, cmd)
                llAutonumber = 1
                lsCodePattern = Left(ObjectType, 1) & "@"
            End If
        End If
        If InStr(lsCodePattern, "@") > 0 Then
            Dim lsnum As String = "0000" & CStr(llAutonumber + 1).Trim
            lsnum = Right(lsnum, 4)
            lsCodePattern = Replace(lsCodePattern, "@", lsnum)
        Else
            lsCodePattern = CStr(llAutonumber + 1).Trim
        End If



        lsSQL = "UPDATE TBLAUTONUMBER SET CurrentCount = " & llAutonumber + 1 & " Where ID = '" & lsID & "'"
        cmd.CommandText = lsSQL
        lbRetVal = fExecuteSQLCmd("SQLConnection", lscnStr, cmd)
        Return lsCodePattern
    End Function

    Public Function fAutoNumberExists(NodeID As Long, ObjType As String, TableName As String, ProjectID As String, ID As String, Code As String, ErrMsg As String) As Boolean
        fAutoNumberExists = False
        Dim lsSQL As String
        Dim lscnStr As String = mscnStr
        '*** Initialize

        If NodeID = 0 Then
            'response.write("No Project Selectedd")
            Exit Function
        End If

        lsSQL = "Select ID from " & TableName & " 
                 WHERE  NodeID = " & NodeID & " 
                    and Code = '" & Code & "' 
                    and Active = 1 "
        If ProjectID Is Nothing Then
        Else
            If TableName.ToUpper = "TBLPROJECTS" Then
            Else

                lsSQL = lsSQL & "   and ObjectID = '" & ProjectID & " '"
            End If
        End If
        If Not ID Is Nothing Then
            If ID.Length = 36 Then lsSQL = lsSQL & " and ID<>'" & ID & "' "
        End If

        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "Projects")

        If ds.Tables(0).Rows.Count > 0 Or msErrorMsg <> "" Then
            Return True
        Else
            Return False
        End If

    End Function
End Class
Public Class clsReportFields



    Public Function LoadRecords(ReportNum As Long, ControlNum As Long, FieldID As String) As Collection
        Dim c As New clsDataLoader
        Dim ds As DataSet = c.LoadReportControls(ReportNum, ControlNum, FieldID)
        Dim dr As DataRow
        Dim dt As DataTable
        Dim cf As New clsReportField
        Dim cFields As Collection = New Collection
        If ds Is Nothing Then Return cFields '  *** Form is not Active on the screen
        If ds.Tables.Count > 0 Then
            dt = ds.Tables(0)
            Dim liCounter As Integer
            For liCounter = 0 To dt.Rows.Count - 1
                dr = dt.Rows(liCounter)
                cf.ReportFieldID = dr("ID")
                cf.ReportFieldID = IIf(IsDBNull(dr("ReportControlFieldID")), "", dr("ReportControlFieldID").ToString)
                cf.FieldName = IIf(IsDBNull(dr("FieldName")), "", dr("FieldName"))
                cf.TableName = IIf(IsDBNull(dr("TableName")), "", dr("TableName"))

                cf.Name = IIf(IsDBNull(dr("Name")), "", dr("Name"))
                cf.NodeID = IIf(IsDBNull(dr("NodeID")), "", dr("NodeID"))
                cf.FieldLength = IIf(IsDBNull(dr("FieldLength")), "", dr("FieldLength"))
                cf.Title = IIf(IsDBNull(dr("FieldTitle")), "", dr("FieldTitle"))
                cf.ValidationTitle = IIf(IsDBNull(dr("FieldValidationTitle")), "", dr("FieldValidationTitle"))
                cf.ValidationPattern = IIf(IsDBNull(dr("FieldValidationPattern")), "", dr("FieldValidationPattern"))
                cf.Validation = IIf(IsDBNull(dr("Validation")), "", dr("Validation"))
                cf.ReportID = IIf(IsDBNull(dr("ReportID")), "", dr("ReportID"))
                cf.ReportControl = IIf(IsDBNull(dr("ReportControl")), "", dr("ReportControl"))
                cf.SortOrder = IIf(IsDBNull(dr("SortOrder")), "", dr("SortOrder"))
                cf.FieldReadOnly = IIf((IsDBNull(dr("FieldReadOnly"))), True, False)
                cf.Active = IIf(IsDBNull(dr("Active")), False, IIf(dr("Active") = True, True, False))
                cf.Required = IIf(IsDBNull(dr("Required")), False, IIf(dr("Required") = True, True, False))
                cf.FieldType = IIf(IsDBNull(dr("FieldType")), "", dr("FieldType"))
                cf.HelpText = IIf(IsDBNull(dr("ControlFieldHelpText")), "", dr("ControlFieldHelpText"))
            Next
        End If

        Return cFields

    End Function

End Class
Public Class clsReportField

    Public ReportFieldID As String
    Public ReportControl As String
    Public FieldName As String
    Public TableName As String
    Public Name As String
    Public Code As String
    Public Title As String
    Public FieldLength As Integer
    Public ValidationTitle As String
    Public ValidationPattern As String
    Public Validation As String
    Public ReportID As Long
    Public FieldReadOnly As Boolean
    Public Active As Boolean
    Public Required As Boolean
    Public FieldType As String
    Public NodeID As String
    Public SortOrder As Integer = 0
    Public ContainerName As String = ""
    Public ColumnSize As Integer = 12
    Public ErrorMessage As String
    Public HelpText As String
    Public Sub New()
        NodeID = System.Web.HttpContext.Current.Session("NodeID")
    End Sub

    Public Function Insert() As Boolean
        Dim c As New Kolassa.DesignCenter.ReportManager.clsDataLoader
        Dim lgID As New Guid
        Dim msSQLcmd As SqlCommand
        lgID = Guid.NewGuid
        Dim lsSQL As String


        '*** Initialize
        Insert = False
        '*** Check for No Selected Category
        'If llNodeID = 0 Then
        ' Exit Function
        ' End If
        If c.fRFFieldExists(0, FieldName, ReportID, ReportFieldID, Name, ErrorMessage) Then
            Name = Name & CStr(10000 * Rnd())  ' Return False
        End If

        lsSQL = "INSERT INTO tblReportFields
           ([ReportID]           ,[ReportControl]           ,[Fieldtype]
           ,[Required]           ,[Active]           ,[NodeID]
           ,[CreateDate]           ,[Createuser]           ,[UpdateDate]           ,[UpdateUser]
           ,[SortOrder]           ,[Validation]           ,[FieldValidationPattern]           ,[FieldValidationTitle]
           ,[TableName]           ,[FieldName]           ,[FieldReadOnly]           ,[FieldLength]
           ,[FieldTitle]           ,[ID]           ,[Name], containername, columnsize, [ControlFieldHelpText])
     VALUES
           (@ReportID,
           ,@pReportControl
           ,@pFieldtype
           ,@pRequired
           ,@pActive
           ,@pNodeID
           ,@pCreateDate
           ,@pCreateuser
           ,@pUpdateDate
           ,@pUpdateUser
           ,@pSortOrder
           ,@pValidation
           ,@pFieldValidationPattern
           ,@pFieldValidationTitle
           ,@pTableName
           ,@pFieldName
           ,@pFieldReadOnly
           ,@pFieldLength
           ,@pFieldTitle
           ,@pID
           ,@pName,@pContainerName, @pColumnSize, @pHelpText)"

        msSQLcmd = New SqlCommand
        msSQLcmd.CommandText = lsSQL
        msSQLcmd.Parameters.Clear()

        msSQLcmd.Parameters.AddWithValue("@ReportID", ReportID)
        msSQLcmd.Parameters.AddWithValue("@pReportControl", ReportControl)
        msSQLcmd.Parameters.AddWithValue("@pFieldtype", FieldType)
        msSQLcmd.Parameters.AddWithValue("@pRequired", Required)
        msSQLcmd.Parameters.AddWithValue("@pActive", Active)
        msSQLcmd.Parameters.AddWithValue("@pNodeID", NodeID)
        msSQLcmd.Parameters.AddWithValue("@pCreateDate", Now())
        msSQLcmd.Parameters.AddWithValue("@pCreateuser", c.fGetUser)
        msSQLcmd.Parameters.AddWithValue("@pUpdateDate", Now())
        msSQLcmd.Parameters.AddWithValue("@pUpdateUser", c.fGetUser)
        msSQLcmd.Parameters.AddWithValue("@pSortOrder", SortOrder)
        msSQLcmd.Parameters.AddWithValue("@pValidation", Validation)
        msSQLcmd.Parameters.AddWithValue("@pFieldValidationPattern", ValidationPattern)
        msSQLcmd.Parameters.AddWithValue("@pFieldValidationTitle", ValidationTitle)
        msSQLcmd.Parameters.AddWithValue("@pTableName", TableName)
        msSQLcmd.Parameters.AddWithValue("@pFieldName", FieldName)
        msSQLcmd.Parameters.AddWithValue("@pFieldReadOnly", FieldReadOnly)
        msSQLcmd.Parameters.AddWithValue("@pFieldLength", FieldLength)
        msSQLcmd.Parameters.AddWithValue("@pFieldTitle", Title)
        msSQLcmd.Parameters.AddWithValue("@pID", ReportFieldID)
        msSQLcmd.Parameters.AddWithValue("@pName", Name)
        msSQLcmd.Parameters.AddWithValue("@pContainerName", ContainerName)
        msSQLcmd.Parameters.AddWithValue("@pColumnSize", ColumnSize)
        msSQLcmd.Parameters.AddWithValue("@pHelpText", HelpText)
        '*** Run The SQL.
        Insert = c.fExecuteSQLCmd("SQLConnection", c.mscnStr, msSQLcmd)
    End Function

    Public Function Update() As Boolean
        Dim c As New Kolassa.DesignCenter.ReportManager.clsDataLoader
        Dim lgID As New Guid
        Dim msSQLcmd As SqlCommand
        lgID = Guid.NewGuid
        Dim lsSQL As String
        ErrorMessage = ""


        '*** Initialize
        Update = False
        '*** Check for No Selected Category
        'If llNodeID = 0 Then
        ' Exit Function
        ' End If nodeID As Long, fieldname As String, ReportID As String, ReportFieldID As String, controlname As String, 
        If c.fRFFieldExists(0, FieldName, ReportID, ReportFieldID, Name, ErrorMessage) Then
            Name = Name & CStr(10000 * Rnd())  ' Return False
        End If
        lsSQL = "Update tblReportFields set
                     [ReportID] = @ReportID
                    ,[ReportControl] = @pReportControl
                    ,[Fieldtype] = @pFieldtype
                    ,[Required] = @pRequired
                    ,[Active] = @pActive
                    ,[NodeID] = @pNodeID
                    ,[UpdateDate] = GetDate()
                    ,[UpdateUser] = @pUser
                    ,[SortOrder] = @pSortOrder
                    ,[Validation] = @pValidation
                    ,[FieldValidationPattern] = @pFieldValidationPattern
                    ,[FieldValidationTitle] = @pFieldValidationTitle
                    ,[TableName] = @pTableName
                    ,[FieldName] = @pFieldName
                    ,[FieldReadOnly] = @pFieldReadOnly
                    ,[FieldLength] =@pFieldLength
                    ,[FieldTitle] = @pFieldTitle
                    ,[ID] = @pID
                    ,[Name] = @pName              ,[ContainerName] = @pContainerName              ,[ColumnSize] = @pColumnSize,[FieldHelpText] = @pHelpText
                  Where ID = @pID"

        msSQLcmd = New SqlCommand
        msSQLcmd.CommandText = lsSQL
        msSQLcmd.Parameters.Clear()

        msSQLcmd.Parameters.AddWithValue("@ReportID", ReportID)
        msSQLcmd.Parameters.AddWithValue("@pReportControl", ReportControl)
        msSQLcmd.Parameters.AddWithValue("@pFieldtype", FieldType)
        msSQLcmd.Parameters.AddWithValue("@pRequired", IIf(Required = True, 1, 0))
        msSQLcmd.Parameters.AddWithValue("@pActive", IIf(Active = True, 1, 0))
        msSQLcmd.Parameters.AddWithValue("@pNodeID", NodeID)
        msSQLcmd.Parameters.AddWithValue("@pUpdateDate", Now())
        msSQLcmd.Parameters.AddWithValue("@pUpdateUser", c.fGetUser)
        msSQLcmd.Parameters.AddWithValue("@pSortOrder", SortOrder)
        msSQLcmd.Parameters.AddWithValue("@pValidation", Validation)
        msSQLcmd.Parameters.AddWithValue("@pFieldValidationPattern", ValidationPattern)
        msSQLcmd.Parameters.AddWithValue("@pFieldValidationTitle", ValidationTitle)
        msSQLcmd.Parameters.AddWithValue("@pTableName", TableName)
        msSQLcmd.Parameters.AddWithValue("@pFieldName", FieldName)
        msSQLcmd.Parameters.AddWithValue("@pFieldReadOnly", IIf(FieldReadOnly = True, 1, 0))
        msSQLcmd.Parameters.AddWithValue("@pFieldLength", FieldLength)
        msSQLcmd.Parameters.AddWithValue("@pFieldTitle", Title)
        msSQLcmd.Parameters.AddWithValue("@pID", ReportFieldID)
        msSQLcmd.Parameters.AddWithValue("@pName", Name)
        msSQLcmd.Parameters.AddWithValue("@pUser", c.fGetUser)
        msSQLcmd.Parameters.AddWithValue("@pContainerName", ContainerName)
        msSQLcmd.Parameters.AddWithValue("@pColumnSize", ColumnSize)
        If HelpText Is Nothing Then
            HelpText = ""
        End If
        msSQLcmd.Parameters.AddWithValue("@pHelpText", IIf(IsDBNull(HelpText), "", HelpText))
        '*** Run The SQL.

        Update = c.fExecuteSQLCmd("SQLConnection", c.mscnStr, msSQLcmd)
        ErrorMessage = c.msErrorMsg
    End Function


End Class

