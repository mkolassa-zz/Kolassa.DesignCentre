Option Strict Off
'Imports System.Data
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
'Imports System.IO
Imports System.Configuration
Imports Microsoft.AspNet.Identity
'Imports Microsoft.AspNet.Identity.EntityFramework
'Imports System.Security.Principal
Public Class clsSelectDataLoader
    Dim mds As DataSet
    Dim mdsUnits As DataSet = New DataSet
	Dim mdsProjects As DataSet = New DataSet
	Dim mdsProjectTypes As DataSet = New DataSet
	Dim mdsUnitProfiles As DataSet = New DataSet
	Dim mdsFloors As DataSet = New DataSet
	Dim mdsQuotes As DataSet = New DataSet
	Dim mdsAllQuotes As DataSet = New DataSet
	Dim mdsVendors As DataSet = New DataSet
	Dim mdsUnitTypes As DataSet = New DataSet
	Dim mdsEventLogs As DataSet = New DataSet
	Dim mdsLookups As DataSet = New DataSet
	Dim mdsLogins As DataSet = New DataSet
	Dim mdsDepositConditions As DataSet = New DataSet
	Dim mdsTiers As DataSet = New DataSet
	Dim mdsCustomers As DataSet = New DataSet
	Dim mdsContacts As DataSet = New DataSet
	Dim mdsCompanyInfo As DataSet = New DataSet
	Public mdsReportCategories As DataSet
	Dim mdsReportControls As DataSet
	Public mdsAllControls As DataSet
	Dim mdsRooms As DataSet
	Dim mdsListItems As DataSet
	Dim mdsChildren As DataSet
	Dim mdsCommunications As DataSet
	Dim cns As SqlConnection
	Dim cno As OleDb.OleDbConnection
	Dim mscnStr As String
	Dim mscnType As String
	Dim mscnDefault As String
	Dim mlCategoryID As Long
	Public msSQLcmd As SqlClient.SqlCommand
	Dim msSQLParameter As SqlParameter
	Dim NL As String = Chr(13) & Chr(10)
	Public Property msErrorMsg As String
	Public Sub New()
		mscnType = "SQLConnection" '"OLEDB"
		'  mscnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Documents and Settings\mkolassa\My Documents\Visual Studio\Projects\WebSites\Select\App_Data\legadata.mdb;User Id=Master;Password=Cubs1;Jet OLEDB:System Database=D:\Documents and Settings\mkolassa\My Documents\Visual Studio\Projects\WebSites\Select\App_Data\Parallax.mdw;"
		mscnStr = ConfigurationManager.ConnectionStrings.Item("ReportManager").ToString
		mscnDefault = ConfigurationManager.ConnectionStrings.Item("DefaultConnection").ToString
		mscnStr = mscnDefault
	End Sub
	Function fGetUser() As String

		fGetUser = Web.HttpContext.Current.User.Identity.GetUserId()
	End Function

	Public Function LoadTables(ByVal llTableID As Long, ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal llID As Long) As DataSet
		Dim lsSQL As String

		'*** Initialize
		LoadTables = Nothing
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If

		lsSQL = "SELECT SelectStatement From tblReportSQL WHERE ReportID = " & llTableID & ";"

		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "Tables")

		lsSQL = ds.Tables(0).Rows(0).Item(0).ToString

		mdsQuotes = ds
		lsSQL = lsSQL & " " & NL &
	   " AND  (NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") &
			 IIf(lbActive = True, " and Active = 1 ", "") & ")" & NL &
		  "" '   IIf(llID > 0, " OR QuoteID = " & llID & " ", "")

		ds = fGetDataset(mscnType, mscnStr, lsSQL, "TableData")


		mdsQuotes = ds
		LoadTables = mdsQuotes

	End Function


	'*******************************************************
	'*** Base - The Root level of These Data Tables
	'*******************************************************
	Public Function LoadBase(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As String) As DataSet
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		'*** Initialize
		LoadBase = Nothing
		'llNodeID = 2
		lsWhere = Replace(lsWhere, "SearchText", " code + name + description ")
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If

		lsSQL = "SELECT NODEID, ID, CODE , Name, Description, Active,  ImageURl, CreateUser, CreateDate, UpdateUser, UpdateDate    " & NL &
				"FROM tblBases                      " & NL &
				"WHERE  ( NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " And " & lsWhere, "") &
					IIf(lbActive = True, " And Active = 1 ", "") & ")" & NL &
					IIf(isGUID(lsID), " AND ID = '" & lsID & "' ", "")


		'*** Load a data set.
		Dim ds As New DataSet()
		' ds = fGetDataset(mscnType, lscnStr, lsSQL, "Projects")
		ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "Projects")
		mdsProjects = ds
		LoadBase = mdsProjects
	End Function
	Public Function DeleteBase(ByVal RecordID As String, llNodeID As Long) As Boolean
		Dim lscnStr As String = mscnDefault
		Dim lsSQL As String = "Update tblBases Set Active=0  WHERE  NodeID=" & llNodeID & " AND ID = '" & RecordID & "'"
		DeleteBase = fRunSQL("SQLConnection", lscnStr, lsSQL)
	End Function
	Public Function InsertBase(ByVal llNodeID As Long, ByVal lsName As String, lsDescription As String, ByVal lsImage As String, lsCode As String) As Boolean
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertBase = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "INSERT INTO tblBases ( ID, Name, Code, Description,  ImageUrl, " &
				"                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
				"Values ( '" & Guid.NewGuid.ToString & "', " &
						 "'" & fTakeOutQuotes(lsName) & "', " & NL &
						  "'" & fTakeOutQuotes(lsCode) & "', " & NL &
						  "'" & fTakeOutQuotes(lsDescription) & "', " & NL &
						  "'" & fTakeOutQuotes(lsImage) & "', " & NL &
						  "'" & Now.ToString & "', " & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
						  "'" & Now.ToString & "', " & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
						   "1, '" & llNodeID & "'); "
		'*** Run The SQL.
		InsertBase = fRunSQL("SQLConnection", lscnStr, lsSQL)
	End Function
	Public Function UpdateBase(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsDescription As String, ByVal lsImage As String, lsProjectType As String, ByVal lsActive As String, ByVal ID As String, lsCode As String) As Boolean
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		UpdateBase = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			Exit Function
		End If
		lsSQL = "Update tblBases  " &
				"Set Name = '" & fTakeOutQuotes(lsName) & "', Description = " & NL &
						  "'" & fTakeOutQuotes(lsDescription) & "', CODE= " & NL &
						  "'" & fTakeOutQuotes(lsCode) & "'       , ImageUrl= " & NL &
						  "'" & fTakeOutQuotes(lsImage) & "'      , UpdateDate=" & NL &
						  "'" & Now.ToString & "'                 , UpdateUser=" & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', Active = " & lsActive & NL &
				 " Where ID='" & ID & "';"
		'*** Run The SQL.
		UpdateBase = fRunSQL("SQLConnection", lscnStr, lsSQL)
	End Function

	'*******************************************************
	'*** Projects
	'*******************************************************
	Public Function LoadProjects(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As String) As DataSet
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		'*** Initialize
		LoadProjects = Nothing
		'llNodeID = 2
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "SELECT F.NODEID, F.CODE ,F.ID, F.Name, F.Description, F.Active, F.Image, f.ImageURL, F.ProjectType , F.Code, F.ProjectTypeName    FROM " & NL &
				"(Select P.NodeID, P.ID, P.Name, P.Description, P.ImageURL, P.Active, P.Image, P.ProjectType, T.Code, T.Name as ProjectTypeName  " & NL &
				"FROM tblProjects as P left join   tblProjectTypes as T on P.ProjectType = T.Code )  F                              " & NL &
				"WHERE  ( NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " And " & lsWhere, "") &
					IIf(lbActive = True, " And Active = 1 ", "") & ")" & NL &
					IIf(isGUID(lsID), " AND f.ID = '" & lsID & "' ", "")


		'*** Load a data set.
		Dim ds As New DataSet()
		' ds = fGetDataset(mscnType, lscnStr, lsSQL, "Projects")
		ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "Projects")
		mdsProjects = ds
		LoadProjects = mdsProjects
	End Function
	Public Function DeleteProjects(ByVal RecordID As String, llNodeID As Long) As Boolean
		Dim lscnStr As String = mscnDefault
		Dim lsSQL As String = "Update tblProjects Set Active=0   WHERE  NodeID=" & llNodeID & " AND ID = '" & RecordID & "'"
		DeleteProjects = fRunSQL("SQLConnection", lscnStr, lsSQL)
	End Function
	Public Function InsertProjects(ByVal llNodeID As Long, ByVal lsName As String, lsDescription As String, ByVal lsImage As String, ByVal lsProjectType As String, lsCode As String) As Boolean
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertProjects = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "INSERT INTO tblProjects ( Name, Code, Description,  ProjectType, " &
				"                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
				"Values ( '" & fTakeOutQuotes(lsName) & "', " & NL &
						  "'" & fTakeOutQuotes(lsCode) & "', " & NL &
						  "'" & fTakeOutQuotes(lsDescription) & "', " & NL &
						  "'" & fTakeOutQuotes(lsProjectType) & "', " & NL &
						  "'" & Now.ToString & "', " & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
						  "'" & Now.ToString & "', " & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
						   "1, '" & llNodeID & "'); "
		'*** Run The SQL.
		InsertProjects = fRunSQL("SQLConnection", lscnStr, lsSQL)
	End Function
	Public Function UpdateProjects(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsDescription As String, ByVal lsImage As String, lsProjectType As String, ByVal lsActive As String, ByVal ID As String, lsCode As String) As Boolean
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		UpdateProjects = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			Exit Function
		End If
		lsSQL = "Update tblProjects  " &
				"Set Name = '" & fTakeOutQuotes(lsName) & "', Description = " & NL &
						  "'" & fTakeOutQuotes(lsDescription) & "', CODE= " & NL &
						  "'" & fTakeOutQuotes(lsCode) & "', image= " & NL &
						  "'" & fTakeOutQuotes(lsImage) & "', ProjectType= " & NL &
						  "'" & fTakeOutQuotes(lsProjectType) & "', UpdateDate=" & NL &
						  "'" & Now.ToString & "', UpdateUser=" & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', Active = " & lsActive & NL &
				 " Where ID='" & ID & "';"
		'*** Run The SQL.
		UpdateProjects = fRunSQL("SQLConnection", lscnStr, lsSQL)
	End Function
	'*******************************************************
	'*** Project Types
	'*******************************************************
	Public Function LoadProjectTypes(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal llID As Long) As DataSet
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		'*** Initialize
		LoadProjectTypes = Nothing
		'llNodeID = 2
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "SELECT * " & NL &
				"FROM tblProjectTypes                                   " & NL &
				"WHERE ( tblProjectTypes.NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") &
					 IIf(lbActive = True, " and Active = 1 ", "") & ")" & NL &
					 IIf(llID > 0, " OR QuoteID = " & llID & " ", "")


		'*** Load a data set.
		Dim ds As New DataSet()
		' ds = fGetDataset(mscnType, lscnStr, lsSQL, "Projects")
		ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "Projects")
		mdsProjectTypes = ds
		LoadProjectTypes = mdsProjectTypes
	End Function
	Public Function DeleteProjectTypes(ByVal RecordID As String, llNodeID As Long) As Boolean
		Dim lscnStr As String = mscnDefault
		Dim lsSQL As String = "Update tblProjects Set Active=False   WHERE  NodeID=" & llNodeID & " AND ID = '" & RecordID & "'"
		DeleteProjectTypes = fRunSQL("SQLConnection", lscnStr, lsSQL)
	End Function
	Public Function InsertProjectTypes(ByVal llNodeID As Long, ByVal lsName As String, lsDescription As String, ByVal lsImage As String, ByVal lsProjectType As String, lsCode As String) As Boolean
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertProjectTypes = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "INSERT INTO tblProjectTypes ( Name, Code, Description, Image, ProjectType " &
				"                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
				"Values ( """ & fTakeOutQuotes(lsName) & """, " & NL &
						  """" & fTakeOutQuotes(lsCode) & """, " & NL &
							"""" & fTakeOutQuotes(lsDescription) & """, " & NL &
						  """" & fTakeOutQuotes(lsImage) & """, " & NL &
						  """" & fTakeOutQuotes(lsProjectType) & """, " & NL &
						  """" & Now.ToString & """, " & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
						  """" & Now.ToString & """, " & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
						   "1, " & llNodeID & "); "
		'*** Run The SQL.
		InsertProjectTypes = fRunSQL("SQLConnection", lscnStr, lsSQL)
	End Function

	Public Function UpdateProjectTypes(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsDescription As String, ByVal lsImage As String, lsCode As String, ByVal lsActive As String, ByVal ID As String) As Boolean
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		UpdateProjectTypes = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			Exit Function
		End If
		lsSQL = "Update tblProjectTypes  " &
				"Set Name = """ & fTakeOutQuotes(lsName) & """, Description = " & NL &
						  """" & fTakeOutQuotes(lsDescription) & """, image= " & NL &
						  """" & fTakeOutQuotes(lsImage) & """, Code= " & NL &
						  """" & fTakeOutQuotes(lsCode) & """, UpdateDate=" & NL &
						  """" & Now.ToString & """, UpdateUser=" & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL &
				 " Where ID=" & ID & ";"
		'*** Run The SQL.
		UpdateProjectTypes = fRunSQL("SQLConnection", lscnStr, lsSQL)
	End Function

	'*******************************************************
	'*** Quotes
	'*******************************************************
	Public Function LoadQuotes(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As String) As DataSet
		Dim lsSQL As String

		'*** Initialize
		LoadQuotes = Nothing
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			'	Exit Function
		End If
		'*** insert Phases if they Do not exist
		Dim lsCurrentUser As String = fGetUser()
		If Not isGUID(lsCurrentUser) Then

			Exit Function
		End If



		If isGUID(lsID) Then
			InsertQuotePhases(lsID, lsCurrentUser)
		Else
			lsID = "00001111-1111-2222-3333-444455556666"
		End If
		lsSQL = "SELECT * " & NL &
				"FROM v_QuoteLookup                                   " & NL &
				"WHERE ( 1=1 " &
					 IIf(1 = 2, " AND NodeID=" & llNodeID & " ", " ") &
					 IIf(lsWhere.Length > 4, " And " & lsWhere, "") &
					 IIf(lbActive = True, " And Active = 1 ", "") & ")" & NL &
					 IIf(lsID = "", "", " And ID = '" & lsID & "' ")


		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "Units")

		mdsQuotes = ds
		LoadQuotes = mdsQuotes
	End Function
	Public Function LoadAllQuotes(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal llID As Long, ParentID As String, ProjectID As String) As DataSet
		Dim lsSQL As String

		'*** Initialize
		LoadAllQuotes = Nothing
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "SELECT * " & NL &
				"FROM v_QuoteLookup                                   " & NL &
			   "WHERE ( 1=1 " & NL &
					IIf(1 = 1, " AND NodeID=" & llNodeID & " ", "") &
					IIf(lsWhere.Length > 4, " And " & lsWhere, "") &
					IIf(lbActive = True, " And Active = 1 ", "") & ")" & NL &
					IIf(isGUID(ParentID), " And UnitParent = '" & ParentID & "' ", "") & NL &
					IIf(isGUID(ProjectID), " And ObjectID = '" & ProjectID & "' ", "")
		' IIf(llID > 0, " OR QuoteID = " & llID & " ", "")


		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "Quotes")

		LoadAllQuotes = ds
		LoadAllQuotes = LoadAllQuotes
	End Function
	Public Function DeleteQuotes(ByVal RecordID As String, llNodeID As Long) As Boolean
		Dim lsSQL As String = "Update tblQuotes Set Active=False   WHERE  NodeID=" & llNodeID & " AND ID = '" & RecordID & "'"
		DeleteQuotes = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function InsertQuotes(ByVal llNodeID As Long, ByVal lsProjectID As String, ByVal lsCustomerID As String, ByVal lsUnitID As String, ByRef lsID As String) As Boolean
		Dim lsSQL As String
		lsID = Guid.NewGuid.ToString
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertQuotes = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "INSERT INTO tblQuote ( ID, ObjectID, CustomerID, UnitID, QuoteStatus, " &
				"                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
				"Values ( '" & lsID & "', " & NL &
						  "'" & fTakeOutQuotes(lsProjectID) & "', " & NL &
						  "'" & fTakeOutQuotes(lsCustomerID) & "', " & NL &
						  "'" & fTakeOutQuotes(lsUnitID) & "', " & NL &
						  "'Active', getdate() ," & NL &
						  "" & " " & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
						  " getdate(), " & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
						   "1, " & llNodeID & "); "
		'*** Run The SQL.
		InsertQuotes = fRunSQL(mscnType, mscnStr, lsSQL)
		'*** Update Phases
		If lsCurrentUser = "" Or lsProjectID = "" Or lsID = "" Then
			Exit Function
		Else
			InsertQuotePhases(lsID, lsCurrentUser)
		End If
	End Function
	Public Function UpdateQuotes(ByVal lsID As String, ByVal lsCustomerID As String, ByVal lsUnitID As String, ByVal lsActive As String, ByVal lsQuoteStatus As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		UpdateQuotes = False

		lsSQL = "Update tblQuote  " &
				"Set " &
				"CustomerID = '" & lsCustomerID & "', " &
				"UnitID = '" & lsUnitID & "', " &
				"QuoteStatus = '" & lsQuoteStatus & "', " &
				"UpdateDate=getdate() ," & NL &
				"UpdateUser='" & fTakeOutQuotes(lsCurrentUser) & "', Active = " & lsActive & NL &
				" Where ID='" & lsID & "';"
		'*** Run The SQL.
		UpdateQuotes = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Function InsertQuotePhases(lsQuoteID As String, ByVal lsCurrentUser As String)
		Dim lsSQL As String
		lsSQL = "insert into tblProjectPhases(ID, ObjectID, Code, Name, Description, CreateDate, CreateUser, Active, NodeID, SortOrder, PhaseStatus) " &
		"Select  newid(), '" & lsQuoteID & "', code, name, description, getdate(),'" & lsCurrentUser & "',1,nodeid,sortorder ,'Pending' " &
		"From ( " &
		"    Select  quote.code As qcode,project.* from tblProjectPhases  As project " &
		"    Left Join ( " &
		"                  Select  ObjectID, code from tblProjectPhases " &
		"                  where ObjectID = '" & lsQuoteID & "' " &
		"              ) as quote " &
		"    On Project.code = quote.code " &
		"where project.objectID =  (Select objectID from tblQuote where id = '" & lsQuoteID & "') and project.active=1 and quote.code is null " &
		") as a;"
		InsertQuotePhases = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function





	'****************************************************
	'*** Requested Upgrades
	Public Function LoadRequestedUpgrades(ByVal llNodeID As Long, ByVal lsRoom As String, ByVal lsPhase As String, ByVal lsQuoteID As String, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As String, Optional ByVal lsCat As String = "") As DataSet
		Dim lsSQL As String

		'*** Initialize
		LoadRequestedUpgrades = Nothing
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		If isGUID(lsQuoteID) = False Then lsQuoteID = "11112222-3333-4444-5555-666677778888"
		If isGUID(lsQuoteID) = False Then lsQuoteID = "11112222-3333-4444-5555-666677778888"
        lsSQL = "SELECT ID as RequestedUpgradeID, " &
                "       UpgradeCategory as Category, " &
                "       UpgradeClass as UpgradeLevel, " &
                "       UpgradeDescription as Description, " &
                "       StyleDescription as Style, " &
                                "       Quantity, CustomerPrice, Adjustments, " &
                                                "       StyleDescription as Style, " &
                "      QUantity * CustomerPrice + Adjustments AS Cost , Comments ,[Standard] " &
                "FROM   tblRequestedUpgrades " &
                "WHERE 1=1  " &
        IIf(lbActive = True, " and Active = 1 ", "") & NL
        If lsID = "00000000-0000-0000-0000-000000000000" Or Not isGUID(lsID) Then
			lsSQL = lsSQL & " AND RoomDescription = '" & lsRoom & "' " &
				"        AND BuildingPhase = '" & lsPhase & "' " &
				"        AND ObjectID = '" & lsQuoteID & "' " &
				IIf(lsWhere.Length > 4, " AND " & lsWhere, "") &
				IIf(lsCat = "", "", " and UpgradeCategory =  '" & lsCat & "' ") & NL
		Else
			lsSQL = lsSQL & IIf(isGUID(lsID), " AND ID = '" & lsID & "' ", "") & NL
		End If
        lsSQL = lsSQL & "ORDER BY UpgradeCategory"


        '*** Load a data set.
        Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "Customers")

		mdsCustomers = ds
		LoadRequestedUpgrades = mdsCustomers
	End Function
	Public Function DeleteRequestedUpgrades(ByVal RecordID As String) As Boolean
		If Not isGUID(RecordID) Then
			DeleteRequestedUpgrades = False
			Exit Function
		End If
		Dim lsSQL As String = "UPDATE  tblRequestedUpgrades set active = 0  WHERE  ID ='" & RecordID & "'"
		DeleteRequestedUpgrades = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function InsertRequestedUpgrades(ByVal llNodeID As Long, ByVal lsProjectID As String,
											ByVal lsQuoteID As String, ByVal lsOptionID As String, liQuantity As Int16) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertRequestedUpgrades = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'response.write("No Project Selectedd")
            Exit Function
        End If

        If Not isGUID(lsProjectID) Or Not isGUID(lsQuoteID) Or Not isGUID(lsOptionID) Or llNodeID = 0 Then
			InsertRequestedUpgrades = False
			Exit Function
		End If
        lsSQL = "insert into tblRequestedUpgrades ( RequestType, QuoteID, RoomDescription, UpgradeDescription " &
                "    , UpgradeCategory, UpgradeClass, StyleDescription, CustomerPrice, Adjustments, Quantity " &
                "    , BuildingPhase, PricingRevNumber, LeadVendor, Standard, RequestedUpgradeFlexText1 " &
                "    , RequestedUpgradeFlexText2, RequestedUpgradeFlexText3, RequestedUpgradeFlexText4 " &
                "    , AdditionalFileToPrint1, AdditionalFileToPrint2, Comments " &
                "    , UpdateDate, UpdateUser, CreateDate, CreateUser, Active " &
                "    , ObjectID, ID, OptionID, UnitTypeID, NodeID)   " &
                "select   'Standard', '" & lsQuoteID & "' , Location, Description " &
                "    , UpgradeCategory, UpgradeLevel, ModelOrStyle, CustomerPrice, 0, " & liQuantity & " " &
                "    , BuildingPhase, PricingRevNumber, LeadVendorID, Standard, '', '', '', '' " &
                "    , AdditionalFileToPrint1, AdditionalFileToPrint2, Comments " &
                "    , getdate(), '" & lsCurrentUser & "' , getdate(), '" & lsCurrentUser & "' , 1 " &
                "    , '" & lsQuoteID & "', NewID(), ID, UnitTypeID,  " & llNodeID & " " &
                "FROM tblUpgradeOptions where ID = '" & lsOptionID & "'"

        '*** Run The SQL.
        InsertRequestedUpgrades = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function InsertRequestedUpgradesCustom(RequestType As String,
			RoomDescription As String, UpgradeDescription As String,
			UpgradeCategory As String, UpgradeClass As String,
			StyleDescription As String, CustomerPrice As Double,
			Adjustments As Double, Quantity As Integer,
			BuildingPhase As String, PricingRevNumber As Integer,
			Standard As Boolean, Comments As String,
			UpgradeOptionID As Integer, UpdateDate As Date,
			UpdateUser As String, CreateDate As Date,
			CreateUser As String, Active As Boolean,
			ObjectID As String, ID As String,
			OptionID As String, UnitTypeID As String,
			llNodeID As Long, QuoteID As String, LeadVendor As String, rf1 As String, rf2 As String, rf3 As String, rf4 As String,
			AdditionalFileToPrint1 As String, AdditionalFileToPrint2 As String)
		Dim lsSQL As String = ""
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertRequestedUpgradesCustom = False
		'*** Check for No Selected Category
		'If llNodeID = 0 Then
		''response.write("No Project Selectedd")
		'Exit Function
		'End If
		If Not isGUID(QuoteID) Or Not isGUID(OptionID) Or llNodeID = 0 Then
			InsertRequestedUpgradesCustom = False
			Exit Function
		End If
		lsSQL = "insert into tblRequestedUpgrades ( RequestType, QuoteID, RoomDescription, UpgradeDescription " &
				"    , UpgradeCategory, UpgradeClass, StyleDescription, CustomerPrice, Adjustments, Quantity " &
				"    , BuildingPhase, PricingRevNumber, LeadVendor, Standard, RequestedUpgradeFlexText1 " &
				"    , RequestedUpgradeFlexText2, RequestedUpgradeFlexText3, RequestedUpgradeFlexText4 " &
				"    , AdditionalFileToPrint1, AdditionalFileToPrint2, Comments, UpgradeOptionID " &
				"    , UpdateDate, UpdateUser, CreateDate, CreateUser, Active " &
				"    , ObjectID, ID, OptionID, UnitTypeID, NodeID, oldrequestedupgradeID)   " &
				"VALUES (   'Custom', '" & QuoteID & "' , '" & fTakeOutQuotes(RoomDescription) & "' , '" & fTakeOutQuotes(UpgradeDescription) & "' " &
				"    , '" & fTakeOutQuotes(UpgradeCategory) & "', '" & fTakeOutQuotes(UpgradeClass) & "', '" & fTakeOutQuotes(StyleDescription) & "',  " &
				"    , " & CustomerPrice & "," & Adjustments & ", " & Quantity & " " &
				"    , '" & BuildingPhase & "'," & PricingRevNumber & ", '" & LeadVendor & "', '" & Standard & "' " &
				"    , '" & fTakeOutQuotes(rf1) & "', '" & fTakeOutQuotes(rf2) & "', '" & fTakeOutQuotes(rf3) & "', '" & fTakeOutQuotes(rf4) & "' " &
				"    , '" & fTakeOutQuotes(AdditionalFileToPrint1) & "', '" & fTakeOutQuotes(AdditionalFileToPrint2) & "' " &
				"    , '" & fTakeOutQuotes(Comments) & "', '" & UpgradeOptionID & "' " &
				"    , getdate(), '" & lsCurrentUser & "' , getdate(), '" & lsCurrentUser & "' , 1 " &
				"    , '" & QuoteID & "', NewID(), ID, UnitTypeID,  " & llNodeID & ", 0 " &
				" )"

		'*** Run The SQL.
		InsertRequestedUpgradesCustom = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function

    Public Function UpdateRequestedUpgrades(ID As String, ByVal llNodeID As Long, ByVal ldAdjustments As Double, ByVal lsComments As String, ByVal liQuantity As Integer) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        UpdateRequestedUpgrades = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            Exit Function
        End If

        lsSQL = "UPDATE [tblRequestedUpgrades]
        SET  [Adjustments] = " & ldAdjustments & "
            ,[Quantity] = " & liQuantity & "
            ,[Comments] = '" & fTakeOutQuotes(lsComments) & "'
            ,[UpdateDate] = '" & Now.ToString & "'
            ,[UpdateUser] = '" & fTakeOutQuotes(lsCurrentUser) & "'
	WHERE ID = '" & ID & "';" ' and  NodeID=" & llNodeID & ";"

        '*** Run The SQL.
        UpdateRequestedUpgrades = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function




    '****************************************************
    '*** Customers
    Public Function LoadCustomers(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As String, SortExpression As String, SortOrder As String) As DataSet
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		'*** Initialize
		LoadCustomers = Nothing
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		If lsWhere Is Nothing Then lsWhere = ""
		lsWhere = Replace(lsWhere, "SearchText", " UPPER( isnull(f.customername,'') + isnull(f.customeraddress ,'') + isnull( f.customercity ,'') + isnull( f.customerstate ,'') + isnull( f.customerpostalcode ,'') + isnull( f.customerCountry ,'') + isnull( f.customerPhone ,'') + isnull( f.Customeremail,''))   ")
		If lsWhere Is Nothing Then lsWhere = ""
		lsSQL = "Select convert(nvarchar(50), f.ID) As ID, f.CustomerName, f.CustomerAddress, f.CustomerCity, f.CustomerState As StateProvince, f.CustomerPostalCode As Postal_Code , 
                   f.CustomerCountry, f.CustomerPhone, f.CustomerEmail, f.NodeID, f.CustomerType, f.Active As CustomerActive, 
				   f.CreateDate As CustomerCreateDate, f.CreateUser As CustomerCreateUser, f.UpdateDate As CustomerUpdateDate, f.UpdateUser As CustomerUpdateUser, 
				   uc.username as CreateUserName, uu.username As UpdateUserName , ci.imageurl 
				From tblCustomers f    Left Join aspnetUsers As uc On f.createuser = convert(varchar(40), uc.ID )
                                         Left Join aspnetUsers as uu on f.UpdateUser = convert(varchar(40), uu.ID )
                                         Left Join(Select * from tblImages where type='Primary' and nodeID = '" & llNodeID & "' ) as ci on f.ID = ci.ObjectID " & NL &
				"WHERE  f.NodeID=" & llNodeID & " " & NL &
					IIf(lsWhere.Length > 4, " And " & lsWhere, "") &
					IIf(lbActive = True, " And f.Active = 1 ", "") & NL &
					IIf(lsID <> "", " and f.ID = '" & lsID & "' ", "") & NL &
				IIf(SortExpression = "", "", " Order By " & SortExpression & " " & SortOrder)


		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "Customers")

		'*** Debugging Start
		Dim dt As DataTable = ds.Tables.Item("Customers")
		Dim rowCustomer As DataRow

		For Each rowCustomer In dt.Rows
			Dim ls As String = rowCustomer.Item(0).ToString
			Console.WriteLine("Ya" & ls & "Ya")
			System.Diagnostics.Debug.Write(ls)
		Next
		'*** Debugging End

		mdsCustomers = ds
		LoadCustomers = mdsCustomers
	End Function
	Public Function DeleteCustomers(ByVal RecordID As String, llNodeID As Long) As Boolean
		Dim lscnStr As String = mscnDefault
		Dim lsSQL As String = "Update tblCustomers Set Active=0   WHERE  NodeID=" & llNodeID & " AND ID = '" & RecordID & "'"
		DeleteCustomers = fRunSQL("SQLConnection", lscnStr, lsSQL)
	End Function
	Public Function InsertCustomers(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsAddress As String,
									ByVal lsCity As String, ByVal lsState As String, ByVal lsPostalCode As String,
									ByVal lsPhone As String, ByVal lsemail As String,
									ByVal lsCustomerCountry As String) As Boolean
		Dim lgID As New Guid
		lgID = Guid.NewGuid
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertCustomers = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "INSERT INTO tblCustomers (ID, CustomerName, CustomerAddress, CustomerCity, CustomerState, CustomerPostalCode, CustomerCountry,  CustomerEmail, CustomerPhone, " &
				"                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
				"Values ( @CustomerGuid, @CustomerName, @CustomerAddress, @CustomerCity, @CustomerState, @CustomerPostalCode, @CustomerCountry, @CustomerEmail, @CustomerPhone, " & NL &
				"         GetDate(), @CurrentUser, GetDate(), @CurrentUser, 1, @CustomerNode );"
		msSQLcmd = New SqlCommand
		msSQLcmd.CommandText = lsSQL
		msSQLcmd.Parameters.Clear()
		msSQLcmd.Parameters.AddWithValue("@CustomerGuid", fTakeOutQuotes(lgID.ToString))
		msSQLcmd.Parameters.AddWithValue("@CustomerName", fTakeOutQuotes(lsName))
		msSQLcmd.Parameters.AddWithValue("@CustomerAddress", fTakeOutQuotes(lsAddress))
		msSQLcmd.Parameters.AddWithValue("@CustomerCity", fTakeOutQuotes(lsCity))
		msSQLcmd.Parameters.AddWithValue("@CustomerState", fTakeOutQuotes(lsState))
		msSQLcmd.Parameters.AddWithValue("@CustomerPostalCode", fTakeOutQuotes(lsPostalCode))
		msSQLcmd.Parameters.AddWithValue("@CustomerEmail", fTakeOutQuotes(lsemail))
		msSQLcmd.Parameters.AddWithValue("@CustomerPhone", fTakeOutQuotes(lsPhone))
		msSQLcmd.Parameters.AddWithValue("@CurrentUser", fTakeOutQuotes(lsCurrentUser))
		msSQLcmd.Parameters.AddWithValue("@CustomerNode", fTakeOutQuotes(llNodeID))
		msSQLcmd.Parameters.AddWithValue("@CustomerCountry", fTakeOutQuotes(lsCustomerCountry))
		'*** Run The SQL.
		InsertCustomers = fExecuteSQLCmd("SQLConnection", lscnStr, msSQLcmd)
	End Function
	Public Function InsertAutoPick(ByVal llNodeID As Long, ByVal lsQuoteID As String, ByVal lsUnitTypeID As String, liBuildingPHase As Integer, ByRef dt As DataTable, ByRef lsmessage As String) As Boolean

		Dim lgID As New Guid
		lgID = Guid.NewGuid
		Dim lsSQL, lsSQL1 As String
		Dim lscnStr As String = mscnDefault
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		Dim msg As String
		Dim ds As DataSet
		'*** Initialize
		InsertAutoPick = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL1 = "		  SELECT NEWID() SelectedItemID,  uo.ID
					     , @QuoteID QuoteID
					     , @QuoteID ObjectID
					     , 'Standard' requesttype
					     ,uo.[Location]
					     ,uo.[Description]
		                 ,uo.UpgradeCategory
					     ,uo.UpgradeLevel
		                 ,uo.ModelOrStyle
					     ,uo.CustomerPrice
					     ,0 iAdjust ,1 iQuant
		                 ,uo.BuildingPhase
					     ,uo.PricingRevNumber 
		                 ,uo.[Standard] 
				       --,LeadVendorID
					     ,uo.Unittypeid
					     ,0  NodeID, 1 itemActive
  					     , @CurrentUser CreateUser, getdate() createdate, @CurrentUser updateuser, getdate() updatedate
				         , uo.AdditionalFileToPrint1, uo.AdditionalFileToPrint2, uo.comments
				       --, ru.ID
					FROM tblUpgradeOptions uo
					    LEFT JOIN (Select * from tblRequestedUpgrades where quoteid=@QuoteID  and active = 1)  ru ON uo.UnitTypeID = ru.UnitTypeID and uo.[location] = ru.RoomDescription and uo.UpgradeCategory = ru.UpgradeCategory
						and uo.UpgradeLevel = ru.UpgradeClass 
		            WHERE 1=1 
					    AND uo.UnitTypeID =  @UnitTypeID
                       -- AND NodeID= @NodeID
					    AND RU.id is  null
					  --AND CustomerPrice= 0
                      --AND UpgradeLevel = 'Standard' 
		                AND uo.[Standard] = 1 
                        AND uo.BuildingPhase = @BuildingPhase"

		lsSQL = " INSERT INTO tblRequestedUpgrades ( ID, OptionID, ObjectID, QuoteID, RequestType, RoomDescription, 
                       UpgradeDescription, UpgradeCategory, UpgradeClass, StyleDescription, CustomerPrice, Adjustments, Quantity
                      ,BuildingPhase, PricingRevNumber, [Standard]
			        --, LeadVendor
			          , UnitTypeID ,NodeID , Active, CreateUser, CreateDate, UpdateUser, UpdateDate ,AdditionalFileToPrint1,AdditionalFileToPrint2,comments
			          ) "

		lsSQL = lsSQL & lsSQL1

		Dim lsSQL2 As String = "SELECT 
					     uo.[Location]
				        ,uo.UpgradeCategory
					     ,uo.UpgradeLevel + uo.ModelOrStyle as levstyle
					 ,uo.[Description]
					FROM tblUpgradeOptions uo
					    LEFT JOIN (Select * from tblRequestedUpgrades where quoteid=@QuoteID  and active = 1)  ru 
                              ON   uo.UnitTypeID      = ru.UnitTypeID 
                               and uo.[location]      = ru.RoomDescription 
                               and uo.UpgradeCategory = ru.UpgradeCategory
					           and uo.UpgradeLevel    = ru.UpgradeClass 
		            WHERE 1=1 
					    AND uo.UnitTypeID =  @UnitTypeID
                        AND RU.id is  null
					    AND uo.[Standard] = 1 
                        AND uo.BuildingPhase = @BuildingPhase"



		msSQLcmd = New SqlCommand
		'msSQLcmd.CommandText = lsSQL
		msSQLcmd.Parameters.Clear()
		msSQLcmd.Parameters.AddWithValue("@QuoteID", fTakeOutQuotes(lsQuoteID))
		msSQLcmd.Parameters.AddWithValue("@CurrentUser", fTakeOutQuotes(lsCurrentUser))
		msSQLcmd.Parameters.AddWithValue("@NodeID", fTakeOutQuotes(llNodeID))
		msSQLcmd.Parameters.AddWithValue("@UnitTypeID", fTakeOutQuotes(lsUnitTypeID))
		msSQLcmd.Parameters.AddWithValue("@BuildingPhase", fTakeOutQuotes(liBuildingPHase))

		'*** Retreive the dataset containing the records that will be inserted.  Then Return those for messaging
		msSQLcmd.CommandText = lsSQL2
		Dim c As New Collection
		c.Add(msSQLcmd)
		ds = New DataSet
		msg = ""
		fgetSQLCommandDataSet("SQLConnection", lscnStr, c, ds, msg)
		If ds.Tables(0).Rows.Count = 0 Then
			msg = "There are no AutoPick items for categories that have not been picked."
		Else
			msg = "There were " & ds.Tables(0).Rows.Count & " Upgrade Items AutoPicked"
		End If

		'*** Run The SQL.
		msSQLcmd.CommandText = lsSQL
		InsertAutoPick = fExecuteSQLCmd("SQLConnection", lscnStr, msSQLcmd)
		If InsertAutoPick = True Then
			dt = ds.Tables(0)
		Else
			msg = "There was a problem with the error picking process"
		End If
		lsmessage = msg
	End Function
	Public Function UpdateCustomers(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsAddress As String,
									ByVal lsCity As String, ByVal lsActive As String,
									lsState As String, lsPostalCode As String, lsEmail As String,
									lsPhone As String, lsCustomerCountry As String,
									ByVal ObjectID As String) As Boolean
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		If UCase(lsActive) = "TRUE" Then
			lsActive = "1"
		Else
			lsActive = "0"
		End If
		'*** Initialize
		UpdateCustomers = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			Exit Function
		End If
		lsSQL = "Update tblCustomers  " &
				"Set CustomerName = @CustomerName, CustomerAddress = @CustomerAddress , CustomerCity= @CustomerCity , " & NL &
						" CustomerState= @CustomerState , CustomerPostalCode= @CustomerPostalCode ," & NL &
						" CustomerEmail= @CustomerEmail , CustomerPhone= @CustomerPhone , CustomerCountry= @CustomerCountry , " & NL &
						" UpdateDate= GetDate() , UpdateUser= @CurrentUser , " & NL &
						" Active = @CustomerActive " & NL &
				 " Where ID= @ObjectID ;"
		msSQLcmd = New SqlCommand
		msSQLcmd.CommandText = lsSQL
		msSQLcmd.Parameters.Clear()
		msSQLcmd.Parameters.AddWithValue("@ObjectID", fTakeOutQuotes(ObjectID.ToString))
		msSQLcmd.Parameters.AddWithValue("@CustomerName", fTakeOutQuotes(lsName))
		msSQLcmd.Parameters.AddWithValue("@CustomerAddress", fTakeOutQuotes(lsAddress))
		msSQLcmd.Parameters.AddWithValue("@CustomerCity", fTakeOutQuotes(lsCity))
		msSQLcmd.Parameters.AddWithValue("@CustomerState", fTakeOutQuotes(lsState))
		msSQLcmd.Parameters.AddWithValue("@CustomerPostalCode", fTakeOutQuotes(lsPostalCode))
		msSQLcmd.Parameters.AddWithValue("@CustomerEmail", fTakeOutQuotes(lsEmail))
		msSQLcmd.Parameters.AddWithValue("@CustomerPhone", fTakeOutQuotes(lsPhone))
		msSQLcmd.Parameters.AddWithValue("@CurrentUser", fTakeOutQuotes(lsCurrentUser))
		msSQLcmd.Parameters.AddWithValue("@CustomerNode", fTakeOutQuotes(llNodeID))
		msSQLcmd.Parameters.AddWithValue("@CustomerActive", fTakeOutQuotes(lsActive))
		msSQLcmd.Parameters.AddWithValue("@CustomerCountry", fTakeOutQuotes(lsCustomerCountry))
		'*** Run The SQL.
		UpdateCustomers = fExecuteSQLCmd("SQLConnection", lscnStr, msSQLcmd)
		'MyCommand = New SqlCommand("UPDATE SeansMessage SET Message = @TicBoxText WHERE Number = 1", dbConn)
		'MyCommand.Parameters.AddWithValue("@TicBoxText", TicBoxText.Text)
	End Function
	'*******************************************************
	'*** Contacts
	'*******************************************************
	Public Function LoadContacts(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As String, lsObjectID As String) As DataSet
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		'*** Initialize
		LoadContacts = Nothing
		'	llNodeID = 2
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "Select (convert(nvarchar(50), f.ID)) as ID , f.ObjectID as ParentID 
                , F.FirstName, f.LastName, f.FullAddress, f.CIty, f.StateProvince, f.PostalCode, f.Country
                , isnull( f.Phone1,'') as Phone1, isnull(f.Phone2,'') as Phone2, f.Email1, f.Email2, f.NodeID, f.Type, f.Active, f.CreateDate, f.CreateUser, f.UpdateDate, f.UpdateUser
                , I.imageURL  , uc.username CreateUserName, uu.username UpdateUserName  " & NL &
				"FROM tblContacts F     Left Join tblImages I on f.ID = I.OBJECTID 
                                        Left Join aspnetUsers as uc On f.createuser = convert(varchar(40), uc.ID )
                                        Left Join aspnetUsers as uu on f.UpdateUser = convert(varchar(40), uu.ID ) " & NL &
				"WHERE  ( f.NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " And " & lsWhere, "") &
					 IIf(lbActive = True, " And f.Active = 1 ", "") & NL &
		IIf(lsObjectID = "", "", " And f.ObjectID ='" & lsObjectID & "'") & ")" & NL & ""
		'IIf(llID > 0, " Or ID = '" & llID & "' ", "")


		'*** Load a data set.
		Dim ds As New DataSet()
		' ds = fGetDataset(mscnType, lscnStr, lsSQL, "Contacts")
		ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "Contacts")
		mdsContacts = ds
		LoadContacts = mdsContacts
	End Function
	Public Function DeleteContacts(ByVal RecordID As String, llNodeID As Long) As Boolean
		Dim lscnStr As String = mscnDefault
		Dim lsSQL As String = "Update tblContacts Set Active=0   WHERE  NodeID=" & llNodeID & " AND ID = '" & RecordID & "'"
		DeleteContacts = fRunSQL("SQLConnection", lscnStr, lsSQL)
	End Function
	Public Function InsertContacts(ByVal llNodeID As Long, ByVal lsFirstName As String, ByVal lsLastName As String, lsObjectID As String,
								   ByVal lsAddress As String, ByVal lsCity As String, ByVal lsState As String, ByVal lsPostalCode As String,
								   ByVal lsCountry As String,
								   ByVal lsPhone1 As String, ByVal lsPhone2 As String, ByVal lsEmail1 As String, ByVal lsEmail2 As String,
								   ByVal lsContactType As String) As Boolean
		'InsertContacts' that has parameters: llNodeID, lsObjectID, lsFirstName, lsLastName, lsAddress, lsCity, lsState, lsPostalCode,
		'lsCountry, lsPhone1, 
		'lsPhone2, lsEmail1, lsEmail2, lsContactType, Phone, City, email, ID, LastName, FirstName, State.
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertContacts = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "INSERT INTO tblContacts ( FirstName, LastName,  Type, ObjectID, " &
				 "                        FullAddress, City, StateProvince, PostalCode, Country, Phone1, Phone2, Email1, Email2, " &
				"                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
				"Values ( '" & fTakeOutQuotes(lsFirstName) & "', " & NL &
						  "'" & fTakeOutQuotes(lsLastName) & "', " & NL &
						  "'" & fTakeOutQuotes(lsContactType) & "', " & NL &
						  "'" & fTakeOutQuotes(lsObjectID) & "', " & NL &
						  "'" & fTakeOutQuotes(lsAddress) & "', " & NL &
						  "'" & fTakeOutQuotes(lsCity) & "', " & NL &
						  "'" & fTakeOutQuotes(lsState) & "', " & NL &
						  "'" & fTakeOutQuotes(lsPostalCode) & "', " & NL &
						  "'" & fTakeOutQuotes(lsCountry) & "', " & NL &
						  "'" & fTakeOutQuotes(lsPhone1) & "', " & NL &
						  "'" & fTakeOutQuotes(lsPhone2) & "', " & NL &
						  "'" & fTakeOutQuotes(lsEmail1) & "', " & NL &
						  "'" & fTakeOutQuotes(lsEmail2) & "', " & NL &
						  "'" & Now.ToString & "', " & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
						  "'" & Now.ToString & "', " & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
						   "1, '" & llNodeID & "'); "
		'*** Run The SQL.
		InsertContacts = fRunSQL("SQLConnection", lscnStr, lsSQL)
	End Function
	Public Function UpdateContacts(ID As String, ParentID As String, FirstName As String, LastName As String, FullAddress As String, City As String, StateProvince As String,
	 PostalCode As String, Country As String, Phone2 As String, Phone1 As String, Email1 As String, Email2 As String, NodeID As Integer, Type As String, Active As Boolean,
	ContactType As String, llNodeID As Long, ImageURL As String) As Boolean
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		UpdateContacts = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			Exit Function
		End If
		lsSQL = "Update tblContacts  " &
				"Set FirstName = @FirstName, LastName=@LastName, FullAddress = @FullAddress , City= @City , " & NL &
						" StateProvince = @StateProvince , PostalCode= @PostalCode ," & NL &
						" Email1= @Email1 , Phone1= @Phone1 , Email2= @Email2 , Phone2= @Phone2 , Country= @Country , " & NL &
						" UpdateDate= GetDate() , UpdateUser= @CurrentUser , " & NL &
						" Active = @Active , Type=@ContactType  , NodeID=@NodeID " & NL &
				 " Where ID= @ObjectID ;"
		msSQLcmd = New SqlCommand
		msSQLcmd.CommandText = lsSQL
		msSQLcmd.Parameters.Clear()
		msSQLcmd.Parameters.AddWithValue("@ObjectID", fTakeOutQuotes(ID.ToString))
		msSQLcmd.Parameters.AddWithValue("@FirstName", fTakeOutQuotes(FirstName))
		msSQLcmd.Parameters.AddWithValue("@LastName", fTakeOutQuotes(LastName))
		msSQLcmd.Parameters.AddWithValue("@FullAddress", fTakeOutQuotes(FullAddress))
		msSQLcmd.Parameters.AddWithValue("@City", fTakeOutQuotes(City))
		msSQLcmd.Parameters.AddWithValue("@StateProvince", fTakeOutQuotes(StateProvince))
		msSQLcmd.Parameters.AddWithValue("@PostalCode", fTakeOutQuotes(PostalCode))
		msSQLcmd.Parameters.AddWithValue("@Email1", fTakeOutQuotes(Email1))
		msSQLcmd.Parameters.AddWithValue("@Phone1", fTakeOutQuotes(Phone1))
		msSQLcmd.Parameters.AddWithValue("@Email2", fTakeOutQuotes(Email2))
		msSQLcmd.Parameters.AddWithValue("@Phone2", fTakeOutQuotes(Phone2))
		msSQLcmd.Parameters.AddWithValue("@CurrentUser", fTakeOutQuotes(lsCurrentUser))
		msSQLcmd.Parameters.AddWithValue("@NodeID", fTakeOutQuotes(NodeID))
		msSQLcmd.Parameters.AddWithValue("@ContactType", fTakeOutQuotes(ContactType))
		msSQLcmd.Parameters.AddWithValue("@Active", fTakeOutQuotes(Active))
		msSQLcmd.Parameters.AddWithValue("@Country", fTakeOutQuotes(Country))
		'*** Run The SQL.
		UpdateContacts = fExecuteSQLCmd("SQLConnection", lscnStr, msSQLcmd)
	End Function
	'*******************************************************
	'*** Personal Data - Like Last Places
	'*******************************************************
	Public Function LoadPersonalData(lsUsageType As String, ByVal liNumRecs As Integer, ByVal lsObjectID As String) As DataSet
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		Dim lsUserID = fGetUser()
		'*** Initialize
		LoadPersonalData = Nothing
		If Not isGUID(lsUserID) Then
			Exit Function
		End If
		If liNumRecs <= 0 Then liNumRecs = 1

		lsSQL = "SELECT top " & Int(liNumRecs) & " (convert(nvarchar(50), guidvalue)) as ""guidvalue"" , textvalue, usagetype, userid from tblPersonalData" &
				 " WHERE userid='" & lsUserID & "' " &
				 IIf(lsUsageType = "", "", " and UsageType ='" & lsUsageType & "' ") &
				"Order By CreateDate desc"


		'*** Load a data set.
		Dim ds As New DataSet()
		' ds = fGetDataset(mscnType, lscnStr, lsSQL, "Contacts")
		ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "PersonalData")
		mdsContacts = ds
		LoadPersonalData = mdsContacts
	End Function
	Public Function DeletePersonalData(ByVal lsUsageType As String) As Boolean
		Dim lscnStr As String = mscnDefault
		Dim lsSQL As String = "delete * from tblPersonalData where UserID = '" & fGetUser() & "' " &
			IIf(lsUsageType = "", "", " and usagetype = '" & lsUsageType & "'")

		DeletePersonalData = fRunSQL("SQLConnection", lscnStr, lsSQL)
	End Function
	Public Function InsertPersonalData(ByVal lsUsageType As String, lsGuidValue As String, ByVal lsTextValue As String) As Boolean

		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertPersonalData = False
		'*** Check for required data
		If lsCurrentUser = "" Or lsUsageType = "" Then
			'response.write("No Project Selectedd")
			InsertPersonalData = False
			Exit Function
		End If
		lsSQL = "INSERT INTO tblPersonalData ( userid, usagetype,textvalue,guidvalue,createdate) " &
				"Values ( '" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
						  "'" & fTakeOutQuotes(lsUsageType) & "', " & NL &
						  "'" & fTakeOutQuotes(lsTextValue) & "', " & NL &
						  "'" & fTakeOutQuotes(lsGuidValue) & "', " & NL &
						  "'" & Now.ToString & "') " & NL

		'*** Run The SQL.
		InsertPersonalData = fRunSQL("SQLConnection", lscnStr, lsSQL)
	End Function
	'****************************************************
	'*** Rooms
	Public Function LoadRooms(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As String) As DataSet
		Dim lsSQL As String

		'*** Initialize
		LoadRooms = Nothing
		If lsWhere = Nothing Then lsWhere = ""
		lsWhere.Replace("like '%%'", "")
		If InStr(lsWhere, "'%%'") > 0 Then lsWhere = ""
		lsWhere = Replace(lsWhere, "SearchText", " UPPER( isnull(r.code,'') + isnull(r.name ,'') + isnull( r.description ,'')   ) ")
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "SELECT * " & NL &
				"FROM tblRooms as R " & NL &
				"WHERE  NodeID=" & llNodeID & " " &
					IIf(lsWhere.Length > 4, " and " & lsWhere, "") & NL &
					IIf(lbActive = True, " and Active = 1 ", "") & NL &
					IIf(isGUID(lsID), " and ID = '" & lsID & "' ", "")


		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "Rooms")

		mdsRooms = ds
		LoadRooms = mdsRooms
	End Function
	'****************************************************
	'*** Quote Phases Rooms
	Public Function LoadPhases(ByVal lsID As String) As DataSet
		Dim lsSQL As String
		If isGUID(lsID) = False Then lsID = "00000000-0000-0000-0000-000000000000"

		'*** Initialize
		LoadPhases = Nothing
		lsSQL = "SELECT * " &
				"FROM tblProjectPhases " &
		"WHERE ObjectID='" & lsID & "' " & " Order By SortOrder"


		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "Rooms")

		'mdsPHases = ds
		LoadPhases = ds ' mdsPhases
	End Function
	Public Function DeletePhases(ByVal RecordID As String, llNodeID As Long) As Boolean
		Dim lscnStr As String = mscnDefault
		Dim lsSQL As String = "Update tblProjectPhases Set Active=0   WHERE  NodeID=" & llNodeID & " AND ID = '" & RecordID & "'"
		DeletePhases = fRunSQL("SQLConnection", lscnStr, lsSQL)
	End Function
	Public Function InsertPhases(ByVal lsObjectID As String, ByVal llNodeID As Long, ByVal lsName As String, lsDescription As String, ByVal lsImage As String, ByVal lsProjectType As String, lsCode As String, ByRef lsMessage As String, ByVal liSortOrder As Int16) As Boolean
		Dim lsSQL As String
		Dim ds As DataSet
		Dim lscnStr As String = mscnDefault
		Dim lsID As String = Guid.NewGuid.ToString
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertPhases = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			lsMessage = ("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "Select * from tblProjectPhases where code='" & lsCode & "' and ObjectID = '" & lsObjectID & "'"
		ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "Phases")
		If ds.Tables("Phases").Rows.Count > 0 Then
			InsertPhases = False
			lsMessage = "Phase already Exists!"
			Exit Function
		End If

		lsSQL = "INSERT INTO tblProjectPhases ( ID,Name, Code, Description, objectID, SortOrder, " &
				"                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
				"Values ( '" & lsID & "', '" & fTakeOutQuotes(lsName) & "', " & NL &
						  "'" & fTakeOutQuotes(lsCode) & "', " & NL &
						  "'" & fTakeOutQuotes(lsDescription) & "', " & NL &
						  "'" & fTakeOutQuotes(lsObjectID) & "', " & NL &
						  "" & (liSortOrder) & ", " & NL &
						  " getdate(), " & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
						  " getdate(), " & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
						   "1, '" & llNodeID & "'); "
		'*** Run The SQL.
		InsertPhases = fRunSQL("SQLConnection", lscnStr, lsSQL)
	End Function
	Public Function UpdatePhases(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsDescription As String, ByVal lsImage As String, lsProjectType As String, ByVal lsActive As String, ByVal ID As String, lsCode As String, lsPhaseStatus As String, ByVal ldPhaseTargetDate As Date, ByVal ldPhaseCompleteDate As Date, ByVal lslevel As String, ByVal liSortOrder As Int16) As Boolean
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		UpdatePhases = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			Exit Function
		End If
		lsSQL = "Update tblProjectPhases  " &
				"Set " & If(lsName = "", "", "Name = '" & fTakeOutQuotes(lsName) & "', ") &
					If(lsDescription = "", "", "Description = " & NL & "'" & fTakeOutQuotes(lsDescription) & "', ") &
					If(lsCode = "", "", "CODE= " & NL & "'" & fTakeOutQuotes(lsCode) & "', ") &
					If(liSortOrder = 0, "", "SortOrder= " & NL & "" & liSortOrder & ", ")

		If lslevel = "Quote" Then
			lsSQL = lsSQL & "PhaseStatus= " & NL & "'" & fTakeOutQuotes(lsPhaseStatus) & "', "
			If ldPhaseTargetDate > Now.AddDays(-10000) Then lsSQL = lsSQL & "  PhaseTargetDate= " & NL & "'" & fTakeOutQuotes(Format(ldPhaseTargetDate, "yyyy-MM-dd")) & "', "
			If ldPhaseCompleteDate > Now.AddDays(-10000) Then lsSQL = lsSQL & " PhaseCompleteDate= " & NL & "'" & fTakeOutQuotes(Format(ldPhaseCompleteDate, "yyyy-MM-dd")) & "', "
			lsSQL = lsSQL & " image= " & NL & "'" & fTakeOutQuotes(lsImage) & "', "
		End If
			lsSQL = lsSQL & "UpdateDate=" & NL & "getdate(), " &
						"UpdateUser=" & NL & "'" & fTakeOutQuotes(lsCurrentUser) & "', " &
						"Active = " & If(UCase(lsActive) = "TRUE", "1", "0") & " " & NL &
						" Where ID='" & ID & "';"
		'*** Run The SQL.
		UpdatePhases = fRunSQL("SQLConnection", lscnStr, lsSQL)
	End Function

    '****************************************************
    '*** Quote Rooms
    Public Function LoadQuoteRooms(ByVal lsID As String, ByVal llNodeID As Long, liPhase As Integer) As DataSet
        Dim lsSQL As String
        If isGUID(lsID) = False Then lsID = "00000000-0000-0000-0000-000000000000"

        '*** Initialize
        LoadQuoteRooms = Nothing
        lsSQL = "SELECT tblUnitProfiles.ID UnitProfileID, tblRooms.ID roomid, tblUnitProfiles.ID UnitTypeID
                      , tblRooms.Name as RoomName, tblRooms.Description as RoomDescription
                      , tblUnits.UnitCode, tblUnitProfiles.Active 
                      , concat(rtrim(tblRooms.Description) ,' ',''
                           ,  case when count(R.ID)>0 then 
					         CONCAT('<span class=""badge badge-primary"">',lTrim(str(Count(R.ID))),'</span>') else ' ' END
                       ) UpgradeCount 
					, count(r.id) ucount 
                 FROM (tblUnitTypes " &
                "   INNER JOIN (tblUnitProfiles " &
                "   INNER JOIN tblRooms ON tblUnitProfiles.RoomID = tblRooms.ID) " &
                "   ON tblUnitTypes.ID = tblUnitProfiles.UnitTypeID) " &
                "   INNER JOIN tblUnits ON tblUnitTypes.ID = tblUnits.UnitTypeID " &
                "   INNER JOIN tblQuote ON tblQUote.UnitID = tblUnits.ID " &
                "   LEFT  JOIN (Select ru.* from  tblRequestedUpgrades ru where ru.active=1 and ru.buildingphase = " & liPhase & " and QuoteID='" & lsID & "') R ON tblQUote.ID = R.QuoteID and tblRooms.Name = r.RoomDescription " &
                "WHERE tblQuote.ID='" & lsID & "' and tblQuote.NodeID = " & llNodeID & " " &
                "Group BY tblUnitProfiles.ID, tblRooms.ID, tblUnitProfiles.ID, tblRooms.Name, tblRooms.Description, tblUnits.UnitCode, tblUnitProfiles.Active ORDER BY tblRooms.Name"


        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Rooms")

        mdsRooms = ds
        LoadQuoteRooms = mdsRooms
    End Function
    '****************************************************
    '*** Unit Rooms
    Public Function LoadUnitRooms(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As String) As DataSet
		Dim lsSQL As String
		If isGUID(lsID) = False Then lsID = "00000000-0000-0000-0000-000000000000"

		'*** Initialize
		LoadUnitRooms = Nothing
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "SELECT tblUnitProfiles.ID UnitProfileID, tblRooms.ID roomid, tblUnitProfiles.ID UnitTypeID, " &
				"tblRooms.Name RoomName, tblRooms.Description RoomDescription, tblUnits.UnitCode, tblUnitProfiles.Active " &
				"FROM (tblUnitTypes " &
				"   INNER JOIN (tblUnitProfiles " &
				"   INNER JOIN tblRooms ON tblUnitProfiles.RoomID = tblRooms.ID) " &
				"   ON tblUnitTypes.ID = tblUnitProfiles.UnitTypeID) " &
				"   INNER JOIN tblUnits ON tblUnitTypes.ID = tblUnits.UnitTypeID " &
				"WHERE tblUnits.ID='" & lsID & "' " &
					IIf(lsWhere.Length > 4, " and " & lsWhere, "") & NL &
					IIf(lbActive = True, " and tblUnitProfiles.Active = 1 ", "") & NL &
					" Order By tblRooms.Name"


		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "Rooms")

		mdsRooms = ds
		LoadUnitRooms = mdsRooms
	End Function

    '****************************************************
    '*** Room Categories
    Public Function LoadRoomCategories(ByVal llNodeID As Long, ByVal lsUnitType As String, ByVal lsPhase As String, ByVal lsRoom As String, ByVal lsQuoteID As String) As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadRoomCategories = Nothing
        If llNodeID = 0 Or lsUnitType = "" Then Exit Function


        '  lsSQL = "SELECT D.UNITTYPEID, C.Name UpgradeCategory, D.Location , D.REQUIRED, R.ID
        '                 , concat(rtrim(c.categorygrouping),' ',rtrim(C.Name) ,' ', CASE WHEN D.REQUIRED=1 or C.CATREQUIRED=1 THEN '<span class=""badge badge-' + CASE WHEN R.ID is NULL THEN 'danger' ELSE 'success' END + '"">R</span>' ELSE '' END ) CATEGORYNAME
        '           FROM    TBLUPGRADECATEGORIES C Inner join tblupgradecategorydetails D on C.ID = D.CATEGORYID
        '                   LEFT JOIN tblRequestedUpgrades R on D.ID = R.OPTIONID and R.QUOTEID = '" & lsQuoteID & "' AND R.ACTIVE=1
        '           WHERE C.Active = 1 AND D.ACTIVE=1
        '             AND D.UnitTypeID='" & lsUnitType & "' 
        '             AND D.BuildingPhase=" & lsPhase & " 
        '             AND D.Location='" & lsRoom & "'
        '           ORDER BY C.SORTORDER, D.SORTORDER, C.name;"

        '  lsSQL = "SELECT D.UNITTYPEID, C.Name UpgradeCategory, D.Location , D.REQUIRED, C.CATREQUIRED, B.CATEGORYDETAILID
        '                 , concat(rtrim(c.categorygrouping),' ',rtrim(C.Name) ,' ', CASE 
        '  WHEN D.REQUIRED=1 or C.CATREQUIRED=1 
        '  THEN '<span class=""badge badge-' + CASE WHEN B.thecount is NULL 	THEN 'danger' ELSE 'success' 	END 	+ '"">R</span>' ELSE '' END ) CATEGORYNAME
        '           FROM    TBLUPGRADECATEGORIES C Inner join tblupgradecategorydetails D on C.ID = D.CATEGORYID
        '            LEFT JOIN (
        'SELECT CATEGORYDETAILID, COUNT(*) as thecount
        '           FROM TBLUPGRADEOPTIONS O
        '		       INNER JOIN  tblRequestedUpgrades R on O.ID = R.OPTIONID 
        '			       and R.QUOTEID = '" & lsQuoteID & "' 
        '				   AND R.ACTIVE=1
        '		   WHERE 1=1 
        '		      AND O.ACTIVE=1  
        '              AND O.UnitTypeID='" & lsUnitType & "' 
        '                                AND O.BuildingPhase=" & lsPhase & "  
        '                                AND O.Location='" & lsRoom & "'
        '	GROUP BY CATEGORYDETAILID
        '  ) as B on D.ID = B.CATEGORYDETAILID
        ' WHERE C.Active = 1 AND D.ACTIVE=1
        '             AND D.UnitTypeID='" & lsUnitType & "' 
        '             AND D.BuildingPhase=" & lsPhase & " 
        '             AND D.Location='" & lsRoom & "'
        '           ORDER BY C.SORTORDER, D.SORTORDER, C.name;"


        lsSQL = "Select * From f_LoadRoomCategories('" & lsUnitType & "', '" & lsRoom & "', '" & lsQuoteID & "', " & lsPhase & ", 0) ORDER BY CATEGORYNAME"
        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Categories")

        mdsRooms = ds
        LoadRoomCategories = mdsRooms
    End Function
    '****************************************************
    '*** Room Categories
    Public Function LoadUnfullfilledRequiredItems(ByVal lsPhase As String, ByVal lsRoom As String, ByVal lsQuoteID As String) As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadUnfullfilledRequiredItems = Nothing
        If lsQuoteID = "" Then Exit Function

        lsSQL = "Select * From f_LoadRoomCategories(
				(Select u.UnitTypeID from tblQuote q inner join tblunits u on u.id = q.unitid
				where q.ID = '" & lsQuoteID & "') 
            , '" & lsRoom & "', '" & lsQuoteID & "', " & lsPhase & ", 1) ORDER BY CATEGORYNAME"
        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Categories")

        LoadUnfullfilledRequiredItems = ds
    End Function
    '****************************************************
    '*** Room Category Levels
    Public Function LoadRoomCategoryLevels(ByVal llNodeID As Long, ByVal lsUnitType As String, ByVal lsPhase As String, ByVal lsRoom As String, ByVal lsCategory As String) As DataSet
		Dim lsSQL As String

		'*** Initialize
		LoadRoomCategoryLevels = Nothing
		If llNodeID = 0 Or lsUnitType = "" Then Exit Function


		lsSQL = "SELECT DISTINCT  O.UpgradeLevel " &
				"FROM     tblUpgradeOptions as O " &
				"WHERE O.UnitTypeID = '" & lsUnitType & "' AND " &
				"      O.Location   = '" & lsRoom & "'     AND " &
				"      O.BuildingPhase= '" & lsPhase & "'  AND " &
				"   O.UpgradeCategory = '" & lsCategory & "' " &
				"ORDER BY O.UpgradeLevel;"



		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "Categories")

		mdsRooms = ds
		LoadRoomCategoryLevels = mdsRooms
	End Function

	'****************************************************
	'*** Room Category Level Styles
	Public Function LoadRoomCategoryLevelStyles(ByVal llNodeID As Long, ByVal lsUnitType As String, ByVal lsPhase As String, ByVal lsRoom As String, ByVal lsCategory As String, ByVal lsLevel As String, Optional ByVal llID As Long = 0) As DataSet
		Dim lsSQL As String

		'*** Initialize
		LoadRoomCategoryLevelStyles = Nothing
		If llID = 0 And (llNodeID = 0 Or lsUnitType = "") Then Exit Function

        lsSQL = "SELECT Distinct Description, ModelorStyle as Style, " &
                   "CustomerPrice, UnitType, UpgradeCategory, " &
                    "Location, UpgradeLevel, 0 as UpgradeOptionID, ID " &
                    "FROM tblUpgradeOptions " &
                    "Where "
        If llID > 0 Then
			lsSQL = lsSQL & " UpgradeOptionID = " & llID & ";"
		Else
			lsSQL = lsSQL & "( active=1 " &
					"    AND UnitTypeID='" & lsUnitType & "'  " &
					"    AND Location='" & lsRoom & "' " &
					"    AND UpgradeLevel='" & fReplaceQuotes(lsLevel) & "' " &
					"    AND UpgradeCategory = '" & fReplaceQuotes(lsCategory) & "');"

		End If




		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "Categories")
        'Dim liCount As Integer = ds.Tables(0).Rows.Count
        mdsRooms = ds
		LoadRoomCategoryLevelStyles = mdsRooms
	End Function

	Public Function DeleteRooms(ByVal RecordID As String, llNodeID As Long) As Boolean
		Dim lsSQL As String = "Update tblRooms Set Active=False  WHERE  NodeID = " & llNodeID & " ID='" & RecordID & "'"
		DeleteRooms = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function InsertRooms(ByVal llNodeID As Long, ByVal lsCode As String, ByVal lsName As String, ByVal lsDescription As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertRooms = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selected")
			Exit Function
		End If
		lsSQL = "INSERT INTO tblRooms ( ID, Code, Name, Description,  " &
				"                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
				"Values ( '" & fTakeOutQuotes(lsCode) & "', " & NL & "'" & fTakeOutQuotes(lsName) & "', " & NL &
						  "'" & fTakeOutQuotes(lsDescription) & "', " & NL &
						  "'" & Now.ToString & "', " & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
						" '" & Now.ToString & "', " & NL &
						 " '" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
						   "true, " & llNodeID & "); "
		'*** Run The SQL.
		InsertRooms = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function UpdateRooms(ByVal llNodeID As Long, ByVal lsCode As String, ByVal lsName As String, ByVal lsDescription As String, ByVal lsActive As String, ByVal lsID As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		UpdateRooms = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			Exit Function
		End If
		lsSQL = "Update tblRooms  " &
				"Set Name = '" & fTakeOutQuotes(lsName) & "', Code = " & NL &
						  "'" & fTakeOutQuotes(lsCode) & "', Description = " & NL &
						  "'" & fTakeOutQuotes(lsDescription) & "', UpdateDate= " & NL &
						  "'" & Now.ToString & "', UpdateUser=" & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', Active = " & If(UCase(lsActive) = "TRUE", "1", "0") & NL &
				 " Where ID='" & lsID & "';"
		'*** Run The SQL.
		UpdateRooms = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function


	'****************************************************
	'*** Unit Profiles
	Public Function LoadUnitProfiles(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As String) As DataSet
		Dim lsSQL As String

		'*** Initialize
		LoadUnitProfiles = Nothing
		If lsWhere = Nothing Then lsWhere = ""
		lsWhere.Replace("like '%%'", "")
		If InStr(lsWhere, "'%%'") > 0 Then lsWhere = ""
		lsWhere = Replace(lsWhere, "SearchText", " UPPER( isnull(t.unittypename,'') + isnull(r.name ,'') ) ")
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "SELECT U.*, T.UnitTypeName + ' ' + R.Name as NAME, T.UnitTypeName + ' ' + R.Name as CODE,T.UnitTypeName + ' ' + R.Name as DESCRIPTION " & NL &
				"FROM (tblUnitProfiles as U INNER JOIN tblUnitTypes as T on U.UnitTypeID = T.ID )  " & NL &
				"           INNER JOIN tblRooms as R on U.RoomID = R.ID   " & NL &
				"WHERE  U.NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") &
					IIf(lbActive = True, " and Active = 1 ", "") & " " & NL &
					IIf(lsID > 0, " OR ID = '" & lsID & "' ", "")


		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "UnitProfiles")

		mdsUnitProfiles = ds
		LoadUnitProfiles = mdsUnitProfiles
	End Function
	Public Function DeleteUnitProfiles(ByVal RecordID As String, llNodeID As Long) As Boolean
		Dim lsSQL As String = "Update tblUnitProfiles Set Active=False  WHERE NodeID=" & llNodeID & " AND ID='" & RecordID & "'"
		DeleteUnitProfiles = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function InsertUnitProfiles(ByVal llNodeID As Long, ByVal lsUnitTypeID As String, ByVal lsRoomID As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertUnitProfiles = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "INSERT INTO tblUnitProfiles ( UnitTypeID, RoomID,  " &
				"                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
				"Values ( '" & lsUnitTypeID & "', " & NL &
						  "'" & lsRoomID & "', " & NL &
						  "'" & Now.ToString & "', " & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
						  "'" & Now.ToString & "', " & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
						   "1, " & llNodeID & "); "
		'*** Run The SQL.
		InsertUnitProfiles = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function UpdateUnitProfiles(ByVal llNodeID As Long, ByVal UnitProfileID As String, ByVal lsUnitTypeID As String,
									   ByVal lsActive As String, ByVal lsRoomID As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		UpdateUnitProfiles = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			Exit Function
		End If
		lsSQL = "Update tblUnitProfiles  " &
				"Set RoomID = """ & fTakeOutQuotes(lsRoomID) & "', UnitTypeID = " & NL &
						  "'" & fTakeOutQuotes(lsUnitTypeID) & "', UpdateDate = " & NL &
						  "'" & Now.ToString & "', UpdateUser=" & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', Active = " & lsActive & NL &
				 " Where UnitProfileID'=" & UnitProfileID & "';"
		'*** Run The SQL.
		UpdateUnitProfiles = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function





	'****************************************************
	'*** Floors

	Public Function LoadFloors(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As String) As DataSet
		Dim lsSQL As String

		'*** Initialize
		LoadFloors = Nothing
		If lsWhere = Nothing Then
			lsWhere = ""
		Else
			lsWhere = Replace(lsWhere, "SearchText", " UPPER( isnull(f.code,'') + isnull(f.name,'') + isnull(l.Description,'') + isnull(f.Description,'') + isnull(cast(f.buildingLevel as varchar) ,'')   ) ")
		End If

		If lsWhere = "" Then
			lbActive = True
		Else : lbActive = False

		End If
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'llNodeID = 1
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "SELECT F.*, L.DESCRIPTION AS BuildingLevelText  " &
				"FROM tblFloors AS F LEFT JOIN (Select lookupid,  DESCRIPTION from tblLookups) AS L ON F.BuildingLevel = L.LookupID   " & NL &
				"WHERE ( F.NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") & NL &
					IIf(lbActive = True, " and F.Active = 1 ", "") & ")" & NL &
					IIf(lsID Is Nothing, "", IIf(lsID = "", "", " OR ID = '" & lsID & "' "))


		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "Floors")

		mdsFloors = ds
		LoadFloors = mdsFloors
	End Function
	Public Function DeleteFloors(ByVal RecordID As String, llNodeID As Long) As Boolean
		Dim lsSQL As String = "Update tblFloors Set Active=False  WHERE  NodeID=" & llNodeID & " AND ID = '" & RecordID & "'"
		DeleteFloors = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function InsertFloors(ByVal llNodeID As Long, ByVal lsFloorName As String, ByVal lsFloorDescription As String, ByVal lsBuildinglevel As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertFloors = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "INSERT INTO tblFloors ( FloorName, FloorDescription, Buildinglevel, " &
				"                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
				"Values ( """ & fTakeOutQuotes(lsFloorName) & """, " & NL &
						  """" & fTakeOutQuotes(lsFloorDescription) & """, " & NL &
						  """" & fTakeOutQuotes(lsBuildinglevel) & """, " & NL &
						  """" & Now.ToString & """, " & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
						  """" & Now.ToString & """, " & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
						   "true, " & llNodeID & "); "
		'*** Run The SQL.
		InsertFloors = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function UpdateFloors(ByVal llNodeID As Long, ByVal lsFloorName As String,
								 ByVal lsFloorDescription As String, ByVal lsBuildingLevel As String,
								ByVal lsActive As String, ByVal FloorID As String) As Boolean
		'*** DataKey from the table must be listed in the Parameters exactly as it appears in the Properties
		'*** of the GridView 
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		UpdateFloors = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			Exit Function
		End If
		lsSQL = "Update tblFloors  " &
			   "Set FloorName = """ & fTakeOutQuotes(lsFloorName) & """, FloorDescription = " & NL &
						"""" & fTakeOutQuotes(lsFloorDescription) & """, BuildingLevel= " & NL &
						 fTakeOutQuotes(lsBuildingLevel) & ", UpdateDate=" & NL &
					  """" & Now.ToString & """, UpdateUser=" & NL &
					 """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL &
		   " Where FloorID=" & FloorID & ";"
		'*** Run The SQL.
		UpdateFloors = fRunSQL(mscnType, mscnStr, lsSQL)
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


	'****************************************************
	'*** Communications
	Public Function LoadCommunications(ByVal llNodeID As Long, lsWhere As String, lsObjectID As String,
								lsID As String, ByVal lbActive As Boolean) As DataSet
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		'  llNodeID = 1
		'*** Initialize
		LoadCommunications = Nothing
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			'Exit Function
		End If
		lsSQL = "Select      c.[ID]               ,    c.[CustomerID]       , " &
				"            c.[Category]         , " &
				"			 c.[Comments]         ,    c.[BuildingPhase]    , " &
				"            c.[UserName]         ,    c.[UpdateDate]       , " &
				"            c.[UpdateUser]       ,    c.[CreateDate]       , " &
				"           cu.[username] as cuname  ,   uu.[username] as uuname  , " &
				"            c.[CreateUser]       ,    c.[Active]           , " &
				"            c.[ObjectID]         ,    c.[NodeID] " &
				"FROM tblCommunications C " &
				"      left join  aspnetusers cu on c.createuser = cu.id " &
				"      left join  aspnetusers uu on c.updateuser = uu.id " &
				"WHERE (c.NodeID Is null Or c.NodeID=" & llNodeID & " ) " &
					IIf(lsWhere.Length > 4, " And " & lsWhere, "") & NL &
					IIf(isGUID(lsObjectID), " And c.objectID='" & lsObjectID & "'", "") & NL &
					IIf(lbActive = True, " And Active = 1 ", "") & NL &
					IIf(lsID = "", "", " And ID = '" & lsID & "' ") & " ORDER BY CREATEDate "

		'*** Load a data set.
		Dim ds As New DataSet()

		ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "Communications")

		mdsCommunications = ds
		LoadCommunications = mdsCommunications
	End Function
	Public Function DeleteCommunications(ByVal RecordID As String, llNodeID As Long) As Boolean
		Dim lsSQL As String = "Delete tblCommunications Set Active=False  WHERE  NodeID=" & llNodeID & " AND ID = '" & RecordID & "'"
		DeleteCommunications = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function InsertCommunications(ByVal llNodeID As Long, ByVal lsObjectID As String,
										 ByVal lsCategory As String, lsComments As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertCommunications = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		Dim lsID As String = Guid.NewGuid.ToString
		lsSQL = "INSERT INTO tblCommunications ( ID, ObjectID,  Category, Comments, " &
				"                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
				"Values (  '" & fTakeOutQuotes(lsID) & "', " & NL &
						  "'" & fTakeOutQuotes(lsObjectID) & "', " & NL &
						  "'" & fTakeOutQuotes(lsCategory) & "', " & NL &
						  "'" & fTakeOutQuotes(lsComments) & "', " & NL &
						  "Getdate(), " & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
						  "GetDate(), " & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
						   " 1, " & llNodeID & "); "
		'*** Run The SQL.
		InsertCommunications = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function


	'****************************************************
	'*** Adjustments
	Public Function LoadAdjustments(ByVal llNodeID As Long, lsWhere As String, lsObjectID As String,
								lsID As String, ByVal lbActive As Boolean) As DataSet
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		'  llNodeID = 1
		'*** Initialize
		LoadAdjustments = Nothing
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			' Exit Function
		End If
		lsSQL = "Select      c.[ID]               , c.NodeID,    " &
				"            c.[AdjustmentDate]         , c.adjustmentamount, " &
				"			 c.[AdjustmentReason]         ,    c.[BuildingPhase]    , " &
				"             c.[UpdateDate]       , " &
				"            c.[UpdateUser]       ,    c.[CreateDate]       , " &
				"           cu.[username] as cuname  ,   uu.[username] as uuname  , " &
				"            c.[CreateUser]       ,    c.[Active]           , " &
				"            c.[ObjectID]          " &
				"FROM tblAdjustments C " &
				"      left join  aspnetusers cu on c.createuser = cu.id " &
				"      left join  aspnetusers uu on c.updateuser = uu.id " &
				"WHERE (NodeID Is null Or NodeID=" & llNodeID & " ) " &
					IIf(lsWhere.Length > 4, " And " & lsWhere, "") & NL &
					IIf(isGUID(lsObjectID), " And c.objectID='" & lsObjectID & "'", "") & NL &
					IIf(lbActive = True, " And Active = 1 ", "") & NL &
					IIf(lsID = "", "", " And ID = '" & lsID & "' ") & " ORDER BY CREATEDate "

		'*** Load a data set.
		Dim ds As New DataSet()

		ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "Adjustments")

		mdsCommunications = ds
		LoadAdjustments = mdsCommunications
	End Function
	Public Function DeleteAdjustments(ByVal ID As String, llNodeID As Long) As Boolean
		Dim lsSQL As String = "Update tblAdjustments Set Active=0  WHERE  NodeID=" & llNodeID & " AND ID = '" & ID & "'"
		DeleteAdjustments = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function InsertAdjustments(ByVal llNodeID As Long, ByVal lsObjectID As String,
										 ByVal ldAdjustmentAmount As Double, lsAdjustmentReason As String,
									   lsBuildingPhase As String, ldAdjustmentDate As Date) As Boolean

		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertAdjustments = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		Dim lsID As String = Guid.NewGuid.ToString
		lsSQL = "INSERT INTO tblAdjustments ( ID, ObjectID,  AdjustmentAmount, AdjustmentDate, AdjustmentReason, BuildingPhase," &
				"                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
				"Values (  '" & fTakeOutQuotes(lsID) & "', " & NL &
						  "'" & fTakeOutQuotes(lsObjectID) & "', " & NL &
						  "" & (ldAdjustmentAmount) & ", " & NL &
									"'" & Format(ldAdjustmentDate, "yyyy-MM-dd") & "', " & NL &
						  "'" & fTakeOutQuotes(lsAdjustmentReason) & "', " & NL &
						  "'" & (lsBuildingPhase) & "', " & NL &
						  "Getdate(), " & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
						  "GetDate(), " & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
						   " 1, " & llNodeID & "); "
		'*** Run The SQL.
		InsertAdjustments = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function UpdateAdjustments(ByVal llNodeID As Long, ByVal lsID As String, ByVal lsObjectID As String, ByVal ldAdjustmentDate As Date,
								  ByVal lsAdjustmentReason As String, ByVal ldAdjustmentAmount As String, ByVal liSortOrder As Integer,
								  ByVal lsActive As String, ByVal liLookupID As Integer) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		UpdateAdjustments = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			Exit Function
		End If
		lsSQL = "Update tblAdjustments  " &
				"Set  AdjustmentDate= " & NL &
						"'" & fTakeOutQuotes(ldAdjustmentDate) & "', AdjustmentReason= " & NL &
						"'" & fTakeOutQuotes(lsAdjustmentReason) & "', AdjustmentAmount= " & NL &
						"" & fTakeOutQuotes(ldAdjustmentAmount) & ", ObjectID= " & NL &
						"'" & fTakeOutQuotes(lsObjectID) & "', NodeID= " & NL &
						"'" & fTakeOutQuotes(llNodeID) & "',  UpdateDate=" & NL &
						"'" & Now.ToString & "', UpdateUser=" & NL &
						"'" & fTakeOutQuotes(lsCurrentUser) & "', Active = " & If(UCase(lsActive) = "TRUE", 1, 0) & NL &
				 " Where ID='" & lsID & "';"
		'*** Run The SQL.
		UpdateAdjustments = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function


	'****************************************************
	'*** Payments
	Public Function LoadPayments(ByVal llNodeID As Long, lsWhere As String, lsObjectID As String,
								lsID As String, ByVal lbActive As Boolean) As DataSet
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		'  llNodeID = 1
		'*** Initialize
		LoadPayments = Nothing
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			' Exit Function
		End If
		lsSQL = "Select      c.[PaymentDueDate]   ,   c.ActualPaymentDate  ,     " &
				"            c.[PaymentDueAmount] ,   c.ActualPaymentAmount, " &
				"			 c.[CheckNumber]      ,    c.[BuildingPhase]   , " &
				"			 c.[PaymentType]      ,    c.[PaymentComment]   , " &
				"            c.[UpdateDate]      , " &
				"            c.[UpdateUser]       ,    c.[CreateDate]       , " &
				"           cu.[username] as cuname  ,   uu.[username] as uuname  , " &
				"            c.[CreateUser]       ,    c.[Active]           , " &
				"            c.[ObjectID]   , C.ID     , c.NodeID    " &
				"FROM tblPayments C " &
				"      left join  aspnetusers cu on c.createuser = cu.id " &
				"      left join  aspnetusers uu on c.updateuser = uu.id " &
				"WHERE (NodeID Is null Or NodeID=" & llNodeID & " ) " &
					IIf(lsWhere.Length > 4, " And " & lsWhere, "") & NL &
					IIf(isGUID(lsObjectID), " And c.objectID='" & lsObjectID & "'", "") & NL &
					IIf(lbActive = True, " And Active = 1 ", "") & NL &
					IIf(lsID = "", "", " And ID = '" & lsID & "' ") & " ORDER BY CREATEDate "

		'*** Load a data set.
		Dim ds As New DataSet()

		ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "Payments")

		mdsCommunications = ds
		LoadPayments = mdsCommunications
	End Function
	Public Function DeletePayments(ByVal RecordID As String, llNodeID As Long) As Boolean
		Dim lsSQL As String = "Update tblPayments Set Active=0  WHERE  NodeID=" & llNodeID & " AND ID = '" & RecordID & "'"
		DeletePayments = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function InsertPayments(ByVal llNodeID As Long, ByVal lsObjectID As String,
										 ByVal ldPaymentDueDate As Date, ByVal ldPaymentDueAmount As Double,
								   ByVal ldActualPaymentDate As Date, ByVal ldActualPaymentAmount As Double,
								   ByVal lsCheckNumber As String, ByVal lsPaymentComment As String,
								   ByVal lsBuildingPhase As String, lsPaymentType As String) As Boolean

		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertPayments = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		Dim lsID As String = Guid.NewGuid.ToString
		lsSQL = "INSERT INTO tblPayments ( ID, ObjectID,  PaymentDueDate, PaymentDueAmount, ActualPaymentDate, ActualPaymentAmount, " &
										   "CheckNumber,PaymentComment, BuildingPhase, PaymentType, " &
				"                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
				"Values (  '" & fTakeOutQuotes(lsID) & "', " & NL &
						  "'" & fTakeOutQuotes(lsObjectID) & "', " & NL &
						  "'" & Format(ldPaymentDueDate, "yyyy-MM-dd") & "', " & NL &
						  "" & ldPaymentDueAmount & ", " & NL &
						  "'" & Format(ldActualPaymentDate, "yyyy-MM-dd") & "', " & NL &
						  "" & ldActualPaymentAmount & ", " & NL &
						  "'" & fTakeOutQuotes(lsCheckNumber) & "', " & NL &
						  "'" & fTakeOutQuotes(lsPaymentComment) & "', " & NL &
						  "'" & (lsBuildingPhase) & "', " & NL &
						  "'" & fTakeOutQuotes(lsPaymentType) & "', " & NL &
						  "Getdate(), " & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
						  "GetDate(), " & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
						   " 1, " & llNodeID & "); "
		'*** Run The SQL.
		InsertPayments = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function UpdatePayments(ByVal lsID As String, ByVal llNodeID As Long, ByVal lsObjectID As String,
										 ByVal ldPaymentDueDate As Date, ByVal ldPaymentDueAmount As Double,
								   ByVal ldActualPaymentDueDate As Date, ByVal ldActualPaymentDueAmount As Double,
								   ByVal lsCheckNumber As String, ByVal lsPaymentComment As String,
								   ByVal lsBuildingPhase As String, lsPaymentType As String, ByVal lsActive As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		UpdatePayments = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			Exit Function
		End If
		lsSQL = "Update tblPayments  " &
				"Set  PaymentDueDate= " & NL &
						" '" & Format(ldPaymentDueDate, "yyyy-MM-dd") & "', PaymentDueAmount= " & NL &
						"  " & ldPaymentDueAmount & ", ActualPaymentDate=  '" & Format(ldActualPaymentDueDate) & "', " &
						" ActualPaymentAmount= " & ldActualPaymentDueAmount & ", " &
						" CheckNumber=    '" & fTakeOutQuotes(lsCheckNumber) & "' , " &
						" PaymentComment= '" & fTakeOutQuotes(lsPaymentComment) & "' , " &
						" BuildingPhase=   '" & lsBuildingPhase & "' , " &
						" PaymentType=    '" & fTakeOutQuotes(lsPaymentType) & "' , " &
						" ObjectID= " & NL &
						"'" & fTakeOutQuotes(lsObjectID) & "', NodeID= " & NL &
						"'" & fTakeOutQuotes(llNodeID) & "',  UpdateDate=" & NL &
						"'" & Now.ToString & "', UpdateUser=" & NL &
						"'" & fTakeOutQuotes(lsCurrentUser) & "', Active = " & If(UCase(lsActive) = "TRUE", 1, 0) & NL &
				 " Where ID='" & lsID & "';"
		'*** Run The SQL.
		UpdatePayments = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	'****************************************************
	'*** Lookups
	Public Function LoadLookups(ByVal llNodeID As Long, lsParentID As String, lsLookupID As String, ByVal lsWhere As String, ByVal lsLookupType As String,
								ByVal lbActive As Boolean, ByVal llID As Long) As DataSet
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		'  llNodeID = 1
		'*** Initialize
		LoadLookups = Nothing
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			' Exit Function
		End If
		lsSQL = "With LookupList as (
                Select a.NodeID, convert(nvarchar(50), a.ID) as ID , convert(nvarchar(50),  a.ParentID) as ParentID , a.LookupID, a.LookupValue, a.DESCRIPTION
                      , 1 as LookupLevel,active from tbllookups a where a.Parentid is null
	                  and lookupCategory = '" & fTakeOutQuotes(lsLookupType) & "'
                union All
                Select  b.NodeID, convert(nvarchar(50), b.ID) as ID, convert(nvarchar(50), b.ParentID) as ParentID, b.LookupID, b.LookupValue, b.DESCRIPTION
                      , a.LookupLevel + 1 as LookupLevel, a.active from LookupList a
                        Inner Join tbllookups b on a.id = b.parentid
                Where b.ParentID is not null
               ) Select * from LookupList " & NL &
				"WHERE (NodeID is null or NodeID=" & llNodeID & " ) " &
						IIf(lsWhere.Length > 4, " and " & lsWhere, "") & NL &
					IIf(isGUID(lsLookupID), " and ID=" & lsLookupID, "") & NL &
					IIf(lbActive = True, " and Active = 1 ", "") & NL &
					IIf(llID > 0, " and lookupID = " & llID & " ", "")

		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "Lookups")

		mdsLookups = ds
		LoadLookups = mdsLookups
	End Function

	Public Function DeleteLookups(ByVal RecordID As String, llNodeID As Long) As Boolean
		Dim lsSQL As String = "Update tblLookups Set Active=False  WHERE  NodeID=" & llNodeID & " AND ID = '" & RecordID & "'"
		DeleteLookups = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function InsertLookups(ByVal llNodeID As Long, ByVal lsID As String, ByVal lsParentID As String, ByVal lsLookupCategory As String,
                                 liSortOrder As Integer, lsDESCRIPTION As String, lsLookupValue As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertLookups = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'response.write("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "INSERT INTO tblVendors ( ParentID, LookupID, LookupCategory, LookupDecsription, LookupValue, SortOrder " &
                "                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
                "Values ( """ & fTakeOutQuotes(lsParentID) & """, " & NL &
                          "" & fTakeOutQuotes(lsID) & ", " & NL &
                          """" & fTakeOutQuotes(lsLookupCategory) & """, " & NL &
                          """" & fTakeOutQuotes(lsDESCRIPTION) & """, " & NL &
                          """" & fTakeOutQuotes(lsLookupValue) & """, " & NL &
                            "" & fTakeOutQuotes(liSortOrder) & ", " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                           "true, " & llNodeID & "); "
        '*** Run The SQL.
        InsertLookups = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function UpdateLookups(ByVal llNodeID As Long, ByVal lsID As String, ByVal lsParentID As String, ByVal lsLookupCategory As String,
                                  ByVal lsDESCRIPTION As String, ByVal lsLookupValue As String, ByVal liSortOrder As Integer,
                                  ByVal lsActive As String, ByVal liLookupID As Integer) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        UpdateLookups = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            Exit Function
        End If
        lsSQL = "Update tblLookups  " &
                "Set LookupID = " & fTakeOutQuotes(liLookupID) & ", ID = " & NL &
                        """" & fTakeOutQuotes(lsID) & """, LookupCategory= " & NL &
                        """" & fTakeOutQuotes(lsLookupCategory) & """, DESCRIPTION= " & NL &
                        """" & fTakeOutQuotes(lsDESCRIPTION) & """, LookupValue= " & NL &
                        """" & fTakeOutQuotes(lsLookupValue) & """, ParentID= " & NL &
                        """" & fTakeOutQuotes(lsParentID) & """, NodeID= " & NL &
                        """" & fTakeOutQuotes(llNodeID) & """,  UpdateDate=" & NL &
                        """" & Now.ToString & """, UpdateUser=" & NL &
                        """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL &
                 " Where ID=""" & lsID & """;"
        '*** Run The SQL.
        UpdateLookups = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function




	'****************************************************
	'*** Logins 
	Public Function LoadLogins(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As String) As DataSet
		Dim lsSQL As String

		'*** Initialize
		LoadLogins = Nothing
		If lsWhere = Nothing Then lsWhere = ""
		lsWhere.Replace("like '%%'", "")
		If InStr(lsWhere, "'%%'") > 0 Then lsWhere = ""
		lsWhere = Replace(lsWhere, "SearchText", " UPPER( isnull(U.name,'') + isnull(U.createuser ,'') + isnull( U.updateuser ,'')   ) ")
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "SELECT *, u.name as NAME, u.CODE, u.Description " & NL &
				"FROM tblLogins             U                      " & NL &
				"WHERE  NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " And " & lsWhere, "") &
					IIf(lbActive = True, " And Active = 1 ", "") & " " & NL &
					IIf(Not lsID Is Nothing, " Or ID = '" & lsID & "' ", "")


		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "frmLogins")

		mdsLogins = ds
		LoadLogins = mdsLogins
	End Function
	Public Function DeleteLogins(ByVal RecordID As Long, llNodeID As Long) As Boolean
		Dim lsSQL As String = "Update tblLogins Set Active=False  WHERE  NodeID=" & llNodeID & " AND ID = '" & RecordID & "'"
		DeleteLogins = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function InsertLogins(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsContact As String, ByVal lsAbbreviation As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertLogins = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "INSERT INTO tblVendors ( VendorName, VendorContact, VendorAbbreviation, " &
				"                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
				"Values ( """ & fTakeOutQuotes(lsName) & """, " & NL &
						  """" & fTakeOutQuotes(lsContact) & """, " & NL &
						  """" & fTakeOutQuotes(lsAbbreviation) & """, " & NL &
						  """" & Now.ToString & """, " & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
						  """" & Now.ToString & """, " & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
						   "true, " & llNodeID & "); "
		'*** Run The SQL.
		InsertLogins = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function UpdateLogins(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsContact As String, ByVal lsAbbreviation As String, ByVal lsActive As String, ByVal VendorID As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		UpdateLogins = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			Exit Function
		End If
		lsSQL = "Update tblVendors  " &
				"Set VendorName = """ & fTakeOutQuotes(lsName) & """, VendorContact = " & NL &
						  """" & fTakeOutQuotes(lsContact) & """, VendorAbbreviation= " & NL &
						  """" & fTakeOutQuotes(lsAbbreviation) & """, UpdateDate=" & NL &
						  """" & Now.ToString & """, UpdateUser=" & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL &
				 " Where VendorID=" & VendorID & ";"
		'*** Run The SQL.
		UpdateLogins = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function




	'****************************************************
	'***
	Public Function LoadEventLogs(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal llID As Long) As DataSet
		Dim lsSQL As String

		'*** Initialize
		LoadEventLogs = Nothing
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "SELECT * " & NL &
				"FROM tblEventLog                                   " & NL &
			   "WHERE  NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " And " & lsWhere, "") &
		 IIf(lbActive = True, " And Active = 1 ", "") & ")" & NL &
					IIf(llID > 0, " Or EventID = " & llID & " ", "")


		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "EventLogs")

		mdsEventLogs = ds
		LoadEventLogs = mdsEventLogs
	End Function
	Public Function DeleteEventLogs(ByVal RecordID As String, llnodeID As Long) As Boolean
		Dim lsSQL As String = "Update tblEventLog Set Active=False   WHERE  NodeID=" & llnodeID & " AND ID = '" & RecordID & "'"
		DeleteEventLogs = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function InsertEventLogs(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsContact As String, ByVal lsAbbreviation As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertEventLogs = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "INSERT INTO tblVendors ( VendorName, VendorContact, VendorAbbreviation, " &
				"                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
				"Values ( """ & fTakeOutQuotes(lsName) & """, " & NL &
						  """" & fTakeOutQuotes(lsContact) & """, " & NL &
						  """" & fTakeOutQuotes(lsAbbreviation) & """, " & NL &
						  """" & Now.ToString & """, " & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
						  """" & Now.ToString & """, " & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
						   "true, " & llNodeID & "); "
		'*** Run The SQL.
		InsertEventLogs = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function UpdateEventLogs(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsContact As String, ByVal lsAbbreviation As String, ByVal lsActive As String, ByVal VendorID As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		UpdateEventLogs = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			Exit Function
		End If
		lsSQL = "Update tblVendors  " &
				"Set VendorName = """ & fTakeOutQuotes(lsName) & """, VendorContact = " & NL &
						  """" & fTakeOutQuotes(lsContact) & """, VendorAbbreviation= " & NL &
						  """" & fTakeOutQuotes(lsAbbreviation) & """, UpdateDate=" & NL &
						  """" & Now.ToString & """, UpdateUser=" & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL &
				 " Where VendorID=" & VendorID & ";"
		'*** Run The SQL.
		UpdateEventLogs = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function

	'****************************************************
	'*** Deposit Conditions
	Public Function LoadDepositConditions(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal llID As Long) As DataSet
		Dim lsSQL As String
		'llNodeID = 1
		'*** Initialize
		LoadDepositConditions = Nothing
		If lsWhere = Nothing Then lsWhere = ""
		lsWhere.Replace("like '%%'", "")
		If InStr(lsWhere, "'%%'") > 0 Then lsWhere = ""
		lsWhere = Replace(lsWhere, "SearchText", " UPPER( isnull(D.code,'') + isnull(D.name ,'') + isnull( D.description ,'')   ) ")
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "SELECT * " & NL &
				"FROM  tblDepositConditions D   " & NL &
				"WHERE ( NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " And " & lsWhere, "") & NL &
IIf(lbActive = True, " And Active = 1 ", "") & ")" & NL &
IIf(llID > 0, " Or DepositTypeID = " & llID & " ", "")

		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "DepositConditions")

		mdsDepositConditions = ds
		LoadDepositConditions = mdsDepositConditions
	End Function
	Public Function DeleteDepositConditions(ByVal RecordID As String, llNodeID As Long) As Boolean
		Dim lsSQL As String = "Update tblDepositConditions Set Active=False   WHERE  NodeID=" & llNodeID & " AND ID = '" & RecordID & "'"
		DeleteDepositConditions = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function InsertDepositConditions(ByVal llNodeID As Long, ByVal lsUnitName As String, ByVal lsFloorID As String,
								ByVal lsUnitTypeID As String, ByVal lsAvailable As String,
								ByVal lsTier As String, ByVal lsDepositType As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertDepositConditions = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "INSERT INTO tblVendors ( UnitName, FloorID, UnitTypeID, Available, Tier, DepositType, " &
				"                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
				"Values ( """ & fTakeOutQuotes(lsUnitName) & """, " & NL &
						  "" & fTakeOutQuotes(lsFloorID) & ", " & NL &
						  """" & fTakeOutQuotes(lsUnitTypeID) & """, " & NL &
						  """" & fTakeOutQuotes(lsAvailable) & """, " & NL &
						  """" & fTakeOutQuotes(lsTier) & """, " & NL &
						  """" & fTakeOutQuotes(lsDepositType) & """, " & NL &
						  """" & Now.ToString & """, " & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
						  """" & Now.ToString & """, " & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
						   "true, " & llNodeID & "); "
		'*** Run The SQL.
		InsertDepositConditions = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function UpdateDepositConditions(ByVal UnitID As String, ByVal lsNodeID As String, ByVal lsUnitName As String, ByVal lsFloorID As String,
								ByVal lsUnitTypeID As String, ByVal lsActive As String, ByVal lsAvailable As String,
								ByVal lsTier As String, ByVal lsDepositType As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		UpdateDepositConditions = False
		'*** Check for No Selected Category
		If lsNodeID = 0 Then
			Exit Function
		End If

		If lsActive.ToUpper = "TRUE" Then lsActive = "1"
		If lsAvailable.ToUpper = "TRUE" Then lsAvailable = "1"

		lsSQL = "Update tblUnits  "
		lsSQL = lsSQL & "Set UnitName = '" & fTakeOutQuotes(lsUnitName) & "', Tier = " & NL
		lsSQL = lsSQL & """" & fTakeOutQuotes(lsTier) & """, FloorID = " & NL
		lsSQL = lsSQL & fTakeOutQuotes(lsFloorID) & ", UnitTypeID= " & NL
		lsSQL = lsSQL & """" & fTakeOutQuotes(lsUnitTypeID) & """, Available= " & NL
		lsSQL = lsSQL & fTakeOutQuotes(lsAvailable) & ", DepositType= " & NL
		lsSQL = lsSQL & """" & fTakeOutQuotes(lsDepositType) & """, UpdateDate=" & NL
		lsSQL = lsSQL & """" & Now.ToString & """, UpdateUser=" & NL
		lsSQL = lsSQL & """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL
		lsSQL = lsSQL & " , NodeID = " & lsNodeID & " " & NL
		lsSQL = lsSQL & " Where UnitID=" & UnitID & ";"
		'*** Run The SQL.
		UpdateDepositConditions = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function

	'****************************************************
	'*** Units
	Public Function LoadUnits(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsObjectID As String, lsID As String) As DataSet
		Dim lsSQL As String
		'llNodeID = 1
		'*** Initialize
		LoadUnits = Nothing
		If lsWhere = Nothing Then lsWhere = ""
		If lsID <> "" Then lsWhere = "" '***If there is an ID Passed, ignore where clause
		lsWhere.Replace("like '%%'", "")
		If InStr(lsWhere, "'%%'") > 0 Then lsWhere = ""
		lsWhere = Replace(lsWhere, "SearchText", " UPPER( isnull(U.code,'') + isnull(U.name ,'') + isnull( U.description ,'')   ) ")
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		'If isGUID(lsObjectID) = False Then lsObjectID = "11112222-3333-4444-5555-666677778888"

		lsSQL = "SELECT  D.DepositTypeName, F.Name as FloorName, Tier.Name as TierName, t.name as UnitTypeName, 
                U.* " & NL &
				"FROM (((( tblUnits  U  left join tblquote  q  on u.id= q.unitid              " & NL &
				"  LEFT JOIN tblUnitTypes  T         ON U.UnitTypeID = T.ID        )         " & NL &
				"  LEFT JOIN tblDepositConditions  D ON U.DepositTypeID = D.ID )  " & NL &
				"  LEFT JOIN tblFloors  F            ON U.FloorID = F.ID             )  " & NL &
				"  LEFT JOIN tblUnitTiers  Tier      ON U.TierID = Tier.ID        )  " & NL &
				"WHERE  U.NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") & NL &
				IIf(lbActive = True, " and U.Active = 1 ", "") & NL &
				IIf(isGUID(lsObjectID), " and u.ObjectID = '" & lsObjectID & "' ", " ") &
				IIf(lsID = "", "", " and u.ID = '" & lsID & "' ")

		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "Units")

		mdsUnits = ds
		LoadUnits = mdsUnits
	End Function
	Public Function DeleteUnits(ByVal RecordID As String, llNodeID As Long) As Boolean
		Dim lsSQL As String = "Update tblUnits Set Active=0  WHERE NodeID=" & llNodeID & " AND ID='" & RecordID & "'"
		DeleteUnits = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function InsertUnits(ByVal llNodeID As Long, lsUnitID As String, ByVal lsUnitName As String, ByVal lsFloorID As String,
								ByVal lsUnitTypeID As String, ByVal lsAvailable As String,
								ByVal lsTier As String, ByVal lsDepositType As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertUnits = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		If lsAvailable.ToUpper = "TRUE" Then lsAvailable = 1
		lsSQL = "INSERT INTO tblUnits ( ID, Name, FloorID, UnitTypeID, Available, TierID, DepositTypeID, 
                                        UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) 
                Values ( '" & fTakeOutQuotes(lsUnitID) & "', " &
				 "     '" & fTakeOutQuotes(lsUnitName) & "', " &
						   lsFloorID & ", " &
						   lsUnitTypeID & ", " &
						   lsAvailable & ", " &
						   lsTier & ", " &
						   lsDepositType & ", " &
						   "'" & Now.ToString & "', " &
						   "'" & fTakeOutQuotes(lsCurrentUser) & "', " &
						   "'" & Now.ToString & "', " &
						   "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
						   "1, " & llNodeID & "); "
		'*** Run The SQL.
		InsertUnits = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function

	Public Function UpdateUnits(ByVal llNodeID As Long, ByVal lsUnitName As String, ByVal lsFloorID As String,
							  ByVal lsUnitTypeID As String, ByVal lsAvailable As String, ByVal UnitID As String,
							  ByVal lsTier As String, ByVal lsDepositType As String, ByVal lsActive As String _
							   , lsID As String) As Boolean


		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		UpdateUnits = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			Exit Function
		End If
		If lsAvailable.ToUpper = "TRUE" Then
			lsAvailable = 1
		Else
			lsAvailable = "0"
		End If
		If lsActive.ToUpper = "TRUE" Then
			lsActive = 1
		Else
			lsActive = "0"
		End If
		lsSQL = "Update tblUnits  "
		lsSQL = lsSQL & "Set UnitName = '" & fTakeOutQuotes(lsUnitName) & "', TierID = " & NL
		lsSQL = lsSQL & lsTier & ", FloorID = " & NL
		lsSQL = lsSQL & lsFloorID & ", UnitTypeID= " & NL
		lsSQL = lsSQL & lsUnitTypeID & ", Available= " & NL
		lsSQL = lsSQL & lsAvailable & ", DepositTypeID= " & NL
		lsSQL = lsSQL & lsDepositType & ", UpdateDate=" & NL
		lsSQL = lsSQL & "'" & Now.ToString & "', UpdateUser=" & NL
		lsSQL = lsSQL & "'" & fTakeOutQuotes(lsCurrentUser) & "', Active = " & lsActive & NL
		lsSQL = lsSQL & " , NodeID = " & llNodeID & " " & NL
		lsSQL = lsSQL & " Where ID='" & lsID & "';"
		'*** Run The SQL.
		UpdateUnits = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function


	'****************************************************
	'*** Missing Upgrades
	Public Function LoadMissingUpgrades(ByVal lsQuoteID As String) As DataSet
		Dim lsSQL As String
		LoadMissingUpgrades = Nothing
		lsSQL = "    SELECT tempselections.BuildingPhase, tempselections.Location, tempselections.UpgradeCategory, " & NL &
				"           tempselections.Required, * " & NL &
				"    FROM tempselections " & NL &
				"    WHERE (tempselections.Required = 1 And tempselections.ValueExists = 0) " & NL &
				"           AND QuoteID = '" & lsQuoteID & "' " & NL &
				"    ORDER BY tempselections.BuildingPhase, tempselections.Location, tempselections.UpgradeCategory"


		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "Tiers")

		mdsTiers = ds
		LoadMissingUpgrades = mdsTiers
	End Function


	'****************************************************
	'*** Tiers
	Public Function LoadTiers(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As String) As DataSet
		Dim lsSQL As String
		'llNodeID = 1
		'*** Initialize
		LoadTiers = Nothing
		If lsWhere = Nothing Then lsWhere = ""
		lsWhere.Replace("like '%%'", "")
		If InStr(lsWhere, "'%%'") > 0 Then lsWhere = ""
		lsWhere = Replace(lsWhere, "SearchText", " UPPER( isnull(U.code,'') + isnull(U.name ,'') + isnull( U.description ,'')   ) ")
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "SELECT * " & NL &
				"FROM tblUnitTiers  U     " & NL &
				"WHERE ( NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") & NL &
					IIf(lbActive = True, " and Active = 1 ", "") & ")" & NL &
					IIf(isGUID(lsID), " OR ID = '" & lsID & "' ", "")


		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "Tiers")

		mdsTiers = ds
		LoadTiers = mdsTiers
	End Function
	Public Function DeleteTiers(ByVal RecordID As String, llNodeID As Long) As Boolean
		Dim lsSQL As String = "Update tblUnitTiers Set Active=False  WHERE NodeID=" & llNodeID & " AND ID='" & RecordID & "'"
		DeleteTiers = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function InsertTiers(ByVal Tier As String, ByVal llNodeID As Long) As Boolean

		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertTiers = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "INSERT INTO tblUnitTiers ( TierName, UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
				"Values ( """ & fTakeOutQuotes(Tier) & """, " & NL &
						  """" & Now.ToString & """, " & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
						  """" & Now.ToString & """, " & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
						   "true, " & llNodeID & "); "
		'*** Run The SQL.
		InsertTiers = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function

	Public Function UpdateTiers(ByVal TierID As Long, ByVal TierName As String, ByVal llNodeID As Long, ByVal lsActive As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		UpdateTiers = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			Exit Function
		End If
		lsSQL = "Update tblUnitTiers  "
		lsSQL = lsSQL & "Set TierName = """ & fTakeOutQuotes(TierName) & """,  Active = " & lsActive & NL
		lsSQL = lsSQL & " , NodeID = " & llNodeID & " " & NL
		lsSQL = lsSQL & " Where TierID=" & TierID & " AND NodeID = " & llNodeID & ";"
		'*** Run The SQL.
		UpdateTiers = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function



	'*************************************************
	'**** Vendors
	Public Function DeleteVendors(ByVal RecordID As String, llNodeID As Long) As Boolean
		Dim lsSQL As String = "Update tblVendors Set Active=False  WHERE NodeID=" & llNodeID & " AND ID='" & RecordID & "'"
		DeleteVendors = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function

	Public Function LoadVendors(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As String) As DataSet
		Dim lsSQL As String

		'*** Initialize
		LoadVendors = Nothing
		If lsWhere = Nothing Then lsWhere = ""
		lsWhere.Replace("like '%%'", "")
		If InStr(lsWhere, "'%%'") > 0 Then lsWhere = ""
		lsWhere = Replace(lsWhere, "SearchText", " UPPER( isnull(V.code,'') + isnull(V.name ,'') + isnull( V.description ,'')   ) ")
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "SELECT * " & NL &
				"FROM tblVendors V                                  " & NL &
			   "WHERE  NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") &
				IIf(lbActive = True, " and U.Active = 1 ", "") & NL &
				IIf(lsID = "", "", " and ID = '" & lsID & "' ")

		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "Vendors")

		mdsVendors = ds
		LoadVendors = mdsVendors
	End Function
	Public Function InsertVendors(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsContact As String, ByVal lsAbbreviation As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertVendors = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "INSERT INTO tblVendors ( VendorName, VendorContact, VendorAbbreviation, " &
				"                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
				"Values ( """ & fTakeOutQuotes(lsName) & """, " & NL &
						  """" & fTakeOutQuotes(lsContact) & """, " & NL &
						  """" & fTakeOutQuotes(lsAbbreviation) & """, " & NL &
						  """" & Now.ToString & """, " & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
						  """" & Now.ToString & """, " & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
						   "true, " & llNodeID & "); "
		'*** Load a data set.
		InsertVendors = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function UpdateVendors(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsContact As String, ByVal lsAbbreviation As String, ByVal lsActive As String, ByVal VendorID As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		UpdateVendors = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			Exit Function
		End If
		lsSQL = "Update tblVendors  " &
				"Set VendorName = """ & fTakeOutQuotes(lsName) & """, VendorContact = " & NL &
						  """" & fTakeOutQuotes(lsContact) & """, VendorAbbreviation= " & NL &
						  """" & fTakeOutQuotes(lsAbbreviation) & """, UpdateDate=" & NL &
						  """" & Now.ToString & """, UpdateUser=" & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL &
				 " Where VendorID=" & VendorID & ";"
		'*** Load a data set.
		UpdateVendors = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function




	'****************************************************
	'***UnitTypes
	Public Function LoadUnitTypes(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As String) As DataSet
		Dim lsSQL As String

		'*** Initialize
		LoadUnitTypes = Nothing
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		Dim lsSearch As String = lsWhere.ToUpper
		Dim liWHere As Integer = InStr(lsSearch, "SEARCHTEXT")
		If liWHere > 0 Then
			lsWhere = lsWhere.Replace("SearchText", " Upper( Name + Code + Description ) ")
		End If
		lsSQL = "SELECT * " & NL &
				"FROM tblUnitTypes                                   " & NL &
				"WHERE ( NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") &
				IIf(lbActive = True, " and Active = 1 ", "") & ")" & NL &
				IIf(isGUID(lsID), " OR ID = '" & lsID & "' ", "")

		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "UnitTypes")

		mdsUnitTypes = ds
		LoadUnitTypes = mdsUnitTypes
	End Function
	Public Function DeleteUnitTypes(ByVal RecordID As String, llNodeID As Long) As Boolean
		Dim lsSQL As String = "Update tblUnitTypes Set Active=False  WHERE NodeID=" & llNodeID & " AND ID='" & RecordID & "'"
		DeleteUnitTypes = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function InsertUnitTypes(ByVal llNodeID As Long, ByVal lsUnitTypeName As String, ByVal lsUnitTypeDescription As String, ByVal lsSubType As String, ByVal lsLevel As String, ByVal lsModelUnit As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertUnitTypes = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If


		lsSQL = "INSERT INTO tblUnitTypes (  UnitTypeName, UnitTypeDescription, SubType, [Level], ModelUnit, UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
				"Values ( """ & fTakeOutQuotes(lsUnitTypeName) & """, " & NL &
						  """" & fTakeOutQuotes(lsUnitTypeDescription) & """, " & NL &
						  """" & fTakeOutQuotes(lsSubType) & """, " & NL &
						  """" & fTakeOutQuotes(lsLevel) & """, " & NL &
						  """" & fTakeOutQuotes(lsModelUnit) & """, " & NL &
						  """" & Now.ToString & """, " & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
						  """" & Now.ToString & """, " & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
						   "true, " & llNodeID & "); "
		'*** Run The SQL.
		InsertUnitTypes = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function UpdateUnitTypes(ByVal llNodeID As Long, ByVal UnitTypeID As Long, ByVal UnitTypeName As String,
									ByVal UnitTypeDescription As String, ByVal SubType As String,
									ByVal lsLevel As String, ByVal ModelUnit As String,
									ByVal lsActive As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		UpdateUnitTypes = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			Exit Function
		End If



		lsSQL = "Update tblUnitTypes  " &
				"Set UnitTypeName = """ & fTakeOutQuotes(UnitTypeName) & """, UnitTypeDescription = " & NL &
						  """" & fTakeOutQuotes(UnitTypeDescription) & """, SubType= " & NL &
						  """" & fTakeOutQuotes(SubType) & """, [Level]=" & NL &
						  """" & fTakeOutQuotes(lsLevel) & """, ModelUnit=" & NL &
						  """" & fTakeOutQuotes(ModelUnit) & """, UpdateDate=" & NL &
						  """" & Now.ToString & """, UpdateUser=" & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL &
				 " Where UnitTypeID=" & UnitTypeID & ";"
		'*** Run The SQL.
		UpdateUnitTypes = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function


	'****************************************************
	'*** Company Information
	Public Function LoadCompanyInfo(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As String) As DataSet
		Dim lsSQL As String

		'*** Initialize
		LoadCompanyInfo = Nothing
		If lsWhere = Nothing Then lsWhere = ""
		lsWhere.Replace("like '%%'", "")
		If InStr(lsWhere, "'%%'") > 0 Then lsWhere = ""
		lsWhere = Replace(lsWhere, "SearchText", " UPPER( isnull(U.code,'') + isnull(U.name ,'') + isnull( U.description ,'')   ) ")
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "SELECT *                         " & NL &
				"FROM   tblCompanyInfo  as  U        " & NL &
				"WHERE  NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") & NL &
					IIf(lbActive = True, " and Active = 1 ", "") & " " & NL &
					IIf(isGUID(lsID), " OR U.ID = '" & lsID & "' ", "")


		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "CompanyInfo")

		mdsCompanyInfo = ds
		LoadCompanyInfo = mdsCompanyInfo
	End Function
	Public Function DeleteCompanyInfo(ByVal RecordID As String, llNodeID As Long) As Boolean
		Dim lsSQL As String = "Update tblCompanyInfo Set Active=False   WHERE  NodeID=" & llNodeID & " AND ID = '" & RecordID & "'"
		DeleteCompanyInfo = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function InsertCompanyInfo(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsContact As String, ByVal lsAbbreviation As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertCompanyInfo = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "INSERT INTO tblVendors ( VendorName, VendorContact, VendorAbbreviation, " &
				"                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
				"Values ( """ & fTakeOutQuotes(lsName) & """, " & NL &
						  """" & fTakeOutQuotes(lsContact) & """, " & NL &
						  """" & fTakeOutQuotes(lsAbbreviation) & """, " & NL &
						  """" & Now.ToString & """, " & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
						  """" & Now.ToString & """, " & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
						   "true, " & llNodeID & "); "
		'*** Run The SQL.
		InsertCompanyInfo = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function UpdateCompanyInfo(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsContact As String, ByVal lsAbbreviation As String, ByVal lsActive As String, ByVal VendorID As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		UpdateCompanyInfo = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			Exit Function
		End If
		lsSQL = "Update tblVendors  " &
				"Set VendorName = """ & fTakeOutQuotes(lsName) & """, VendorContact = " & NL &
						  """" & fTakeOutQuotes(lsContact) & """, VendorAbbreviation= " & NL &
						  """" & fTakeOutQuotes(lsAbbreviation) & """, UpdateDate=" & NL &
						  """" & Now.ToString & """, UpdateUser=" & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL &
				 " Where VendorID=" & VendorID & ";"
		'*** Run The SQL.
		UpdateCompanyInfo = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function

	'****************************************************
	'*** Requested Upgrades
	Public Function LoadUpgradeOptions(ByVal llNodeID As Long, ByVal lsRoom As String, ByVal lsPhase As String, ByVal lsQuoteID As String, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As String, Optional ByVal lsCat As String = "") As DataSet
		Dim lsSQL As String

		'*** Initialize
		LoadUpgradeOptions = Nothing
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If

		lsSQL = "SELECT * " &
				"FROM   tblUpgradeOptions " &
				"WHERE 1=1  " &
		IIf(lbActive = True, " and Active = 1 ", "") & NL
		If lsID = "00000000-0000-0000-0000-000000000000" Or Not isGUID(lsID) Then
			lsSQL = lsSQL & " AND RoomDescription = '" & lsRoom & "' " &
				"        AND BuildingPhase = '" & lsPhase & "' " &
				IIf(lsWhere.Length > 4, " AND " & lsWhere, "") &
				IIf(lsCat = "", "", " and UpgradeCategory =  '" & lsCat & "' ") & NL
		Else
			lsSQL = lsSQL & IIf(isGUID(lsID), " AND ID = '" & lsID & "' ", "") & NL
		End If
		lsSQL = lsSQL & "ORDER BY UpgradeCategory"


		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "UpgradeOptions")

		mdsCustomers = ds
		LoadUpgradeOptions = mdsCustomers
	End Function
    Public Function DeleteUpgradeOptions(ByVal RecordID As String, llNodeID As long) As Boolean
		If Not isGUID(RecordID) Then
			DeleteUpgradeOptions = False
			Exit Function
		End If
        Dim lsSQL As String = "UPDATE  tblUpgradeOptions set active = 0  WHERE  ID ='" & RecordID & "' and " & llNodeID & " in (select nodeid from tblUnits where UnitTypeID =  tblupgradeoptions.UnitTypeID)"
        DeleteUpgradeOptions = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function InsertUpgradeOptions(ByVal llNodeID As Long, UnitType As String,
	 Location As String, UpgradeCategory As String, UpgradeLevel As String,
	 Description As String, ModelOrStyle As String, Comments As String,
	 LeadVendor As String, CustomerPrice As Double, DeveloperPrice As Double,
	 ToVendorPrice As Double, BuildingPhase As Integer, PricingRevNumber As Integer,
	 OptionStatus As String, Standard As String, AdditionalFileToPrint1 As String,
	 AdditionalFileToPrint2 As String, lbActive As Boolean, UnitTypeID As String,
	 ID As String) As Boolean

		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertUpgradeOptions = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selected")
			Exit Function
		End If
		If Not isGUID(UnitTypeID) Or llNodeID = 0 Then
			InsertUpgradeOptions = False
			Exit Function
		End If
		lsSQL = "INSERT INTO tblUpgradeOptions
					(UnitType                ,Location               ,UpgradeCategory
					,UpgradeLevel            ,Description            ,ModelOrStyle     ,Comments
					,LeadVendor              ,CustomerPrice          ,DeveloperPrice   ,ToVendorPrice
					,BuildingPhase           ,PricingRevNumber       ,OptionStatus     ,Standard
					,AdditionalFileToPrint1  ,AdditionalFileToPrint2 ,UpdateDate       ,UpdateUser
					,CreateDate              ,CreateUser             ,Active           ,UnitTypeID           ,ID)
				VALUES
					( 
           '" & fTakeOutQuotes(UnitType) & "', 
           '" & fTakeOutQuotes(Location) & "', 
           '" & fTakeOutQuotes(UpgradeCategory) & "', 
           '" & fTakeOutQuotes(UpgradeLevel) & "', 
           '" & fTakeOutQuotes(Description) & "', 
           '" & fTakeOutQuotes(ModelOrStyle) & "', 
           '" & fTakeOutQuotes(Comments) & "',
            " & LeadVendor & ", 
            " & CustomerPrice & ", 
            " & DeveloperPrice & ",
            " & ToVendorPrice & ", 
           '" & fTakeOutQuotes(BuildingPhase) & "', 
            " & PricingRevNumber & ",
           '" & fTakeOutQuotes(OptionStatus) & "', 
            " & Standard & ",
           '" & fTakeOutQuotes(AdditionalFileToPrint1) & "', 
           '" & fTakeOutQuotes(AdditionalFileToPrint2) & "', 
            getDate(),  '" & fTakeOutQuotes(lsCurrentUser) & "',
		    getDate(),  '" & fTakeOutQuotes(lsCurrentUser) & "', 1,
           '" & fTakeOutQuotes(UnitTypeID) & "', 
           '" & fTakeOutQuotes(ID) & "')"


		'*** Run The SQL.
		InsertUpgradeOptions = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function


    Public Function UpdateUpgradeOptions(ByVal llNodeID As Long, UnitType As String,
	 Location As String, UpgradeCategory As String, UpgradeLevel As String,
	 Description As String, ModelOrStyle As String, Comments As String,
	 LeadVendor As String, CustomerPrice As Double, DeveloperPrice As Double,
	 ToVendorPrice As Double, BuildingPhase As Integer, PricingRevNumber As Integer,
	 OptionStatus As String, Standard As String, AdditionalFileToPrint1 As String,
	 AdditionalFileToPrint2 As String, lbActive As Boolean, UnitTypeID As String,
	 ID As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		UpdateUpgradeOptions = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			Exit Function
		End If


        lsSQL = "UPDATE tblUpgradeOptions
				SET 
					UnitType        =  '" & fTakeOutQuotes(UnitType) & "', 
					Location        =  '" & fTakeOutQuotes(Location) & "', 
					UpgradeCategory =  '" & fTakeOutQuotes(UpgradeCategory) & "', 
					UpgradeLevel    =  '" & fTakeOutQuotes(UpgradeLevel) & "', 
					Description     =  '" & fTakeOutQuotes(Description) & "', 
					ModelOrStyle    =  '" & fTakeOutQuotes(ModelOrStyle) & "', 
					Comments        =  '" & fTakeOutQuotes(Comments) & "',
					LeadVendor      =   " & LeadVendor & ", 
					CustomerPrice   =   " & CustomerPrice & ", 
					DeveloperPrice  =   " & DeveloperPrice & ",
					ToVendorPrice   =   " & ToVendorPrice & ", 
					BuildingPhase   =  '" & fTakeOutQuotes(BuildingPhase) & "', 
					PricingRevNumber  = " & PricingRevNumber & ",
					OptionStatus    =  '" & fTakeOutQuotes(OptionStatus) & "', 
					Standard        =   " & Standard & ",
					AdditionalFileToPrint1  =  '" & fTakeOutQuotes(AdditionalFileToPrint1) & "', 
					AdditionalFileToPrint2  =  '" & fTakeOutQuotes(AdditionalFileToPrint2) & "', 
					UpdateDate  =    getDate(),UpdateUser = '" & fTakeOutQuotes(lsCurrentUser) & "', Active=lsActive,
					UnitTypeID  =  '" & fTakeOutQuotes(UnitTypeID) & "', 
				WHERE ID = '" & ID & "' and " & llNodeID & " in and 1 in (select nodeid from tblUnits where UnitTypeID =  tblupgradeoptions.UnitTypeID);"
        '*** Run The SQL.
        UpdateUpgradeOptions = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	'************* END UPGRADE OPTIONS *********************************


	'**************************************
	'*** Report Builder Stuff
	'***************************************
	Public Function LoadReportCategories(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As String) As DataSet
		Dim lsSQL As String
		'llNodeID = 0
		'*** Initialize
		LoadReportCategories = Nothing
		lsWhere = Replace(lsWhere, "SearchText", " code + reportdescription ")
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If


		lsSQL = "Select reportcategorydescription as name, '' as Code, '' as Description, * from tblReportCategories  " &
				"WHERE  ( (NodeID=0 or NodeID=" & llNodeID & ") " & IIf(lsWhere.Length > 4, " And " & lsWhere, "") &
					IIf(lbActive = True, " And Active = 1 ", "") & ")" & NL &
					IIf(isGUID(lsID), " AND ID = '" & lsID & "' ", "")

		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "Categories")

		mdsReportCategories = ds
		LoadReportCategories = mdsReportCategories
	End Function
	Public Function InsertReportCategory(ByVal ReportCategoryID As Long, ByVal lsName As String, ByVal lsType As String, ByVal HideLists As Boolean) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertReportCategory = False
		'*** Check for No Selected Category

		lsSQL = "INSERT INTO tblReportCategories ( ReportCategoryID, ReportCategoryDescription, " &
				"                       ReportCategoryType, ReportCategoryHideLists, " &
				"                  UpdateDate, UpdateUser, CreateDate, CreateUser, Active,NodeID ) " &
				"Values ( " & ReportCategoryID & ", """ & fTakeOutQuotes(lsName) & """, " & NL &
						  """" & fTakeOutQuotes(lsType) & """, " & NL &
						  """" & fTakeOutQuotes(HideLists) & """, " & NL &
						  """" & Now.ToString & """, " & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
						  """" & Now.ToString & """, " & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
						   "true, " & "0" & "); "
		'*** Run The SQL.
		InsertReportCategory = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function UpdateReportCategory(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsCategoryType As String, ByVal lsActive As String, ByVal ID As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		UpdateReportCategory = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			Exit Function
		End If
		lsSQL = "Update tblReportCategories  " &
				"Set ReportCategoryDescription = """ & fTakeOutQuotes(lsName) & """, " &
						 "ReportCategoryType = " & NL &
						  """" & fTakeOutQuotes(lsCategoryType) & """ " & NL &
						  ", UpdateDate=" & NL &
						  """" & Now.ToString & """, UpdateUser=" & NL &
						  """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL &
				 " Where ReportCategoryID=" & ID & ";"
		'*** Run The SQL.
		UpdateReportCategory = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function
	Public Function DeleteReportCategory(ByVal RecordID As String, llNodeID As Long) As Boolean
		Dim lsSQL As String = "Update tblReportCategories Set Active=False   WHERE  NodeID=" & llNodeID & " AND ID = '" & RecordID & "'"
		DeleteReportCategory = fRunSQL(mscnType, mscnStr, lsSQL)
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
		LoadListItems = mdsReportControls

LoadListItemsExit:
		Exit Function
LoadListItemsError:
		''response.write(Err.Description)
		Resume LoadListItemsExit
	End Function

	Public Function LoadChildren(ByVal lsCnStr As String, ByVal lsSQL As String) As DataSet
		Dim lscnType As String = "OLEDB"

		On Error GoTo LoadChildrenError
		LoadChildren = New DataSet

		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(lscnType, lsCnStr, lsSQL, "Children")

		mdsChildren = ds
		LoadChildren = mdsReportControls

LoadChildrenExit:
		Exit Function
LoadChildrenError:
		'response.write(Err.Description)
		Resume LoadChildrenExit
	End Function
	'*******************************************************
	'*** Images
	'*******************************************************
	Public Function LoadImages(lsObjectID As String) As DataSet
		'ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As Long) As DataSet
		Dim llNodeID As Long '= 2
		Dim lsWhere As String = ""
		Dim lbActive As Boolean = True
		Dim lsID As String = ""

		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		'*** Initialize
		LoadImages = Nothing
		'llNodeID = 2
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "SELECT * " & NL &
				"FROM tblImages                                   " & NL &
				"WHERE ( tblImages.NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " And " & lsWhere, "") &
					 IIf(lbActive = True, " And Active = 1 ", "") & ")" & NL &
							   IIf(lsObjectID <> "", " And ( ObjectID = '" & lsObjectID & "'", "") & ")" & NL &
					 IIf(lsID <> "", " OR ID = '" & lsID & "' ", "")


		'*** Load a data set.
		Dim ds As New DataSet()
		' ds = fGetDataset(mscnType, lscnStr, lsSQL, "Projects")
		ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "Projects")
		mdsProjectTypes = ds
		LoadImages = mdsProjectTypes
	End Function
	Public Function DeleteImages(ByVal RecordID As String, llNodeID As Long) As Boolean
		Dim lscnStr As String = mscnDefault
		Dim lsSQL As String = "Update tblImage Set Active=False   WHERE  NodeID=" & llNodeID & " AND ID = '" & RecordID & "'"
		DeleteImages = fRunSQL("SQLConnection", lscnStr, lsSQL)
	End Function
	Public Function InsertImages(ByVal lsObjectID As String, ByVal llNodeID As Long, ByVal lsName As String,
								 lsDescription As String, ByVal liOrder As Integer, ByVal lsImage As Byte(),
								 ByVal lsType As String, lsURL As String) As Boolean
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		InsertImages = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If

		msSQLParameter = New SqlParameter("@pImage", SqlDbType.Image)
		msSQLParameter.Value = lsImage

		lsSQL = "INSERT INTO tblImages ( ObjectID, Name,  Description, Image, Type, ImageOrder, " &
				"                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
				"Values ( '" & fTakeOutQuotes(lsObjectID) & "', " & NL &
						  "'" & fTakeOutQuotes(lsName) & "', " & NL &
						  "'" & fTakeOutQuotes(lsDescription) & "', " & NL &
						  "@pImage " & fTakeOutQuotes("") & ", " & NL &
						  "'" & fTakeOutQuotes(lsType) & "', " & NL &
						   "" & fTakeOutQuotes(liOrder) & ", " & NL &
						  "'" & Now.ToString & "', " & NL &
						  " convert(uniqueidentifier,'" & fTakeOutQuotes(lsCurrentUser) & "'), " & NL &
						  "'" & Now.ToString & "', " & NL &
						  " convert(uniqueidentifier,'" & fTakeOutQuotes(lsCurrentUser) & "'), " & NL &
						   "1, " & llNodeID & "); "
		'*** Run The SQL.
		InsertImages = fRunSQL("SQLConnection", lscnStr, lsSQL)
	End Function


	Public Function Updateimages(ByVal lsObjectID As String, ByVal llNodeID As Long, ByVal lsName As String, ByVal lsDescription As String, ByVal lsImage As String, lsImageURL As String, ByVal lsActive As String, ByVal ID As String, ByVal liOrder As Integer) As Boolean
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		'*** Initialize
		Updateimages = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			Exit Function
		End If
		lsSQL = "Update tblImages  " &
				"Set Name = '" & fTakeOutQuotes(lsName) & "', Description = " & NL &
						  "'" & fTakeOutQuotes(lsDescription) & "', image= " & NL &
						  "'" & fTakeOutQuotes(lsImage) & "', ImageOrder= " & NL &
						   "" & fTakeOutQuotes(liOrder) & ", ImageURL= " & NL &
						  "'" & fTakeOutQuotes(lsImageURL) & "', UpdateDate=" & NL &
						  "'" & Now.ToString & "', UpdateUser=" & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "', Active = " & lsActive & NL &
				 " Where ID='" & ID & "';"
		'*** Run The SQL.
		Updateimages = fRunSQL("SQLConnection", lscnStr, lsSQL)
	End Function
	'*******************
	'*** Things
	'*******************
	Public Function LoadThings(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As String, lsTableName As String) As DataSet
		Dim lsSQL As String

		'*** Initialize
		LoadThings = Nothing
		If lsWhere = Nothing Then lsWhere = ""
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If
		lsSQL = "SELECT * " & NL &
				"FROM " & lsTableName & "                                   " & NL &
				"WHERE ( NodeID is null or NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") &
				IIf(lbActive = True, " and Active = 1 ", "") & ")" & NL &
				IIf(lsID Is Nothing, "", " OR ID = '" & lsID & "' ")

		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "UnitTypes")

		mdsUnitTypes = ds
		LoadThings = mdsUnitTypes
	End Function
	Public Function InsertThings(ByVal formValue As List(Of KeyValuePair(Of String, String)), ByVal lsTableName As String, ByVal lsID As String, ByVal llNodeID As Long,
								 ByVal lsName As String, lsDescription As String, ByVal lsCode As String) As Boolean

		Dim cmd As SqlCommand
		Dim c As Collection
		Dim lsSQL As String
		Dim lscnStr As String = mscnDefault
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString

		'*** Initialize
		InsertThings = False

		'*** Check for No Selected Node
		If llNodeID = 0 Then
			Exit Function
		End If
		If Not isGUID(lsID) Then
			Dim g As Guid
			g = Guid.NewGuid
			lsID = g.ToString
		End If
		c = New Collection
		cmd = New SqlCommand
		lsSQL = "INSERT INTO " & lsTableName & " ( ID, Code,  Name,  Description, " &
				"                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
				"Values ( '" & fTakeOutQuotes(lsID) & "', " & NL &
						  "'" & fTakeOutQuotes(lsCode) & "', " &
						  "'" & fTakeOutQuotes(lsName) & "', " & NL &
						  "'" & fTakeOutQuotes(lsDescription) & "', " & NL &
						  "'" & Now.ToString & "', " & NL &
						  " convert(uniqueidentifier,'" & fTakeOutQuotes(lsCurrentUser) & "'), " & NL &
						  "'" & Now.ToString & "', " & NL &
						  " convert(uniqueidentifier,'" & fTakeOutQuotes(lsCurrentUser) & "'), " & NL &
						   "1, " & llNodeID & "); "
		cmd.CommandText = lsSQL
		c.Add(cmd)

		For Each p As KeyValuePair(Of String, String) In formValue
			Select Case p.Key.ToUpper
				Case "ID", "CODE", "NAME", "DESCRIPTION", "ACTIVE"
				Case Else
					If Not p.Value Is Nothing Then
						cmd = New SqlCommand
						lsSQL = "Update " & lsTableName & "  " &
								"Set " & p.Key & " = @updateparam " &
								"Where ID='" & lsID & "';"
						cmd.CommandText = lsSQL
						cmd.Parameters.AddWithValue("updateparam", p.Value)
						c.Add(cmd)
					End If
			End Select
		Next



		'*** Run The SQL.
		InsertThings = fRunSQLCommands("SQLConnection", lscnStr, c)



	End Function
	Public Function UpdateThings(ByVal formValue As List(Of KeyValuePair(Of String, String)), ByVal llNodeID As Long, ByVal lsName As String, ByVal lsDescription As String, ByVal lsCode As String, ByVal ID As String, ByVal lsTable As String, lsActive As String) As Boolean
		Dim lsSQL As String
		Dim cmd As SqlCommand
		Dim c As Collection
		Dim lscnStr As String = mscnDefault
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
		If lsActive.ToUpper = "TRUE" Then lsActive = "1"
		If lsActive.ToUpper = "FALSE" Then lsActive = "0"
		'*** Initialize
		UpdateThings = False
		'*** Check for No Selected Category
		If llNodeID = 0 Then
			Exit Function
		End If
		c = New Collection
		cmd = New SqlCommand

		lsSQL = "Update " & lsTable & "  " &
				"Set Name = '" & fTakeOutQuotes(lsName) & "', Description = " & NL &
						  "'" & fTakeOutQuotes(lsDescription) & "', code= " & NL &
						  "'" & fTakeOutQuotes(lsCode) & "', UpdateDate=" & NL &
						  "'" & Now.ToString & "', UpdateUser=" & NL &
						  "'" & fTakeOutQuotes(lsCurrentUser) & "'  " & NL &
				 " Where ID='" & ID & "';"
		cmd.CommandText = lsSQL
		c.Add(cmd)

		For Each p As KeyValuePair(Of String, String) In formValue
			Select Case p.Key.ToUpper
				Case "ID", "CODE", "NAME", "DESCRIPTION", "ACTIVE", "OBJECTID"
				Case Else
					cmd = New SqlCommand
					lsSQL = "Update " & lsTable & "  " &
							 "Set " & p.Key & " = @updateparam " &
							 "Where ID='" & ID & "' and (" & p.Key & " != @updateparam or " & p.Key & " is null) and not (" & p.Key & " is null and @updateparam is null);"

					cmd.CommandText = lsSQL
					'*** is P.value nothing?  Could be null, could be 0 or ''
					If p.Value Is Nothing Or p.Value = "--" Then
						cmd.Parameters.AddWithValue("updateparam", DBNull.Value)
					Else
						cmd.Parameters.AddWithValue("updateparam", p.Value)
					End If
					Debug.Print(lsSQL & " PARAMVALUE:" & cmd.Parameters(0).SqlValue.ToString & " " & cmd.Parameters(0).Value)

					c.Add(cmd)
			End Select
		Next



		'*** Run The SQL.
		UpdateThings = fRunSQLCommands("SQLConnection", lscnStr, c)
	End Function
	Public Function DeleteThings(ByVal lsTable As String, ByVal RecordID As String, llNodeID As Long) As Boolean
		Dim lscnStr As String = mscnDefault
		Dim lsSQL As String = "Update " & lsTable & " Set Active=False  WHERE  NodeID = " & llNodeID & " ID='" & RecordID & "'"
		DeleteThings = fRunSQL("SQLConnection", lscnStr, lsSQL)
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
	'*******************************************************
	'*** Adhoc
	'*******************************************************
	Public Function LoadAdhoc(ByVal llNodeID As Long, ByVal lsSQL As String, ByVal lbActive As Boolean, ByVal lsID As String, lsObjectID As String) As DataSet

		Dim lscnStr As String = mscnDefault
		'*** Initialize
		LoadAdhoc = Nothing
		'If llNodeID = 0 Then llNodeID = 2

		If llNodeID = 0 Then
			'response.write("No Project Selectedd")
			Exit Function
		End If

		'*** Load a data set.
		Dim ds As New DataSet()
		' ds = fGetDataset(mscnType, lscnStr, lsSQL, "Contacts")
		ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "Contacts")
		mdsContacts = ds
		LoadAdhoc = mdsContacts
	End Function
	Private Function fGetDataset(ByVal lsConnectionType As String, ByVal lsCn As String, ByVal lsSQL As String, ByVal lsTableName As String) As DataSet
		Dim ds As DataSet = New DataSet
		Try
			Debug.Print("<fGetDataset>" & lsSQL & "</fGetDataset>")
			'    Threading.Thread.Sleep(5000)
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
		Catch
			Debug.Print(Err.Description)
		End Try
		fGetDataset = ds
	End Function
	Public Function fExecuteSQLCmd(ByVal lsConnectionType As String, ByVal lsCn As String, ByVal cms As SqlCommand) As Boolean

		Select Case lsConnectionType
			Case "SQLConnection"
				cns = New SqlClient.SqlConnection

				cns.ConnectionString = lsCn
				cns.Open()
				cms.Connection = cns


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
						Debug.Print(msErrorMsg)
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
	Public Function fgetSQLCommandDataSet(ByVal lsConnectionType As String, ByVal lsCn As String, ByVal cCommands As Collection, ByRef ds As DataSet, msg As String) As Boolean
		Try
			Select Case lsConnectionType
				Case "SQLConnection"
					cns = New SqlClient.SqlConnection
					Dim cms As New SqlClient.SqlCommand
					cns.ConnectionString = lsCn
					cns.Open()
					Dim da As SqlDataAdapter = New SqlDataAdapter()
					Dim dr As SqlDataReader
					Dim tb As DataTable
					tb = New DataTable()
					Try

						For Each cms In cCommands
							cms.Connection = cns
							dr = cms.ExecuteReader()
							tb.Load(dr)
							ds.Tables.Add(tb)
						Next

					Catch ex As Exception
						msErrorMsg = ex.Message
						fgetSQLCommandDataSet = False
						Exit Function
					End Try
					cns.Close()
				Case "OLEDB"

			End Select
			fgetSQLCommandDataSet = True
		Catch
			msErrorMsg = msErrorMsg & Err.Description
			fgetSQLCommandDataSet = False
		End Try
	End Function
	Public Function fRunSQL(ByVal lsConnectionType As String, ByVal lsCn As String, ByVal lsSQL As String) As Boolean
		Try
			Select Case lsConnectionType
				Case "SQLConnection"
					cns = New SqlClient.SqlConnection
					Dim cms As New SqlClient.SqlCommand
					cns.ConnectionString = lsCn
					cns.Open()
					cms.Connection = cns
					cms.CommandText = lsSQL
					Debug.Print("<frunSQL>" & lsSQL & "</frunSQL>")
					If msSQLParameter IsNot Nothing Then
						cms.Parameters.Add(msSQLParameter)
					End If
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
					cmo.CommandText = lsSQL
					cmo.ExecuteNonQuery()
					cno.Close()
			End Select
			fRunSQL = True
		Catch
			msErrorMsg = msErrorMsg & Err.Description
			fRunSQL = False
		End Try
	End Function
	Protected Overrides Sub Finalize()
		MyBase.Finalize()
	End Sub
	Function fTakeOutQuotes(ByVal lsStr As String) As String
		lsStr = Replace(lsStr, "script", "scri_pt")
		lsStr = Replace(lsStr, """", """""")
		lsStr = Replace(lsStr, "'", "''")
		fTakeOutQuotes = Trim(lsStr)
	End Function

	Public Function TrimAll(ByVal TextIn As String, Optional ByVal TrimChar As String = " ", Optional ByVal CtrlChar As String = Chr(0)) As String

		Try ' Replace ALL Duplicate Characters in String with a Single Instance
			TrimAll = Replace(TextIn, TrimChar, CtrlChar)   ' In case of CrLf etc.

			While InStr(TrimAll, CtrlChar + CtrlChar) > 0
				TrimAll = TrimAll.Replace(CtrlChar + CtrlChar, CtrlChar)
			End While

			TrimAll = TrimAll.Trim(CtrlChar)                ' Trim Begining and End
			TrimAll = TrimAll.Replace(CtrlChar, TrimChar)   ' Replace with Original Trim Character(s)
		Catch Exp As Exception
			TrimAll = TextIn    ' Justin Case
		End Try

		Return TrimAll

	End Function
	Public Function fReplaceQuotes(ByVal ls As String) As String
		fReplaceQuotes = Replace(ls, "'", "''")
	End Function
    Function fcheckforIncompatibilities(ByVal lSQuoteID As String, ByVal lsUpgradeOptionID As String) As DataSet

        Dim lsSQL As String = "Select  [dbo].[fcheckforIncompatibilities]( '" & lSQuoteID & "','" & lsUpgradeOptionID & "')"

        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Incompatibilities")


        fcheckforIncompatibilities = ds


    End Function
    Function fcheckforIncompatibilitiesOLD(ByVal llQuoteID As Long,
            ByVal lsRoomDesc As String,
            ByVal lsUpgradeDesc As String,
            ByVal lsUpgradeCategory As String,
            ByVal lsUpgradeClass As String,
            ByVal lsStyle As String,
            ByVal llUpgradeOptionID As Long, ByRef lsMsg As String) As Boolean

        fcheckforIncompatibilitiesOLD = False
        Dim lsUnit As String
        Dim lsUnitType As String
        Dim lsSQL As String
        Dim d As DataSet

        Dim rs As DataTable
        Dim rs2 As DataTable
        Dim r As DataRow
        Dim r2 As DataRow
        lsSQL = "SELECT tblUnits.UnitName, tblUnitTypes.UnitTypeName, tblQuote.QuoteID
				 FROM tblUnitTypes LEFT JOIN (tblUnits LEFT JOIN tblQuote ON tblUnits.UnitID = tblQuote.UnitID) ON 
                      tblUnitTypes.UnitTypeID = tblUnits.UnitTypeID 
                 WHERE tblQuote.QuoteID=" & llQuoteID & ";"

        d = fGetDataset(mscnType, mscnStr, lsSQL, "Comapt")
        rs = d.Tables(0)
        lsUnit = rs.Rows(0).Item(0).ToString
        lsUnitType = rs.Rows(0).Item(1).ToString


        Dim liUnit As Integer
        Dim liSection As Integer
        Dim lsUnitText As String
        Dim lsRoomText As String
        Dim liRoom As Integer
        Dim liSectionOther As Integer
        '*** Loop for Unit, Then Unittype
        For liUnit = 1 To 2
            If liUnit = 1 Then
                lsUnitText = " EntityType = 2 and EntityID = '" & lsUnit & "' "
            Else
                lsUnitText = " EntityType = 1 and EntityID = '" & lsUnitType & "' "
            End If
            '*** Loop for first Section, Then Second
            For liSection = 1 To 2
                If liSection = 1 Then
                    liSectionOther = 2
                Else
                    liSectionOther = 1
                End If
                '*** Loop For Room
                For liRoom = 1 To 2
                    '*** Our Table of INcompats has a first and a second (where the first selction is incomatible with the second
                    '*** We will loop twice processing all of the First Items then the second
                    If liRoom = 1 Then
                        lsRoomText = " Location" & liSection & " = '" & lsRoomDesc & "'  "
                    Else
                        lsRoomText = " Location" & liSection & " = '(Anywhere)'  "
                    End If
                    '*** Are there any records in the incompatible table for the selection we are looking at?
                    lsSQL = "Select * from tblIncompatibilities where " & lsUnitText & " AND " & lsRoomText & " 
                                AND (Category" & liSection & "=""" & fReplaceQuotes(lsUpgradeCategory) & """ OR Category" & liSection & "='(Anything)' ) 
				                AND (Class" & liSection & "=""" & fReplaceQuotes(lsUpgradeClass) & """ OR Class" & liSection & "='(Anything)' )  
                                AND (Description" & liSection & "=""" & fReplaceQuotes(lsUpgradeDesc) & """ OR Description" & liSection & "='(Anything)' )  
                                AND (ModelOrStyle" & liSection & "=""" & fReplaceQuotes(lsStyle) & """ OR ModelOrStyle" & liSection & "='(Anything)' )
                                AND (severity = 1 or severity = 2 )"
                    'Debug.Print(lsSQL)

                    d = fGetDataset(mscnType, mscnStr, lsSQL, "compat")
                    rs = d.Tables("compat")

                    '*** Loop through all Matches for the Incompatibilities table that we found and see if we actually have any of those selections
                    If rs.Rows.Count > 0 Then
                        r = rs.Rows(0)
                        ' Debug.Print("Found First: " & lsSQL)
                        '*** Found THIS item in Inc List, Let's check for another in the requested Upgrades
                        lsSQL = "Select * from tblRequestedUpgrades where quoteid = " & llQuoteID & " "
                        If r("Location" & liSectionOther) <> "(Anywhere)" Then
                            lsSQL = lsSQL & " AND RoomDescription = '" & r("Location" & liSectionOther) & "' "
                        End If

                        If r("Category" & liSectionOther) <> "(Anything)" Then
                            lsSQL = lsSQL & " AND UpgradeCategory = """ & fReplaceQuotes(r("Category" & liSectionOther)) & """ "
                        End If

                        If r("Description" & liSectionOther) <> "(Anything)" Then
                            lsSQL = lsSQL & " AND UpgradeDescription = """ & fReplaceQuotes(r("Description" & liSectionOther)) & """ "
                        End If

                        If r("Class" & liSectionOther) <> "(Anything)" Then
                            lsSQL = lsSQL & " AND UpgradeClass = """ & fReplaceQuotes(r("Class" & liSectionOther)) & """ "
                        End If

                        If r("ModelOrStyle" & liSectionOther) <> "(Anything)" Then
                            lsSQL = lsSQL & " AND StyleDescription" & liSectionOther & " = """ & fReplaceQuotes(r("ModelOrStyle" & liSectionOther)) & """ "
                        End If
                        '   Debug.Print(lsSQL)

                        d = fGetDataset(mscnType, mscnStr, lsSQL, "rs2")
                        rs2 = d.Tables("rs2")
                        If rs2.Rows.Count > 0 Then
                            r2 = rs2.Rows(0)
                            lsMsg = lsMsg & ("The item you are trying to add is incompatible with the following Rule.")
                            lsMsg = lsMsg & r2.Item("IncompatibilityID").ToString '  .DoCmd.OpenForm("frmIncompatMSG", acNormal, , "IncompatibilityID=" & rs("IncompatibilityID"), acFormReadOnly, acDialog)
                            '*** gbContinue comes back from the frmIncompatMSG form and indicates whether the
                            '*** item was only a warning and the user said Continue or whether is was denied
                            '    If Session("gbContinue") = True Then
                            fcheckforIncompatibilitiesOLD = False
                            'Else
                            '   fcheckforIncompatibilities = True
                            'End If
                        End If
                        rs2.Dispose()
                        ' rs2.Close()
                        ' rs.MoveNext()
                    End If
                    rs.Dispose()
                    'rs.Close()
                Next liRoom
                '*** End Room Loop
            Next liSection
            '*** End Section Loop
        Next liUnit
        '*** End Unit Loop
        rs = Nothing
        rs2 = Nothing
    End Function
    '*************************************
    '*** Data Validation Functions
    '*************************************


    Dim gbContinue As Boolean
	Dim gbMissingSelections As Boolean
	Sub test()
		Dim dt As DataTable = New DataTable
		fRequired(1, 2, dt)
	End Sub
	Function fRequired(ByVal llQuoteID As Long, ByVal lsPhaseID As String, ByVal dt As DataTable) As Boolean
		fRequired = fCheckForRequiredItems(llQuoteID, lsPhaseID, dt)

		If fRequired = True Then
			Exit Function
		Else
			fRequired = fMissingSelections(llQuoteID, "Form", lsPhaseID)
		End If
	End Function
	Sub testreq()
		Dim dt As New DataTable
		'response.write(fRequired(1, 2, dt))
	End Sub

	Function fCheckForRequiredItems(ByVal llQuoteID As Long, ByVal lsPhaseID As String, ByVal dtIncompat As DataTable) As Boolean
		'*** THIS IS FROM THE INCOMPATIBILITIES LIST
		Dim d As DataSet
		Dim r As DataRow
		Dim rsRule As DataTable 'ADODB.Recordset
		Dim rs As DataTable 'ADODB.Recordset
		Dim rs2 As DataTable 'ADODB.Recordset
		Dim lsUnit As String
		Dim lsUnitType As String
		Dim lsSQL As String
		Dim liUnit As Integer
		Dim liSection As Integer
		Dim lsUnitText As String
		Dim lsRoomText As String
		Dim liRoom As Integer
		Dim liSectionOther As Integer


		Dim lsRoomDesc As String
		Dim lsUpgradeDesc As String
		Dim lsUpgradeCategory As String
		Dim lsUpgradeClass As String
		Dim lsStyle As String
		Dim llUpgradeOptionID As Long


		If lsPhaseID = "1" Then
			fCheckForRequiredItems = False
			Exit Function
		End If
		On Error GoTo fCheckForRequiredItems_Error

		fCheckForRequiredItems = False
		rsRule = New DataTable ' ADODB.Recordset
		rs2 = New DataTable 'ADODB.Recordset
		rs = New DataTable 'ADODB.Recordset

		'*** Get Unit and Unit Type for this Quote
		lsSQL = "SELECT tblUnits.UnitName, tblUnitTypes.UnitTypeName, tblQuote.QuoteID " &
		   "FROM tblUnitTypes LEFT JOIN (tblUnits LEFT JOIN tblQuote ON tblUnits.UnitID = tblQuote.UnitID) ON " &
		   "tblUnitTypes.UnitTypeID = tblUnits.UnitTypeID " &
		   "WHERE (((tblQuote.QuoteID)=" & llQuoteID & "));"

		d = fGetDataset(mscnType, mscnStr, lsSQL, "Required")
		rs = d.Tables("Required")


		If rs.Rows.Count > 0 Then
			r = rs.Rows(0)

			lsUnit = r(0)
			lsUnitType = r(1)
			rs.Dispose()


			'*** Loop for Unit, Then UnitType
			For liUnit = 1 To 2
				If liUnit = 1 Then
					lsUnitText = " EntityType = 2 and EntityID = '" & lsUnit & "' "
				Else
					lsUnitText = " EntityType = 1 and EntityID = '" & lsUnitType & "' "
				End If

				'*** Loop for first Section, Then Second
				For liSection = 1 To 1 '2 *** Do not want to go backward at this juncture  Only Check First Item against Second, Not Second Against First
					If liSection = 1 Then
						liSectionOther = 2
					Else
						liSectionOther = 1
					End If

					'*** Get all required Items for that Unit / Unit Type
					lsSQL = "Select * from tblIncompatibilities where " & lsUnitText
					lsSQL = lsSQL & " AND (Severity = 3 )  "

					' Debug.Print(lsSQL)
					d = fGetDataset(mscnType, mscnStr, lsSQL, "Rules")
					rsRule = d.Tables("Rules")

					Dim liRowCount As Integer
					If rsRule.Rows.Count > 0 Then
						For liRowCount = 0 To rsRule.Rows.Count - 1
							r = rsRule.Rows(liRowCount)
							'rsRule.Open(lsSQL, CurrentProject.Connection)


							lsRoomDesc = r("Location" & liSection)
							lsUpgradeDesc = r("Description" & liSection)
							lsUpgradeCategory = r("Category" & liSection)
							lsUpgradeClass = r("Class" & liSection)
							lsStyle = r("ModelOrStyle" & liSection)


							'*** Was one of the Base Items Selected?
							lsSQL = "Select * from tblRequestedUpgrades where quoteid = " & llQuoteID & " "
							If lsRoomDesc <> "(Anywhere)" Then lsSQL = lsSQL & " AND RoomDescription = '" & lsRoomDesc & "' "
							If lsUpgradeCategory <> "(Anything)" Then lsSQL = lsSQL & " AND UpgradeCategory = '" & lsUpgradeCategory & "' "
							If lsUpgradeDesc <> "(Anything)" Then lsSQL = lsSQL & " AND UpgradeDescription = '" & lsUpgradeDesc & "' "
							If lsUpgradeClass <> "(Anything)" Then lsSQL = lsSQL & " AND UpgradeClass = '" & lsUpgradeClass & "' "
							If lsStyle <> "(Anything)" Then lsSQL = lsSQL & " AND StyleDescription" & liSectionOther & " = '" & lsStyle & "' "
							' Debug.Print(lsSQL)

							' rs.Open(lsSQL, CurrentProject.Connection)
							d = fGetDataset(mscnType, mscnStr, lsSQL, "Found")
							rs = d.Tables("Found")

							Dim liRowCount2 As Integer
							If rs.Rows.Count > 0 Then
								'*** We Found Something
								'*** Found THIS item in Inc List, Let's check for another in the requested Upgrades
								lsSQL = "Select * from tblRequestedUpgrades where quoteid = " & llQuoteID & " "
								If r("Location" & liSectionOther) <> "(Anywhere)" Then lsSQL = lsSQL & " AND RoomDescription = '" & r("Location" & liSectionOther) & "' "
								If r("Category" & liSectionOther) <> "(Anything)" Then lsSQL = lsSQL & " AND UpgradeCategory = '" & r("Category" & liSectionOther) & "' "
								If r("Description" & liSectionOther) <> "(Anything)" Then lsSQL = lsSQL & " AND UpgradeDescription = '" & r("Description" & liSectionOther) & "' "
								If r("Class" & liSectionOther) <> "(Anything)" Then lsSQL = lsSQL & " AND UpgradeClass = '" & r("Class" & liSectionOther) & "' "
								If r("ModelOrStyle" & liSectionOther) <> "(Anything)" Then lsSQL = lsSQL & " AND StyleDescription" & liSectionOther & " = '" & r("ModelOrStyle" & liSectionOther) & "' "

								'  Debug.Print(lsSQL)

								' rs2.Open(lsSQL, CurrentProject.Connection)
								d = fGetDataset(mscnType, mscnStr, lsSQL, "Found2")
								rs2 = d.Tables("Found2")
								Dim r2 As DataRow

								Dim liRowCount3 As Integer
								If rs2.Rows.Count = 0 Then

									'response.write("An item you selected requires you to select another item compatible with the following Rule.")

									dtIncompat = New DataTable ' DoCmd.OpenForm("frmIncompatMSG", acNormal, , "IncompatibilityID=" & rsRule("IncompatibilityID"), acFormReadOnly, acDialog)
									'*** gbContinue comes back from the frmIncompatMSG form and indicates whether the
									'*** item was only a warning and the user said Continue or whether is was denied

									fCheckForRequiredItems = True

								End If
								rs2.Dispose()

							End If
							rs.Dispose()


							''*** End Room Loop

						Next
					End If
					rsRule.Dispose()
					'*** End Rule Loop

				Next liSection
				'*** End Section Loop
			Next liUnit
			'*** End Unit Loop

			rs = Nothing
			rs2 = Nothing
			rsRule = Nothing

		End If

fCheckForRequiredItems_Exit:
		Exit Function

fCheckForRequiredItems_Error:
		'response.write(Err.Description & " " & Err.Number)
		Resume fCheckForRequiredItems_Exit
	End Function
	Sub testmissing()
		fMissingSelections(1, "", True)
	End Sub
	Function fMissingSelections(ByVal lsQuoteID As String, ByVal lsOutput As String, ByVal lsPhase As String) As Boolean
		Dim lsSQL As String

		lsSQL = "Delete * from tempselections Where quoteid = '" & lsQuoteID & "';"
		fMissingSelections = fRunSQL(mscnType, mscnStr, lsSQL)

		lsSQL = "Insert into TempSelections (   UnitType, UnitTypeID, Location, UpgradeCategory,          QuoteID, BuildingPhase, UnitName, ValueExists, Required ) " &
			"SELECT  Distinct  tblUpgradeOptions.UnitType,  tblUnitTypes.ID as UnitTypeID, tblUpgradeOptions.Location, tblUpgradeOptions.UpgradeCategory,  " &
			"         tblQuote.ID as quoteID, tblUpgradeOptions.BuildingPhase, tblUnits.UnitName, 0 AS ValueExists , 0 as Optionrequired " &
			"FROM     (tblUnitTypes LEFT JOIN (tblUnits LEFT JOIN tblQuote ON tblUnits.ID = tblQuote.UnitID)   " &
			"         ON tblUnitTypes.ID = tblUnits.UnitTypeID) INNER JOIN tblUpgradeOptions ON   " &
			"         tblUnitTypes.UnitTypeName = tblUpgradeOptions.UnitType  " &
			"where   tblUpgradeOptions.BuildingPhase = '" & lsPhase & "'  " &
			" And tblQuote.ID ='" & lsQuoteID & "';"

		'Debug.Print(lsSQL)

		fMissingSelections = fRunSQL(mscnType, mscnStr, lsSQL)

		'*** Take out Electrical
		lsSQL = "Delete * from tempselections where ucase(left(upgradeCategory,10)) = 'ELECTRICAL' where QuoteID='" & lsQuoteID & "';"
		fMissingSelections = fRunSQL(mscnType, mscnStr, lsSQL)

		'*** Update record if Value has been selected
		lsSQL = "UPDATE s set valueexists = 1 " &
				"FROM " &
				"tblRequestedUpgrades u INNER JOIN tempSelections s ON  " &
				"      (u.UpgradeCategory = s.UpgradeCategory) AND  " &
				"      (u.RoomDescription = s.Location) AND   " &
				"      (u.QuoteID         = s.QuoteID)       " &
				"WHERE  s.QuoteID ='" & lsQuoteID & "' and u.active = 1;"

		'Debug.Print(lsSQL)
		fMissingSelections = fRunSQL(mscnType, mscnStr, lsSQL)

		'*** Update the Required Field Based on the CategorySummary Table
		lsSQL = "UPDATE s 	" &
				"SET    s.Required = 1 	" &
				"FROM   tempSelections s INNER JOIN tblUpgradeCategorySummary u ON 	" &
				"       s.UpgradeCategory = u.UpgradeCategory 	" &
				"WHERE  u.Required=1 and s.QUoteID'" & lsQuoteID & "';"

		'Debug.Print(lsSQL)
		fMissingSelections = fRunSQL(mscnType, mscnStr, lsSQL)


		'*** Delete Rows where Upgrade Record Exists for the unit but Room is not defined in the
		'*** Unit Profile
		lsSQL = "SELECT tblUnitTypes.UnitTypeName, " &
				"tblUnitTypes.UnitTypeDescription, " &
				"tblUnitTypes.UnitTypeCode,  " &
				"tblUnitProfiles.RoomID,      " &
				"r.Name as RoomName, " &
				"r.Description as RoomDescription, " &
				"tblQuote.ID " &
				"FROM (tblUnitTypes  " &
				"     LEFT JOIN tblUnitProfiles ON          tblUnitTypes.ID = tblUnitProfiles.UnitTypeID)          " &
				"	  LEFT JOIN (tblUnits  " &
				"	  LEFT JOIN tblQuote ON     tblUnits.ID = tblQuote.UnitID) ON         tblUnitTypes.ID = tblUnits.UnitTypeID  " &
				"	  LEFT JOIN tblRooms r on tblunitprofiles.RoomID= r.ID " &
				"WHERE  tblQuote.ID = '" & lsQuoteID & "';"

		'*** Load a data set.
		Dim ds As New DataSet()
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "Rooms")
		Dim dt As New DataTable
		dt = ds.Tables(0)
		Dim dr As DataRow

		lsSQL = "Delete * from TempSelections Where 1=1 and quoteid='" & lsQuoteID & "' "
		For Each dr In dt.Rows
			lsSQL = lsSQL & " And location <> '" & fReplaceQuotes(dr("RoomName")) & "' "
		Next
		dt.Dispose()

		'Debug.Print(lsSQL)
		fMissingSelections = fRunSQL(mscnType, mscnStr, lsSQL)


		lsSQL = "select * from tempselections where valueexists = 0 and quoteID ='" & lsQuoteID & "';"
		ds = fGetDataset(mscnType, mscnStr, lsSQL, "TempSelections")

		If ds.Tables(0).Rows.Count > 0 Then
			'*** There are No Values missing
			fMissingSelections = True

		Else
			If lsOutput = "Report" Then
				'*** Put this back DoCmd.OpenReport("rptSelectedItems", acViewPreview, , , acWindowNormal)
			Else
				'*** Put this back DoCmd.OpenForm("frmmissingselections", acNormal, , , acFormReadOnly, acDialog, "Visible")
			End If

			If gbMissingSelections = True Then
				fMissingSelections = False
			Else
				fMissingSelections = True
			End If
		End If
		dt = Nothing
		ds = Nothing

	End Function

	Public Function UpdateQuoteStatus(ByVal llQuoteID As Long, ByVal lsPhase As String, ByVal lsStatus As String, ByVal lsPhaseDate As String, ByVal lsDate As String) As Boolean
		Dim lsSQL As String
		Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString

		'*** Initialize
		UpdateQuoteStatus = False
		'*** Check for No Selected Category
		If llQuoteID = 0 Or lsStatus = "" Then
			Exit Function
		End If

		lsSQL = "Update tblQuote  " &
				"Set " & lsPhase & " = '" & fTakeOutQuotes(lsStatus) & "',  " & NL &
						  lsPhaseDate & " = '" & lsDate & "' " & NL &
				 " Where QuoteID=" & llQuoteID & ";"
		'*** Run The SQL.
		UpdateQuoteStatus = fRunSQL(mscnType, mscnStr, lsSQL)
	End Function

End Class

